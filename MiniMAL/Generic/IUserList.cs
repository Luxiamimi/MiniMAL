using System.Xml;

namespace MiniMAL.Generic
{
    internal interface IUserList
    {
        void LoadFromXml(XmlDocument xmlDocument);
    }
}