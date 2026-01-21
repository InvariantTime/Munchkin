namespace Munchkin.Notification;

public interface INotifier<out T> : IDisposable
{
    IDisposable Subscribe(INotifyListener<T> listener);
}
