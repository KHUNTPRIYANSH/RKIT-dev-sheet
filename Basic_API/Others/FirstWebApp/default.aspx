<%@ Page Language="C#" AutoEventWireup="true"  Inherits="FirstWebApp._default" %>

<!DOCTYPE html>
<script runat="server">

    protected void button1_Click(object sender, EventArgs e)
    {
        Response.Write("Button is clickeddddddd");
    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <% Response.Write("yooooo"); %>
    <form id="form1" runat="server">
        <div>
            hiii
        </div>
        <asp:Button ID="button1" runat="server" Text="click me" BackColor="#FF99CC" OnClick="button1_Click" Width="240px" Height="150px" />
    </form>
</body>
</html>
