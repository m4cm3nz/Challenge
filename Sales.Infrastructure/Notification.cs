using Sales.Domain;

namespace Sales.Data
{
    public abstract class Notification : INotification
    {
        readonly string notification;
        protected Notification(string notification)
        {
            this.notification = notification;
        }

        public override string ToString()
        {
            return notification;
        }
    }
}

