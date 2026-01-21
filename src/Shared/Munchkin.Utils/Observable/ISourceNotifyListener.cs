namespace Munchkin.Utils.Observable;

public interface ISourceNotifyListener<TSource, in TValue>
{
    void OnNotify(TSource source, TValue value);
}
