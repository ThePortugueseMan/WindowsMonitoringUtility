using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitoring_Utility;

internal class BackgroundMonitoring
{
    private Task? monitoringTask;
    private readonly PeriodicTimer timer;
    private readonly CancellationTokenSource cts = new();
    private Routines ops;


    public BackgroundMonitoring(int frequencyInMinutes, string processName, int maxLifeTime)
    {
        timer = new PeriodicTimer(TimeSpan.FromMinutes(frequencyInMinutes));
        ops = new Routines(maxLifeTime, processName);
    }

    public void Start()
    {
        monitoringTask = MonitoringWorkAsync();
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
