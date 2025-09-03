using System;

namespace UserPortal.SharedKernel.Domain;

/// <summary>
/// Interface reperesenting a entity with a id property.
/// This interface helps to identify entities and also ensure that all entitites of this kind have id property which might be beneficial in some cases.
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}
