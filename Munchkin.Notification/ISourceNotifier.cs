namespace Munchkin.Notification;

public interface ISourceNotifier<TSource, out TValue> : IDisposable
{
    IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener);
}
