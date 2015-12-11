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
        //server = "192.168.0.167\\NTFS";
        server = "localhost";
        user = "sa";
        pass = "K66admin";
        database = "LSA";
        datosConexion = "Data Source=" + server + ",1433;Network Library=DBMSSOCN;" +
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

    public void Insertar_reportes_Sql()
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();
            string sql = "INSERT INTO LSA_REPORTES(path_reporte) VALUES(@param1);";
            SqlCommand cmd = new SqlCommand(sql, connection);
            foreach (string filename in Directory.GetFiles(@"\\Ntfs\vep\Reportes\", "*.rpt"))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = filename;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }

        }


    }


    public String Insertar_user_dep(String user, String dep)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Asignar_Dep_User", connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@user", user));
            cmd.Parameters.Add(new SqlParameter("@dep", dep));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {

                while (rdr.Read())
                {
                    return rdr["resultado"].ToString();
                }
            }


        }

        return "0";
    }

    public String Insertar_rep_dep(String rep, String dep)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand("Asignar_Dep_rep", connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@rep", rep));
            cmd.Parameters.Add(new SqlParameter("@dep", dep));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                
                while (rdr.Read())
                {
                    return rdr["resultado"].ToString();
                }
            }


        }

        return "0";
    }



    public void Insertar_Dep(String nombre)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();
            string sql = "INSERT INTO LSA_DEPTOS(nombre_departamento) VALUES(@param1);";
            SqlCommand cmd = new SqlCommand(sql, connection);
            
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = nombre;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            

        }
       
    }

    public void Insertar_Reporte(String nombre, String url)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();
            string sql = "INSERT INTO LSA_REPORTES(nombre_reporte,path_reporte) VALUES(@param1,@param2);";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = url;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();


        }

    }

    public void Insertar_Usuario(String nombre, String pass)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();
            string sql = "INSERT INTO LSA_USUARIOS(username,password) VALUES(@param1,pwdencrypt(@param2));";
            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@param1", SqlDbType.VarChar).Value = nombre;
            cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = pass;
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();


        }

    }







    //DELETE

    public String Delete_Departamento(string dep, string dec, string param)
    {
        using (SqlConnection connection = new SqlConnection(datosConexion))
        {
            connection.Open();

            SqlCommand cmd = new SqlCommand(dec, connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter(param, dep));

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {

                while (rdr.Read())
                {
                    return rdr["resultado"].ToString();
                }
            }


        }

        return "0";
    }


}
