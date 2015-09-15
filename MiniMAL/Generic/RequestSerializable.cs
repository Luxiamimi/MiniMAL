using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MiniMAL.Types;

namespace MiniMAL.Generic
{
    public abstract class RequestSerializable<TRequestData, TMyStatus> : IRequestSerializable<TRequestData>
        where TRequestData : IRequestData
    {
        [XmlElement(ElementName = "status")]
        public TMyStatus Status { get; set; }

        [XmlElement(ElementName = "score")]
        public int? Score { get; set; }

        [XmlElement(ElementName = "date_start")]
        public MALDate? DateStart { get; set; }

        [XmlElement(ElementName = "date_finish")]
        public MALDate? DateFinish { get; set; }

        [XmlElement(ElementName = "priority")]
        public Priority? Priority { get; set; }

        [XmlElement(ElementName = "enable_discussion")]
        public int? EnableDiscussion { get; set; }

        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }

        [XmlElement(ElementName = "tags")]
        public MALTags? Tags { get; set; }

        public abstract bool StatusSpecified { get; }

        [XmlIgnore]
        public bool ScoreSpecified
        {
            get { return Score.HasValue; }
        }

        [XmlIgnore]
        public bool DateStartSpecified
        {
            get { return DateStart.HasValue; }
        }

        [XmlIgnore]
        public bool DateFinishSpecified
        {
            get { return DateFinish.HasValue; }
        }

        [XmlIgnore]
        public bool PrioritySpecified
        {
            get { return Priority.HasValue; }
        }

        [XmlIgnore]
        public bool EnableDiscussionSpecified
        {
            get { return EnableDiscussion.HasValue; }
        }

        [XmlIgnore]
        public bool CommentsSpecified
        {
            get { return Comments == ""; }
        }

        [XmlIgnore]
        public bool TagsSpecified
        {
            get { return Tags.HasValue; }
        }

        public abstract void GetData(TRequestData data);

        public string SerializeDataToString()
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
            private readonly Encoding _encoding;

            public override Encoding Encoding
            {
                get { return _encoding; }
            }

            public CustomStringWriter(Encoding encoding)
            {
                _encoding = encoding;
            }
        }

        public class CustomXmlWriter : XmlTextWriter
        {
            public CustomXmlWriter(TextWriter stringWriter)
                : base(stringWriter)
            {
            }

            public override void WriteEndElement()
            {
                WriteFullEndElement();
            }
        }
    }
}