namespace Invio.ApiClient
{
    public interface IInvioApiClient
    {
        IPaymentActions Payments { get; }
        IAccountRequestActions AccountRequests { get; }
        IRemittanceActions Remittances { get; }
    }
}