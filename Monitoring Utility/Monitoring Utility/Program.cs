using Monitoring_Utility;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        ParseArgs parseArgs = new(args);        

        var monitoringTask = 
            new BackgroundMonitoring(parseArgs.processName, parseArgs.maxLifeTime, parseArgs.monitoringFrequency);

        monitoringTask.Start();

        ConsoleKeyInfo input;
        do
        {
            while(!Console.KeyAvailable)
            {

            }
            input = Console.ReadKey();
        }
        while (input.Key != ConsoleKey.Q);

        Console.Clear();
        monitoringTask.StopAsync();
    }


}

