using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DockManager.Controls.Tabbed
{
    public class TabbedItemPanel : TabPanel
    {
        public delegate void OnDragoutTabItemEventHandler(TabbedItem tab);
        public event OnDragoutTabItemEventHandler OnDragoutTabItem;

        private TabbedControl tabControl;
        private TabbedItem draggedTab;
        private bool draggingWindow;        

        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            draggedTab = GetPointTabItem();
            tabControl = draggedTab.ParentTabControl;

            if (LastTabMoveWindow && draggedTab != null && this.Children.Count == 1)
            {
                draggingWindow = true;
                Window.GetWindow(this).DragMove();
            }
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            if (this.draggedTab == null || this.draggingWindow) return;
            
            var tab = GetPointTabItem();
            if (tab == null || draggedTab == tab) return;

            var tabItem = tab.ParentTabControl.ItemContainerGenerator.ItemFromContainer(tab);
            var tabIndex = tab.ParentTabControl.ItemContainerGenerator.IndexFromContainer(tab);
            var draggedTabIndex = tab.ParentTabControl.ItemContainerGenerator.IndexFromContainer(draggedTab);

            int insertIndex;
            if (tabIndex > draggedTabIndex)
                insertIndex = tabIndex - 1;
            else
                insertIndex = tabIndex + 1;

            tabControl.RemoveItem(tabItem, false);
            tabControl.ItemsList.Insert(insertIndex, tabItem);

            if (tabControl.ItemsSource != null
                && tabControl.ItemsSource is System.Collections.Specialized.INotifyCollectionChanged != true)
            {
                throw new Exception("ItemsSource 는 INotifyCollectionChanged 컬렉션이어햐 합니다.");
                //var itemsSource = tabControl.ItemsSource;
                //tabControl.ItemsSource = null;
                //tabControl.ItemsSource = itemsSource;
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            draggedTab = null;
            draggingWindow = false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            if (this.draggedTab == null || this.draggingWindow) return;

            RaiseDragoutTabItem(draggedTab);

            draggedTab = null;
            draggingWindow = false;
        }

        private void RaiseDragoutTabItem(TabbedItem draggedTab)
        {
            if (OnDragoutTabItem != null)
                OnDragoutTabItem(draggedTab);
        }

        private TabbedItem GetPointTabItem()
        {               
            var p = Mouse.GetPosition(this);
            var result = VisualTreeHelper.HitTest(this, p);
            if (result == null) { return null; }

            var source = result.VisualHit;
            while (source != null && !this.Children.Contains(source as UIElement))
            {
                source = VisualTreeHelper.GetParent(source);
            }
            if (source == null) { return null; }

            return source as TabbedItem;
        }

        /// <summary>
        /// 마지막 남은 탭을 드래그 할 때 윈도우를 드래그합니다.
        /// </summary>
        public bool LastTabMoveWindow { get; set; }
    }
}
