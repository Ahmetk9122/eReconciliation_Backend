using Castle.DynamicProxy;
using eReconciliation.Core.CrossCuttingConcerns.Caching;
using eReconciliation.Core.Utilities.Interceptors;
using eReconciliation.Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliation.Core.Aspects.Caching
{
    public class CacheAspect : MethodInterception
    {
        // Önceden tanımlanmış bir önbellek süresi ve önbellek yöneticisi için alanlar.
        private int _duration;
        private ICacheManager _cacheManager;

        // Yapıcı metod, önbellek süresini ayarlar ve bir ICacheManager örneğini alır.
        public CacheAspect(int duration = 60)
        {
            _duration = duration; // Önbellek süresi, varsayılan olarak 60 saniye.
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
            // _cacheManager, bağımlılık enjeksiyonu (Dependency Injection) ile alınır.
        }

        // Intercept metodu, bir metodun çalıştırılmasını yakalar ve bu metodun önbelleğe alınmış bir sonucu olup olmadığını kontrol eder.
        public override void Intercept(IInvocation invocation)
        {
            // Metodun adını, sınıf adı ve metod adı ile birleştirerek tam bir metod adı oluşturur.
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");

            // Metodun argümanlarını bir listeye dönüştürür.
            var arguments = invocation.Arguments.ToList();

            // Argümanlar ve metod adı ile benzersiz bir önbellek anahtarı oluşturur.
            var key = $"{methodName}({string.Join(", ", arguments.Select(x => x?.ToString() ?? "<Null>"))})";

            // Eğer önbellekte bu anahtar için bir değer varsa, metodun çalıştırılmasını durdurur ve önbellekteki değeri döndürür.
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key); // Önbellekteki değeri alır ve döndürür.
                return; // Metodun çalıştırılmasını sonlandırır.
            }

            // Önbellekte bir değer yoksa, orijinal metodun çalıştırılmasını sağlar.
            invocation.Proceed();

            // Orijinal metod çalıştırıldıktan sonra, sonucu önbelleğe ekler.
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
            // Metodun sonucunu belirtilen süre boyunca (duration) önbelleğe kaydeder.
        }
    }
}