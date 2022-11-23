<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerInvoice.aspx.cs" Inherits="Mediqura.Web.Reports.ReportViewerInvoice" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="MediReportViewer" runat="server" PrintMode="Pdf" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
