using System;
using System.Windows.Input;

namespace WpfGui.ViewModel.Commands
{
    /// <summary>
    /// Implementation of ICommand to bind commands to the view
    /// </summary>
    public class CommandHandler : ICommand
    {
        private Action<object> action;
        private Func<bool> canExecute;

        public CommandHandler(Action<object> action, Func<bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object param = null)
        {
            return canExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
        public event EventHandler CanExecuteChanged;

        public void Execute(object param = null)
        {
            action(param);
        }
    }
}
