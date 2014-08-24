using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMAL.Anime;
using MiniMAL.Manga;

namespace MiniMAL.Test
{
    [TestClass]
    public class RequestTest
    {
        private const string TestUsername = "TryMiniMAL";
        private const string TestPassword = "tryminimal";

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void SearchAnime()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            Assert.IsTrue(client.SearchAnime("cowboy", "bebop").Any(e => e.Title == "Cowboy Bebop"));
        }

        [TestMethod]
        public void SearchManga()
        {
            var client = new MiniMALClient();
            client.Login(TestUsername, TestPassword);

            Assert.IsTrue(client.SearchAnime("monster").Any(e => e.Title == "Monster"));
        }
    }
}
