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
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for EntryAl
/// </summary>
public class EntryAl
{
    public EntryAl()
    {

          System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();
        string ConnStr = (string)asr.GetValue("eb", typeof(string));

        baglanti = new SqlConnection(ConnStr);
     //   baglanti = new SqlConnection("Data Source=.; Initial Catalog=eksi_bahis; Integrated Security=true");
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
    public void EntryAlma(string eid)
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

                zaman = source.vericek("<a class=\"entry-date ", " ~").Right(16);
            }
            else
            {
                zaman = zaman1.Right(16);

            }
            string ozet = source.vericek("og:description\" content=\"", "\"");
            string yazar = source.vericek("entry-author\" href=\"/biri/", "\"");
            string favoritecount = source.vericek("data-favorite-count=\"", "\"");
            cmd(eid, baslik, entry, ozet, yazar, favoritecount, zaman,0);
            wc = null;
        }
        catch (Exception e)
        {
            //    cmd(eid, e.ToString(), "hata", "01/01/1900");
        }
    }

    private void cmd(string eid, string baslik, string entry, string ozet, string yazar, string favoritecount, string zaman,int pop)
    {

        try
        {
            SqlCommand cmd = new SqlCommand("exec eb_SP_EntryAdd @id,@baslik,@yazi,@ozet,@yazar,@favoritecount,@trh,@pop", baglanti);

            cmd.Parameters.AddWithValue("@id", eid);
            cmd.Parameters.AddWithValue("@baslik", baslik);
            cmd.Parameters.AddWithValue("@yazi", entry);
            cmd.Parameters.AddWithValue("@ozet", ozet);
            cmd.Parameters.AddWithValue("@yazar", yazar);
            cmd.Parameters.AddWithValue("@favoritecount", favoritecount);
            cmd.Parameters.AddWithValue("@trh", zaman);
            cmd.Parameters.AddWithValue("@pop", pop);

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


    public void PopulerBasliklariAl()
    {
        System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();
        string ConnStr = (string)asr.GetValue("eb", typeof(string));

        baglanti = new SqlConnection(ConnStr);


        WebClient wc = new WebClient();
       
        wc.Encoding = Encoding.UTF8;
        long tick = DateTime.Now.Ticks;

        string source = wc.DownloadString("https://eksisozluk.com/basliklar/populer?_=" + tick.ToString());


        string pattern = @"href=\""(.*?)a=popular\""";
        //   Match link = Regex.Match(parca, linkRx, RegexOptions.Singleline);
        Regex rgx = new Regex(pattern);

        foreach (Match match in rgx.Matches(source))
        {
            string rp = "?day=" + DateTime.Now.ToString("yyyy-MM-dd") + "&a=nice";
            string link = match.Value.Replace("href=\"", "").Replace("\"", "").Replace("?a=popular", rp).ToString();
            // string link = "/24-mayis-2015-galatasaray-besiktas-maci--4470751?day=2015-05-24&a=nice";
            PopulerBasliktakiEntryleriAl(link);

        }



    }

    public void PopulerBasliktakiEntryleriAl(string url) { 
          System.Configuration.AppSettingsReader asr = new System.Configuration.AppSettingsReader();
        string ConnStr = (string)asr.GetValue("eb", typeof(string));

        baglanti = new SqlConnection(ConnStr);
        
        WebClient wc = new WebClient();
        //wc.Headers.Add("Content-Type: text/html; charset=windows-1254");
        //wc.Headers.Add("Content-Type: text/html; charset=iso-8859-9;");

        //wc.Headers.Add("Content-Type: application/x-www-form-urlencoded");
        //wc.Headers.Add("User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/536.5 (KHTML, like Gecko) Chrome/19.0.1084.56 Safari/536.5");
        //wc.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        //wc.Headers.Add("Accept-Encoding: identity");
        //wc.Headers.Add("Accept-Language: tr-TR");
        //wc.Headers.Add("Accept-Language: tr-TR,tr;q=0.8");
        wc.Encoding = Encoding.UTF8;
       
        string source = wc.DownloadString("https://eksisozluk.com" + url);
        string baslik = source.vericek("og:title\" content=\"", "\"");

                source = source.vericek("entry-list", "</ul>");
        source = source.Replace("</li>", "|").ToString();
        string[] ayir = source.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        int i = 1;

        foreach (string src in ayir)
        {



        //string pattern = @"li data-id(.*?)</li>";
        ////   Match link = Regex.Match(parca, linkRx, RegexOptions.Singleline);
        //Regex rgx = new Regex(pattern);

        //foreach (Match match in rgx.Matches(source))
        //{
            try
            {
               // string src = match.Value.ToString();
                string zaman = "";
                string entry = src.vericek("<div class=\"content\">", "</div>");
                string zaman1 = src.vericek("<a class=\"entry-date ", "</a>");
                if (zaman1.IndexOf("~") != -1)
                {

                    zaman = src.vericek("<a class=\"entry-date ", " ~").Right(16);
                }
                else
                {
                    if (zaman1.IndexOf(":") == -1)
                    {

                        zaman = zaman1.Right(10);
                    }
                    else
                    {
                        zaman = zaman1.Right(16);
                    }
                }
                string eid = src.vericek("data-id=\"", "\"");
                string ozet = "";// src.vericek("og:description\" content=\"", "\"");
                string yazar = src.vericek("data-author=\"", "\"");
                string favoritecount = src.vericek("data-favorite-count=\"", "\"");
                cmd(eid, baslik, entry, ozet, yazar, favoritecount, zaman,i);
            }
            catch { }

            i++;    
   }

   
  
    }
}
