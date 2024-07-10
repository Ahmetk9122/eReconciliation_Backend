using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Entities.Concrete;


namespace eReconciliation.Business
{
    public interface ICompanyService
    {
        IResult Add(Company company);
        IResult UpdateCompany(Company company);
        IResult AddCompanyAndUserCompany(CompanyDto companyDto);
        IDataResult<List<Company>> GetList();
        IDataResult<Company> GetCompanyById(int companyId);
        IResult CompanyExists(Company company);
        IResult UserCompanyMapingAdd(int userId, int companyId);
        IDataResult<UserCompany> GetCompany(int userId);

    }
}