using System;
using RestSharp;

namespace Invio.ApiClient
{
    public class InvioApiClient : IInvioApiClient
    {
        readonly Guid _bankToken;
        readonly IRestClient _client;

        public InvioApiClient(string bankId, Guid bankToken)
        {
            _bankToken = bankToken;
            _client =
                new RestClient("http://inviomobileapi.apphb.com/bank/" + bankId)
                    {Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_bankToken.ToString())};
        }

        public IPaymentActions Payments
        {
            get { return new PaymentActions(_client); }
        }

        public IAccountRequestActions AccountRequests
        {
            get { return new AccountRequestActions(_client); }
        }

        public IRemittanceActions Remittances
        {
            get { return new RemittanceActions(_client); }
        }        
    }
}