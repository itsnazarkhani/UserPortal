using System.ComponentModel.DataAnnotations;

namespace UserPortal.Core.ValueObjects;

/// <summary>
/// Represents an email value object which can be sent.
/// </summary>
/// <param name="ReceiverEmailAddress">Email address which is going to receive the email.</param>
/// <param name="Subject">Subject of the email.</param>
/// <param name="Body">Body section of email.</param>
public record class Email(
    [Required]
    string ReceiverEmailAddress,
    [Required]
    string Subject,
    string? Body
);