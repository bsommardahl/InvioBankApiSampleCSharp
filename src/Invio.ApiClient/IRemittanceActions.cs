using System;
using System.Collections.Generic;

namespace Invio.ApiClient
{
    public interface IRemittanceActions
    {
        IEnumerable<PendingRemittance> GetRemittances();

        void ProcessRemittance(Guid remittanceId, DateTime dateProcessed, decimal newBalance,
                               string reference);

        void FailRemittance(Guid remittanceId, DateTime dateOfFailure, string reason, string reference);
    }
}