using Api.v1.Services;

namespace Api.v1.BackgroundTasks.UserBgTasks;

public class UserCountingServiceHostedService : BackgroundService
{
    private IServiceProvider _services { get; }

    public UserCountingServiceHostedService(IServiceProvider services) => _services = services;

    protected override async Task ExecuteAsync(CancellationToken ctoken)
    {
        using var scope = _services.CreateScope();
        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<UserCountingService>();
        await scopedProcessingService.DoWork(ctoken);
    }

    public override async Task StopAsync(CancellationToken ctoken)
    {
        await base.StopAsync(ctoken);
    }
}
