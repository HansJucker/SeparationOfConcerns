public class MontyHall
{
    public static void Play(int times)
    {
        if (times < 0)
        {
            throw new ArgumentException("negative numbers are not supported");
        }
        var wonSticking = 0;
        for (var i = 0; i < times; i++)
        {
            // first, prepare the game
            var doorsWithPrice = PrepareDoors();

            // second, let the player make his guess
            var playerGuess = GetRandomInt();


            // third, reveal a losing door to be eliminated from the choices
            var choices = RemoveLosingDoor(doorsWithPrice, playerGuess);


            // fourth, count wins by 1) sticking to choice, and 2) changing choice
            var stickingWins = StickingWins(doorsWithPrice, choices, playerGuess);

            if (stickingWins)
            {
                wonSticking++;
            } 
        }

        // finally, print the statistics
        PrintResults(times, wonSticking);
    }

    public static bool StickingWins(Dictionary<int, bool> doorsWithPrice, HashSet<int> choices, int playerGuess)
    {
        return doorsWithPrice[playerGuess];
    }

    public static void PrintResults(int times, int wonSticking)
    {
        var wonChanging = times - wonSticking;

        Console.WriteLine($"played {times} times");
        Console.WriteLine($"won {wonSticking} times by sticking to choice");
        Console.WriteLine($"won {wonChanging} times by changing the choice");
        Console.WriteLine($"sticking wins {wonSticking / (float)times * 100:0.00}% of games");
        Console.WriteLine($"changing wins {(wonChanging) / (float)times * 100:0.00}% of games");
    }

    public static Dictionary<int, bool> PrepareDoors()
    {
        var doorsWithPrice = new Dictionary<int, bool> { { 1, false }, { 2, false }, { 3, false } };        
        doorsWithPrice[GetRandomInt()] = true;

        return doorsWithPrice;
    }

    public static int GetRandomInt()
    {
        return (int)(new Random().NextInt64() % 3 + 1);
    }

    public static HashSet<int> RemoveLosingDoor(Dictionary<int, bool> doors, int playerGuess) 
    {
        var choices =  doors.Keys.ToHashSet();

        for (var j = 1; j <= 3; j++)
        {
            if (j != playerGuess && doors[j] == false)
            {
                choices.Remove(j);
                break;
            }
        }
        return choices;
    }
}