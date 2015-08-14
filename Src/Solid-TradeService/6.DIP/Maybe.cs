using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TradeApp
{
    public class Maybe<T> : IEnumerable<T>
    {

        private readonly IEnumerable<T> _values;

        public Maybe()
        {
            _values = new T[0];
        }

        public Maybe(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _values = new[] {value};
        }

        public override string ToString()
        {
            if (_values.Any())
                return _values.Single().ToString();

            return typeof(T).Name + " Is Empty";
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
