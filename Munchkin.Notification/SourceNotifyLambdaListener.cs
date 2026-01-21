namespace Munchkin.Notification;

public class SourceNotifyLambdaListener<TSource, TValue> : ISourceNotifyListener<TSource, TValue>
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
