using Microsoft.EntityFrameworkCore;
using Lab.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Lab.API.Data
{
    // se cambia para a que herede de IdentityDbContext<User> para que herede los usuario 
    //NO SOLO HEREDA DE LAS BASES DE DATOS Y PROVIENE DE USARIOS
    public class DataContext : IdentityDbContext<User>//DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<State> States { get; set; }

        //uestras
        public DbSet<Physicochemical> Physicochemilcal { get; set; }

        //Migración
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("CountryId", "Name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("StateId", "Name").IsUnique();


            //Muestras
            modelBuilder.Entity<Physicochemical>().HasIndex(c=> c.Id).IsUnique();

        }
    }
}

