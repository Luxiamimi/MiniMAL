using StarLess;

namespace MiniMAL.Console.Commands
{
    internal class UpdateAnimeCommand : MiniMALCommand
    {
        public UpdateAnimeCommand(MiniMALClient client)
            : base(client, "update-anime", "Update an anime in the user's animelist")
        {
            RequiredArguments.Add(new Argument<int>("id", "anime id in MyAnimeList database"));

            var scoreOption = new Option("s", "score", "change score");
            scoreOption.Arguments.Add(new Argument<int>("value", "value between 1 and 10, 0 = none",
                Validator.ValueRange("value", 0, 10)));
            Options.Add(scoreOption);

            var statusOption = new Option("S", "status", "change status");
            statusOption.Arguments.Add(new Argument<WatchingStatus>("value",
                "1-watching, 2-completed, 3-on hold, 4-dropped, 6-plan to watch"));
            Options.Add(statusOption);
        }

        protected override void Action(ArgumentsValues args, OptionsValues options)
        {
            var data = new AnimeRequestData();

            if (options.ContainsKey("score"))
                data.Score = options["score"].Value<int>("value");

            ListRequestResult result = Client.UpdateAnime(args.Value<int>("id"), data);

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