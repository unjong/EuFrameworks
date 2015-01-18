using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EU.Wpf.Mvvm
{
    public class ViewControl : ContentControl, IView
    {
        static ViewControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewControl), new FrameworkPropertyMetadata(typeof(ViewControl)));
        }

        public void ApplyViewModel(ViewModelBase viewModel)
        {
            this.ViewModel = viewModel;
        }

        public ViewModelBase ViewModel 
        { 
            get
            {
                return this.DataContext as ViewModelBase;
            }
            set
            {
                this.DataContext = value;
                value.View = this;
            }
        }

        public bool DialogResult
        {
            set
            {
                var win = UIHelper.FindAncestor<Window>(this);
                win.DialogResult = value;
            }
        }
    }
}
