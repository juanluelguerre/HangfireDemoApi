using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Hangfire;
using Hangfire.Server;
using HangfireDemoApi.Data;
using HangfireDemoApi.Models;
using HangfireDemoApi.Services;
using HangfireDemoApi.UnitTests.Utils.SpecimenBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace HangfireDemoApi.UnitTests.Utils;

[ExcludeFromCodeCoverage]
public class DemoFixture
{
    public ServiceCollection Services { get; private set; }
    public ServiceProvider ServiceProvider { get; private set; }
    public PerformContext HangfirePerformContext { get; private set; }

    public DemoFixture()
    {
        this.Services = [];

        var dbContextOptions = new DbContextOptionsBuilder<DemoDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDemoDb")
            .Options;
        var dbContext = Substitute.For<DemoDbContext>(dbContextOptions);
        this.Services.AddScoped(typeof(DemoDbContext), _ => dbContext);

        //this.Services.AddDbContext<DemoDbContext>(options =>
        //{
        //    options.UseInMemoryDatabase("DemoDb");
        //});

        this.Services.AddScoped<ISchedulerService, SchedulerService>();

        this.ServiceProvider = this.Services.BuildServiceProvider();

        this.HangfirePerformContext = GetHangfirePerformContextMock();

        // Use Hangfire storage      
        GlobalConfiguration.Configuration.UseInMemoryStorage();
    }

    private static PerformContext GetHangfirePerformContextMock()
    {
        var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
        fixture.Customizations.Add(new HangfireBuilder());

        var performContext = fixture.Create<PerformContext>();


        var mockedPerformContext = Substitute.ForPartsOf<PerformContext>(performContext);


        return mockedPerformContext;
    }
}
