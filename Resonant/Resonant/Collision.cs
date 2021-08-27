using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Collision
    {
        public bool is_colliding;
        public Vector2 normal;
        public Type type;
        public Collider collided;

        public Collision(bool is_colliding, Vector2 normal, Type type, Collider collided)
        {
            this.is_colliding = is_colliding;
            this.normal = normal;
            this.type = type;
            this.collided = collided;
        }
    }
}
