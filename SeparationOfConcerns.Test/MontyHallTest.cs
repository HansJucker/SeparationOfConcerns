


namespace SeparationOfConcerns.Test;


public class MontyHallTests
{
    [Fact]
    public void Play_WithNegativeTimes_ThrowsArgumentException()
    {
        // Arrange
        int invalidTimes = -1;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => MontyHall.Play(invalidTimes));
        Assert.Equal("negative numbers are not supported", exception.Message);
    }

    [Fact]
    public void StickingWins_WithCorrectGuess_ReturnsTrue()
    {
        // Arrange
        var doors = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false } };
        var playerGuess = 2;

        // Act
        var result = MontyHall.StickingWins(doors, new HashSet<int> { 1, 2, 3 }, playerGuess);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void StickingWins_WithIncorrectGuess_ReturnsFalse()
    {
        // Arrange
        var doors = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false } };
        var playerGuess = 1;

        // Act
        var result = MontyHall.StickingWins(doors, new HashSet<int> { 1, 2, 3 }, playerGuess);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void PrepareDoors_AssignsOnePriceToARandomDoor()
    {
        // Act
        var doors = MontyHall.PrepareDoors();

        // Assert
        var prizeDoorCount = doors.Values.Count(value => value == true);
        Assert.Equal(1, prizeDoorCount); // Only one door should have a price
    }

    [Fact]
    public void RemoveLosingDoor_RemovesCorrectLosingDoor()
    {
        // Arrange
        var doors = new Dictionary<int, bool> { { 1, false }, { 2, true }, { 3, false } };
        var playerGuess = 2; // Player guesses door 2, which has the prize

        // Act
        var remainingChoices = MontyHall.RemoveLosingDoor(doors, playerGuess);

        // Assert
        Assert.Contains(2, remainingChoices); // Player's choice should remain
        Assert.DoesNotContain(1, remainingChoices); // Door 1 should be removed as a losing door
        Assert.Contains(3, remainingChoices); // Door 3 should be removed as a losing door
    }

    [Fact]
    public void GetRandomInt_ReturnsValidDoorNumber()
    {
        // Act
        var result = MontyHall.GetRandomInt();

        // Assert
        Assert.InRange(result, 1, 3); // Result should be between 1 and 3
    }

    [Fact]
    public void Play_PrintsCorrectResults_WhenPlayedMultipleTimes()
    {
        // Arrange
        var timesToPlay = 100;

        // Capture console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            MontyHall.Play(timesToPlay);

            // Assert
            var output = sw.ToString();
            Assert.Contains($"played {timesToPlay} times", output);
            Assert.Contains("won", output); // Checking for winning stats
        }
    }
}
