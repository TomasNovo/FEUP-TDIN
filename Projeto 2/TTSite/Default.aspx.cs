using System;
using System.Drawing;
using System.Web.UI;
using System.ServiceModel;
using TTService;
using System.Data;
using System.Collections.Generic;

public partial class _Default : Page {
  TTProxy proxy;

  protected void Page_Load(object sender, EventArgs e) {

    string image_src = System.IO.Directory.GetCurrentDirectory().ToString() + "\\t.png";

    logo.Attributes["src"] = image_src;

        proxy = new TTProxy();
    if (!Page.IsPostBack) {                           // only on first request of a session
      DropDownList1.DataSource = proxy.GetUsers();
      DropDownList1.DataBind();
    }
  }

  protected void Button1_Click(object sender, EventArgs e) {

        Username.Text = System.IO.Directory.GetCurrentDirectory();
        
    int id;

    if (DropDownList1.SelectedIndex > 0) {
      if (TextBox1.Text.Length > 0) {
        id = proxy.AddTicket(DropDownList1.SelectedValue, TextBox1.Text);
        Label1.ForeColor = Color.DarkBlue;
        Label1.Text = "Result: Inserted with Id = " + id;
      }
      else {
        Label1.ForeColor = Color.Red;
        Label1.Text = "Result: Please describe a problem!";
      }
    }
    else {
      Label1.ForeColor = Color.Red;
      Label1.Text = "Result: Select an Author!";
    }

    //Register user
    if(Username.Text != null && Usermail.Text != null)
    proxy.AddUserToDB(Username.Text, Usermail.Text);

    //Register ticket 
    DateTime date = DateTime.Now;
    if(Username.Text != null && TextBox4.Text != null && TextBox1.Text != null)
    proxy.AddTicketToDB(Username.Text, date, TextBox4.Text, TextBox1.Text);
    
  }

  protected void Button2_Click(object sender, EventArgs e) {
    if (DropDownList1.SelectedIndex > 0) {
      GridView1.DataSource = proxy.GetTickets(DropDownList1.SelectedValue);
      GridView1.DataBind();
      GridView1.Visible = true;
      Label2.Text = "";
    }
    else {
      GridView1.Visible = false;
      Label2.Text = "Select an Author!";
    }
  }
}

class TTProxy : ClientBase<ITTService>, ITTService {
  public DataTable GetUsers() {
    return Channel.GetUsers();
  }

  public DataTable GetTickets(string author) {
    return Channel.GetTickets(author);
  }

  public int AddTicket(string author, string desc) {
    return Channel.AddTicket(author, desc);
  }

  //our methods
  public int AddUserToDB(string username, string email) {
    return Channel.AddUserToDB(username, email);
  }

    public int AddTicketToDB(string username, System.DateTime date, string title, string description)
    {
        return Channel.AddTicketToDB(username, date, title, description);
    }

    public List<string> GetUsersMongo()
    {
        return Channel.GetUsersMongo();
    }
}

