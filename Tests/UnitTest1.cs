using App.Entidades;

namespace Tests
{
    public class Tests
    {
        private Producto _producto1;
        private Producto _producto2;
        private Pedido _pedido;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _producto1 = new Producto("Laptop", 1000m, 20);
            _producto2 = new Producto("Mouse", 50m, 50);
            _pedido = new Pedido();
        }

        [Test(Author = "Darwin Ruiz", Description = "Agregar un producto correctamente con el stock suficiente")]
        public void AgregarProductoStockSuficiente()
        {
            //Arrange
            int cantidadPedido = 2;

            //Act
            _pedido.AgregarProducto(_producto1, cantidadPedido);

            //Assert

            Assert.That(_pedido.Items.Count, Is.EqualTo(1));
            Assert.That(_pedido.Items[0].producto.Nombre, Is.EqualTo(_producto1.Nombre));
            Assert.That(_pedido.Items[0].cantidad, Is.EqualTo(cantidadPedido));
        }


        [Test(Author = "Darwin Ruiz", Description = "Agregar un producto con un Stock insuficiente, se espera una excepción")]
        public void AgregarProductoStockInSuficiente()
        {
            //Arrange
            int cantidadPedido = 25;

            //Act
            var resultado = Assert.Throws<InvalidOperationException>(() => _pedido.AgregarProducto(_producto1, cantidadPedido));

            //Assert
            Assert.That(resultado.Message, Is.EqualTo("No hay suficiente Stock Disponible."));
        }

        [Test(Author = "Darwin Ruiz", Description = "Validar que al realizar pedidos menos de 10 no se aplique descuente")]
        public void CalcularTotalSinDescuento()
        {
            // Arrange
            _pedido.AgregarProducto(_producto1, 5); // 5 * 1000 = 5000
            _pedido.AgregarProducto(_producto2, 3); // 3 * 50 = 150

            // Act
            decimal total = _pedido.CalcularTotal();

            // Assert
            Assert.That(total, Is.EqualTo(5150)); // 5000 + 150 + (5150 * 0.12)
        }

        [Test(Author = "Darwin Ruiz", Description = "Validar que al realizar pedidos mayores de 10 se aplique el 10% de descuente")]
        public void CalcularTotalConDescuento()
        {
            // Arrange
            _pedido.AgregarProducto(_producto1, 15); // 15 * 1000 = 15000 (10% de descuento)
            _pedido.AgregarProducto(_producto2, 5); // 5 * 50 = 250

            // Act
            decimal total = _pedido.CalcularTotal();

            // Assert
            Assert.That(total, Is.EqualTo(15900)); // (15000 * 0.9) + 250 + ((15900) * 0.12)
        }

        [Test(Author = "Darwin Ruiz", Description = "Validar si al realizar los pedidos el Stock se reduce correctamente")]
        public void ProcesarPedidoReduceStockCorrectamente()
        {
            // Arrange
            _pedido.AgregarProducto(_producto1, 2);
            _pedido.AgregarProducto(_producto2, 3);

            // Act
            _pedido.ProcesarPedido();

            // Assert
            Assert.That(_producto1.Stock, Is.EqualTo(18)); // Stock inicial 20 - 2
            Assert.That(_producto2.Stock, Is.EqualTo(47)); // Stock inicial 50 - 3
        }
    }
}