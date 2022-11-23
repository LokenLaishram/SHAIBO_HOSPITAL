<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Phr_ItemSaleTracking.aspx.cs" Inherits="Mediqura.Web.MedPhr.Phr_ItemSaleTracking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function Print_PhrSaleTracking() {
            objFrom = document.getElementById("<%=txt_from.ClientID %>")
            objTo = document.getElementById("<%=txt_To.ClientID %>")
            objItemName = document.getElementById("<%=txtitemname.ClientID %>")
            objBatchNo = document.getElementById("<%=txtBatchNo.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrSaleTracking&DateFrom=" + objFrom.value + "&DateTo=" + objTo.value + "&ItemName=" + objItemName.value + "&BatchNo=" + objBatchNo.value)
        }
        function PhrSaleTrackDetails() {
            objFrom = document.getElementById("<%=txt_from.ClientID %>")
            objTo = document.getElementById("<%=txt_To.ClientID %>")
            objItemID = document.getElementById("<%=hdnItemID.ClientID %>")
            objBatchNo = document.getElementById("<%=txtBatchNo.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrSaleTrackDetails&DateFrom=" + objFrom.value + "&DateTo=" + objTo.value + "&ItemID=" + objItemID.value + "&BatchNo=" + objBatchNo.value)
        }
    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerSaleList" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabSaleList" runat="server" CssClass="Tab1" HeaderText="Sale Tracker">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelGRN">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_from" class="input-group-addon cusspan" runat="server">Date From  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_from"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_from" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_from" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_to" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_To"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_To" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_To" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Supplier</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlsupplier"></asp:DropDownList>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblitemname" class="input-group-addon cusspan" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnTextChanged="ItemName_OnTextChanged"
                                            ID="txtitemname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtitemname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtitemname" ID="FilteredTextBoxExtender3"
                                            runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="-,#,:|  " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                              
                                  <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblBatchNo" class="input-group-addon cusspan" runat="server">Batch No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="25"
                                            ID="txtBatchNo"></asp:TextBox>                                      
                                        <asp:FilteredTextBoxExtender TargetControlID="txtitemname" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="-/\:|  " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-3" style="text-align: right;">
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btnsearch_Click" />
                                    <asp:Button ID="btn_print" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return Print_PhrSaleTracking();" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnreset_Click" />
                                </div>
                            </div>
                            <%--                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg2" runat="server">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-M">
                                                <div class="grid" style="float: left; width: 100%; height: 58vh; overflow: auto;">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvPhrSaleItemList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="gvPhrSaleItemList_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" Text='<%# Eval("ItemName") %>' CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" style="text-decoration:underline; cursor:pointer">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%"  />
                                                            </asp:TemplateField>
															<asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Batch No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblbatchnos" Style="text-align: left !important;" runat="server" Text='<%# Eval("BatchNo") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    MRP/unit(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmrpperunit" Style="text-align: left !important;" runat="server" Text='<%# Eval("MRPperUnit","{0:00.00}") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Total Sales
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalsale" Style="text-align: left !important;" runat="server" Text='<%# Eval("TotalSalesQty","{0:00.00}") %>' ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                   Sales Amt(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNetsaleamt" Style="text-align: left !important;" runat="server" Text='<%# Eval("TotalSalesAmnt","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Total Return 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totalreturn" Style="text-align: left !important;" runat="server" Text='<%# Eval("TotalReturnQty","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Return Amt(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsalereturnAmt" Style="text-align: left !important;" runat="server" Text='<%# Eval("TotalSalesReturnAmnt","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                  Gross Sales(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblactualsale" Style="text-align: left !important;" runat="server" Text='<%# Eval("ActualSalesAmnt","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabtranferlist" runat="server" CssClass="Tab1" HeaderText="Sale Details">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                    <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8">
                                    <div class="form-group input-group">
                                        <span id="lbl_itemname" class="input-group-addon cusspan" runat="server">Item Name </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_itemname"></asp:TextBox>
                                        <asp:HiddenField ID="hdnItemID" runat="server" />
										<asp:HiddenField ID="hdnbatchno" runat="server" />
                                    </div>
                                </div>
								<div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Patient</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlpatienttype" OnSelectedIndexChanged="ddlpatienttype_SelectedIndexChanged" AutoPostBack="true">
											<asp:ListItem Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="1">EMG</asp:ListItem>
											<asp:ListItem Value="2">IPD</asp:ListItem>
											<asp:ListItem Value="3">OPD</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg4" runat="server">
                                            <asp:Label ID="lbl_result" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <%--<div class="pbody">--%>
                                        <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto;">
                                            <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIV2" class="text-center loading" runat="server">
                                                        <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gvSaleDetailslist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." ShowFooter="true"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            SlNo.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="0.1%" />
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Supplier
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_supplier" runat="server" Text='<%# Eval("Supplier")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
													
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Bill No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RecieptNo" runat="server" Text='<%# Eval("RecieptNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Sales Date 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SaleDate" runat="server" Text='<%# Eval("AddedDate", "{0:dd/MM/yyyy  hh:mm tt}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Patient
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerType" runat="server" Text='<%# Eval("CustomerType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Customer Details
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_UHID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                            <asp:Label ID="lbl_customerName" runat="server" Text='<%# Eval("CustomerName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            MRP/Qty
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_transferFrom" runat="server" Text='<%# Eval("MRPperQty","{0:00.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Sale Qty
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SaleEqvQty" runat="server" Text='<%# Eval("SaleEqvQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Return Qty
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ReturnEqvQty" runat="server" Text='<%# Eval("ReturnQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Sale Amt(₹) 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SaleAmount" runat="server" Text='<%# Eval("NetAmount","{0:00.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Return Amt(₹) 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ReturnAmount" runat="server" Text='<%# Eval("SalesReturnAmnt","{0:00.00}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle BackColor="#e8e8e8" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 10px;"></div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltotolsaleqty" class="input-group-addon cusspan" runat="server">Total Sale Qty</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txttotolsaleqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblTotalReturnQty" class="input-group-addon cusspan" runat="server">Total Return Qty</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txtTotalReturnQty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltotalsaleamount" class="input-group-addon cusspan" runat="server">Total Sale Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txttotalsaleamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltotalreturnamt" class="input-group-addon cusspan" runat="server">Total Return Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txttotalreturnamt"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9"></div>
                                <div class="col-md-3" style="text-align: right; padding-top: 10px;">
                                    <asp:Button ID="btn_prints" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PhrSaleTrackDetails();" />
                                    <asp:Button ID="btn_resets" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnreset_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
