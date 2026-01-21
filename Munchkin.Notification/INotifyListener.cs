namespace Munchkin.Notification;

public interface INotifyListener<in T>
{
    void OnNotify(T value);
}
