using System;

namespace UserPortal.SharedKernel.Domain;

public interface IEntity
{
    Guid Id { get; set; }
}
