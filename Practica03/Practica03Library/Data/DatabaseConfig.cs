using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica03Library.Data
{
    public static class DatabaseConfig
    {
        public static string ConnectionString { get; } = @"Data Source=.\\SQLEXPRESS;Initial Catalog=Practica03;Integrated Security=True;Trust Server Certificate=True";
    }
}
