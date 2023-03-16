namespace Monitoring_Utility;

public class ParseArgs
{
    public void ValidateArgs(string[] args)
    {
        if(args.Length < 3)
        {
            throw new ArgumentException("Not enough arguments");
        }
        if (!Int32.TryParse(args[args.Length - 1], out _) ||
            !Int32.TryParse(args[args.Length - 2], out _))
        {
            throw new ArgumentException("Last 2 arguments must be int.");
        }
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

        if (!Int32.TryParse(args[args.Length - 1], out returnIntArray[0]))
        { throw new Exception("Couldn't parse an int from the second argument"); }
        if (!Int32.TryParse(args[args.Length - 2], out returnIntArray[1]))
        { throw new Exception("Couldn't parse an int from the third argument"); }

        return returnIntArray;
    }
}
