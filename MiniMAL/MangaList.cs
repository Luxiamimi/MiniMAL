using System.Collections.Generic;
using MiniMAL.Internal;

namespace MiniMAL
{
    public class MangaList : UserList<Manga, TypeManga, PublishingStatus, ReadingStatus>
    {
        protected override string XmlEntityName { get { return "manga"; } }

        public override List<Manga> this[ReadingStatus key]
        {
            get
            {
                List<Manga> result = base[key];

                if (key != ReadingStatus.Reading)
                    return result;
                foreach (Manga m in base[ReadingStatus.Completed])
                    if (m.MyReadChapters < m.Chapters)
                        result.Add(m);

                return result;
            }
        }
    }
}