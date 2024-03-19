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
    public partial class Pacientes : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();

        public Pacientes()
        {
            InitializeComponent();
            loaddata();
        }

        public void loaddata()
        {


            dgvPacientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPacientes.EnableHeadersVisualStyles = false;

            string consulta = "SELECT IDPACIENTE AS EXPEDIENTE, NOMBRE, APELLIDOS ,FECHA_NAC AS NACIMIENTO, TELEFONO, CORREO_ELECTRONICO AS EMAIL FROM PACIENTES";
            SqlDataAdapter adaptador1 = new SqlDataAdapter(consulta, con);
            DataTable dt = new DataTable();
            adaptador1.Fill(dt);
            dgvPacientes.DataSource = dt;

            con.Close();
        }

        private void agregar_btn_Click(object sender, EventArgs e)
        {
            new NuevoPaciente().ShowDialog();
        }

        private void dgvPacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int IDPaciente;
                int rowindex = dgvPacientes.CurrentCell.RowIndex;

                IDPaciente = Convert.ToInt32(dgvPacientes.Rows[rowindex].Cells["EXPEDIENTE"].Value.ToString());

                //Editar datos
                if (dgvPacientes.Columns[e.ColumnIndex].Name == "Editar")
                {
                    new EditarPaciente(IDPaciente).ShowDialog();
                }
                else if (dgvPacientes.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM PACIENTES WHERE IDPACIENTE = @IDPaciente", con);
                        cmd.Parameters.AddWithValue("@IDPaciente", IDPaciente);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("El registro ha sido borrado");

                        string consulta = "SELECT * FROM PACIENTES";
                        SqlDataAdapter adaptador1 = new SqlDataAdapter(consulta, con);
                        DataTable dt = new DataTable();
                        adaptador1.Fill(dt);
                        dgvPacientes.DataSource = dt;
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el registro: " + ex.Message);
            }
        }
    }
}

  

