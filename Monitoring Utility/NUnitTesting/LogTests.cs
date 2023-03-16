using Monitoring_Utility;

namespace UtilityNUnitTests;

internal class LogTests
{
    [Test]
    public void Writes_Message_To_Log_Last_Line()
    {
        // ARRANGE
        string message = "Log Writing Test";
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
