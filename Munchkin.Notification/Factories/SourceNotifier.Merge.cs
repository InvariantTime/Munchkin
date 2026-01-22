using Munchkin.Notification.Factories;
using Munchkin.Utils;

namespace Munchkin.Notification;

public static partial class SourceNotifier
{
    public static ISourceNotifier<TSource, TValue> Merge<TSource, TValue>(IEnumerable<ISourceNotifier<TSource, TValue>> notifiers)
    {
        var subject = new SourceNotifySubject<TSource, TValue>();
        var listener = new MergedNotifyListener<TSource, TValue>(subject);

        var disposeBuilder = Disposable.CreateBuilder();

        foreach (var notifier in notifiers)
        {
            var subscribe = notifier.Subscribe(listener);
            disposeBuilder.Add(subscribe);
        }

        disposeBuilder.Add(subject);

        var disposable = disposeBuilder.Build();

        return new SourceNotifyBridgeWrapper<TSource, TValue>(subject, disposable);
    }

    public static ISourceNotifier<TSource, TValue> Merge<TSource, TValue>(params ISourceNotifier<TSource, TValue>[] notifiers)
    {
        return Merge(notifiers.AsEnumerable());
    }
}

internal sealed class MergedNotifyListener<TSource, TValue> : ISourceNotifyListener<TSource, TValue>
{
    private readonly SourceNotifySubject<TSource, TValue> _notifier;

    public MergedNotifyListener(SourceNotifySubject<TSource, TValue> notifier)
    {
        _notifier = notifier;
    }

    public void OnNotify(TSource source, TValue value)
    {
        _notifier.OnNotify(source, value);
    }
}