using FluentValidation;

namespace UserPortal.UseCases.Validations.Configurations;

public interface IValidationModeConfig
{
    CascadeMode RuleLevelCascadeMode { get; }
    CascadeMode ClassLevelCascadeMode { get; }
}