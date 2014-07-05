using System.Collections.Generic;
using System.Linq;

namespace MiniMAL.Internal
{
    public class UserList<T, TSeriesType, TSeriesStatus, TMyStatus> : IEnumerable<T>
        where T : Entry<TSeriesType, TSeriesStatus, TMyStatus>
    {
        private readonly Dictionary<TMyStatus, List<T>> _dictionary;

        internal UserList()
        {
            _dictionary = new Dictionary<TMyStatus, List<T>>();
        }

        public IEnumerable<TMyStatus> Status
        {
            get { return _dictionary.Keys; }
        }

        public int Count
        {
            get
            {
                return ToList().Count;
            }
        }

        public void Add(T x)
        {
            if (!_dictionary.ContainsKey(x.MyStatus))
                _dictionary[x.MyStatus] = new List<T>();
            _dictionary[x.MyStatus].Add(x);
        }

        public virtual List<T> this[TMyStatus key]
        {
            get
            {
                return _dictionary.ContainsKey(key) ? _dictionary[key] : new List<T>();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ToList().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ToList().GetEnumerator();
        }

        public List<T> ToList()
        {
            var allEntries = new List<T>();
            foreach (var list in _dictionary.Values)
                allEntries = allEntries.Concat(list).ToList();
            return allEntries;
        }
    }
}