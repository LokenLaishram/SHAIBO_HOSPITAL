<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="UserManager.aspx.cs" Inherits="Mediqura.Web.Admin.UserManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" TagName="CustomPager" Src="~/UserControls/CustomPager.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function Validate() {

            var str = "";
            var i = 0;
            if (document.getElementById("<%=ddlemp.ClientID%>").selectedIndex == "0") {
                str = str + "\n Please Select Employee.";
                document.getElementById("<%=ddlemp.ClientID %>").focus();
                i++;
            }
            if (document.getElementById("<%=txtusername.ClientID%>").value == "") {
                str = str + "\n Please Enter User Name.";
                document.getElementById("<%=txtusername.ClientID %>").focus();
                i++;
            }
            if (document.getElementById("<%=txtpassword.ClientID%>").value == "") {
                str = str + "\n Please Enter Password.";
                document.getElementById("<%=txtpassword.ClientID %>").focus();
                i++;
            }

            if (document.getElementById("<%=txtpassword.ClientID%>").value = "") {
                str = str + "\n Please Enter Password.";
                document.getElementById("<%=txtpassword.ClientID %>").focus();
                i++;
            }
            if (document.getElementById("<%=txtcpassword.ClientID%>").value == "") {
                str = str + "\n Please Enter Confirm Password.";
                document.getElementById("<%=txtcpassword.ClientID %>").focus();
                i++;
            }

            if (document.getElementById("<%=ddlrole.ClientID%>").selectedIndex == "0") {
                str = str + "\n Please Select Role.";
                document.getElementById("<%=ddlrole.ClientID %>").focus();
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
            <div class="container-fluid ">
                <div class="row">
                    <div class="col-sm-2 upper-heading">
                        <h5 class="heading text-left text-primary ">User Manager</h5>
                        <hr class="underline" />
                    </div>
                    <div class="col-sm-5 text-primary">
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12" style="z-index: 0">
                        <%-- Start of input column--%>
                        <div class="row">
                            <%-- Start of input row--%>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Employee Name</span>
                                    <asp:DropDownList ID="ddlemp" runat="server" Class="form-control input-sm col-sm dropdown custextbox">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">User Name</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtusername"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                                        runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtusername">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Role Name</span>
                                    <asp:DropDownList ID="ddlrole" runat="server" Class="form-control input-sm col-sm custextbox cusDropDown">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- End of  input first row--%>
                        <div class="row">

                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Password</span>
                                    <asp:TextBox runat="server" oncopy="return false" onpaste="return false" Class="form-control input-sm col-sm custextbox" TextMode="Password"
                                        ID="txtpassword"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                                        runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtpassword">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan" style="padding-left: 0px; padding-right: 0px">Confirm Password</span>
                                    <asp:TextBox ID="txtcpassword" oncopy="return false" onpaste="return false" Class="form-control input-sm col-sm custextbox" TextMode="Password"
                                        runat="server"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender FilterType="UppercaseLetters,LowercaseLetters,Numbers"
                                        runat="server" ID="FilteredTextBoxExtender2" TargetControlID="txtcpassword">
                                    </asp:FilteredTextBoxExtender>
                                </div>
                                <span class="text-danger">
                                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtpassword" ControlToCompare="txtcpassword"
                                        runat="server" ErrorMessage="Password not match." ValidationGroup="grp"></asp:CompareValidator>
                                </span>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span class="input-group-addon cusspan">Discount Notification</span>
                                    <asp:DropDownList ID="ddl_dis_notification" runat="server" Class="form-control input-sm col-sm custextbox cusDropDown">
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Value="1">YES</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-8"></div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Add"
                                    OnClick="btnsave_Click" OnClientClick="return Validate();" />
                                <asp:Button ID="btnsearch" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" Text="Search"
                                    OnClick="btnsearch_Click" />
                                <asp:Button ID="btncancel" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" Text="Reset"
                                    OnClick="btncancel_Click" />
                            </div>
                        </div>
                    </div>
                    <%-- End of input second row--%>
                </div>
                <%-- End of input column--%>
            </div>
            <%-- End of input row--%>
            <div class="row cusrow">
                <%-- start of result display row--%>
                <div class="col-sm-12">
                    <div class="panel panel-info cuspanel">
                        <div class="pbody">
                            <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                <asp:GridView ID="GvUserdetails" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" EmptyDataText="No record found..."
                                    OnPageIndexChanging="GvUserdetails_PageIndexChanging" AutoGenerateColumns="False"
                                    Width="100%" PageSize="10" HorizontalAlign="Center" OnRowCommand="GvUserdetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                ID
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("LoginID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                User Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Password
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblpassword" runat="server" Text='<%# Eval("UserPassword") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Role Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblrolename" runat="server" Text='<%# Eval("RoleName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Emp Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Date
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbladdeddate" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Discount Notification
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiscountNotfication" runat="server" Text='<%# Eval("isNotification")%>'></asp:Label>
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

                                    <HeaderStyle BackColor="#d8ebf5" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- End of result display row--%>
            </div>
            <%-- End of container fluid--%>
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
