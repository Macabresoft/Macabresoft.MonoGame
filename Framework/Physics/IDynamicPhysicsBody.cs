﻿namespace Macabre2D.Framework {

    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents a dynamic physics body, which can be moved and start collisions with other bodies.
    /// It handles interactions with <see cref="Collider"/>.
    /// </summary>
    public interface IDynamicPhysicsBody : IPhysicsBody {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is kinematic.
        /// </summary>
        /// <value><c>true</c> if this instance is kinematic; otherwise, <c>false</c>.</value>
        bool IsKinematic { get; set; }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        /// <value>The mass.</value>
        float Mass { get; set; }

        /// <summary>
        /// Gets or sets the velocity. This is always axis aligned.
        /// </summary>
        /// <value>The velocity.</value>
        Vector2 Velocity { get; set; }
    }
}