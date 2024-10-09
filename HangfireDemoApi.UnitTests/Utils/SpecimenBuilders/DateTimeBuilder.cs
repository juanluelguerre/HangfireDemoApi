using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace HangfireDemoApi.UnitTests.Utils.SpecimenBuilders;

public class DateTimeBuilder : ISpecimenBuilder
{
    private readonly RandomDateTimeSequenceGenerator dates = new();

    public object Create(object request, ISpecimenContext context)
    {
        switch (request)
        {
            case PropertyInfo { Name: "CreatedOn" } prop
                when prop.PropertyType == typeof(DateTime):
                return GetDate(DateTime.Now.AddDays(-1));
            case PropertyInfo { Name: "LastModifiedOn" } prop1
                when prop1.PropertyType == typeof(DateTime?):
                return GetDate(DateTime.Now);
        }

        var value = this.dates.Create(request, context);

        if (value is DateTime dt)
            return GetDate(dt);

        return new NoSpecimen();
    }

    private static DateTime GetDate(DateTime dt)
    {
        return new DateTime(
            dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second,
            dt.Millisecond);
    }
}
