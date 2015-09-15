using System;
using MiniMAL.Generic;

namespace MiniMAL.Anime
{
    public class AnimeRequestData : RequestData<WatchingStatus>
    {
        public int? Episode { get; set; }
        public int? DownloadedEpisodes { get; set; }
        public int? StorageType { get; set; }
        public float? StorageValue { get; set; }
        public int? TimesRewatched { get; set; }
        public int? RewatchValue { get; set; }
        public int? EnableRewatching { get; set; }
        public string FansubGroup { get; set; }

        static public AnimeRequestData DefaultAddRequest(WatchingStatus status)
        {
            var result = new AnimeRequestData
            {
                Status = status,
                Episode = 1,
                Score = 0,
                DateStart = DateTime.Now
            };
            return result;
        }
    }
}