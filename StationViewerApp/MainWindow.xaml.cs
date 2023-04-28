using StationViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StationViewerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_LoadStation_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private void LoadFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text Files (*.txt)|*.txt"
            };

            if (dlg.ShowDialog() == true)
                ReadStation(dlg.FileName);
        }

        public void ReadStation(string filename)
        {
            Station station = new Station();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line; //line format x1,y1,x2,y2
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "" || line.StartsWith("//"))
                        continue;

                    if (line.StartsWith("STATION:"))
                    {
                        station.Name = line.Split(':')[1];
                    }
                    if (line.StartsWith("POINT:"))
                    {
                        string[] values = line.Replace("POINT:", "").Split(',');
                        station.AddPoint(values[0], Int32.Parse(values[1]), Int32.Parse(values[2]));
                    }
                    else if (line.StartsWith("SEGMENT:"))
                    {
                        string[] values = line.Replace("SEGMENT:", "").Split(',');
                        station.AddSegment(values[0], values[1], Int32.Parse(values[2]), values[3]);
                    }
                    else if (line.StartsWith("PARK:"))
                    {
                        string[] values = line.Replace("PARK:", "").Split(':');
                        Park park = new Park();
                        park.Name = values[0];

                        foreach (string pathName in values[1].Split(','))
                            park.Paths.Add(pathName);

                        station.AddPark(park);
                    }
                }
            }

            //set station for custom control
            StationPreview.Station = station;
        }
    }
}
