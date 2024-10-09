using Hangfire;
using Hangfire.Storage;
using HangfireDemoApi.Data;
using HangfireDemoApi.Models;

namespace HangfireDemoApi.Services;

public class SchedulerService(DemoDbContext context) : ISchedulerService
{
    public void Enqueue(Guid jobId, string name)
    {
        if (jobId == Guid.Empty)
        {
            throw new ArgumentNullException($"Invalid jobId {jobId}");
        }

        var job = new Job { Id = jobId, Name = name };
        context.Set<Job>().Add(job);

        BackgroundJob.Enqueue(() => Console.WriteLine($"Enqueue: {name}"));

        context.SaveChanges();
    }

    public void Schedule(Guid jobId, string name, TimeSpan delay)
    {
        if (delay < TimeSpan.Zero)
        {
            throw new ArgumentException("Delay cannot be negative.");
        }

        var hangfireId =
            BackgroundJob.Schedule(() => Console.WriteLine($"Schedule: {name}"), delay);

        var job = new Job
        {
            Id = jobId,
            Name = name,
            CronExpression = GetCronExpressionFromDelay(delay),
            InternalId = hangfireId
        };

        context.Set<Job>().Add(job);
        context.SaveChanges();
    }

    public void AddOrUpdateRecurringJob(Guid jobId, string name, string cronExpression)
    {
        var job = context.Set<Job>().Find(jobId);
        if (job == null)
        {
            job = new Job { Id = jobId, Name = name, CronExpression = cronExpression };
            context.Set<Job>().Add(job);
        }
        else
        {
            job.Name = name;
            job.CronExpression = cronExpression;
            context.Set<Job>().Update(job);
        }

        RecurringJob.AddOrUpdate(jobId.ToString(),
            () => Console.WriteLine($"Recurring Job: {name}"),
            cronExpression);

        context.SaveChanges();
    }

    public void RemoveRecurringJob(Guid jobId)
    {
        var recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs()
            .Where(job => job.Id.Contains($"- {jobId}")).ToList();

        foreach (var job in recurringJobs)
        {
            RecurringJob.RemoveIfExists(job.Id);
        }
    }

    public void RemoveJob(Guid jobId)
    {
        var job = context.Set<Job>().FirstOrDefault(j => j.Id == jobId);
        if (job == null)
        {
            throw new InvalidOperationException($"Job with ID {jobId} not found.");
        }

        context.Set<Job>().Remove(job);

        var deleted = BackgroundJob.Delete(job.InternalId);
        if (!deleted)
        {
            throw new InvalidOperationException($"Job with ID {jobId} not found.");
        }

        context.SaveChanges();
    }

    private static string GetCronExpressionFromDelay(TimeSpan delay)
    {
        var targetTime = DateTime.Now.Add(delay);
        return
            $"{targetTime.Minute} {targetTime.Hour} {targetTime.Day} {targetTime.Month} ? {targetTime.Year}";
    }
}
