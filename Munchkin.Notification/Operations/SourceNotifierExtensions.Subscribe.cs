namespace Munchkin.Notification;

public static partial class SourceNotifierExtensions
{
    public static IDisposable Subscribe<TSource, TValue>(this ISourceNotifier<TSource, TValue> notifier, INotifyListener<TValue> listener)
    {
        return notifier.Subscribe(new ListenerWrapper<TSource, TValue>(listener));
    }

    public static IDisposable Subscribe<TSource, TValue>(this ISourceNotifier<TSource, TValue> notifier, Action<TSource, TValue> listener)
    {
        return notifier.Subscribe(new SourceNotifyLambdaListener<TSource, TValue>(listener));
    }

    private class ListenerWrapper<TSource, TValue> : ISourceNotifyListener<TSource, TValue>
    {
        private readonly INotifyListener<TValue> _listener;

        public ListenerWrapper(INotifyListener<TValue> listener)
        {
            _listener = listener;
        }

        public void OnNotify(TSource source, TValue value)
        {
            _listener.OnNotify(value);
        }
    }
}

internal class SourceNotifyLambdaListener<TSource, TValue> : ISourceNotifyListener<TSource, TValue>
{
    private readonly Action<TSource, TValue> _lambda;

    public SourceNotifyLambdaListener(Action<TSource, TValue> lambda)
    {
        ArgumentNullException.ThrowIfNull(lambda);
        _lambda = lambda;
    }

    public void OnNotify(TSource source, TValue value)
    {
        _lambda.Invoke(source, value);
    }
}