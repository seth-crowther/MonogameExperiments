using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Resonant
{
    public abstract class Collider
    {
        #region protected attributes
        protected Vector2 position;
        protected Vector2 acceleration;
        protected Vector2 dimensions;
        protected GraphicsDevice GraphicsDevice = Globals.GraphicsDevice;
        protected Vector2 scale;
        protected Texture2D sprite;
        protected int hp;
        protected float rotation;
        protected Vector2 rotationOrigin;
        public Vector2 newVelocity = Vector2.Zero;
        public bool Airborne;
        #endregion protected attributes
        #region static attributes
        public static CollisionManager CollisionManager;
        #endregion static attributes
        #region Getters and Setters
        public float Right
        {
            get { return position.X + dimensions.X; }
        }

        public float Left
        {
            get { return position.X; }
        }
        public float Top
        {
            get { return position.Y; }
        }

        public float Bottom
        {
            get { return position.Y + dimensions.Y; }
        }

        public Vector2 Centre
        {
            get { return position + dimensions / 2; }
        }

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity { get; set; }

        public Vector2 Acceleration
        {
            get { return acceleration; }
            protected set { acceleration = value; }
        }


        public Vector2 Dims
        {
            get { return dimensions; }
        }

        public bool Stationary { get; protected set; }

        #endregion Getters and Setters

        //Constructor
        public Collider(Vector2 pos, Vector2 dims)
        {
            position = pos;
            dimensions = dims;
            CollisionManager.AddCollider(this);
        }

        //Function that detects a collision between 2 hitboxes and returns the status of the collision and it's direction
        //in the form of a normal

        //BUG: collision normals not working properly...
        public Collision DetectCollision(Collider hitbox)
        {
            Collision collision = new Collision(false, Vector2.Zero, hitbox.GetType(), hitbox);
            float yOverlap = 0;
            float xOverlap = 0;
            //Y checks
            if ((this.Left + 1 <= hitbox.Right) && (this.Right - 1 >= hitbox.Left))
            {
                yOverlap = Math.Min(hitbox.Bottom - this.Top, this.Bottom - hitbox.Top);

                if (yOverlap >= 0)
                {
                    collision.is_colliding = true;
                    if (this.Top > hitbox.Top)
                        collision.normal.Y = -1;
                    else
                        collision.normal.Y = 1;
                }
            }

            //X checks
            if ((this.Top + 1 <= hitbox.Bottom) && (this.Bottom - 1 >= hitbox.Top))
            {
                xOverlap = Math.Min(hitbox.Right - this.Left, this.Right - hitbox.Left);

                if (xOverlap >= 0)
                {
                    collision.is_colliding = true;
                    if (this.Left > hitbox.Left)
                        collision.normal.X = -1;
                    else
                        collision.normal.X = 1;
                }
            }

            //Detect the smallest collision only
            if (xOverlap < yOverlap)
            {
                collision.normal.Y = 0;
            }
            else
            {
                collision.normal.X = 0;
            }
            return collision;
        }

        public virtual void Delete()
        {
            CollisionManager.DeleteCollider(this);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, rotation, rotationOrigin, scale, SpriteEffects.None, 0.0f);
        }
    }
}
