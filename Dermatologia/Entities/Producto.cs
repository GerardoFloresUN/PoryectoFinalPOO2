namespace Dermatologia.Entities
{
    public class Producto
    {
         public Guid Id { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Disponibilidad { get; set; }
    }
}