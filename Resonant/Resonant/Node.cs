using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class Node
    {
        public Vector2 Position { get; private set; }
        public HashSet<Node> AdjacentNodes { get; private set; }
        public Node Prev { get; set; }
        public float Cost { get; set; }
        public Node(Vector2 pos)
        {
            Position = pos;
            Prev = null;
            AdjacentNodes = new HashSet<Node>();
        }
        public void AddAdjNode(Node adj)
        {
            AdjacentNodes.Add(adj);
            adj.AdjacentNodes.Add(this);
        }

        public bool Equals(Node other)
        {
            return (this.Position == other.Position);
        }
    }
}
