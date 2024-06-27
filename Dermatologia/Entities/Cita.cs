namespace Dermatologia.Entities
{
    public class Cita
    {
        public Guid Id { get; set; }
        public string NombreDeCita { get; set; }
        public string Telefono { get; set; }
        public string DoctorDeCita { get; set; }
        public DateTime FechaDeCita { get; set; }
    }
}