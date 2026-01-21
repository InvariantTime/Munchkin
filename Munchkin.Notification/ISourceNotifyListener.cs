namespace Munchkin.Notification;

public interface ISourceNotifyListener<TSource, in TValue>
{
    void OnNotify(TSource source, TValue value);
}
