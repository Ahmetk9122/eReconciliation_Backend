using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Abstract;
using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;

namespace eReconciliation.Business.Concrete
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimService(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        [SecuredOperation("Admin")]
        public IResult AddOperationClaim(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.AddedOperationClaim);
        }
        [SecuredOperation("Admin")]
        public IResult DeleteOperationClaim(OperationClaim operationClaim)
        {
            // var result = _userOperationClaimDal.GetList(x => x.OperationClaimId == operationClaim.Id);
            // if ((result?.Count ?? 0) > 0)
            // {
            //     throw new Exception("Rolü Silmek için önce bağlantılarını kaldırın.");
            // }
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.DeleteOperationClaim);
        }

        [SecuredOperation("Admin")]
        public IDataResult<OperationClaim> OperationClaimGetById(int operationClaimId)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(x => x.Id == operationClaimId));
        }

        [SecuredOperation("OperationClaim.GetList,Admin")]
        public IDataResult<List<OperationClaim>> OperationClaimGetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetList());
        }

        [SecuredOperation("Admin")]
        public IResult UpdateOperationClaim(OperationClaim operationClaim)
        {
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.UpdateOperationClaim);
        }
    }
}