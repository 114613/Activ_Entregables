using Practica03Library.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Data
{
    public class FacturaRepository
    {
        public void RegistrarFactura(Factura factura)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Registrar_Factura", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@FormaPago", factura.FormaPago.Nombre);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);

                
                cmd.ExecuteNonQuery();

                foreach(var detalle in factura.Detalle)
                {
                    RegistrarDetalleFactura(factura.NroFactura, detalle);
                }
            }
        }

        public void RegistrarDetalleFactura(int nroFactura, DetalleFactura detalle)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Registrar_DetalleFactura", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);
                cmd.Parameters.AddWithValue("@ArticuloId", detalle.Articulo.Id);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

                
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public Factura ConsultarFactura(int nroFactura)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Consultar_Factura", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Factura
                        {
                            NroFactura = (int)reader["NroFactura"],
                            Fecha = (DateTime)reader["Fecha"],
                            Cliente = reader["Cliente"].ToString(),
                            FormaPago = new FormaPago { Nombre = reader["FormaPago"].ToString() },
                            Detalle = ConsultarDetallesFactura(nroFactura)
                        };
                    }
                }
                cnn.Close();
            }
            return null;
        }

        private List<DetalleFactura> ConsultarDetallesFactura(int nroFactura)
        {
            var detalles = new List<DetalleFactura>();
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();

                var cmd = new SqlCommand("SP_Consultar_DetallesFactura", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NroFactura", nroFactura);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var detalle = new DetalleFactura
                        {
                            Articulo = new Articulo
                            {
                                Id = (int)reader["ArticuloId"],
                                Nombre = reader["Nombre"].ToString(),
                                PrecioUnitario = (float)reader["PrecioUnitario"]
                            },
                            Cantidad = (int)reader["Cantidad"]
                        };
                        detalles.Add(detalle);
                    }
                }
                cnn.Close();
            }
            return detalles;
        }

        public List<Factura> ConsultarFacturas(DateTime? fecha, string formaPago)
        {
            var facturas = new List<Factura>();
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();

                var cmd = new SqlCommand("SP_Consultar_Facturas", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Fecha", fecha.HasValue ? (object)fecha.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@FormaPago", formaPago ?? (object)DBNull.Value);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var factura = new Factura
                        {
                            NroFactura = (int)reader["NroFactura"],
                            Fecha = (DateTime)reader["Fecha"],
                            Cliente = reader["Cliente"].ToString(),
                            FormaPago = new FormaPago { Nombre = reader["FormaPago"].ToString() }
                        };
                        facturas.Add(factura);
                    }
                }
                cnn.Close();
            }
            return facturas;
        }

        public void EditarFactura(Factura factura)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Editar_Factura", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@NroFactura", factura.NroFactura);
                cmd.Parameters.AddWithValue("@Fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("@FormaPago", factura.FormaPago.Nombre);
                cmd.Parameters.AddWithValue("@Cliente", factura.Cliente);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
