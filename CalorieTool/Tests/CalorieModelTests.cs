using Xunit;

public class CalorieModelTests
{
    [Fact]
    public void CalculateCalories_ForMale_ReturnsCorrectBMR()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 30,
            Sex = "male",
            Weight = 180, // in pounds
            Height = 70,  // 5'10"
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.InRange(model.BMR, 1790, 1810); // Check BMR is in the expected range
    }

    [Fact]
    public void CalculateCalories_ForFemale_ReturnsCorrectBMR()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 30,
            Sex = "female",
            Weight = 150, // in pounds
            Height = 65,  // 5'5"
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.InRange(model.BMR, 1380, 1400); // Check BMR is in the expected range
    }

    [Theory]
    [InlineData("sedentary", 1.2)]
    [InlineData("light", 1.375)]
    [InlineData("moderate", 1.55)]
    [InlineData("heavy", 1.725)]
    [InlineData("intense", 1.9)]
    public void CalculateCalories_ForActivityLevel_ReturnsCorrectCaloriesToMaintain(string activityLevel, double multiplier)
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 25,
            Sex = "male",
            Weight = 200, // in pounds
            Height = 72,  // 6'0"
            ActivityLevel = activityLevel
        };

        // Act
        model.CalculateCalories();

        // Assert
        var expectedCaloriesToMaintain = model.BMR * multiplier;
        Assert.InRange(model.CaloriesToMaintain, expectedCaloriesToMaintain - 10, expectedCaloriesToMaintain + 10);
    }

    [Fact]
    public void CalculateCalories_WithZeroInputs_ShouldReturnZeroCalories()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 0, // Invalid input
            Sex = "male",
            Weight = 0, // Invalid input
            Height = 0, // Invalid input
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.Equal(0, model.BMR);
        Assert.Equal(0, model.CaloriesToMaintain);
        Assert.Equal(0, model.CaloriesToGain);
        Assert.Equal(0, model.CaloriesToLose);
    }

    [Fact]
    public void CalculateCalories_ForCaloricGainLoss_ReturnsCorrectValues()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 35,
            Sex = "male",
            Weight = 185, // in pounds
            Height = 71,  // 5'11"
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        // Check Calories to Gain and Calories to Lose
        Assert.Equal(model.CaloriesToMaintain + 500, model.CaloriesToGain);
        Assert.Equal(model.CaloriesToMaintain - 500, model.CaloriesToLose);
    }
    [Fact]
    public void CalculateCalories_ForMaxValues_ReturnsCorrectBMR()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 100,      // Max Age
            Sex = "male",
            Weight = 400,   // High weight (400 pounds)
            Height = 84,    // Tall height (7 feet)
            ActivityLevel = "intense"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.InRange(model.BMR, 2600, 2700); // Expected BMR range for extreme values
        Assert.InRange(model.CaloriesToMaintain, 4900, 5100); // Calories to maintain weight with intense activity
    }

    [Fact]
    public void CalculateCalories_InvalidGender_ReturnsDefaultBMR()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 25,
            Sex = "invalid",  // Invalid gender
            Weight = 160,
            Height = 68, // 5'8"
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.Equal(0, model.BMR); // Check if it handled the invalid gender by defaulting to 0 BMR
        Assert.Equal(0, model.CaloriesToMaintain); // Should return 0 for invalid input
    }

    [Fact]
    public void CalculateCalories_ForMinimumHeightAndWeight_ReturnsPositiveValues()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 18,
            Sex = "female",
            Weight = 90,  // Minimum reasonable weight
            Height = 54,  // Minimum reasonable height (4'6")
            ActivityLevel = "sedentary"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.InRange(model.BMR, 1050, 1150); // Expected BMR range for minimum height and weight
        Assert.InRange(model.CaloriesToMaintain, 1250, 1350); // Check calories to maintain for sedentary level
    }

    [Fact]
    public void CalculateCalories_WithNegativeValues_ShouldReturnZero()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = -5,    // Invalid negative age
            Sex = "male",
            Weight = -100,  // Invalid negative weight
            Height = -60,   // Invalid negative height
            ActivityLevel = "moderate"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.Equal(0, model.BMR); // Should return 0 for invalid negative values
        Assert.Equal(0, model.CaloriesToMaintain); // Should return 0 for invalid input
    }

    [Fact]
    public void CalculateCalories_ForDifferentActivityLevels_ReturnsExpectedResults()
    {
        // Arrange
        var model = new CalorieModel
        {
            Age = 30,
            Sex = "male",
            Weight = 160,
            Height = 70,  // 5'10"
            ActivityLevel = "heavy"
        };

        // Act
        model.CalculateCalories();

        // Assert
        Assert.InRange(model.BMR, 1650, 1750); // Check BMR for the given inputs
        Assert.InRange(model.CaloriesToMaintain, 2800, 2900); // Check calories for heavy activity
        Assert.InRange(model.CaloriesToGain, 3300, 3400); // Calories to gain weight
        Assert.InRange(model.CaloriesToLose, 2300, 2400); // Calories to lose weight
    }
}
