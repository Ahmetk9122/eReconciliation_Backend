using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business.Concrete
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        [SecuredOperation("Admin,UserOperationClaim.Add")]
        public IResult AddUserOperationClaim(UserOperationClaim operationClaim)
        {
            _userOperationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.AddedUserOperationClaim);
        }

        [SecuredOperation("Admin,UserOperationClaim.Delete")]
        public IResult DeleteUserOperationClaim(UserOperationClaim operationClaim)
        {
            _userOperationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.DeleteUserOperationClaim);
        }

        [SecuredOperation("Admin,UserOperationClaim.Update")]
        public IResult UpdateUserOperationClaim(UserOperationClaim operationClaim)
        {
            _userOperationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.UpdateUserOperationClaim);
        }

        [SecuredOperation("Admin,UserOperationClaim.Get")]
        public IDataResult<UserOperationClaim> UserOperationClaimGetById(int userOperationClaimId)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(x => x.Id == userOperationClaimId));
        }

        [SecuredOperation("Admin,UserOperationClaim.GetList")]
        public IDataResult<List<UserOperationClaim>> UserOperationClaimGetList(int userId, int companyId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetList(x => x.UserId == userId && x.CompanyId == companyId));
        }
    }
}