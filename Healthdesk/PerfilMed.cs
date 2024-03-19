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
    public partial class PerfilMed : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();

        public PerfilMed(int medico)
        {
            InitializeComponent();


            {
                SqlCommand cmd = new SqlCommand("SELECT NOMBRE, APELLIDOS, CEDULA,CENTRO_ESTUDIOS, ESPECIALIDAD, FECHA_NAC, TELEFONO, CORREO_ELECTRONICO FROM MEDICOS WHERE IDMEDICO = @idMedico", con);
                cmd.Parameters.AddWithValue("@idMedico", medico);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtNombre.Text = reader["NOMBRE"].ToString();
                    txtApellido.Text = reader["APELLIDOS"].ToString();
                    txtCentroEstudios.Text = reader["CENTRO_ESTUDIOS"].ToString();
                    txtEspecialidad.Text = reader["ESPECIALIDAD"].ToString();
                    txtCedula.Text = reader["CEDULA"].ToString();
                    txtFeNac.Text = ((DateTime)reader["FECHA_NAC"]).ToString("dd/MM/yyyy");
                    txtTelefono.Text = reader["TELEFONO"].ToString();
                    txtCorreo.Text = reader["CORREO_ELECTRONICO"].ToString();
                }
                con.Close();
            }

        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
               
                  
                con.Open();
                    string cedula = txtCedula.Text;

                    SqlCommand cmd = new SqlCommand("UPDATE MEDICOS SET CORREO_ELECTRONICO = @Correo, TELEFONO = @Telefono WHERE CEDULA = @Cedula", con);
                    cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    // Ejecutar el comando SQL
                    int filasActualizadas = cmd.ExecuteNonQuery();

                    if (filasActualizadas > 0)
                    {
                        MessageBox.Show("Los datos del médico han sido actualizados correctamente.");

                        // Refrescar los datos en los TextBox
                        CargarDatosMedico(cedula);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún médico con la cédula proporcionada.");
                    }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos del médico: " + ex.Message);
            }
        }

        private void CargarDatosMedico(string cedula)
        {
            try
            {
              

                    SqlCommand cmd = new SqlCommand("SELECT CORREO_ELECTRONICO, TELEFONO FROM MEDICOS WHERE CEDULA = @Cedula", con);
                    cmd.Parameters.AddWithValue("@Cedula", cedula);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Mostrar los datos en los TextBox
                        txtCorreo.Text = reader["CORREO_ELECTRONICO"].ToString();
                        txtTelefono.Text = reader["TELEFONO"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún médico con la cédula proporcionada.");
                    }
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos del médico: " + ex.Message);
            }

        }
    }
}