using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    class ThreesStyles : BaseStyles
    {
        private Dictionary<int, TileStyle> styles { get; set; }

        public ThreesStyles(int maxNumber)
        {
            styles = InitialiseStyles(maxNumber);
        }

        public Dictionary<int, TileStyle> InitialiseStyles(int maxTile)
        {
            // In Threes, all tiles are the same colour except:
            //      1 is blue
            //      2 is pink
            //      The maximum valued tile has yellow text

            // Get styles for 1 and 2, then add the rest in the loop.
            var styles = new Dictionary<int, TileStyle>()
            {
                { 1, GetStyle(GetColor("#66CCFF"), GetColor("#FFFFFF")) },
                { 2, GetStyle(GetColor("#FF6680"), GetColor("#FFFFFF")) }
            };

            for (int i = 3; i <= maxTile; i *= 2)
            {
                if(maxTile > 2 && i == maxTile)
                {
                    styles.Add(maxTile, GetStyle(GetColor("#8D95A6"), GetColor("#FFE380")));
                }
                else
                {
                    styles.Add(i, GetStyle(GetColor("#8D95A6"), GetColor("#000000")));
                }
            }
            return styles;
        }

        public override TileStyle GetStyle(Color tileColor, Color fontColor)
        {
            var style = GetDefaultStyle();
            style.Font.Color = new SolidColorBrush(fontColor);
            style.Shape.Fill = new SolidColorBrush(tileColor);
            return style;
        }

        private TileStyle GetDefaultStyle()
        {
            var style = new TileStyle();
            style.Font.Family = new FontFamily("Tempus Sans ITC");
            style.Font.Size = 40;
            style.Font.Weight = FontWeights.Bold;
            style.Shape.Border = new SolidColorBrush(Colors.Transparent);
            style.Shape.Radius = 4;
            return style;
        }

        public override Dictionary<int, TileStyle> GetStylesCollection()
        {
            return styles;
        }
    }
}
