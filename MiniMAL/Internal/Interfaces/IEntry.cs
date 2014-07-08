using System.Xml;

namespace MiniMAL.Internal.Interfaces
{
    internal interface IEntry
    {
        void LoadFromXmlNode(XmlNode e);
    }
}