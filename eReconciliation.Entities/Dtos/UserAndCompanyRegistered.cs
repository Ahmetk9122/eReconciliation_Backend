using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Entities.Dtos
{
    public class UserAndCompanyRegistered
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public Company Company { get; set; }
    }
}