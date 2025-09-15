using FluentValidation;

namespace UserPortal.UseCases.Validations.Configurations;

public class DefaultValidationModeConfig : IValidationModeConfig
{
    public CascadeMode RuleLevelCascadeMode => CascadeMode.Stop;
    public CascadeMode ClassLevelCascadeMode => CascadeMode.Continue;
}