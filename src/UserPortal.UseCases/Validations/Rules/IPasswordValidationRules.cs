using System;
using FluentValidation;

namespace UserPortal.UseCases.Validations.Rules;

public interface IPasswordValidationRules
{
    int MinLength { get; }
    int MaxLength { get; }
    string Pattern { get; }
    string EmptyMessage { get; }
    string LengthMessage { get; }
    string PatternMessage { get; }
}