using EU.Wpf.AppClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EU.App.DMC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.StepInitializeCultures(Thread.CurrentThread.CurrentCulture.Name, Thread.CurrentThread.CurrentUICulture.Name);
            bootstrapper.Run(this);
        }
    }
}
