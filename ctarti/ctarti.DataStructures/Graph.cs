using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ctarti.DataStructures
{
    public class GraphNode
    {
        public int Key;

        public GraphNode(int key)
        { this.Key = key; }

        public override string ToString()
        { return Key.ToString(); }
    }

    public class GraphCollection
    {

    }
}
