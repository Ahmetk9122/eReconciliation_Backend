using eReconciliation.Business.Constans;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;

namespace eReconciliation.Business
{
    public class CompanyService : ICompanyService
    {

        /// <summary>
        /// interfaceler yenilenemediği için kurucu metodda yenileme işlemi yapılır.
        /// </summary>
        private readonly ICompanyDal _companyDal;

        public CompanyService(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public IResult Add(Company company)
        {
            if (!((company.Name?.Length ?? 0) > 10)) throw new Exception("Şirket Adı En Az 10 Karakter olmalıdır.");
            _companyDal.Add(company);
            return new SuccessResult(Messages.AddedCompany);
        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyDal.Get(x => x.Name == company.Name && x.TaxDepartment == company.TaxDepartment && x.TaxIdNumber == company.TaxIdNumber && x.IdentityNumber == x.IdentityNumber);
            if (result != null)
                return new ErrorResult(Messages.CompanyAlreadyExist);
            return new SuccessResult();
        }

        public IResult UserCompanyMapingAdd(int userId, int companyId)
        {
            _companyDal.UserCompanyMapingAdd(userId, companyId);
            return new SuccessResult();
        }

        /// <summary>
        /// Business katmanı amacı
        /// Kullanıcı yetkileri
        /// Transcaption
        /// Log
        /// Validation işlemlerini yaparak hata çıkmazsa veritabanıan yönlendermektir.
        /// </summary>
        /// <returns></returns>
        IDataResult<List<Company>> ICompanyService.GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetList(), Messages.List);
        }
    }
}