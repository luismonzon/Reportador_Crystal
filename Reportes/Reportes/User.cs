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
    public partial class User : Form
    {

        Conexion conexiones;
        public User()
        {
            InitializeComponent();
            conexiones = new Conexion();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {

        }

        private void LLenar_arbol()
        {
            DataTable Departamentos = conexiones.GetSql("select X.*, Z.nombre_reporte, Z.path_reporte from ("+
	                                                    "select A.id_departamento, A.nombre_departamento, B.id_reporte  from LSA_DEPTOS A Left Join LSA_REPORT_DEP B on A.id_departamento=B.id_departamento )X  Left Join LSA_REPORTES Z on X.id_reporte=Z.id_reporte where id_departamento in(select id_departamento from LSA_USER_DEP where username='"+VariablesGlobales.Usuario+"')");

            string padre_actual = "";
            TreeNode nuevo = new TreeNode();
            foreach (DataRow item in Departamentos.Rows)
            {

                String nuevo_padre = item["id_departamento"].ToString();
                if (padre_actual != "")
                {
                    if (padre_actual != nuevo_padre)
                    {
                        nuevo = new TreeNode(item["nombre_departamento"].ToString());
                        nuevo.Tag = item["id_departamento"].ToString();
                        if (!item["path_reporte"].ToString().Equals(""))
                        {
                            TreeNode reporte = new TreeNode(item["nombre_reporte"].ToString());
                            reporte.Tag = item["path_reporte"].ToString();
                            nuevo.Nodes.Add(reporte);
                        }
                        this.treeView1.Nodes.Add(nuevo);
                        padre_actual = item["id_departamento"].ToString();
                    }
                    else
                    {
                        TreeNode reporte = new TreeNode(item["nombre_reporte"].ToString());
                        reporte.Tag = item["path_reporte"].ToString();
                        nuevo.Nodes.Add(reporte);


                    }
                }
                else
                {
                    padre_actual = item["id_departamento"].ToString();
                    nuevo = new TreeNode(item["nombre_departamento"].ToString());
                    nuevo.Tag = item["id_departamento"].ToString();
                    if (!item["path_reporte"].ToString().Equals(""))
                    {
                        TreeNode reporte = new TreeNode(item["nombre_reporte"].ToString());
                        reporte.Tag = item["path_reporte"].ToString();
                        nuevo.Nodes.Add(reporte);
                    }
                    this.treeView1.Nodes.Add(nuevo);
                }




            }


        }
    }
}
