using System.ComponentModel.DataAnnotations;

namespace dotnetRoutes.Models
{
    public class ModelLogin
    {        
        [Required(ErrorMessage = "" )]
        public string? Usuario { get; set; }
        [Required( ErrorMessage = "Necessário informar a senha de acesso.")]
        public string? Senha { get; set; }        
    }
}
