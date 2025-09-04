using System;
using UserPortal.Core.ValueObjects;

namespace UserPortal.UseCases.Services;

/// <summary>
/// Represents a service for sending emails in the application.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="email">The email to send.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains true if the email was sent successfully; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown when email is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the email service is not properly configured or cannot send the email.</exception>
    Task<bool> SendEmailAsync(Email email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies if the email service is properly configured and can send emails.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the service is properly configured and can send emails; otherwise, false.</returns>
    Task<bool> VerifyServiceAsync(CancellationToken cancellationToken = default);
}