using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DockManager.Controls.Dock
{
    class DocumentPaneHost : ItemsControl
    {
        static DocumentPaneHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPaneHost), new FrameworkPropertyMetadata(typeof(DocumentPaneHost)));
        }
    }
}
