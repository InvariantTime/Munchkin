namespace Munchkin.Notification;

public static partial class NotifierExtensions
{
    public static IDisposable Subscribe<T>(this INotifier<T> notifier, Action<T> listener)
    {
        return notifier.Subscribe(new NotifyLambdaListener<T>(listener));
    }

    public static ReadOnlyNotifier<T> AsReadOnly<T>(this INotifier<T> notifier)
    {
        return new ReadOnlyNotifier<T>(notifier);
    }

    public static ISourceNotifier<TSource, TValue> WithSource<TSource, TValue>(this INotifier<TValue> notifier, TSource source)
    {
        return new SourceNotifierWrapper<TSource, TValue>(notifier, source);
    }

    private class SourceNotifierWrapper<TSource, TValue> : ISourceNotifier<TSource, TValue>, IDisposable
    {
        private readonly SourceNotifySubject<TSource, TValue> _notifier;
        private readonly IDisposable _disposable;

        public SourceNotifierWrapper(INotifier<TValue> notifier, TSource source)
        {
            _notifier = new SourceNotifySubject<TSource, TValue>(source);
            _disposable = notifier.Subscribe(OnNotify);
        }

        public IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener)
        {
            return _notifier.Subscribe(listener);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnNotify(TValue value)
        {
            _notifier.OnNotify(value);
        }
    }
}
