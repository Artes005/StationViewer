using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationViewer.PathSearch
{
    /// <summary>
    /// Vertex info
    /// </summary>
    public class GraphVertexInfo
    {
        public PathPoint Vertex { get; set; }

        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Edges weight sum
        /// </summary>
        public int EdgesWeightSum { get; set; }

        public PathPoint PreviousVertex { get; set; }

        public GraphVertexInfo(PathPoint vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
}
