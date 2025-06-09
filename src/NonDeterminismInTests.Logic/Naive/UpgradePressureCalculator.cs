namespace NonDeterminismInTests.Logic.Naive;

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

        return Math.Clamp((decimal)delta / range, 0.0m, 1.0m);
    }
}