<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="HRControllManager.aspx.cs" Inherits="Mediqura.Web.MedHR.HRControllManager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

    	var Page;
    	function pageLoad() {

    		Page = Sys.WebForms.PageRequestManager.getInstance();

    		Page.add_initializeRequest(OnInitializeRequest);

    	}

    	function OnInitializeRequest(sender, args) {

    		var postBackElement = args.get_postBackElement();

    		if (Page.get_isInAsyncPostBack()) {
    			alert('One request is already in progress....');
    			args.set_cancel(true);
    		}

    	}
    </script>
    <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <%-- Start of container-fluid--%>
            <div class="row">
                <%-- Start of upper row--%>
                <div class="col-sm-2 upper-heading">
                    <h5 class="heading text-left text-primary ">HR Control Manager  </h5>
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
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Department<span
                                style="color: red">*</span> </span>
                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddldepartmentID_SelectedIndexChanged" 
								Class="form-control input-sm col-sm custextbox" ID="ddldepartmentID">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Designation <span
                                style="color: red">*</span> </span>
                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                ID="ddldesignation" >
                            </asp:DropDownList>
                        </div>
                    </div>
					<div class="col-sm-3">
                        <div class="form-group input-group">
                            <span class="input-group-addon cusspan">Employee Type <span
                                style="color: red">*</span> </span>
                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                ID="ddlemployeetype" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3" style="text-align: right">
                    <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" />
                    <asp:Button ID="btncancel" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btncancel_Click" />
                </div>
                </div>
            </div>
          
           
            <div class="row cusrow pad-top">
             
                <div class="col-sm-12">
                    <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">

                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                            <ProgressTemplate>
                                <div id="DIVloading" runat="server" class="loading text-center ">
                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:GridView ID="GvHRcontrolList" CssClass="table-hover grid_table result-table" runat="server"
                            EmptyDataText="No record found..." OnRowDataBound="GvHRcontrolList_RowDataBound"
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
                                        Employee
                                    </HeaderTemplate>
                                    <ItemTemplate>
										 <asp:Label ID="lblempID" Visible="false" runat="server" Text='<%# Eval("EmployeeID")%>'></asp:Label>
                                        <asp:Label ID="lblemployee" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Designation
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesignationID" Visible="false" runat="server" Text='<%# Eval("DesignationID")%>'></asp:Label>
										 <asp:Label ID="lbldepartmentID" Visible="false" runat="server" Text='<%# Eval("DepartmentID")%>'></asp:Label>
                                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
								<asp:TemplateField>
                                    <HeaderTemplate>
                                        Employee Type
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblemployeetypeID" Visible="false" runat="server" Text='<%# Eval("EmployeeTypeID")%>'></asp:Label>
										 <asp:Label ID="lblemployeetype"  runat="server" Text='<%# Eval("EmployeeType")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Leave Request Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblleaverequest" Visible="false" runat="server" Text='<%# Eval("LeaveRequestEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_leaverequest" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                      Leave Approve Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblleaveapprove" Visible="false" runat="server" Text='<%# Eval("LeaveApproveEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_leaveapprove" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                        Roster update Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblrosterupdate" Visible="false" runat="server" Text='<%# Eval("RosterUpdateEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_rosterupdate" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Roster Change Request Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblrosterchangerequest" Visible="false" runat="server" Text='<%# Eval("RosterChangeRequestEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_rosterchangerequest" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Roster Change Approve Enable 
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblrosterchangeapprove" Visible="false" runat="server" Text='<%# Eval("RosterChangeApproveEnable") %>'></asp:Label>
                                        <asp:CheckBox ID="chekboxselect_rosterchangeapprove" runat="server" />
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
                    <asp:Button ID="btnupdate" Style="margin-left: 6px" runat="server" Visible="false"  Class="btn  btn-sm cusbtn exprt" Text="Update" OnClick="btnupdate_Click"/>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
