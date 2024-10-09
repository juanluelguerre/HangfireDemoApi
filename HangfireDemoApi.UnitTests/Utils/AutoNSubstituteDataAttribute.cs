using AutoFixture.Xunit2;

namespace HangfireDemoApi.UnitTests.Utils;

public class AutoNSubstituteDataAttribute() : AutoDataAttribute(() =>
    new AutoFixture.Fixture().Customize(new AutoFixtureCustomizations()));
