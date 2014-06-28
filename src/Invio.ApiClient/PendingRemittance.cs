using System;

namespace Invio.ApiClient
{
    public abstract class PendingRemittance
    {
        public decimal Amount { get; set; }

        public DateTime DateSubmitted { get; set; }

        public string RemittanceAgencyCode { get; set; }

        public Guid Id { get; set; }
    }
}