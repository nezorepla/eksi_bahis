﻿using System;
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

public partial class _Default : System.Web.UI.Page 
{
    yazar Yazar = new yazar();
    DebeAl debeal = new DebeAl();
    db DB = new db();

    protected void Page_Load(object sender, EventArgs e)
    {
        string Nick= "eskimo";
        Int16 YazarFlg=1;

        
        
        Yazar.GelenYazar(Nick,YazarFlg);
        Session["yazar"] = Nick;
        Session["yazar_ID"] = Yazar.YazarId;
        Session["suser"] = Yazar.Suser;

        if (!debeal.DebeKontrol())
        {
            debeal.al();
            DB.ExecuteStoredProcedure("EB_SP_DEBE", "eb");
        }
        Response.Redirect("Oranlar.aspx");
    }
}
