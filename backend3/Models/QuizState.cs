using System.Text.Json.Serialization;

namespace Backend3.Models
{
    public class QuizState
    {
        public const int QuestionCount = 4;

        public List<QuizQuestion> Questions { get; set; } = new();

        public List<int?> Answers { get; set; } = new();

        public int Current { get; set; }

        public bool IsFinished { get; set; }

        [JsonIgnore]
        public QuizQuestion CurrentQuestion => Questions[Current];

        [JsonIgnore]
        public bool IsLastQuestion => Current >= Questions.Count - 1;

        public static QuizState Create(int count = QuestionCount)
        {
            var rng = Random.Shared;
            var state = new QuizState();

            for (var i = 0; i < count; i++)
            {
                state.Questions.Add(new QuizQuestion
                {
                    Left = rng.Next(1, 10),
                    Right = rng.Next(1, 10),
                    Operator = rng.Next(2) == 0 ? "+" : "-"
                });
                state.Answers.Add(null);
            }

            return state;
        }
    }
}
