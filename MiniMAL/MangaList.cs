using System.Collections.Generic;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class MangaList : UserList<Manga, TypeManga, PublishingStatus, ReadingStatus>
    {
        public override List<Manga> this[ReadingStatus key]
        {
            get
            {
                var result = base[key];

                if (key != ReadingStatus.Reading) return result;
                foreach (var m in base[ReadingStatus.Completed])
                    if (m.MyReadChapters < m.Chapters)
                        result.Add(m);

                return result;
            }
        }
    }
}