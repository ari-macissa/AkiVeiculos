using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AkiVeiculos.Models
{
    public class AkiVeiculosContext : IdentityDbContext
    {
        public AkiVeiculosContext(DbContextOptions<AkiVeiculosContext> options)
            : base(options)
        {
        }



        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Anuncio> Anuncio { get; set; }
    }
}
