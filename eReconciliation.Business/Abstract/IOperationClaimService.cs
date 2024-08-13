using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Results.Abstract;

namespace eReconciliation.Business.Abstract
{
    public interface IOperationClaimService
    {
        IResult AddOperationClaim(OperationClaim operationClaim);
        IResult UpdateOperationClaim(OperationClaim operationClaim);
        IResult DeleteOperationClaim(OperationClaim operationClaim);
        IDataResult<OperationClaim> OperationClaimGetById(int operationClaimId);
        IDataResult<List<OperationClaim>> OperationClaimGetList();
    }
}