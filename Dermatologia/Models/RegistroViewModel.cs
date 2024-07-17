using System.ComponentModel.DataAnnotations;

namespace Dermatologia.Models
{
    public class RegistroViewModel
    {
        public RegistroViewModel()
        {
        }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "El ampo debe ser un correo electrónico valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
    }
}