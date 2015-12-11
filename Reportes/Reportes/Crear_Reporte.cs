using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Reportes
{
    public partial class Crear_Reporte : Form
    {

        Conexion conexion;
        String id;
        BindingSource Reportes;
        int operacion;

        public Crear_Reporte()
        {
            InitializeComponent();
            //conexion BDD
            conexion = new Conexion();
            Habilitar(false);
            inicializar();
        }

   
        
        private void inicializar() {

            DataTable tabla = conexion.GetSql("Select * from LSA_REPORTES");
            Reportes = new BindingSource();
            Reportes.DataSource = tabla;

            this.bindingNavigator1.BindingSource = Reportes;
            this.textBox1.DataBindings.Clear();
            this.textBox2.DataBindings.Clear();

            this.textBox1.DataBindings.Add(new Binding("Text", Reportes, "nombre_reporte", true));
            this.textBox2.DataBindings.Add(new Binding("Text", Reportes, "path_reporte", true));

        }

       

        private void Habilitar(bool estado) {
            this.textBox1.Enabled = estado;
            this.textBox2.Enabled = estado;
            this.button1.Enabled = estado;
            this.button2.Enabled = estado;
            this.button3.Enabled = estado;
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {

                this.textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Conexion nueva = new Conexion();
            if (operacion == 0)
            {
                DataRowView data = (DataRowView)bindingNavigator1.BindingSource.Current;
                nueva.GetSql("Update LSA_REPORTES set nombre_reporte='" + this.textBox1.Text + "', path_reporte='" + textBox2.Text + "' where id_reporte=" + data["id_reporte"].ToString() + "");
            }
            else
            {
                nueva.Insertar_Reporte(this.textBox1.Text.ToString(),this.textBox2.Text);

            }

            Habilitar(false);
            inicializar();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Habilitar(true);
            this.operacion = 0;

        }

        private void bindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {
            DataRowView data = (DataRowView)bindingNavigator1.BindingSource.Current;
            conexion.Delete_Departamento(data["id_reporte"].ToString(), "Eliminar_Reporte", "@rep");
            inicializar();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            this.operacion = 1;
        }
    }
}
