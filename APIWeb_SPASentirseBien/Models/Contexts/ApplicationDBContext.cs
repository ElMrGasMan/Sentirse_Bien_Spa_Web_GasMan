using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIWeb_SPASentirseBien.Models.Contexts
{
    public class ApplicationDBContext : IdentityDbContext<Usuario>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Necesario para definir una relacion de muchos a muchos
            modelBuilder.Entity<Usuario>()
                .HasMany(ut => ut.Turnos)
                .WithMany(ut => ut.Usuarios);
        }
    }
}