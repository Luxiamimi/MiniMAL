using System.Collections.Generic;
using System.Xml;

namespace MiniMAL.Generic
{
    internal class SearchResult<TSearchEntry, TSeriesType, TSeriesStatus> : List<TSearchEntry>, ISearchResult
        where TSearchEntry : SearchEntry<TSeriesType, TSeriesStatus>, new()
    {
        public void LoadFromXml(XmlDocument xmlDocument)
        {
            Clear();
            if (xmlDocument.DocumentElement == null)
                return;

            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                if (node.Name != "entry")
                    continue;

                var e = new TSearchEntry();
                e.LoadFromXmlNode(node);
                Add(e);
            }
        }
    }
}