using eReconciliation.Business.BusinessAspects;
using eReconciliation.Business.Constans;
using eReconciliation.Business.ValidationRules.FluentValidation;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Aspects.Autofac.Validation;
using eReconciliation.Core.Aspects.Caching;
using eReconciliation.Core.Aspects.Performance;
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
        public IDataResult<UserCompany> GetCompany(int userId)
        {
            return new SuccessDataResult<UserCompany>(_companyDal.GetCompany(userId));
        }
        [CacheAspect(60)]
        public IDataResult<Company> GetCompanyById(int companyId)
        {
            var result = _companyDal.Get(x => x.Id == companyId);
            if (result == null)
                throw new Exception("Şirket bilgisine ulaşılamadı!");

            return new SuccessDataResult<Company>(result);
        }

        [CacheRemoveAspect("ICompanyService.Get")]
        [ValidationAspect(typeof(CompanyValidator))]
        public IResult Add(Company company)
        {
            if (!((company.Name?.Length ?? 0) > 4)) throw new Exception("Şirket Adı En Az 10 Karakter olmalıdır.");
            _companyDal.Add(company);
            return new SuccessResult(Messages.AddedCompany);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("Company.Update,Admin")]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult UpdateCompany(Company company)
        {
            GetCompanyById(company.Id);
            _companyDal.Update(company);
            return new SuccessResult(Messages.UpdatedCompany);

        }

        [ValidationAspect(typeof(CompanyValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult AddCompanyAndUserCompany(CompanyDto companyDto)
        {
            Add(companyDto.Company);
            UserCompanyMapingAdd(companyDto.UserId, companyDto.Company.Id);

            return new SuccessResult(Messages.AddedCompany);
        }

        public IResult CompanyExists(Company company)
        {
            var result = _companyDal.Get(x => x.Name == company.Name && x.TaxDepartment == company.TaxDepartment && x.TaxIdNumber == company.TaxIdNumber && x.IdentityNumber == x.IdentityNumber);
            if (result != null)
                return new ErrorResult(Messages.CompanyAlreadyExist);
            return new SuccessResult();
        }

        [CacheRemoveAspect("ICompanyService.Get")]
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
        [CacheAspect(60)]
        public IDataResult<List<Company>> GetList()
        {
            return new SuccessDataResult<List<Company>>(_companyDal.GetList(), Messages.List);
        }


    }
}