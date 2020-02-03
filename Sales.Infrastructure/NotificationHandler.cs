using Sales.Domain;
using System.Collections.Concurrent;

namespace Sales.Data
{
    public class NotificationHandler : INotificationHandler
    {
        public static NotificationHandler Instance => new NotificationHandler();
        private NotificationHandler() => Items = new ConcurrentBag<INotification>();

        public INotificationHandler Add(INotification notification)
        {
            Items.Add(notification);
            return this;
        }

        public bool HasMessages => Items.Count > 0;
        public ConcurrentBag<INotification> Items { get; }
    }
}

