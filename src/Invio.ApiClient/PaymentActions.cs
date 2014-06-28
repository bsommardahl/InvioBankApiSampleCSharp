using System;
using System.Collections.Generic;
using RestSharp;

namespace Invio.ApiClient
{
    public class PaymentActions : IPaymentActions
    {
        readonly IRestClient _client;

        public PaymentActions(IRestClient client)
        {
            _client = client;
        }

        public IEnumerable<PendingPayment> Get(DateTime start, DateTime end)
        {
            var restRequest = new RestRequest("/payments/unpaid");
            restRequest.AddParameter("start", start);
            restRequest.AddParameter("end", end);
            return _client.Get<PaymentList>(restRequest).Data.Payments;
        }

        public void ProcessWithSuccess(Guid paymentOrderId, DateTime paymentDate, decimal newBalance,
                                       string reference)
        {
            var restRequest = new RestRequest("/payments/success");
            restRequest.AddParameter("paymentOrderId", paymentOrderId);
            restRequest.AddParameter("paymentDate", paymentDate);
            restRequest.AddParameter("accountBalance", newBalance);
            restRequest.AddParameter("reference", reference);
            _client.Post(restRequest);
        }

        public void ProcessWithFailure(Guid paymentOrderId, DateTime dateOfFailure, string reason, string reference)
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