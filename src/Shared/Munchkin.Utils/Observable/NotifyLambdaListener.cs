namespace Munchkin.Utils.Observable;

public class NotifyLambdaListener<T> : INotifyListener<T>
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
