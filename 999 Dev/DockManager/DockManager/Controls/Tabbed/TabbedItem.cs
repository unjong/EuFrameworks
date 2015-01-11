using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DockManager.Controls.Tabbed
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public class TabbedItem : TabItem
    {
        public static readonly RoutedEvent TabClosingEvent = EventManager.RegisterRoutedEvent("TabClosing", RoutingStrategy.Bubble,typeof(RoutedEventHandler), typeof(TabbedItem));

        public event RoutedEventHandler TabClosing
        {
            add { AddHandler(TabClosingEvent, value); }
            remove { RemoveHandler(TabClosingEvent, value); }
        }

        private Button PART_CloseButton;
        static TabbedItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabbedItem), new FrameworkPropertyMetadata(typeof(TabbedItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            PART_CloseButton = this.GetTemplateChild("PART_CloseButton") as Button;
            PART_CloseButton.Click += PART_CloseButton_Click;
        }

        private void PART_CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.OnRaiseTabClosingEvent(new RoutedEventArgs(TabClosingEvent, this));
        }

        internal virtual void OnRaiseTabClosingEvent(RoutedEventArgs e)
        {
            this.RaiseEvent(e);
            if (e.Handled) return;

            ParentTabControl.RemoveTab(this.DataContext);
        }

        public TabbedControl ParentTabControl
        {
            get { return ItemsControl.ItemsControlFromItemContainer(this) as TabbedControl; }
        }

        public int Index
        {
            get
            {
                return ParentTabControl == null ? -1 : ParentTabControl.GetTabIndex(this);
            }
        }
    }
}
