using API.DTO;
using API.Interfaces;
using System.Data;

namespace API.Services
{
    public class GameEngineService : IGameEngineService
    {
        public GeneratedQuestionDto GenerateQuestion(int difficulty)
        {
            int numOperands = difficulty;
            int digitLength = difficulty;
            var rand = new Random();
            if (difficulty == 0)
            {
                var num = rand.Next(1, 5);
                numOperands = Math.Max(2, difficulty + num);
                digitLength = difficulty + num;
            }
            else
            {
                numOperands = difficulty + 1;
                digitLength = difficulty;
            }

            

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

            string equation = "";
            for (int i = 0; i < numbers.Count; i++)
            {
                equation += numbers[i];
                if (i < ops.Count)
                    equation += $" {ops[i]} ";
            }

            float answer;
            try
            {
                var result = new DataTable().Compute(equation, null);
                answer = Convert.ToSingle(result);
            }
            catch
            {
                equation = "1 + 1";
                answer = 2;
            }

            return new GeneratedQuestionDto
            {
                Equation = equation,
                Answer = answer
            };
        }

    }
}
