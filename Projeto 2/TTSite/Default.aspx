<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TTSystem</title>
    <style type="text/css">
      .auto-style1 {
        width: 100%;
        font-family: Arial;
        /*text-align: center;*/
      }
      .auto-style3 {
        width: 65px;
        font-family: Arial;
        /*text-align: center;*/
      }
    </style>
    <link rel="stylesheet" href="default.css"/>
</head>
<body style="background-color: #3c4043">
    <form id="form1" runat="server">
    <div>
       <asp:Image ID="logo" runat="server"></asp:Image>
      <h1>Ticket Factory</h1>
      <table class="auto-style1">
        <tr>
          <td class="auto-style2">Please identify yourself:</</td>
        </tr>
        <tr>
            <td class="auth">
                <asp:TextBox ID="Username" placeholder="Name" runat="server" Height="20px" TextMode="SingleLine" Width="200px" style="background-color: #181a1b"></asp:TextBox>
                <asp:TextBox ID="Usermail" placeholder="Email" runat="server" Height="20px" TextMode="Email" Width="200px" style="background-color: #181a1b"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td id="problem" class="auto-style3">Fill your trouble ticket: </td>
        </tr>
        <tr>
            <td id="tb4">
            <asp:TextBox ID="TextBox4" placeholder="Title" runat="server" Height="20px" TextMode="SingleLine" Width="200px" style="background-color: #181a1b"></asp:TextBox>
            </td>
        </tr>
        <tr>
          <td id="tb1">
            <asp:TextBox ID="TextBox1" placeholder="Describe here your problem..." runat="server" Height="100px" TextMode="MultiLine" Width="420px" style="background-color: #181a1b"></asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          </td>
         </tr>
          <tr>
            <td id="submit">
                <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
            </td>
        </tr>
          <tr>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>

            </td>
        </tr>
      </table>
      <p>
        <asp:Label ID="Label1" runat="server" Text="Result:" Font-Bold="True" Font-Names="Arial" ForeColor="#0000CC"></asp:Label>
      </p>
      <p>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Submitted problems" />
      </p>
      <p>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
      </p>
      <p>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:Label>
      </p>
    </div>
  </form>
</body>
</html>