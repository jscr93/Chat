using System;
using System.Collections.Generic;
using System.Windows;

namespace ChatApplication
{
    public static class UIObservableCollection
    {
        public static void ClearOnUI<T>(this ICollection<T> collection)
        {
            Action ClearMethod = collection.Clear;
            Application.Current.Dispatcher.BeginInvoke(ClearMethod);
        }

        public static void AddOnUI<T>(this ICollection<T> collection, T item)
        {
            Action<T> addMethod = collection.Add;
            Application.Current.Dispatcher.BeginInvoke(addMethod, item);
        }

    }
}