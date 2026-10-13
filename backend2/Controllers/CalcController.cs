using Microsoft.AspNetCore.Mvc;
using backend2.Models;
using backend2.Services;

namespace backend2.Controllers
{
    public class CalcController : Controller
    {
        private readonly ICalculatorService _calcService;

        public CalcController(ICalculatorService calcService)
        {
            _calcService = calcService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manual()
        {
            if (Request.Method.Equals("POST", System.StringComparison.OrdinalIgnoreCase))
            {
                int a = int.Parse(Request.Form["a"]);
                int b = int.Parse(Request.Form["b"]);
                string op = Request.Form["op"];

                string result = _calcService.Calculate(a, b, op);
                ViewBag.Result = int.TryParse(result, out _) ? $"{a} {op} {b} = {result}" : result;
                return View("Result");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ManualWithSeparateHandlers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ManualWithSeparateHandlersPost()
        {
            int a = int.Parse(Request.Form["a"]);
            int b = int.Parse(Request.Form["b"]);
            string op = Request.Form["op"];

            string result = _calcService.Calculate(a, b, op);
            ViewBag.Result = int.TryParse(result, out _) ? $"{a} {op} {b} = {result}" : result;
            return View("Result");
        }

        [HttpGet]
        public IActionResult ModelBindingInParameters()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBindingInParameters(int a, int b, string op)
        {
            ViewBag.Result = _calcService.Calculate(a, b, op);
            return View("Result2");
        }

        [HttpGet]
        public IActionResult ModelBindingInSeparateModel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModelBindingInSeparateModel(CalcModel model)
        {
            ViewBag.Result = _calcService.Calculate(model.A, model.B, model.Op);
            return View("Result2");
        }
    }
}
