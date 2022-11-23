<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ControlManager.aspx.cs" Inherits="Mediqura.Web.Admin.ControlManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <%-- Start of container-fluid--%>
            <div class="row">
                <%-- Start of upper row--%>
                <div class="col-sm-2 upper-heading">
                    <h5 class="heading text-left text-primary ">Page Control Manager  </h5>
                    <hr class="underline" />
                </div>
                <div class="col-sm-2">
                    <h5 class="heading text-center text-success ">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </h5>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="col-sm-4">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Role<span
                                style="color: red">*</span> </span>
                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlroleID_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                ID="ddlroleID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Employe <span
                                style="color: red">*</span> </span>
                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                ID="ddlemployee">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Pages </span>
                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                ID="ddlpages">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12" style="text-align: right">
                    <asp:Button ID="btnsearch" runat="server" Text="Search" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" />
                    <asp:Button ID="btncancel" runat="server" Text="Reset" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" OnClick="btncancel_Click" />
                </div>
            </div>
            <%-- End of role input column--%>

            <%-- End of role input row--%>
            <div class="row cusrow pad-top">
                <%-- Start of panel row--%>
                <div class="col-sm-12">
                    <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">
                        <asp:GridView ID="GvcontrolList" CssClass="table-hover grid_table result-table" runat="server"
                            EmptyDataText="No record found..." OnRowDataBound="GvcontrolList_RowDataBound"
                            AutoGenerateColumns="False" Width="100%" class="grid" HorizontalAlign="Center">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Sl. No.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Module
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblmodule" runat="server" Text='<%# Eval("Module") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Page
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("SitemapID")%>'></asp:Label>
                                        <asp:Label ID="lblpage" runat="server" Text='<%# Eval("PageName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="4%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Save Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsave" Visible="false" runat="server" Text='<%# Eval("SaveEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Save" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Update Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblupdate" Visible="false" runat="server" Text='<%# Eval("UpdateEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Update" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Search Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsearch" Visible="false" runat="server" Text='<%# Eval("SearchEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Search" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Edit Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbledit" Visible="false" runat="server" Text='<%# Eval("EditEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Edit" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Delete Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldelete" Visible="false" runat="server" Text='<%# Eval("DeleteEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Delete" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Print Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblprint" Visible="false" runat="server" Text='<%# Eval("PrintEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Print" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Service Amnt 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_serviceamount" Visible="false" runat="server" Text='<%# Eval("AmountEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Amount" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Export Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblexport" Visible="false" runat="server" Text='<%# Eval("ExportEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_Export" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#d8ebf5" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%-- End of panel row--%>
            <%-- End of container-fluid--%>
            <div class="row">
                <div class="col-md-7">
                </div>
                <div class="col-md-5">
                    <asp:Button ID="btnupdate" Style="margin-left: 6px" runat="server" Visible="false" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsave_Click" Class="btn  btn-sm cusbtn exprt" Text="Update" />
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgress2" runat="server">
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
