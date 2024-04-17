using eReconciliation.Core.Entities;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Entities.Dtos
{
    public class UserAndCompanyRegisteredDto : IDto
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public Company Company { get; set; }
    }
}