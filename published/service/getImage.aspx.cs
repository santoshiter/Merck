using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mime;
using System.Media;
using System.IO;

public partial class service_getImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UInt64 Id = Convert.ToUInt64(Request.QueryString["id"]);
        String Type = Request.QueryString["type"];
        string fileName = "";
        if (Type == null)
        {
            fileName = Server.MapPath(@"/Uploads/Images/" + Id + ".jpeg");
        }
        else
        {
            fileName = Server.MapPath(@"/Uploads/Thumbnails/" + Id + ".jpeg");
        }

        Response.ContentType = "image/jpeg";
        Response.WriteFile(fileName);

    }
}