using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Resonant
{
    public class Platform : Collider
    {
        public List<Node> SurfaceNodes { get; set; }
        public Tags Tag { get; private set; }

        public enum Tags
        {
            Floor,
            Left,
            Middle,
            Right,
            Connector
        }
        public Platform(Vector2 pos, Vector2 dims, Tags tag) : base(pos, dims)
        {
            Position = pos;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
            Stationary = true;
            scale = dims;
            rotation = 0;
            rotationOrigin = Vector2.Zero;
            SurfaceNodes = new List<Node>();
            Tag = tag;

            Initialize();
        }
        public virtual void Initialize()
        {
            //Creating sprite of platform
            sprite = new Texture2D(Globals.GraphicsDevice, 1, 1);
            sprite.SetData(new[] { Color.Black });

            for (int i = 0; i <= Dims.X; i += 10)
            {
                SurfaceNodes.Add(new Node(new Vector2(Position.X + i, Position.Y)));
            }

            //Adjacency of nodes on the same platform
            for (int j = 0; j < SurfaceNodes.Count; j++)
            {
                if (!(j == SurfaceNodes.Count - 1))
                {
                    SurfaceNodes[j].AddAdjNode(SurfaceNodes[j + 1]);
                }
            }
        }

        public bool Equals(Platform other)
        {
            return (other.Position == this.Position);
        }
    }
}
