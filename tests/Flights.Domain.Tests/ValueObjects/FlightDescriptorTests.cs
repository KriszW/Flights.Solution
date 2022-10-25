namespace Flights.Domain.Tests.ValueObjects;

public class FlightDesignatorTests
{
    [Theory]
    [InlineData("EZY2016")]
    [InlineData("W93649")]
    [InlineData("FR8143")]
    public void Should_Be_Valid(string designator)
    {
        // Arrange

        // Act

        var flightDesignator = FlightDesignator.Create(designator);

        // Assert
        Assert.True(flightDesignator.IsSuccess);
        Assert.False(flightDesignator.IsFailure);
        Assert.Empty(flightDesignator.Error.Code);
        Assert.Empty(flightDesignator.Error.Message);
        Assert.NotNull(flightDesignator.Value);
    }

    [Theory]
    [InlineData("EZY-2016")]
    [InlineData("/W93649")]
    [InlineData("FR.8143")]
    public void Should_Be_InValid_For_ContainingNotOnlyDigitOrLetters(string designator)
    {
        // Arrange

        // Act

        var flightDesignator = FlightDesignator.Create(designator);

        // Assert
        Assert.False(flightDesignator.IsSuccess);
        Assert.True(flightDesignator.IsFailure);
        Assert.NotEmpty(flightDesignator.Error.Code);
        Assert.NotEmpty(flightDesignator.Error.Message);
        Assert.NotNull(flightDesignator.Error);
    }

    [Fact]
    public void Should_Be_InValid_For_Empty()
    {
        // Arrange

        // Act

        var flightDesignator = FlightDesignator.Create(string.Empty);

        // Assert
        Assert.False(flightDesignator.IsSuccess);
        Assert.True(flightDesignator.IsFailure);
        Assert.NotEmpty(flightDesignator.Error.Code);
        Assert.NotEmpty(flightDesignator.Error.Message);
        Assert.NotNull(flightDesignator.Error);
    }
}
