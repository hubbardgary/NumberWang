using NumberWang.Extensions;
using NumberWang.Gui;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WpfGui.TileStyles;
using WpfGui.TileStyles.GameStyles;
using WpfGui.ViewModel;

namespace NumberWang.WpfGui
{
    public partial class MainWindow : Window, IGameGui
    {
        // Values used to size dynamically created tiles
        internal const int TileSize = 50;
        internal double GridTop;
        internal double GridLeft;

        // Collection to keep track of dynamically created tiles
        internal Dictionary<string, UIElement> registeredElements;

        internal BaseViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new BaseViewModel();
            vm = (BaseViewModel)this.DataContext;
            AttachViewModelEvents();

            // Set the location where the top left tile should be laid
            GridTop = (Canvas.GetTop(BoardContainer) + BoardContainer.StrokeThickness);
            GridLeft = (Canvas.GetLeft(BoardContainer) + BoardContainer.StrokeThickness);
        }

        #region Attach ViewModel Events

        // Events that can be invoked from the ViewModel.
        // These should only cover UI manipulations that are tricky to do
        // in XAML (e.g. custom animations and dynamic content generation)
        private void AttachViewModelEvents()
        {
            vm.InitializeBoard += (() =>
            {
                registeredElements = new Dictionary<string, UIElement>();
                InitializeTiles(vm.game.Board.GetLength(0));
                Play();
            });

            vm.UpdateBoard += ((direction) =>
            {
                UpdateBoard(direction);
            });

            vm.TearDownBoard += (() =>
            {
                foreach (KeyValuePair<string, UIElement> x in registeredElements)
                {
                    UnregisterName(x.Key.ToString());
                    GridCanvas.Children.Remove(x.Value);
                }
                registeredElements.Clear();
            });
        }
        #endregion

        #region Tile Generation methods

        // Methods involved in spawning and styling tiles based on the current
        // state of the game.Board array.
        public void Play()
        {
            DrawBoard(vm.game);
        }

        public void DrawBoard(IGameEngine game)
        {
            var styles = StylesFactory.GetStyles(game).GetStylesCollection();
            
            game.Board.ForEachCell((i, j) =>
            {
                if (game.Board[i, j] != 0)
                {
                    var tile = (ContentControl)this.FindName(String.Format("tile{0}{1}", i, j));
                    DisplayTile(tile, styles, game.Board[i, j].ToString());
                }
            });
            DisplayTile(NextTile, styles, game.NextNumber.ToString());
        }

        private void DisplayTile(ContentControl tile, Dictionary<int, TileStyle> styles, string text)
        {
            var template = tile.Template;
            var tileShape = (Rectangle)template.FindName("TileShape", tile);
            var tileText = (TextBlock)template.FindName("TileText", tile);
            var style = styles[Convert.ToInt32((text))];

            tileShape.RadiusX = tileShape.RadiusY = style.Shape.Radius;
            tileShape.Fill = style.Shape.Fill;
            tileShape.Stroke = style.Shape.Border;
            tileText.FontFamily = style.Font.Family;
            tileText.FontSize = style.Font.Size;
            tileText.FontWeight = style.Font.Weight;
            tileText.Foreground = style.Font.Color;
            tileText.Text = text;
        }

        private void HideTile(ContentControl tile)
        {
            var template = tile.Template;
            var tileShape = (Rectangle)template.FindName("TileShape", tile);
            var tileText = (TextBlock)template.FindName("TileText", tile);
            tileShape.Stroke = null;
            tileShape.Fill = null;
            tileText.Text = "";
        }

        private void InitializeTiles(int boardSize)
        {
            BoardContainer.Width = TileSize * (vm.game.Board.GetUpperBound(0) + 1) + (2 * BoardContainer.StrokeThickness);
            BoardContainer.Height = TileSize * (vm.game.Board.GetUpperBound(0) + 1) + (2 * BoardContainer.StrokeThickness);

            // Generate tiles for selected game
            vm.game.Board.ForEachCell((i, j) =>
            {
                ContentControl c = GenerateTile(i, j);
                RegisterControl(c);
            });
            NextTile.ApplyTemplate();
        }

        private ContentControl GenerateTile(int i, int j)
        {
            ContentControl c = new ContentControl();
            c.Name = string.Format("tile{0}{1}", i.ToString(), j.ToString());
            c.Template = (ControlTemplate)FindResource("TileControl");
            c.ApplyTemplate();
            GridCanvas.Children.Add(c);
            Canvas.SetTop(c, TileSize * i + GridTop);
            Canvas.SetLeft(c, TileSize * j + GridLeft);
            return c;
        }

        private void RegisterControl(ContentControl c)
        {
            RegisterName(c.Name, c);
            registeredElements.Add(c.Name, c);
            RegisterName(c.Name + "TileShape", c.Template.FindName("TileShape", c));
            registeredElements.Add(c.Name + "TileShape", (UIElement)c.Template.FindName("TileShape", c));
            RegisterName(c.Name + "TileText", c.Template.FindName("TileText", c));
            registeredElements.Add(c.Name + "TileText", (UIElement)c.Template.FindName("TileText", c));
        }
        #endregion

        #region Animation methods

        private void UpdateBoard(Direction direction)
        {
            vm.game.MoveMatrix.ForEachCell((i, j) =>
            {
                // MoveMatrix defines the distance the tile needs to move.
                // Only perform animations for tiles that have moved.
                if (vm.game.MoveMatrix[i, j] != 0)
                {
                    var transformation = new TranslateTransform();
                    var animation = GetAnimation(direction, vm.game.MoveMatrix[i, j]);
                    animation.FillBehavior = FillBehavior.Stop;
                    animation.Completed += (sender, args) =>
                    {
                        AnimationCompleted();
                    };

                    string cellId = string.Format("{0}{1}", i.ToString(), j.ToString());
                    ContentControl tile = (ContentControl)FindName("tile" + cellId);

                    // Increase Z index for moving tiles so they overlap the tile they're merging with
                    Panel.SetZIndex(tile, 1);

                    if (direction == Direction.Left || direction == Direction.Right)
                        transformation.BeginAnimation(TranslateTransform.XProperty, animation);
                    else
                        transformation.BeginAnimation(TranslateTransform.YProperty, animation);

                    tile.RenderTransform = transformation;
                }
            });
        }

        private void AnimationCompleted()
        {
            vm.game.MoveMatrix.ForEachCell((i, j) =>
            {
                string cellId = string.Format("{0}{1}", i.ToString(), j.ToString());
                HideTile((ContentControl)FindName("tile" + cellId));
                Panel.SetZIndex((ContentControl)FindName("tile" + cellId), 0);
            });
            DrawBoard(vm.game);
            vm.UpdateBoardCompleted();
        }

        private DoubleAnimation GetAnimation(Direction direction, int units)
        {
            int distance = units * TileSize;
            switch (direction)
            {
                case Direction.Left:
                case Direction.Up:
                    return new DoubleAnimation(0, -distance, TimeSpan.FromSeconds(0.3));
                case Direction.Right:
                case Direction.Down:
                    return new DoubleAnimation(0, distance, TimeSpan.FromSeconds(0.3));
            }
            return null;
        }
        #endregion
    }
}
