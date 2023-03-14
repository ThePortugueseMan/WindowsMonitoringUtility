namespace Monitoring_Utility;

internal class Logs
{
    string logFilePath = $"{Environment.CurrentDirectory}\\logs.log";

    public void WriteLine(string message)
    {
        using (StreamWriter w = File.AppendText(logFilePath))
        {
            w.WriteLine(message);
        }
    }


}
