using System;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class MangaRequestData : RequestData<ReadingStatus>
    {
        public int? Chapter { get; set; }
        public int? Volume { get; set; }
        public int? DownloadedChapters { get; set; }
        public int? TimesReread { get; set; }
        public int? RereadValue { get; set; }
        public int? EnableRereading { get; set; }
        public string ScanGroup { get; set; }
        public int? RetailVolumes { get; set; }

        static public MangaRequestData DefaultAddRequest(ReadingStatus status)
        {
            var result = new MangaRequestData
            {
                Status = status,
                Chapter = 1,
                Volume = 1,
                Score = 0,
                DateStart = DateTime.Now
            };
            return result;
        }
    }
}