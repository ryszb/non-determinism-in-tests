using NonDeterminismInTests.Logic.Naive;

namespace NonDeterminismInTests.Tests.Naive;

public class UpgradePressureCalculatorTests
{
    [Theory]
    [InlineData(0, 1000, 500, 0.5)]     // Midpoint
    [InlineData(0, 1000, 0, 0.0)]       // Exactly at min
    [InlineData(0, 1000, 1000, 1.0)]    // Exactly at max
    [InlineData(100, 1000, 0, 0.0)]     // Below min, clamped
    [InlineData(0, 1000, 1500, 1.0)]    // Above max, clamped
    public void Calculates_upgrade_pressure_correctly_for_specific_cases(
        int minThreshold, int maxThreshold, int usage, decimal expected)
    {
        var calculator = new UpgradePressureCalculator(minThreshold, maxThreshold);

        var result = calculator.Calculate(usage);

        Assert.Equal(expected, result);
    }
}