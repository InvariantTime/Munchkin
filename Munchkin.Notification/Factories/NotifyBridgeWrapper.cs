
namespace Munchkin.Notification.Factories;

internal class NotifyBridgeWrapper<T> : INotifier<T>
{
    private readonly INotifier<T> _notifier;
    private readonly IDisposable _disposable;

    public NotifyBridgeWrapper(INotifier<T> notifier, IDisposable disposable)
    {
        _notifier = notifier;
        _disposable = disposable;
    }

    public IDisposable Subscribe(INotifyListener<T> listener)
    {
        return _notifier.Subscribe(listener);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
