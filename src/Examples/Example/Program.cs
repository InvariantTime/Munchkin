using System.Threading.Channels;

var source = new PeriodicEventSource(1);

var cts = new CancellationTokenSource();
EventDispatcher dispatcher = new EventDispatcher(source);

while (cts.IsCancellationRequested == false)
{
    var @event = await dispatcher.WaitForEventAsync(cts.Token);
    Console.WriteLine($"Handled event {@event} at {@event.Time}");
}


interface IEvent
{
    DateTime Time { get; }
}

class SimpleEvent : IEvent
{
    public DateTime Time { get; }

    public SimpleEvent()
    {
        Time = DateTime.Now;
    }
}

class EventDispatcher
{
    private readonly Channel<IEvent> _eventBuffer;
    private readonly IEventSource _source;

    public EventDispatcher(IEventSource source)
    {
        _source = source;
        _eventBuffer = Channel.CreateUnbounded<IEvent>();
    }

    public async Task<IEvent> WaitForEventAsync(CancellationToken cancellation)
    {
        var @event = await _source.GetEventAsync(cancellation);
        return @event;
    }
}

interface IEventSource
{
    ValueTask<IEvent> GetEventAsync(CancellationToken cancellation);
}

class PeriodicEventSource : IEventSource
{
    private readonly Channel<IEvent> _channel;
    private readonly PeriodicTimer _timer;

    public PeriodicEventSource(int seconds)
    {
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(seconds));
        _channel = Channel.CreateUnbounded<IEvent>();

        Task.Run(async () =>
        {
            while (true)//TODO: cancellation
            {
                await _timer.WaitForNextTickAsync();
                await _channel.Writer.WriteAsync(new SimpleEvent());
            }
        });
    }

    public ValueTask<IEvent> GetEventAsync(CancellationToken cancellation)
    {
        return _channel.Reader.ReadAsync(cancellation);
    }
}