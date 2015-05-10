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
using System.Net;
using System.Text;
using ExtensionMethods;
/// <summary>
/// Summary description for EntryAl
/// </summary>
public class EntryAl
{
    public EntryAl()
    {

        baglanti = new SqlConnection("Data Source=.; Initial Catalog=eksi_bahis; Integrated Security=true");
    }

    public SqlConnection baglanti;
    //void EntryGetir()
    //{
    //    if (baglanti.State == ConnectionState.Closed)
    //    {
    //        baglanti.Open();
    //    }
    //    SqlCommand cmd = new SqlCommand("SELECT eid FROM eb_dat_dunun where durum = 0 ORDER BY id", baglanti);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    adp.Fill(dt);
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        EntryAlma(dr[0].ToString());

    //    }
    //    baglanti.Close();
    //}

 //   10.05.2015 10:34
    public  void EntryAlma(string eid)
    {

        WebClient wc = new WebClient();
        wc.Encoding = Encoding.UTF8;
        string source = "";
        string zaman = "";
        try
        {
            source = wc.DownloadString("https://eksisozluk.com/entry/" + eid);
           // string baslik = source.vericek("&quot;", "&quot;");
            string baslik = source.vericek("og:title\" content=\"", "\"");
            string entry = source.vericek("<div class=\"content\">", "</div>");
            string zaman1 = source.vericek("<a class=\"entry-date ", "</a>");
            if (zaman1.IndexOf("~") != -1)
            {
               
                zaman = source.vericek("<a class=\"entry-date ", " ~").Right(16);  }
            else
            {
                zaman = zaman1.Right(16);
          
            } 
            string ozet = source.vericek("og:description\" content=\"", "\"");
            string yazar = source.vericek("entry-author\" href=\"/biri/", "\"");
            string favoritecount = source.vericek("data-favorite-count=\"", "\"");
            cmd(eid, baslik,entry, ozet, yazar, favoritecount, zaman);
            wc = null;
        }
        catch (Exception e)
        {
            //    cmd(eid, e.ToString(), "hata", "01/01/1900");
        }
    }

    private void cmd(string eid,string baslik, string entry, string ozet, string yazar, string favoritecount, string zaman)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("exec eb_SP_EntryAdd @id,@baslik,@yazi,@ozet,@yazar,@favoritecount,@trh", baglanti);

            cmd.Parameters.AddWithValue("@id", eid);
            cmd.Parameters.AddWithValue("@baslik", baslik);
            cmd.Parameters.AddWithValue("@yazi", entry);
            cmd.Parameters.AddWithValue("@ozet", ozet);
            cmd.Parameters.AddWithValue("@yazar", yazar);
            cmd.Parameters.AddWithValue("@favoritecount", favoritecount);
            cmd.Parameters.AddWithValue("@trh", zaman);

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }

            cmd.ExecuteNonQuery();

            baglanti.Close();

        }
        catch (SqlException)
        {
            //MessageBox.Show("Hata olustu!");
        }
    }
    

}
