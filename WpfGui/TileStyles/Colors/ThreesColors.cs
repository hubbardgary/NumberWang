using System.Collections.Generic;
using System.Windows.Media;

namespace WpfGui.TileStyles
{
    public static class ThreesColors
    {
        public static Dictionary<int, Color> GetColors()
        {
            return new Dictionary<int, Color>()
            {
                { 1, Colors.AliceBlue },
                { 2, Colors.AntiqueWhite },
                { 3, Colors.Aquamarine },
                { 6, Colors.LightSkyBlue },
                { 12, Colors.LimeGreen },
                { 24, Colors.CornflowerBlue },
                { 48, Colors.PaleGreen },
                { 96, Colors.Pink },
                { 192, Colors.PowderBlue },
                { 384, Colors.RosyBrown },
                { 768, Colors.Salmon },
                { 1536, Colors.Silver },
                { 3072, Colors.Tan }
            };
        }
    }
}
