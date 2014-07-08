using System.Collections.Generic;
using System.Xml;
using MiniMAL.Internal.Interfaces;

namespace MiniMAL.Internal
{
    public class SearchResult<TSearchEntry> : List<TSearchEntry>, ISearchResult
        where TSearchEntry : ISearchEntry, new()
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