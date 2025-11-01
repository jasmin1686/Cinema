using Cinema.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Cinema.ViewModels;

namespace Cinema.DataAcess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MovieSubimg> MovieSubimgs { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinemaa> Cinemaas { get; set; }
        public DbSet<Actormovie> Actormovies { get; set; }
        public DbSet<ApplicationUserOTP>ApplicationUserOTPs { get; set; }
        //public object MovieSubimg { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Integrated Security=True;Initial Catalog=Cinema_13;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actormovie>()
                .HasKey(am => new { am.ActorId, am.MovieId }); 

            modelBuilder.Entity<Actormovie>()
                .HasOne(am => am.Actor)
                .WithMany(a => a.Actormovies)
                .HasForeignKey(am => am.ActorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Actormovie>()
                .HasOne(am => am.Movie)
                .WithMany(m => m.Actormovies)
                .HasForeignKey(am => am.MovieId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Cinema.ViewModels.RegisterVM> RegisterVM { get; set; } = default!;
    }
}
