using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misc
{
    public class VarRef<T>
    {
        private Func<T> _get;
        private Action<T> _set;

        public VarRef(Func<T> @get, Action<T> @set)
        {
            _get = @get;
            _set = @set;
        }

        public T Value
        {
            get { return _get(); }
            set { _set(value); }
        }
    }
}
