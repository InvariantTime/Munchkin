


CancellationTokenSource cts = new();


var eventDispatcher = new EventDispatcher();

while (cts.IsCancellationRequested == false)
{
    var events = eventDispatcher.GetEvents();
}


interface IEvent
{
    string Name { get; }
}

class EventDispatcher
{
    private readonly Queue<IEvent> _events = new();


    public void AddEvent(IEvent @event)
    {
        _events.Enqueue(@event);
    }

    public IEnumerable<IEvent> GetEvents()
    {
        var events = _events.ToArray();
        _events.Clear();

        return events;
    }
}