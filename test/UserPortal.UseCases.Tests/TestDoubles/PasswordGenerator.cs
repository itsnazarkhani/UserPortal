using Bogus;

namespace UserPortal.UseCases.Tests.TestDoubles;

public static class PasswordGenerator
{
    private static readonly char[] SpecialChars = "@#!$%^&*?".ToCharArray();
    private static readonly Faker Faker = new("fa");

    public static string GenerateSecurePassword(int length = 12)
    {
        if (length < 4) throw new ArgumentException("length must be at least 4", nameof(length));

        var lower = Faker.Random.Char('a', 'z');
        var upper = Faker.Random.Char('A', 'Z');
        var digit = Faker.Random.Char('0', '9');
        var special = Faker.PickRandom(SpecialChars);

        var allChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@#!$%^&*?";
        var remaining = Faker.Random.String2(length - 4, allChars);

        var passwordChars = new List<char> { lower, upper, digit, special };
        passwordChars.AddRange(remaining);

        return new string(passwordChars.OrderBy(_ => Faker.Random.Int()).ToArray());
    }
}
