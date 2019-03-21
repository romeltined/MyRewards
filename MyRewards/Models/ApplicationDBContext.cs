 using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
 
namespace MyRewards.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MyRewards.Models.VoucherType> VoucherTypes { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Staff> Staffs { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Merchant> Merchants { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Voucher> Vouchers { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.VoucherSpendLog> VoucherSpendLogs { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.VoucherTransferLog> VoucherTransferLogs { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.RefreshToken> RefreshTokens { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.VoucherOrderLog> VoucherOrderLogs { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Manager> Managers { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.SessionGuid> SessionGuids { get; set; }

        public System.Data.Entity.DbSet<MyRewards.Models.Settlement> Settlements { get; set; }
    }
}