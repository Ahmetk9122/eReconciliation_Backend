using AutoMapper;
using eReconciliation.Entities;
using eReconciliation.Entities.Concrete;
using eReconciliation.Entities.Dtos;

namespace eReconciliation.Business
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<MailParameter, MailParameterDto>();
            CreateMap<MailParameterDto, MailParameter>();

            CreateMap<UserForRegister, UserForRegisterDto>();
            CreateMap<UserForRegisterDto, UserForRegister>();

            CreateMap<UserForLogin, UserForLoginDto>();
            CreateMap<UserForLoginDto, UserForLogin>();

            CreateMap<MailTemplate, MailTemplateDto>();
            CreateMap<MailTemplateDto, MailTemplate>();

            CreateMap<CurrencyAccount, CurrencyAccountDto>();
            CreateMap<CurrencyAccountDto, CurrencyAccount>();

            CreateMap<AccountReconciliation, AccountReconciliationDto>();
            CreateMap<AccountReconciliationDto, AccountReconciliation>();

            CreateMap<AccountReconciliationDetail, AccountReconciliationDetailDto>();
            CreateMap<AccountReconciliationDetailDto, AccountReconciliationDetail>();

            CreateMap<BaBsReconciliation, BaBsReconciliationDto>();
            CreateMap<BaBsReconciliationDto, BaBsReconciliation>();
        }
    }
}