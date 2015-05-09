using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    yazar y = new yazar();
    protected void Page_Load(object sender, EventArgs e)
    {
        string user=Session["yazar"].ToString();
        string YID =  Session["yazar_ID"].ToString() ;
        if (y.YazarId == 0)
            // Response.Redirect("Default.aspx");
            y.GelenYazar(user);

        lblYazar.Text = user;
        lblBakiye.Text = y.BakiyeAl(YID);
    }
}
