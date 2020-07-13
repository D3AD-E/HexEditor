using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hex_Editor
{
    class ObservableList<T> : List<T>
    {
        public event EventHandler OnAdd;
        public event EventHandler OnClear;

        public new void Add(T item) 
        {
            OnAdd?.Invoke(this, null);
            base.Add(item);
        }
        public new void Clear()
        {
            OnClear?.Invoke(this, null);
            base.Clear();
        }
    }
}
