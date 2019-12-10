using SpeedRunWcf.Client.SpeedRunWebservice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedRunWcf.Client {
    class Program {

        static void Main(string[] args) {

            PrintWelcomeMessage();

            Console.WriteLine("Press any key to start.");

            Console.ReadKey(true);

            SpeedRunServiceAccessor accessor = new SpeedRunServiceAccessor();

            Console.WriteLine("Retrieving Speed Run Data from web service...");

            SpeedRunFetchResult fetchResult = accessor.GetSpeedRunData();

            if(fetchResult.ResultType == ResultType.Success) {

                Console.WriteLine($"Successfully retrieved {fetchResult.ResultCount} records.");

                PrintSpeedRunData(fetchResult.SpeedRuns);

            }
            else {

                HandleError(fetchResult.ResultType, fetchResult.ErrorMessage);

            }

            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

        private static void PrintSpeedRunData(IEnumerable<SpeedRun> speedRuns) {

            if (speedRuns == null)
                throw new ArgumentNullException(nameof(speedRuns));

            foreach(SpeedRun sr in speedRuns) {

                string gameInfo = "Game: " + sr.Game.Name + " published by " + sr.Game.Publisher;

                if(sr.Game.ReleaseDate.HasValue)
                    gameInfo += " in " + sr.Game.ReleaseDate.Value.Year;
                
                Console.WriteLine(gameInfo);
                Console.WriteLine($"Player: {sr.Player.GamerTag}");
                Console.WriteLine($"Completion Time: {sr.Time}");
                Console.WriteLine($"Record Date: {sr.RecordDate.ToString("yyyy-MM-dd")}");

                Console.WriteLine("#################");

            }

        }

        private static void HandleError(ResultType resultType, string message) {

            ConsoleColor currentConsoleColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;

            if (resultType == ResultType.ServerError)
                Console.WriteLine("A server error occurred.");
            else
                Console.WriteLine("An unexpected error occurred.");

            Console.WriteLine("Error Message: " + message);

            Console.ForegroundColor = currentConsoleColor;
        }

        private static void PrintWelcomeMessage() {

            Console.WriteLine("Welcome to the Speed Run Webservice Client.");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine();

        }

    }

}
