using System;
using System.Collections.Generic;
using RestSharp;

namespace Invio.ApiClient
{
    public class RemittanceActions : IRemittanceActions
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
}