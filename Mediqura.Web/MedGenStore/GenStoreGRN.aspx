<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="GenStoreGRN.aspx.cs" Inherits="Mediqura.Web.MedGenStore.GenStoreGRN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function calculate1() {

            var txtcpbeforeTax = document.getElementById("<%=txt_CpberforeTax.ClientID%>").value;
            var txt_Tax = document.getElementById("<%=txt_tax.ClientID%>").value;
            var txt_qtyperunit = document.getElementById("<%=txt_qty.ClientID%>").value;
            var txt_MRP = document.getElementById("<%=txt_mrp.ClientID%>").value;

            var txt_rate = document.getElementById("<%=txt_rateperqty.ClientID%>").value;
            document.getElementById("<%=txt_totalCPbeforeDisc.ClientID%>").value = (txt_qtyperunit * txt_rate).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var txt_discountperqty = document.getElementById("<%=txt_discountperqty.ClientID%>").value;
            if (document.getElementById("<%=ddl_discountype.ClientID%>").value == "1") {

                if (+(txt_discountperqty / 100 * (txt_qtyperunit * txt_rate) <= +(txt_qtyperunit * txt_rate))) {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = (+txt_qtyperunit * txt_rate - +(txt_discountperqty / 100 * (txt_qtyperunit * txt_rate))).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

                }
                else {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = "";
                    alert("Discount amount could not be grater than total cost prize.");
                }
            }
            if (document.getElementById("<%=ddl_discountype.ClientID%>").value == "2") {
                if (+(txt_discountperqty) <= +(txt_qtyperunit * txt_rate)) {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = +txt_qtyperunit * txt_rate - +txt_discountperqty;
                }
                else {
                    document.getElementById("<%=txt_CpberforeTax.ClientID%>").value = "";
                    alert("Discount amount could not be grater than total cost prize.");
                }
            }
            var totalqty = txt_qtyperunit;
            var myResult1 = (txtcpbeforeTax * txt_Tax / 100).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var myResult = (+txtcpbeforeTax + +myResult1).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var cpperqty = (myResult / totalqty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var txt_freeqty = document.getElementById("<%=txt_freeqty.ClientID%>").value;
            var Nettotalqty = +totalqty + +txt_freeqty;
            var totalmrp = (txt_MRP * totalqty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            document.getElementById("<%=txt_taxamount.ClientID%>").value = myResult1;
            document.getElementById("<%=txt_totalcp.ClientID%>").value = myResult;
            document.getElementById("<%=txt_cp.ClientID%>").value = cpperqty
            document.getElementById("<%=txt_totalreceivedqty.ClientID%>").value = Nettotalqty;

            document.getElementById("<%=txt_totalmrp.ClientID%>").value = totalmrp;

        }
    </script>
    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
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
                            <div class="row">
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
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_PONo" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz/-()" Enabled="True"></asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server"
                                            ServiceMethod="GetPONo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_PONo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Batch No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_batchno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Item Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="txt_itemname_TextChanged" AutoPostBack="True"
                                            ID="txt_itemname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span40" class="input-group-addon cusspan" runat="server">Unit</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_Unit"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Mfg.Company </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_company">
                                        </asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetCompanyName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_company"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Supplier <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_supplier">
                                        </asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetSupplierName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_supplier"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Received Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_receiveddate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_receiveddate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_receiveddate" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">MFG Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_mfgdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_mfgdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_mfgdate" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Expiry Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_expdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_expdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_expdate" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span46" class="input-group-addon cusspan" runat="server">Temperature</span>
                                        <asp:TextBox runat="server" MaxLength="5" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_temp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span44" class="input-group-addon cusspan" runat="server">Rate Per Qty <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="10" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_rateperqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txt_rateperqty" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Number of Qty <span
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
                                        <span id="Span43" class="input-group-addon cusspan" runat="server">Total C.P(₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="25" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalCPbeforeDisc"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_CpberforeTax" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span39" class="input-group-addon cusspan" runat="server">Discount Type</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="ddl_discountype">
                                            <asp:ListItem Value="1">%</asp:ListItem>
                                            <asp:ListItem Value="2">Value</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Discount</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" MaxLength="5" Style="z-index: 3"
                                            ID="txt_discountperqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txt_discountperqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span45" class="input-group-addon cusspan" runat="server">Total C.P(₹) - Disc.</span>
                                        <asp:TextBox runat="server" MaxLength="25" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_CpberforeTax"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txt_totalcp" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Tax(%) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_tax" MaxLength="5"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_tax" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan" runat="server">Tax Amount 
                                        </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_taxamount" MaxLength="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_TotalCP" class="input-group-addon cusspan" runat="server">Total C.P(₹) +Tax <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="25" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalcp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txt_totalcp" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">CP/qty(₹)</span>
                                        <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_cp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" ValidChars="0123456789." TargetControlID="txt_cp"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                              
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">M.R.P per Qty.(₹)</span>
                                         <asp:TextBox runat="server" onkeyup="return calculate1();" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_mrp"></asp:TextBox>
                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txt_mrp" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                 </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Total M.R.P(₹)</span>
                                        <asp:TextBox runat="server" MaxLength="10"  CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalmrp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txt_totalmrp" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Free Qty</span>
                                        <asp:TextBox MaxLength="5" onkeyup="return calculate1();" runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_freeqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txt_freeqty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" placeholder="Total Qty. Recieved" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalreceivedqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Class="btn  btn-sm scusbtn" />
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan" runat="server">Rack Grp <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_rackgrp_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_rackgrp">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span39" class="input-group-addon cusspan" runat="server">Rack SubGrp <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_racksubgrp">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                               
                            </div>--%>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Item List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
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
                                                                            <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_batchno" Visible="false" Text='<%# Eval("BatchNo") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_receiptNo" Visible="false" Text='<%# Eval("ReceiptNo") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_PONos" Visible="false" Text='<%# Eval("PONo") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_rateperQty" Visible="false" Text='<%# Eval("RatePerQty") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_temp" Visible="false" Text='<%# Eval("Temperature") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_discountperQty" Visible="false" Text='<%# Eval("DiscountPerQty") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_cpafterdisc" Visible="false" Text='<%# Eval("TotalCPBeforeDisc") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_discounttype" Visible="false" Text='<%# Eval("DiscountType") %>' runat="server"></asp:Label>
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
                                                                            <asp:Label ID="lbl_rackgrp" Visible="false" Text='<%# Eval("RackGroup") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_racksubgrp" Visible="false" Text='<%# Eval("RackSubGroup") %>' runat="server"></asp:Label>
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
                                                                            Total CP(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_cpbeforTax" Visible="false" runat="server" Text='<%# Eval("TotalCPbeforeTax","{0:0#.##}")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP","{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Total MRP(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_totalmrp" runat="server" Text='<%# Eval("TotalMRP","{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Tax(%)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_tax" runat="server" Text='<%# Eval("Tax")%>'></asp:Label>
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
                                                            <asp:AsyncPostBackTrigger ControlID="gvstocklist" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_recivedqty"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_recivedqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_recivedqty" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Free Qty</span>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalfreeqty"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txt_totalfreeqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_totalfreeqty" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span34" class="input-group-addon cusspan" runat="server">Total Recvd. Qty</span>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_total_recvqty"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txt_total_recvqty" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_total_recvqty" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Total C.P(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalamount"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_totalamount" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_totalamount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Total M.R.P(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalMRPS"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_totalMRPS" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_totalMRPS" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Total Discount(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_discount"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_discount" ValidChars="0123456789." Enabled="True"></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_discount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-md-3">
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
                                    <asp:Button ID="btnsave" runat="server"  Text="Save" Class="btn  btn-sm cusbtn"
                                        OnClick="btnsave_Click" />
                                    <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Stock Detail List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Reciept No.</span>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_recdno"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                    ServiceMethod="GetrecdNo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_recdno"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_recdno" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">PO No.</span>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txtPONo"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                    ServiceMethod="GetPONo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtPONo"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtPONo" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Stock No</span>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txtstockno"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server"
                                                    ServiceMethod="GetPONo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtstockno"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtPONo" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span35" class="input-group-addon cusspan" runat="server">Group </span>
                                        <asp:DropDownList runat="server" ID="ddl_itemgroup" AutoPostBack="true" OnSelectedIndexChanged="ddl_itemgroup_SelectedIndexChanged" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span36" class="input-group-addon cusspan" runat="server">Sub.Group </span>
                                        <asp:DropDownList runat="server" ID="ddl_subgroup" AutoPostBack="true" OnSelectedIndexChanged="ddl_itemsubgroup_SelectedIndexChanged" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Item Name</span>

                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtItemName"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"
                                            ServiceMethod="GetItemNames" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtItemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Mfg.company </span>
                                        <asp:TextBox runat="server" ID="txt_comp" CssClass="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server"
                                            ServiceMethod="GetCompanyName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_comp"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Supplier </span>
                                        <asp:TextBox runat="server" ID="txt_suppl" CssClass="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server"
                                            ServiceMethod="GetSupplierName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_suppl"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span38" class="input-group-addon cusspan" runat="server">Status </span>
                                        <asp:DropDownList runat="server" ID="ddl_Status" CssClass="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
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
                                <div class="col-sm-4">
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
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span37" class="input-group-addon cusspan" runat="server">Recieved By</span>
                                        <asp:DropDownList ID="ddl_stockrecievedby" runat="server" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span41" class="input-group-addon cusspan" runat="server">Rack Grp </span>
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_seacrhrackgrp_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_seacrhrackgrp">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span42" class="input-group-addon cusspan" runat="server">Rack SubGrp </span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_searchracksubgrp">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" Visible="false" Class="btn  btn-sm cusbtn" Text="Print" />
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
                                        <%--<div class="pbody">--%>
                                        <div class="grid" style="float: left; width: 100%; overflow: auto">
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
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
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
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Total CP(₹)
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP", "{0:0#.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_totcp" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            CP/Unit(₹)
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_cpperunit" runat="server" Text='<%# Eval("CPPerUnit", "{0:0#.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_totcpperunit" Visible="false" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
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
                                                            TMRP(₹)
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_totalmrp" runat="server" Text='<%# Eval("TotalMRP", "{0:0#.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_totmrp" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            MRPU(₹)
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_mrpperunit" runat="server" Text='<%# Eval("MRPPerUnit", "{0:0#.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_totmrpperunit" Visible="false" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            MRPQ(₹)
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_mrpperqty" runat="server" Text='<%# Eval("MRPPerQty", "{0:0#.##}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_totmrpperqty" Visible="false" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
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
                                                            Condemn Qty.
                                                            <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_totalcondemned" ReadOnly="true" Width="77px" Height="18px" runat="server" Text='<%# Eval("TotalCondemned")%>'> </asp:TextBox>
                                                            <asp:CheckBox ID="chekboxselect" runat="server" onclick="Check_Click(this);" AutoPostBack="true" OnCheckedChanged="chekboxselect_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="txt_totcondemned" runat="server" />
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
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
                                                            <asp:Label ID="lbl_totcpremitem" runat="server" />
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
                                                            Rack Detail
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_rackdetail" runat="server" Text='<%# Eval("RackDetail")%>'></asp:Label>
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
                                                <FooterStyle BackColor="#e8e8e8" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />
                                            </asp:GridView>
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
