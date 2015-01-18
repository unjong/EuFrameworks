using EU.Wpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU.App.DMC.Services
{
    public class ViewLocator : ViewLocatorBase
    {
        private static ViewLocator _Current;
        public static ViewLocator Current 
        { 
            get 
            {
                if (_Current == null)
                    _Current = new ViewLocator();

                return _Current;
            } 
        }

        public bool ShowDlgSelectSnapin()
        {
            return true;
        }
    }
}
