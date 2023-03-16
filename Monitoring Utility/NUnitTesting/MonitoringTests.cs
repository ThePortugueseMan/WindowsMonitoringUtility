using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Diagnostics;
using Monitoring_Utility;
using System.ComponentModel;

namespace UtilityNUnitTests;

internal class MonitoringTests
{
    [Test]
    public void Zero_LifeTime_Terminates_Process_When_Called()
    {
        // ARRANGE
        string processName = "notepad";       
        Process process = Process.Start(processName);

        // ACT
        BackgroundMonitoring bckgMonitor =
            new BackgroundMonitoring(processName, 0, 0);
        bckgMonitor.Start();
        bckgMonitor.StopAsync();

        // ASSERT        
        Assert.IsTrue(process.HasExited);
    }

    [TestCase(1)]
    public void Terminates_Process_With_X_LifeTime_In_Minutes(int lifeTime)
    {
        // ARRANGE
        string processName = "notepad";
        int monitoringFrequency = 0;
        Process process = Process.Start(processName);
        Thread.Sleep(1000);


        // ACT
        BackgroundMonitoring bckgMonitor =
            new BackgroundMonitoring(processName, lifeTime, monitoringFrequency);
        bckgMonitor.Start();

        // ASSERT
        Assert.That(
            () => process.HasExited, Is.EqualTo(true).After(lifeTime).Minutes.PollEvery(1).Seconds);
    }
}
