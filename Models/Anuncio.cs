using System;
using System.ComponentModel.DataAnnotations;

namespace AkiVeiculos.Models
{
    public class Anuncio
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data da venda:")]
        [Required(ErrorMessage = "Favor informar {0}")]
        public DateTime DataVenda { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "Favor informar {0}")]
        [Display(Name = "Valor da compra:")]
        public double ValorCompra { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "Favor informar {0}")]
        [Display(Name = "Valor da venda:")]
        public double ValorVenda { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        [Display(Name = "Cor do veículo:")]
        public string Cor { get; set; }

        public Modelo Modelo { get; set; }

        public int ModeloId { get; set; }

        public Anuncio()
        {

        }

        public Anuncio(int id, DateTime dataVenda, double valorCompra, double valorVenda, string cor, Modelo modelo)
        {
            Id = id;
            DataVenda = dataVenda;
            ValorCompra = valorCompra;
            ValorVenda = valorVenda;
            Cor = cor;
            Modelo = modelo;
        }

        [Display(Name = "Lucro:")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Lucro
        {
            get
            {
                return ValorVenda - ValorCompra;
            }
        }


    }
}
