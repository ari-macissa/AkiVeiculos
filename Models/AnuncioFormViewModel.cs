using System.Collections.Generic;

namespace AkiVeiculos.Models
{
    public class AnuncioFormViewModel
    {
        public Anuncio Anuncio { get; set; }
        public ICollection<Marca> Marcas { get; set; }
        public ICollection<Modelo> Modelos { get; set; }
    }
}