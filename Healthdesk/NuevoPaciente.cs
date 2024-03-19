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
    public partial class NuevoPaciente : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();
        public NuevoPaciente()
        {
            InitializeComponent();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellidos.Text) && !string.IsNullOrEmpty(txtCorreo.Text) && !string.IsNullOrEmpty(txtFecha.Text) && !string.IsNullOrEmpty(txtTelefono.Text))
                {


                    if (!PacienteExistente(con, txtNombre.Text, txtApellidos.Text, txtFecha.Text, txtCorreo.Text, txtTelefono.Text))
                    {
                        string sql = "INSERT INTO PACIENTES (NOMBRE, APELLIDOS, FECHA_NAC, TELEFONO, CORREO_ELECTRONICO) VALUES (@Nombre, @Apellidos, @FechaNac, @Email, @Telefono)";
                        SqlCommand comando = new SqlCommand(sql, con);
                        comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        comando.Parameters.AddWithValue("@Apellidos", txtApellidos.Text);
                        comando.Parameters.AddWithValue("@FechaNac", txtFecha.Text);
                        comando.Parameters.AddWithValue("@Email", txtCorreo.Text);
                        comando.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Se ha agregado al paciente");
                        this.Close();
                        Pacientes formPacientes = (Pacientes)Application.OpenForms["Pacientes"];
                        formPacientes?.loaddata();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un paciente con estos datos en la base de datos.");
                    }

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Datos vacíos o incorrectos. Revise la información.");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al agregar al paciente: " + ex.Message);
                con.Close();
            }
        }


        private bool PacienteExistente(SqlConnection con, string nombre, string apellidos, string fechaNac, string email, string telefono)
        {
            string sql = "SELECT COUNT(*) FROM PACIENTES WHERE NOMBRE = @Nombre AND APELLIDOS = @Apellidos AND FECHA_NAC = @FechaNac AND CORREO_ELECTRONICO = @Email AND TELEFONO = @Telefono";
            SqlCommand comando = new SqlCommand(sql, con);
            comando.Parameters.AddWithValue("@Nombre", nombre);
            comando.Parameters.AddWithValue("@Apellidos", apellidos);
            comando.Parameters.AddWithValue("@FechaNac", fechaNac);
            comando.Parameters.AddWithValue("@Email", email);
            comando.Parameters.AddWithValue("@Telefono", telefono);
            int count = (int)comando.ExecuteScalar();
            return count > 0;
        }
    }
}
