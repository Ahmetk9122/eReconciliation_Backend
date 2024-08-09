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
    public class BaBsReconciliationDetailService : IBaBsReconciliationDetailService
    {
        private readonly IBaBsReconciliationDetailDal _baBsReconciliationDetailDal;

        public BaBsReconciliationDetailService(IBaBsReconciliationDetailDal baBsReconciliationDetailDal)
        {
            _baBsReconciliationDetailDal = baBsReconciliationDetailDal;
        }

        public IResult AddBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Add(baBsReconciliationDetail);
            return new SuccessResult(Messages.AddedbaBsReconciliationDetail);

        }
        [TransactionScopeAspect]
        public IResult AddBaBsReconciliationDetailToExcel(string filePath, int baBsReconciliationId)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string description = reader.GetString(1);


                        if (description != "Açıklama" && description != null)
                        {
                            DateTime date = reader.GetDateTime(0);
                            double amount = reader.GetDouble(2);

                            BaBsReconciliationDetail baBsReconciliationDetail = new BaBsReconciliationDetail()
                            {
                                BaBsReconciliationId = baBsReconciliationId,
                                Date = date,
                                Description = description,
                                Amount = Convert.ToDecimal(amount),
                            };
                            _baBsReconciliationDetailDal.Add(baBsReconciliationDetail);
                        }
                    }
                }
            }
            File.Delete(filePath);
            return new SuccessResult(Messages.AddedAccountReconciliationDetail);
        }

        public IDataResult<BaBsReconciliationDetail> BaBsReconciliationDetailGetById(int baBsReconciliationDetailId)
        {
            return new SuccessDataResult<BaBsReconciliationDetail>(_baBsReconciliationDetailDal.Get(x => x.Id == baBsReconciliationDetailId));
        }

        public IDataResult<List<BaBsReconciliationDetail>> BaBsReconciliationDetailGetList(int baBsReconciliationId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDetail>>(_baBsReconciliationDetailDal.GetList(x => x.BaBsReconciliationId == baBsReconciliationId));
        }

        public IResult DeleteBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Delete(baBsReconciliationDetail);
            return new SuccessResult(Messages.DeletebaBsReconciliationDetail);
        }

        public IResult UpdateBaBsReconciliationDetail(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            _baBsReconciliationDetailDal.Update(baBsReconciliationDetail);
            return new SuccessResult(Messages.UpdatebaBsReconciliationDetail);
        }
    }
}