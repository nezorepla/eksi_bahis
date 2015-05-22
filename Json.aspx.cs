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

public partial class Json : System.Web.UI.Page
{
    db DB = new db();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = DB.Getdata("exec EB_SP_Bulten_Debe", "eb");
        
        string val = dt.ToJSON();
        Response.Clear();
        //Response.ContentType = "application/json; charset=utf-8";
        Response.Write(val);
        Response.End();
    }
}
