using System.Collections.Generic;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class AnimeList : UserList<Anime, TypeAnime, AiringStatus, WatchingStatus>
    {
        public override List<Anime> this[WatchingStatus key]
        {
            get
            {
                var result = base[key];

                if (key != WatchingStatus.Watching) return result;
                foreach (var a in base[WatchingStatus.Completed])
                    if (a.MyWatchedEpisodes < a.Episodes)
                        result.Add(a);

                return result;
            }
        }
    }
}