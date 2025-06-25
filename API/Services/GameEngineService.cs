using API.Interfaces;

namespace API.Services;

public class GameEngineService : IGameEngineService
{
     public string GenerateQuestion(int difficulty)
    {
        var rand = new Random();
        int numOperands = difficulty + 1;
        int digitLength = difficulty;

        List<int> numbers = new();
        List<char> ops = new();
        char[] allowedOps = ['+', '-', '*', '/'];

        for (int i = 0; i < numOperands; i++)
        {
            int number = rand.Next((int)Math.Pow(10, digitLength - 1), (int)Math.Pow(10, digitLength));
            numbers.Add(number);
            if (i < numOperands - 1)
                ops.Add(allowedOps[rand.Next(allowedOps.Length)]);
        }

        string question = "";
        for (int i = 0; i < numbers.Count; i++)
        {
            question += numbers[i];
            if (i < ops.Count)
                question += $" {ops[i]} ";
        }

        return question;
    }
    
}