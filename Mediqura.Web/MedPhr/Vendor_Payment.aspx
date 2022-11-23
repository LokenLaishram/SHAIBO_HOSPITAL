<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Vendor_Payment.aspx.cs" Inherits="Mediqura.Web.MedPhr.Vendor_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        function ItemChildGridview(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "../Images/plus.gif") {
                $("#" + displayIcon).closest("tr")
					.after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
					.html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "../Images/minus.gif");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "../Images/plus.gif");
            }
        }

        function PrintvendorRecieptDetails(InVoiceNo) {
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=VendorPurchasepayment&InVoiceNo=" + InVoiceNo)
        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <style type="text/css">
                .gridViewHeader th {
                    background-color: #c4d3ee;
                }
            </style>
            <%-- End of Style --%>
            <%-- Style Child Gridview --%>
            <style type="text/css">
                .Grid th {
                    background-color: #DF7401;
                    color: White;
                    font-size: 10pt;
                    line-height: 200%;
                }

                .Grid td {
                    background-color: #F3E2A9;
                    color: black;
                    font-size: 10pt;
                    line-height: 200%;
                    text-align: center;
                }

                .ChildGrid th {
                    background-color:;
                    color: Black;
                    font-size: 10pt;
                    border: 1px solid green;
                }

                .ChildGrid td {
                    background-color: #e2f5d9;
                    color: black;
                    font-size: 10pt;
                    text-align: center;
                    border: 1px solid green;
                }
            </style>
            <asp:TabContainer ID="tabcontainervendorpay" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Purchase List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div5">
                            <contenttemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div6" runat="server">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>

                                </div>

                            </div>
                            <div class="row">
                                   
                                <div class="col-sm-3">
                                   <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Date From  </span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_purchasedatefrom"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_purchasedatefrom" />

                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_purchasedatefrom" />

                                    </div>
                                </div>
                                   <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Date To  </span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_purchasedateto"></asp:TextBox>

                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_purchasedateto" />

                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_purchasedateto" />

                                    </div>
                                </div>
								<div class="col-sm-3">
											<div class="form-group input-group">
												<span id="Span28" class="input-group-addon cusspan" runat="server">Supplier</span>
												<asp:DropDownList runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="ddl_purchasesupplier" AutoPostBack="true" OnSelectedIndexChanged="ddl_purchasesupplier_SelectedIndexChanged"></asp:DropDownList>
												

											</div>
										</div>
								<div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnpurchasesearch" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnpurchasesearch_Click" />

                                       
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            
                               <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divpurchase" runat="server">
                                            <asp:Label ID="lblpurchasemsg" runat="server" Height="13px"></asp:Label>

                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height:50vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress4" runat="server"><ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        
</ProgressTemplate>
</asp:UpdateProgress>

                                                    <asp:GridView ID="GVPurchaseList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."   OnRowDataBound="GVPurchaseList_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center" ><Columns>
															<asp:TemplateField><ItemTemplate>
																				<a href="JavaScript:ItemChildGridview('div<%# Eval("InVoiceNo") %>');">
																					<img alt="Detail" id='imgdiv<%# Eval("InVoiceNo") %>' src="../Images/plus.gif" width="20px" />
																				</a>
																				<div id='div<%# Eval("InVoiceNo") %>' style="display: none;">
																					<asp:GridView ID="GridChildpurchase" runat="server" AutoGenerateColumns="false" DataKeyNames="InVoiceNo"
																						CssClass="ChildGrid">
																						<Columns>
																							<asp:BoundField ItemStyle-Width="293px" DataField="ItemName" HeaderText="Item" />
																							<asp:BoundField ItemStyle-Width="93px" DataField="NoUnit" DataFormatString="{0:0#.##}" HeaderText="Unit" />
																							<asp:BoundField ItemStyle-Width="93px" DataField="QtyPerUnit" HeaderText="Qty/Unit" />
																							<asp:BoundField ItemStyle-Width="93px" DataField="BatchNo" HeaderText="BatchNo" />
																							<asp:BoundField ItemStyle-Width="93px" DataField="ExpiryDate" HeaderText="ExpiryDt." />
																							<asp:BoundField ItemStyle-Width="100px" DataField="MRP" DataFormatString="{0:0#.##}" HeaderText="MRP(₹)" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="Rate" DataFormatString="{0:0#.##}" HeaderText="Rate(₹)" />
																					<asp:BoundField ItemStyle-Width="100px" DataField="Taxable" DataFormatString="{0:0#.##}" HeaderText="Taxable" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="CGST" DataFormatString="{0:0#.##}" HeaderText="CGST(%)" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="SGST" DataFormatString="{0:0#.##}" HeaderText="SGST(%)" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="TotalCP" DataFormatString="{0:0#.##}" HeaderText="Total(₹)" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="RecievedBy" HeaderText="Recieved By" />
																							<asp:BoundField ItemStyle-Width="100px" DataField="RecievedDate" HeaderText="Recieved Date" />
																						</Columns>
																					</asp:GridView>
																				</div>
																			
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="0.1%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                           Sl No.
                                                       
</HeaderTemplate>
<ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
	<asp:Label ID="lblpurcaseReceiptNo" Style="text-align: left !important;" runat="server" Visible="false"
																					Text='<%# Eval("ReceiptNo") %>'></asp:Label>
																 
															 
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="0.1%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                           Invoice No.
                                                       
</HeaderTemplate>
<ItemTemplate>
                                                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InVoiceNo")%>'></asp:Label>
																 
															 
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                    Supplier 
                                                                
</HeaderTemplate>
<ItemTemplate>
                                 <asp:Label ID="lblpurcaseSupplierID" Visible="false" runat="server" Text='<%# Eval("SupplierID")%>'></asp:Label>

                                   <asp:Label ID="lnkpurcasesuoolierName"  runat="server" Text='<%# Eval("SupplierName")%>' Style="text-decoration:none; font-weight:bold"></asp:Label>  
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                  Recieved Qty.
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lblrevievedQty" runat="server" Text='<%# Eval("TotalRecievedQty")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                 Free Qty.
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lblfreeqty" runat="server" Text='<%# Eval("TotalFreeQty")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                  MRP(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbl_mrp" runat="server" Text='<%# Eval("TotalMRP", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                    Discount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbl_discount" Text='<%# Eval("Discount", "{0:0#.##}") %>' runat="server"></asp:Label>
                                                                 
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                 Payable Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("TotalCP", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                 Paid Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lblpaidamt" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
<asp:TemplateField><HeaderTemplate>
                                                                 Due Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbldueamt" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1.5%" />
</asp:TemplateField>
															<asp:TemplateField><HeaderTemplate>
																				Added On 
																			
</HeaderTemplate>
<ItemTemplate>
																				<asp:Label ID="lbl_purchaseItemAddedOn" runat="server" Text='<%# Eval("ItemAddedOn")%>'></asp:Label>
																			
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="1%" />
</asp:TemplateField>
															<asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintvendorRecieptDetails('<%# Eval("InVoiceNo")%>'); return false;">Print</a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
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
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-left">
												<span id="Span33" class="input-group-addon cusspan" runat="server">Total Payable Amount(₹)</span>

												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_totalpurcaseamount"></asp:TextBox>

												</div>
									</div>
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-right">
												<span id="Span34" class="input-group-addon cusspan" runat="server">Total paid Amount(₹)</span>

												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_totalpurcasepaid"></asp:TextBox>

												</div>
									</div>
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-right">
												<span id="Span35" class="input-group-addon cusspan" runat="server">Total Due Amount(₹)</span>

												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_purcasedue"></asp:TextBox>


												</div>
									</div>
									
							</div>
                           </contenttemplate>
                        </div>

                    </ContentTemplate>

                </asp:TabPanel>
                <asp:TabPanel ID="tabpanelvendorpay" runat="server" HeaderText="Vendor Payment">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <contenttemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                   <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Supplier <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_vendor" runat="server" AutoPostBack="True"  class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_vendor_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                   <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Date From  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_datefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_datefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_datefrom" />
                                    </div>
                                </div>
                                   <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_dateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dateto" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                               <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Status </span>
                                        <asp:DropDownList ID="ddlstatus" runat="server"  class="form-control input-sm col-sm custextbox" >
											<asp:ListItem Value="1" Text="Due"></asp:ListItem>
											<asp:ListItem Value="2" Text="Paid"></asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </div>
                                </div>
                               <div class="col-sm-8">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                         <asp:Button ID="btnprints" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm  cusbtn" Text="Print" OnClick="btnprints_Click"/>
                                       <asp:Button ID="btnresets" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click"  />
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
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height:50vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        
</ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvVendorpay" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."   OnRowCommand="gvVendorpay_RowCommand"
                                                        Width="100%" HorizontalAlign="Center" >
                                                        <Columns>
                                                         <asp:TemplateField><HeaderTemplate>
                                                           Sl No.
                                                       
</HeaderTemplate>
<ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
																 
															 
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="0.1%" />
</asp:TemplateField>
                                                            <asp:TemplateField><HeaderTemplate>
                                                                    Supplier Name
                                                                
</HeaderTemplate>
<ItemTemplate>
                                 <asp:Label ID="lblSupplierID" Visible="false" runat="server" Text='<%# Eval("SupplierID")%>'></asp:Label>

                                                                    <asp:LinkButton ID="lnkvendorName" CommandName="Details" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"  runat="server" Text='<%# Eval("SupplierName")%>' Style="text-decoration:underline; font-weight:bold"></asp:LinkButton>  
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="6%" />
</asp:TemplateField>
                                                             <asp:TemplateField><HeaderTemplate>
                                                                  Total Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbl_totalamount" runat="server" Text='<%# Eval("TotalAmount", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="2%" />
</asp:TemplateField>
                                                            <asp:TemplateField><HeaderTemplate>
                                                                    Paid Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lbl_paidamount" Text='<%# Eval("TotalPaidAmount", "{0:0#.##}") %>' runat="server"></asp:Label>
                                                                 
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="2%" />
</asp:TemplateField>
                                                            <asp:TemplateField><HeaderTemplate>
                                                                 Due Amount(₹)
                                                                
</HeaderTemplate>
<ItemTemplate>
                                                                    <asp:Label ID="lblrec" runat="server" Text='<%# Eval("TotalDueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="2%" />
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
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-left">
												<span id="Span5" class="input-group-addon cusspan" runat="server">Total Amount(₹)</span>
												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_TotalAmount"></asp:TextBox>
												</div>
									</div>
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-right">
												<span id="Span3" class="input-group-addon cusspan" runat="server">Total paid(₹)</span>
												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_TotalPaid"></asp:TextBox>
												</div>
									</div>
									<div class="col-sm-4">
									
											<div class="form-group input-group pull-right">
												<span id="Span4" class="input-group-addon cusspan" runat="server">Total Due(₹)</span>
												<asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
													ID="txt_TotalDue"></asp:TextBox>

												</div>
									</div>
									
							</div>
                           </contenttemplate>
                        </div>

                    </ContentTemplate>

                </asp:TabPanel>
                <asp:TabPanel ID="tabvendorrecord" runat="server" CssClass="Tab2" HeaderText="Payment Counter">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div3">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg2" runat="server">
                                            <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span6" class="input-group-addon cusspan" runat="server">Supplier</span>
                                                <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_tap2supplier" ReadOnly="true"></asp:TextBox>
                                                <asp:TextBox runat="server" Visible="false"
                                                    ID="txt_tap2supplierID" ReadOnly="true"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span7" class="input-group-addon cusspan" runat="server">Date From </span>


                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap2datefrom" ReadOnly="true"></asp:TextBox>


                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span24" class="input-group-addon cusspan" runat="server">Date To </span>

                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap2dateTo"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span16" class="input-group-addon cusspan" runat="server">Payment No </span>

                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_PaymentNo"></asp:TextBox>

                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="fixeddiv">
                                                <div class="row fixeddiv" id="divmsg4" runat="server">
                                                    <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cusrow pad-top ">
                                        <div class="col-sm-12">
                                            <div>
                                                <div class="pbody">
                                                    <div class="grid" style="float: left; width: 100%; overflow: auto; min-height: 30vh;">
                                                        <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvvendorrecordlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." OnRowDataBound="gvvendorrecordlist_RowDataBound"
                                                                    Width="100%" HorizontalAlign="Center">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <a href="JavaScript:ItemChildGridview('div<%# Eval("InvoiceNo") %>');">
                                                                                    <img alt="Detail" id='imgdiv<%# Eval("InvoiceNo") %>' src="../Images/plus.gif" width="20px" />
                                                                                </a>
                                                                                <div id='div<%# Eval("InvoiceNo") %>' style="display: none;">
                                                                                    <asp:GridView ID="GridChild1" runat="server" AutoGenerateColumns="false" DataKeyNames="InvoiceNo"
                                                                                        CssClass="ChildGrid">
                                                                                        <Columns>
                                                                                            <asp:BoundField ItemStyle-Width="293px" DataField="ItemName" HeaderText="Item" />
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="NoUnit" DataFormatString="{0:0#.##}" HeaderText="Unit" />
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="QtyPerUnit" HeaderText="Qty/Unit" />
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="BatchNo" HeaderText="BatchNo" />
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="ExpiryDate" HeaderText="ExpiryDt." />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="MRP" DataFormatString="{0:0#.##}" HeaderText="MRP(₹)" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="Rate" DataFormatString="{0:0#.##}" HeaderText="Rate(₹)" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="Taxable" DataFormatString="{0:0#.##}" HeaderText="Taxable" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="CGST" DataFormatString="{0:0#.##}" HeaderText="CGST(%)" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="SGST" DataFormatString="{0:0#.##}" HeaderText="SGST(%)" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="TotalCP" DataFormatString="{0:0#.##}" HeaderText="Total(₹)" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="RecievedBy" HeaderText="Recieved By" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="RecievedDate" HeaderText="Recieved Date" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Sl No.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex+1%>
                                                                                <asp:Label ID="lblVendorPayment_ID" Visible="false" runat="server" Text='<%# Eval("VendorPayment_ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Invoice No.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_InvoiceNo" runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Reciept No.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceiptNo" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("ReceiptNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Supplier
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_SupplierID" Visible="false" runat="server" Text='<%# Eval("SupplierID")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_SupplierName" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Added On 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_ItemAddedOn" runat="server" Text='<%# Eval("ItemAddedDate")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Amount
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%# Eval("Amount","{0:0#.##}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Paid(₹)
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_PaidAmount" runat="server" Text='<%# Eval("PaidAmount","{0:0#.##}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Payable(₹)
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_DueAmount" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("DueAmount","{0:0#.##}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Paid Amount
																				
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_gvpaidamount" runat="server" ReadOnly="true"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Due(₹)
																				
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_gvdueamount" runat="server" ReadOnly="true"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Select
																				<asp:CheckBox ID="chkpaidall" runat="server" OnCheckedChanged="paidall_CheckedChanged" AutoPostBack="true" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkpaid" runat="server" OnCheckedChanged="paid_CheckedChanged" AutoPostBack="true" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>



                                                                    </Columns>


                                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="gvvendorrecordlist" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel runat="server" DefaultButton="btnpaid">
                                        <div class="row">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span14" class="input-group-addon cusspan" runat="server">Amount(₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_tap2totalamount"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span15" class="input-group-addon cusspan" runat="server">Payable Amount(₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_tap2payableamount"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span12" class="input-group-addon cusspan" runat="server">Due Amount(₹) </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                                        ID="txt_tap2dueamount"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span9" class="input-group-addon cusspan" runat="server">Paid Amount(₹) </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="10"
                                                        ID="txt_tap2Paidamount" onfocus="this.select();" AutoPostBack="true" OnTextChanged="txt_tap2Paidamount_TextChanged"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_tap2Paidamount" ID="FilteredTextBoxExtender1"
                                                        runat="server" ValidChars="., " FilterType="Custom, Numbers"
                                                        Enabled="True">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span13" class="input-group-addon cusspan" runat="server">Payment Mode </span>
                                                    <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="ddl_tap2paymentmode" AutoPostBack="true" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span8" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                                    <asp:HiddenField runat="server" ID="hdnbankID" />
                                                    <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtbank"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span17" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
                                                    <asp:Label ID="lbl_accno" Visible="False" runat="server"></asp:Label>
                                                    <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_chequenumber"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span26" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtinvoicenumber"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-9">
                                                <div class="form-group input-group">
                                                    <span id="Span11" class="input-group-addon cusspan" runat="server">Remark </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="200"
                                                        ID="txt_tap2remark"></asp:TextBox>
                                                </div>
                                            </div>


                                            <div class="col-sm-3">
                                                <div class="form-group input-group pull-right">
                                                    <asp:Button ID="btnpaid" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Paid" OnClick="btnpaid_Click" />
                                                    <asp:Button ID="btnpaidprint" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Print" OnClick="btnpaidprint_Click" />
                                                </div>
                                            </div>

                                        </div>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </ContentTemplate>

                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Payment List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div4">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg5" runat="server">
                                            <asp:Label ID="lblmessage3" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span20" class="input-group-addon cusspan" runat="server">Payment No </span>

                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap3paymentno" MaxLength="15"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtpaymentno_AutoCompleteExtender" runat="server"
                                                    ServiceMethod="GetVendorpaymentno" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_tap3paymentno"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_tap3paymentno" ID="FilteredTextBoxExtender2"
                                                    runat="server" ValidChars=". |:-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span18" class="input-group-addon cusspan" runat="server">Date From </span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap3datefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                    TargetControlID="txt_tap3datefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_tap3datefrom" />


                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span19" class="input-group-addon cusspan" runat="server">Date To </span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap3dateto"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                    TargetControlID="txt_tap3dateto" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_tap3dateto" />

                                            </div>
                                        </div>


                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span27" class="input-group-addon cusspan" runat="server">Supplier </span>
                                                <asp:DropDownList ID="ddl_tap3supplier" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>

                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span25" class="input-group-addon cusspan" runat="server">payment Mode </span>
                                                <asp:DropDownList ID="ddl_tap3paymentmode" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span30" class="input-group-addon cusspan" runat="server">Status </span>
                                                <asp:DropDownList ID="ddl_tap3status" runat="server" class="form-control input-sm col-sm custextbox">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:Button ID="btntap3search" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btntap3search_Click" />
                                                <asp:Button ID="btntap3print" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm  cusbtn" Text="Print" OnClick="btntap3print_Click" />
                                                <asp:Button ID="btntap3reset" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btntap3reset_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="fixeddiv">
                                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                                    <asp:Label ID="lblresult3" runat="server" Height="13px"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cusrow pad-top ">
                                        <div class="col-sm-12">
                                            <div>
                                                <div class="pbody">
                                                    <div class="grid" style="float: left; width: 100%; overflow: auto; height: 50vh;">
                                                        <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvpaymentlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." OnRowDataBound="gvpaymentlist_RowDataBound" OnRowCommand="gvpaymentlist_RowCommand"
                                                                    Width="100%" HorizontalAlign="Center">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                                            <ItemTemplate>
                                                                                <a href="JavaScript:ItemChildGridview('div<%# Eval("PaymentNo") %>');">
                                                                                    <img alt="Detail" id='imgdiv<%# Eval("PaymentNo") %>' src="../Images/plus.gif" width="20px" />
                                                                                </a>
                                                                                <div id='div<%# Eval("PaymentNo") %>' style="display: none;">
                                                                                    <asp:GridView ID="GridChild" runat="server" AutoGenerateColumns="false" DataKeyNames="PaymentNo"
                                                                                        CssClass="ChildGrid">
                                                                                        <Columns>
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="InvoiceNo" HeaderText="InVoice No." />
                                                                                            <asp:BoundField ItemStyle-Width="93px" DataField="ReceiptNo" HeaderText="ReceiptNo" />
                                                                                            <asp:BoundField ItemStyle-Width="223px" DataField="SupplierName" HeaderText="Supplier" />
                                                                                            <asp:BoundField ItemStyle-Width="100px" DataField="ItemAddedOn" HeaderText="Added On" />
                                                                                            <asp:BoundField ItemStyle-Width="160px" DataField="TotalAmount" HeaderText="Amount(₹)"
                                                                                                DataFormatString="₹.{0:###,###,###.00}" HtmlEncode="False" />
                                                                                            <asp:BoundField ItemStyle-Width="160px" DataField="TotalPaidAmount" HeaderText="Paid Amount(₹)"
                                                                                                DataFormatString="₹.{0:###,###,###.00}" HtmlEncode="False" />
                                                                                            <asp:BoundField ItemStyle-Width="160px" DataField="DueAmount" HeaderText="Due Amount(₹)"
                                                                                                DataFormatString="₹.{0:###,###,###.00}" HtmlEncode="False" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Payment No.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_paymentNo" runat="server" Text='<%# Eval("PaymentNo")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_headerID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Amount
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%# Eval("Amount","{0:0#.##}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Paid
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_PaidAmount" runat="server" Text='<%# Eval("PaidAmount","{0:0#.##}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Due
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_DueAmount" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("DueAmount","{0:0#.##}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Payment Mode
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_tap3paymentmode" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("Paymenttype") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Paid By
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_tap3paidby" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Remark
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_tap3remark" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("PaymentRemark") %>'></asp:Label>
                                                                                <asp:TextBox ID="txt_tap3remarks" Width="150px" Height="18px" Visible="false" runat="server" Text='<%# Eval("Remark")%>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Paid On
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_tap3paidon" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("PaymentOn") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="cus-Delete-header">Print</span>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkdupilactereiept" runat="server" CommandName="DuplicateReciept" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                           <i class="fa fa-print cus-delete-color"></i>
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


                                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="gvpaymentlist" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span21" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap3totalpaid"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span22" class="input-group-addon cusspan" runat="server">Due Amount(₹) </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_tap3totaldue"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>



                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </ContentTemplate>

                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
