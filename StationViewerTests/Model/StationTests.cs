using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationViewer.Tests
{
    [TestClass()]
    public class StationTests
    {
        [TestMethod()]
        public void AddPointTest()
        {
            Station station = new Station();
            station.AddPoint("A", 382, 158);
            station.AddPoint("B", 647, 160);
            station.AddPoint("C", 680, 134);

            Assert.IsTrue(station.Points.Count == 3
                && station.Points[0].Equals(new PathPoint("A", 382, 158))
                && station.Points[1].Equals(new PathPoint("B", 647, 160))
                && station.Points[2].Equals(new PathPoint("C", 680, 134))
                );
        }

        [TestMethod()]
        public void FindParkPointsTest()
        {
            Station station = CreateTestStation();

            List<PathPoint> expectedPoints = new List<PathPoint>() { station.FindPoint("B"), station.FindPoint("I"), station.FindPoint("F"), station.FindPoint("E") };
            CollectionAssert.AreEquivalent(expectedPoints, station.FindParkPoints("PARK2"));
        }

        [TestMethod()]
        public void FindPathPointsTest()
        {
            Station station = CreateTestStation();

            List<PathPoint> expectedPoints = new List<PathPoint>() { station.FindPoint("B"), station.FindPoint("C"), station.FindPoint("D"), station.FindPoint("E") };
            CollectionAssert.AreEquivalent(expectedPoints, station.FindPathPoints("PATH3"));

        }

        [TestMethod()]
        public void AddSegmentTest()
        {
            Station station = new Station();
            station.AddPoint("A", 382, 158);
            station.AddPoint("B", 647, 160);
            station.AddPoint("C", 680, 134);
            station.AddSegment("A", "B", 137, "PATH1");
            station.AddSegment("B", "C", 128, "PATH2");
            station.AddSegment("A", "C", 42, "PATH3");


            Assert.IsTrue(station.FindPoint("A").PathSegments[0].Length == 137
                && station.FindPoint("A").PathSegments[1].Length == 42
               && station.FindPoint("B").PathSegments[0].Length == 137
                && station.FindPoint("B").PathSegments[1].Length == 128
                && station.FindPoint("C").PathSegments[0].Length == 128
                && station.FindPoint("C").PathSegments[1].Length == 42);
        }

        [TestMethod()]
        public void AddParkTest()
        {
            Station station = new Station();

            station.AddPark(new Park
            {
                Name = "PARK1",
                Paths = new List<string> { "PATH2", "PATH4" }
            });

            station.AddPark(new Park
            {
                Name = "PARK2",
                Paths = new List<string> { "PATH2", "PATH5" }
            });

            station.AddPark(new Park
            {
                Name = "PARK3",
                Paths = new List<string> { "PATH3", "PATH4", "PATH5", "PATH7" }
            });


            Assert.IsTrue(station.Parks.Count == 3
                && station.Parks[1].Equals(new Park
                {
                    Name = "PARK2",
                    Paths = new List<string> { "PATH2", "PATH5" }
                }));
        }

        [TestMethod()]
        public void FindParkTest()
        {
            Station station = new Station();
            station.AddPark(new Park
            {
                Name = "PARK1",
                Paths = new List<string> { "PATH2", "PATH4" }
            });

            Assert.IsTrue(station.FindPark("PARK1") != null);
            Assert.IsTrue(station.FindPark("PARK100") == null);
        }

        private Station CreateTestStation()
        {
            Station station = new Station();

            station.AddPoint("A", 382, 158);
            station.AddPoint("B", 647, 160);
            station.AddPoint("C", 680, 134);
            station.AddPoint("D", 950, 134);
            station.AddPoint("E", 981, 158);
            station.AddPoint("F", 1015, 160);
            station.AddPoint("I", 519, 160);
            station.AddPoint("H", 490, 185);

            station.AddSegment("A", "I", 137, "PATH1");
            station.AddSegment("I", "B", 128, "PATH2");
            station.AddSegment("B", "C", 42, "PATH3");
            station.AddSegment("C", "D", 270, "PATH3");
            station.AddSegment("D", "E", 39, "PATH3");
            station.AddSegment("B", "E", 334, "PATH4");
            station.AddSegment("E", "F", 34, "PATH5");
            station.AddSegment("H", "I", 38, "PATH7");

            station.AddPark(new Park
            {
                Name = "PARK1",
                Paths = new List<string> { "PATH2", "PATH4" }
            });

            station.AddPark(new Park
            {
                Name = "PARK2",
                Paths = new List<string> { "PATH2", "PATH5" }
            });

            station.AddPark(new Park
            {
                Name = "PARK3",
                Paths = new List<string> { "PATH3", "PATH4", "PATH5", "PATH7" }
            });

            return station;
        }
    }
}