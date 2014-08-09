using MiniMAL.Console.Commands.Abstract;
using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class UpdateMangaCommand : MiniMALCommand
    {
        public UpdateMangaCommand(MiniMALClient client)
            : base(client, "update-manga", "Update an manga in the user's mangalist")
        {
            RequiredArguments.Add(new Argument<int>("id", "manga id in MyAnimeList database"));

            var scoreOption = new Option("s", "score", "change score");
            scoreOption.Arguments.Add(new Argument<int>("value", "value between 1 and 10, 0 = none",
                Validator.ValueRange("value", 0, 10)));
            Options.Add(scoreOption);

            var statusOption = new Option("S", "status", "change status");
            statusOption.Arguments.Add(new Argument<ReadingStatus>("value",
                "1-reading, 2-completed, 3-on hold, 4-dropped, 6-plan to read"));
            Options.Add(statusOption);
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            var data = new MangaRequestData();

            if (options.ContainsKey("score"))
                data.Score = options["score"].Value<int>("value");

            if (options.ContainsKey("status"))
                data.Status = options["status"].Value<ReadingStatus>("value");

            ListRequestResult result = Client.UpdateManga(args.Value<int>("id"), data);

            switch (result)
            {
                case ListRequestResult.Updated:
                    System.Console.WriteLine("Updated");
                    break;
                case ListRequestResult.NoParametersPassed:
                    System.Console.WriteLine("Empty request - use options");
                    break;
            }
        }
    }
}