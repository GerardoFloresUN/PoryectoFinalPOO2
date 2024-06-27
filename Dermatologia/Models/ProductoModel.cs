namespace Dermatologia.Models
{
    public class ProductoModel
    {
         public Guid Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Disponibilidad { get; set; }
    }
}