using System;
using System.Data.SqlClient;
using System.Data;
using System.IO;
public class Conexion
{

    String database;
    String user;
    String pass;
    String server;
    String datosConexion;
    public Conexion()
    {
        server = "NTFS";
        user = "sa";
        pass = "K66admin";
        database = "LSA";
        datosConexion = "Data Source=192.168.0.167\\" + server + ",1433;Network Library=DBMSSOCN;" +
            "Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass + ";";
    }

    public DataTable GetSql(String comando)
    {
        DataTable tabla = new DataTable();
        try
        {

            using (SqlConnection con = new SqlConnection(datosConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);

                try
                {
                    cmd.ExecuteNonQuery();



                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tabla);
                    da.Dispose();

                    if (tabla.Rows[0][0].ToString().Equals(""))
                    {
                        return null;
                    }

                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    tabla = null;
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            tabla = null;
        }
        return tabla;
    }

    public void Insertar_reportes_Sql() {
        using(SqlConnection connection = new SqlConnection(datosConexion))
            {
                connection.Open();
                string sql =  "INSERT INTO LSA_REPORTES(path_reporte) VALUES(@param1);";
                SqlCommand cmd = new SqlCommand(sql,connection);
                 foreach (string filename in Directory.GetFiles(@"\\Ntfs\vep\Reportes\", "*.rpt"))
                  {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = filename ;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                  }
                
            }
    

    }


}
