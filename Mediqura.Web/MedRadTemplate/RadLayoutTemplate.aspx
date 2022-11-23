<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="RadLayoutTemplate.aspx.cs" Inherits="Mediqura.Web.MedRadTemplate.RadLayoutTemplate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
</script>
    <asp:TabContainer ID="tabcontainer" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="TabPanel2" runat="server" CssClass="Tab2" HeaderText="Layout Template">
            <ContentTemplate>
                <div class="custab-panel" id="Div101">
                    <div class="row fixeddiv" id="div4" runat="server" style="padding-bottom: 2px; margin-bottom: 10px;">
                        <asp:Label ID="lbl_message3" runat="server"></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-sm-10">
                            <dx:ASPxRichEdit ID="RichLayoutTemplate" runat="server" WorkDirectory="~\App_Data\WorkDirectory1"></dx:ASPxRichEdit>
                        </div>
                        <div class="col-sm-2">
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:Button ID="btn_updatelayout" runat="server" CssClass="btn btn-sm scusbtn" OnClick="btn_updatelayout_Click" Text="Save" />
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btn_reset" runat="server" CssClass="btn btn-sm scusbtn" OnClick="btn_reset_Click" Text="Reset" />
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

</asp:Content>
