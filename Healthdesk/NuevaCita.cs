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
    public partial class NuevaCita : Form
    {

        SqlConnection con = ConexionBD.ObtenerConexion();

        public NuevaCita(int idmed)
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
            labelMEDICO.Text = idmed.ToString();
        }

        private void cmdFiltrar_Click(object sender, EventArgs e)
        {

            
            try
            {
                string valor = txtApp.Text;

                // Construir la consulta SQL para buscar por IDPaciente
                string consulta = "SELECT IDPACIENTE, NOMBRE, APELLIDOS FROM PACIENTES WHERE IDPACIENTE = @Valor";

               

                    SqlCommand cmd = new SqlCommand(consulta, con);
                    cmd.Parameters.AddWithValue("@Valor", valor);

                    // Ejecutar la consulta y obtener los resultados
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Asignar los valores a los TextBoxes
                        txtIDPaciente.Text = reader["IDPACIENTE"].ToString();
                        txtNombre.Text = reader["NOMBRE"].ToString();
                        txtApellidos.Text = reader["APELLIDOS"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún paciente con el ID proporcionado.");
                    }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar paciente: " + ex.Message);
            }
        }

       

private void save_btn_Click(object sender, EventArgs e)
        {
            int idmedico = int.Parse(labelMEDICO.Text);
           
            try
            {
              
                    con.Open(); 

                    string sql = @"INSERT INTO CITAS (IDPACIENTE, IDMEDICO, FECHA, HORA, ESTADO, ID_TIPOCITA, OBSERVACIONES)
                           VALUES (@IdPaciente, @IdMedico, @Fecha, @Hora, 'pendiente', 1)";

                    // Crea el comando SQL y asigna los parámetros
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@IdPaciente", txtIDPaciente.Text);
                    cmd.Parameters.AddWithValue("@IdMedico", idmedico);
                    cmd.Parameters.AddWithValue("@Fecha", dateTimePicker1);
                    cmd.Parameters.AddWithValue("@Hora", comboHorario.SelectedValue);   
              
               

                    // Ejecuta el comando SQL
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("La cita ha sido agregada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar la cita.");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la cita: " + ex.Message);
            }
        }
    
    } 
}
