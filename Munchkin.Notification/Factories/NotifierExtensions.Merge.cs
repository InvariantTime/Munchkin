namespace Munchkin.Notification;

public static partial class NotifierExtensions
{
    public static INotifier<T> MergeNotifiers<T>(this IEnumerable<INotifier<T>> notifiers)
    {
        return Notifier.Merge(notifiers);
    }
}