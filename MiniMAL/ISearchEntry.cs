using System.Xml;

namespace MiniMAL
{
    public interface ISearchEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}