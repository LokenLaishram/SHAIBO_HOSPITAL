<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="GenDeptStockAllocation.aspx.cs" Inherits="Mediqura.Web.MedGenStore.GenDeptStockAllocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabbedassign" runat="server" CssClass="Tab2" HeaderText="GEN DepartmentWise Stock Allocation">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panelassignbed">
                                <contenttemplate>
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                      
                                    <div class="col-sm-12">
                                       <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Department <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_dept" runat="server"  class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_dept_SelectedIndexChanged" >
                                        </asp:DropDownList>

                                     </div>
                                  </div>
                                  
                                    </div>
                                <div class="row">
                                   <div class="col-sm-6"></div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click"/>
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset"  OnClick="btnresets_Click"/>
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
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvdeptstock" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="false" PageSize="10" OnRowDataBound="gvdeptstock_RowDataBound" 
                                                        Width="100%" HorizontalAlign="Center"> 
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                           
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Department
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_deptID" Visible="false" runat="server" Text='<%# Eval("deptID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_dept" runat="server" Text='<%# Eval("deptName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                            <HeaderTemplate>
                                                               Is Stock Available? 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                             <asp:Label ID="lblavail" Visible="false" runat="server" Text='<%# Eval("stockAvail") %>'></asp:Label>
                                                               <asp:CheckBox ID="chkselectIsStockAvail" runat="server" onclick="Check_Click(this);" />
                                                              
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
                            </div>
                                <div class="row">
                                    <div class="col-lg-12" Width="25px"></div>
                                </div>
                              <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnUpdate" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Update" OnClick="btnUpdate_Click" />
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

