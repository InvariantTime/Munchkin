namespace Munchkin.Notification;

public static partial class SourceNotifierExtensions
{
    public static ISourceNotifier<TSource, TValue> Merge<TSource, TValue>(this IEnumerable<ISourceNotifier<TSource, TValue>> notifiers)
    {
        return SourceNotifier.Merge(notifiers);
    }
}
