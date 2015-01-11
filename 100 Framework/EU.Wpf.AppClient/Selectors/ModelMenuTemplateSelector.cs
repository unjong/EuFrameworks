using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EU.Wpf.AppClient.Selectors
{
    public class ModelMenuTemplateSelector : ItemContainerTemplateSelector
    {
        //private DataTemplate MenuTemplate;
        private DataTemplate SeparatorTemplate;

        public ModelMenuTemplateSelector()
        {
            //MenuTemplate = Application.Current.Resources["modelMenuTemplate"] as DataTemplate;
            SeparatorTemplate = Application.Current.Resources["ModelSeparatorTemplate"] as DataTemplate;
        }

        public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl)
        {
            if (item is Mvvm.Model.SeparatorModel)
                return SeparatorTemplate;
            else
                return base.SelectTemplate(item, parentItemsControl);
        }
    }
}
