<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="StockGRN.aspx.cs" Inherits="Mediqura.Web.MedStore.StockGRN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function calculate1() {

            var txt_nounit = document.getElementById("<%=txt_no0funit.ClientID%>").value;
            var txt_QtyPerunit = document.getElementById("<%=txt_qty.ClientID%>").value;
        	var txt_CP = document.getElementById("<%=txt_totalcp.ClientID%>").value;        	
            var totalqty = txt_QtyPerunit * txt_nounit;
            var txt_discountperqty = document.getElementById("<%=txt_discountperqty.ClientID%>").value;
            var txt_sgst = document.getElementById("<%=txt_sgst.ClientID%>").value;
            var txt_cgst = document.getElementById("<%=txt_cgst.ClientID%>").value;
            var txt_igst = document.getElementById("<%=txt_igst.ClientID%>").value;
            var txt_rateperunit = document.getElementById("<%=txt_rateperunit.ClientID%>").value;

            document.getElementById("<%=txt_totalcp.ClientID%>").value = ((+txt_rateperunit) * (+txt_nounit)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
			 
            if (document.getElementById("<%=ddl_discountype.ClientID%>").value == "1") {

                if (+(txt_discountperqty / 100 * (txt_CP) <= +(txt_CP))) {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = ((+txt_rateperunit) * (+txt_nounit) - +(txt_discountperqty / 100 * (+txt_rateperunit) * (+txt_nounit))).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                }
                else {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = "";
                    alert("Discount amount could not be greater than  cost prize.");
                }
            }
            if (document.getElementById("<%=ddl_discountype.ClientID%>").value == "2") {

                if (+(txt_discountperqty) <= +(txt_CP)) {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = (+txt_rateperunit) * (+txt_nounit) - +txt_discountperqty;
                }
                else {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = "";
                    alert("Discount amount could not be greater than  cost prize.");
                }
            }
            var txt_freeqty = document.getElementById("<%=txt_freeqty.ClientID%>").value;
            var Nettotalqty = +totalqty + (+txt_freeqty * txt_QtyPerunit);
            document.getElementById("<%=txt_totalreceivedqty.ClientID%>").value = Nettotalqty;
            var TaxableAmount = document.getElementById("<%=txt_CpberforeTax.ClientID%>").value;
            document.getElementById("<%=txt_RateafterTax.ClientID%>").value = (+TaxableAmount + ((TaxableAmount * (+txt_sgst / 100)) + (TaxableAmount * (+txt_cgst / 100)) + (TaxableAmount * (+txt_igst / 100)))).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var AmountAfterTax = document.getElementById("<%=txt_RateafterTax.ClientID%>").value;
            document.getElementById("<%=txt_cp.ClientID%>").value = (+AmountAfterTax / (+totalqty)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var txt_mrpperunit = document.getElementById("<%=txt_mrpperunit.ClientID%>").value;
            document.getElementById("<%=txt_totalmrp.ClientID%>").value = (+txt_mrpperunit * +txt_nounit).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var TotalMRP = document.getElementById("<%=txt_totalmrp.ClientID%>").value;
            document.getElementById("<%=txt_mrp.ClientID%>").value = (+TotalMRP / (+totalqty)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
        }
    	
        function calculate2() {
       
        	document.getElementById("<%=txt_paidamount.ClientID%>").value="";
        	document.getElementById("<%=txt_due.ClientID%>").value="";
        	var txt_totalCP = document.getElementById("<%=txt_totalamount.ClientID%>").value;
        	var txt_totalDisc = document.getElementById("<%=txt_discount.ClientID%>").value;
        	var totalreturnamt = document.getElementById("<%=txttotalreturnamt.ClientID%>").value == "" ? "0" : document.getElementById("<%=txttotalreturnamt.ClientID%>").value; 
            if (+txt_totalCP >= (+txt_totalDisc)) {
            	document.getElementById("<%=txt_nettotalCp.ClientID%>").value = (+txt_totalCP - (+txt_totalDisc)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            	var tot = (+txt_totalCP - (+txt_totalDisc) - (+totalreturnamt)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            	document.getElementById("<%=txt_payableamt.ClientID%>").value = (+txt_totalCP - (+txt_totalDisc) - (+totalreturnamt)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0]; 
            	
            }
            else {
                document.getElementById("<%=txt_discount.ClientID%>").value = "";
            	document.getElementById("<%=txt_nettotalCp.ClientID%>").value = txt_totalCP;
            	document.getElementById("<%=txt_payableamt.ClientID%>").value = (+txt_totalCP - totalreturnamt).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            }
        }
        function calculate3() {
            var txt_PaidAmount = document.getElementById("<%=txt_paidamount.ClientID%>").value;
        	var txt_totalnetCP = document.getElementById("<%=txt_nettotalCp.ClientID%>").value;
        	var totalreturnamt = document.getElementById("<%=txttotalreturnamt.ClientID%>").value == "" ? "0" : document.getElementById("<%=txttotalreturnamt.ClientID%>").value;
        	var payable = document.getElementById("<%=txt_payableamt.ClientID%>").value == "" ? "0" : document.getElementById("<%=txt_payableamt.ClientID%>").value;
        	if (+payable >= (+txt_PaidAmount)) {
        		document.getElementById("<%=txt_due.ClientID%>").value = (payable - (+txt_PaidAmount)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            }
        	if (+payable < (+txt_PaidAmount)) {

                document.getElementById("<%=txt_due.ClientID%>").value = "";
            }
        }
    	function returncalculate1() {

    		var txt_nounit = document.getElementById("<%=txtreturnQty.ClientID%>").value;
    		var TotalRecievedQty = document.getElementById("<%=TotalRecievedQty.ClientID%>").value;
    		if (+txt_nounit > (+TotalRecievedQty))
    		{
    			document.getElementById("<%=txtreturnQty.ClientID%>").value = "";
    			alert("Return Quantity could not be greater than  Recieved Quantity.");
    		}
    		var txt_rateperunit = document.getElementById("<%=Rate.ClientID%>").value;
	        var txt_sgst = document.getElementById("<%=txt_returnsgst.ClientID%>").value;
		 	var txt_cgst = document.getElementById("<%=txt_returncgst.ClientID%>").value;
		 	var txt_igst = document.getElementById("<%=txt_returnigst.ClientID%>").value;
		 	document.getElementById("<%=txt_returnamt.ClientID%>").value = ((+txt_rateperunit) * (+txt_nounit)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
		    var TaxableAmount = document.getElementById("<%=txt_returnamt.ClientID%>").value;
		 	document.getElementById("<%=txt_returnNetamnt.ClientID%>").value = (+TaxableAmount + ((TaxableAmount * (+txt_sgst / 100)) + (TaxableAmount * (+txt_cgst / 100)) + (TaxableAmount * (+txt_igst / 100)))).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
    	}   	

    	function returncalculate2() {
						
			var txt_sgst = document.getElementById("<%=txt_returnsgst.ClientID%>").value;
		 	var txt_cgst = document.getElementById("<%=txt_returncgst.ClientID%>").value;
			var txt_igst = document.getElementById("<%=txt_returnigst.ClientID%>").value;
			
		 	var TaxableAmount = document.getElementById("<%=txt_returnamt.ClientID%>").value;
			document.getElementById("<%=txt_returnNetamnt.ClientID%>").value = (+TaxableAmount + ((TaxableAmount * (+txt_sgst / 100)) + (TaxableAmount * (+txt_cgst / 100)) + (TaxableAmount * (+txt_igst / 100)))).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
		}

    </script>
    <%-- MODAL --%>
    <script type="text/javascript">
        function cancelClick() {
        	$find('modalbehavior').hide();
        	document.getElementById("<%=txtReturnItem.ClientID%>").value="";
        	
        }


    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerGRN" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabstockGRN" runat="server" CssClass="Tab1" HeaderText="Goods Received Notes(Stock Register)">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelGRN">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row" style="margin-top: -5px !important;">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span26" class="input-group-addon cusspan" runat="server">Purchase Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_purchagetype" OnSelectedIndexChanged="ddlpurchagetype_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="With PO"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Without PO"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">PO No. <span id="idpo" visible="false" runat="server"
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_PONo"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Receipt No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="100" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_receivedno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span27" class="input-group-addon cusspan" runat="server">Group <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlgroup">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Item Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="txt_itemname_TextChanged" AutoPostBack="True" 
                                            ID="txt_itemname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem"  CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblHSNCode" class="input-group-addon cusspan" runat="server">HSN/SAC<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtHSNCode"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Batch No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_batchno"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Mfg Company</span>
                                        <asp:DropDownList ID="ddl_mfgcompnay" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Supplier <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_Supplier" runat="server" OnSelectedIndexChanged="ddl_Supplier_SelectedIndexChanged" AutoPostBack="true" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                        <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_supplier">
                                        </asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetSupplierName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_supplier"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3" style="margin-bottom: 0px !important;">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Received Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_receiveddate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_receiveddate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_receiveddate" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Expiry Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_expdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_expdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_expdate" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span42" class="input-group-addon cusspan" runat="server">Temperature(°C) </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="txt_temperature"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">No of Unit | Strip <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="txt_no0funit"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txt_no0funit" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
								<div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblfreeqty" class="input-group-addon cusspan" runat="server">Free Qty</span>
                                        <asp:TextBox MaxLength="5" placeholder="Free Qty" runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_freeqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txt_freeqty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Qty/(Unit | Strip) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="5" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_qty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt_qty" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                

                            </div>
                            <div class="row">
								<div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Rate/(unit | Strip(₹)) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="25" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_rateperunit"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_rateperunit" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_TotalCP" class="input-group-addon cusspan" runat="server">Total C.P | Rate(₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="25" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalcp"></asp:TextBox>
										 
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txt_totalcp" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span41" class="input-group-addon cusspan" runat="server">Discount Type</span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_discountype_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="ddl_discountype">
                                            <asp:ListItem Value="1">%</asp:ListItem>
                                            <asp:ListItem Value="2">Value</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span40" class="input-group-addon cusspan" runat="server">Discount</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="txt_discountperqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_discountperqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span45" class="input-group-addon cusspan" runat="server">Taxable Amount</span>
                                        <asp:TextBox runat="server" MaxLength="25" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_CpberforeTax"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txt_CpberforeTax" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
								<div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span43" class="input-group-addon cusspan" runat="server">CGST(%)</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();" onchange="return GST1();" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_cgst"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" ValidChars="0123456789." TargetControlID="txt_cgst" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span44" class="input-group-addon cusspan" runat="server">SGST(%)</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();"  MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_sgst"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" ValidChars="0123456789." TargetControlID="txt_sgst" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span46" class="input-group-addon cusspan" runat="server">IGST(%)</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();"  MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_igst"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" ValidChars="0123456789." TargetControlID="txt_igst" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">(CP | Rate) + Tax</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_RateafterTax"></asp:TextBox>
                                    </div>
                                </div>
								<div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Rate per qty(₹)</span>
                                        <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_cp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" ValidChars="0123456789." TargetControlID="txt_cp" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan" runat="server">M.R.P/Unit | Strip(₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="10" onkeyup="return calculate1();" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_mrpperunit"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txt_mrpperunit" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Total M.R.P(₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="10" onkeyup="return calculate1();" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalmrp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txt_totalmrp" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">M.R.P per Qty.(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_mrp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">TottalReceived Qty</span>
                                        <asp:TextBox runat="server" placeholder="Total Recv Qty" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalreceivedqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Class="btn  btn-sm scusbtn" />
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnreturn" runat="server" Text="Add Return" Class="btn btn-sm cusbtn" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">
                                    <asp:ModalPopupExtender ID="mpconfirmation" runat="server" TargetControlID="btnsave" PopupControlID="popupwindow"
                                        BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupwindow" BackColor="#D1EEEE" Style="display: none;">

                                        <div class="row popup" style="width: 353px!important">
                                            <div class="col-sm-12 popup-header" style="width: 333px !important;">
                                                <div class="col-xs-11">
                                                    <h5 style="color: red;">Are you sure to save?</h5>
                                                </div>

                                            </div>
                                            <div class="col-sm-12 popup-div-msg" id="div_message" runat="server">
                                                <asp:Label ID="message" CssClass="popup-lbl-msg" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Tot. Recieved. :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totalrecievedqty" Style="font-weight: bold; font-size: 25px; text-align: center" CssClass="cuspopuptext" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Discount(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totaldiscount" Style="font-weight: bold; font-size: 25px; text-align: center" CssClass="cuspopuptext" runat="server"></asp:Label>
                                                    </div>
                                                </div>
												<div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Return Amt.(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totalreturnamt" Style="font-weight: bold; font-size: 25px; text-align: center" runat="server" class="cuspopuptext">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Paid Amt.(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totpaidamount" Style="font-weight: bold; font-size: 25px; text-align: center" runat="server" class="cuspopuptext">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Due Amt.(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totdueamount" Style="font-weight: bold; font-size: 25px; text-align: center" runat="server" class="cuspopuptext">
                                                        </asp:Label>
                                                    </div>
                                                </div>
												<div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Refund Amt.(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="Refundableamt" Style="font-weight: bold; font-size: 25px; text-align: center" runat="server" class="cuspopuptext">
                                                        </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="form-group input-group cuspanelbtngrp  pull-left">
                                                    <div class="col-md-12" style="text-align: right">
                                                        <asp:Button ID="btnyes" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..';" Text="Yes" Class="btn  btn-sm cusbtn" OnClick="btnyes_Click" />
                                                        <asp:Button ID="btnno" runat="server" Text="No" Class="btn  btn-sm cusbtn" OnClick="btnno_Click" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                            <%-- Return PopUp --%>
                            <div class="row cusrow" style="margin-top: -900px;">
                                <div class="col-sm-12">
                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="modalbehavior" runat="server" TargetControlID="btnreturn" PopupControlID="ReturnPopUpwindow"
                                        BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True" CancelControlID="closePopup" OnCancelScript="cancelClick();">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="ReturnPopUpwindow" BackColor="#D1EEEE" Style="display: none; margin-top: -20px; height: 400px;">
                                        <div class="row popup" style="width: 1100px; height: 50px !important;">
                                            <div class="col-sm-12" >
                                                <div class="row">
                                      
                                                        <div class="row">
                                                            <div class="col-sm-11" style="text-align: center; color: red;">
                                                                <h4><u>Return Item</u> </h4>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:HyperLink ID="closePopup" runat="server" Style="cursor:pointer" CssClass="ClosePopupCls">Close [x]</asp:HyperLink>
                                                            </div>
                                                        </div>
                                                   
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
															<span style="color:red;"> Item name</span>
                                                            <asp:TextBox runat="server" Width="500px" placeholder="Search Return item" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" onfocus="this.select();"
                                                                ID="txtReturnItem" OnTextChanged="txtReturnItem_TextChanged"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                ServiceMethod="GetReturnItemName" MinimumPrefixLength="1"
                                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtReturnItem"
                                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                            </asp:AutoCompleteExtender>

                                                        </div>
                                                    </div>
													<asp:Panel runat="server" DefaultButton="btnretunadd">
                                                    <div class="col-sm-1">
                                                        <div class="form-group input-group">
															<span style="color:red;"> Rtn Qty.</span>
                                                            <asp:TextBox runat="server" placeholder="Qty" Class="form-control input-sm col-sm custextbox"
                                                                ID="txtreturnQty" onkeyup="return returncalculate1();" MaxLength="5" ValidChars="0123456789"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="Rate"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="TotalRecievedQty"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="ID"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="BatchNo"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="ReceiptNo"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="ItemID"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="CompanyID"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="SupplierID"></asp:TextBox>
															<asp:TextBox runat="server" Style="display:none;" ID="ItemName"></asp:TextBox>
                                                        </div>
                                                    </div>
													
                                                    <div class="col-sm-1">
                                                        <div class="form-group input-group">
                                                          <span style="color:red;"> Taxable(₹)</span>
															  <asp:TextBox runat="server" onkeyup="return returncalculate2();" MaxLength="8" placeholder="Amount" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_returnamt" ValidChars="0123456789."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <div class="form-group input-group">
                                                           <span style="color:red;"> SGST(%)</span>
															 <asp:TextBox runat="server" onkeyup="return returncalculate2();" MaxLength="3" placeholder="SGST" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_returnsgst" ValidChars="0123456789."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <div class="form-group input-group">
                                                           <span style="color:red;"> CGST(%)</span>
															 <asp:TextBox runat="server" onkeyup="return returncalculate2();" MaxLength="3" placeholder="CGST" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_returncgst" ValidChars="0123456789."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <div class="form-group input-group">
                                                           <span style="color:red;"> IGST(%)</span>
															 <asp:TextBox runat="server" onkeyup="return returncalculate2();" MaxLength="3" placeholder="IGST" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_returnigst" ValidChars="0123456789."></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1 ">
                                                        <div class="form-group input-group">
                                                           <span style="color:red;"> Total(₹)</span>
															 <asp:TextBox runat="server"  placeholder="Net Amnt" disabled="disabled" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_returnNetamnt"></asp:TextBox>
                                                        </div>
                                                    </div>
													 <div class="col-sm-1 pull-right">
                                                        <div class="form-group input-group">
                                                           <asp:Button ID="btnretunadd" runat="server" Text="Add" OnClick="btnretunadd_Click" Class="btn  btn-sm scusbtn" />
                                                        </div>
                                                    </div>
														</asp:Panel>
													<div class="pbody">
                                            <div class="gridview-container-Lab">
                                                <div class="grid" style="float: left; width: 95%; height:30vh; overflow: auto">
													<asp:GridView ID="gvstockreturn" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table" OnRowCommand="gvstockreturn_RowCommand"
                                                        EmptyDataText="No record found..." OnRowDataBound="gvstockreturn_RowDataBound"
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
                                                                    <asp:Label ID="lblrtnserialID" Visible="false" Text="<%# Container.DataItemIndex+1%>" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblrtnID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    
                                                                    <asp:Label ID="lbl_rtnreceiptNo" Visible="false" Text='<%# Eval("ReceiptNo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_rtnItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_rtnCompanyID" Visible="false" Text='<%# Eval("CompanyID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_rtnSupplierID" Visible="false" Text='<%# Eval("SupplierID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblrtnparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                    
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Batch no
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   
                                                                   <asp:Label ID="lbl_rtnbatchno"  Text='<%# Eval("BatchNo") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rtn Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   
                                                                    <asp:Label ID="lbl_rtntotalquantity" runat="server" Text='<%# Eval("TotalQuantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Taxable(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtntaxable" runat="server" Text='<%# Eval("TaxableAmount","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    SGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtnsgst" runat="server" Text='<%# Eval("SGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtncgst" runat="server" Text='<%# Eval("CGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtnigst" runat="server" Text='<%# Eval("IGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    AfterTax
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtncpAfterTax" runat="server" Text='<%# Eval("CPafterTax")%>'></asp:Label>
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
													<div class="col-md-12 pull-right">
														
														<div class="col-md-3">
														<div class="form-group input-group">
                                            <span id="Span31" class="input-group-addon cusspan" runat="server">Return Qty.</span>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtgvreturnqty"></asp:TextBox>
                                        </div>
														</div>
														<div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span20" class="input-group-addon cusspan" runat="server">Return Amt.(₹)</span>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtgvreturnamt"></asp:TextBox>
                                        </div>
															</div>
                                    </div>
                                                </div>
												
                                            </div>
                                        </div>
										
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Item List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-Lab">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvstocklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."
                                                        Width="100%" HorizontalAlign="Center" OnRowDataBound="gvstocklist_RowDataBound" OnRowCommand="gvstocklist_RowCommand">
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
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblHSNCode" Visible="false" Text='<%# Eval("HSNCode") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_batchno" Visible="false" Text='<%# Eval("BatchNo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_receiptNo" Visible="false" Text='<%# Eval("ReceiptNo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_PONo" Visible="false" Text='<%# Eval("PONo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_CompanyID" Visible="false" Text='<%# Eval("CompanyID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_SupplierID" Visible="false" Text='<%# Eval("SupplierID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_NoOfUnit" Visible="false" Text='<%# Eval("NoOfUnit") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_QtyperUnit" Visible="false" Text='<%# Eval("QtyperUnit") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_CPperunit" Visible="false" Text='<%# Eval("CPperunit") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_MRPperunit" Visible="false" Text='<%# Eval("MRPperunit") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ReceivedDate" Visible="false" Text='<%# Eval("ReceivedDate","{0:dd-MM-yyyy}")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_MfgDate" Visible="false" Text='<%# Eval("MfgDate","{0:dd-MM-yyyy}")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ExpDate" Visible="false" Text='<%# Eval("ExpDate","{0:dd-MM-yyyy}")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_temp" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Temperature") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_free_ItemAmount" Visible="false" runat="server" Text='<%# Eval("FreeItemAmount")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_totalquantity" runat="server" Text='<%# Eval("TotalQuantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Free Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_freequantity" runat="server" Text='<%# Eval("FreeQuantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totalrecdquantity" runat="server" Text='<%# Eval("TotalRecdQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Balance.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_Equibalance" ReadOnly="true" Width="70px" runat="server" Text='<%# Eval("EquivalentQtyBalance", "{0:0#.##}")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    MRP(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totalmrp" runat="server" Text='<%# Eval("TotalMRP","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    MRP/Qty(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_mrpQty" runat="server" Text='<%# Eval("MRPPerQty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CP|Rate(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Disc(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_disc" runat="server" Text='<%# Eval("Discount","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Taxable(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_taxable" runat="server" Text='<%# Eval("TaxableAmount","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    SGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sgst" runat="server" Text='<%# Eval("SGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cgst" runat="server" Text='<%# Eval("CGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_igst" runat="server" Text='<%# Eval("IGST")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CP-Disc+Tax
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cpAfterTax" runat="server" Text='<%# Eval("CPafterTax")%>'></asp:Label>
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
                            </div>
                            <div class="row" style="height: 10px"></div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span32" class="input-group-addon cusspan" runat="server">Recieved Qty</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_recivedqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_recivedqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Free Qty</span>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalfreeqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt_totalfreeqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span34" class="input-group-addon cusspan" runat="server">Total Recvd. Qty</span>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_total_recvqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt_total_recvqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Total M.R.P(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalMRPS"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_totalMRPS" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnsave">
                                <div class="row">

                                    <div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">Total C.P(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_totalamount"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_totalamount" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span39" class="input-group-addon cusspan" runat="server">Due Amt.(₹)</span>
                                            <asp:TextBox runat="server" MaxLength="16" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_due"></asp:TextBox>
                                        </div>
                                    </div>
									
                                    <div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Net Total CP(₹)</span>
                                            <asp:TextBox runat="server" MaxLength="16" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_nettotalCp"></asp:TextBox>
											
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Return Amt.(₹)</span>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalreturnamt"></asp:TextBox>
                                        </div>
                                    </div> 
									
                                </div>
                                <div class="row">
									<div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Total Discount(₹)</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate2();" MaxLength="16" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_discount"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_discount" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
									<div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span29" class="input-group-addon cusspan" runat="server">Payable(₹) <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" MaxLength="16" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_payableamt" disabled="disabled"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" TargetControlID="txt_payableamt" ValidChars="0123456789.-" Enabled="True"></asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
									<div class="col-md-3">
                                        <div class="form-group input-group">
                                            <span id="Span47" class="input-group-addon cusspan" runat="server">Paid Amt.(₹) <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" onkeyup="return calculate3();" MaxLength="16" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_paidamount"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_paidamount" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-3 " style="display:none;">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Received By <span
                                                style="color: red">*</span></span>
                                            <asp:HiddenField ID="hdn_total_free_item_Amount" runat="server" />
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="ddl_receivedby">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..';" Text="Save" Class="btn  btn-sm cusbtn"
                                            OnClick="btnsave_Click" />
                                        <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />

                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Stock Detail List">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server">
                            <div class="custab-panel" id="Div1">
                                <contenttemplate>
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                    </div>
                                </div>
 
                                      <div class="row">
                                        <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Item Name</span>
                                       
                                                    <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="txtItemName_TextChanged" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                        ID="txtItemName"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"
                                                        ServiceMethod="GetItemNames" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtItemName"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
                                             </div>
                                    </div>
                                           <div class="row">
                                      <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span23" class="input-group-addon cusspan" runat="server">Recieved From  </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_recdfrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_recdfrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_recdfrom" />
                                        </div>
                                    </div>
                                      <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span24" class="input-group-addon cusspan" runat="server">Recieved To  </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_recdTo"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_recdTo" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_recdTo" />
                                        </div>
                                    </div>
                                </div>
                                    
                                      <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span37" class="input-group-addon cusspan" runat="server">Recieved By</span>
                                          <asp:DropDownList ID="ddl_stockrecievedby" runat="server" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                        </div>
                                    </div>
                                      <div class="col-sm-3">
                                        <div class="form-group input-group">
                                           <span id="Span38" class="input-group-addon cusspan" runat="server">Status </span>
                                           <asp:DropDownList runat="server" ID="ddl_Status"  CssClass="form-control input-sm col-sm custextbox" >
                                               <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                               <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                           </asp:DropDownList>
                                        </div>
                                    </div>
                                        <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                             <asp:Button ID="btnprint" runat="server" Class="btn  btn-sm cusbtn" Visible="false" Text="Print"  />
                                          <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 37vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                  <asp:GridView ID="gvstocklist1" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." ShowFooter="true" OnPageIndexChanging="gvstocklist1_PageIndexChanging" AllowPaging="true" PageSize="10"
                                                                Width="2000px" HorizontalAlign="Center" OnRowDataBound="gvstocklist1_RowDataBound" OnRowCommand="gvstocklist1_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Sl No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            Total
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Stock No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lbl_StockNO" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            RC No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_recdno" runat="server" Text='<%# Eval("ReceiptNo")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            PO No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_PONo" runat="server" Text='<%# Eval("PONo")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Item Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                             Qty
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("RecvQty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totalqty" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Free Qty
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_freequantity" runat="server" Text='<%# Eval("FreeQuantity")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totfreeqty" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Total Qty.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_totalrecdquantity" runat="server" Text='<%# Eval("TotalRecievedQty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totrecdqty" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Total CP(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CPafterTax", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totcp" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="3%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            CP/Qty(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_cpperqty" runat="server" Text='<%# Eval("CPPerQty", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totcpperqty" Visible="false" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Total MRP(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_totalmrp" runat="server" Text='<%# Eval("TotalMRP", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totmrp" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="3%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            MRP/Unit(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mrpperunit" runat="server" Text='<%# Eval("MRPPerUnit", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totmrpperunit" Visible="false" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            MRP/Qty(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mrpperqty" runat="server" Text='<%# Eval("MRPPerQty", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totmrpperqty" Visible="false" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Total Issued
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_totalissued" runat="server" Text='<%# Eval("TotalIssued")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totissued" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Condemn Qty. <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_totalcondemned" ReadOnly="true" Width="77px" Height="18px" runat="server" Text='<%# Eval("TotalCondemned")%>'> </asp:TextBox>
                                                                          <asp:CheckBox ID="chekboxselect" runat="server" onclick="Check_Click(this);" AutoPostBack="true" OnCheckedChanged="chekboxselect_CheckedChanged" />
                                                                             </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="txt_totcondemned" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField >
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            Balance
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_balstock" runat="server" Text='<%# Eval("BalStock")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totbalstock" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            CP(RItem)(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_cpremitem" runat="server" Text='<%# Eval("CPRemItem", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totcpremitem"  runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            MRP(RItem)(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mrpremitem" runat="server" Text='<%# Eval("MRPRemItem", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totmrpremitem" Visible="false" runat="server" />
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false">
                                                                        <HeaderTemplate>
                                                                            MFG Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mfddate" runat="server" Text='<%# Eval("MfgDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Expiry Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_expdate" runat="server" Text='<%# Eval("ExpDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Received Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_recddate" runat="server" Text='<%# Eval("ReceivedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Mfg.Company
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mfgcompany" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Supplier
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_supplier" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Received By
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_recdby" runat="server" Text='<%# Eval("RecdBy")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Remarks
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
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
                                                                <FooterStyle BackColor="#e8e8e8"/>
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                            </asp:GridView>
                                                      
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                              <div class="row">

                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">


                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                        runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>

                                    </div>



                                </div>
                            </contenttemplate>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
