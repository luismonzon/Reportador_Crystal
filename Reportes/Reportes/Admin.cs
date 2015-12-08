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
        Form1 login;
        public Admin(Form1 login)
        {
            InitializeComponent();
            //Inicializar todos los componentes
            this.login = login;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Purple;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            conexiones = new Conexion();
            this.treeView1.AllowDrop = true;
            LLenar_arbol();
            Llenar_Reportes();
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
                else {
                    padre_actual = item["id_departamento"].ToString();
                    nuevo = new TreeNode(item["nombre_departamento"].ToString());
                    nuevo.Tag = item["id_departamento"].ToString();
                    if(!item["path_reporte"].ToString().Equals("")){
                        TreeNode reporte = new TreeNode(item["nombre_reporte"].ToString());
                        reporte.Tag = item["path_reporte"].ToString();
                        nuevo.Nodes.Add(reporte);
                    }
                    this.treeView1.Nodes.Add(nuevo);
                }  
             
              
    

            }                                                


        }
        private void Llenar_Reportes()
        {
            DataTable Departamentos = conexiones.GetSql("select id_reporte, nombre_reporte, path_reporte from LSA_REPORTES");

            this.dataGridView1.DataSource = Departamentos;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 20;
            
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column.Width = 110;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 300;
            dataGridView1.ReadOnly = true;
           
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                // Look for a file extension.
                if (e.Node.Nodes.Count==0)
                    VariablesGlobales.ShowReport(e.Node.Tag.ToString());
            }
            // If the file is not found, handle the exception and inform the user.
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("File not found.");
            }
        }

  


    

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
                login.Close();
              
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            dataGridView1.DoDragDrop(dataGridView1.SelectedRows, DragDropEffects.Move);
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            

            if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {
                Point pt;
                TreeNode destinationNode;
                pt = treeView1.PointToClient(new Point(e.X, e.Y));
                destinationNode = treeView1.GetNodeAt(pt);
                if(destinationNode.Parent==null){
                DataGridViewSelectedRowCollection rowToMove = e.Data.GetData(typeof(DataGridViewSelectedRowCollection)) as DataGridViewSelectedRowCollection;

                foreach (DataGridViewRow row in rowToMove)
                {
                    //insert base de datos
                    string val = conexiones.Insertar_rep_dep(row.Cells[0].Value.ToString(), destinationNode.Tag.ToString());
                    if (val != "1")
                    {
                        MessageBox.Show("Error al asignar Departamento/Reporte");
                    }
                    else
                    {
                        TreeNode nuevo = new TreeNode(row.Cells[1].Value.ToString());
                        nuevo.Tag = row.Cells[2].ToString();
                        destinationNode.Nodes.Add(nuevo);
                    }
                   
                }

                }
            }
            
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
