using System;
using UserPortal.Core.ValueObjects;

namespace UserPortal.Application.Interfaces;

/// <summary>
/// Email sender service for sending emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends email to the address and values specificed for email.
    /// </summary>
    /// <param name="email">Email value object containing,
    ///  email address of receiver, subject and body of the email to be sent.
    /// </param>
    void SendEmail(Email email);
}