using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MiniMAL.Internal
{
    public abstract class EntryRequestData<TMyStatus> where TMyStatus : struct
    {
        [XmlElement(ElementName = "status")]
        public int Status { get; set; }
        [XmlElement(ElementName = "score")]
        public int? Score { get; set; }
        [XmlElement(ElementName = "date_start")]
        public MiniMALDate? DateStart { get; set; }
        [XmlElement(ElementName = "date_finish")]
        public MiniMALDate? DateFinish { get; set; }
        [XmlElement(ElementName = "priority")]
        public int? Priority { get; set; }
        [XmlElement(ElementName = "enable_discussion")]
        public int? EnableDiscussion { get; set; }
        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }
        [XmlElement(ElementName = "tags")]
        public string[] Tags { get; set; }

        internal EntryRequestData()
        {
        }

        public abstract string SerializeToString();

        public class CustomXmlWriter : XmlTextWriter
        {
            public CustomXmlWriter(CustomStringWriter stringWriter)
                : base(stringWriter)
            {
            }

            public override void WriteEndElement()
            {
                WriteFullEndElement();
            }
        }

        public class CustomStringWriter : StringWriter
        {
            Encoding _encoding;

            public CustomStringWriter(Encoding encoding)
                : base()
            {
                _encoding = encoding;
            }

            public override Encoding Encoding
            {
                get { return _encoding; }
            }
        }
    }
}