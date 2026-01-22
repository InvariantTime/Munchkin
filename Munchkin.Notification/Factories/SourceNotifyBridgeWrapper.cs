
namespace Munchkin.Notification;

internal class SourceNotifyBridgeWrapper<TSource, TValue> : ISourceNotifier<TSource, TValue>
{
    private readonly IDisposable _disposable;
    private readonly ISourceNotifier<TSource, TValue> _notifier;

    public SourceNotifyBridgeWrapper(ISourceNotifier<TSource, TValue> notifier, IDisposable disposable)
    {
        _notifier = notifier;
        _disposable = disposable;
    }

    public IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener)
    {
        return _notifier.Subscribe(listener);
    }

    public void Dispose()
    {
        _disposable.Dispose();
    }
}
