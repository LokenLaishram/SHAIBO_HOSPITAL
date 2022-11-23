<%@ Page Language="C#" AutoEventWireup="true" CodeFile="server.aspx.cs" Inherits="aceoffix_runtime.server" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>Welcome to use Aceoffix product</h3>
        <p style="background-color:Gray; color:White; font-weight:bold">Aceoffix registration information:</p>
        <asp:Label ID="LabelReg" runat="server" Text=""></asp:Label>
        <br />
        <p style="background-color:Gray; color:White; font-weight:bold">Aceoffix running information:</p>
        <asp:Label ID="LabelLog" runat="server" Text="No logs."></asp:Label>
    </div>
    </form>
</body>
</html>
