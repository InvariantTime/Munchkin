namespace Munchkin.Utils;

public static class Disposable
{
    public static readonly IDisposable Empty = new EmptyDisposable();


    public static DisposableBuilder CreateBuilder(int count = 0)
    {
        return new DisposableBuilder(count);
    }

    private class EmptyDisposable : IDisposable
    {
        public void Dispose()
        {
        }
    }
}
