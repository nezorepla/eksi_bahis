using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ExtensionMethods;

public partial class Oranlar : System.Web.UI.Page
{
    EntryAl ea = new EntryAl();
    db DB = new db();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                lblBulten.Text = Session["yazar_ID"].ToString();
            }
            catch
            {
                Response.Redirect("Default.aspx");
            }
        }
        Bulten();


    }



    public void Bulten()
    {
       // DataTable dt = DB.Getdata("exec EB_SP_Bulten_Debe", "eb");
       //// lblBulten.Text = dt.HTMLTableString("bulten", "bulten");
       // lblBulten.Text = dt.ToJSON();
    }


    protected void btnEidAl_Click(object sender, EventArgs e)
    {
        string eid = txtEid.Text.ToString().Right(8).OnlyNumber();//.Replace("#","").Trim();

        ea.EntryAlma(eid);
        Bulten();
    }
}
