using System;
using System.ComponentModel.DataAnnotations;
using AkiVeiculos.Models.Enums;

namespace AkiVeiculos.Models
{
    public class Modelo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requerido")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} Deve ter entre {2} e {1} caracteres")]
        [Display(Name = "Descrição do modelo:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        [Display(Name = "Tipo de combustível:")]
        public TipoCombustivel Combustivel { get; set; }

        public Marca Marca { get; set; }

        public int MarcaId { get; set; }


        public Modelo()
        {

        }

        public Modelo(int id, string nome, int ano, TipoCombustivel combustivel, Marca marca)
        {
            Id = id;
            Nome = nome;
            Ano = ano;
            Combustivel = combustivel;
            Marca = marca;
        }

    }
}
