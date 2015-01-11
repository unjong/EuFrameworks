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

namespace DockManager.Controls.Dock
{
    public class DockManager : ContentControl
    {
        static DockManager()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DockManager), new FrameworkPropertyMetadata(typeof(DockManager)));
        }

        public ObservableCollection<IDockPane> DocumentPanes
        {
            get { return (ObservableCollection<IDockPane>)GetValue(DocumentPanesProperty); }
            set { SetValue(DocumentPanesProperty, value); }
        }

        public static readonly DependencyProperty DocumentPanesProperty =
            DependencyProperty.Register("DocumentPanes", typeof(ObservableCollection<IDockPane>), typeof(DockManager), new PropertyMetadata(new ObservableCollection<IDockPane>()));
    }
}
