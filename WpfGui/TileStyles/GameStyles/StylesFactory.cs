using NumberWang;
using System;

namespace WpfGui.TileStyles.GameStyles
{
    public static class StylesFactory
    {
        public static IStyles GetStyles(IGameEngine game)
        {
            if (game is Threes)
                return new ThreesStyles(game.GetMaxNumber());
            else if (game is Fives)
                return new FivesStyles();
            else if (game is Eights)
                return new EightsStyles();
            else if (game is TwentyFortyEight)
                return new TwentyFortyEightStyles();
            throw new NotSupportedException(String.Format("Styles not implemented for {0}", game.GetType().Name));
        }
    }
}
