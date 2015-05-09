using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for db
/// </summary>
public class db
{
    public db()
    {

    }

    private static string GetConnStr(String pConnKey)
    {
        System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();
        string ConnStr = (string)asr.GetValue(pConnKey, typeof(string));
        return ConnStr;
    }
    //public void GelenYazar(String Procedure, String User, String pConnKey)
    //{
    //    SqlConnection Conn = new SqlConnection(GetConnStr(pConnKey));
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.CommandText = Procedure;
    //    cmd.Connection = Conn;
    //    if (Conn.State == ConnectionState.Closed)
    //        Conn.Open();
    //    cmd.Parameters.Add("@Yazar", SqlDbType.VarChar, 250).Value = User;

    //    try
    //    {
    //        cmd.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        Conn.Close();
    //    }
    //}


    public void ExecuteStoredProcedure(String query, String pConnKey)
    {
        SqlConnection Conn = new SqlConnection(GetConnStr("pConnKey"));
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = query;
        cmd.Connection = Conn;
        if (Conn.State == ConnectionState.Closed)
            Conn.Open();
        //  cmd.Parameters.Add("@Parameters", SqlDbType.Char).Value = Parameters;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            Conn.Close();
        }
    }

    //public DataTable Getdata(SqlDataAdapter da, String pConnKey)
    //{
    //    SqlConnection conn = new SqlConnection(GetConnStr(pConnKey));
     
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        da = new SqlDataAdapter(query, conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        da.Fill(dt);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //    return dt;
    //}
    public DataTable Getdata(string query, String pConnKey)
    {
        SqlConnection conn = new SqlConnection(GetConnStr(pConnKey));
        SqlDataAdapter da;
        DataTable dt = new DataTable();
        try
        {
            da = new SqlDataAdapter(query, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            conn.Close();
        }
        return dt;
    }
}
