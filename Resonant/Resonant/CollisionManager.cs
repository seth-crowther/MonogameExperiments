using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Resonant
{
    public class CollisionManager
    {
        public List<Collider> Colliders { get; private set; }
        private List<Collider> ToDelete = new List<Collider>();

        public CollisionManager()
        {
            Colliders = new List<Collider>();
        }

        public void CorrectAllCollisions()
        {
            ToDelete = new List<Collider>();

            foreach (Collider c in Colliders)
            {
                List<Collision> collisions = GetCollisions(c);
                if (collisions.Count == 0 && c is Entity)
                {
                    c.Airborne = true;
                }
                foreach (Collision co in collisions)
                {
                    if (co.is_colliding)
                    {
                        if (!c.Stationary && !co.collided.Stationary)
                        {
                            MovableMovable(co.collided, c);
                        }
                        else if (c.Stationary && !co.collided.Stationary)
                        {
                            MovableStationary(co, c);
                        }
                    }
                }
            }

            foreach (Collider c in ToDelete)
            {
                c.Delete();
            }
        }

        public void MovableStationary(Collision co, Collider stationary)
        {
            if (!(co.collided is Bullet))
            {
                Vector2 translation;
                if (co.normal.X < 0)
                {
                    translation.X = co.collided.Right - stationary.Left;
                }
                else
                {
                    translation.X = stationary.Right - co.collided.Left;
                }

                if (co.normal.Y < 0)
                {
                    translation.Y = co.collided.Bottom - stationary.Top;
                }
                else
                {
                    translation.Y = stationary.Bottom - co.collided.Top;
                }

                translation.X = Math.Abs(translation.X);
                translation.Y = Math.Abs(translation.Y);
                translation *= co.normal;

                if (co.is_colliding && co.normal.Y < 0)
                    co.collided.Airborne = false;
                else
                    co.collided.Airborne = true;

                co.collided.Position += translation;
            }
            else
            {
                ToDelete.Add(co.collided);
            }
        }

        public void MovableMovable(Collider colliderA, Collider colliderB)
        {
            if (colliderA is Bullet && colliderB is Entity)
            {
                ToDelete.Add(colliderA);
                colliderB.Hp -= 20;
                if (colliderB.Hp <= 0)
                {
                    ToDelete.Add(colliderB);
                }
            }
        }

        public List<Collision> GetCollisions(Collider coll)
        {
            List<Collision> collisions = new List<Collision>();

            for (int i = 0; i < Colliders.Count; i++)
            {
                if (!Colliders[i].Equals(coll))
                {
                    Collision collision = coll.DetectCollision(Colliders[i]);
                    if (collision.is_colliding)
                    {
                        collisions.Add(collision);
                    }
                }
            }
            return collisions;
        }
        public void AddCollider(Collider c)
        {
            Colliders.Add(c);
        }

        public void DeleteCollider(Collider c)
        {
            Colliders.Remove(c);
        }
    }
}
