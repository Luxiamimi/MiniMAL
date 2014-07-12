using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace MiniMAL
{
    public struct MiniMALClientData
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

        public MiniMALClientData(string username, string password)
            : this()
        {
            Username = username;
            DecryptedPassword = password;
        }

        static public void Save(MiniMALClientData data, string filename)
        {
            var serializer = new XmlSerializer(typeof(MiniMALClientData));
            var sw = new StreamWriter(filename);
            serializer.Serialize(sw, data);
            sw.Close();
        }

        static public MiniMALClientData Load(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException();

            var serializer = new XmlSerializer(typeof(MiniMALClientData));
            var sr = new StreamReader(filename);
            MiniMALClientData data;
            try
            {
                data = (MiniMALClientData)serializer.Deserialize(sr);
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