﻿namespace Macabresoft.MonoGame.Core {

    using Microsoft.Xna.Framework;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents a dynamic physics body, which can be moved and start collisions with other
    /// bodies. It handles interactions with <see cref="Collider" />.
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

    /// <summary>
    /// A dynamic body.
    /// </summary>
    public sealed class DynamicPhysicsBody : SimplePhysicsBody, IDynamicPhysicsBody {

        /// <summary>
        /// Empty dynamic physics body.
        /// </summary>
        public static readonly new IDynamicPhysicsBody Empty = new EmptyDynamicPhysicsBody();

        private bool _isKinematic;
        private float _mass = 1f;
        private Vector2 _velocity;

        /// <inheritdoc />
        [DataMember(Name = "Kinematic")]
        public bool IsKinematic {
            get {
                return this._isKinematic;
            }

            set {
                this.Set(ref this._isKinematic, value);
            }
        }

        /// <inheritdoc />
        [DataMember]
        public float Mass {
            get {
                return this._mass;
            }

            set {
                this.Set(ref this._mass, value);
            }
        }

        /// <inheritdoc />
        [DataMember]
        public Vector2 Velocity {
            get {
                return this._velocity;
            }

            set {
                this.Set(ref this._velocity, value);
            }
        }

        private class EmptyDynamicPhysicsBody : EmptyPhysicsBody, IDynamicPhysicsBody {

            /// <inheritdoc />
            public bool IsKinematic { get => false; set { return; } }

            /// <inheritdoc />
            public float Mass { get => 0f; set { return; } }

            /// <inheritdoc />
            public Vector2 Velocity { get => Vector2.Zero; set { return; } }
        }
    }
}