using Monitoring_Utility;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        /*
        if (args.Length != 3) { throw new Exception("Program expects 3 arguments"); }
        
        string programName = args[0];
        if(!Int32.TryParse(args[1], out int maxLifeTime))
            {throw new Exception("Couldn't parse an int from the second argument");}
        if (!Int32.TryParse(args[2],out int monitoringFrequency))
        { throw new Exception("Couldn't parse an int from the third argument"); }
        */

        int maxLifeTime = 1;
        int monitoringFrequency = 1;
        string processName = "Notepad";

        var monitoringTask = new BackgroundMonitoring(monitoringFrequency, processName, maxLifeTime);
        monitoringTask.Start();

        ConsoleKeyInfo input;
        do
        {
            input = Console.ReadKey();
        }
        while (input.Key != ConsoleKey.Q);

        monitoringTask.StopAsync();
    }
}

