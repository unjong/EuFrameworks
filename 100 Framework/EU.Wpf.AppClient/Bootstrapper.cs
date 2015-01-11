using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace EU.Wpf.AppClient
{
    public class Bootstrapper
    {
        private Application App;

        public void StepInitializeCultures(string culture, string uiCulture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            }
            if (!string.IsNullOrEmpty(uiCulture))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);
            }
        }

        public void Run(Application app)
        {
            App = app;
#if DEBUG
            RunCore();
#else
            RunWithExceptionHandling();
#endif

            var rd = ResourceService.GetSharedDictionary(new Uri("/EU.Wpf.AppClient;component/Styles/StylesRoot.xaml", UriKind.Relative));
            app.Resources.MergedDictionaries.Add(rd);
        }

        private void RunCore()
        {
        }


        private void RunWithExceptionHandling()
        {
            App.DispatcherUnhandledException += AppDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;

            try
            {
                RunCore();
            }
            catch (Exception ex)
            {
                HandleException(ex, false);
            }
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
            e.Handled = true;
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception, e.IsTerminating);
        }

        private static void HandleException(Exception ex, bool isTerminating)
        {
            if (ex == null) { return; }

            //ExceptionPolicy.HandleException(ex, "Default Policy");

            if (!isTerminating)
            {
                MessageBox.Show("UnknownError\n\n" + ex.ToString()
                    , ApplicationInfo.ProductName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
