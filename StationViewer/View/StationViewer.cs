using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StationViewer
{
    public class StationViewer : Control
    {
        Station station;
        public Station Station
        {
            get => station;
            set
            {
                station = value;
                viewModel.Station = value;
            }
        }

        static StationViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StationViewer), new FrameworkPropertyMetadata(typeof(StationViewer)));
        }

        StationViewerViewModel viewModel;
        Button button_ResetPreview;
        Canvas canvas_Preview;
        ComboBox combobox_ParkColor;
        
        bool drag = false;
        Point dragStartPoint;

        public override void OnApplyTemplate()
        {
            //bind controls
            button_ResetPreview = GetTemplateChild("button_ResetPreview") as Button;
            canvas_Preview = GetTemplateChild("canvas_Preview") as Canvas;
            combobox_ParkColor = GetTemplateChild("comboBox_ParkColor") as ComboBox;

            //create events
            button_ResetPreview.Click += Button_ResetPreview_Click;
            canvas_Preview.MouseDown += Canvas_MouseDown;
            canvas_Preview.MouseMove += Canvas_MouseMove;
            canvas_Preview.MouseUp += Canvas_MouseUp;
            canvas_Preview.MouseLeave += Canvas_MouseLeave;
            canvas_Preview.MouseWheel += Canvas_MouseWheel;
            combobox_ParkColor.SelectionChanged += Combobox_ParkColor_SelectionChanged;

            //set viewmodel
            viewModel = new StationViewerViewModel();
            DataContext = viewModel;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            drag = true;    // start dragging
            dragStartPoint = Mouse.GetPosition(canvas_Preview); // save start drag point
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // if dragging, adjust rectangle position based on mouse movement
            if (drag)
            {
                Point newPoint = Mouse.GetPosition(canvas_Preview);
                viewModel.MovePreview(newPoint.X - dragStartPoint.X, newPoint.Y - dragStartPoint.Y);

                dragStartPoint = newPoint;
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e) => drag = false;   // stop dragging

        private void Canvas_MouseLeave(object sender, MouseEventArgs e) => drag = false;   // stop dragging

        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e) => viewModel.ScalePreview(e.Delta);

        private void Button_ResetPreview_Click(object sender, RoutedEventArgs e) => viewModel.ResetPreview();

        private void Combobox_ParkColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combobox_ParkColor.SelectedItem is SolidColorBrush brush)
                viewModel.ParkFillColor = brush.ToString();
        }
    }
}

