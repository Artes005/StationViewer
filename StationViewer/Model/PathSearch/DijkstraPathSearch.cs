using System.Collections.Generic;

namespace StationViewer.PathSearch
{
    /// <summary>
    /// Diikstra path search algorythm
    /// </summary>
    public class DijkstraPathSearch
    {
        Station graph;

        List<GraphVertexInfo> infos;

        public DijkstraPathSearch(Station graph)
        {
            this.graph = graph;
        }

        /// <summary>
        /// Initialize info
        /// </summary>
        private void InitInfo()
        {
            infos = new List<GraphVertexInfo>();
            foreach (var v in graph.Points)
            {
                infos.Add(new GraphVertexInfo(v));
            }
        }

        /// <summary>
        /// Get vertex info
        /// </summary>
        GraphVertexInfo GetVertexInfo(PathPoint v)
        {
            foreach (var i in infos)
            {
                if (i.Vertex.Equals(v))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Search unvisited vertex with a minimal sum
        /// </summary>
        private GraphVertexInfo FindUnvisitedVertexWithMinSum()
        {
            var minValue = int.MaxValue;
            GraphVertexInfo minVertexInfo = null;
            foreach (var i in infos)
            {
                if (i.IsUnvisited && i.EdgesWeightSum < minValue)
                {
                    minVertexInfo = i;
                    minValue = i.EdgesWeightSum;
                }
            }

            return minVertexInfo;
        }

        /// <summary>
        /// Search shortest path by vertex name
        /// </summary>
        public List<string> FindShortestPath(string startName, string finishName)
        {
            return FindShortestPath(graph.FindPoint(startName), graph.FindPoint(finishName));
        }

        /// <summary>
        /// Search shortest path by PathPoint
        /// </summary>
        public List<string> FindShortestPath(PathPoint startVertex, PathPoint finishVertex)
        {
            InitInfo();
            var first = GetVertexInfo(startVertex);
            first.EdgesWeightSum = 0;
            while (true)
            {
                var current = FindUnvisitedVertexWithMinSum();
                if (current == null)
                {
                    break;
                }

                SetSumToNextVertex(current);
            }

            return GetPath(startVertex, finishVertex);
        }

        /// <summary>
        /// Get edges sum for the next vertex
        /// </summary>
        /// <param name="info">Current vertex info</param>
        private void SetSumToNextVertex(GraphVertexInfo info)
        {
            info.IsUnvisited = false;
            foreach (var e in info.Vertex.PathSegments)
            {
                var nextInfo = GetVertexInfo(e.ConnectedPoint);
                var sum = info.EdgesWeightSum + e.Length;
                if (sum < nextInfo.EdgesWeightSum)
                {
                    nextInfo.EdgesWeightSum = sum;
                    nextInfo.PreviousVertex = info.Vertex;
                }
            }
        }

        /// <summary>
        /// Path generation
        /// </summary>
        /// <param name="startVertex">Start point</param>
        /// <param name="endVertex">End point</param>
        /// <returns>Path</returns>
        private List<string> GetPath(PathPoint startVertex, PathPoint endVertex)
        {
            List<string> points = new List<string>();
            points.Add(endVertex.ToString());
            while (startVertex != endVertex && endVertex != null)
            {
                endVertex = GetVertexInfo(endVertex).PreviousVertex;

                if (endVertex != null)
                    points.Insert(0, endVertex.ToString());
            }

            return points;
        }
    }
}
