using System.Xml;

namespace MiniMAL.Interfaces
{
    public interface ISearchResult
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}