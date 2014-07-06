using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MiniMAL.Internal
{
    // TODO : Fix tags
    public abstract class EntryRequestData
    {
        [XmlElement(ElementName = "status")]
        public int Status { get; set; }

        [XmlElement(ElementName = "score")]
        public int? Score { get; set; }

        [XmlElement(ElementName = "date_start")]
        public MALDate? DateStart { get; set; }

        [XmlElement(ElementName = "date_finish")]
        public MALDate? DateFinish { get; set; }

        [XmlElement(ElementName = "priority")]
        public int? Priority { get; set; }

        [XmlElement(ElementName = "enable_discussion")]
        public int? EnableDiscussion { get; set; }

        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }

        [XmlElement(ElementName = "tags")]
        public MALTags? Tags { get; set; }

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
        public bool TagsSpecified { get { return Tags.HasValue; } }

        public string SerializeToString()
        {
            var result = new CustomStringWriter(new UTF8Encoding());
            var xml = new CustomXmlWriter(result);
            var serializer = new XmlSerializer(GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(xml, this, ns);

            return result.ToString();
        }

        public class CustomStringWriter : StringWriter
        {
            public override Encoding Encoding { get { return _encoding; } }
            private readonly Encoding _encoding;

            public CustomStringWriter(Encoding encoding)
            {
                _encoding = encoding;
            }
        }

        public class CustomXmlWriter : XmlTextWriter
        {
            public CustomXmlWriter(TextWriter stringWriter)
                : base(stringWriter) {}

            public override void WriteEndElement()
            {
                WriteFullEndElement();
            }
        }
    }
}