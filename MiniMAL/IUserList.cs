using System.Xml;

namespace MiniMAL
{
    public interface IUserList
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}