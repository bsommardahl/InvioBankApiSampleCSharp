using System;

namespace Invio.ApiClient
{
    public class ApprovalRequest
    {
        public AccountHolderInfo BankAccount { get; set; }
        public string Status { get; set; }
        public DateTime LastActionTime { get; set; }
        public string LastActionReference { get; set; }
        public string LastActionReason { get; set; }
        public Guid Id { get; set; }
    }
}