using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Data
{
    public class DataHelper
    {
        private readonly string _cnnString = @"Data Source=Express;Initial Catalog=Practica02;Integrated Security=True";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_cnnString);
        }
    }
}
