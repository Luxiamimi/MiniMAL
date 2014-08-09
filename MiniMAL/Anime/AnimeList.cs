using System.Collections.Generic;
using MiniMAL.Internal;

namespace MiniMAL.Anime
{
    public class AnimeList : UserList<Anime, TypeAnime, AiringStatus, WatchingStatus>
    {
        protected override string XmlEntityName { get { return "anime"; } }

        public override List<Anime> this[WatchingStatus key]
        {
            get
            {
                List<Anime> result = base[key];

                if (key != WatchingStatus.Watching)
                    return result;
                foreach (Anime a in base[WatchingStatus.Completed])
                    if (a.MyWatchedEpisodes < a.Episodes)
                        result.Add(a);

                return result;
            }
        }
    }
}