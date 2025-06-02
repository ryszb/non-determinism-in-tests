using NonDeterminismInTests.Logic.Naive;

public class CoolingSystemTests
{
    [Fact]
    public void Should_activate_cooling_when_temperature_above_max()
    {
        var system = new CoolingSystem(50, 100);
        Assert.True(system.ShouldActivateCooling(200));
    }

    [Fact]
    public void Should_deactivate_cooling_when_temperature_below_min()
    {
        var system = new CoolingSystem(50, 100);
        Assert.True(system.ShouldDeactivateCooling(10));
    }
}
