<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="GenSubstockManagement.aspx.cs" Inherits="Mediqura.Web.MedGenUtility.GenSubstockManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)
        }

        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()

            if (Page.get_isInAsyncPostBack()) {
                ddl_department_SelectedIndexChanged
                alert('One request is already in progress....')
                args.set_cancel(true)
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineritemgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabitemgroupmaster" runat="server" CssClass="Tab2" HeaderText="GEN Sub-Stock Manager">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panelitemgroupmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Department <span
                                                style="color: red">* </span></span>
                                            <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Employee</span>
                                            <asp:DropDownList ID="ddlemployee" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Stock type</span>
                                            <asp:DropDownList ID="ddlstocktype" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-lg-4"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
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
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="Gvsubtockmanagement" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="Gvsubtockmanagement_RowDataBound" AllowPaging="false" PageSize="10"
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
                                                                    Employee
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblemployeeID" runat="server" Text='<%# Eval("EmployeeID")%>'></asp:Label>
                                                                    <asp:Label ID="lblemployeeName" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Stock Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblstocktypeID" runat="server" Text='<%# Eval("GenSubStockID")%>'></asp:Label>
                                                                    <asp:DropDownList ID="ddl_stocktype" AutoPostBack="true" OnSelectedIndexChanged="ddl_stocktype_SelectedIndexChanged" Style="text-align: left !important;" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Request Enable
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblrequestenableID" runat="server" Text='<%# Eval("GenItemRequestEnable")%>'></asp:Label>
                                                                    <asp:CheckBox ID="chkrequestenable" OnCheckedChanged="chkrequestenable_CheckedChanged" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Verify Enable
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblverifyenableID" runat="server" Text='<%# Eval("GenItemVerifyEnable")%>'></asp:Label>
                                                                    <asp:CheckBox ID="chkverifyenable" OnCheckedChanged="chkverifyenable_CheckedChanged" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approve Enable
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblapproveenableID" runat="server" Text='<%# Eval("GenItemApproveEnable")%>'></asp:Label>
                                                                    <asp:CheckBox ID="chkapproveenable" AutoPostBack="true" OnCheckedChanged="chkapproveenable_CheckedChanged" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    HandOver Enable
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblhandoverenableID" runat="server" Text='<%# Eval("GenItemHandoverEnable")%>'></asp:Label>
                                                                    <asp:CheckBox ID="chkhandoverenable" AutoPostBack="true" OnCheckedChanged="chkhandoverenable_CheckedChanged" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
