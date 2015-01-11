using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DockManager
{
    public class DragManager
    {
        private FrameworkElement dragSource;
        private object dragData;

        private bool isDragging;
        private Point startPosition;
        private DragDropEffects dragDropEffects = DragDropEffects.Move;

        public delegate void DragStartEventHandler(FrameworkElement dragSource, object dragData, Point position);
        public event DragStartEventHandler DragStart;

        public DragManager(FrameworkElement dragSource, object dragData, DragDropEffects dragDropEffects = DragDropEffects.Move)
        {
            this.dragSource = dragSource;
            this.dragData = dragData;
            this.dragDropEffects = dragDropEffects;

            dragSource.PreviewMouseLeftButtonDown += dragSource_PreviewMouseLeftButtonDown;
            dragSource.PreviewMouseMove += dragSource_PreviewMouseMove;
            dragSource.PreviewMouseLeftButtonUp += dragSource_PreviewMouseLeftButtonUp;
        }

        private void dragSource_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender.Equals(e.Source) != true) return;

            this.isDragging = true;
            this.startPosition = e.GetPosition(null);
        }

        private void dragSource_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.isDragging != true || sender.Equals(e.Source) != true) return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point position = e.GetPosition(null);

                if (Math.Abs(position.X - startPosition.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(position.Y - startPosition.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    //StartDrag(e);
                    //DragDrop.DoDragDrop(this.dragSource, this.dragSource, this.dragDropEffects);
                    RaiseDragStart(position);

                    this.isDragging = false;
                }
            }   
        }

        private void dragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = false;
        }

        private void RaiseDragStart(Point mousePos)
        {
            if (DragStart != null)
                DragStart(this.dragSource, this.dragData, mousePos);            
        }

        public static DragManager Create(FrameworkElement dragSource, object dragData)
        {
            var dm = new DragManager(dragSource, dragData);
            return dm;
        }
    }
}
