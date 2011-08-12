using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fluency.Utils
{
#if SILVERLIGHT
    public class SilverlightListDictionary : IDictionary
    {
        private readonly IList<DictionaryEntry> _dictionaryEntries = new List<DictionaryEntry>();
        private readonly object _syncRoot = new object();

        public bool Contains(object key)
        {
            return _dictionaryEntries.Where(x => x.Key == key).Any();
        }

        public void Add(object key, object value)
        {
            if (!Contains(key))
                _dictionaryEntries.Add(new DictionaryEntry(key, value));
        }

        public void Clear()
        {
            _dictionaryEntries.Clear();
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return new SilverlightListDictionaryEnumerator(_dictionaryEntries);
        }

        public void Remove(object key)
        {
            for(int i = 0; i < _dictionaryEntries.Count; i++)
                if (_dictionaryEntries[i].Key == key)
                {
                    _dictionaryEntries.RemoveAt(i);
                    return;
                }
        }

        public object this[object key]
        {
            get { return _dictionaryEntries.Where(x => x.Key == key).Select(x => x.Value).FirstOrDefault(); }
            set
            {
                Remove(key);
                _dictionaryEntries.Add(new DictionaryEntry(key, value));
            }
        }

        public ICollection Keys
        {
            get { return new SilverlightListDictionaryCollection(_dictionaryEntries, x => x.Key); }
        }

        public ICollection Values
        {
            get { return new SilverlightListDictionaryCollection(_dictionaryEntries, x => x.Value); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = 0; i < _dictionaryEntries.Count; i++)
                array.SetValue(_dictionaryEntries[i], i + index);
        }

        public int Count
        {
            get { return _dictionaryEntries.Count; }
        }

        public object SyncRoot
        {
            get { return _syncRoot; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        private class SilverlightListDictionaryEnumerator : IDictionaryEnumerator
        {
            private readonly IList<DictionaryEntry> _entries;
            private int index = -1;

            public SilverlightListDictionaryEnumerator(IList<DictionaryEntry> entries)
            {
                _entries = entries;
            }

            public bool MoveNext()
            {
                return ++index < _entries.Count;
            }

            public void Reset()
            {
                index = -1;
            }

            public object Current
            {
                get
                {
                    ValidateIndex();
                    return _entries[index];
                }
            }

            public object Key
            {
                get
                {
                    ValidateIndex();
                    return _entries[index].Key;
                }
            }

            public object Value
            {
                get
                {
                    ValidateIndex();
                    return _entries[index].Value;
                }
            }

            public DictionaryEntry Entry
            {
                get
                {
                    ValidateIndex();
                    return _entries[index];
                }
            }

            private void ValidateIndex()
            {
                if (index < 0 || index > _entries.Count)
                    throw new InvalidOperationException("Enumerator is outside the bounds of the collection");
            }
        }

        private class SilverlightListDictionaryCollection : ICollection
        {
            private readonly IList<DictionaryEntry> _entries;
            private readonly Func<DictionaryEntry, object> _selector;
            private readonly object _syncRoot = new object();

            public SilverlightListDictionaryCollection(IList<DictionaryEntry> entries, Func<DictionaryEntry, object> selector)
            {
                _entries = entries;
                _selector = selector;
            }

            public IEnumerator GetEnumerator()
            {
                return _entries.Select(_selector).GetEnumerator();
            }

            public void CopyTo(Array array, int index)
            {
                int i = 0;
                foreach (object item in _entries.Select(_selector))
                    array.SetValue(item, i++ + index);
            }

            public int Count
            {
                get { return _entries.Count; }
            }

            public object SyncRoot
            {
                get { return _syncRoot; }
            }

            public bool IsSynchronized
            {
                get { return false; }
            }
        }
    }
#endif
}