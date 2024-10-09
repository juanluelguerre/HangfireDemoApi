using Hangfire;
using Hangfire.MemoryStorage;
using HangfireDemoApi.Data;
using HangfireDemoApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

builder.Services.AddDbContext<DemoDbContext>(options => { options.UseInMemoryDatabase("DemoDb"); });

var app = builder.Build();

app.UseHangfireDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("enqueue",
    (ISchedulerService schedulerService) =>
        schedulerService.Enqueue(Guid.NewGuid(), "Immediate Job"));

app.MapPost("schedule",
    (ISchedulerService schedulerService) =>
        schedulerService.Schedule(Guid.NewGuid(), "Delay Job", TimeSpan.FromMinutes(5)));

app.Run();
