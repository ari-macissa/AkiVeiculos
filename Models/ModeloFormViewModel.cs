using System.Collections.Generic;

namespace AkiVeiculos.Models
{
    public class ModeloFormViewModel
    {
        public Modelo Modelo { get; set; }
        public ICollection<Marca> Marcas { get; set; }
    }
}