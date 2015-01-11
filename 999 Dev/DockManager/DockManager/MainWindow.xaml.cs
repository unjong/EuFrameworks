using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DockManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<object> tabItems;
        public MainWindow()
        {
            InitializeComponent();

            tabItems = new  ObservableCollection<object>();
            tabItems.Add("abcd");
            tabItems.Add("efgh");
            tabItems.Add("ijkl");
            tabItems.Add("mnop");
            tabItems.Add("qrst");
            tabItems.Add("uvwx");
            tabItems.Add("yz..");

            cTabbedControl.ItemsSource = tabItems;
        }
    }
}
