using Practica02.Controllers;
using Practica02.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly DataHelper _helper;

        public ArticuloRepository(DataHelper helper)
        {
            _helper = helper;
        }

        public void AgregarArticulo(Articulo articulo)
        {
            using (var cnn = _helper.GetConnection())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SP_Agregar_Articulo", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Nombre", articulo.Nombre));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", articulo.PrecioUnitario));

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }

        public Articulo ConsultarArticulo(int id)
        {
            using (var cnn = _helper.GetConnection())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SP_Consultar_Articulo", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", id));

                using (SqlDataReader reader = cmd.ExecuteReader())
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

        public IEnumerable<Articulo> ListarArticulo()
        {
            List<Articulo> articulos = new List<Articulo>();

            using (var cnn = _helper.GetConnection())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SP_Listar_Articulos", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        articulos.Add(new Articulo
                        {
                            Id = (int)reader["Id"],
                            Nombre = reader["Nombre"].ToString(),
                            PrecioUnitario = (float)reader["PrecioUnitario"]
                        });
                    }
                }
                cnn.Close();
            }
            return articulos;
        }

        public void EditarArticulo(Articulo articulo)
        {
            using (var cnn = _helper.GetConnection())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SP_Editar_Articulo", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", articulo.Id));
                cmd.Parameters.Add(new SqlParameter("@Nombre", articulo.Nombre));
                cmd.Parameters.Add(new SqlParameter("@PrecioUnitario", articulo.PrecioUnitario));

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
        }

        public void EliminarArticulo(int id)
        {
            using (var cnn = _helper.GetConnection())
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("SP_Eliminar_Articulo", cnn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", id));

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
    }
}
