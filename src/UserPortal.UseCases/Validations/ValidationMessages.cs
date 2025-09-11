using System;

namespace UserPortal.UseCases.Validations;

public static class ValidationMessages
{
    public static string ValidLengthRange(string something, int min, int max) =>
        $"{something} باید حداقل {min} کاراکتر و حداکثر {max} کاراکتر باشد.";
}
