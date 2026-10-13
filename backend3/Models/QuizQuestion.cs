using System.Text.Json.Serialization;

namespace Backend3.Models
{
    public class QuizQuestion
    {
        public int Left { get; set; }
        public int Right { get; set; }
        public string Operator { get; set; }

        [JsonIgnore]
        public string Text => $"{Left} {Operator} {Right}";

        [JsonIgnore]
        public int CorrectAnswer => Operator == "+" ? Left + Right : Left - Right;
    }
}
