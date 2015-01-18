using EU.Wpf.AppClient;
using EU.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU.App.DMC.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        public override void InitViewModel()
        {
            base.InitViewModel();
        }

        public string Title { get { return ApplicationInfo.Title; } }
    }
}
