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
    public partial class Crear_Usuario : Form
    {
        Conexion conexion;
        String id;
        BindingSource Usuarios;
        int operacion;

        public Crear_Usuario()
        {
            InitializeComponent();

            //conexion BDD
            conexion = new Conexion();
            Habilitar(false);
            inicializar();
        }
        
       
        
    

        private void inicializar() {

            DataTable tabla = conexion.GetSql("Select * from LSA_USUARIOS");
            Usuarios = new BindingSource();
            Usuarios.DataSource = tabla;

            this.bindingNavigator1.BindingSource = Usuarios;
            this.textBox1.DataBindings.Clear();
            this.textBox3.DataBindings.Clear();
            this.comboBox1.DataBindings.Clear();
       
            this.textBox1.DataBindings.Add(new Binding("Text", Usuarios, "username", true));
            this.comboBox1.DataBindings.Add(new Binding("SelectedItem", Usuarios, "rol", true));
            comboBox1.Items.Clear();
            this.comboBox1.Items.Insert(0, "administrador");
            this.comboBox1.Items.Insert(1, "usuario");

        }

       

        private void Habilitar(bool estado) {
            this.textBox1.Enabled = estado;
            this.button1.Enabled = estado;
            this.button2.Enabled = estado;
            this.textBox3.Enabled = estado;
            this.comboBox1.Enabled = estado;

            comboBox1.Items.Clear();
            this.comboBox1.Items.Insert(0, "administrador");
            this.comboBox1.Items.Insert(1, "usuario");
          
        }

       

        private void button1_Click_1(object sender, EventArgs e)
        {
            Conexion nueva = new Conexion();
            if (operacion == 0)
            {
                DataRowView data = (DataRowView)bindingNavigator1.BindingSource.Current;
                string sql = "Update LSA_USUARIOS set username='" + this.textBox1.Text + "'";
                if(this.textBox3.Text!=""){
                    sql += ", password=pwdencrypt('" + textBox3.Text + "') ";
                }
                if (!this.comboBox1.SelectedItem.Equals("")){
                    sql += ",rol='" + this.comboBox1.SelectedItem + "' ";
                }
                sql+=" where username='" + data["username"].ToString()+"'";
                nueva.GetSql(sql);
                this.textBox3.Clear();
            }
            else
            {
                nueva.Insertar_Usuario(this.textBox1.Text,this.textBox3.Text);

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
            conexion.Delete_Departamento(data["username"].ToString(), "Eliminar_Usuario", "@user");
            inicializar();
        }

        private void bindingNavigatorAddNewItem_Click_1(object sender, EventArgs e)
        {
            Habilitar(true);
            this.operacion=1;
            comboBox1.Items.Clear();
            this.comboBox1.Items.Insert(0, "administrador");
            this.comboBox1.Items.Insert(1, "usuario");
            
        }
        private void Limpiar() {
            this.textBox1.Text = "";
            this.textBox3.Text = "";

        }
    }
}
