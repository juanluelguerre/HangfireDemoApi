namespace HangfireDemoApi.Services;

public interface ISchedulerService
{
    public void Enqueue(Guid jobId, string name);
    public void Schedule(Guid jobId, string name, TimeSpan delay);
    public void AddOrUpdateRecurringJob(Guid jobId, string name, string cronExpression);
    public void RemoveRecurringJob(Guid jobId);
    public void RemoveJob(Guid jobId);
}
