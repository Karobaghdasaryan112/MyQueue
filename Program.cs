using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyQueue<int> queue = new MyQueue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            foreach (var item in queue)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
    public class MyQueue<T> : IEnumerable<T>, ICollection
    {
        private T[] values;
        private int _size;
        private int _capacity;
        public int Count => _size;
        public bool IsReadOnly => false;
        public bool IsSynchronized => false;
        public object SyncRoot => this;
        private void Initialize()
        {
            _capacity = (_capacity == 0) ? 4 : _capacity;
            values = new T[_capacity];
            _size = 0;
        }
        private void SetCapacity()
        {
            if (_size == _capacity)
            {
                _capacity *= 2;
            }
        }
        private int GetIndex(T item)
        {
            for (int i = 0; i <= _size; i++)
            {
                if (values[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }
        public MyQueue()
        {
            Initialize();
        }
        public MyQueue(int capacity)
        {
            _capacity = capacity;
            values = new T[capacity];
            _size = 0;
        }
        public MyQueue(ICollection<T> values)
        {
            if (values == null) throw new ArgumentNullException();
            _capacity = values.Count;
            Initialize();
            foreach (var item in values)
            {
                Enqueue(item);
            }
        }
        public void Enqueue(T item)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            SetCapacity();
            values[_size++] = item;
        }
        public T Dequeue()
        {
            Remove();
            return values[GetIndex(values[0])];
        }
        private void Remove()
        {
            int index = GetIndex(values[0]);
            Array.Copy(values, index+1, values, index, _size - (index + 1));
            Array.Resize(ref values, --_size);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
        }
        public bool Contains(T item)
        {
            foreach (var item1 in values)
            {
                if (item1.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        public void CopyTo(Array array, int arrayIndex)
        {
            array.CopyTo(values, arrayIndex);
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumeraotor<T>(values,_size);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
    public class MyEnumeraotor<T> : IEnumerator<T>
    {
        private T[] _values;
        private int _size;
        private T current;
        private int index;
        public MyEnumeraotor(T[] values,int size)
        {
            _values = values;
            _size = size;
        }
        public void Dispose() { }
        public bool MoveNext()
        {
            while(index < _size)
            {
                current = _values[index++];
                return true;
            }
            return false;
        }
        public T Current => current;
        object IEnumerator .Current => Current;
        public void Reset()
        {
            _size = 0;
            _values = null;
        }
    } 
}
