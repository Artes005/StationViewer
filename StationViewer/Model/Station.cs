using System.Collections.Generic;

namespace StationViewer
{
    /// <summary>
    /// Station
    /// </summary>
    public class Station
    {
        public string Name { get; set; }

        /// <summary>
        /// Points list
        /// </summary>
        public List<PathPoint> Points { get; }

        public List<Park> Parks { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Station()
        {
            Points = new List<PathPoint>();
            Parks = new List<Park>();
        }

        /// <summary>
        /// Add point
        /// </summary>
        /// <param name="pointName">Point name</param>
        public void AddPoint(string pointName, int x, int y)
        {
            Points.Add(new PathPoint(pointName, x, y));
        }

        /// <summary>
        /// Search point by name
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns>Point</returns>
        public PathPoint? FindPoint(string pointName)
        {
            foreach (var v in Points)
            {
                if (v.Name.Equals(pointName))
                    return v;
            }

            return null;
        }

        /// <summary>
        /// Search points by park
        /// </summary>
        /// <param name="pointName">Point name</param>
        /// <returns>Point</returns>
        public List<PathPoint> FindParkPoints(string parkName)
        {
            List<PathPoint> points = new List<PathPoint>();


            Park park = FindPark(parkName);
            if (park == null)
                return new List<PathPoint>();

            foreach (PathPoint p in Points)
            {
                foreach (PathSegment s in p.PathSegments)
                if (park.Paths.Contains(s.PathName))
                    points.Add(p);
            }

            return points;
        }

        public List<PathPoint> FindPathPoints(string path)
        {
            List<PathPoint> points = new List<PathPoint>();

            foreach (PathPoint p in Points)
            {
                if (p.IsOnPath(path))
                        points.Add(p);
            }

            return points;
        }

        /// <summary>
        /// Add segment
        /// </summary>
        /// <param name="point1Name">Point1 name</param>
        /// <param name="point2Name">Point2 name</param>
        /// <param name="length">Segment length</param>
        public void AddSegment(string point1Name, string point2Name, int length, string pathName)
        {
            var v1 = FindPoint(point1Name);
            var v2 = FindPoint(point2Name);
            if (v2 != null && v1 != null)
            {
                v1.AddPathSegment(v2, length, pathName);  //add segment to point 1
                v2.AddPathSegment(v1, length, pathName);  //add segment to point 2
            }
        }

        /// <summary>
        /// Add parc
        /// </summary>
        public void AddPark(Park park)
        {
            Parks.Add(park);
        }

        public Park? FindPark(string parkName)
        {
            foreach (var v in Parks)
            {
                if (v.Name.Equals(parkName))
                    return v;
            }

            return null;
        }
    }
}
