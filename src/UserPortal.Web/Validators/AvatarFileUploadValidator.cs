using FluentValidation;
using UserPortal.Web.Models;

namespace UserPortal.Web.Validators
{
    public class AvatarFileUploadValidator : AbstractValidator<AvatarFileUpload>
    {
        public AvatarFileUploadValidator()
        {
            RuleFor(x => x.File).Must((file, context) =>
            {
                return file.File.ContentType == "image/jpeg";
            });

            RuleFor(x => x.File).Must((file, context) =>
            {
                return file.File.FileName.EndsWith(".jpg");
            }).WithMessage("The file extension type is not valid.");

            RuleFor(x => x.File.FileName)
                .Matches(@"^[A-Za-z0-9_.-]*$")
                .WithMessage("The file name is not valid.");


            RuleFor(x => x).Must((file, context) =>
            {
                var exeSignatures = new List<string>()
                {
                    "4D-5A",
                    "5A 4D"
                };

                BinaryReader binary = new BinaryReader(file.File.OpenReadStream());
                byte[] bytes = binary.ReadBytes(2);
                string fileSequenceHex = BitConverter.ToString(bytes);
                foreach (var exesignature in exeSignatures)
                {
                    if (exesignature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                        return false;
                }
                return true;
            }).WithName("FileContent")
              .WithMessage("The file content is not valid");
        }
    }
}
