using NumberWang;
using System.Collections.Generic;
using System.Windows.Media;

namespace WpfGui.TileStyles
{
    public static class Styles
    {
        public static Dictionary<int, Color> GetColors(IGameEngine game)
        {
            if (game is Threes)
                return ThreesColors.GetColors();
            if (game is Eights)
                return EightsColors.GetColors();
            if (game is TwentyFortyEight)
                return TwentyFortyEightColors.GetColors();
            return new Dictionary<int, Color>();
        }
    }
}
