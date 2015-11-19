namespace WpfGui.TileStyles
{
    public class TileStyle
    {
        public ShapeStyle Shape { get; set; }
        public FontStyle Font { get; set; }

        public TileStyle()
        {
            Shape = new ShapeStyle();
            Font = new FontStyle();
        }
    }
}
