using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eReconciliation.Core.Aspects.Performance
{
    public class SendMailDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}