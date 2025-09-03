using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserPortal.Core.ValueObjects;

/// <summary>
/// Represents an email message as an immutable value object.
/// </summary>
public sealed record class Email
{
    /// <summary>
    /// Gets the email address of the recipient.
    /// </summary>
    public string ReceiverEmailAddress { get; }

    /// <summary>
    /// Gets the subject of the email.
    /// </summary>
    public string Subject { get; }

    /// <summary>
    /// Gets the body content of the email.
    /// </summary>
    public string Body { get; }

    private Email(string receiverEmailAddress, string subject, string body)
    {
        ReceiverEmailAddress = receiverEmailAddress;
        Subject = subject;
        Body = body;
    }

    /// <summary>
    /// Creates a new email message with the specified parameters.
    /// </summary>
    /// <param name="receiverEmailAddress">The recipient's email address.</param>
    /// <param name="subject">The email subject line.</param>
    /// <param name="body">The email body content.</param>
    /// <returns>A new <see cref="Email"/> instance.</returns>
    /// <exception cref="ArgumentException">Thrown when any of the parameters are invalid.</exception>
    public static Email Create(string receiverEmailAddress, string subject, string? body = null)
    {
        return new Email(
            receiverEmailAddress.Trim().ToLowerInvariant(),
            subject.Trim(),
            body?.Trim() ?? string.Empty
        );
    }

    /// <summary>
    /// Creates a new email message with the same recipient and subject but different body.
    /// </summary>
    /// <param name="newBody">The new body content.</param>
    /// <returns>A new <see cref="Email"/> instance with updated body.</returns>
    public Email WithBody(string? newBody) =>
        new(ReceiverEmailAddress, Subject, newBody?.Trim() ?? string.Empty);

    /// <summary>
    /// Checks if two email addresses are equivalent (case-insensitive comparison).
    /// </summary>
    /// <param name="email1">First email address.</param>
    /// <param name="email2">Second email address.</param>
    /// <returns>True if the email addresses are equivalent, false otherwise.</returns>
    public static bool AreEmailAddressesEquivalent(string? email1, string? email2)
    {
        if (email1 == null && email2 == null) return true;
        if (email1 == null || email2 == null) return false;

        return string.Equals(
            email1.Trim(),
            email2.Trim(),
            StringComparison.OrdinalIgnoreCase
        );
    }
}