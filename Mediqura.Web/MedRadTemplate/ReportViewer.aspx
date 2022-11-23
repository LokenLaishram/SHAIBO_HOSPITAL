<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Mediqura.Web.MedRadTemplate.ReportViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <iframe id="pdfiframe" width="1330" height="630" name="pdfiframe" runat="server"></iframe>
        </div>
    </form>
</body>
</html>
