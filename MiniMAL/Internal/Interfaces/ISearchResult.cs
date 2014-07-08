using System.Xml;

namespace MiniMAL.Internal.Interfaces
{
    public interface ISearchResult
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}