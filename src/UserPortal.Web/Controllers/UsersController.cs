using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UserPortal.Infrastructure.Data.UnitOfWork;
using UserPortal.Web.Models;

namespace UserPortal.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IWebHostEnvironment env;
        public readonly IUnitOfWork unitOfWork;

        public UsersController(IWebHostEnvironment env, IUnitOfWork unitOfWork)
        {
            this.env = env;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("{userId:guid}/avatar")]
        public async Task<IActionResult> UpdateUserImage(
            Guid userId,
            IFormFile file,
            [FromServices] IValidator<AvatarFileUpload> validator,
            CancellationToken cancellationToken = default)
        {
            // Validate file
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
            if (user == null)
                return NotFound("User not found.");

            var avatarFileModel = new AvatarFileUpload() { File = file };

            ValidationResult result = await validator.ValidateAsync(avatarFileModel);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                return BadRequest(errors);
            }

            var uploadPath = Path.Combine(env.WebRootPath, "avatars");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            if (user.AvatarId != Guid.Empty)
            {
                var oldAvatarPath = Path.Combine(uploadPath, $"{user.AvatarId}.jpg");
                if (System.IO.File.Exists(oldAvatarPath))
                    System.IO.File.Delete(oldAvatarPath);
            }

            var newAvatarId = Guid.NewGuid();
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{newAvatarId}{fileExtension}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
                await file.CopyToAsync(stream);

            user.AvatarId = newAvatarId;
            user.UpdatedAt = DateTime.UtcNow;

            unitOfWork.Users.Update(user);
            await unitOfWork.CommitAsync(cancellationToken);

            var fileUrl = $"{Request.Scheme}://{Request.Host}/avatars/{fileName}";
            return Created(fileUrl, new { Url = fileUrl });
        }
    }
}
