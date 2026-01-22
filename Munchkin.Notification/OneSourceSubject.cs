
namespace Munchkin.Notification;

public class OneSourceSubject<TSource, TValue> : ISourceNotifier<TSource, TValue>, INotifyListener<TValue>
{
    private readonly SourceNotifySubject<TSource, TValue> _notifier;
    private readonly TSource _source;

    public OneSourceSubject(TSource source)
    {
        _notifier = new SourceNotifySubject<TSource, TValue>();
        _source = source;
    }

    public void OnNotify(TValue value)
    {
        _notifier.OnNotify(_source, value);
    }

    public IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener)
    {
        return _notifier.Subscribe(listener);
    }

    public void Dispose()
    {
        _notifier.Dispose();
    }
}
