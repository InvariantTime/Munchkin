namespace Munchkin.Notification;

public static partial class NotifierExtensions
{
    public static ISourceNotifier<TSource, TValue> WithSource<TSource, TValue>(this INotifier<TValue> notifier, TSource source)
    {
        return new SourceNotifierWrapper<TSource, TValue>(notifier, source);
    }

    public static ISourceNotifier<TSource, TValue> MapToSource<TOrigin, TSource, TValue>(this INotifier<TOrigin> notifier, 
        Func<TOrigin, TSource> sourceMapper, Func<TOrigin, TValue> valueMapper)
    {
        return new SourceNotifierWrapper<TOrigin, TSource, TValue>(notifier, sourceMapper, valueMapper);
    }

    public static ISourceNotifier<TSource, TOrigin> MapToSource<TOrigin, TSource>(this INotifier<TOrigin> notifier,
        Func<TOrigin, TSource> sourceMapper)
    {
        return new SourceNotifierWrapper<TOrigin, TSource, TOrigin>(notifier, sourceMapper, (o) => o);
    }
}

internal class SourceNotifierWrapper<TSource, TValue> : ISourceNotifier<TSource, TValue>
{
    private readonly SourceNotifySubject<TSource, TValue> _notifier;
    private readonly IDisposable _disposable;
    private readonly TSource _source;

    public SourceNotifierWrapper(INotifier<TValue> notifier, TSource source)
    {
        _notifier = new SourceNotifySubject<TSource, TValue>();
        _disposable = notifier.Subscribe(OnNotify);
        _source = source;
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
        _notifier.OnNotify(_source, value);
    }
}

internal class SourceNotifierWrapper<TOrigin, TSource, TValue> : ISourceNotifier<TSource, TValue>
{
    private readonly SourceNotifySubject<TSource, TValue> _notifier;
    private readonly Func<TOrigin, TSource> _sourceMapper;
    private readonly Func<TOrigin, TValue> _valueMapper;
    private readonly IDisposable _disposable;

    public SourceNotifierWrapper(INotifier<TOrigin> notifier, Func<TOrigin, TSource> source, Func<TOrigin, TValue> value)
    {
        _notifier = new SourceNotifySubject<TSource, TValue>();
        _sourceMapper = source;
        _valueMapper = value;
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

    private void OnNotify(TOrigin origin)
    {
        var source = _sourceMapper.Invoke(origin);
        var value = _valueMapper.Invoke(origin);

        _notifier.OnNotify(source, value);
    }
}