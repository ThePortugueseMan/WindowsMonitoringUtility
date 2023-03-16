namespace Monitoring_Utility;

public class Logs
{
    public string logFilePath = $"{Environment.CurrentDirectory}\\logs.log";

    public void WriteLine(string message)
    {
        using (StreamWriter w = File.AppendText(logFilePath))
        {
            w.WriteLine(message);
        }
    }
}