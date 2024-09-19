using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(CalorieModel model)
    {
        if (ModelState.IsValid)
        {
            // Perform calorie calculation
            model.CalculateCalories();

            // Pass the model with calculated values back to the view
            return View(model);
        }

        // If the model is invalid, return the same view with error messages
        return View(model);
    }
}
