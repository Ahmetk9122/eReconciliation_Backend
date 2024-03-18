using eReconciliation.Core.Entities;

namespace eReconciliation.Entities
{
    public class UserForRegister : IEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordRegister { get; set; }
    }
}