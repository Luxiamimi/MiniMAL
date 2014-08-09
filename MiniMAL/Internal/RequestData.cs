namespace MiniMAL.Internal
{
    public abstract class RequestData<TMyStatus> : IRequestData
    {
        public TMyStatus Status { get; set; }
        public int? Score { get; set; }
        public MALDate? DateStart { get; set; }
        public MALDate? DateFinish { get; set; }
        public Priority? Priority { get; set; }
        public int? EnableDiscussion { get; set; }
        public string Comments { get; set; }
        public MALTags? Tags { get; set; }

        internal RequestData() {}
    }
}