using System;
using AgriMarketAPI.Interfaces;

namespace AgriMarketAPI.Notifications
{
    public class EmailNotifier : INotifiable
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"[EMAIL SENT TO {recipient}]: {message}");
        }
    }

    public class SmsNotifier : INotifiable
    {
        public void SendNotification(string recipient, string message)
        {
            Console.WriteLine($"[SMS SENT TO {recipient}]: {message}");
        }
    }
}