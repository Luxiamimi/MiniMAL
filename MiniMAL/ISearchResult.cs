using System.Xml;

namespace MiniMAL
{
    public interface ISearchResult
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}