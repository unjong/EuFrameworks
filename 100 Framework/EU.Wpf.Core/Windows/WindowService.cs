using EU.Core;
using EU.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EU.Wpf.Windows
{
    public static class WindowService
    {
        public static bool ShowPopup(ViewModelBase vm)
        {
            var win = new Window();
            win.Content = vm.View;
            return (bool)win.ShowDialog();
        }

        public static EuReturn SelectItemDialog(object[] items)
        {
            var dlg = new SelectItemDialog();
            var vm = dlg.DataContext as SelectItemDialog.ThisModel;
            vm.Items = items;

            var ret = new EuReturn();
            ret.IsSuccess = ShowPopup(vm);
            ret.Parameters = new object[] { vm.SelectedItem };
            
            return ret;
        }
    }
}
