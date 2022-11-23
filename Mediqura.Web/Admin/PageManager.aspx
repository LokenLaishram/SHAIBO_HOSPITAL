<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PageManager.aspx.cs" Inherits="Mediqura.Web.Admin.PageManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid" style="z-index: 0px;">
                <link href="../Styles/Cstyles.css" rel="stylesheet" type="text/css" />
                <div class="row">
                    <div class="col-sm-2 upper-heading">
                        <h5 class="heading text-left text-primary ">Page Manager</h5>
                        <hr class="underline" />
                    </div>
                    <div class="col-sm-5 cus-message">
                        <asp:ValidationSummary ID="valSummaryMessage" runat="server" CssClass="cus-message" />
                        <asp:Label ID="lblError" runat="server" CssClass="errorText" />
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-3">

                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Role Names</span>
                                    <asp:DropDownList ID="drpRole" runat="server" class="form-control input-sm col-sm custextbox"
                                        AutoPostBack="true" OnSelectedIndexChanged="drpRole_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group input-group pull-right" style=" margin-right: -79px;">
                                    <asp:Button ID="btnSubmit" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnClear" runat="server" Class="btn   btn-sm cusbtn" CausesValidation="False"
                                        Text="Cancel" OnClick="btnClear_Click" />
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-4 ">
                                <label class="text-primary container-fluid common_heading">
                                    Assign Page Rights</label>
                                <div id="section2" class="cus-scroll">
                                    <asp:Table ID="tblPermission" runat="server" style="background-color:#fff" class=" table table-hover table-bordered fontstyle custable"
                                        Width="100%">
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
