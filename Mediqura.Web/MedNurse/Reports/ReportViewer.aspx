<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Mediqura.Web.MedNurse.Reports.ReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="MediReportViewer" runat="server" PrintMode="Pdf" AutoDataBind="true" />

        </div>
    </form>
</body>