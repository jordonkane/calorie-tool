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
}
