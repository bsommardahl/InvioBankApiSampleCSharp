using System;
using System.Collections.Generic;
using RestSharp;

namespace Invio.ApiClient
{
    public class AccountRequestActions : IAccountRequestActions
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
}