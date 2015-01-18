using EU.App.DMC.ViewModels;
using EU.App.DMC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EU.App.DMC.Services
{
    class ViewModelLocator : EU.Wpf.Mvvm.ViewModelLocatorBase
    {
        public static ViewModelLocator Current { get { return Application.Current.Resources["ViewModelLocator"] as ViewModelLocator; } }

        public ViewModelLocator()
        {
            if (IsDesignMode != true)
            {
                RegisterView(typeof(MainViewModel), typeof(MainView));
                RegisterView(typeof(WorkspaceViewModel), typeof(WorkspaceView));
            }
        }

        public MainWindowViewModel MainWindowViewModel { get { return base.GetInstance<MainWindowViewModel>(); } }
        public MainViewModel MainViewModel { get { return base.GetInstance<MainViewModel>(); } }
    }
}