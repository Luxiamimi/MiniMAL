using System.Xml;

namespace MiniMAL.Internal.Interfaces
{
    public interface ISearchEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}