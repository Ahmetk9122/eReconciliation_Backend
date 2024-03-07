using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;


namespace eReconciliation.Business
{
    public interface ICompanyService
    {
        IResult Add(Company company);
        IDataResult<List<Company>> GetList();

    }
}