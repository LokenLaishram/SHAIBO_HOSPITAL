<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ServiceMapping.aspx.cs" Inherits="Mediqura.Web.MedUtility.ServiceMapping" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerServiceGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPanelServiceGroup" runat="server" HeaderText="Services Mapping Master">
                    <ContentTemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Group Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_group_type" AutoPostBack="True" runat="server"  class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_group_type_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Common</asp:ListItem>
                                                <asp:ListItem Value="2">Investigation</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Service Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_servicetype" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Mapping Type <span
                                            style="color: red">*</span></span>
                                                <asp:DropDownList runat="server" ID="ddl_map_type" class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddl_map_type_SelectedIndexChanged">
                                                    <asp:ListItem value="0">-Select-</asp:ListItem>
                                                    <asp:ListItem value="1">Group Wise</asp:ListItem>
                                                    <asp:ListItem value="2">Service Wise</asp:ListItem>
                                                </asp:DropDownList>
                                          
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Sub Group  </span>
                                                <asp:DropDownList runat="server" ID="ddl_subservicetype" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                          
                                    </div>
                                </div>
                           </div>
                            <div class="row">
                                <div class="col-lg-6">
                                       <div class="form-group input-group">
                                           
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Services<span
                                                style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" 
                                            ID="txtServices"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtServices" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetServices" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtServices"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                           </div>

                                </div>
                               <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnadd" runat="server" Class="btn  btn-sm cusbtn" Text="Add" OnClick="btn_add_nurse_Click" />
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn"  Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                    </div>
                                </div>
 </div>

                            <div class="row">
                                <div class="col-lg-8"></div>
                            
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
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                            <asp:GridView ID="GVMapping" runat="server" CssClass="table-hover grid_table result-table" OnRowCommand="GVMapping_RowCommand"
                                                                EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center">
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
                                                                            Service Type
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceTypeID" Visible="false" Text='<%# Eval("ServicetypeID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblServiceTypeName" Text='<%# Eval("Servicetype") %>' runat="server"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="Label1" runat="server">Service Name</asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                           <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                           <asp:Label ID="lblserviceID" Visible="false" Text='<%# Eval("ServiceID") %>' runat="server"></asp:Label>
                                                                           <asp:Label ID="lblServiceName" Text='<%# Eval("ServiceName") %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
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

                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-lg-8"></div>
                               <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnupdate" Visible="false" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Update" OnClick="btnupdate_Click" />
                                   </div>
                                </div>
                            </div>
                       </div>
                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>