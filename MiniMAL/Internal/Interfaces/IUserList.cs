using System.Xml;

namespace MiniMAL.Internal.Interfaces
{
    public interface IUserList
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}