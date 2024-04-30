using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCarApi.Models;

namespace MyCarApi.Context
{
    public class MyCarContext : IdentityDbContext<ApplicationUser>   {
        public MyCarContext(DbContextOptions<MyCarContext> options) : base(options){
            }
        
    }
}