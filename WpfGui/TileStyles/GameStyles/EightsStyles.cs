using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    public class EightsStyles : BaseStyles
    {
        public override Dictionary<int, TileStyle> GetStylesCollection()
        {
            return new Dictionary<int, TileStyle>()
            {
                { 3, GetStyle(GetColor("#2878CB"), GetColor("#85E5F9")) },
                { 5, GetStyle(GetColor("#26891E"), GetColor("#8AFD81")) },
                { 8, GetStyle(GetColor("#BAA43F"), GetColor("#FFFEA8")) },
                { 16, GetStyle(GetColor("#B579C2"), GetColor("#FDEFFC")) },
                { 32, GetStyle(GetColor("#AF2A2B"), GetColor("#FF9293")) },
                { 64, GetStyle(GetColor("#506E91"), GetColor("#A8CEF7")) },
                { 128, GetStyle(GetColor("#C09264"), GetColor("#FEFDF8")) },
                { 256, GetStyle(GetColor("#78B2B8"), GetColor("#E7FDFC")) },
                { 512, GetStyle(GetColor("#000000"), GetColor("#FFFFFF")) },
                { 1024, GetStyle(GetColor("#000000"), GetColor("#FFFFFF")) }
            };
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
            style.Font.Family = new FontFamily("Franklin Gothic Heavy");
            style.Font.Size = 40;
            style.Font.Weight = FontWeights.Bold;
            style.Shape.Border = new SolidColorBrush(Colors.Transparent);
            style.Shape.Radius = 6;
            return style;
        }
    }
}
