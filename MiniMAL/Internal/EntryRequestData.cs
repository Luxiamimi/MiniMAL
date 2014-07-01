using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MiniMAL.Internal
{
    // TO-DO : Fix tags
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
        public string Tags { get; set; }

        [XmlIgnore]
        public bool ScoreSpecified { get { return Score.HasValue; } }
        [XmlIgnore]
        public bool DateStartSpecified { get { return DateStart.HasValue; } }
        [XmlIgnore]
        public bool DateFinishSpecified { get { return DateFinish.HasValue; } }
        [XmlIgnore]
        public bool PrioritySpecified { get { return Priority.HasValue; } }
        [XmlIgnore]
        public bool EnableDiscussionSpecified { get { return EnableDiscussion.HasValue; } }
        [XmlIgnore]
        public bool CommentsSpecified { get { return Comments == ""; } }
        [XmlIgnore]
        public bool TagsSpecified { get { return Tags == ""; } }

        internal EntryRequestData()
        {
        }

        public string SerializeToString()
        {
            CustomStringWriter result = new CustomStringWriter(new UTF8Encoding());
            CustomXmlWriter xml = new CustomXmlWriter(result);
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(xml, this, ns);

            return result.ToString();
        }

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