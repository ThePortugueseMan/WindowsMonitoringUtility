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

    [Test]
    public void NonParsable_Int_On_Last_2Args_Throws()
    {
        // ARRANGE
        string[] inputArgs = { "process", "p", "1" };

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
        string[] inputArgs = { "Microsoft", "Visual", "Studio", "1", "1"};
        ParseArgs parseArgs = new();

        // ACT
        string processName = parseArgs.GetProcessNameFromArgs(inputArgs);

        // ASSERT
        Assert.That(processName, Is.EqualTo("Microsoft Visual Studio"));
    }


    
}