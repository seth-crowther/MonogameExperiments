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


        public Node GetClosestNode()
        {
            Node returnNode = null;
            float minDistance = Single.MaxValue;

            foreach (Node n in RoomManager.MapGraph.Nodes)
            {
                if (n.Position.Y >= this.Centre.Y)
                {
                    Vector2 vector = n.Position - this.CentreBottom;
                    float distance = vector.Length();
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        returnNode = n;
                    }
                }
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
