using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reportes
{
    public partial class Depa_User : Form
    {
        Conexion conexiones;
        Form1 login;
        public Depa_User()
        {
            InitializeComponent();
        }


        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VariablesGlobales.Usuario = "";
            VariablesGlobales.Password = "";
            login.Show();
            this.Close();
        }

        private void asignarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Crear_Departamento nuevo = new Crear_Departamento();
            nuevo.ShowDialog();
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
