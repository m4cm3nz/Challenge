using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain
{
    public interface INotificationHandler
    {
        INotificationHandler Add(INotification notification);
        bool HasMessages { get; }
        ConcurrentBag<INotification> Items { get; } 
    }
}
