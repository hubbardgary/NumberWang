using NumberWang;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfGui.ViewModel.Commands;

namespace WpfGui.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // The game forms the model
        public IGameEngine game { get; set; }

        // Detect whether animation of a move is in progress
        public bool MoveInProgress { get; set; }

        // Event to inform view when a bound property changes
        public event PropertyChangedEventHandler PropertyChanged;

        #region Events to trigger methods in the view
        public delegate void InitializeBoardAction();
        public event InitializeBoardAction InitializeBoard;

        public delegate void UpdateBoardAction(Direction direction);
        public event UpdateBoardAction UpdateBoard;

        public delegate void TearDownBoardAction();
        public event TearDownBoardAction TearDownBoard;
        #endregion

        #region Properties bound to the view
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Score"));
            }
        }

        private Visibility _nextTileVisibility;
        public Visibility NextTileVisibility
        {
            get { return _nextTileVisibility; }
            set
            {
                _nextTileVisibility = value;
                PropertyChanged(this, new PropertyChangedEventArgs("NextTileVisibility"));
            }
        }

        private Visibility _scoreVisibility;
        public Visibility ScoreVisibility
        {
            get { return _scoreVisibility; }
            set
            {
                _scoreVisibility = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ScoreVisibility"));
            }
        }

        private Visibility _quitCurrentGameVisibility = Visibility.Hidden;
        public Visibility QuitCurrentGameVisibility
        {
            get { return _quitCurrentGameVisibility; }
            set
            {
                _quitCurrentGameVisibility = value;
                PropertyChanged(this, new PropertyChangedEventArgs("QuitCurrentGameVisibility"));
            }
        }

        private bool _playAgainVisible;
        public bool PlayAgainVisible
        {
            get { return _playAgainVisible; }
            set
            {
                _playAgainVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PlayAgainVisible"));
            }
        }

        private bool _hideGameSelection;
        public bool HideGameSelection
        {
            get { return _hideGameSelection; }
            set
            {
                _hideGameSelection = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HideGameSelection"));
                }
            }
        }
        #endregion

        #region Custom Command implementations
        private CommandHandler _moveCommand;
        public CommandHandler MoveCommand
        {
            get { return _moveCommand ?? (_moveCommand = new CommandHandler((param) => MoveAction((string)param), () => MovePermitted())); }
        }

        public void MoveAction(string direction)
        {
            if (game != null)
            {
                Direction directionToMove = (Direction)Enum.Parse(typeof(Direction), direction);
                if (game.Move(directionToMove))
                {
                    if (UpdateBoard != null)
                    {
                        MoveInProgress = true;
                        MoveCommand.RaiseCanExecuteChanged();
                        UpdateBoard(directionToMove);
                        Score = game.Score();
                    }
                }
            }
        }
        public bool MovePermitted()
        {
            return game != null && !game.GameOver() && !MoveInProgress;
        }

        public void UpdateBoardCompleted()
        {
            MoveInProgress = false;
            MoveCommand.RaiseCanExecuteChanged();
            if (game.GameOver())
            {
                ScoreVisibility = Visibility.Visible;
                PlayAgainVisible = true;
                QuitCurrentGameVisibility = Visibility.Hidden;
            }
        }

        private ICommand _startGameCommand;
        public ICommand StartGameCommand
        {
            get { return _startGameCommand ?? (_startGameCommand = new CommandHandler((param) => StartGameAction((string)param), () => true)); }
        }

        public void StartGameAction(string gameMode)
        {
            switch (gameMode)
            {
                case "Threes":
                    game = new Threes();
                    break;
                case "Fives":
                    game = new Fives();
                    break;
                case "Eights":
                    game = new Eights();
                    break;
                case "2048":
                    game = new TwentyFortyEight();
                    break;
            }
            if (game != null)
            {
                NextTileVisibility = game.NextNumberVisible ? Visibility.Visible : Visibility.Hidden;
                ScoreVisibility = game.ScoreVisible ? Visibility.Visible : Visibility.Hidden;
                QuitCurrentGameVisibility = Visibility.Visible;
                HideGameSelection = true;
                if (InitializeBoard != null)
                {
                    InitializeBoard();
                    MoveCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private ICommand _playAgainCommand;
        public ICommand PlayAgainCommand
        {
            get { return _playAgainCommand ?? (_playAgainCommand = new CommandHandler((param) => PlayAgainAction(), () => true)); }
        }

        public void PlayAgainAction()
        {
            if (TearDownBoard != null)
            {
                TearDownBoard();
            }
            game = null;
            Score = 0;
            HideGameSelection = false;
            PlayAgainVisible = false;
            QuitCurrentGameVisibility = Visibility.Hidden;
        }

        private ICommand _quitCurrentGameCommand;
        public ICommand QuitCurrentGameCommand
        {
            get { return _quitCurrentGameCommand ?? (_quitCurrentGameCommand = new CommandHandler((param) => QuitCurrentGameAction(), () => true)); }
        }

        public void QuitCurrentGameAction()
        {
            if (ConfirmQuit())
            {
                PlayAgainAction();
            }
        }
        #endregion

        private bool ConfirmQuit()
        {
            return MessageBox.Show("Are you sure you want to quit?", "Confirm quit", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }

}
