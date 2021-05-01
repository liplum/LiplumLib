using System;
using System.Collections;
using System.Collections.Generic;

namespace Liplum
{
    public class ParallelList<P, S> : IList<(P, S)>
    {

        private readonly List<P> _primarys;

        public P[] Primarys
        {
            get => _primarys.ToArray();
        }

        private readonly List<S> _secondarys;
        public S[] Secondarys
        {
            get => _secondarys.ToArray();
        }
        public int Count
        {
            get; private set;
        } = 0;

        public int Capacity
        {
            get; private set;
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public (P, S) this[int index]
        {
            get => (Primarys[index], Secondarys[index]);
            set
            {
                Primarys[index] = value.Item1;
                Secondarys[index] = value.Item2;
            }
        }

        public ParallelList()
        {
            _primarys = new();
            Capacity = _primarys.Capacity;
            _secondarys = new();
        }

        public ParallelList(int capacity)
        {
            Capacity = capacity;
            _primarys = new(capacity);
            _secondarys = new(capacity);
        }

        IEnumerator<(P, S)> IEnumerable<(P, S)>.GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return (_primarys[i], _secondarys[i]);
            }
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return (_primarys[i], _secondarys[i]);
            }
        }

        public object Clone()
        {
            var other = new ParallelList<P, S>();
            foreach ((P, S) pair in this)
            {
                other.Add(pair);
            }
            return other;
        }

        public int IndexOf((P, S) item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_primarys[i].Equals(item.Item1) && _secondarys.Equals(item.Item2))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, (P, S) item)
        {
            _primarys.Insert(index, item.Item1);
            _secondarys.Insert(index, item.Item2);
            UpdateCount();
        }

        public void RemoveAt(int index)
        {
            _primarys.RemoveAt(index);
            _secondarys.RemoveAt(index);
            UpdateCount();
        }

        public void Add((P, S) item)
        {
            _primarys.Add(item.Item1);
            _secondarys.Add(item.Item2);
            UpdateCount();
        }

        public void Clear()
        {
            _primarys.Clear();
            _secondarys.Clear();
            UpdateCount();
        }

        public bool Contains((P, S) item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_primarys[i].Equals(item.Item1) && _secondarys.Equals(item.Item2))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo((P, S)[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || arrayIndex >= array.Length)
            {
                throw new IndexOutOfRangeException();
            }
            var restCount = array.Length - arrayIndex;
            var totalCount = restCount < Count ? restCount : Count;
            var endIndex = arrayIndex + totalCount;
            for (int i = 0, j = arrayIndex; j < endIndex; i++, j++)
            {
                array[j] = (Primarys[i], Secondarys[i]);
            }
        }

        public bool Remove((P, S) item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_primarys[i].Equals(item.Item1) && _secondarys.Equals(item.Item2))
                {
                    _primarys.Remove(item.Item1);
                    _secondarys.Remove(item.Item2);
                    UpdateCount();
                    return true;
                }
            }
            return false;
        }
        private void UpdateCount()
        {
            Count = _primarys.Count;
        }
    }
}
