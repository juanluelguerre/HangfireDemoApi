using AutoFixture.Kernel;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.Storage;
using NSubstitute;

namespace HangfireDemoApi.UnitTests.Utils.SpecimenBuilders;

public class HangfireBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not Type type) return new NoSpecimen();

        if (type == typeof(PerformContext))
        {
            var methodInfo = typeof(HangfireBuilder).GetMethod("SomeFakeMethod");
            var backgroundJob = new BackgroundJob("job1", new Job(methodInfo), DateTime.Now,
                new Dictionary<string, string>()
                {
                    { "RetryCount", "1" }
                });

            var storage = new Hangfire.InMemory.InMemoryStorage();
            var connection = Substitute.For<IStorageConnection>();
            var monitor = Substitute.For<IJobCancellationToken>();

            return new PerformContext(storage, connection, backgroundJob, monitor);
        }

        if (type == typeof(BackgroundJob))
        {
            var methodInfo = typeof(HangfireBuilder).GetMethod("SomeFakeMethod");
            return new BackgroundJob("job1", new Job(methodInfo), DateTime.Now);
        }

        if (type == typeof(JobStorage))
        {
            return new Hangfire.InMemory.InMemoryStorage();
        }

        if (type == typeof(IStorageConnection))
        {
            return Substitute.For<IStorageConnection>();
        }

        if (type == typeof(IJobCancellationToken))
        {
            return Substitute.For<IJobCancellationToken>();
        }

        if (type == typeof(Job))
        {
            return Substitute.For<Job>();
        }

        return new NoSpecimen();
    }

#pragma warning disable CA1822
    public void SomeFakeMethod()
#pragma warning restore CA1822
    {
    }
}
