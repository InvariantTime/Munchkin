namespace Munchkin.Notification;

public static partial class NotifierExtensions
{
    public static IDisposable Subscribe<T>(this INotifier<T> notifier, Action<T> listener)
    {
        return notifier.Subscribe(new NotifyLambdaListener<T>(listener));
    }
}

internal class NotifyLambdaListener<T> : INotifyListener<T>
{
    private readonly Action<T> _lambda;

    public NotifyLambdaListener(Action<T> lambda)
    {
        ArgumentNullException.ThrowIfNull(lambda);
        _lambda = lambda;
    }

    public void OnNotify(T value)
    {
        _lambda.Invoke(value);
    }
}