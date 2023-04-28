using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace StationViewer
{
    public static class ContourGenerator
    {

        /// <summary>
        /// Jarvis march algorythm. "Gift wrapping"
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointCollection GiftWrapper(List<Point> points) {
            if (points.Count < 3)
                return new PointCollection(); //"At least 3 points required"

            var contourPoints = new PointCollection();

            // get leftmost point
            Point vPointOnHull = points.Where(p => p.X == points.Min(min => min.X)).First();

            Point vEndpoint;
            do {
                contourPoints.Add(vPointOnHull);
                vEndpoint = points[0];

                for (int i = 1; i < points.Count; i++) {
                    if ((vPointOnHull == vEndpoint)
                        || (Orientation(vPointOnHull, vEndpoint, points[i]) == -1)) {
                        vEndpoint = points[i];
                    }
                }

                vPointOnHull = vEndpoint;

            }
            while (vEndpoint != contourPoints[0]);

            return contourPoints;
        }

        /// <summary>
        /// 3 points orientation test
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p"></param>
        /// <returns>-1 Left-hand orientation, 1 - Right-hand orientation, 0 - collinear orientation </returns>
        private static int Orientation(Point p1, Point p2, Point p) {
            // Determinant
            double orientation = (p2.X - p1.X) * (p.Y - p1.Y) - (p.X - p1.X) * (p2.Y - p1.Y);

            if (orientation > 0)
                return -1; //     (* Orientation is to the left-hand side  *)
            if (orientation < 0)
                return 1; // (* Orientation is to the right-hand side *)

            return 0; //  (* Orientation is neutral aka collinear  *)
        }


    }
}
