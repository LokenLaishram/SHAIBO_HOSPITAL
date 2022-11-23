<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ChangeRefferal.aspx.cs" Inherits="Mediqura.Web.MedBills.ChangeRefferal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="custab-panel" id="depositdetaildiv">
                <div class="fixeddiv">
                    <div class="row fixeddiv" id="div1" runat="server">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span23" class="input-group-addon cusspan" runat="server">Patient Type <span
                                style="color: red">*</span></span>
                            <asp:DropDownList ID="ddl_patienttype" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span32" class="input-group-addon cusspan" runat="server">Bill No.</span>
                            <asp:TextBox runat="server" placeholder="" Class="form-control input-sm col-sm custextbox"
                                ID="txtbillNo" AutoPostBack="true" OnTextChanged="txtbillNo_OnTextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                ServiceMethod="GetBillNo" MinimumPrefixLength="1"
                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtbillNo"
                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group input-group">
                            <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Details</span>
                            <asp:TextBox runat="server" placeholder=" " ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                ID="txtPatientDetails"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span2" class="input-group-addon cusspan" runat="server">Source Type</span>
                            <asp:TextBox runat="server" placeholder=" " ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                ID="txtSourceType"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span3" class="input-group-addon cusspan" runat="server">Referal Name</span>
                            <asp:TextBox runat="server" placeholder=" " ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                ID="txtReferalName"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span38" class="input-group-addon cusspan" runat="server">Source <span
                                style="color: red">*</span></span>
                            <asp:DropDownList ID="ddl_source" AutoPostBack="true" OnSelectedIndexChanged="ddl_source_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                <asp:ListItem Value="1">Walkin</asp:ListItem>
                                <asp:ListItem Value="2">Referal Hospital</asp:ListItem>
                                <asp:ListItem Value="3">Referal Doctor</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span24" class="input-group-addon cusspan" runat="server">Referred By <span
                                style="color: red">*</span> </span>
                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                ID="txt_referal" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_referal"
                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                            </asp:AutoCompleteExtender>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Update" Class="btn  btn-sm cusbtn pull-right" OnClick="btnsave_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
