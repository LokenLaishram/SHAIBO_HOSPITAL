<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="GradeEmployeeBedAssigner.aspx.cs" inherits="Mediqura.Web.MedHR.GradeEmployeeBedAssigner" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:content id="Content1" contentplaceholderid="Mediquraplaceholder" runat="server">
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
            <asp:TabContainer ID="tabcontainerNurseStatn" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabNurseStatn" runat="server" CssClass="Tab2" HeaderText="MSB Grade Employee Bed Assigner">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btn_save">
                            <div class="custab-panel" id="panelOTRolesmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Employee Grade</span>
                                                   <asp:DropDownList ID="ddl_emp_grade" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                       
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Ward/Bed</span>
                                             <asp:DropDownList ID="ddl_ward" runat="server"  class="form-control input-sm col-sm custextbox">
                                                                </asp:DropDownList>
                                             
                                    </div>
                                        </div>
                               
                                  <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnadd" runat="server" Class="btn  btn-sm cusbtn" Text="Add" OnClick="btnadd_Click" />
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btn_reset_Click" />
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
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GVmsbBedAllot" runat="server" CssClass="table-hover grid_table result-table" OnRowCommand="GVmsbBedAllot_RowCommand"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVmsbBedAllot_RowDataBound"
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
                                                                Employee Grade
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>
                                                                 <asp:Label ID="lbl_empGradeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("EmployeeGradeID") %>'></asp:Label>
                                                                 <asp:Label ID="lbl_empGrade" runat="server"
                                                                    Text='<%# Eval("EmployeeGrade") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                              <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Allotted Bed
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                   <asp:Label ID="lbl_BedID" Visible="false" runat="server"
                                                                    Text='<%# Eval("BedAllotedID") %>'></asp:Label>
                                                                 <asp:Label ID="lbl_alloted_bedID" Visible="true" runat="server"
                                                                    Text='<%# Eval("BedAlloted") %>'></asp:Label>
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Self
                                                                <asp:CheckBox ID="checkAllSelf" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lbl_self_check" Visible="false" runat="server"
                                                                    Text='<%# Eval("isSelf") %>'></asp:Label>
                                                                <asp:CheckBox ID="checkBoxSelf" runat="server" onclick="Check_Click(this);" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                          <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Dependent
                                                                <asp:CheckBox ID="checkAllDependent" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lbl_dependent_check" Visible="false" runat="server"
                                                                    Text='<%# Eval("isDependent") %>'></asp:Label>
                                                                <asp:CheckBox ID="checkBoxDependent" runat="server" onclick="Check_Click(this);" />
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
                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group pull-right">
                                        <asp:Button ID="btn_save" runat="server" UseSubmitBehavior="False" OnClick="btn_save_Click" Text="Update" Class="btn  btn-sm cusbtn" />
                                     </div>
                                </div>
                            </div>
                  </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
