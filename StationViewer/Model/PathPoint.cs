using System.Collections.Generic;
using System.Windows;

namespace StationViewer
{
    /// <summary>
    /// Path point
    /// </summary>
    public class PathPoint
    {
        /// <summary>
        /// Point name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// X coord from base point
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Y coord from base point
        /// </summary>
        public int Y { get; }


        /// <summary>
        /// Path segments
        /// </summary>
        public List<PathSegment> PathSegments { get; }


        public PathPoint(string vertexName, int x, int y)
        {
            Name = vertexName;
            X = x;
            Y = y;
            PathSegments = new List<PathSegment>();
        }

        /// <summary>
        /// Add segment
        /// </summary>
        /// <param name="newSegment">Segment</param>
        public void AddPathSegment(PathSegment newSegment)
        {
            PathSegments.Add(newSegment);
        }

        /// <summary>
        /// Add segment
        /// </summary>
        /// <param name="vertex">End point</param>
        /// <param name="segmentLength">Segment length</param>
        public void AddPathSegment(PathPoint vertex, int segmentLength, string pathName)
        {
            AddPathSegment(new PathSegment(vertex, segmentLength, pathName));
        }

        public bool IsOnPath(string path)
        {
            foreach (PathSegment segment in PathSegments)
                if (path == segment.PathName)
                    return true;

            return false;
        }

        public Point ToPoint() => new Point(X, Y);

        public override bool Equals(object obj)
        {
            // If the passed object is null
            if (obj == null)
            {
                return false;
            }
            if (!(obj is PathPoint))
            {
                return false;
            }
            return (this.X == ((PathPoint)obj).X)
                && (this.Y == ((PathPoint)obj).Y)
                && (this.Name == ((PathPoint)obj).Name);
        }
        //Overriding the GetHashCode method
        //GetHashCode method generates hashcode for the current object
        public override int GetHashCode()
        {
            //Performing BIT wise OR Operation on the generated hashcode values
            //If the corresponding bits are different, it gives 1.
            //If the corresponding bits are the same, it gives 0.
            return Name.GetHashCode() ^ X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString() => Name;
    }
}