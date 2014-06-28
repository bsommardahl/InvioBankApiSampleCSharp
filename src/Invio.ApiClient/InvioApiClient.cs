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

        public PaymentActions Payments
        {
            get { return new PaymentActions(_client); }
        }

        public AccountRequestActions AccountRequests
        {
            get { return new AccountRequestActions(_client); }
        }

        public RemittanceActions Remittances
        {
            get { return new RemittanceActions(_client); }
        }

        #region Nested type: AccountRequestActions

        public class AccountRequestActions
        {
            readonly IRestClient _client;

            public AccountRequestActions(IRestClient client)
            {
                _client = client;
            }

            public IEnumerable<ApprovalRequest> Get()
            {
                var restRequest = new RestRequest("/bankAccountApprovalRequests/pending");
                return _client.Get<PendingAccountList>(restRequest).Data.ApprovalRequests;
            }

            public void Approve(Guid approvalRequestId, string reference)
            {
                var restRequest = new RestRequest("/bankAccountApprovalRequests/" + approvalRequestId + "/approve");
                restRequest.AddParameter("reference", reference);
                _client.Post(restRequest);
            }

            public void Deny(Guid approvalRequestId, string reason, string reference)
            {
                var restRequest = new RestRequest("/bankAccountApprovalRequests/" + approvalRequestId + "/deny");
                restRequest.AddParameter("reference", reference);
                restRequest.AddParameter("reason", reason);
                _client.Post(restRequest);
            }
        }

        #endregion

        #region Nested type: PaymentActions

        public class PaymentActions
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

        #endregion

        #region Nested type: RemittanceActions

        public class RemittanceActions
        {
            readonly IRestClient _client;

            public RemittanceActions(IRestClient client)
            {
                _client = client;
            }

            public IEnumerable<PendingRemittance> GetRemittances()
            {
                var restRequest = new RestRequest("/remittances/pending");
                return _client.Get<RemittanceList>(restRequest).Data.Remittances;
            }

            public void ProcessRemittance(Guid remittanceId, DateTime dateProcessed, decimal newBalance,
                                          string reference)
            {
                var restRequest = new RestRequest("/remittances/success");
                restRequest.AddParameter("remittanceId", remittanceId);
                restRequest.AddParameter("dateProcessed", dateProcessed);
                restRequest.AddParameter("accountBalance", newBalance);
                restRequest.AddParameter("reference", reference);
                _client.Post(restRequest);
            }

            public void FailRemittance(Guid remittanceId, DateTime dateOfFailure, string reason, string reference)
            {
                var restRequest = new RestRequest("/remittances/failure");
                restRequest.AddParameter("remittanceId", remittanceId);
                restRequest.AddParameter("DateOfFailure", dateOfFailure);
                restRequest.AddParameter("reason", reason);
                restRequest.AddParameter("reference", reference);
                _client.Post(restRequest);
            }
        }

        #endregion
    }
}