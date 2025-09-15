using System;
using FluentValidation;

namespace UserPortal.UseCases.Validations.Rules;

public interface IEmailValidationRules
{
    string EmptyMessage { get; }
    string InvalidMessage { get; }
}