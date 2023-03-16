using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Utility;

public class BackgroundMonitoring
{
    private Task? monitoringTask;
    private readonly PeriodicTimer timer;
    private readonly CancellationTokenSource cts = new();
    private Routines ops;


    public BackgroundMonitoring(string processName, int maxLifeTime, int frequencyInMinutes)
    {
        try 
        {
            timer = new PeriodicTimer(TimeSpan.FromMinutes(frequencyInMinutes));
        }
        catch(ArgumentOutOfRangeException)
        {
            timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
        }
        
        ops = new Routines(maxLifeTime, processName);
    }

    public void Start()
    {
        monitoringTask = MonitoringWorkAsync();
        Console.WriteLine($"{DateTime.Now} - Monitoring started. Press q to stop.");
    }

    private async Task MonitoringWorkAsync()
    {
        try 
        {
            do
            {
                ops.CheckingRoutine();
            }
            while (await timer.WaitForNextTickAsync(cts.Token));     
        }
        catch(OperationCanceledException) { }
    }

    public async Task StopAsync()
    {
        if (monitoringTask is null)
        {
            return;
        }

        cts.Cancel();
        await monitoringTask;
        cts.Dispose();
    }

}
