<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="herend1.jpg" /><br />
        <br />
        <asp:ChangePassword ID="ChangePassword1" runat="server" CancelDestinationPageUrl="~/Query1.aspx"
            ContinueDestinationPageUrl="~/Query1.aspx">
        </asp:ChangePassword>
        &nbsp;</div>
    </form>
</body>
</html>
