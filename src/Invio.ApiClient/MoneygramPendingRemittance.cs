namespace Invio.ApiClient
{
    public class MoneygramPendingRemittance : PendingRemittance
    {
        public string Pin { get; set; }

        public string Password { get; set; }
    }
}