using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Diagnostics;
using Monitoring_Utility;
using System.ComponentModel;

namespace UtilityNUnitTests;

internal class MonitoringTests
{
    [TestCase("notepad")]
    [TestCase("calc")]
    public void Zero_LifeTime_Terminates_Process_When_Called(string processName)
    {
        // ARRANGE      
        Process process = Process.Start(processName);

        // ACT
        BackgroundMonitoring bckgMonitor =
            new BackgroundMonitoring(processName, 0, 0);
        bckgMonitor.Start();
        bckgMonitor.StopAsync();

        // ASSERT        
        Assert.That(
            () => process.HasExited, Is.EqualTo(true).After(2).Seconds.PollEvery(1).Seconds);
    }

    [TestCase("notepad", 1)]
    [TestCase("notepad", 2)]
    public void Terminates_Process_With_X_LifeTime_In_Minutes(string processName, int lifeTime)
    {
        // ARRANGE
        int monitoringFrequency = 0;
        Process process = Process.Start(processName);

        // ACT
        BackgroundMonitoring bckgMonitor =
            new BackgroundMonitoring(processName, lifeTime, monitoringFrequency);
        bckgMonitor.Start();

        // ASSERT
        Assert.That(
            () => process.HasExited, Is.EqualTo(true).After(lifeTime).Minutes.PollEvery(1).Seconds);
    }
}
