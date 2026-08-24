using Kerem.CodingTracker.Features.CreateCodingSession;

namespace Kerem.CodingTracker.Tests;

public class ValidatorTests
{
    [Theory]
    [InlineData("2026-08-24 14:30", true)]
    [InlineData("2026-01-01 00:00", true)]
    [InlineData("2026-8-24 14:30", false)]
    [InlineData("2026-08-24 14:3", false)]
    [InlineData("24-08-2026 14:30", false)]
    [InlineData("2026-08-24", false)]
    [InlineData("2026-08-24T14:30", false)]
    [InlineData("", false)]
    [InlineData("not a date", false)]
    public void ValidateDateFormat_ReturnsExpectedResult(string input, bool expected)
    {
        var result = Validator.ValidateDateFormat(input);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("abort", true)]
    [InlineData("Abort", false)]
    [InlineData("ABORT", false)]
    [InlineData("", false)]
    [InlineData("2026-08-24 14:30", false)]
    public void Abort_ReturnsExpectedResult(string input, bool expected)
    {
        var result = Validator.Abort(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ValidateStartAndEndDate_ReturnsTrue_WhenStartIsBeforeEnd()
    {
        var start = new DateTime(2026, 8, 24, 9, 0, 0);
        var end = new DateTime(2026, 8, 24, 17, 0, 0);

        var result = Validator.ValidateStartAndEndDate(start, end);

        Assert.True(result);
    }

    [Fact]
    public void ValidateStartAndEndDate_ReturnsTrue_WhenStartEqualsEnd()
    {
        var same = new DateTime(2026, 8, 24, 9, 0, 0);

        var result = Validator.ValidateStartAndEndDate(same, same);

        Assert.True(result);
    }

    [Fact]
    public void ValidateStartAndEndDate_ReturnsFalse_WhenStartIsAfterEnd()
    {
        var start = new DateTime(2026, 8, 24, 17, 0, 0);
        var end = new DateTime(2026, 8, 24, 9, 0, 0);

        var result = Validator.ValidateStartAndEndDate(start, end);

        Assert.False(result);
    }

    [Fact]
    public void ValidateStartAndEndDate_ReturnsFalse_WhenEndIsOnEarlierDay()
    {
        var start = new DateTime(2026, 8, 24, 9, 0, 0);
        var end = new DateTime(2026, 8, 23, 9, 0, 0);

        var result = Validator.ValidateStartAndEndDate(start, end);

        Assert.False(result);
    }
}
