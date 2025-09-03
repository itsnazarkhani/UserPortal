using System;

namespace UserPortal.SharedKernel.Domain;

/// <summary>
/// Represents an entity with a unique identifier.
/// </summary>
/// <remarks>
/// This interface helps to identify domain entities and ensures that all entities have a unique identifier.
/// Implementing this interface enables generic repository patterns and common domain operations.
/// </remarks>
public interface IEntity : IEntity<Guid>
{
}

/// <summary>
/// Represents an entity with a unique identifier of a specific type.
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <remarks>
/// This interface helps to identify domain entities and ensures that all entities have a unique identifier.
/// Implementing this interface enables generic repository patterns and common domain operations.
/// The generic version allows for different types of identifiers while maintaining type safety.
/// </remarks>
public interface IEntity<TId> where TId : IEquatable<TId>
{
    /// <summary>
    /// Gets the unique identifier of the entity.
    /// </summary>
    TId Id { get; set; }
}
