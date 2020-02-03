namespace Sales.Data
{
    public class Error : Notification
    {
        public static Error Message(string message) => new Error(message);
        private Error(string notification) : base(notification) { }
    }
}
