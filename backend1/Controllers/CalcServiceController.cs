using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    public class CalcServiceController : Controller
    {
        private readonly ICalcService _calcService;

        public CalcServiceController(ICalcService calcService)
        {
            _calcService = calcService;
        }

        public IActionResult PassUsingModel()
        {
            var model = new CalcViewModel
            {
                RandValueFirst = _calcService.RandValueFirst,
                RandValueSecond = _calcService.RandValueSecond,
                Add = _calcService.GetAdd(),
                Sub = _calcService.GetSub(),
                Mult = _calcService.GetMult(),
                Div = _calcService.GetDiv()
            };
            return View(model);
        }

        public IActionResult PassUsingViewData()
        {
            ViewData["RandValueFirst"] = _calcService.RandValueFirst;
            ViewData["RandValueSecond"] = _calcService.RandValueSecond;
            ViewData["Add"] = _calcService.GetAdd();
            ViewData["Sub"] = _calcService.GetSub();
            ViewData["Mult"] = _calcService.GetMult();
            ViewData["Div"] = _calcService.GetDiv();
            return View();
        }

        public IActionResult PassUsingViewBag()
        {
            ViewBag.RandValueFirst = _calcService.RandValueFirst;
            ViewBag.RandValueSecond = _calcService.RandValueSecond;
            ViewBag.Add = _calcService.GetAdd();
            ViewBag.Sub = _calcService.GetSub();
            ViewBag.Mult = _calcService.GetMult();
            ViewBag.Div = _calcService.GetDiv();
            return View();
        }

        public IActionResult AccessServiceDirectly()
        {
            return View();
        }
    }
}
