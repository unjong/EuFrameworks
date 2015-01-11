using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EU.Wpf.Controls.Tabbed
{
    public static class TabbedSupport
    {
        #region DokingToTabbed Attached Property

        public static readonly DependencyProperty DokingToTabbedProperty = DependencyProperty.RegisterAttached
        (
            "DokingToTabbed",
            typeof(bool),
            typeof(Window),
            new PropertyMetadata(false, DokingToTabbedProperty_Changed)
        );

        public static bool GetDokingToTabbed(Window obj)
        {
            return (bool)obj.GetValue(DokingToTabbedProperty);
        }

        public static void SetDokingToTabbed(Window obj, bool value)
        {
            obj.SetValue(DokingToTabbedProperty, value);
        }

        static void DokingToTabbedProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = d as Window;

            win.LocationChanged -= DokingToTabbedProperty_LocationChanged;

            if ((bool)e.NewValue)
                win.LocationChanged += DokingToTabbedProperty_LocationChanged;
        }

        static void DokingToTabbedProperty_LocationChanged(object sender, EventArgs e)
        {
            var win = sender as Window;
            if (win.IsLoaded != true) return;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Equals(win)) continue;

                var point = Mouse.GetPosition(window);
                //var point = win.PointToScreen(Mouse.GetPosition(window));
                //var p = InputHelper.GetMousePosition();
                //var offset_p = new Point(p.X - win.Left, p.Y - win.Top);

                //point.X -= offset_p.X;
                //point.Y -= offset_p.Y;

                var r = VisualTreeHelper.HitTest(window, point);
                if (r == null) continue;

                var tabControl = UIHelper.FindAncestor<TabbedControl>(r.VisualHit);
                var tab = UIHelper.FindAncestor<TabbedItem>(r.VisualHit);

                if (tabControl != null)
                    tabControl.DokingOverTab = tab;

                break;
            }
        }

        #endregion
    }
}
