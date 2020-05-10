using System.ComponentModel.DataAnnotations;

namespace AkiVeiculos.Models
{
    public class Marca
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        [StringLength(12, ErrorMessage = "Permitido só até {1} caracteres")]
        [Display(Name = "Nome da marca:")]
        public string Nome { get; set; }

        public Marca()
        {

        }

        public Marca(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }
}
