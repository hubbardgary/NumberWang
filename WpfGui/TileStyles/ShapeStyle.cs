using System.Windows.Media;

namespace WpfGui.TileStyles
{
    public class ShapeStyle
    {
        public Brush Fill { get; set; }
        public Brush Border { get; set; }
        public int BorderThickness { get; set; } = 1;
        public int Radius { get; set; }
    }
}