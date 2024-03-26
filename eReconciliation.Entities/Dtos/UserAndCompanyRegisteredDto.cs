using eReconciliation.Entities.Concrete;

namespace eReconciliation.Entities.Dtos
{
    public class UserAndCompanyRegisteredDto
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public Company Company { get; set; }
    }
}