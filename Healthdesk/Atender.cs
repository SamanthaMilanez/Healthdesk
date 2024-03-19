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
    public partial class Atender : Form
    {
        SqlConnection con = ConexionBD.ObtenerConexion();

        public Atender(int idcita)
        {
            InitializeComponent();

            string nombreM, apellidosM, nombreP, apellidosP;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT M.NOMBRE AS NombreMedico, M.APELLIDOS AS ApellidosMedico,M.ESPECIALIDAD AS EspecialidadMedico,M.CEDULA AS CEDULA,M.CENTRO_ESTUDIOS AS CentroUniversitario,P.NOMBRE AS NombrePaciente,P.APELLIDOS AS ApellidosPaciente,P.IDPACIENTE AS IdPaciente, P.FECHA_NAC AS FECHA, C.IDCITA AS IdCita,C.FECHA AS FechaCita, C.OBSERVACIONES FROM CITAS C JOIN PACIENTES P ON C.IDPACIENTE = P.IDPACIENTE JOIN MEDICOS M ON C.IDMEDICO = M.IDMEDICO WHERE C.IDCITA = @idcita", con);
                cmd.Parameters.AddWithValue("@idcita", idcita);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Mostrar los datos en los TextBox
                   nombreM = reader["NombreMedico"].ToString();
                   apellidosM = reader["ApellidosMedico"].ToString();
                   lblEspe.Text = reader["EspecialidadMedico"].ToString();
                   lblCentro.Text = reader["CentroUniversitario"].ToString();
                   lblCed.Text = "Ced.Prof." + reader["CEDULA"].ToString();
                   txtObservaciones.Text = reader["OBSERVACIONES"].ToString();
                   nombreP = reader["NombrePaciente"].ToString();
                   apellidosP = reader["ApellidosPaciente"].ToString();
                   txtPaciente.Text = reader["IdCita"].ToString();
                   lblFecha.Text = ((DateTime)reader["FechaCita"]).ToString("dd/MM/yyyy");
                    txtEdad.Text = reader["FECHA"].ToString();
                    txtPaciente.Text = reader["IDPACIENTE"].ToString();
                    lblDoctor.Text = "Dr. " + nombreM + " " + apellidosM;
                    txtName.Text = nombreP + " " + apellidosP;
                    lblIDCita.Text = Convert.ToString(idcita);

                    // Obtener la fecha de nacimiento del TextBox
                    DateTime fechaNacimiento = DateTime.Parse(txtEdad.Text);

                    // Calcular la diferencia de tiempo entre la fecha actual y la fecha de nacimiento
                    TimeSpan diferencia = DateTime.Today - fechaNacimiento;

                    // Calcular la edad en años
                    int edad = (int)(diferencia.Days / 365.25);

                    // Asignar la edad al TextBox
                    txtEdad.Text = edad.ToString() + " años";




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
