using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace RunFoxyRun
{
    public static class LeaderBoardService
    {
        private const string Path = "user://leaders.save";

        public static bool CanAdd(int score)
        {
            var leaders = ReadLeaders(Path);

            return leaders.Count < 10 || leaders.Min(x => x.Score) < score;
        }

        public static void UpdateLeaders(string name, int score)
        {
            var leaders = ReadLeaders(Path);

            if (leaders.Count < 10)
            {
                leaders.Add(new Leader { Name = GetName(name), Score = score });

                WriteLeaders(Path, leaders.OrderByDescending(x => x.Score).ToList());
            }
            else if (leaders.Min(x => x.Score) < score)
            {
                var item = leaders.Last();
                leaders.Remove(item);

                leaders.Add(new Leader { Name = GetName(name), Score = score });

                WriteLeaders(Path, leaders.OrderByDescending(x => x.Score).ToList());
            }
        }

        public static IEnumerable<Leader> GetLeaders()
        {
            return ReadLeaders(Path);
        }

        private static string GetName(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? "Foxy" : name;
        }

        private static List<Leader> ReadLeaders(string path)
        {
            var board = new File();

            if (!board.FileExists(path))
                return new List<Leader>();

            board.Open(path,File.ModeFlags.Read);

            var leaders = JsonConvert.DeserializeObject<List<Leader>>(board.GetAsText());

            board.Close();

            return leaders;
        }

        private static void WriteLeaders(string path, List<Leader> leaders)
        {
            var board = new File();

            board.Open(path, File.ModeFlags.Write);

            board.StoreLine(JsonConvert.SerializeObject(leaders));

            board.Close();
        }
    }

    public class Leader
    {
        public string Name { get; set; }

        public int Score { get; set; }
    }
}