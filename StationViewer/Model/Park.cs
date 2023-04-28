using System.Collections.Generic;
using System.Linq;

namespace StationViewer
{
    public class Park
    {
        public string Name { get; set; }
        public List<string> Paths { get; set; } = new List<string>();

        public bool ContainsSegment(PathSegment segment)
        {
            return Paths.Contains(segment.PathName);
        }

        public override bool Equals(object obj)
        {
            // If the passed object is null
            if (obj == null)
            {
                return false;
            }
            if (!(obj is Park))
            {
                return false;
            }
            return (this.Name == ((Park)obj).Name)
                && (this.Paths.SequenceEqual(((Park)obj).Paths));
        }
        //Overriding the GetHashCode method
        //GetHashCode method generates hashcode for the current object
        public override int GetHashCode()
        {
            //Performing BIT wise OR Operation on the generated hashcode values
            //If the corresponding bits are different, it gives 1.
            //If the corresponding bits are the same, it gives 0.
            return Name.GetHashCode() ^ Paths.GetHashCode();
        }
    }
}
