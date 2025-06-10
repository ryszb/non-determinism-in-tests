using NonDeterminismInTests.Logic.Refactored;
using static NonDeterminismInTests.Tests.Refactored.RandomNumberGenerator;

namespace NonDeterminismInTests.Tests.Refactored;

public class UsageAmountTests
{
    [Fact]
    public void Passing_non_negative_value_in_ctor()
    {
        var validValue = AnyIntBiggerOrEqualThan(0);

        var usage = new UsageAmount(validValue);

        Assert.Equal(validValue, usage.Value);
    }

    [Fact]
    public void Passing_negative_value_in_ctor()
    {
        int invalidValue = AnyIntLowerThan(0);

        Assert.Throws<ArgumentOutOfRangeException>(() => new UsageAmount(invalidValue));
    }

    [Fact]
    public void Implicit_conversion_to_int()
    {
        var validValue = AnyIntBiggerOrEqualThan(0);
        var usage = new UsageAmount(validValue);

        int convertedValue = usage;

        Assert.Equal(validValue, convertedValue);
    }
}
