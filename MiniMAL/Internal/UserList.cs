using System.Collections.Generic;
using System.Linq;

namespace MiniMAL.Internal
{
    public class UserList<T, TSeriesType, TSeriesStatus, TMyStatus> : IEnumerable<T>
        where T : Entry<TSeriesType, TSeriesStatus, TMyStatus>
    {
        private Dictionary<TMyStatus, List<T>> dictionary;

        internal UserList()
        {
            dictionary = new Dictionary<TMyStatus, List<T>>();
        }

        public IEnumerable<TMyStatus> Status
        {
            get { return dictionary.Keys; }
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
            if (!dictionary.ContainsKey(x.MyStatus))
                dictionary[x.MyStatus] = new List<T>();
            dictionary[x.MyStatus].Add(x);
        }

        public List<T> this[TMyStatus key]
        {
            get
            {
                if (dictionary.ContainsKey(key))
                    return dictionary[key];
                else
                    return new List<T>();
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
            List<T> allEntries = new List<T>();
            foreach (List<T> list in dictionary.Values)
                allEntries = allEntries.Concat(list).ToList();
            return allEntries;
        }
    }
}