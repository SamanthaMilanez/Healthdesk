using System.Data.SqlClient;
using System.Data;


namespace Healthdesk
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            txtContraseña.UseSystemPasswordChar = true;
        }

        //CONEXION ABIERTA
        private SqlConnection con = ConexionBD.ObtenerConexion();

        private void close_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //AUTENTICACION 
        private void login_btn_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtContraseña.Text;

            try
            {
                SqlCommand cmd = new SqlCommand("select IDMEDICO from MEDICOS where USUARIO= @usuario AND CONTRASENA =@pas", con);
                cmd.Parameters.AddWithValue("usuario", username);
                cmd.Parameters.AddWithValue("pas", password);
                SqlDataAdapter myadapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                myadapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string valor = dt.Rows[0]["IDMEDICO"].ToString();
                    new Home(valor).Show();
                    this.Hide();
                } 
                else
                {
                    MessageBox.Show("USUARIO O CONTRASENA INCORRECTOS","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }    
            }
            catch 
            {
                MessageBox.Show("Error");
                
            }
            finally
            {
                con.Close();
            }
            
           
        }

        //MOSTRAR CONTRASENA
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtContraseña.UseSystemPasswordChar = false;
            }
            else
            {
                txtContraseña.UseSystemPasswordChar = true;
            }
        }

        //MINIMIZAR VENTANA
        private void minimizar_btn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

       //VENTANA MOVIBLE
        Point lastPoint;
       
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}