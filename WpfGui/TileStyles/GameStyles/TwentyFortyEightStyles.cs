using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    public class TwentyFortyEightStyles : BaseStyles
    {
        public override Dictionary<int, TileStyle> GetStylesCollection()
        {
            return new Dictionary<int, TileStyle>()
            {
                { 2, GetStyle(GetColor("#EFE3DE"), GetColor("#7B716B")) },
                { 4, GetStyle(GetColor("#EEE2CD"), GetColor("#7B6D63")) },
                { 8, GetStyle(GetColor("#EFB27B"), GetColor("#FFF7F7")) },
                { 16, GetStyle(GetColor("#F79663"), GetColor("#F7F7EF")) },
                { 32, GetStyle(GetColor("#F77D63"), GetColor("#F7F3EF")) },
                { 64, GetStyle(GetColor("#F76142"), GetColor("#F7F7F7")) },
                { 128, GetStyle(GetColor("#E7CE73"), GetColor("#F7F3EF")) },
                { 256, GetStyle(GetColor("#EECA63"), GetColor("#F7F7F7")) },
                { 512, GetStyle(GetColor("#E7C64A"), GetColor("#FFF7F7")) },
                { 1024, GetStyle(GetColor("#EFC242"), GetColor("#F7F7F7")) }
            };
        }

        public override TileStyle GetStyle(Color tileColor, Color fontColor)
        {
            var style = new TileStyle();
            style.Font.Color = new SolidColorBrush(fontColor);
            style.Font.Family = new FontFamily("Franklin Gothic Heavy");
            style.Font.Size = 20;
            style.Font.Weight = FontWeights.Bold;
            style.Shape.Fill = new SolidColorBrush(tileColor);
            style.Shape.Border = new SolidColorBrush(Colors.Transparent);
            style.Shape.Radius = 1;
            return style;
        }
    }
}
