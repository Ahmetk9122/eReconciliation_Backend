using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Business.Constans;
using eReconciliation.Core.Aspects.Autofac.Transaction;
using eReconciliation.Core.Utilities.Results.Abstract;
using eReconciliation.Core.Utilities.Results.Concrete;
using eReconciliation.DataAccess;
using eReconciliation.Entities.Concrete;
using ExcelDataReader;

namespace eReconciliation.Business
{
    public class BaBsReconciliationService : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal _baBsReconciliationDal;
        private readonly ICurrencyAccountService _currencyAccountService;

        public BaBsReconciliationService(IBaBsReconciliationDal baBsReconciliationDal, ICurrencyAccountService currencyAccountService)
        {
            _baBsReconciliationDal = baBsReconciliationDal;
            _currencyAccountService = currencyAccountService;
        }
        public IDataResult<BaBsReconciliation> BaBsReconciliationGetById(int baBsReconciliationId)
        {
            return new SuccessDataResult<BaBsReconciliation>(_baBsReconciliationDal.Get(x => x.Id == baBsReconciliationId));
        }

        public IDataResult<List<BaBsReconciliation>> BaBsReconciliationGetList(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>(_baBsReconciliationDal.GetList(x => x.CompanyId == companyId));
        }
        [TransactionScopeAspect]
        public IResult AddBaBsReconciliationToExcel(string filePath, int companyId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetString(0);


                        if (code != "Cari Kodu" && code != null)
                        {
                            string type = reader.GetString(1);
                            double mounth = reader.GetDouble(2);
                            double year = reader.GetDouble(3);
                            double quantity = reader.GetDouble(4);
                            double total = reader.GetDouble(5);

                            int currencyAccountId = _currencyAccountService.GetCurrencyAccountByCode(code, companyId).Data.Id;

                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation()
                            {
                                CompanyId = companyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = Convert.ToInt16(mounth),
                                Year = Convert.ToInt16(year),
                                Quantity = Convert.ToInt16(quantity),
                                Total = Convert.ToInt16(total),
                            };
                            _baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedAccountReconciliation);

        }
        public IResult AddBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Add(baBsReconciliation);
            return new SuccessResult(Messages.AddedbaBsReconciliation);
        }
        public IResult DeleteBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Delete(baBsReconciliation);
            return new SuccessResult(Messages.DeletebaBsReconciliation);
        }
        public IResult UpdateBaBsReconciliation(BaBsReconciliation baBsReconciliation)
        {
            _baBsReconciliationDal.Update(baBsReconciliation);
            return new SuccessResult(Messages.UpdatebaBsReconciliation);
        }
    }
}