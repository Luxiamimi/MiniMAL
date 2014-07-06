using StarLess;

namespace MiniMAL.Console.Commands
{
    public class AddMangaCommand : MiniMALCommand
    {
        public AddMangaCommand(MiniMALClient client)
            : base(client, "add-manga", "Add an manga in the user's mangalist")
        {
            RequiredArguments.Add(new Argument("id", typeof(int), "manga id in MyAnimeList database"));
            RequiredArguments.Add(new Argument("status", typeof(ReadingStatus),
                "status in your list"));
        }

        protected override void Action(ArgumentsValues arguments, OptionsValues options)
        {
            Client.AddManga((int)arguments["id"],
                MangaRequestData.DefaultAddRequest((ReadingStatus)arguments["status"]));
        }
    }
}