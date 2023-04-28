using StationViewer.PathSearch;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace StationViewer
{
    public class StationViewerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LineViewModel> Lines { get; private set; } = new ObservableCollection<LineViewModel>(); //all station paths

        public ObservableCollection<string> Points { get; private set; } = new ObservableCollection<string>(); //all station points

        public ObservableCollection<LineViewModel> SelectedParkLines { get; private set; } = new ObservableCollection<LineViewModel>(); //selected park paths

        PointCollection selectedParkContour = new PointCollection();   //selected park contour
        public PointCollection SelectedParkContour {
            get => selectedParkContour;
            private set {
                selectedParkContour = value;
                NotifyPropertyChanged("SelectedParkContour");
            }
        }

        PointCollection selectedPointsShortestPathRaw = new PointCollection();

        PointCollection selectedPointsShortestPath = new PointCollection();   //shortest path between 2 points
        public PointCollection SelectedPointsShortestPath {
            get => selectedPointsShortestPath;
            private set {
                selectedPointsShortestPath = value;
                NotifyPropertyChanged("SelectedPointsShortestPath");
            }
        }


        string parkFillColor;
        public string ParkFillColor {
            get => parkFillColor;
            set {
                parkFillColor = value;
                NotifyPropertyChanged("ParkFillColor");
            }
        }

        public ObservableCollection<string> Parks { get; private set; } = new ObservableCollection<string>(); //list of parks at the station

        string parkSelected;
        public string ParkSelected {
            get => parkSelected;

            set {
                parkSelected = value;
                NotifyPropertyChanged("ParkSelected");
                RefreshPreview();
            }

        }

        string stationName;
        public string StationName {
            get => stationName;
            set {
                stationName = value;
                NotifyPropertyChanged("StationName");
            }
        }

        string point1Selected;
        public string Point1Selected {
            get => point1Selected;
            set {
                point1Selected = value;
                SearchShortestPathBetweenPoints();
                RefreshPreview();
            }
        }

        string point2Selected;
        public string Point2Selected {
            get => point2Selected;
            set {
                point2Selected = value;
                SearchShortestPathBetweenPoints();
                RefreshPreview();
            }
        }

        public double PreviewXOffset { get; set; } = 0;
        public double PreviewYOffset { get; set; } = 0;

        private double previewScale = 1;
        public double PreviewScale {
            get => previewScale;
            set {
                if (value > 0.3 && value < 5) //min, max limits
                    previewScale = value;
            }
        }

        Station station = new Station();
        public Station Station {
            get => station;
            set {
                station = value;

                Parks.Clear();

                foreach (string park in station.Parks.Select(y => y.Name).ToList())
                    Parks.Add(park);

                Points.Clear();

                foreach (var point in station.Points)
                    Points.Add(point.Name);

                StationName = station.Name;

                RefreshPreview();
            }
        }

        public StationViewerViewModel() {
        }

        public void RefreshPreview() {
            //layers
            //1. Selected parc backcolor
            //2. Station paths
            //3. Selected park paths
            //4. Selected points with shortest path

            Park parkSelected = Station.FindPark(ParkSelected);


            #region Selected park backcolor

            SelectedParkContour = new PointCollection();

            if (parkSelected != null) {
                var points = Station.FindParkPoints(ParkSelected).Select(x => x.ToPoint()).ToList();

                foreach (Point p in ContourGenerator.GiftWrapper(points))
                    SelectedParkContour.Add(new Point(p.X * PreviewScale + PreviewXOffset, p.Y * PreviewScale + PreviewYOffset));
            }
            #endregion

            #region Station paths

            Lines.Clear();

            foreach (PathPoint point in Station.Points) {
                foreach (PathSegment segment in point.PathSegments) {
                    var line = new LineViewModel();
                    line.X1 = point.X * PreviewScale + PreviewXOffset;
                    line.Y1 = point.Y * PreviewScale + PreviewYOffset;
                    line.X2 = segment.ConnectedPoint.X * PreviewScale + PreviewXOffset;
                    line.Y2 = segment.ConnectedPoint.Y * PreviewScale + PreviewYOffset;

                    Lines.Add(line);
                }
            }
            #endregion

            #region Selected park lines

            SelectedParkLines.Clear();

            if (parkSelected != null) {
                foreach (PathPoint point in Station.FindParkPoints(ParkSelected)) {
                    foreach (PathSegment segment in point.PathSegments) {
                        if (parkSelected.ContainsSegment(segment)) {
                            var line = new LineViewModel();
                            line.X1 = point.X * PreviewScale + PreviewXOffset;
                            line.Y1 = point.Y * PreviewScale + PreviewYOffset;
                            line.X2 = segment.ConnectedPoint.X * PreviewScale + PreviewXOffset;
                            line.Y2 = segment.ConnectedPoint.Y * PreviewScale + PreviewYOffset;

                            SelectedParkLines.Add(line);
                        }
                    }
                }
            }
            #endregion

            #region Selected points with shortest path

            if (Point1Selected != null && Point2Selected != null) {
                SelectedPointsShortestPath = new PointCollection();

                foreach (var point in selectedPointsShortestPathRaw)
                    SelectedPointsShortestPath.Add(new Point(point.X * PreviewScale + PreviewXOffset, point.Y * PreviewScale + PreviewYOffset));
            }

            #endregion
        }

        private void SearchShortestPathBetweenPoints() {
            if (Point1Selected != null && Point2Selected != null) {
                selectedPointsShortestPathRaw.Clear();

                var pathSearch = new DijkstraPathSearch(Station);

                foreach (string point in pathSearch.FindShortestPath(Point1Selected, Point2Selected))
                    selectedPointsShortestPathRaw.Add(Station.FindPoint(point).ToPoint());
            }
        }

        public void MovePreview(double xOffset, double yOffset) {
            PreviewXOffset += xOffset;
            PreviewYOffset += yOffset;

            RefreshPreview();
        }

        public void ScalePreview(int delta) {
            PreviewScale += delta * 0.001;

            RefreshPreview();
        }

        public void ResetPreview() {
            PreviewScale = 1;
            PreviewXOffset = 0;
            PreviewYOffset = 0;

            RefreshPreview();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
