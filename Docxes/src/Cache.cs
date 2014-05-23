using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VrankenBischof.Docxes {

    public class Cache<TValue> {

        private TValue _Value;        
        private Func<TValue> _ValueGetter;


        public bool IsValueCached { get; private set; }

        public TValue Value {
            get {
                if (IsValueCached == false) {
                    _Value = _ValueGetter.Invoke();
                    IsValueCached = true;
                }

                return _Value;
            }
        }


        public Cache(Func<TValue> valueGetter) {
            _ValueGetter = valueGetter;
        }


        public void Clear() {
            _Value = default(TValue);
            IsValueCached = false;
        }

        public static implicit operator TValue(Cache<TValue> cache) {
            return cache.Value;
        }

    }

}
