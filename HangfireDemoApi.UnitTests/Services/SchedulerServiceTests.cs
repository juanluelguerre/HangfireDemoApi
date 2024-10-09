using FluentAssertions;
using FluentAssertions.Execution;
using Hangfire;
using HangfireDemoApi.Data;
using HangfireDemoApi.Services;
using HangfireDemoApi.UnitTests.Utils;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Job = HangfireDemoApi.Models.Job;

namespace HangfireDemoApi.UnitTests.Services
{
    public class SchedulerServiceTests(DemoFixture demoFixture)
        : IClassFixture<DemoFixture>
    {
        [Theory, AutoNSubstituteData]
        public void Enqueue_ShouldEnqueueJob(Job job)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            var mockJobSet = DbSetMockFactory.CreateDbSetMock(job);
            dbContext.Set<Job>().Returns(mockJobSet);

            // Act            
            schedulerService.Enqueue(job.Id, job.Name);

            // Assert            
            dbContext.Received().SaveChanges();
        }

        [Theory, AutoNSubstituteData]
        public void Schedule_ShouldScheduleJob(Guid jobId, string message,
            TimeSpan delay)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            // Act
            schedulerService.Schedule(jobId, message, delay);

            // Assert
            dbContext.Received().SaveChanges();
        }

        [Theory, AutoNSubstituteData]
        public void AddOrUpdateRecurringJob_ShouldAddOrUpdateRecurringJob(Guid jobId,
            string message)
        {
            // Arrange            
            var cronExpression = GenerateRandomCronExpressionMock();
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            // Act
            schedulerService.AddOrUpdateRecurringJob(jobId, message, cronExpression);

            // Assert            
            dbContext.Received().SaveChanges();
        }

        [Theory, AutoNSubstituteData]
        public void RemoveJob_ShouldRemoveJob(Job job)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            var mockJobSet = DbSetMockFactory.CreateDbSetMock(job);
            dbContext.Set<Job>().Returns(mockJobSet);

            var hangfireId = BackgroundJob.Enqueue(() => Console.WriteLine());
            job.InternalId = hangfireId;

            // Act
            schedulerService.RemoveJob(job.Id);

            // Assert
            using (new AssertionScope())
            {
                dbContext.Received().SaveChanges();
            }
        }

        [Fact]
        public void Enqueue_ShouldThrowException_WhenJobIsNull()
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();

            // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var act = () => schedulerService.Enqueue(Guid.Empty, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void Schedule_ShouldThrowException_WhenDelayIsNegative(Guid jobId, string message)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();

            // Act
            var act = () => schedulerService.Schedule(jobId, message, TimeSpan.FromMinutes(-1));

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void AddOrUpdateRecurringJob_ShouldThrowException_WhenCronExpressionIsInvalid(
            Guid jobId, string message)
        {
            // Arrange
            const string invalidCronExpression = "invalid cron";
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();

            // Act
            var act = () =>
                schedulerService.AddOrUpdateRecurringJob(jobId, message, invalidCronExpression);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage($"CRON expression is invalid. *");
        }

        [Theory, AutoNSubstituteData]
        public void RemoveRecurringJob_ShouldNotThrow_WhenJobDoesNotExist(Guid jobId)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();

            // Act
            var act = () => schedulerService.RemoveRecurringJob(jobId);

            // Assert
            act.Should().NotThrow();
        }

        [Theory, AutoNSubstituteData]
        public void RemoveJob_ShouldThrowException_WhenJobDoesNotExist(Guid jobId)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            var mockJobSet = DbSetMockFactory.CreateDbSetMock<Job>();
            dbContext.Set<Job>().Returns(mockJobSet);

            // Act
            var act = () => schedulerService.RemoveJob(jobId);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage($"Job with ID {jobId} not found.");
        }

        [Theory, AutoNSubstituteData]
        public void RemoveJob_ShouldThrowException_WhenBackgroundJobDeleteFails(Job job)
        {
            // Arrange
            var schedulerService =
                demoFixture.ServiceProvider.GetRequiredService<ISchedulerService>();
            var dbContext = demoFixture.ServiceProvider.GetRequiredService<DemoDbContext>();

            var mockJobSet = DbSetMockFactory.CreateDbSetMock(job);
            dbContext.Set<Job>().Returns(mockJobSet);

            job.InternalId = "invalid-id";

            // Act
            var act = () => schedulerService.RemoveJob(job.Id);

            // Assert
            using (new AssertionScope())
            {
                act.Should().Throw<InvalidOperationException>()
                    .WithMessage($"Job with ID {job.Id} not found.");
                dbContext.Received().SaveChanges();
            }
        }

        private static string GenerateRandomCronExpressionMock()
        {
            var random = new Random();
            var minute = random.Next(0, 60);
            var hour = random.Next(0, 24);
            var dayOfMonth = random.Next(1, 32);
            var month = random.Next(1, 13);
            var dayOfWeek = random.Next(0, 7);

            return $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}";
        }
    }
}
