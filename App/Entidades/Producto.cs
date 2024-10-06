

namespace App.Entidades
{
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public Producto(string nombre, decimal precio, int stock)
        {
            this.Nombre = nombre;
            this.Precio = precio;
            this.Stock = stock;
        }

        public void ReducirStock(int cantidad)
        {
            if(cantidad > this.Stock)
            {
                throw new InvalidOperationException("Stock insuficiente.");
            }

            this.Stock -= cantidad;
        }
    }
}
