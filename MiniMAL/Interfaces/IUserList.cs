using System.Xml;

namespace MiniMAL.Interfaces
{
    public interface IUserList
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}