<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Mediqura.Web.Reports.ReportViewer" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <form id="form1" runat="server"> 
        <div>
            <cr:crystalreportviewer id="MediReportViewer" runat="server" printmode="Pdf" autodatabind="true" />
        </div>
    </form>
</body>
</html>

