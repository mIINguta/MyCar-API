using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCarApi.Models;

namespace MyCarApi.Context
{
    public class MyCarContext : IdentityDbContext    {
        public MyCarContext(DbContextOptions<MyCarContext> options) : base(options){
            }
          public DbSet<User> Users {get; set;}
        public void ConfigureServices (IServiceCollection services){
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MyCarContext>();
        }
        
    }
}