using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Resonant
{
    public class NavGraph
    {
        public HashSet<Node> Nodes { get; private set; }
        public List<Node> Path { get; private set; }

        private List<Node> unvisited;
        private class NodeEqualityComparer : IEqualityComparer<Node>
        {
            bool IEqualityComparer<Node>.Equals(Node x, Node y)
            {
                return x.Equals(y);
            }

            int IEqualityComparer<Node>.GetHashCode(Node obj)
            {
                throw new NotImplementedException();
            }
        }

        public NavGraph()
        {
            Nodes = new HashSet<Node>();
            Path = new List<Node>();
        }

        public void Update(List<Platform> newPlats)
        {
            foreach (Platform p in newPlats)
            {
                foreach (Node n in p.SurfaceNodes)
                {
                    Nodes.Add(n);
                }
            }
        }

        public void FindPath(Node source, Node dest)
        {
            unvisited = new List<Node>(Nodes);

            //Initialising costs and previous nodes
            foreach (Node v in unvisited)
            {
                if (v.Equals(source))
                {
                    v.Cost = 0.0f;
                }
                else
                {
                    v.Cost = Single.MaxValue;
                }
                v.Prev = null;
            }

            //Initialising visited list
            HashSet<Node> visited = new HashSet<Node>();

            //Flag to check if destination node has been reached with the lowest cost
            bool finished = false;

            //Dijkstra's algorithm
            while (!finished)
            {
                //Current node is set to unvisited node with lowest cost
                Node current_node = GetLowestCost(unvisited);

                //Break out of algorithm if node with lowest cost is destination node
                if (current_node.Equals(dest))
                {
                    finished = true;
                }
                else
                {
                    foreach (Node adjNode in current_node.AdjacentNodes)
                    {
                        if (!visited.Contains(adjNode, new NodeEqualityComparer()))
                        {
                            float cost = current_node.Cost + GetDistance(current_node, adjNode);

                            if (cost < adjNode.Cost)
                            {
                                adjNode.Cost = cost;
                                adjNode.Prev = current_node;
                            }
                        }
                    }
                    visited.Add(current_node);
                    unvisited.Remove(current_node);
                }
            }

            //Outputting the path from the destination back to the source
            Path.Clear();
            GetPathToSource(dest);
        }

        public Node GetLowestCost(List<Node> nodes)
        {
            float minCost = Single.MaxValue;
            Node current_node = null;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Cost <= minCost)
                {
                    minCost = nodes[i].Cost;
                    current_node = nodes[i];
                }
            }
            return current_node;
        }

        public float GetDistance(Node start, Node end) 
        {
            Vector2 vector = end.Position - start.Position;
            return vector.Length();
        }

        //Recursively add previous nodes to path list
        public void GetPathToSource(Node dest) 
        {
            Path.Add(dest);
            if (dest.Prev == null)
            {
                Path.Reverse();
                return;
            }
            else
            {
                GetPathToSource(dest.Prev);
            }
        }
    }
}
