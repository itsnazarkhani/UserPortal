using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPortal.UseCases.DTOs;
using UserPortal.UseCases.Validations.Configurations;
using UserPortal.UseCases.Validations.Rules;

namespace UserPortal.UseCases.Validations.DTOs
{
    public class UserSettingsDtoValidator : AbstractValidator<UserSettingsDto>
    {
        public UserSettingsDtoValidator(
            IValidationModeConfig validationModeConfig)
        {
            RuleLevelCascadeMode = validationModeConfig.RuleLevelCascadeMode;
            ClassLevelCascadeMode = validationModeConfig.ClassLevelCascadeMode;

            // FirstName validation
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithMessage("First name cannot exceed 100 characters.");

            // LastName validation
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            // Avatar validation - only validate if provided
            When(x => x.Avatar != null && x.Avatar.Length > 0, () =>
            {
                RuleFor(x => x.Avatar)
                    .Must(file => file.ContentType == "image/jpeg" ||
                                  file.ContentType == "image/jpg" ||
                                  file.ContentType == "image/png" ||
                                  file.ContentType == "image/gif")
                    .WithMessage("Only JPEG, PNG, and GIF images are allowed.");

                RuleFor(x => x.Avatar)
                    .Must(file => file.FileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                  file.FileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                  file.FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                  file.FileName.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    .WithMessage("The file extension type is not valid.");

                RuleFor(x => x.Avatar!.FileName)
                    .Matches(@"^[A-Za-z0-9_.-]*$")
                    .WithMessage("The file name contains invalid characters.");

                RuleFor(x => x.Avatar)
                    .Must(file =>
                    {
                        // Check file size (e.g., max 5MB)
                        const long maxFileSize = 5 * 1024 * 1024; // 5MB
                        return file?.Length <= maxFileSize;
                    })
                    .WithMessage("File size must not exceed 5MB.");

                RuleFor(x => x.Avatar)
                    .Must(file =>
                    {
                        var exeSignatures = new List<string>()
                        {
                            "4D-5A", // MZ (DOS/Windows executable)
                            "5A-4D"  // ZM (reversed)
                        };

                        using (var binary = new BinaryReader(file.OpenReadStream()))
                        {
                            if (file.Length < 2)
                                return false;

                            byte[] bytes = binary.ReadBytes(2);
                            string fileSequenceHex = BitConverter.ToString(bytes);

                            foreach (var exeSignature in exeSignatures)
                            {
                                if (exeSignature.Equals(fileSequenceHex, StringComparison.OrdinalIgnoreCase))
                                    return false;
                            }
                        }

                        return true;
                    })
                    .WithMessage("The file content is not valid or appears to be an executable.");
            });
        }
    }
}