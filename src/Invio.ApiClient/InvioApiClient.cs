using System;
using System.Collections.Generic;
using RestSharp;

namespace Invio.ApiClient
{
    public class InvioApiClient
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

        public IEnumerable<PendingPayment> GetPayments(DateTime start, DateTime end)
        {
            var restRequest = new RestRequest("/payments/unpaid");
            restRequest.AddParameter("start", start);
            restRequest.AddParameter("end", end);
            return _client.Get<PaymentList>(restRequest).Data.Payments;
        }

        public void ProcessPayment(Guid paymentOrderId, DateTime paymentDate, decimal newBalance, string reference)
        {
            var restRequest = new RestRequest("/payments/success");
            restRequest.AddParameter("paymentOrderId", paymentOrderId);
            restRequest.AddParameter("paymentDate", paymentDate);
            restRequest.AddParameter("accountBalance", newBalance);
            restRequest.AddParameter("reference", reference);
            _client.Post(restRequest);
        }

        public void FailPayment(Guid paymentOrderId, DateTime dateOfFailure, string reason, string reference)
        {
            var restRequest = new RestRequest("/payments/failure");
            restRequest.AddParameter("paymentOrderId", paymentOrderId);
            restRequest.AddParameter("dateOfFailure", dateOfFailure);
            restRequest.AddParameter("reason", reason);
            restRequest.AddParameter("reference", reference);
            _client.Post(restRequest);
        }
    }
}