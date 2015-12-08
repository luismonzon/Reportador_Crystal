using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace Reportes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Conexion log = new Conexion();
            this.contraseña.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Conexion log = new Conexion();
        
            DataTable res =log.GetSql("SELECT username,password"+
                                      " FROM LSA_USUARIOS"+
                                      " WHERE PWDCOMPARE(N'"+this.contraseña.Text.ToString()+"',password) = 1 and username ='"+this.usuario.Text.ToString()+"'");
            if(res!=null){

                VariablesGlobales.Password = contraseña.Text.ToString();
                VariablesGlobales.Usuario = usuario.Text.ToString();
                
                Admin admin = new Admin(this);
                admin.Show();
                this.Hide();

               // User nuevo = new User();
               // nuevo.Show();
            }
            else
            {
                MessageBox.Show("Error, datos de acceso invalidos","Error de autenticacion");
            }
            
        }

        private void ShowReport(String url) { 
            ReportDocument myReportDocument = new ReportDocument();
            myReportDocument.Load(@url);
           
            Reportes reportes = new Reportes();

            myReportDocument.SetDatabaseLogon(VariablesGlobales.Usuario,VariablesGlobales.Password);
            reportes.crystalReportViewer1.ReportSource = myReportDocument;
            reportes.crystalReportViewer1.DisplayToolbar = true;
            reportes.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
