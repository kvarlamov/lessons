using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class PowerSet<T>
    {
        //todo - think is it possible to rewrite on List<>
        private int _bulkCapacity;
        private const int _maxCircles = 1;
        private int _size;
        private const int Step = 1;
        public readonly T[] _entries;

        public PowerSet() : this(20000)
        {
        }

        public PowerSet(int capacity)
        {
            _entries = new T[capacity];
            _bulkCapacity = capacity;
        }

        public int Size()
        {
            return _size;
        }

        public void Put(T value)
        {
            if (value == null)
                return;
            
            //todo - need to move common code in function
            int index = HashFun(value);

            for (int circle = 0; circle <= _maxCircles; circle++)
            {
                if (_entries[index] == null)
                {
                    _entries[index] = value;
                    _size++;
                    return;
                }
                
                if (_entries[index].Equals(value))
                    return;

                for (int i = index + Step; i < _bulkCapacity; i += Step)
                {
                    if (_entries[i] == null)
                    {
                        _entries[i] = value;
                        _size++;
                        return;
                    }
                    
                    if (_entries[i].Equals(value))
                        return;
                }

                index = 0;
            }
        }

        public bool Get(T value)
        {
            int index = HashFun(value);

            for (int circle = 0; circle <= _maxCircles; circle++)
            {
                if (_entries[index] != null && _entries[index].Equals(value))
                    return true;

                for (int i = index + Step; i < _bulkCapacity; i += Step)
                {
                    if (_entries[i] != null && _entries[i].Equals(value))
                        return true;
                }

                index = 0;
            }

            return false;
        }

        public IEnumerable<T> GetEntries()
        {
            foreach (var entry in _entries)
            {
                yield return entry;
            }
        }

        public bool Remove(T value)
        {
            int index = HashFun(value);
            
            //fast deletion
            if (_entries[index] != null && _entries[index].Equals(value))
            {
                _entries[index] = default;
                _size = _size - 1 <= 0 ? 0 : _size - 1;
                return true;
            }

            //slow deletion
            for (int i = 0; i < _entries.Length; i++)
            {
                if (_entries[i] != null && _entries[i].Equals(value))
                {
                    _entries[i] = default;
                    _size = _size - 1 <= 0 ? 0 : _size - 1;
                    return true;
                }
            }

            return false;
        }

        public PowerSet<T> Intersection(PowerSet<T> set2)
        {
            var newSet = new PowerSet<T>(Size());
            foreach (var entry in _entries)
            {
                if (entry == null)
                    continue;
                
                if (set2.Get(entry))
                    newSet.Put(entry);
            }

            return newSet;
        }

        public PowerSet<T> Union(PowerSet<T> set2)
        {
            var newSet = new PowerSet<T>(Size() + set2.Size());
            foreach (var entry in _entries)
            {
                if (entry == null)
                    continue;
                
                newSet.Put(entry);
            }

            foreach (var entry in set2.GetEntries())
            {
                if (entry == null)
                    continue;
                
                newSet.Put(entry);
            }
            
            return newSet;
        }

        public PowerSet<T> Difference(PowerSet<T> set2)
        {
            var newSet = new PowerSet<T>(Size());
            foreach (var entry in _entries)
            {
                if (entry == null)
                    continue;
                
                if (!set2.Get(entry))
                    newSet.Put(entry);
            }
            
            return newSet;
        }

        public bool IsSubset(PowerSet<T> set2)
        {
            foreach (var entry in set2.GetEntries())
            {
                if (entry == null)
                    continue;
                
                if (!Get(entry))
                    return false;
            }

            return true;
        }

        private int HashFun(T value)
        {
            if (typeof(T) != typeof(string)) throw new NotImplementedException();
            
            byte[] data = System.Text.Encoding.UTF8.GetBytes((string)(object)value);
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            
            var res = sum % _bulkCapacity;
            
            return res;
        }
    }
}