
using Munchkin.Utils;

namespace Munchkin.Notification;

public class SourceNotifySubject<TSource, TValue> : ISourceNotifier<TSource, TValue>, ISourceNotifyListener<TSource, TValue>
{
    private bool _isDisposed;
    private Node? _root;

    public SourceNotifySubject()
    {
        _isDisposed = false;
    }

    public void OnNotify(TSource source, TValue value)
    {
        if (_isDisposed == true)
            return;

        var current = _root;

        while (current != null)
        {
            current.Listener.OnNotify(source, value);
            current = current.Next;
        }
    }

    public IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener)
    {
        if (_isDisposed == true)
            return Disposable.Empty;

        var node = new Node(listener, this);
        return node;
    }

    public void Dispose()
    {
        _isDisposed = true;
        _root = null;
    }

    private class Node : IDisposable
    {
        private readonly SourceNotifySubject<TSource, TValue> _parent;

        public Node? Previous { get; set; }

        public Node? Next { get; set; }

        public ISourceNotifyListener<TSource, TValue> Listener { get; }

        public Node(ISourceNotifyListener<TSource, TValue> listener, SourceNotifySubject<TSource, TValue> parent)
        {
            Listener = listener;
            _parent = parent;

            lock (_parent)
            {
                if (_parent._root == null)
                {
                    _parent._root = this;
                }
                else
                {
                    var last = _parent._root.Previous ?? _parent._root;
                    last.Next = this;
                    Previous = last;
                    _parent._root.Previous = this;
                }
            }
        }

        public void Dispose()
        {
            lock (_parent)
            {
                if (_parent._root == this)
                {
                    _parent._root = Next;
                    Next?.Previous = Previous;
                }
                else
                {
                    var root = _parent._root!;
                    var prev = Previous!;

                    if (root.Previous == this)
                    {
                        root.Previous = prev;
                    }

                    prev.Next = Next;
                    Next?.Previous = prev;
                }
            }
        }
    }
}
