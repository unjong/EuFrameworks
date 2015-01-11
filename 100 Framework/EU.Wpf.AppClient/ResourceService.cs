using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EU.Wpf.AppClient
{
    public static class ResourceService
    {
        private static readonly Dictionary<Uri, ResourceDictionary> cachedDictionaries = new Dictionary<Uri, ResourceDictionary>();

        public static ResourceDictionary GetSharedDictionary(Uri source)
        {
            if (source.IsAbsoluteUri && source.Scheme == "pack")
            {
                source = new Uri(source.AbsolutePath, UriKind.Relative);
            }

            ResourceDictionary dictionary;
            if (!cachedDictionaries.TryGetValue(source, out dictionary))
            {
                dictionary = (ResourceDictionary)Application.LoadComponent(source);
                cachedDictionaries.Add(source, dictionary);
            }
            return dictionary;
        }
    }
}
