using System;

namespace UserPortal.Core.Constants;

public static class ValidationConstants
{
    public static class Password
    {
        public const int MinLength = 6;
        public const int MaxLength = 120;
    }

    public static class UserName
    {
        public const int MinLength = 3;
        public const int MaxLength = 50;
    }
}
