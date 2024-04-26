using Microsoft.EntityFrameworkCore;
using MyCarApi.Models;

namespace MyCarApi.Context
{
    public class MyCarContext : DbContext
    {
        public MyCarContext(DbContextOptions<MyCarContext> options) : base(options){

        }

        public DbSet<Usuario> Usuarios {get; set;}
    }
}