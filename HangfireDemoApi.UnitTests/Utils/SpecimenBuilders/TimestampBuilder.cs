using AutoFixture.Kernel;

namespace HangfireDemoApi.UnitTests.Utils.SpecimenBuilders;

public static class TimestampBuilder
{
    public static ISpecimenBuilder OverloadTimestampInBuilder()
    {
        return new FilteringSpecimenBuilder(
            new FixedBuilder(GetDate(DateTime.Now.AddDays(1))),
            new ParameterSpecification(
                typeof(DateTime),
                "timestamp"));
    }

    private static DateTime GetDate(DateTime dt)
    {
        return new DateTime(
            dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second,
            dt.Millisecond);
    }
}
