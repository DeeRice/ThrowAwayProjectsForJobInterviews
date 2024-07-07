using IntegraPartnersContactApplicationAPI.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace IntegraPartnersContactApplicationAPI
{
    public class IntegraPartnersContactAPIDataContext : DbContext
    {
        public IntegraPartnersContactAPIDataContext(DbContextOptions<IntegraPartnersContactAPIDataContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } = null!;
    }
}
