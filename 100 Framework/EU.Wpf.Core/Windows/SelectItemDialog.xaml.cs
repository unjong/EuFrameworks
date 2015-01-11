using EU.Wpf.Mvvm;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace EU.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for SelectItemDialog.xaml
    /// </summary>
    public partial class SelectItemDialog : ViewControl
    {
        public SelectItemDialog()
        {
            InitializeComponent();

            ApplyViewModel(new ThisModel());
        }
        
        public class ThisModel : ViewModelBase
        {
            public enum emCommands { OK, Cancel }

            public ThisModel()
            {
                CreateCommand(emCommands.OK);
                CreateCommand(emCommands.Cancel);
            }

            public ICommand CommandOK { get { return Commands[emCommands.OK]; } }
            public ICommand CommandCancel { get { return Commands[emCommands.Cancel]; } }

            public IEnumerable<object> Items
            {
                get { return (IEnumerable<object>)GetValue(ItemsProperty); }
                set { SetValue(ItemsProperty, value); }
            }

            public static readonly DependencyProperty ItemsProperty =
                DependencyProperty.Register("Items", typeof(IEnumerable<object>), typeof(SelectItemDialog), new PropertyMetadata());

            public object SelectedItem { get { return _SelectedItem; } set { _SelectedItem = value; OnPropertyChanged(); } }
            private object _SelectedItem;

            public override void CommandOnExcute(object key, object parameter)
            {
                if (key.Equals(emCommands.OK))
                {
                    this.View.DialogResult = true;
                }
                else if (key.Equals(emCommands.Cancel))
                {
                    this.View.DialogResult = false;
                }
            }

            public override bool CommandOnCanExcute(object key, object parameter)
            {
                if (key.Equals(emCommands.OK))
                {
                    return this.SelectedItem != null;
                }

                return base.CommandOnCanExcute(key, parameter);
            }
        }
    }
}
