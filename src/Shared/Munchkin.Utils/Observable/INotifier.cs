namespace Munchkin.Utils.Observable;

public interface INotifier<out T>
{
    IDisposable Subscribe(INotifyListener<T> listener);
}
