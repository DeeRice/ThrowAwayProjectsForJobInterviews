using IntegraPartnersContactApplicationAPI.Interface;
using IntegraPartnersContactApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace IntegraPartnersContactApplicationAPI
{
    public class IntegraPartnersContactAPIDataContext : DbContext
    {
        public IntegraPartnersContactAPIDataContext(DbContextOptions<IntegraPartnersContactAPIDataContext> options)
            : base(options)
        {
        }
        public IntegraPartnersContactAPIDataContext()
        {
           
        }
        public virtual DbSet<Users> Users { get; set; } = null!;

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SavingChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
