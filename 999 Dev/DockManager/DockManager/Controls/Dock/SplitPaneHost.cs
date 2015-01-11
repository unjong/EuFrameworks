using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DockManager.Controls.Dock
{
    class SplitPaneHost : ContentControl
    {
        static SplitPaneHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitPaneHost), new FrameworkPropertyMetadata(typeof(SplitPaneHost)));
        }
    }
}
