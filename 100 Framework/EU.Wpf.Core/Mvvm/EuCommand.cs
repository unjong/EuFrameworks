using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EU.Wpf.Mvvm
{
    public delegate void ICommandOnExecute(ICommand sender, object parameter);
    public delegate bool ICommandOnCanExecute(ICommand sender, object parameter);

    public class EuCommand : ICommand
    {
        private ICommandOnExecute _execute;
        private ICommandOnCanExecute _canExecute;

        public EuCommand(ICommandOnExecute execute, ICommandOnCanExecute canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke(this, parameter);
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null
                ? true
                : this._canExecute.Invoke(this, parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (_canExecute != null) { CommandManager.RequerySuggested += value; } }
            remove { if (_canExecute != null) { CommandManager.RequerySuggested -= value; } }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public static EuCommand Create(ICommandOnExecute onExecute, ICommandOnCanExecute onCanExecute = null)
        {
            return new EuCommand(onExecute, onCanExecute);
        }
    }
}
