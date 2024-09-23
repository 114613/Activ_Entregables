using Practica03Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Data
{
    public class ArticuloRepository
    {
        public void AgregarArticulo(Articulo articulo)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Agregar_Articulo", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@PrecioUnitario", articulo.PrecioUnitario);

                
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public Articulo ConsultarArticulo(int id)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Consultar_Articulo", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Articulo
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            PrecioUnitario = (float)reader["PrecioUnitario"]
                        };
                    }
                }
                cnn.Close();
            }
            return null;
        }

        public void EditarArticulo(Articulo articulo)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("sp_EditarArticulo", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", articulo.Id);
                cmd.Parameters.AddWithValue("@Nombre", articulo.Nombre);
                cmd.Parameters.AddWithValue("@PrecioUnitario", articulo.PrecioUnitario);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public void RegistrarBajaArticulo(int id)
        {
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Registrar_Baja_Articulo", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public List<Articulo> ListarArticulos()
        {
            var articulos = new List<Articulo>();
            using (var cnn = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                cnn.Open();
                var cmd = new SqlCommand("SP_Listar_Articulos", cnn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var articulo = new Articulo
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            PrecioUnitario = (float)reader["PrecioUnitario"]
                        };
                        articulos.Add(articulo);
                    }
                }
                cnn.Close();
            }
            return articulos;
        }
    }
}
