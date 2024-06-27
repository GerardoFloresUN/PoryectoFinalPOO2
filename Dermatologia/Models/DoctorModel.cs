namespace Dermatologia.Models
{
    public class DoctorModel
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public int Experiencia { get; set; }
        public string Educacion { get; set; }
    }
}