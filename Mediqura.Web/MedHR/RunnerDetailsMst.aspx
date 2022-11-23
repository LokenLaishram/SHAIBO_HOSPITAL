<%@ Page Title="Collection Centre" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="RunnerDetailsMst.aspx.cs" Inherits="Mediqura.Web.MedHR.RunnerDetailsMst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:TabContainer ID="tabcontainerRunnerTax" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabRunnerTaxMST" runat="server" CssClass="Tab2" HeaderText="Runner Master">
                    <ContentTemplate>
                     <div class="custab-panel" id="panelRunnerTaxMST">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Code <span style="color: red;">*</span> <span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_RunnerCode" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Name <span style="color: red;">*</span><span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_runnerName" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Commission (%) <span style="color: red;">*</span><span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_tax" MaxLength="3"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_tax" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Contact No <span style="color: red;">*</span><span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_contactNo" MaxLength="10"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_contactNo" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Address <span style="color: red;">*</span><span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_runnerAddress" MaxLength="500"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Email  <span style="color: red;">*</span><span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_email" MaxLength="500"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm scusbtn" Text="Add" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <!----------Tab 1 Grid View------------->
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                <asp:GridView ID="GvRunnerTax" runat="server" CssClass="table-hover grid_table result-table"
                                                    AllowPaging="True" OnRowCommand="GvDocTax_RowCommand"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Code
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lblServiceType" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RunnerCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserviceName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RunnerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Address
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RunnerAddress") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Email
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_email" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("EmailID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Contact No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_contactno" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Commission(%)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_tax" runat="server" Text='<%# Eval("RunnerTax","{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Added On
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Edit-header">Edit</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                                    <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Delete</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                     <i class="fa fa-trash-o cus-delete-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
          <asp:UpdateProgress ID="updateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
