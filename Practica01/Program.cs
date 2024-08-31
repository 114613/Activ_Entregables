using Practica01.Data;
using Practica01.Domain;
using Practica01.Services;

class Program
{
    static void Main(string[] args)
    {
        var repositorio = new FacturaRepository();
        var servicio = new FacturaService(repositorio);

        var cliente = new Cliente("Gabriel Yoles Trucco", "Calle 123");
        var formaPago = new FormaPago("transferencia");
        var factura = new Factura(1, DateTime.Now, formaPago, cliente);


        factura.AgregarDetalle(new DetalleFactura(new Articulo("Articulo1", 100), 2));
        factura.AgregarDetalle(new DetalleFactura(new Articulo("Articulo1", 100), 3)); //Agrega cantidad
        factura.AgregarDetalle(new DetalleFactura(new Articulo("Articulo2", 200), 1));

        servicio.CrearFactura(factura);

        // Mostrar detalles de la factura
        Factura? facturaObtenida = servicio.ObtenerFactura(1);
        if (facturaObtenida != null)
        {
            Console.WriteLine($"Factura Nro: {facturaObtenida.NroFactura}");
            Console.WriteLine($"Cliente: {facturaObtenida.Cliente}");
            Console.WriteLine($"Total: {facturaObtenida.Total()}");
        }
        else
        {
            Console.WriteLine("Factura no encontrada.");
        }

        // Actualizar una factura
        if (facturaObtenida != null)
        {
            facturaObtenida.FormaPago = new FormaPago("Tarjeta");

            facturaObtenida.Detalles.Clear();
            facturaObtenida.AgregarDetalle(new DetalleFactura(new Articulo("Articulo1", 100), 5));
            facturaObtenida.AgregarDetalle(new DetalleFactura(new Articulo("Articulo2", 200), 3));


            servicio.ActualizarFactura(facturaObtenida);

            Factura facturaActualizada = servicio.ObtenerFactura(1);

            if (facturaActualizada != null)
            {
                Console.WriteLine($"Factura Nro: {facturaActualizada.NroFactura}");
                Console.WriteLine($"Cliente: {facturaActualizada.Cliente.Nombre}");
                Console.WriteLine($"Forma de Pago: {facturaActualizada.FormaPago.Nombre}");
                Console.WriteLine($"Total: {facturaActualizada.Total()}");
            }
        }

        // Eliminar una factura
        servicio.Eliminar(1);

        // Mostrar todas las facturas
        var todasLasFacturas = servicio.ListarFacturas();
        Console.WriteLine($"Total facturas registradas: {todasLasFacturas.Count}");
    }
}
