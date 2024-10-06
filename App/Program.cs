using App.Entidades;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Crear productos
            var producto1 = new Producto("Laptop", 1000m, 20);
            var producto2 = new Producto("Mouse", 50m, 50);
            var producto3 = new Producto("Teclado", 80m, 30);

            // Crear un pedido
            var pedido = new Pedido();

            try
            {
                // Agregar productos al pedido
                pedido.AgregarProducto(producto1, 2); // Sin descuento
                pedido.AgregarProducto(producto2, 12); // Con descuento
                pedido.AgregarProducto(producto3, 5);  // Sin descuento

                // Calcular el total del pedido
                var total = pedido.CalcularTotal();
                Console.WriteLine($"El total del pedido con impuestos es: {total:C}");

                // Procesar el pedido (reducir el stock)
                pedido.ProcesarPedido();
                Console.WriteLine("Pedido procesado exitosamente.");

                // Mostrar el stock restante de los productos
                Console.WriteLine($"Stock restante de {producto1.Nombre}: {producto1.Stock}");
                Console.WriteLine($"Stock restante de {producto2.Nombre}: {producto2.Stock}");
                Console.WriteLine($"Stock restante de {producto3.Nombre}: {producto3.Stock}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
