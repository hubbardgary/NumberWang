using System.Collections.Generic;
using System.Windows.Media;

namespace WpfGui.TileStyles.GameStyles
{
    public interface IStyles
    {
        Dictionary<int, TileStyle> GetStylesCollection();
        TileStyle GetStyle(Color tileColor, Color fontColor);
    }
}
