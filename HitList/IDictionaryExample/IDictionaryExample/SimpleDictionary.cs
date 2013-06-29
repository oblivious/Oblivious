using System;
using System.Collections;

namespace IDictionaryExample
{
    public class SimpleDictionary : IDictionary
    {
        private readonly DictionaryEntry[] _items;

        public SimpleDictionary(int numItems)
        {
            _items = new DictionaryEntry[numItems];
        }

        public bool Contains(object key)
        {
            int index;
            return TryGetIndexOfKey(key, out index);
        }

        public void Add(object key, object value)
        {
            // Add the new key/value pair even if this key already exists in the dictionary.
            if (Count == _items.Length)
                throw new InvalidOperationException("The dictionary cannot hold any more items.");
            _items[Count++] = new DictionaryEntry(key, value);
        }

        public void Clear()
        {
            Count = 0;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            // Construct anf return an enumerator.
            return new SimpleDictionaryEnumerator(this);
        }

        public void Remove(object key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            // Try to find the key in the DictionaryEntry array
            int index;
            if (TryGetIndexOfKey(key, out index))
            {
                // If the key is found, slide all the items up. Kind of an ugly way of removing an item but whatever...
                Array.Copy(_items, index + 1, _items, index, Count - index - 1);
                Count--;
            }
            // If the key is not in the dictionary, just return;
        }

        public object this[object key]
        {
            get
            {
                // If this key is in the dictionary, return its value.
                int index;
                return TryGetIndexOfKey(key, out index) ? _items[index].Value : null;
            }
            set
            {
                // If this key is in the dictionary, change its value.
                int index;
                if (TryGetIndexOfKey(key, out index))
                {
                    // The key was found; change its value.
                    _items[index].Value = value;
                }
                else
                {
                    // This key is not in the dictionary. Add this key/value pair.
                    Add(key, value);
                }
            }
        }

        public ICollection Keys
        {
            get
            {
                var keys = new object[Count];
                for (var n = 0; n < Count; n++)
                    keys[n] = _items[n].Key;
                return keys;
            }
        }
        public ICollection Values
        {
            get
            {
                var values = new object[Count];
                for (var n = 0; n < Count; n++)
                    values[n] = _items[n].Value;
                return values;
            }
        }

        public bool IsReadOnly { get { return false; } }
        public bool IsFixedSize { get { return false; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary)this).GetEnumerator();
        }

        public void CopyTo(Array array, int index) { throw new NotImplementedException(); }

        public int Count { get; private set; }
        public object SyncRoot { get { throw new NotImplementedException(); } }
        public bool IsSynchronized { get { return false; } }

        private bool TryGetIndexOfKey(object key, out int index)
        {
            for (index = 0; index < Count; index++)
            {
                // If the key is found, return true (the index is also returned).
                if (_items[index].Key.Equals(key))
                    return true;
            }

            // Key not found, return false (index should be ignored by the caller).
            return false;
        }

        private class SimpleDictionaryEnumerator : IDictionaryEnumerator
        {
            // A copy of the SimpleDictionary object's key / value pairs.
            private readonly DictionaryEntry[] _items;
            private int _index = -1;

            public SimpleDictionaryEnumerator(SimpleDictionary sd)
            {
                _items = new DictionaryEntry[sd.Count];
                Array.Copy(sd._items, 0, _items, 0, sd.Count);
            }

            // Return the current item
            public object Current { get { ValidateIndex(); return _items[_index]; } }

            // Return the current dictionary entry.
            public DictionaryEntry Entry { get { return (DictionaryEntry)Current; } }

            // Return the key of the current item.
            public object Key { get { ValidateIndex(); return _items[_index].Key; } }

            // Return th evalue of the current item.
            public object Value { get { ValidateIndex(); return _items[_index].Value; } }

            // Advance to the next item.
            public bool MoveNext()
            {
                if (_index < _items.Length - 1)
                {
                    _index++;
                    return true;
                }
                return false;
            }

            private void ValidateIndex()
            {
                if (_index < 0 || _index >= _items.Length)
                    throw new InvalidOperationException("Enumerator is before or after the collection");
            }

            // Reset the index to restart the enumeration.
            public void Reset() { _index = -1; }
        }
    }
}
