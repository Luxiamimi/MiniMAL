using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MiniMAL.Generic
{
    // TODO : Implements synchronizable UserList
    public abstract class UserList<T, TSeriesType, TSeriesStatus, TMyStatus> : IUserList, IEnumerable<T>
        where T : Entry<TSeriesType, TSeriesStatus, TMyStatus>, new()
    {
        private readonly Dictionary<TMyStatus, List<T>> _dictionary;

        public IEnumerable<TMyStatus> Status
        {
            get { return _dictionary.Keys; }
        }

        public int Count
        {
            get { return ToList().Count; }
        }

        protected abstract string XmlEntityName { get; }

        internal UserList()
        {
            _dictionary = new Dictionary<TMyStatus, List<T>>();
        }

        public virtual List<T> this[TMyStatus key]
        {
            get { return _dictionary.ContainsKey(key) ? _dictionary[key] : new List<T>(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ToList().GetEnumerator();
        }

        public void LoadFromXml(XmlDocument xmlDocument)
        {
            _dictionary.Clear();
            if (xmlDocument.DocumentElement == null)
                return;

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                if (node.Name != XmlEntityName)
                    continue;

                var e = new T();
                e.LoadFromXmlNode(node);
                Add(e);
            }
        }

        public void Add(T x)
        {
            if (!_dictionary.ContainsKey(x.MyStatus))
                _dictionary[x.MyStatus] = new List<T>();
            _dictionary[x.MyStatus].Add(x);
        }

        public List<T> ToList()
        {
            var allEntries = new List<T>();
            foreach (List<T> list in _dictionary.Values)
                allEntries = allEntries.Concat(list).ToList();
            return allEntries;
        }

        public float MeanScore(TMyStatus status = default(TMyStatus))
        {
            List<T> list = status.Equals(default(TMyStatus)) ? ToList() : this[status];

            if (list != null && list.Any())
                return (float)list.Sum(e => e.MyScore) / list.Count;

            return 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ToList().GetEnumerator();
        }
    }
}