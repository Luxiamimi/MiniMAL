using System.Xml;

namespace MiniMAL.Generic
{
    internal interface ISearchResult
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}