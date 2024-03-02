using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eReconciliation.Core.Entities;

namespace eReconciliation.Core.DataAccess
{
    //IEntityRepositorye verilecek olan T bir class olmalı IEntity den türetme almış olmalı ve newlenebilir olmalıdır.
    //Newlenebilir olmasını istenmesinin sebebi Interface gönderimini engellemektir.
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        //bir sorgu yollayabilirim veya sorgu göndermeden direk liste isteyebilirim.
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);

    }
}