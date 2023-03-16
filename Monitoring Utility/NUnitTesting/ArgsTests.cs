using Microsoft.VisualStudio.TestPlatform.TestHost;
using Monitoring_Utility;

namespace UtilityNUnitTests;

public class ArgsTests
{
    [Test]
    public void Less_Than_3Args_Throws()
    {
        // ARRANGE
        string[] inputArgs = { "process", "2" };

        // ACT + ASSERT
        Assert.Throws<ArgumentException>(() => Program.Main(inputArgs));
    }

    [TestCase("p", "1")]
    [TestCase("1", "&")]
    public void NonParsable_Int_On_Last_2Args_Throws(string argBeforeLast, string lastArg)
    {
        // ARRANGE
        string[] inputArgs = { "process", argBeforeLast, lastArg };

        // ACT + ASSERT
        Assert.Throws<ArgumentException>(() => Program.Main(inputArgs));
    }

    [Test]
    public void Null_Arg_Throws()
    {
        // ARRANGE
        string[] inputArgs = { "process", "2", null };

        // ACT + ASSERT
        Assert.Throws<NullReferenceException>(() => Program.Main(inputArgs));
    }

    [Test]
    public void Process_Name_With_Spaces_Parses()
    {
        // ARRANGE
        string[] inputArgs = { "Microsoft", "Visual", "Studio", "1", "1" };

        // ACT
        ParseArgs parseArgs = new(inputArgs);

        // ASSERT
        Assert.That(parseArgs.processName, Is.EqualTo("Microsoft Visual Studio"));
    }

    [TestCase(false, new string[] {"notepad","1","1"})]
    public void Parses_Last_2Ints(bool someBool, string[] args)
    {
        // ARRANGE
        int maxLifeTime = Int32.Parse(args[args.Length - 2]);
        int monitoringFrequency = Int32.Parse(args[args.Length - 1]);

        // ACT
        ParseArgs parseArgs = new(args);

        // ASSERT
        Assert.Multiple(() =>
        {
            Assert.That(parseArgs.maxLifeTime, Is.EqualTo(maxLifeTime));
            Assert.That(parseArgs.monitoringFrequency, Is.EqualTo(monitoringFrequency));
        });
        
    }



}