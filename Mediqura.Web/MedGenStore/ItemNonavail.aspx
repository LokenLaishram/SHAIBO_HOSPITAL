<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ItemNonAvail.aspx.cs" Inherits="Mediqura.Web.MedGenStore.ItemNonAvail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintDeptWiseStockList() {
            objgenstock = document.getElementById("<%=ddl_substock.ClientID %>")
            objstatus = document.getElementById("<%=ddl_purchagestatus.ClientID %>")
            objitem = document.getElementById("<%=txtItemName.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=NonAvail&Stock=" + objgenstock.value + "&Item=" + objitem.value + "&StatusID=" + objstatus.value)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Non Available Item ">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <contenttemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                   <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Sub Stock </span>
                                        <asp:DropDownList ID="ddl_substock" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cus-long-span" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-span"
                                            ID="txtItemName" AutoPostBack="True" OnTextChanged="txtItemName_TextChanged"  ></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemDetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtItemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted"></asp:AutoCompleteExtender>
                                        <asp:Label ID="lblservicename" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                                   <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Purchage Status </span>
                                        <asp:DropDownList runat="server" ID="ddl_purchagestatus" CssClass="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                             <asp:ListItem Value="1">Normal Requirment</asp:ListItem>
                                             <asp:ListItem Value="2">Urgent Requirment</asp:ListItem>
                                             <asp:ListItem Value="3">Stat Requirment</asp:ListItem>
                                             <asp:ListItem Value="4">First time to Purchage</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                               <div class="row">
                               <div class="col-sm-8">
                               
                                </div>
                                   <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                         <asp:Button ID="btnprints" runat="server"  Class="btn  btn-sm  cusbtn" Text="Print" OnClientClick="return PrintDeptWiseStockList();"  />
                                       <asp:Button ID="btnresets" runat="server" OnClick="btnresets_Click" Class="btn  btn-sm cusbtn" Text="Reset"/>
                                    </div>
                                </div>
                                       </div>
                           <div class="row">
                                   <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div1" runat="server">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-L">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvNonavailitems" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."  OnRowDataBound="GvNonavailitems_RowDataBound"  
                                                        Width="100%" HorizontalAlign="Center" >
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
                                                               Item Name
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                                 <asp:Label ID="lbl_status" Visible="false" runat="server" Text='<%# Eval("StockStatus")%>'></asp:Label>                              
                                                                    <asp:Label ID="lbl_itemName" Style="text-align:center !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                     </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                              <HeaderTemplate>
                                                               Sub Stock
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                              <asp:Label ID="lbl_substock" Style="text-align:center !important;" runat="server"
                                                              Text='<%# Eval("StockTypeName") %>'></asp:Label>
                                                              </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                              <HeaderTemplate>
                                                               Status
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                              <asp:Label ID="lbl_c" Style="text-align:center !important;" runat="server"
                                                              ></asp:Label>
                                                              </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                       </Columns>
                                                       <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                       <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          </contenttemplate>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

