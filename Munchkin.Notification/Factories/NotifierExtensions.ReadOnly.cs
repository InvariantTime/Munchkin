using Munchkin.Notification.Factories;
using Munchkin.Utils;

namespace Munchkin.Notification;

public static partial class NotifierExtensions
{
    public static INotifier<T> AsReadOnly<T>(this INotifier<T> notifier)
    {
        return new NotifyBridgeWrapper<T>(notifier, Disposable.Empty);
    }
}
