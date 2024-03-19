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
    public partial class EditarPaciente : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();

        public EditarPaciente(int idpaciente)
        {
            InitializeComponent();

            SqlCommand cmdPaciente = new SqlCommand("SELECT IDPACIENTE,NOMBRE, APELLIDOS, FECHA_NAC, TELEFONO, CORREO_ELECTRONICO FROM PACIENTES WHERE IDPACIENTE = @idpaciente", con);
            cmdPaciente.Parameters.AddWithValue("@idpaciente", idpaciente);

            using (SqlDataReader reader = cmdPaciente.ExecuteReader())
            {
                if (reader.Read())
                {
                    txtIDPaciente.Text = reader.GetInt32(0).ToString();
                    txtNombre.Text = reader.GetString(1);
                    txtApellidos.Text = reader.GetString(2);
                    txtFecha.Text = reader.GetDateTime(3).ToString("yyyy-MM-dd");
                    txtTelefono.Text = reader.GetString(4);
                    txtEmail.Text = reader.IsDBNull(5) ? "" : reader.GetString(5);
                }
                else
                {
                    // Manejar el caso en el que no se encuentre ningún paciente con el ID especificado
                }
            }

            con.Close();
           
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                int IDPac = int.Parse(txtIDPaciente.Text);

                SqlCommand cmd = new SqlCommand("UPDATE PACIENTES SET NOMBRE = @nombre, APELLIDOS = @apellidos, CORREO_ELECTRONICO = @Email, TELEFONO = @telefono, FECHA_NAC = @nacimiento WHERE IDPaciente = @IDPac", con);
                cmd.Parameters.AddWithValue("@IDPac", IDPac);
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellidos", txtApellidos.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@nacimiento", txtFecha.Text);
                

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Se han actualizado los datos");

                Pacientes formPacientes = (Pacientes)Application.OpenForms["Pacientes"];
                formPacientes?.loaddata();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        
    }
}
