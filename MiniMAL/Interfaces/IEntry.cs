using System.Xml;

namespace MiniMAL.Interfaces
{
    public interface IEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}