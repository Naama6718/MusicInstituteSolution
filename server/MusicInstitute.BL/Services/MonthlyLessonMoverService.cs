using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using MusicInstitute.BL.Api;
using MusicInstitute.DAL.Api;
using Microsoft.Extensions.DependencyInjection;

public class MonthlyLessonMoverService : BackgroundService
{
    private readonly ILogger<MonthlyLessonMoverService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer? _timer;

    public MonthlyLessonMoverService(
        ILogger<MonthlyLessonMoverService> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(
            DoWork,
            null,
            TimeSpan.Zero,
            TimeSpan.FromDays(30));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        try
        {
            _logger.LogInformation("Starting monthly lesson move...");

            // יצירת scope חדש לכל ריצה
            using (var scope = _scopeFactory.CreateScope())
            {
                var bookedLessonsManager = scope.ServiceProvider.GetRequiredService<IBookedLessons_Manager_BL>();
                await bookedLessonsManager.MovePastLessonsToPassedAsync();
            }

            _logger.LogInformation("Finished moving lessons.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error moving lessons from Booked to Passed.");
        }
    }

    public override void Dispose()
    {
        _timer?.Dispose();
        base.Dispose();
    }
}