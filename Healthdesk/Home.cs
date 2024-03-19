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
    public partial class Home : Form
    {
        public Home(string user_medico)
        {
            InitializeComponent();

            Dashboard dashadmin = new Dashboard();
            dashadmin.TopLevel = false;
            panelContenedor.Controls.Add(dashadmin);
            dashadmin.BringToFront();
            dashadmin.Show();
            lblIDMEDICO.Text = user_medico;
  
            //INFORMACION MEDICO
            SqlConnection con = ConexionBD.ObtenerConexion();
            {
             
                //NOMBRE
                SqlCommand cmdNombre = new SqlCommand("SELECT NOMBRE FROM MEDICOS WHERE IDMEDICO = @idMedico", con);
                cmdNombre.Parameters.AddWithValue("@idMedico", user_medico);

                string nombre = (string)cmdNombre.ExecuteScalar();
               
                //APELLIDO
                SqlCommand cmdApellido = new SqlCommand("SELECT APELLIDOS FROM MEDICOS WHERE IDMEDICO = @idMedico", con);
                cmdApellido.Parameters.AddWithValue("@idMedico", user_medico);

                string apellido = (string)cmdApellido.ExecuteScalar();

                //CEDULA
                SqlCommand cmdCedula = new SqlCommand("SELECT CEDULA FROM MEDICOS WHERE IDMEDICO = @idMedico", con);
                cmdCedula.Parameters.AddWithValue("@idMedico", user_medico);

                lblCedula.Text="Ced.Prof. " + (string)cmdCedula.ExecuteScalar();


                //ESPECIALIDAD
                SqlCommand cmdEspecialidad = new SqlCommand("SELECT ESPECIALIDAD FROM MEDICOS WHERE IDMEDICO = @idMedico", con);
                cmdEspecialidad.Parameters.AddWithValue("@idMedico", user_medico);

                lblEspe.Text = (string)cmdEspecialidad.ExecuteScalar();


                lblMedico.Text = nombre + " " + apellido;
                con.Close();

            }

        }

        private void Cerrar_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }

        private void cuenta_btn_Click(object sender, EventArgs e)
        {
            int idMedico = int.Parse(lblIDMEDICO.Text);
            PerfilMed miperfil = new PerfilMed(idMedico);
            miperfil.TopLevel = false;
            panelContenedor.Controls.Add(miperfil);
            miperfil.BringToFront();
            miperfil.Show();
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            Dashboard dashadmin = new Dashboard();
            dashadmin.TopLevel = false;
            panelContenedor.Controls.Add(dashadmin);
            dashadmin.BringToFront();
            dashadmin.Show();
        }

        private void Pacientes_btn_Click(object sender, EventArgs e)
        {
            Pacientes pacientesLista= new Pacientes();
            pacientesLista.TopLevel = false;
            panelContenedor.Controls.Add(pacientesLista);
            pacientesLista.BringToFront();
            pacientesLista.Show();
        }

        private void citas_btn_Click(object sender, EventArgs e)
        {
            int idMedico = int.Parse(lblIDMEDICO.Text);
            RevisarCitas citasLista = new RevisarCitas(idMedico);
            citasLista.TopLevel = false;
            panelContenedor.Controls.Add(citasLista);
            citasLista.BringToFront();
            citasLista.Show();
        }

        private void Medicos_btn_Click(object sender, EventArgs e)
        {
            int idMedico = int.Parse(lblIDMEDICO.Text);
            HISTORIAL citashisto = new HISTORIAL(idMedico);
            citashisto.TopLevel = false;
            panelContenedor.Controls.Add(citashisto);
            citashisto.BringToFront();
            citashisto.Show();
        }
    }
}
