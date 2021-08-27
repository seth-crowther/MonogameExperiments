using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Resonant
{
    public abstract class Entity : Collider
    {
        #region private attributes
        protected DateTime reload;
        protected int maxMag;
        protected List<Collision> collisions;
        public static RoomManager RoomManager;
        public static EntityManager EntityManager;
        //Use this variable so GetNodeBeneath() only needs to be called once per frame
        protected Node beneath;
        #endregion private attributes
        #region getters and setters
        public float JumpVel { get; protected set; }
        public float MoveVel { get; protected set; }
        public DateTime LastShootTime { get; set; }
        public int Magazine { get; set; }
        public bool Reloading { get; protected set; }
        public Vector2 CentreBottom { get { return new Vector2(this.Centre.X, this.Bottom); } }
        #endregion getters and setters
        public Entity(Vector2 pos, Vector2 dims, EntityManager em) : base(pos, dims)
        { 
            position = pos;
            dimensions = dims;
            EntityManager = em;

            rotation = 0;
            scale = Vector2.One;
            rotationOrigin = Vector2.Zero;
            newVelocity = new Vector2(0, Velocity.Y);
            Acceleration = new Vector2(0, Globals.Gravity);
        }

        //BUG: If standing on the edge of a platform, can get a lower platform which is bad for pathing
        public Platform UpdatePlatformBeneath()
        {
            float minDistance = Single.MaxValue;
            Platform returnPlatform = null;

            foreach (Platform p in RoomManager.LoadedPlatforms)
            {
                if (this.Centre.X <= p.Right && this.Centre.X >= p.Left)
                {
                    if (this.Position.Y < p.Position.Y)
                    {
                        if (p.Position.Y - this.Bottom < minDistance)
                        {
                            minDistance = p.Position.Y - this.Bottom;
                            returnPlatform = p;
                        }
                    }
                }
            }

            foreach (Collision c in CollisionManager.GetCollisions(this))
            {
                if (c.is_colliding && c.normal.Y > 0 && c.collided is Platform)
                {
                    if (returnPlatform == null)
                    {
                        returnPlatform = (Platform)c.collided;
                    }
                    else if (c.collided.Position.Y < returnPlatform.Position.Y)
                    {
                        returnPlatform = (Platform)c.collided;
                    }
                    break;
                }
            }

            Console.WriteLine(returnPlatform.Position.ToString());
            return returnPlatform;
        }

        public Node GetClosestNode()
        {
            Node returnNode = null;
            float minDistance = Single.MaxValue;

            foreach (Node n in RoomManager.MapGraph.Nodes)
            {
                //if (n.Position.Y >= this.Bottom) //This breaks something
                //{
                Vector2 vector = n.Position - this.CentreBottom;
                float distance = vector.Length();
                if (distance < minDistance)
                {
                    minDistance = distance;
                    returnNode = n;
                }
                //}
            }
            return returnNode;
        }

        protected void HandleCollisions()
        {
            foreach (Collision c in CollisionManager.GetCollisions(this))
            {
                if (c.is_colliding && c.normal.Y != 0 && c.collided.Stationary)
                {
                    Velocity = new Vector2(Velocity.X, 0);
                }
            }
        }
    }
}
