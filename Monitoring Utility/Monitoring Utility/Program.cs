using Monitoring_Utility;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        string processName;
        int monitoringFrequency, maxLifeTime;
        if (args.Length == 3) 
        {
            processName = args[0];
            if (!Int32.TryParse(args[1], out maxLifeTime))
            { throw new Exception("Couldn't parse an int from the second argument"); }
            if (!Int32.TryParse(args[2], out monitoringFrequency))
            { throw new Exception("Couldn't parse an int from the third argument"); } 
        }
        else 
        {
            processName = "notepad";
            monitoringFrequency = 1;
            maxLifeTime = 1;
        }

        var monitoringTask = new BackgroundMonitoring(monitoringFrequency, processName, maxLifeTime);
        monitoringTask.Start();

        ConsoleKeyInfo input;
        do
        {
            input = Console.ReadKey();
        }
        while (input.Key != ConsoleKey.Q);

        Console.Clear();
        monitoringTask.StopAsync();
    }
}

