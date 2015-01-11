using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EU.Wpf.Controls.Tabbed
{
    [TemplatePart(Name = "PART_HeaderPanel", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SelectedContentHost", Type = typeof(ContentPresenter))]
    public class TabbedControl : TabControl
    {
        private TabbedItemPanel PART_HeaderPanel;
        private ContentPresenter PART_SelectedContentHost;

        static TabbedControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabbedControl), new FrameworkPropertyMetadata(typeof(TabbedControl)));
        }

        public TabbedControl()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_HeaderPanel = this.GetTemplateChild("PART_HeaderPanel") as TabbedItemPanel;
            PART_SelectedContentHost = this.GetTemplateChild("PART_SelectedContentHost") as ContentPresenter;

            PART_HeaderPanel.LastTabMoveWindow = this.LastTabMoveWindow;
            PART_HeaderPanel.OnDragoutTabItem += PART_HeaderPanel_OnDragoutTabItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var tabItem = new TabbedItem();
            tabItem.Loaded += tabItem_Loaded;

            return tabItem;
        }

        private void tabItem_Loaded(object sender, RoutedEventArgs e)
        {
            var tabItem = sender as TabbedItem;
            tabItem.Loaded -= tabItem_Loaded;
        }

        public int GetTabIndex(TabbedItem tabbedItem)
        {
            return this.ItemContainerGenerator.IndexFromContainer(tabbedItem);
        }

        public System.Collections.IList ItemsList
        {
            get
            {
                return this.ItemsSource == null
                    ? this.Items
                    : this.ItemsSource as System.Collections.IList;
            }
        }

        public bool LastTabMoveWindow
        {
            get { return (bool)GetValue(LastTabMoveWindowProperty); }
            set { SetValue(LastTabMoveWindowProperty, value); }
        }

        public static readonly DependencyProperty LastTabMoveWindowProperty =
            DependencyProperty.Register("LastTabMoveWindow", typeof(bool), typeof(TabbedControl), new PropertyMetadata(false));

        private AreaFillAdorner dokingOveredAdorner;

        private void ApplyDokingOverAdorner(TabbedItem tab)
        {
            var items = new Collection<KeyValuePair<FrameworkElement, Point>>();

            { // tab header
                var border = new Border();
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#70007ACC"));
                border.Width = tab.ActualWidth;
                border.Height = tab.ActualHeight;
                var p = tab.TranslatePoint(new Point(0, 0), this);
                items.Add(new KeyValuePair<FrameworkElement, Point>(border, p));
            }
            { // tab content
                var border = new Border();
                border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#70007ACC"));
                border.Width = PART_SelectedContentHost.ActualWidth;
                border.Height = PART_SelectedContentHost.ActualHeight;
                var p = PART_SelectedContentHost.TranslatePoint(new Point(0, 0), this);
                items.Add(new KeyValuePair<FrameworkElement, Point>(border, p));
            }

            var layer = AdornerLayer.GetAdornerLayer(this);
            if (layer == null) return;

            if (dokingOveredAdorner != null)
                layer.Remove(dokingOveredAdorner);

            dokingOveredAdorner = new AreaFillAdorner(this, items);
            layer.Add(dokingOveredAdorner);
        }

        private void PART_HeaderPanel_OnDragoutTabItem(TabbedItem tab)
        {
            var dragData = this.ItemContainerGenerator.ItemFromContainer(tab);
            var size = this.RenderSize;
            var p = this.PointToScreen(Mouse.GetPosition(this));
            var rootWindow = UIHelper.GetRootWindow(this);

            RemoveTab(dragData);

            var tc = new TabbedControl();
            tc.LastTabMoveWindow = true;
            tc.Items.Add(dragData);

            var mover = new Border();
            mover.Background = new SolidColorBrush(Colors.Transparent);
            mover.PreviewMouseLeftButtonUp += mover_PreviewMouseLeftButtonUp;
            mover.MouseMove += mover_MouseMove;

            var root = new Grid();
            root.Children.Add(tc);
            root.Children.Add(mover);

            var win = new Window()
            {
                Left = p.X - 8,
                Top = p.Y - 8,
                Width = size.Width,
                Height = size.Height,
                Content = root,
                WindowStyle = System.Windows.WindowStyle.None,
                AllowsTransparency = true,
                Opacity = 0.8,
            };
            win.Owner = rootWindow;
            win.Show();

            TabbedSupport.SetDokingToTabbed(win, true);

            Mouse.Capture(mover);
        }

        private void mover_MouseMove(object sender, MouseEventArgs e)
        {
            var win = Window.GetWindow(sender as DependencyObject);
            var p = this.PointToScreen(Mouse.GetPosition(this));
            win.Left = p.X - 6;
            win.Top = p.Y - 6;
        }

        private void mover_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var grid = ((FrameworkElement)sender).Parent as Grid;
            var tabControl = grid.Children[0] as TabbedControl;
            var win = grid.Parent as Window;

            grid.Children.RemoveRange(0, grid.Children.Count);
            win.Content = null;
            win.Close();

            if (DokingOverTab != null)
            {
                var index = this.ItemContainerGenerator.IndexFromContainer(DokingOverTab);
                var tab = tabControl.ItemsList[0];
                tabControl.RemoveTab(tab);

                DokingTab(index, tab);
            }
            else
            {
                FloatTab(tabControl, win.RenderSize, new Point(win.Left, win.Top));
            }

            DokingOverTab = null;
        }


        public virtual void FloatTab(FrameworkElement content, Size size, Point position)
        {
            var win = new Window();

            win.Owner = Window.GetWindow(this);
            win.Content = content;
            win.Left = position.X;
            win.Top = position.Y;
            win.Width = size.Width;
            win.Height = size.Height;

            TabbedSupport.SetDokingToTabbed(win, true);

            win.Show();
        }

        private void DokingTab(int index, object item)
        {
            this.ItemsList.Insert(index, item);
            this.SelectedItem = item;
        }

        public virtual void RemoveTab(object item)
        {
            int selectedIndex = this.SelectedIndex;
            var removedSelectedTab = this.SelectedItem == item;

            this.RemoveItem(item);

            if (removedSelectedTab && this.Items.Count > 0)
            {
                this.SelectedItem = this.Items[Math.Min(selectedIndex, this.Items.Count - 1)];
            }
            else if (removedSelectedTab)
            {
                this.SelectedItem = null;
            }
        }

        public void RemoveItem(object item, bool rebind = true)
        {
            if (this.ItemsSource == null)
            {
                this.Items.Remove(item);
            }
            else if (this.ItemsSource is System.Collections.Specialized.INotifyCollectionChanged)
            {
                ((System.Collections.IList)this.ItemsSource).Remove(item);
            }
            else
            {
                var list = this.ItemsSource as System.Collections.IList;
                list.Remove(item);
                if (rebind)
                {
                    this.ItemsSource = list;
                }
            }
        }

        private TabbedItem _DokingOverTab;
        public TabbedItem DokingOverTab
        {
            get
            {
                return _DokingOverTab;
            }
            set
            {
                if (_DokingOverTab == value) return;

                if (dokingOveredAdorner != null)
                {
                    var layer = AdornerLayer.GetAdornerLayer(this);
                    if (layer != null)
                        layer.Remove(dokingOveredAdorner);
                }
                _DokingOverTab = value;

                if (value != null)
                    ApplyDokingOverAdorner(_DokingOverTab);
            }
        }
    }
}
