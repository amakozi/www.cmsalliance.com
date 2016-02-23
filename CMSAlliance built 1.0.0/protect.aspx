<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txt_password.Text.ToLower() == "pa55word")
        {
            Session["preview"] = true;
            Response.Redirect("~/");
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Site is currently under construction</h2>

    <p>Enter Preview Password to view the site</p>
    <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Preview" onclick="Button1_Click" />

    </form>
</body>
</html>
