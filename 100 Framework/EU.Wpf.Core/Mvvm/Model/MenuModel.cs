using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EU.Wpf.Mvvm.Model
{
    public interface IMenuModel
    {
    }

    public class SeparatorModel : IMenuModel
    {
    }

    public class MenuModel : NotifyObject, IMenuModel
    {
        public string Display { get; set; }
        public ICommand Command { get; set; }
        public string InputGestureText { get; set; }
        public ObservableCollection<IMenuModel> Items { get; set; }

        public MenuModel()
        {
            this.Items = new ObservableCollection<IMenuModel>();
        }

        public static MenuModel Create(string display, ICommand command = null, string inputGestureText = null)
        {
            return new MenuModel()
            {
                Display = display,
                Command = command,
                InputGestureText = inputGestureText,
            };
        }

        public static IMenuModel CreateSeparator()
        {
            return new SeparatorModel();
        }
    }
}
