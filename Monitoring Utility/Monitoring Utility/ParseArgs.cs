namespace Monitoring_Utility;

public class ParseArgs
{
    public string processName;
    public int maxLifeTime, monitoringFrequency;

    public ParseArgs(string[] args)
    {
        if (args.Length < 3)
        {
            throw new ArgumentException("Not enough arguments");
        }

        processName = GetProcessNameFromArgs(args);

        int[] lastTwoInts = GetLastTwoIntsFromArgs(args);
        maxLifeTime = lastTwoInts[0];
        monitoringFrequency = lastTwoInts[1];

    }

    public string GetProcessNameFromArgs(string[] args)
    {
        string[] processNameArray = new string[args.Length - 2];
        Array.Copy(args, processNameArray, args.Length - 2);

        return string.Join(" ", processNameArray);
    }

    public int[] GetLastTwoIntsFromArgs(string[] args)
    {
        int[] returnIntArray = new int[2];

        if (!Int32.TryParse(args[args.Length - 2], out returnIntArray[0]))
        { 
            throw new Exception("Couldn't parse an int from the second to last argument"); 
        }
        else if (returnIntArray[0] < 0) 
            { throw new FormatException("Lifetime can't be negative"); }

        if (!Int32.TryParse(args[args.Length - 1], out returnIntArray[1]))
        { 
            throw new Exception("Couldn't parse an int from the third argument");
        }
        else if (returnIntArray[1] < 0)
            { throw new FormatException("Monitoring Frequency can't be negative"); }

        return returnIntArray;
    }
}
