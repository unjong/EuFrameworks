using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace DockManager.Controls.Tabbed
{
    public class AreaFillAdorner : Adorner
    {        
        #region Properties

        private ICollection<KeyValuePair<FrameworkElement, Point>> childs;
        private VisualCollection visualChildren;

        #endregion

        public AreaFillAdorner(UIElement adornerElement, ICollection<KeyValuePair<FrameworkElement, Point>> adornerChilds)
            : base(adornerElement)
        {
            this.visualChildren = new VisualCollection(this);
            this.childs = adornerChilds;            

            foreach (var child in adornerChilds)
            {
                var el = child.Key;
                this.visualChildren.Add(el);   
            }
        }

        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ArrangeAdorner();

            return base.ArrangeOverride(finalSize);
        }
        private void ArrangeAdorner()
        {
            foreach(var child in this.childs)
            {
                var el = child.Key;
                var point = child.Value;
                var rect = new Rect(point.X, point.Y, el.Width, el.Height);

                el.Arrange(rect);
            }            
        }
    }
}
