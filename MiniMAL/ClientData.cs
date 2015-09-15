using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace MiniMAL
{
    public struct ClientData
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }

        [XmlIgnore]
        public string DecryptedPassword
        {
            get { return Encoding.GetString(ProtectedData.Unprotect(Password, null, DataProtectionScope.CurrentUser)); }
            set { Password = ProtectedData.Protect(Encoding.GetBytes(value), null, DataProtectionScope.CurrentUser); }
        }

        static private readonly ASCIIEncoding Encoding = new ASCIIEncoding();

        public ClientData(string username, string password)
            : this()
        {
            Username = username;
            DecryptedPassword = password;
        }

        static public void Save(ClientData data, string filename)
        {
            var serializer = new XmlSerializer(typeof(ClientData));
            var sw = new StreamWriter(filename);
            serializer.Serialize(sw, data);
            sw.Close();
        }

        static public ClientData Load(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            var serializer = new XmlSerializer(typeof(ClientData));
            var sr = new StreamReader(filename);
            ClientData data;
            try
            {
                data = (ClientData)serializer.Deserialize(sr);
            }
            catch (Exception)
            {
                sr.Close();
                throw;
            }

            sr.Close();
            return data;
        }
    }
}