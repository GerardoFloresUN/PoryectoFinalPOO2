namespace Dermatologia.Models
{
    public class CitaModel
    {
        public Guid Id { get; set; }
        public string NombreDeCita { get; set; }
        public string Telefono { get; set; }
        public string DoctorDeCita { get; set; }
        public DateTime FechaDeCita { get; set; }
    }
}