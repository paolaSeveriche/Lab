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

        //Muestras
        public DbSet<Physicochemical> Physicochemical { get; set; }
        public DbSet<Microbiological> Microbiological { get; set; }
        public DbSet<DataSample> DataSample { get; set; }

        //Resultado
        public DbSet<Result> Results { get; set; }

        //Migración
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex("CountryId", "Name").IsUnique();
            modelBuilder.Entity<City>().HasIndex("StateId", "Name").IsUnique();


            //Muestras
            modelBuilder.Entity<Physicochemical>().HasIndex(c=> c.Id).IsUnique();
            modelBuilder.Entity<Microbiological>().HasIndex(c => c.Id).IsUnique();
            modelBuilder.Entity<DataSample>().HasIndex(c=> c.Id).IsUnique();   


            //Resultado
            modelBuilder.Entity<Result>().HasIndex(c=> c.Id).IsUnique();   
        }
    }
}

