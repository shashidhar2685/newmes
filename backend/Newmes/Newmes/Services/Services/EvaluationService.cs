using System.Linq;
using Newmes.Api.Models;

namespace Newmes.Api.Services
{
    /// <summary>
    /// Very‑basic evaluator.  Replace the IsCorrect() body with a real DB look‑up.
    /// </summary>
    // Top: using Newmes.Models; (or Newmes.Api.Models) – keep correct namespace

    public static class EvaluationService
    {
        public class EvalResult
        {
            public int Total { get; set; }
            public int Correct { get; set; }
            public int Wrong { get; set; }
            public int NotAttempted { get; set; }
            public int Score { get; set; }
        }

        public static EvalResult Evaluate(TestSubmitDto dto)
        {
            int total = dto.Answers.Count;
            int correct = 0;
            int wrong = 0;
            int notAttempted = 0;

            foreach (var ans in dto.Answers)
            {
                // treat null/empty as not attempted
                if (string.IsNullOrWhiteSpace(ans.SelectedOption))
                {
                    notAttempted++;
                    continue;
                }

                string userAns = ans.SelectedOption.Trim().ToUpper();

                // TODO: real DB lookup – for now stub:
                bool isCorrect = FakeIsCorrect(ans.QuestionId, userAns);

                if (isCorrect) correct++; else wrong++;
            }

            return new EvalResult
            {
                Total = total,
                Correct = correct,
                Wrong = wrong,
                NotAttempted = notAttempted,
                Score = correct
            };
        }

        // Temporary stub – replace later
        private static bool FakeIsCorrect(int qId, string userAns)
            => userAns == "B";   // Every “B” is marked correct for quick testing
    }

}
