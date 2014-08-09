using System.Xml;

namespace MiniMAL
{
    public interface IEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}