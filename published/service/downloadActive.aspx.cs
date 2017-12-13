using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spire.Presentation;
using Spire.Presentation.Collections;
using Spire.Presentation.Drawing;
using Spire.Presentation.Converter;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using Medtrix.DataAccessControl;

public partial class service_downloadActive : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var userid = Request.QueryString["id"];
        if (userid != null)
        {
            DataCommand command = new DataCommand("getActiveSlides");
            command.Add("userid", userid);

            DataSet ds = (DataSet)command.Execute(true);
            DataTable dt = ds.Tables[0];
            Presentation newP = new Presentation();
            Presentation helper = new Presentation();
            var pname = "";

            foreach (DataRow dr in dt.Rows)
            {
                var presentationName = dr["presentationid"].ToString() + dr["type"].ToString();
                Int32 slideIndex = Convert.ToInt32(dr["slideindex"].ToString());
                Int32 orderNumber = Convert.ToInt32(dr["ordernumber"].ToString());
                //if(pname !=  presentationName) {
                helper.LoadFromFile(Server.MapPath("~/Uploads/" + presentationName)); //loading ppt file
                    pname = presentationName;
                //}
                ISlide orgSlide = helper.Slides[slideIndex]; // reading the slide
                newP.Slides.Insert(orderNumber, orgSlide); // inserting in to new ppt
            }

            //newP.SaveToFile(Server.MapPath("~/Uploads/tt.ppt"), FileFormat.PPT);

            Byte[] Content = newP.GetBytes();

            Response.ContentType = "application/octet-stream";
            Response.AddHeader("content-disposition", "attachment; filename=presentation.ppt");
            Response.BufferOutput = true;
            Response.OutputStream.Write(Content, 0, Content.Length);
            Response.End(); //downloading it
        }
        


    }
}