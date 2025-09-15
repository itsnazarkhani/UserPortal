using Bogus;
using UserPortal.UseCases.Tests.TestDoubles;
using UserPortal.UseCases.Validations.Configurations;

namespace UserPortal.UseCases.Tests.ValidationTests;

public abstract class ValidatorTestBase
{
    protected readonly Faker Faker = new("fa");
    protected readonly FakeEmailValidationRules EmailRules = new();
    protected readonly FakePasswordValidationRules PasswordRules = new();
    protected readonly IValidationModeConfig ValidationMode = new FakeValidationModeConfig();
}
