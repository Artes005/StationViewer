namespace StationViewer
{
    /// <summary>
    /// Segment of the path
    /// </summary>
    public class PathSegment
    {
        /// <summary>
        /// Connected point
        /// </summary>
        public PathPoint ConnectedPoint { get; }

        /// <summary>
        /// Segment length
        /// </summary>
        public int Length { get; }

        public string PathName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectedPoint">Connected point</param>
        /// <param name="length">Segment length</param>
        public PathSegment(PathPoint connectedPoint, int length, string pathName)
        {
            ConnectedPoint = connectedPoint;
            Length = length;
            PathName = pathName;
        }
    }
}

