using System.ComponentModel.DataAnnotations;
using Backend3.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Backend3.Models
{
    public class QuizViewModel
    {
        [BindNever]
        public string Question { get; set; }

        [Range(0, 100)]
        public int Step { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "Please enter your answer.")]
        [StringLength(6, ErrorMessage = "The Answer is too long.")]
        [IntegerAnswer(MinValue = -100, MaxValue = 100)]
        public string Answer { get; set; }
    }

    public class QuizResultItem
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class QuizResultViewModel
    {
        public int Right { get; set; }
        public int Total { get; set; }
        public List<QuizResultItem> Items { get; set; } = new();
    }
}
