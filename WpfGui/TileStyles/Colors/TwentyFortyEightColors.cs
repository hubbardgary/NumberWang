using System.Collections.Generic;
using System.Windows.Media;

namespace WpfGui.TileStyles
{
    public static class TwentyFortyEightColors
    {
        public static Dictionary<int, Color> GetColors()
        {
            return new Dictionary<int, Color>()
            {
                { 2, Colors.AliceBlue },
                { 4, Colors.AntiqueWhite },
                { 8, Colors.Aquamarine },
                { 16, Colors.LightSkyBlue },
                { 32, Colors.LimeGreen },
                { 64, Colors.CornflowerBlue },
                { 128, Colors.PaleGreen },
                { 256, Colors.Pink },
                { 512, Colors.PowderBlue },
                { 1024, Colors.RosyBrown },
                { 2048, Colors.Salmon },
                { 4096, Colors.Silver },
                { 8192, Colors.Tan }
            };
        }
    }
}
