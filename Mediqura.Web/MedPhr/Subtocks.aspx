<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Subtocks.aspx.cs" Inherits="Mediqura.Web.MedPhr.Subtocks" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function printlist() {
            objgenstock = document.getElementById("<%=ddl_substock.ClientID %>")
            objstatus = document.getElementById("<%=ddl_stocstaus.ClientID %>")
            objitem = document.getElementById("<%=txtItemName.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_datefrom.ClientID %>")
            objdateto = document.getElementById("<%=txt_dateto.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=SubStockStatus&StockType=" + objgenstock.value + "&Item=" + objitem.value + "&StockStatus=" + objstatus.value + "&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Sub Stock Status">
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
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Sub Stock <span
                                            style="color: red">*</span></span>
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
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Stock Status </span>
                                        <asp:DropDownList runat="server" ID="ddl_stocstaus" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                               <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Date From  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_datefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_datefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_datefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_dateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dateto" />
                                    </div>
                                </div>
                               <div class="col-sm-6">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                         <asp:Button ID="btnprints" runat="server"  Class="btn  btn-sm  cusbtn" Text="Print" OnClientClick="return printlist();"  />
                                        <asp:Button ID="btn_save" runat="server" Class="btn  btn-sm cusbtn" Text="Save"  OnClick="btn_save_Click" />
                                       <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                                    <asp:GridView ID="gvDeptWise" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."   OnRowDataBound="gvstockstatus_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center" >
                                                        <Columns>
                                                
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Stock ID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubtockID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblstockstatus"  runat="server" Text='<%# Eval("StockStatus")%>'></asp:Label>  
																	<asp:Label ID="lblEquivalentQtyPerUnit"   runat="server" Text='<%# Eval("EquQtyPerUnit")%>'></asp:Label> 
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                   Main Stock No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_mainstock" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  Total Received Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrec" runat="server" Text='<%# Eval("QtyApproved", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  Total Used Unit(-) 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_used" runat="server" Text='<%# Eval("QtyUsed", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                 Return unit Main Stock(-)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_return" runat="server" Text='<%# Eval("ReturnQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  Total Balance Unit
                                                              </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_balance" Width="70px" runat="server" Text='<%# Eval("AvailableBalAfterUsed", "{0:0#.##}")%>' OnTextChanged="lbl_balance_OnTextChanged" AutoPostBack="true"></asp:TextBox>
																	
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  Equivalent Balance
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_Equibalance" ReadOnly="true"  Width="70px" runat="server" Text='<%# Eval("TotalEquivalentQtyBalance", "{0:0#.##}")%>'></asp:TextBox>
																	
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  MRP Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_mrpperunit" runat="server" Text='<%# Eval("MRPperunit", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                  MRP Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_mrpperqty" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>

                                                         <asp:TemplateField>
                                                           <HeaderTemplate>
                                                            Received Date
                                                          </HeaderTemplate>
                                                            <ItemTemplate>
                                                            <asp:Label ID="lbl_recddate" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                             <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                         </asp:TemplateField> 
                                                             <asp:TemplateField>
                                                           <HeaderTemplate>
                                                            Expiry Date
                                                          </HeaderTemplate>
                                                            <ItemTemplate>
                                                            <asp:TextBox ID="lbl_expirydate" runat="server" Text='<%# Eval("ExpiryDate")%>'></asp:TextBox>
                                                                  <asp:Label ID="lbl_Subheading" Visible="false" runat="server" Text='<%# Eval("SubHeading")%>'></asp:Label>
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
                            <div class="row">
                            <div class="col-sm-12">
                                   <div class="col-sm-9">
                                      </div>
                                     <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_update" runat="server"  Visible="false"   Text="Update" Class="btn  btn-sm cusbtn"
                                            OnClick="btn_update_Click" />
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
