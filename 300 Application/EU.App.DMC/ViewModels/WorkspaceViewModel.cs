using EU.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EU.App.DMC.ViewModels
{
    class WorkspaceViewModel : ViewModelBase
    {
        public WorkspaceViewModel()
        {            
        }

        public object WorkMain
        {
            get { return (object)GetValue(WorkMainProperty); }
            set { SetValue(WorkMainProperty, value); }
        }

        public static readonly DependencyProperty WorkMainProperty =
            DependencyProperty.Register("WorkMain", typeof(object), typeof(WorkspaceViewModel), new PropertyMetadata());

        //public void LoadConsole(string path)
        //{
        //    var assembly = Assembly.LoadFile(path + @"\EU.App.DMC.Consoles.Sample.dll");
        //    var t = assembly.GetType(assembly.GetName().Name + ".SampleConsole");
        //    var instance = Activator.CreateInstance(t);

        //    WorkMain = instance;
        //}

        /// <summary>
        /// 워크스페이스를 닫습니다.
        /// </summary>
        public void Close()
        {
        }

        public void LoadSnapin()
        {
            EU.App.DMC.Services.ViewLocator.Current.ShowDlgSelectSnapin();
        }
    }
}
