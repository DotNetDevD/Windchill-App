using Microsoft.AspNetCore.Mvc;
using Windchill_App.Models;

namespace Windchill_App.Controllers
{
    public class WindChillController : Controller
    {
        [HttpGet]
        public IActionResult CalculateEnglishWindChill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateEnglishWindChill(WindChillInputModel windChillInput)
        {

            double windChillFahrenheit = CalculateСhillFactor(windChillInput, 35.74, 0.6215, 35.75, 0.16, 0.4275);

            if (windChillInput.Temperature == Convert.ToDouble(TempData["Temperature"]) ||
            windChillInput.WindSpeed == Convert.ToDouble(TempData["WindSpeed"]))
            {
                ViewBag.AdditionMessage = "Do you wanna to enter new data values?";
            }

            if (windChillFahrenheit >= windChillInput.Temperature)
            {
                ViewBag.Error = "Wind chill factor is greater than or equal to the actual temperature.";
            }
            else if (windChillInput.WindSpeed < 4)
            {
                ViewBag.Error = "Wind can't be below 0";
            }

            ViewBag.Result = Math.Round(windChillFahrenheit, 2);

            TempData["Temperature"] = windChillInput.Temperature.ToString();
            TempData["WindSpeed"] = windChillInput.WindSpeed.ToString();

            return View();
        }

        [HttpGet]
        public IActionResult CalculateMetricWindChill()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateMetricWindChill(WindChillInputModel windChillInput)
        {

            double windChillCelsius = CalculateСhillFactor(windChillInput, 13.12, 0.6215, 11.37, 0.16, 0.3965);

            if (windChillInput.Temperature == Convert.ToDouble(TempData["Temperature"]) ||
            windChillInput.WindSpeed == Convert.ToDouble(TempData["WindSpeed"]))
            {
                ViewBag.AdditionMessage = "Do you wanna to enter new data values?";
            }

            if (windChillCelsius >= windChillInput.Temperature)
            {
                ViewBag.Error = "Wind chill factor is greater than or equal to the actual temperature.";
            }
            else if (windChillInput.WindSpeed < 3)
            {
                ViewBag.Error = "Wind can't below 0";
            }

            ViewBag.Result = Math.Round(windChillCelsius, 2);

            TempData["Temperature"] = windChillInput.Temperature.ToString();
            TempData["WindSpeed"] = windChillInput.WindSpeed.ToString();

            return View();
        }

        private double CalculateСhillFactor(WindChillInputModel windChillInput, double constanta,
            double constantaTemperature, double constantaSpeedOne, double coefPow, double constantaSpeedTwo)
        {
            return constanta + constantaTemperature * windChillInput.Temperature - constantaSpeedOne * Math.Pow(windChillInput.WindSpeed, coefPow)
                                  + constantaSpeedTwo * windChillInput.Temperature * Math.Pow(windChillInput.WindSpeed, coefPow);
        }
    }
}
