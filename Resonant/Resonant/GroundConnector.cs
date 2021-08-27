using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class GroundConnector : Platform
    {
        private readonly Room left, right;
        public GroundConnector(Vector2 pos, Vector2 dims, Tags tag, Room leftRoom, Room rightRoom) : base(pos, dims, tag)
        {
            left = leftRoom;
            right = rightRoom;
        }

        public override void Initialize()
        {
            //Creating sprite of platform
            sprite = new Texture2D(Globals.GraphicsDevice, 1, 1);
            sprite.SetData(new[] { Color.Black });

            for (int i = 10; i < Dims.X; i += 10)
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

        public void InitializeAdjacency()
        {
            foreach (Platform p in left.Platforms)
            {
                if (p.Tag == Tags.Floor)
                {
                    SurfaceNodes[0].AddAdjNode(p.SurfaceNodes[^1]);
                }
            }

            foreach (Platform p in right.Platforms)
            {
                if (p.Tag == Tags.Floor)
                {
                    SurfaceNodes[^1].AddAdjNode(p.SurfaceNodes[0]);
                }
            }
        }
    }
}
