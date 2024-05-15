using eReconciliation.Core.Entities;

namespace eReconciliation.Entities.Dtos
{
    public class UserForRegistertoSecondAccountDto : UserForRegisterDto
    {
        public int CompanyId { get; set; }
    }
}