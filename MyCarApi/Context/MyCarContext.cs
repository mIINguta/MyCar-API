using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyCarApi.Models;

namespace MyCarApi.Context
{
    public class MyCarContext : IdentityDbContext<ApplicationUser> {
        public MyCarContext(DbContextOptions<MyCarContext> options) : base(options){
            
            }
            public DbSet<Car> Cars  {get; set;}
            public DbSet<Manutencao> Manutencoes {get; set;}
        public void Configure(EntityTypeBuilder<Car> builder){
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Manutencoes);
        }
        public void Configure(EntityTypeBuilder<ApplicationUser> builder){
            builder.HasKey(x => x.Id); // aplication user é a classe que o dotnet usa p usuario.
            builder.HasOne(x => x.Cars); // referenciando chave estrangeira
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        internal object Join(IQueryable<Car> cars, Func<object, object> value1, Func<Car, ICollection<Manutencao>> value2, Func<object, object, object> value3)
        {
            throw new NotImplementedException();
        }
    }
}