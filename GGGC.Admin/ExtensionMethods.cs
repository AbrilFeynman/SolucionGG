using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGGC.Admin
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableList<T>(this IEnumerable<T> data)
        {
            ObservableCollection<T> dataToReturn = new ObservableCollection<T>();

            foreach (T t in data)
                dataToReturn.Add(t);

            return dataToReturn;
        }
    }
}
