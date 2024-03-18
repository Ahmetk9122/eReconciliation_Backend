using AutoMapper;
using eReconciliation.Core.Entities;

namespace eReconciliation.Core.Extensions
{
    public static class MappingExtensions
    {
        private static IMapper _mapper;
        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }
        public static TDtoObject ConvertTo<TDtoObject>(this IEntity entity)
        {
            return _mapper.Map<TDtoObject>(entity);
        }
        public static TEntityObject ConvertTo<TEntityObject>(this IDto entity)
        {
            return _mapper.Map<TEntityObject>(entity);
        }
    }
}