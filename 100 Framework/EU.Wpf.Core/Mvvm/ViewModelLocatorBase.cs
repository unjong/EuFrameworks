using EU.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;

namespace EU.Wpf.Mvvm
{
    public abstract class ViewModelLocatorBase
    {
        private Dictionary<Type, ViewModelBase> viewModelCache = new Dictionary<Type, ViewModelBase>();

        public TViewModel GetInstance<TViewModel>()
        {
            var obj = GetViewModel(typeof(TViewModel)) 
                ?? (TViewModel)Activator.CreateInstance<TViewModel>();

            return (TViewModel)obj;
        }
        
        private object GetViewModel(Type type)
        {
            return viewModelCache.ContainsKey(type) 
                ? viewModelCache[type] 
                : null;
        }

        public void Close(Type type)
        {
            var vm = GetViewModel(type);
            if (vm == null) return;

            ((ViewModelBase)vm).Dispose();
            viewModelCache.Remove(type);
        }

        public void RegisterView(Type viewModelType, Type viewType)
        {
            var template = new DataTemplate();
            template.DataType = viewModelType;
            template.VisualTree = new FrameworkElementFactory(viewType);

            Application.Current.Resources.Add(template.DataTemplateKey, template);
        }
        
        public bool IsDesignMode { get { return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue); } }
    }
}
