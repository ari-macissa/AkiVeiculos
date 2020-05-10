using System.ComponentModel.DataAnnotations;

namespace AkiVeiculos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        [StringLength(10, MinimumLength = 5)]
        [Display(Name = "Usuário:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Favor informar {0}")]
        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        public Usuario()
        {

        }

        public Usuario(int id, string login, string senha)
        {
            Id = id;
            Login = login;
            Senha = senha;
        }


    }
}
