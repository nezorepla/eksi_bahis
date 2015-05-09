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

/// <summary>
/// Summary description for yazar
/// </summary>
public class yazar
{
    db DB = new db();
    public yazar()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private int yazarId;
    public int YazarId
    {
        get
        {
            return yazarId;
        }
        set { YazarId = value; }
    }    private double bakiye;
    public double Bakiye
    {
        get
        {
            return bakiye;
        }
        set { Bakiye = value; }
    }
    private Boolean suser;
    public Boolean Suser
    {
        get
        {
            return suser;
        }
        set { Suser = value; }
    }
    public void GelenYazar(string Nick)
    {
        DataTable dt = DB.Getdata("exec EB_SP_YazarGiris '" + Nick + "',1", "eb");
        yazarId = int.Parse(dt.Rows[0][0].ToString());
        suser = Boolean.Parse(dt.Rows[0][1].ToString());
    }
    public void GelenYazar(string Nick, Int16 YFlg)
    {
        DataTable dt = DB.Getdata("exec EB_SP_YazarGiris '" + Nick + "'," + YFlg, "eb");
        yazarId = int.Parse(dt.Rows[0][0].ToString());
        suser = Boolean.Parse(dt.Rows[0][1].ToString());
    }
    public void BakiyeAl(Int32 YID)
    {
        DataTable dt = DB.Getdata("exec EB_SP_YazarBakiye " + YazarId, "eb");
        Bakiye = double.Parse(dt.Rows[0][0].ToString());
    }
    public   string  BakiyeAl(string   YID)
    {
        DataTable dt = DB.Getdata("exec EB_SP_YazarBakiye " + YazarId, "eb");
     
        return dt.Rows[0][0].ToString();
    }
}
