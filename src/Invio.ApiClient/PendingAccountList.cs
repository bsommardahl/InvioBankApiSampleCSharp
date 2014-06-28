using System.Collections.Generic;

namespace Invio.ApiClient
{
    public class PendingAccountList
    {
        public IEnumerable<ApprovalRequest> ApprovalRequests { get; set; }
    }
}