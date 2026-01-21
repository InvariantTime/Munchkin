namespace Munchkin.Utils.Observable;

public interface INotifyListener<in T>
{
    void OnNotify(T value);
}
