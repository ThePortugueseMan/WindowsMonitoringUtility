namespace Monitoring_Utility;

internal class ProcessBackup
{
    public DateTime startDate;
    public DateTime killDate;
    public string processName;
    public int processId;
    public TimeSpan lifetime;

    public ProcessBackup(DateTime _startDate, string _processName, DateTime _killDate, int _processId)
    {
        startDate = _startDate;
        killDate = _killDate;
        processName = _processName;
        processId = _processId;

        lifetime = killDate.Subtract(startDate);
    }
}
