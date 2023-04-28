using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationViewer.PathSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationViewer.PathSearch.Tests
{
    [TestClass()]
    public class DijkstraPathSearchTests
    {
        [TestMethod()]
        public void FindShortestPathTest()
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

            //A-F = A I B E F
            //D-A = A I B C D
            //B-D = B C D
            //B-E = B E
            //H-D = H I B C D
            
            DijkstraPathSearch pathSearch = new DijkstraPathSearch(station);

            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("A", "A")) == "A");
            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("A", "F")) == "AIBEF");
            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("D", "A")) == "DCBIA");
            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("B", "D")) == "BCD");
            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("B", "E")) == "BE");
            Assert.IsTrue(String.Join("", pathSearch.FindShortestPath("H", "D")) == "HIBCD");
        }
    }
}