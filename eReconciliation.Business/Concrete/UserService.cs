using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.ValidationRules.FluentValidation;
using eReconciliation.Core.Aspects.Autofac.Validation;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.DataAccess;
using FluentValidation;

namespace eReconciliation.Business
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public void Add(User user)
        {
            _userDal.Add(user);
        }
        [CacheRemoveAspect("IUserService.Get")]
        public void Update(User user)
        {
            _userDal.Update(user);
        }
        [CacheAspect(60)]
        public User GetByMail(string email)
        {
            return _userDal.Get(x => x.Email == email);
        }
        [CacheAspect(60)]
        public User GetById(int id)
        {
            return _userDal.Get(x => x.Id == id);
        }
        [CacheAspect(60)]
        public User GetByMailConfirmValue(string confirmValue)
        {
            return _userDal.Get(x => x.MailConfirmValue == confirmValue);
        }
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return _userDal.GetClaims(user, companyId);
        }

    }
}