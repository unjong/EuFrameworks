using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EU.Wpf.Mvvm
{
    public abstract class ViewModelBase : NotifyDependencyObject
    {
        public IView View { get; set; }

        public ObservableDictionary<object, ICommand> Commands { get; private set; }
        public ObservableDictionary<string, dynamic> Fields { get; private set; }

        public ViewModelBase()
        {
            InitViewModel();
        }
        
        public virtual void InitViewModel()
        {
            Commands = new ObservableDictionary<object, ICommand>();
            Fields = new ObservableDictionary<string, dynamic>();
        }
        
        public void SetCommand(object commandKey, ICommand command)
        {
            if (Commands.ContainsKey(commandKey))
                Commands[commandKey] = command;
            else
                Commands.Add(commandKey, command);
        }

        public ICommand CreateCommand(object commandKey, ICommandOnExecute onExecute = null, ICommandOnCanExecute onCanExecute = null)
        {
            if (onExecute == null) onExecute = CommandOnExcute;
            if (onCanExecute == null) onCanExecute = CommandOnCanExcute;

            var command = EuCommand.Create(onExecute, onCanExecute);
            SetCommand(commandKey, command);

            return command;
        }

        private void CommandOnExcute(ICommand sender, object parameter)
        {
            var command = this.Commands.Where(p => p.Value.Equals(sender)).FirstOrDefault();
            CommandOnExcute(command.Key, command.Value);
        }

        public virtual void CommandOnExcute(object key, object parameter) { }

        private bool CommandOnCanExcute(ICommand sender, object parameter)
        {
            var command = this.Commands.Where(p => p.Value.Equals(sender)).FirstOrDefault();
            return CommandOnCanExcute(command.Key, command.Value);
        }

        public virtual bool CommandOnCanExcute(object key, object parameter) { return true; }
        
        public ICommand CreateCommandAsync(object commandKey, Action onExecute, Func<bool> onCanExecute = null)
        {
            var command = AsyncCommand.Create(token => RunCommandAsync(onExecute, token));
            SetCommand(commandKey, command);

            return command;
        }

        private async Task RunCommandAsync(Action action, CancellationToken token = new CancellationToken())
        {
            await Task.Run(action, token);
        }

        public void SetField(object fieldKey, dynamic value)
        {
            string fieldName = fieldKey.ToString();

            if (Fields.ContainsKey(fieldName))
                Fields[fieldName] = value;
            else
                Fields.Add(fieldName, value);

            RaisePropertyChanged("Fields");
        }        
    }
}
