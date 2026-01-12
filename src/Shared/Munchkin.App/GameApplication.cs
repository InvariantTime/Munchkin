namespace Munchkin.App;

public class GameApplication
{
    private CancellationTokenSource? _cts;
    private Task? _executionTask;

    public bool IsRunning => _executionTask != null && _executionTask.Status == TaskStatus.Running;

    public Task RunAsync(CancellationToken cancellation = default)
    {
        if (IsRunning == true)
            throw new InvalidOperationException("Application is already runned");

        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellation);

        var execution = Task.Run(() => ExecuteAsync(_cts.Token), _cts.Token);

        return Task.CompletedTask;
    }

    public async Task CancelAsync(CancellationToken cancellation = default)
    {
        if (_executionTask == null || _executionTask.Status == TaskStatus.Running)
            return;

        try
        {
            _cts!.Cancel();
        }
        finally
        {
            await _executionTask.WaitAsync(cancellation)
                .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);

            _cts?.Dispose();
        }
    }

    private async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (cancellation.IsCancellationRequested == false)
        {
             
        }
    }
}
