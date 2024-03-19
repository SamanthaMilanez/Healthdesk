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
    public partial class Citas : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();
        public Citas(int idmed)
        {
            InitializeComponent();
           
          

            dgvCitas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCitas.EnableHeadersVisualStyles = false;

            string consulta = "SELECT C.IDCITA AS CITA, P.NOMBRE AS NOMBRE, P.APELLIDOS AS APELLIDO, C.HORA, C.ESTADO AS ESTADO,C.FECHA,T.TIPO AS TIPO FROM CITAS C JOIN PACIENTES P ON C.IDPACIENTE = P.IDPACIENTE JOIN TIPO_CITAS T ON C.ID_TIPOCITA = T.ID_TIPO WHERE C.ESTADO = 'Pendiente' AND C.IDMEDICO = @IDMEDICO; ";
            SqlCommand cmd = new SqlCommand(consulta, con);


            cmd.Parameters.AddWithValue("@IDMEDICO", idmed);

            // Crear un SqlDataAdapter para ejecutar el comando y llenar el DataTable
            SqlDataAdapter adaptador1 = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptador1.Fill(dt);

            dgvCitas.DataSource = dt;


            con.Close();



        }

        

      
    }

   
}
