namespace Munchkin.Utils.Observable;

public class NotifySubject<T> : INotifier<T>, INotifyListener<T>
{
    private Node? _root;

    public IDisposable Subscribe(INotifyListener<T> listener)
    {
        var node = new Node(listener, this);
        return node;
    }

    public void OnNotify(T value)
    {
        var current = _root;

        while (current != null)
        {
            current.Listener.OnNotify(value);
            current = current.Next;
        }
    }

    private class Node : IDisposable
    {
        private readonly NotifySubject<T> _parent;

        public Node? Previous { get; set; }

        public Node? Next { get; set; }

        public INotifyListener<T> Listener { get; }

        public Node(INotifyListener<T> listener, NotifySubject<T> parent)
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
