using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Entidades;

namespace PeliculasApi
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Asi se configura una llave primaria compuesta
            modelBuilder.Entity<PeliculasActores>()
                 .HasKey(x => new { x.ActorId, x.PeliculaId });

            modelBuilder.Entity<PeliculasGeneros>()
                 .HasKey(x => new { x.PeliculaId, x.GeneroId });

            modelBuilder.Entity<PeliculasCines>()
                 .HasKey(x => new { x.PeliculaId, x.CineId });


            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Los dbSet sirven para indicar que tablas voy a tener en BD con base en las clases
        /// </summary>
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Cine> Cines { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<PeliculasActores> PeliculasActores { get; set; }

        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }

        public DbSet<PeliculasCines> PeliculasCines { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }
}
