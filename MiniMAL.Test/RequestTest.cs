using System.Collections.Generic;
using System.Linq;
using MiniMAL.Anime;
using MiniMAL.Manga;
using NUnit.Framework;

namespace MiniMAL.Test
{
    [TestFixture]
    public class RequestTest
    {
        private const string TestUsername = "TryMiniMAL";
        private const string TestPassword = "tryminimal";

        [Test]
        public void RemoveAnime()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            if (client.LoadAnimelist().All(e => e.Id != 1))
                client.AddAnime(1, AnimeRequestData.DefaultAddRequest(WatchingStatus.Watching));

            Assert.IsTrue(client.LoadAnimelist().Any(e => e.Id == 1));

            client.DeleteAnime(1);

            Assert.IsFalse(client.LoadAnimelist().Any(e => e.Id == 1));
        }

        [Test]
        public void RemoveManga()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            if (client.LoadMangalist().All(e => e.Id != 1))
                client.AddManga(1, MangaRequestData.DefaultAddRequest(ReadingStatus.Reading));

            Assert.IsTrue(client.LoadMangalist().Any(e => e.Id == 1));

            client.DeleteManga(1);

            Assert.IsFalse(client.LoadMangalist().Any(e => e.Id == 1));
        }

        [Test]
        public void SearchAnime()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            List<AnimeSearchEntry> result = client.SearchAnime("cowboy bebop");

            Assert.IsTrue(result.Any(e => e.Title == "Cowboy Bebop"));
        }

        [Test]
        public void SearchManga()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            List<MangaSearchEntry> result = client.SearchManga("monster");

            Assert.IsTrue(result.Any(e => e.Title == "Monster"));
        }
    }
}
