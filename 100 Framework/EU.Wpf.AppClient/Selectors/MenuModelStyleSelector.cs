using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EU.Wpf.AppClient.Selectors
{
    public class MenuModelStyleSelector : StyleSelector
    {
        private Style MenuStyle;
        private Style SeparatorStyle;

        public MenuModelStyleSelector()
        {
            MenuStyle = Application.Current.Resources["ModelMenuStyle"] as Style;
            SeparatorStyle = Application.Current.Resources["StandardSeparator"] as Style;
        }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is Mvvm.Model.SeparatorModel)
                return SeparatorStyle;
            else if (item is Mvvm.Model.MenuModel)
                return MenuStyle;
            else
                return Application.Current.Resources[typeof(MenuItem)] as Style;
        }
    }
}
