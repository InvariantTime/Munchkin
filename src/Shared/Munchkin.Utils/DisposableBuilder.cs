using System.Buffers;

namespace Munchkin.Utils;

public ref struct DisposableBuilder
{
    private const int _newArrayLengthMultiplier = 2;

    private object? _disposable;
    private int _count;

    public DisposableBuilder()
    {
        _count = 0;
    }

    public DisposableBuilder(int count)
    {
        if (count > 0)
            _disposable = new IDisposable[count];
    }

    public void Add(IDisposable disposable)
    {
        if (_disposable == null)
        {
            _disposable = disposable;
            _count = 1;
        }
        else if (_disposable is IDisposable other)
        {
            var array = ArrayPool<IDisposable>.Shared.Rent(2);
            array[0] = other;
            array[1] = disposable;
            _disposable = array;
            _count = 2;
        }
        else if (_disposable is IDisposable[])
        {
            AddToArray(disposable);
        }
    }

    private void AddToArray(IDisposable disposable)
    {
        if (_disposable is not IDisposable[] array)
            return;

        if (_count == array.Length)
        {
            var newArray = ArrayPool<IDisposable>.Shared.Rent(_count * _newArrayLengthMultiplier);
            Array.Copy(array, newArray, array.Length);
            ArrayPool<IDisposable>.Shared.Return(array, true);

            newArray[_count] = disposable;
            _disposable = newArray;
        }
        else
        {
            array[_count] = disposable;
        }

        _count++;
    }

    public IDisposable Build()
    {
        if (_disposable is IDisposable disposable)
            return disposable;

        if (_disposable is IDisposable[] array)
            return new CombineDisposable(array.AsSpan(0, _count).ToArray());

        return Disposable.Empty;
    }
}

internal sealed class CombineDisposable : IDisposable
{
    private readonly IDisposable[] _disposables;

    public CombineDisposable(IDisposable[] disposables)
    {
        _disposables = disposables;
    }

    public void Dispose()
    {
        foreach (var dis in _disposables)
            dis.Dispose();
    }
}