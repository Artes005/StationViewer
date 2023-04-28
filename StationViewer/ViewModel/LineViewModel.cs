namespace StationViewer
{
    public class LineViewModel
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public void MirrorY()
        {
            Y1 = -Y1;
            Y2 = -Y2;
        }

        public void OffsetY(double height)
        {
            Y1 += height;
            Y2 += height;
        }

        public LineViewModel Copy()
        {
            LineViewModel lvm = new();
            lvm.X1 = X1;
            lvm.Y1 = Y1;
            lvm.X2 = X2;
            lvm.Y2 = Y2;

            return lvm;
        }
    }
}
