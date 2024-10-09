using AutoFixture;
using AutoFixture.AutoNSubstitute;
using HangfireDemoApi.UnitTests.Utils.SpecimenBuilders;

namespace HangfireDemoApi.UnitTests.Utils;

public class AutoFixtureCustomizations : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize(new AutoNSubstituteCustomization());
        fixture.Customizations.Add(new DateTimeBuilder());
        fixture.Customizations.Add(TimestampBuilder.OverloadTimestampInBuilder());
    }
}
