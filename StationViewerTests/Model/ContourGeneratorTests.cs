using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace StationViewer.Tests
{
    [TestClass()]
    public class ContourGeneratorTests
    {
        [TestMethod()]
        public void GiftWrapperTest()
        {
            Point A = new Point(382, 158); //A
            Point B = new Point(647, 160); //B
            Point C = new Point(680, 134); //C
            Point D = new Point(950, 134); //D
            Point E = new Point(981, 158); //E
            Point F = new Point(1015, 160); //F
            Point I = new Point(519, 160); //I
            Point H = new Point(490, 185); //H

            List<Point> allPoints = new() { A, B, C, D, E, F, I, H };
            PointCollection expectedPoints = new() { A, H, F, D, C };


            PointCollection calculatedPoints = ContourGenerator.GiftWrapper(allPoints);

            CollectionAssert.AreEqual(expectedPoints, calculatedPoints);
        }
    }
}