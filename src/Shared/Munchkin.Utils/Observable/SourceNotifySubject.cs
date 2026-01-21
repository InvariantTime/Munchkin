
namespace Munchkin.Utils.Observable;

public class SourceNotifySubject<TSource, TValue> : ISourceNotifier<TSource, TValue>, INotifyListener<TValue>
{
    private readonly TSource _source;

    private Node? _root;

    public SourceNotifySubject(TSource source)
    {
        _source = source;
    }

    public void OnNotify(TValue value)
    {
        var current = _root;

        while (current != null)
        {
            current.Listener.OnNotify(_source, value);
        }
    }

    public IDisposable Subscribe(ISourceNotifyListener<TSource, TValue> listener)
    {
        var node = new Node(listener, this);
        return node;
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
