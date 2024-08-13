using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eReconciliation.DataAccess.Concrete
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=postgres");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // LoadSeedDatas(modelBuilder);
            // LoadUniqueColumns(modelBuilder);

            base.OnModelCreating(modelBuilder);

        }

        #region [ LoadUniqueColumns ]
        private void LoadUniqueColumns(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<UserOperationClaim>()
            //     .HasOne(uoc => uoc.OperationClaim)
            //     .WithMany() // Burada boş bırakıyoruz çünkü `OperationClaim` üzerinde navigation property yok.
            //     .HasForeignKey(uoc => uoc.OperationClaimId)
            //     .OnDelete(DeleteBehavior.Restrict); // Silme davranışını engelle

            // // OperationClaim silme işlemini engelleyen bir filter uyguluyoruz.
            // modelBuilder.Entity<OperationClaim>()
            //     .HasQueryFilter(oc => !UserOperationClaims.Any(uoc => uoc.OperationClaimId == oc.Id));


        }
        #endregion

        #region [ LoadSeedDatas ]
        private void LoadSeedDatas(ModelBuilder modelBuilder)
        {
            var currentDate = DateTime.Now; // Mevcut tarih ve saat

            modelBuilder.Entity<OperationClaim>().HasData(
                new OperationClaim { Name = "Admin", Description = "Admin", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliationDetail.Add", Description = "AccountReconciliationDetail.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliationDetail.Get", Description = "AccountReconciliationDetail.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliationDetail.Update", Description = "AccountReconciliationDetail.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliationDetail.Delete", Description = "AccountReconciliationDetail.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliationDetail.GetList", Description = "AccountReconciliationDetail.GetList", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliation.Add", Description = "AccountReconciliation.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliation.Get", Description = "AccountReconciliation.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliation.Update", Description = "AccountReconciliation.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliation.Delete", Description = "AccountReconciliation.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "AccountReconciliation.GetList", Description = "AccountReconciliation.GetList", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliationDetail.Add", Description = "BaBsReconciliationDetail.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliationDetail.Get", Description = "BaBsReconciliationDetail.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliationDetail.Update", Description = "BaBsReconciliationDetail.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliationDetail.Delete", Description = "BaBsReconciliationDetail.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliationDetail.GetList", Description = "BaBsReconciliationDetail.GetList", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliation.Add", Description = "BaBsReconciliation.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliation.Get", Description = "BaBsReconciliation.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliation.Update", Description = "BaBsReconciliation.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliation.Delete", Description = "BaBsReconciliation.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "BaBsReconciliation.GetList", Description = "BaBsReconciliation.GetList", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "Company.Update", Description = "Company.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "CurrencyAccount.Add", Description = "CurrencyAccount.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "CurrencyAccount.Get", Description = "CurrencyAccount.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "CurrencyAccount.Update", Description = "CurrencyAccount.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "CurrencyAccount.Delete", Description = "CurrencyAccount.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "MailParameter.Update", Description = "MailParameter.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "MailTemplate.Update", Description = "MailTemplate.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "MailTemplate.Delete", Description = "MailTemplate.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "User.Update", Description = "User.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "UserOperationClaim.Add", Description = "UserOperationClaim.Add", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "UserOperationClaim.Get", Description = "UserOperationClaim.Get", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "UserOperationClaim.Update", Description = "UserOperationClaim.Update", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "UserOperationClaim.Delete", Description = "UserOperationClaim.Delete", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "UserOperationClaim.GetList", Description = "UserOperationClaim.GetList", AddedAt = currentDate, IsActive = true },
                new OperationClaim { Name = "OperationClaim.GetList", Description = "OperationClaim.GetList", AddedAt = currentDate, IsActive = true }
            );
        }
        #endregion

        #region DBSET
        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
        public DbSet<AccountReconciliation> AccountReconciliations { get; set; }
        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail> BaBsReconciliationDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<TermsandCondition> TermsandConditions { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserReletionship> UserReletionships { get; set; }
        public DbSet<UserThemeOption> UserThemeOptions { get; set; }
        #endregion
    }
}