<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="RoleManager.aspx.cs" Inherits="Mediqura.Web.Admin.RoleManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" TagName="CustomPager" Src="~/UserControls/CustomPager.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function Validate() {

            var str = "";
            var i = 0;

            if (document.getElementById("<%=txtcode.ClientID%>").value == "") {
                str = str + "\n Please enter Role Code.";
                document.getElementById("<%=txtcode.ClientID %>").focus();
                i++;

            }
            if (document.getElementById("<%=txtdescription.ClientID%>").value == "") {
                str = str + "\n Please enter Role Name.";
                document.getElementById("<%=txtdescription.ClientID %>").focus();
                i++;

            }


            if (str.length > 0) {
                alert("Check Following Required Fields : " + str);
                return false;
            }
            else
                return true;
        }

    </script>
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid">
                <%-- Start of container-fluid--%>
                <div class="row">
                    <%-- Start of upper row--%>
                    <div class="col-sm-2 upper-heading">
                        <h5 class="heading text-left text-primary ">Role Manager  </h5>
                        <hr class="underline" />
                    </div>
                    <div class="col-sm-2">
                        <h5 class="heading text-center text-success ">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Role Code</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" placeholder="Enter role code"
                                        ID="txtcode"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Role Name</span>
                                    <asp:TextBox ID="txtdescription" Class="form-control input-sm col-sm custextbox"
                                        placeholder="Enter role name" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <asp:Button ID="btnsave" runat="server" Text="Add" Class="btn  btn-sm cusbtn"
                                        OnClick="btnsave_Click" OnClientClick="return Validate()" />
                                    <asp:Button ID="btnsearch" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Search" Class="btn  btn-sm cusbtn"
                                        OnClick="btnsearch_Click" />
                                    <asp:Button ID="btncancel" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" Class="btn  btn-sm cusbtn"
                                        OnClick="btncancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- End of role input column--%>
                </div>
                <%-- End of role input row--%>
                <div class="row cusrow">
                    <%-- Start of panel row--%>
                    <div class="col-sm-12">
                        <div class="panel panel-info cuspanel">
                            <div class="pbody">
                                <div class="grid" style="float: left; width: 100%; height: 60vh; overflow: auto">
                                    <asp:GridView ID="GvRoledetails" CssClass="table-hover grid_table result-table" runat="server" AllowPaging="true"
                                        EmptyDataText="No record found..." OnPageIndexChanging="GvRoledetails_PageIndexChanging"
                                        AutoGenerateColumns="False" Width="100%" class="grid" PageSize="10" HorizontalAlign="Center"
                                        OnRowCommand="GvRoledetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    ID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("RoleID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Code
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcode" runat="server" Text='<%# Eval("RoleCode") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Role Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("RoleName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    AddedBy
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("AddedBy")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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

                                        <HeaderStyle BackColor="#d8ebf5" />
                                    </asp:GridView>
                                    <center>
                                    
                                    </center>
                                </div>
                            </div>
                            <%-- End of panel body--%>
                        </div>
                        <%-- End of panel --%>
                    </div>
                </div>
                <%-- End of panel row--%>
            </div>
            <%-- End of container-fluid--%>
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
