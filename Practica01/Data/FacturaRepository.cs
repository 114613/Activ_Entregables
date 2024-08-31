using Practica01.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.Data
{
    public class FacturaRepository : IFacturaRepository
    {
        private string cnnString = $"Data Source=SQLEXPRESS;Initial Catalog=Practica01;Integrated Security=True";

        public void Agregar(Factura factura)
        {
            using (SqlConnection cnn = new SqlConnection(cnnString))
            {
                cnn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_InsertarFactura", cnn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    cmd.Parameters.AddWithValue("@FormaPagoId", factura.FormaPago.Id);
                    cmd.Parameters.AddWithValue("@ClienteId", factura.Cliente.Id);

                    int facturaId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach(var detalle in factura.Detalles)
                    {
                        using (SqlCommand detalleCmd = new SqlCommand("sp_InsertarDetalleFactura", cnn))
                        {
                            detalleCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            detalleCmd.Parameters.AddWithValue("@FacturaId", facturaId);
                            detalleCmd.Parameters.AddWithValue("@ArticuloId", detalle.Articulo.Id);
                            detalleCmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                            detalleCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public Factura GetById(int nroFactura)
        {
            Factura? factura = null;

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Factura WHERE NroFactura = @NroFactura", connection))
                {
                    cmd.Parameters.AddWithValue("@NroFactura", nroFactura);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var formaPagoId = (int)reader["FormaPagoId"];
                            var clienteId = (int)reader["ClienteId"];
                            var formaPago = ObtenerFormaPagoPorId(formaPagoId, connection);
                            var cliente = ObtenerClientePorId(clienteId, connection);

                            factura = new Factura(nroFactura, (DateTime)reader["Fecha"], formaPago, cliente);
                        }
                    }
                }

                if (factura != null)
                {
                    factura.Detalles = ObtenerDetallesPorFacturaId(nroFactura, connection);
                }
            }

            return factura;
        }

        public List<Factura> GetAll()
        {
            var facturas = new List<Factura>();

            using (SqlConnection connection = new SqlConnection(cnnString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Factura", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nroFactura = (int)reader["NroFactura"];
                            var formaPagoId = (int)reader["FormaPagoId"];
                            var clienteId = (int)reader["ClienteId"];
                            var formaPago = ObtenerFormaPagoPorId(formaPagoId, connection);
                            var cliente = ObtenerClientePorId(clienteId, connection);

                            var factura = new Factura(nroFactura, (DateTime)reader["Fecha"], formaPago, cliente);

                            factura.Detalles = ObtenerDetallesPorFacturaId(nroFactura, connection);

                            facturas.Add(factura);
                        }
                    }
                }
            }

            return facturas;
        }

        public void Actualizar(Factura factura)
        {
            using (var connection = new SqlConnection(cnnString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_ActualizarFactura", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Fecha", factura.Fecha);
                    command.Parameters.AddWithValue("@FormaPago", factura.FormaPago.Nombre);
                    command.Parameters.AddWithValue("@Cliente", factura.Cliente);
                    command.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand("DELETE FROM DetalleFactura WHERE NroFactura = @NroFactura", connection))
                {
                    command.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                    command.ExecuteNonQuery();
                }

                foreach (var detalle in factura.Detalles)
                {
                    using (var command = new SqlCommand("sp_InsertarDetalleFactura", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                        command.Parameters.AddWithValue("@Articulo", detalle.Articulo.Nombre);
                        command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        command.Parameters.AddWithValue("@PrecioUnitario", detalle.Articulo.PrecioUnitario);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Eliminar(int nroFactura)
        {
            using (var connection = new SqlConnection(cnnString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_EliminarFactura", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@NroFactura", nroFactura);
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand("DELETE FROM Factura WHERE NroFactura = @NroFactura", connection))
                {
                    command.Parameters.AddWithValue("@NroFactura", nroFactura);
                    command.ExecuteNonQuery();
                }
            }
        }

        private FormaPago ObtenerFormaPagoPorId(int formaPagoId, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM FormaPago WHERE Id = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Id", formaPagoId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new FormaPago((string)reader["Nombre"]);
                    }
                }
            }

            return null;
        }

        private Cliente ObtenerClientePorId(int clienteId, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cliente WHERE Id = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Id", clienteId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cliente((string)reader["Nombre"], (string)reader["Direccion"]);
                    }
                }
            }

            return null;
        }

        private List<DetalleFactura> ObtenerDetallesPorFacturaId(int facturaId, SqlConnection connection)
        {
            var detalles = new List<DetalleFactura>();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM DetalleFactura WHERE FacturaId = @FacturaId", connection))
            {
                cmd.Parameters.AddWithValue("@FacturaId", facturaId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var articuloId = (int)reader["ArticuloId"];
                        var articulo = ObtenerArticuloPorId(articuloId, connection);
                        var cantidad = (int)reader["Cantidad"];

                        detalles.Add(new DetalleFactura(articulo, cantidad));
                    }
                }
            }

            return detalles;
        }

        private Articulo ObtenerArticuloPorId(int articuloId, SqlConnection connection)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Articulo WHERE Id = @Id", connection))
            {
                cmd.Parameters.AddWithValue("@Id", articuloId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Articulo((string)reader["Nombre"], (float)reader["PrecioUnitario"]);
                    }
                }
            }

            return null;
        }
    }
}
