using NumberWang;
using System;
using System.ComponentModel;
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
                Direction d = (Direction)Enum.Parse(typeof(Direction), direction);
                if (game.Move(d))
                {
                    if (UpdateBoard != null)
                    {
                        MoveInProgress = true;
                        MoveCommand.RaiseCanExecuteChanged();
                        UpdateBoard(d);
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
                PlayAgainVisible = true;
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
                case "Eights":
                    game = new Eights();
                    break;
                case "2048":
                    game = new TwentyFortyEight();
                    break;
            }
            if (game != null)
            {
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
            get { return _playAgainCommand ?? (_playAgainCommand = new CommandHandler((param) => PlayAgainAction((string)param), () => true)); }
        }

        public void PlayAgainAction(string gameMode)
        {
            if (TearDownBoard != null)
            {
                TearDownBoard();
            }
            game = null;
            Score = 0;
            HideGameSelection = false;
            PlayAgainVisible = false;
        }
        #endregion

    }

}
