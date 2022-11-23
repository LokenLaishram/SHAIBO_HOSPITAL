<%@ Page Title="Lab Test" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabServiceMaster.aspx.cs" Inherits="Mediqura.Web.MedUtility.LabServiceMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <h2 class="breadcumb_cus">Lab Service Master</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                <div class="custab-panel" id="panellabgroupmaster">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="divmsg1" runat="server">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span10" class="input-group-addon cusspan" runat="server">Lab Group <span
                                    style="color: red">*</span></span>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddl_labgroup" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_labgroup" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span3" class="input-group-addon cusspan" runat="server">Lab Sub-Group <span
                                    style="color: red">*</span></span>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="ddl_labsubgroup" class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_labsubgroup" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group input-group">
                                <span id="Span1" class="input-group-addon cusspan" runat="server">Test Name <span
                                    style="color: red">*</span></span>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="100"
                                            ID="txt_labtestname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetLabServices" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_labtestname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3" style="visibility: hidden">
                            <div class="form-group input-group">
                                <span id="Span4" class="input-group-addon cusspan" runat="server">Test Center <span
                                    style="color: red">*</span></span>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="ddl_testcenter" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddl_testcenter" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="col-sm-3" style="visibility: hidden">
                            <div class="form-group input-group">
                                <span id="Span2" class="input-group-addon cusspan" runat="server">Amount(Rs.) <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                    ID="txt_testamount"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_testamount" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
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
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span6" class="input-group-addon cusspan" runat="server">Report Type  <span id="id_reporttpye"
                                    style="color: red" runat="server">*</span></span>
                                <asp:DropDownList ID="ddl_reportypes" runat="server" class="form-control input-sm col-sm custextbox">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Parameter</asp:ListItem>
                                    <asp:ListItem Value="2">Remark</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-8"></div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Add" UseSubmitBehaviour="false" OnClick="btnsave_Click" />
                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                            </div>
                        </div>
                    </div>
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
                                        <asp:GridView ID="GvLabService" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="GvLabService_PageIndexChanging" AllowPaging="true" AllowCustomPaging="true" PageSize="10"
                                            EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GvLabService_RowCommand"
                                            Width="100%" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Sl.No
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# (Container.DataItemIndex+1)+(GvLabService.PageIndex)*GvLabService.PageSize %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        Test Center
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_testcenter" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("TestCenter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Test Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                        <asp:Label ID="lbl_testname" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("TestName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Sub Group
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_subgroup" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("ServiceSubGroup") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                        Amount(Rs.)
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_testamount" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("TestAmount","{0:0#.##}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Report Type
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_reporttype" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("ReportType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Added By
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Added On
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Remarks
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtremarks" Width="120px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
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
                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-8">
                                <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                runat="server">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnexport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
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
