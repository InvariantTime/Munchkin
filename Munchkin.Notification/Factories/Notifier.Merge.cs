
using Munchkin.Notification.Factories;
using Munchkin.Utils;

namespace Munchkin.Notification;

public static partial class Notifier
{
    public static INotifier<T> Merge<T>(IEnumerable<INotifier<T>> notifiers)
    {
        var subject = new NotifySubject<T>();
        var listener = new MergedNotifyListener<T>(subject);

        var disposeBuilder = Disposable.CreateBuilder();

        foreach (var notifier in notifiers)
        {
            var subscribe = notifier.Subscribe(listener);
            disposeBuilder.Add(subscribe);
        }

        disposeBuilder.Add(subject);

        var disposable = disposeBuilder.Build();

        return new NotifyBridgeWrapper<T>(subject, disposable);
    }

    public static INotifier<T> Merge<T>(params INotifier<T>[] notifiers)
    {
        return Merge(notifiers.AsEnumerable());
    }

}

internal class MergedNotifyListener<T> : INotifyListener<T>
{
    private readonly NotifySubject<T> _notifier;

    public MergedNotifyListener(NotifySubject<T> notifier)
    {
        _notifier = notifier;
    }

    public void OnNotify(T value)
    {
        _notifier.OnNotify(value);
    }
}