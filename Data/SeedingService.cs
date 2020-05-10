using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AkiVeiculos.Models;
using AkiVeiculos.Models.Enums;

namespace AkiVeiculos.Data
{
    public class SeedingService
    {
        private AkiVeiculosContext _context;

        public SeedingService(AkiVeiculosContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Anuncio.Any() ||
                _context.Marca.Any() ||
                _context.Modelo.Any())
            {
                return; // DB has been seeded
            }

            Marca marca1 = new Marca(1, "Fiat");
            Marca marca2 = new Marca(2, "Ford");
            Marca marca3 = new Marca(3, "Renault");
            Marca marca4 = new Marca(4, "Toyota");

            Modelo modelo1 = new Modelo(1, "Toro Volcano 2.0", 2020, TipoCombustivel.Diesel, marca1);
            Modelo modelo2 = new Modelo(2, "Ranger 2.2 CD XLS", 2020, TipoCombustivel.Diesel, marca2);
            Modelo modelo3 = new Modelo(3, "Sandero Life 1.0 12V", 2020, TipoCombustivel.Flex, marca3);
            Modelo modelo4 = new Modelo(4, "Corolla XEi 2.0", 2020, TipoCombustivel.Flex, marca4);

            Anuncio anuncio1 = new Anuncio(1, new DateTime(2020, 01, 03), 90000, 120000, "preto", modelo1);
            Anuncio anuncio2 = new Anuncio(2, new DateTime(2020, 01, 05), 100000, 154000, "branco", modelo2);
            Anuncio anuncio3 = new Anuncio(3, new DateTime(2020, 02, 01), 30000, 50000, "azul", modelo3);
            Anuncio anuncio4 = new Anuncio(4, new DateTime(2020, 03, 10), 80000, 110000, "prata", modelo4);

            Usuario usuario1 = new Usuario(1,"admin","123456");

            _context.Marca.AddRange(marca1, marca2, marca3, marca4);

            _context.Modelo.AddRange(modelo1, modelo2, modelo3, modelo4);

            _context.Anuncio.AddRange(anuncio1, anuncio2, anuncio3, anuncio4);

_context.Usuario.AddRange(usuario1);

            _context.SaveChanges();
        }
    }
}
