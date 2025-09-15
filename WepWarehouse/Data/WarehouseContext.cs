using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace WepWarehouse.Data
{
    public class WarehouseContext : IdentityDbContext<ApplicationUser>
    {
        IConfiguration config;
        public WarehouseContext(IConfiguration _config)
        {
            config = _config;
        }
        public DbSet<Country> countries { get; set; }
        public DbSet<WareHouse> wareHouses { get; set; }

        public DbSet<WareHouseItem> wareItems { get; set; }

        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("WarehouseConn"));
            base.OnConfiguring(optionsBuilder);
        }
    }
    







}
        
