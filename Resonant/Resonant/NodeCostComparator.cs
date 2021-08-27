using System;
using System.Collections.Generic;
using System.Text;

namespace Resonant
{
    public class NodeCostComparator : IComparer<Node>
    {
        public int Compare(Node one, Node two)
        {
            return one.Cost.CompareTo(two.Cost);
        }
    }
}
