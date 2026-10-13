using System.Globalization;
using Backend3.Infrastructure;
using Backend3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend3.Controllers
{
    public class MockupsController : Controller
    {
        private const string StateKey = "QuizState";
        private const string CommandNext = "next";
        private const string CommandFinish = "finish";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            var state = QuizState.Create();
            HttpContext.Session.SetObject(StateKey, state);
            return View(BuildViewModel(state));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Quiz(QuizViewModel model, string command)
        {
            var state = HttpContext.Session.GetObject<QuizState>(StateKey);
            if (state == null || state.IsFinished)
            {
                return RedirectToAction(nameof(Quiz));
            }

            if (model.Step != state.Current)
            {
                ModelState.AddModelError(string.Empty,
                    "This form is out of date. Please answer the current question.");
                ModelState.Remove(nameof(model.Step));
                ModelState.Remove(nameof(model.Answer));
                return View(BuildViewModel(state));
            }

            if (command != CommandNext && command != CommandFinish)
            {
                ModelState.AddModelError(string.Empty, "Unknown action.");
            }

            if (!ModelState.IsValid)
            {
                var invalid = BuildViewModel(state);
                invalid.Answer = model.Answer;
                return View(invalid);
            }

            state.Answers[state.Current] = int.Parse(
                model.Answer.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);

            if (command == CommandFinish || state.IsLastQuestion)
            {
                state.IsFinished = true;
                HttpContext.Session.SetObject(StateKey, state);
                return View("QuizResult", BuildResult(state));
            }

            state.Current++;
            HttpContext.Session.SetObject(StateKey, state);

            ModelState.Clear();
            return View(BuildViewModel(state));
        }

        [HttpGet]
        public IActionResult QuizResult()
        {
            var state = HttpContext.Session.GetObject<QuizState>(StateKey);
            if (state == null || !state.IsFinished)
            {
                return RedirectToAction(nameof(Quiz));
            }

            return View(BuildResult(state));
        }

        private static QuizViewModel BuildViewModel(QuizState state)
        {
            return new QuizViewModel
            {
                Question = state.CurrentQuestion.Text,
                Step = state.Current
            };
        }

        private static QuizResultViewModel BuildResult(QuizState state)
        {
            var result = new QuizResultViewModel();

            for (var i = 0; i < state.Questions.Count; i++)
            {
                if (state.Answers[i] is not int given)
                {
                    continue;
                }

                var question = state.Questions[i];
                result.Items.Add(new QuizResultItem
                {
                    Text = $"{question.Text} = {given}",
                    IsCorrect = given == question.CorrectAnswer
                });
            }

            result.Total = result.Items.Count;
            result.Right = result.Items.Count(x => x.IsCorrect);
            return result;
        }
    }
}
