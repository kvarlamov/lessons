using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures
{
    public class PowerSet<T>
    {
        public readonly List<T> _entries;

        public PowerSet() : this(20000)
        {
        }

        public PowerSet(int capacity)
        {
            _entries = new List<T>(capacity);
        }

        public int Size()
        {
            return _entries.Count;
        }

        public void Put(T value)
        {
            if (value == null)
                return;
            if (!_entries.Contains(value))
                _entries.Add(value);
        }

        public bool Get(T value)
        {
            return _entries.Contains(value);
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
            return _entries.Remove(value);
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
    }
}