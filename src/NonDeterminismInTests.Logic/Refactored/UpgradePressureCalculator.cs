namespace NonDeterminismInTests.Logic.Refactored;

public class UpgradePressureCalculator
{
    private readonly int _minThreshold;
    private readonly int _maxThreshold;

    public UpgradePressureCalculator(int minThreshold, int maxThreshold)
    {
        _minThreshold = minThreshold;
        _maxThreshold = maxThreshold;
    }

    public decimal Calculate(int usage)
    {
        var range = _maxThreshold - _minThreshold;
        var delta = usage - _minThreshold;

        return Math.Min(1.0m, Math.Max(0.0m, (decimal)delta / range));
    }
}