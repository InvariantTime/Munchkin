using Munchkin.Core.Events;

namespace Munchkin.App;

public class EventDispatcher
{
    public Task<IGameEvent> WaitForEventAsync(CancellationToken cancellation)
    {
        return Task.FromResult<IGameEvent>(null!);
    }
}
