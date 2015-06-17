using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace DemoApp.Common.Common
{
    public class VisualHelper
    {
        public static T GetParentOfT<T>(DependencyObject parent) where T : DependencyObject
        {
            while (parent != null)
            {
                if (parent is T)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        //Only executes if an object has not been loaded.
        public static bool ExecuteOnLoaded(FrameworkElement element, Action<RoutedEventArgs> onLoaded)
        {
            RoutedEventHandler loaded = null;
            loaded = (s, e) =>
            {
                onLoaded(e);
                element.Loaded -= loaded;
            };

            element.Loaded += loaded;
            return false;
        }
    }
}
