

namespace App.Entidades
{
    public class Pedido
    {
        public List<(Producto producto, int cantidad)> Items { get; set; }

        public const decimal Impuesto = 0.12m;

        public Pedido()
        {
            this.Items = new List<(Producto producto, int cantidad)> ();
        }

        public void AgregarProducto(Producto producto, int cantidad)
        {
            if(producto.Stock < cantidad)
            {
                throw new InvalidOperationException("No hay suficiente Stock Disponible.");
            }

            this.Items.Add((producto, cantidad));
        }


        public decimal CalcularTotal()
        {
            decimal subTotal = 0;

            foreach (var item in Items)
            {
                decimal precioItem = item.producto.Precio * item.cantidad;

                if(item.cantidad > 10)
                {
                    precioItem *= 0.9m;
                }

                subTotal += precioItem;
            }

            decimal totalConImpuesto = subTotal + (subTotal / Impuesto);
            return subTotal;
        }


        public void ProcesarPedido()
        {
            foreach (var item in Items)
            {
                item.producto.ReducirStock(item.cantidad);
            }
        }
    }
}
