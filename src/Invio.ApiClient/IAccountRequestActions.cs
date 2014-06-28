using System;
using System.Collections.Generic;

namespace Invio.ApiClient
{
    public interface IAccountRequestActions
    {
        IEnumerable<ApprovalRequest> Get();
        void Approve(Guid approvalRequestId, string reference);
        void Deny(Guid approvalRequestId, string reason, string reference);
    }
}