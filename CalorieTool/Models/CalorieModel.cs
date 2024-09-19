using System.ComponentModel.DataAnnotations;

public class CalorieModel
{
    [Required]
    public int Age { get; set; }

    [Required]
    public string Sex { get; set; }

    [Required]
    public double Weight { get; set; }

    [Required]
    public int Height { get; set; }

    [Required]
    public string ActivityLevel { get; set; }

    public double BMR { get; private set; }
    public double CaloriesToMaintain { get; private set; }
    public double CaloriesToGain { get; private set; }
    public double CaloriesToLose { get; private set; }

    // Method to calculate BMR and calorie expenditure
    public void CalculateCalories()
    {
        if (Sex == "male")
        {
            BMR = ((10 * Weight / 2.2) + (6.25 * Height / 0.393701) - (5 * Age) + 5);
        }
        else
        {
            BMR = ((10 * Weight / 2.2) + (6.25 * Height / 0.393701) - (5 * Age) - 161);
        }

        switch (ActivityLevel)
        {
            case "sedentary":
                CaloriesToMaintain = BMR * 1.2;
                break;
            case "light":
                CaloriesToMaintain = BMR * 1.375;
                break;
            case "moderate":
                CaloriesToMaintain = BMR * 1.55;
                break;
            case "heavy":
                CaloriesToMaintain = BMR * 1.725;
                break;
            case "intense":
                CaloriesToMaintain = BMR * 1.9;
                break;
            default:
                CaloriesToMaintain = BMR;
                break;
        }

        CaloriesToGain = CaloriesToMaintain + 500;
        CaloriesToLose = CaloriesToMaintain - 500;
    }
}
