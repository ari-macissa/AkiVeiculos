using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AkiVeiculos.Models
{
    public class AkiVeiculosContext : DbContext
    {
        public AkiVeiculosContext(DbContextOptions<AkiVeiculosContext> options)
            : base(options)
        {
        }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
