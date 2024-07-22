namespace Dermatologia.Entities
{
    public class Doctor
    {
         public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public int Experiencia { get; set; }
        public string Educacion { get; set; }

        public List<Cita> Citas { get; set; }
    }
}