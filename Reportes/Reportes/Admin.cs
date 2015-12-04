using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reportes
{
    public partial class Admin : Form
    {
        Conexion conexiones;
        public Admin()
        {
            InitializeComponent();
            conexiones = new Conexion();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LLenar_arbol() {
            DataTable Departamentos = conexiones.GetSql("select X.*, Z.nombre_reporte, Z.path_reporte from (" +
                                                        " select A.id_departamento, A.nombre_departamento, B.id_reporte  from LSA_DEPTOS A Left Join LSA_REPORT_DEP B on A.id_departamento=B.id_departamento )X  Left Join LSA_REPORTES Z on X.id_reporte=Z.id_reporte ");

            string padre_actual = "";
            TreeNode nuevo = new TreeNode();
            foreach (DataRow item in Departamentos.Rows)
            {

                String nuevo_padre = item["id_departamento"].ToString();
                if(padre_actual!=""){
                    if (padre_actual != nuevo_padre)
                    {
                        nuevo = new TreeNode(item["nombre_departamento"].ToString());
                        this.treeView1.Nodes.Add(nuevo);
                    }
                    else
                    {
                        
                        nuevo.Nodes.Add(new TreeNode(item["path_reporte"].ToString()));

                    }
                }
                else {
                    padre_actual = item["id_departamento"].ToString();
                    nuevo = new TreeNode(item["nombre_departamento"].ToString());
                    this.treeView1.Nodes.Add(nuevo);
                }  
             
              
    

            }                                                


        }

        private void Admin_Load(object sender, EventArgs e)
        {
            LLenar_arbol();
        }
    }
}
