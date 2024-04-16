using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public interface IMailParameterService
    {
        public IResult UpdateMailParameter(MailParameter mailParameter);
        public IDataResult<MailParameter> GetMailParameter(int companyId);
    }
}