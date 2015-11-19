using System.Collections.Generic;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    public abstract class BaseStyles : IStyles
    {
        public abstract Dictionary<int, TileStyle> GetStylesCollection();

        public abstract TileStyle GetStyle(Color tileColor, Color fontColor);

        public Color GetColor(string htmlCode)
        {
            return (Color)ColorConverter.ConvertFromString(htmlCode);
        }
    }
}
