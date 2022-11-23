<%@ Page Language="C#" Title="Report Viewer" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Mediqura.Web.MedLab.Report.ReportViewer" %>

<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="MediReportViewer" runat="server" PrintMode="Pdf" AutoDataBind="true" />

        </div>
    </form>
</body>

