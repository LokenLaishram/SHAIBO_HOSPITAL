<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OPSales.aspx.cs" Inherits="Mediqura.Web.MedPhr.OPSales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        var Page
        function pageLoad() {
            if (+document.getElementById("<%=hdnbillsubmittype.ClientID%>").value == "2") {
                document.getElementById("<%=btnsave.ClientID%>").value = "Send Req";
            }
            else {
                document.getElementById("<%=btnsave.ClientID%>").value = "Save";
            }
        }
        function Printreciept() {
            objbillno = document.getElementById("<%=txt_billNo.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PHROPbill&BillNo=" + objbillno.value)
        }
        function PrintDuplicatebills(billno) {
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PHROPbill&BillNo=" + billno)
        }
        function PrintOpbillList() {
            objbillno = document.getElementById("<%=txt_billnos.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_datefrom.ClientID %>")
            objdateTo = document.getElementById("<%=txt_dateto.ClientID %>")
            objpaymode = document.getElementById("<%=ddl_paymodes.ClientID %>")
            objcollectedby = document.getElementById("<%=ddl_collectedby.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PHROPbillList&BillNo=" + objbillno.value + "&Paymode=" + objpaymode.value + "&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateTo.value + "&CollectedBy=" + objcollectedby.value + "&Status=" + objcollectedby.value)
        }
        function calculate1() {
            var hdn_mrp_per_Qty = document.getElementById("<%=hdnmrpperqty.ClientID%>").value;
            var hdnequivalent = document.getElementById("<%=hdnequivalentqty.ClientID%>").value;
            var nounit = document.getElementById("<%=txt_nounit.ClientID%>").value
            document.getElementById("<%=txt_equivalentqty.ClientID%>").value = (+nounit * hdnequivalent).toString();
            document.getElementById("<%=txt_Rate.ClientID%>").value = (+document.getElementById("<%=txt_equivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var finalequiv = document.getElementById("<%=txt_equivalentqty.ClientID%>").value;
            var Totalavail = document.getElementById("<%=txt_totalavail.ClientID%>").value;
            if (+finalequiv > +Totalavail) {
                document.getElementById("<%=txt_nounit.ClientID%>").value = "1";
                document.getElementById("<%=txt_equivalentqty.ClientID%>").value = (1 * hdnequivalent).toString();
                document.getElementById("<%=txt_Rate.ClientID%>").value = (+document.getElementById("<%=txt_equivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                alert("Sales quantity exceeds Available Qty.");
                return false;
            }
        }
        function calculate2() {
            var hdn_mrp_per_Qty = document.getElementById("<%=hdnmrpperqty.ClientID%>").value;
            var hdnequivalent = document.getElementById("<%=hdnequivalentqty.ClientID%>").value;

            document.getElementById("<%=txt_nounit.ClientID%>").value = (+document.getElementById("<%=txt_equivalentqty.ClientID%>").value / hdnequivalent).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            document.getElementById("<%=txt_Rate.ClientID%>").value = (+document.getElementById("<%=txt_equivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var finalequiv = document.getElementById("<%=txt_equivalentqty.ClientID%>").value;
            var Totalavail = document.getElementById("<%=txt_totalavail.ClientID%>").value;

            if (+finalequiv > +Totalavail) {
                document.getElementById("<%=txt_nounit.ClientID%>").value = "1";
                document.getElementById("<%=txt_equivalentqty.ClientID%>").value = (1 * hdnequivalent).toString();
                document.getElementById("<%=txt_Rate.ClientID%>").value = (+document.getElementById("<%=txt_equivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                alert("Sales quantity exceeds Available Qty.");
                return false;
            }

        }
        function calculate3() {
            var totalbill = document.getElementById("<%=txt_totalamount.ClientID%>").value;
            var discountPC = document.getElementById("<%=txt_discountPC.ClientID%>").value;
            document.getElementById("<%=txt_dueAmount.ClientID%>").value = "";
            document.getElementById("<%=txt_discountvalue.ClientID%>").value = (+totalbill * (+discountPC / 100)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var discountvalue = (+totalbill * (+discountPC / 100)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            document.getElementById("<%=txt_PaidAmount.ClientID%>").value = Math.round((+totalbill) - (+discountvalue));
            if (+discountvalue > +totalbill) {
                document.getElementById("<%=txt_discountPC.ClientID%>").value = "";
                document.getElementById("<%=txt_discountvalue.ClientID%>").value = "";
                document.getElementById("<%=txt_PaidAmount.ClientID%>").value = totalbill;
                alert("Discount Exeeds total bill.");
                return false;
            }
            if (+discountvalue > 10) {
                document.getElementById("<%=btnsave.ClientID%>").value = "Send Req";
                document.getElementById("<%=hdnbillsubmittype.ClientID%>").value = "2";
            }
            else {
                document.getElementById("<%=btnsave.ClientID%>").value = "Save";
                document.getElementById("<%=hdnbillsubmittype.ClientID%>").value = "1";
            }
        }
        function calculate4() {
            var totalbill = document.getElementById("<%=txt_totalamount.ClientID%>").value;
            var discountValue = document.getElementById("<%=txt_discountvalue.ClientID%>").value;
            document.getElementById("<%=txt_dueAmount.ClientID%>").value = "";
            document.getElementById("<%=txt_discountPC.ClientID%>").value = (+discountValue / +totalbill * 100).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            document.getElementById("<%=txt_PaidAmount.ClientID%>").value = Math.round((+totalbill) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value));

            if (+document.getElementById("<%=txt_discountvalue.ClientID%>").value > +totalbill) {
                document.getElementById("<%=txt_discountPC.ClientID%>").value = "";
                document.getElementById("<%=txt_discountvalue.ClientID%>").value = "";
                document.getElementById("<%=txt_PaidAmount.ClientID%>").value = totalbill;
                alert("Discount Exeeds total bill.");
            }
            if (+document.getElementById("<%=txt_discountvalue.ClientID%>").value > 10) {
                document.getElementById("<%=btnsave.ClientID%>").value = "Send Req";
                document.getElementById("<%=hdnbillsubmittype.ClientID%>").value = "2";
            }
            else {
                document.getElementById("<%=btnsave.ClientID%>").value = "Save";
                document.getElementById("<%=hdnbillsubmittype.ClientID%>").value = "1";
            }

        }
        function calculate5() {

            var Apaidamount = (+document.getElementById("<%=txt_totalamount.ClientID%>").value) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);
            var CPaidamount = document.getElementById("<%=txt_PaidAmount.ClientID%>").value;
            var totalamount = document.getElementById("<%=txt_totalamount.ClientID%>").value;
            var discount = document.getElementById("<%=txt_discountvalue.ClientID%>").value;

            document.getElementById("<%=txt_dueAmount.ClientID%>").value = ((+totalamount - +discount) - +CPaidamount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var due = ((+totalamount - +discount) - +CPaidamount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            if (+CPaidamount > (+totalamount - +discount)) {
                document.getElementById("<%=txt_PaidAmount.ClientID%>").value = Math.round((+totalamount - +discount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0]);
                document.getElementById("<%=txt_dueAmount.ClientID%>").value = "";
            }
            if (+due > 0) {
                document.getElementById("<%=ddl_responsible.ClientID%>").disabled = false;
            }
            else {
                document.getElementById("<%=ddl_responsible.ClientID%>").value = 0;
                document.getElementById("<%=ddl_responsible.ClientID%>").disabled = true;
            }
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="Maintabcontainor" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabOPIssueReturn" runat="server" CssClass="Tab1" HeaderText="OP Sales">
                    <ContentTemplate>
                        <div class="custab-panel" id="divtab1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel ID="panel1" runat="server" DefaultButton="btnadd">
                                <div class="row">
                                    <div class="col-sm-9">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cus-long-span" runat="server">Custommer <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_custommer" AutoPostBack="true" OnTextChanged="txt_custommer_TextChanged"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdntransID" />
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_custommer"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cus-long-span" runat="server">Bill No </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_billNo"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnbillsubmittype" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-9">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cus-long-span" runat="server">Search Items  <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txt_drugsname_TextChanged" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_drugsname"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                ServiceMethod="GetDrugs" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_drugsname"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" Width="265px" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_nodayscounttoexpire"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cus-long-span" runat="server">Search Composition</span>
                                            <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txt_searchcomposition_TextChanged" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_composition"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="Getsearchbycomposition" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_composition"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:HiddenField runat="server" ID="hdnmrpperqty" />
                                            <asp:HiddenField runat="server" ID="hdnchkupdate" />
                                            <asp:HiddenField runat="server" ID="hdnequivalentqty" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cus-long-span" runat="server">Total Available Qty</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_totalavail"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_nounit" ID="FilteredTextBoxExtender4"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cus-long-span" runat="server">Sale in Unit</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate1();" onfocus="this.select();" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_nounit"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_nounit" ID="FilteredTextBoxExtender1"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cus-long-span" runat="server">Sale in Qty</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate2();" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_equivalentqty"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_equivalentqty" ID="FilteredTextBoxExtender3"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" placeholder="Rate (₹)" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_Rate"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnadd" OnClick="btnadd_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" runat="server" Text="ADD" Class="btn  btn-sm scusbtn" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
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
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Payment Mode :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_paymentmode" Style="font-weight: bold; font-size: 25px; text-align: center" CssClass="cuspopuptext" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-6" style="font-size: 20px;">Discount(₹) :</div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lbl_totaldiscount" Style="font-weight: bold; font-size: 25px; text-align: center" CssClass="cuspopuptext" runat="server"></asp:Label>
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
                            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                                <div class="row cusrow pad-top ">
                                    <label class="gridview-label">Item List</label>
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="gridview-container-Lab">
                                                    <div class="grid" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:GridView ID="GvItemlist" runat="server" OnRowCommand="GvItemlist_RowCommand" OnRowDataBound="GvItemlist_RowDataBound" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..."
                                                            Width="100%" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Sl.No.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblserialID" Text=' <%# Container.DataItemIndex+1%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Item Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_UHID" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbl_substockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>

                                                                        <asp:Label ID="lbl_trasactionID" Visible="false" Text='<%# Eval("TransactionID") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lbl_batch" Visible="false" Text='<%# Eval("BatchNo") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Expiry Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_expirydate" runat="server" Text='<%# Eval("ExpDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Rate
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_rate" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        No.Unit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_unit" runat="server" Text='<%# Eval("NoUnit", "{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Equiv.Qty.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("EquivalentQty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Net Charge
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_netcharges" runat="server" Text='<%# Eval("NetCharge", "{0:0#.##}")%>'></asp:Label>
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
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Payment Mode <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                            <asp:HiddenField runat="server" ID="hdnbankID" />
                                            <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                                ID="txtbank"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
                                            <asp:Label ID="lbl_accno" Visible="False" runat="server"></asp:Label>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_chequenumber"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span25" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtinvoicenumber"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Total Amount(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_totalamount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Discount (PC)</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate3();" MaxLength="3" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_discountPC"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_discountPC" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">Discount (Value)</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate4();" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_discountvalue"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_discountvalue" ID="FilteredTextBoxExtender5"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Paid Amount(₹)</span>
                                            <asp:TextBox runat="server" onkeyup="return calculate5();" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_PaidAmount"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_PaidAmount" ID="FilteredTextBoxExtender6"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span20" class="input-group-addon cusspan" runat="server">Due Amount</span>
                                            <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_dueAmount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Responsible By</span>
                                            <asp:DropDownList runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddl_responsible">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Remarks <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_remarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsave_Click" Text="Save" Class="btn  btn-sm cusbtn" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return Printreciept();" Class="btn  btn-sm scusbtn" />
                                            <asp:Button ID="btnresets" runat="server" Text="Reset" OnClick="btnresets_Click" Class="btn  btn-sm scusbtn" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Bill List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
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
                                                <span id="Span2" class="input-group-addon cusspan" runat="server">Bill No.</span>
                                                <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_billnos"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span23" class="input-group-addon cusspan" runat="server">From <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_datefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                    TargetControlID="txt_datefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_datefrom" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span24" class="input-group-addon cusspan" runat="server">To <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_dateto"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                    TargetControlID="txt_dateto" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dateto" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span34" class="input-group-addon cusspan" runat="server">Pay Mode </span>
                                                <asp:DropDownList ID="ddl_paymodes" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span30" class="input-group-addon cusspan" runat="server">Collected By</span>
                                                <asp:DropDownList ID="ddl_collectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span17" class="input-group-addon cusspan" runat="server">Status </span>
                                                <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                    <asp:ListItem Value="0" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="InActive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8"></div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:Button ID="btnsearc" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnsearc_Click" Text="Search" />
                                                <asp:Button ID="btnprints" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClientClick="return PrintOpbillList();" />
                                                <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="fixeddiv">
                                                <div class="row fixeddiv" id="div2" runat="server">
                                                    <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
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
                                                                <div id="DIV3" class="text-center loading" runat="server">
                                                                    <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:GridView ID="GvbillList" runat="server" AutoGenerateColumns="False" OnRowCommand="GvbillList_RowCommand" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..."
                                                            Width="100%" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Sl No.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex+1%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Bill No.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_transactionID" Visible="false" runat="server" Text='<%# Eval("TransactionID")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_billno" runat="server" Text='<%# Eval("BillNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Customer Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_custom" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Total Bill
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_totalBill" runat="server" Text='<%# Eval("TotalBillAmount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Discount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_discount" runat="server" Text='<%# Eval("Discount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Paid Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_paidamnt" runat="server" Text='<%# Eval("PaidAmount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Due Amnt
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_due" runat="server" Text='<%# Eval("DueAmount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Recieved By
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_recievedby" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Remarks
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtremarks" Width="150px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Bill Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_billdate" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Print
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("BillNo")%>'); return false;">Print</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                            <HeaderStyle BackColor="#D8EBF5" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Total Amount.(₹) </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalbillamount"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span15" class="input-group-addon cusspan" runat="server">Total Discount(₹) </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totaldiscount"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span22" class="input-group-addon cusspan" runat="server">Total Paid Amount(₹)</span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalcollected"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span31" class="input-group-addon cusspan" runat="server">Total Due Amount(₹)</span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totaldueamount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="visibility: hidden">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" />
                                            <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                            runat="server">
                                                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnexport" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel2" runat="server" CssClass="Tab2" HeaderText="Request List">
                    <ContentTemplate>
                        <div class="custab-panel" id="div6">
                            <div class="row fixeddiv" id="div4" runat="server">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Req No</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_reqno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span27" class="input-group-addon cusspan" runat="server">From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_reqdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_reqdatefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_reqdatefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_reqdateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_reqdateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_reqdateto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Status </span>
                                        <asp:DropDownList ID="ddl_RequestStatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Request"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Approved"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Rejected"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Settled"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Request Status </span>
                                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_reqsearch" OnClick="btn_reqsearch_Click" runat="server" Text="Search" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnreserREQ" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreserREQ_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div5" runat="server">
                                            <asp:Label ID="lbl_result3" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIV3" class="text-center loading" runat="server">
                                                            <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvDiscountRequest" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowCommand="GvDiscountRequest_RowCommand" AllowPaging="true" OnRowDataBound="gvstockstatus_RowDataBound" PageSize="10"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Request No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_transID" Visible="false" runat="server" Text='<%# Eval("TransactionID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_reqno" runat="server" Text='<%# Eval("ReqNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Customer
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_customer" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Total Bill
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_totalbill" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("TotalBillAmount","{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Req. Amnt.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_requestedamnt" runat="server" Text='<%# Eval("RequestedAmount","{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Aprvd. Amnt. 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_approved" runat="server" Text='<%# Eval("ApprovedAmount","{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Reqd By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RequestedBy" runat="server" Text='<%# Eval("ReqstBy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_requestDate" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ReqDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Staus
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_statusID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RequestStatus") %>'></asp:Label>
                                                                <asp:Label ID="lblstatus" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" Width="100px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Pay</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkpay" Font-Underline="true" runat="server" CommandName="Pay" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                    OnClientClick="javascript: return confirm('Are you sure to Pay ?');">
                                                                      Pay
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
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                </asp:GridView>
                                            </div>
                                        </div>
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
