<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="AssignPageRights.aspx.cs" Inherits="Mediqura.Web.Admin.AssignPageRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <script type="text/javascript">
                function checkAll(objRef) {
                    var GridView = objRef.parentNode.parentNode.parentNode;
                    var inputList = GridView.getElementsByTagName("input");
                    for (var i = 0; i < inputList.length; i++) {
                        //Get the Cell To find out ColumnIndex
                        var row = inputList[i].parentNode.parentNode;
                        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                            if (objRef.checked) {
                                //If the header checkbox is checked
                                //check all checkboxes
                                //and highlight all rows
                                row.style.backgroundColor = "white";
                                inputList[i].checked = true;
                            }
                            else {
                                //If the header checkbox is checked
                                //uncheck all checkboxes
                                //and change rowcolor back to original
                                if (row.rowIndex % 2 == 0) {
                                    //Alternating Row Color
                                    row.style.backgroundColor = "white";
                                }
                                else {
                                    row.style.backgroundColor = "white";
                                }
                                inputList[i].checked = false;
                            }
                        }
                    }
                }
                function Check_Click(objRef) {
                    //Get the Row based on checkbox
                    var row = objRef.parentNode.parentNode;
                    if (objRef.checked) {
                        //If checked change color to Aqua
                        row.style.backgroundColor = "white";
                    }
                    else {
                        //If not checked change back to original color
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                    }
                    //Get the reference of GridView
                    var GridView = row.parentNode;
                    //Get all input elements in Gridview
                    var inputList = GridView.getElementsByTagName("input");
                    for (var i = 0; i < inputList.length; i++) {
                        //The First element is the Header Checkbox
                        var headerCheckBox = inputList[0];
                        //Based on all or none checkboxes
                        //are checked check/uncheck Header Checkbox
                        var checked = true;
                        if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                            if (!inputList[i].checked) {
                                checked = false;
                                break;
                            }
                        }
                    }
                    headerCheckBox.checked = checked;
                }
            </script>
            <div class="row">
                <div class="col-sm-2 upper-heading">
                    <h5 class="heading text-left text-primary ">Page Manager</h5>
                    <hr class="underline" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6 fixeddiv">
                    <div class="row fixeddiv" id="divmsg1" runat="server">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span class="input-group-addon cusspan">Role Name <span
                            style="color: red">*</span></span>
                        <asp:DropDownList ID="drpRole" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox"
                            OnSelectedIndexChanged="drpRole_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span class="input-group-addon cusspan">Menu <span
                            style="color: red">*</span></span>
                        <asp:DropDownList ID="ddl_menuheader" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_menuheader_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3" style="visibility: hidden">
                    <div class="form-group input-group">
                        <span class="input-group-addon cusspan">Page</span>
                        <asp:DropDownList ID="ddl_subheader" runat="server" class="form-control input-sm col-sm custextbox"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_subheader_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row cusrow pad-top">
                <div class="col-sm-6">
                    <div>
                        <div class="pbody">
                            <div class="grid" style="float: left; width: 100%; max-height: 60vh; overflow: auto">
                                <asp:GridView ID="Gvpagemanager" runat="server" CssClass="table-hover grid_table result-table"
                                    AllowPaging="false" EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" OnRowDataBound="Gvpagemanager_RowDataBound" HorizontalAlign="Center">
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
                                                Page Description
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sitemapID" Visible="false" Style="text-align: left !important;" runat="server"
                                                    Text='<%# Eval("PageID") %>'></asp:Label>
                                                <asp:Label ID="lbl_menustatus" Visible="false" Style="text-align: left !important;" runat="server"
                                                    Text='<%# Eval("IsView") %>'></asp:Label>
                                                <asp:Label ID="lbl_menuHeader" Visible="false" Style="text-align: left !important;" runat="server"
                                                    Text='<%# Eval("IsMenuheader") %>'></asp:Label>
                                                <asp:Label ID="lbl_page" Style="text-align: left !important;" runat="server"
                                                    Text='<%# Eval("PageName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" onclick="Check_Click(this);" />
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
            </div>
            <div class="row">
                <div class="col-lg-7"></div>
                <div class="col-sm-2">
                    <div class="form-group input-group cuspanelbtngrp ">
                        <asp:Button ID="btnupdate" Visible="false" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnupdate_Click" Text="Save" />
                    </div>
                </div>
            </div>
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
