namespace AgriMarketAPI.Interfaces
{
    public interface INotifiable
    {
        void SendNotification(string recipient, string message);
    }
}