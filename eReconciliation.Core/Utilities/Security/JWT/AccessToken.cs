using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eReconciliation.Core.Utilities
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int CompanyId { get; set; }
    }
}