namespace Munchkin.Utils.Observable;

public interface ISourceNotifier<TSource, out TValue>
{
    IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener);
}
