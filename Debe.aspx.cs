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

public partial class Debe : System.Web.UI.Page
{
    db DB = new db();
    DebeAl debe = new DebeAl();
    protected void Page_Load(object sender, EventArgs e)
    {

        lblDebe.Text = DBYaz();

    }

    public string DBYaz() {

        DataTable dt = DB.Getdata("exec EB_SP_Dunun", "eb");
 return  dt.HTMLTableString( "debe", "debe");
    }
}
