using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    public class FivesStyles : BaseStyles
    {
        public override Dictionary<int, TileStyle> GetStylesCollection()
        {
            return new Dictionary<int, TileStyle>()
            {
                { 2, GetStyle(GetColor("#34495E"), GetColor("#ECF0F1"), GetColor("#34495E")) },
                { 3, GetStyle(GetColor("#2ECC71"), GetColor("#2C3E50"), GetColor("#2ECC71")) },
                { 5, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#1ABC9C")) },
                { 10, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#E74C3C")) },
                { 20, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#3498DB")) },
                { 40, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#F1C40F")) },
                { 80, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#9B59B6")) },
                { 160, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#2ECC71")) },
                { 320, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#C0392B")) },
                { 640, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#8E44AD")) },
                { 1280, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#F39C12")) },
                { 2560, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#2980B9")) },
                { 5120, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#27AE60")) },
                { 10240, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#D35400")) },
                { 20480, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#9B59B6")) },
                { 40960, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#F1C40F")) },
                { -1, GetStyle(GetColor("#ECF0F1"), GetColor("#2C3E50"), GetColor("#3498DB")) }
            };
        }

        public override TileStyle GetStyle(Color tileColor, Color fontColor)
        {
            throw new NotImplementedException();
        }

        public TileStyle GetStyle(Color tileColor, Color fontColor, Color borderColor)
        {
            var style = GetDefaultStyle();
            style.Font.Color = new SolidColorBrush(fontColor);
            style.Shape.Fill = new SolidColorBrush(tileColor);
            style.Shape.Border = new SolidColorBrush(borderColor);
            return style;
        }

        private TileStyle GetDefaultStyle()
        {
            var style = new TileStyle();
            style.Font.Family = new FontFamily("Yu Gothic UI Semilight");
            style.Font.Size = 40;
            style.Font.Weight = FontWeights.Bold;
            style.Shape.BorderThickness = 6;
            style.Shape.Radius = 50;
            return style;
        }
    }
}
