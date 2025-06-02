namespace NonDeterminismInTests.Logic.Naive;

public class CoolingSystem
{
    private readonly int _minTemp;
    private readonly int _maxTemp;

    public CoolingSystem(int minTemp, int maxTemp)
    {
        _minTemp = minTemp;
        _maxTemp = maxTemp;
    }

    public bool ShouldActivateCooling(int currentTemperature)
    {
        return currentTemperature >= _maxTemp;
    }

    public bool ShouldDeactivateCooling(int currentTemperature)
    {
        return currentTemperature <= _minTemp;
    }
}
