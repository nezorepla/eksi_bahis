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
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Data.SqlClient;


/// <summary>
/// Summary description for DebeAl
/// </summary>
public class DebeAl
{
    db DB = new db();
    public DebeAl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public Boolean DebeKontrol()
    {
        DataTable dt = DB.Getdata("  select   isnull(count(*),0) rv  from EB_Def_Durum where DATEDIFF(day,sdt,getdate())=0", "eb");
        bool rv = false;
        if (int.Parse(dt.Rows[0][0].ToString()) > 0)
            rv = true;
        return rv;

    }

    public SqlConnection baglanti;


    public void al()
    {
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
        long tick = DateTime.Now.Ticks;

        string source = wc.DownloadString("https://eksisozluk.com/istatistik/dunun-en-begenilen-entryleri?_=" + tick.ToString());

        //StreamWriter writer = new StreamWriter("c:\\DTS_PACKAGES\\sozluktest.txt");
        //writer.WriteLine(source);
        // writer.Close();

        source = vericek(source, "topic-list\"", "</ol>");
        source = source.Replace("<li>", "|").ToString();
        string[] ayir = source.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        int i = 1;

        foreach (string parca in ayir)
        {

            string linkRx = @"href=\""(.*?)\""";
            string titleRx = @"(<span.*?>.*?</span>)"; // @"<span class=""caption"">";
            string autRx = @"(<div.*?>.*?</div>)";
            Match link = Regex.Match(parca, linkRx, RegexOptions.Singleline);
            Match title = Regex.Match(parca, titleRx, RegexOptions.Singleline);
            Match autor = Regex.Match(parca, autRx, RegexOptions.Singleline);
            if (link.Success)
            {
                insert(
                i.ToString(), link.Value.Replace("href=\"", "").Replace("\"", "").ToString(),
                title.Value.Replace("<span class=\"caption\">", "").Replace("</span>", "").ToString(),
                autor.Value.Replace("<div class=\"detail\">", "").Replace("</div>", "").ToString());


                //writer.WriteLine(i.ToString());
                //writer.WriteLine(link.Value.Replace("href=\"", "").Replace("\"", "").ToString());
                //writer.WriteLine(title.Value.Replace("<span class=\"caption\">", "").Replace("</span>", "").ToString());
                //writer.WriteLine(autor.Value.Replace("<div class=\"detail\">", "").Replace("</div>", "").ToString());
                //writer.WriteLine("");
                i++;

            }

        }
        //writer.Close();










    }

    public void insert(string a, string b, string c, string d)
    {
        SqlCommand cmd = new SqlCommand("INSERT INTO EB_Tmp_Dunun (sira,adres,baslik,yazar,tarih) VALUES (@a,@b,@c,@d,getdate())", baglanti);
        cmd.Parameters.AddWithValue("@a", a);
        cmd.Parameters.AddWithValue("@b", b);
        cmd.Parameters.AddWithValue("@c", c);
        cmd.Parameters.AddWithValue("@d", d);
        if (baglanti.State == ConnectionState.Closed)
        {
            baglanti.Open();
        }

        cmd.ExecuteNonQuery();
        baglanti.Close();
    }

    public static string vericek(string StrData, string StrBas, string StrSon)
    {
        try
        {
            int IntBas = StrData.IndexOf(StrBas) + StrBas.Length;
            int IntSon = StrData.IndexOf(StrSon, IntBas + 1);
            return StrData.Substring(IntBas, IntSon - IntBas);
        }
        catch
        {
            return "";
        }

    }




    public string HTMLTableString(DataTable dt, string id, string css)
    {

        String RVl = "";
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id=\"" + id + "\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\"  class=\"" + css + "\"  ><thead><tr>");
            foreach (DataColumn c in dt.Columns)
            {
                sb.AppendFormat("<th>{0}</th>", c.ColumnName);
            }
            sb.AppendLine("</tr></thead><tbody>");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>"); foreach (object o in dr.ItemArray)
                {
                    sb.AppendFormat("<td>{0}</td>", o.ToString());
                    //System.Web.HttpUtility.HtmlEncode());
                } sb.AppendLine("</tr>");
            } sb.AppendLine("</tbody></table>");
            RVl = sb.ToString();
        }
        catch (Exception ex)
        {
            RVl = "HATA @ConvertDataTable2HTMLString: " + ex;
        }
        return RVl;
    }


}
