using Microsoft.VisualStudio.TestPlatform.TestHost;
using Monitoring_Utility;

namespace UtilityNUnitTests;

public class ParseArgsTests
{
    [TestCase(false, new string[] { "1"})]
    [TestCase(false, new string[] { "1", "2" })]
    public void Less_Than_3Args_Throws(bool someBool, string[] args)
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

    [TestCase("Opera Web Browser", new string[] { "Opera", "Web", "Browser", "1", "1" })]
    [TestCase("Microsoft Visual Studio", new string[] { "Microsoft", "Visual", "Studio", "1", "1" })]
    public void Process_Name_With_Spaces_Parses(string expected, string[] args)
    {
        // ACT
        ParseArgs parseArgs = new(args);

        // ASSERT
        Assert.That(parseArgs.processName, Is.EqualTo(expected));
    }

    [TestCase(false, new string[] { "process", "string", "1", "1" })]
    [TestCase(false, new string[] { "process", "10", "100" })]
    [TestCase(false, new string[] {"process","1","1"})]
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