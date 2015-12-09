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
    public partial class Crear_Departamento : Form
    {
        Conexion conexion;
        String id;
        BindingSource Departamentos;
        int operacion;
        public Crear_Departamento()
        {
            InitializeComponent();
            //conexion BDD
            conexion = new Conexion();
            Habilitar(false);
            inicializar();

            
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion nueva = new Conexion();
            if (operacion == 0) {
                DataRowView data = (DataRowView)bindingNavigator1.BindingSource.Current;
                nueva.GetSql("Update LSA_DEPTOS set nombre_departamento='"+this.textBox1.Text+"' where id_departamento="+data["id_departamento"].ToString()+"");
            }
            else {
                nueva.Insertar_Dep(this.textBox1.Text.ToString());
                
            }

            Habilitar(false);
            inicializar();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            this.operacion=1;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

            DataRowView data =(DataRowView)bindingNavigator1.BindingSource.Current;
            conexion.Delete_Departamento(data["id_departamento"].ToString(),"Eliminar_Departamento", "@dep");
            inicializar();
        }

        private void inicializar() {

            DataTable tabla = conexion.GetSql("Select * from LSA_DEPTOS");
            Departamentos = new BindingSource();
            Departamentos.DataSource = tabla;

            this.bindingNavigator1.BindingSource = Departamentos;
            this.textBox1.DataBindings.Clear();
            this.textBox1.DataBindings.Add(new Binding("Text", Departamentos, "nombre_departamento", true));

        }

       

        private void Habilitar(bool estado) {
            this.textBox1.Enabled = estado;
            this.button1.Enabled = estado;
            this.button2.Enabled = estado;
        
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Habilitar(true);
            this.operacion = 0;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
