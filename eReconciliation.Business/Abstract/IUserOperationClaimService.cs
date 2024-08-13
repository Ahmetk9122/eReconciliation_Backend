using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult AddUserOperationClaim(UserOperationClaim operationClaim);
        IResult UpdateUserOperationClaim(UserOperationClaim operationClaim);
        IResult DeleteUserOperationClaim(UserOperationClaim operationClaim);
        IDataResult<UserOperationClaim> UserOperationClaimGetById(int operationClaimId);
        IDataResult<List<UserOperationClaim>> UserOperationClaimGetList(int userId, int companyId);
    }
}