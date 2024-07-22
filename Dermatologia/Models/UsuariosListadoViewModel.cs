namespace Dermatologia.Models
{
    public class UsuariosListadoViewModel
    {
        public UsuariosListadoViewModel()
        {
        }

        public List<UsuarioViewModel> Usuarios { get; set; }

        public string Mensaje { get; set; }
    }
}