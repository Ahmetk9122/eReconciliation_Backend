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
            _companyDal.Add(company);
            return new SuccessResult(Messages.AddedCompany);
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