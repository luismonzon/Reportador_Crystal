using System;
using System.Data.SqlClient;
public class Conexion
{

    String database;
    String user;
    String pass;
    String server;
    
    public Conexion()
    {

    }

    public void EjecutarSql(String  comando)
    {
        try
        {
            datosConexion = "Data Source=" + server + ";" + "Initial Catalog=" + database + ";Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(datosConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void GetSql(String comando)
    {
        try
        {
            datosConexion = "Data Source=" + server + ";" + "Initial Catalog=" + database + ";Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(datosConexion))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(comando, con);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


}
