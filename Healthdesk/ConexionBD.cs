using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Healthdesk
{
    internal class ConexionBD
    {
        private static string ConnectionString = "Data Source=DESKTOP-EFB0523\\SQLEXPRESS; Initial Catalog=HEALTHDESK; Integrated Security=true";
        private static SqlConnection con = new SqlConnection(ConnectionString);
         
        public static SqlConnection ObtenerConexion()
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
            }
            return con;
        }
    }


}
