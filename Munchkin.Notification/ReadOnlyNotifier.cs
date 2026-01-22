
namespace Munchkin.Notification;

public class ReadOnlyNotifier<T> : INotifier<T>
{
    private readonly INotifier<T> _notifier;

    public ReadOnlyNotifier(INotifier<T> notifier)
    {
        _notifier = notifier;
    }

    public IDisposable Subscribe(INotifyListener<T> listener)
    {
        return _notifier.Subscribe(listener);
    }

    public void Dispose()
    {
    }
}
