using Monitoring_Utility;

namespace UtilityNUnitTests;

internal class LogTests
{
    [TestCase("LogWriting Test")]
    [TestCase("Another Test")]
    public void Writes_Message_To_Logs_Last_Line(string message)
    {
        // ARRANGE
        Logs logs = new();

        // ACT
        logs.WriteLine(message);

        // ASSERT
        Assert.That(message, Is.EqualTo(Get_Logs_Last_Line(logs)));
    }

    public string Get_Logs_Last_Line(Logs log)
    {
        return File.ReadLines(log.logFilePath).Last();
    }
}
