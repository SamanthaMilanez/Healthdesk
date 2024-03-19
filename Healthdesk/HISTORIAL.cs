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
    public partial class HISTORIAL : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();
        public HISTORIAL(int idmed)
        {
            InitializeComponent();


            dgvConsulta.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvConsulta.EnableHeadersVisualStyles = false;

            string consulta = "SELECT C.IDCITA AS CITA, P.NOMBRE AS NOMBRE, P.APELLIDOS AS APELLIDO, C.HORA, C.FECHA,T.TIPO AS TIPO FROM CITAS C JOIN PACIENTES P ON C.IDPACIENTE = P.IDPACIENTE JOIN TIPO_CITAS T ON C.ID_TIPOCITA = T.ID_TIPO WHERE C.ESTADO = 'Atendida' AND C.IDMEDICO = @IDMEDICO; ";
            SqlCommand cmd = new SqlCommand(consulta, con);


            cmd.Parameters.AddWithValue("@IDMEDICO", idmed);

            // Crear un SqlDataAdapter para ejecutar el comando y llenar el DataTable
            SqlDataAdapter adaptador1 = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adaptador1.Fill(dt);

            dgvConsulta.DataSource = dt;


            con.Close();

        }
        
    }
}
