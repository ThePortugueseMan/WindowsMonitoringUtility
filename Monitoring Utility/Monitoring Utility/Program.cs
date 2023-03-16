using Monitoring_Utility;
using System.Diagnostics;
using System.Threading;

internal class Program
{
    private static void Main(string[] args)
    {
        string processName;
        int monitoringFrequency, maxLifeTime;
        ParseArgs parseArgs = new();

        parseArgs.ValidateArgs(args);
        processName = parseArgs.GetProcessNameFromArgs(args);
        if (false)
        {
            processName = args[0];

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

