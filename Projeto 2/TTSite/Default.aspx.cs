using System;
using System.Drawing;
using System.Web.UI;
using System.ServiceModel;
using TTService;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public partial class _Default : Page
{
    TTProxy proxy;

    protected void Page_Load(object sender, EventArgs e)
    {

        string image_src = System.IO.Directory.GetCurrentDirectory().ToString() + "\\t.png";

        logo.Attributes["src"] = image_src;

        proxy = new TTProxy();
        if (!Page.IsPostBack)
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (Username.Text == "")
        {
            Label1.ForeColor = Color.Red;
            Label1.Text = "Result: Select an Author!";
            return;
        }
        
        if (Usermail.Text == "")
        {
            Label1.ForeColor = Color.DarkBlue;
            Label1.Text = "Result: Insert an email!";
            return;
        }

        if (Title.Text == "")
        {
            Label1.ForeColor = Color.Red;
            Label1.Text = "Result: Please describe a title!";
            return;
        }

        if (Description.Text == "")
        {
            Label1.ForeColor = Color.Red;
            Label1.Text = "Result: Please describe a problem!";
            return;
        }
            
        proxy.AddTicket(Username.Text, Usermail.Text, Title.Text, Description.Text);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Username.Text.Length > 0)
        {
            GridView1.DataSource = proxy.GetTicketsByUser(Username.Text);
            GridView1.DataBind();
            GridView1.Visible = true;
            Label2.Text = "";
        }
        else
        {
            GridView1.Visible = false;
            Label2.Text = "Select an Author!";
        }
    }
}



