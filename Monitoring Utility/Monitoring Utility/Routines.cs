using System.Diagnostics;

namespace Monitoring_Utility;

internal class Routines
{
    string _processName;
    int _maxLifeTime;
    Logs logs = new();

    public Routines(int _maxlifeTime, string _processName)
    {
        this._processName = _processName;
        this._maxLifeTime = _maxlifeTime;
    }


    public void CheckingRoutine()
    {
        Process[] activeProcesses = Process.GetProcessesByName(_processName);

        foreach (Process process in activeProcesses) 
        {
            if(DateTime.Now.Subtract(process.StartTime).TotalMinutes >= _maxLifeTime)
                { KillProcessRoutine(process); }
        }
    }
    private void KillProcessRoutine(Process process)
    {
        ProcessBackup processBackup = 
            new(process.StartTime, process.ProcessName, DateTime.Now, process.Id);

        try
        {
            process.Kill();
            logs.WriteLine($"{processBackup.killDate} [{processBackup.processName}, Id: {processBackup.processId}]: " +
                $"KILLED with {Math.Truncate(processBackup.lifetime.TotalMinutes)}:{processBackup.lifetime.Seconds} (mm:ss)");
        }
        catch (Exception) { throw new Exception("Couldn't complete KillProcessRoutine"); }
        
    }
}
