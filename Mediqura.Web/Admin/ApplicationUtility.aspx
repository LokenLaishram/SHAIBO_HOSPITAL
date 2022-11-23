<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="ApplicationUtility.aspx.cs" inherits="Mediqura.Web.Admin.ApplicationUtility" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:content id="Content1" contentplaceholderid="Mediquraplaceholder" runat="server">
  <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerblockmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabblockmaster" runat="server" CssClass="Tab2" HeaderText="Application Utility">
                    <contenttemplate>
                        <div class="custab-panel" id="ipdadmissiondetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="gridview-container">
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvApplicationUtility" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="GvApplicationUtility_RowDataBound"
                                                        EmptyDataText="No record found..."  AutoGenerateColumns="False"  
                                                        Width="100%" HorizontalAlign="Center" >
                                                        <Columns>
                                                            <asp:TemplateField >
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
                                                                    Application
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsActive" Style="text-align: center !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("Status") %>'></asp:Label>
                                                                    <asp:Label ID="lblID" Style="text-align: center !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("AppID") %>'></asp:Label>
                                                                    <asp:Label ID="lblblock" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("AppName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Enable
                                                                <asp:CheckBox Visible="false" ID="chekboxall" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekboxselect" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                               </Columns>
                                                    </asp:GridView>
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
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnupdateClick" />
                                    </div>
                                </div>
                            </div>
                            </div>
                        </contenttemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
