using System.Xml;

namespace MiniMAL.Interfaces
{
    public interface ISearchEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}