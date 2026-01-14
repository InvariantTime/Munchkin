using System.Threading.Channels;

var sources = new IEventSource[]
{
    new PeriodicEventSource("1 second", TimeSpan.FromSeconds(1)),
    new PeriodicEventSource("5 seconds", TimeSpan.FromSeconds(5)),
    new PeriodicEventSource("10 seconds", TimeSpan.FromSeconds(10)),
    new ClickEventSource()
};

var dispatcher = new EventDispatcher(sources);
var cts = new CancellationTokenSource();

while (cts.IsCancellationRequested == false)
{
    var @event = await dispatcher.WaitForEventAsync(cts.Token);
    await Console.Out.WriteLineAsync($"Event {@event} at {@event.Time}");
}

await dispatcher.StopAsync(CancellationToken.None);


class EventDispatcher
{
    private readonly CancellationTokenSource _cancellation;
    private readonly Task _executionTask;
    private readonly Channel<IEvent> _queue;

    public EventDispatcher(IEnumerable<IEventSource> sources)
    {
        _cancellation = new CancellationTokenSource();
        _queue = Channel.CreateUnbounded<IEvent>();

        var tasks = sources.Select(x => x.StartListeningAsync(_queue, _cancellation.Token));
        _executionTask = Task.WhenAll(tasks);
    }

    public async Task<IEvent> WaitForEventAsync(CancellationToken cancellation)
    {
        return await _queue.Reader.ReadAsync(cancellation);
    }

    public async Task StopAsync(CancellationToken cancellation)
    {

        try
        {
            await _cancellation.CancelAsync();
        }
        finally
        {
            await _executionTask.WaitAsync(cancellation)
                .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);

            _cancellation.Dispose();
        }
    }
}

interface IEventSource
{
    Task StartListeningAsync(Channel<IEvent> queue, CancellationToken cancellation);
}

interface IEvent
{
    DateTime Time { get; }
}

class SimpleEvent : IEvent
{
    public DateTime Time { get; }

    public string Message { get; }

    public SimpleEvent(string message)
    {
        Time = DateTime.Now;
        Message = message;
    }

    public override string ToString()
    {
        return Message;
    }
}

class PeriodicEventSource : IEventSource
{
    private readonly string _message;
    private readonly PeriodicTimer _timer;

    public PeriodicEventSource(string message, TimeSpan time)
    {
        _message = message;
        _timer = new PeriodicTimer(time);
    }

    public async Task StartListeningAsync(Channel<IEvent> queue, CancellationToken cancellation)
    {
        while (await _timer.WaitForNextTickAsync(cancellation) == true)
            await queue.Writer.WriteAsync(new SimpleEvent(_message), cancellation);
    }
}

class ClickEventSource : IEventSource
{
    public Task StartListeningAsync(Channel<IEvent> queue, CancellationToken cancellation)
    {
        return Task.Run(async () =>
        {
            while (cancellation.IsCancellationRequested == false)
            {
                var key = Console.ReadKey(true).KeyChar;
                await queue.Writer.WriteAsync(new SimpleEvent($"On click {key}"), cancellation);
            }
        });
    }
}