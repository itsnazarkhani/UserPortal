using System;
using FluentValidation;
using UserPortal.UseCases.Validations.Configurations;

namespace UserPortal.UseCases.Tests.TestDoubles;

public class FakeValidationModeConfig : IValidationModeConfig
{
    public CascadeMode RuleLevelCascadeMode => CascadeMode.Stop;
    public CascadeMode ClassLevelCascadeMode => CascadeMode.Continue;
}