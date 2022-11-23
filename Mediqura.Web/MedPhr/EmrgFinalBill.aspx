<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmrgFinalBill.aspx.cs" Inherits="Mediqura.Web.MedPhr.EmrgFinalBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../Images/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "../Images/plus.gif";
            }
        }
        function pageLoad() {

            if (+document.getElementById("<%=hdnbillsubmittype.ClientID%>").value == "2") {
                document.getElementById("<%=btnsave.ClientID%>").value = "Send Req";
            }
            else {
                document.getElementById("<%=btnsave.ClientID%>").value = "Save";
            }
            var options = {
                now: "0:01", //hh:mm 24 hour format only, defaults to current time
                twentyFour: false,  //Display 24 hour format, defaults to false
                upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
                downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS
                hoverState: 'hover-state', //The hover state class to use, for custom CSS
                title: 'Timepicker', //The Wickedpicker's title,
                showSeconds: false, //Whether or not to show seconds,
                timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
                secondsInterval: 1, //Change interval for seconds, defaults to 1,
                minutesInterval: 1, //Change interval for minutes, defaults to 1
                beforeShow: null, //A function to be called before the Wickedpicker is shown
                afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
                show: null, //A function to be called when the Wickedpicker is shown
                clearable: false, //Make the picker's input clearable (has clickable "x")
            };
            var options1 = {
                now: "23:59", //hh:mm 24 hour format only, defaults to current time
                twentyFour: false,  //Display 24 hour format, defaults to false
                upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
                downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS
                hoverState: 'hover-state', //The hover state class to use, for custom CSS
                title: 'Timepicker', //The Wickedpicker's title,
                showSeconds: false, //Whether or not to show seconds,
                timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
                secondsInterval: 1, //Change interval for seconds, defaults to 1,
                minutesInterval: 1, //Change interval for minutes, defaults to 1
                beforeShow: null, //A function to be called before the Wickedpicker is shown
                afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
                show: null, //A function to be called when the Wickedpicker is shown
                clearable: false, //Make the picker's input clearable (has clickable "x")
            };
            $('.timepicker').wickedpicker(options);
            $('.timepicker1').wickedpicker(options1);
        }
        function calculate3() {
            var totalbill = document.getElementById("<%=txttotalamount.ClientID%>").value;
            var discountPC = document.getElementById("<%=txt_discountPC.ClientID%>").value;
            var adjusted = document.getElementById("<%=txtadjustedamount.ClientID%>").value;

            document.getElementById("<%=txt_dueamount.ClientID%>").value = "";
            document.getElementById("<%=txt_discountvalue.ClientID%>").value = (+totalbill * (+discountPC / 100)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            var discountvalue = (+totalbill * (+discountPC / 100)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var payable = document.getElementById("<%=hdnpayable.ClientID%>").value;
            document.getElementById("<%=txt_payableamount.ClientID%>").value = (+payable) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);
            document.getElementById("<%=txt_totalpaid.ClientID%>").value = (+payable) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);

            if (+discountvalue > +payable) {
                document.getElementById("<%=txt_discountPC.ClientID%>").value = "";
                document.getElementById("<%=txt_discountvalue.ClientID%>").value = "";
                document.getElementById("<%=txt_totalpaid.ClientID%>").value = +payable;
                document.getElementById("<%=txt_payableamount.ClientID%>").value = +payable;
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
            totalbill = document.getElementById("<%=txttotalamount.ClientID%>").value;
            var discountValue = document.getElementById("<%=txt_discountvalue.ClientID%>").value;
            var adjusted = document.getElementById("<%=txtadjustedamount.ClientID%>").value;

            document.getElementById("<%=txt_dueamount.ClientID%>").value = "";
            document.getElementById("<%=txt_discountPC.ClientID%>").value = (+discountValue / +totalbill * 100).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var payable = document.getElementById("<%=hdnpayable.ClientID%>").value;

            document.getElementById("<%=txt_payableamount.ClientID%>").value = (+payable) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);
            document.getElementById("<%=txt_totalpaid.ClientID%>").value = (+payable) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);

            if (+document.getElementById("<%=txt_discountvalue.ClientID%>").value > (+payable)) {
                document.getElementById("<%=txt_discountPC.ClientID%>").value = "";
                document.getElementById("<%=txt_discountvalue.ClientID%>").value = "";
                document.getElementById("<%=txt_totalpaid.ClientID%>").value = +payable;
                document.getElementById("<%=txt_payableamount.ClientID%>").value = +payable;
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

            var Apaidamount = (+document.getElementById("<%=txt_payableamount.ClientID%>").value) - (+document.getElementById("<%=txt_discountvalue.ClientID%>").value);
            var CPaidamount = document.getElementById("<%=txt_totalpaid.ClientID%>").value;
            var totalamount = document.getElementById("<%=txt_payableamount.ClientID%>").value;
            var discount = document.getElementById("<%=txt_discountvalue.ClientID%>").value;
        	var payable = document.getElementById("<%=hdnpayable.ClientID%>").value;

        	document.getElementById("<%=txt_dueamount.ClientID%>").value = ((+payable - +discount) - +CPaidamount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
        	var due = ((+payable - +discount) - +CPaidamount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
        	if (+CPaidamount > (+payable - +discount)) {
        		document.getElementById("<%=txt_totalpaid.ClientID%>").value = Math.round((+payable - +discount).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0]);
                document.getElementById("<%=txt_dueamount.ClientID%>").value = "";
            }
            if (+due > 0) {
                document.getElementById("<%=ddl_responsible.ClientID%>").disabled = false;
            }
            else {
                document.getElementById("<%=ddl_responsible.ClientID%>").value = 0;
                document.getElementById("<%=ddl_responsible.ClientID%>").disabled = true;
            }
        }
        function Printreciept() {
            objbillno = document.getElementById("<%=txt_billnumber.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=EmrgFinalBill&BillNo=" + objbillno.value)
        }
        function PrintDuplicatebills(billno) {
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=EmrgFinalBill&BillNo=" + billno)
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel1" runat="server" HeaderText="Emrg Final Billing">
                    <ContentTemplate>
                        <div class="custab-panel" id="Emrgadmissiondetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">Emrg.No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                            ID="txt_emrgno" AutoPostBack="True" OnTextChanged="txt_emrgno_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetEmrgNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_emrgno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtname"></asp:TextBox>
                                        <asp:HiddenField ID="hdnbillsubmittype" runat="server" />
                                        <asp:HiddenField ID="hdnreqno" runat="server" />
                                        <asp:HiddenField ID="hdnpayable" runat="server" />

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_bill" class="input-group-addon cusspan" runat="server">Bill Number</span>
                                        <asp:HiddenField ID="hdnbillnumber" runat="server" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_billnumber"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:HiddenField ID="hdnuhid" runat="server"></asp:HiddenField>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Age"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Admission Date</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_admissionDate"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">C/O</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_careof"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_department"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_doctor" class="input-group-addon cusspan" runat="server">Doctor</span>
                                        <asp:Label runat="server" ID="lbl_UHIDTemp" Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_doctor"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="Div4" class="row cusrow " runat="server">
                                <div id="Div5" class="col-sm-12" runat="server">
                                    <div class="gridview-container-Lab">
                                        <div id="Div8" class="grid" runat="server" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                            <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                            <asp:GridView ID="gvEMRGitemlist" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>SL No.</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Issue Date</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_recordID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            <asp:Label ID="lbl_issuedate" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy | hh:mm:ss tt}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Issue.No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_iisueno" runat="server" Text='<%# Eval("EmgDrgIssueNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Particulars</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_particulars" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Rate </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_rate" runat="server" Text='<%# Eval("MRPperQty","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Qty</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Amount</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("NetAmount","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" Width="1%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#D8EBF5" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height: 10px">
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Bill(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Balance(A/C)(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbalanceinac"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Adjusted(A/C)(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtadjustedamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                        <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                        <asp:HiddenField runat="server" ID="hdnbankID" />
                                        <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbank"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Discount (PC)</span>
                                        <asp:TextBox runat="server" onkeyup="return calculate3();" MaxLength="3" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_discountPC"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_discountPC" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="0123456789."
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan" runat="server">Discount (Value)</span>
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
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Payable(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_payableamount" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Paid (₹)<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" onkeyup="return calculate5();" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalpaid"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_totalpaid" ID="FilteredTextBoxExtender1"
                                            runat="server" ValidChars="012345678.9"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Due(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dueamount"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Responsible By</span>
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_responsible">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Refundable(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_refundable" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Remarks</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdiscremoarks"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group input-group pull-right">
                                    <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Pay" OnClick="btnsave_Click" Class="btn  btn-sm cusbtn" />
                                    <asp:Button ID="btn_refund" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Refund" Class="btn  btn-sm scusbtn" OnClick="btn_refund_Click" />
                                    <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return Printreciept();" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" Class="btn  btn-sm scusbtn" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Bill List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" style="color: red" runat="server">Emrg.No.<span
                                                style="color: red"></span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_emrgNoList" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                ServiceMethod="GetFEmrgNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_emrgNoList"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpatientNames" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                            <asp:DropDownList ID="ddlpaymentmodes" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                            <asp:TextBox ID="txttimepickerfrom" runat="server" class="timepicker form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                            <asp:TextBox ID="txttimepickerto" runat="server" class="timepicker1 form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Bill No.</span>
                                            <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_billnos"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">Collected By</span>
                                            <asp:DropDownList ID="ddlcollectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnprints" runat="server" Visible="false" Class="btn  btn-sm cusbtn" Text="Print" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" Text="Reset" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvEmrgfinalbill" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." PageSize="10" OnRowCommand="GvEmrgfinalbill_RowCommand" AllowPaging="true" AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
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
                                                                    Bill No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_billno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UHID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluhid" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Emrg.No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_emrgno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("EmrgNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("PatientAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Bill 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("TotalBillAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Adjusted 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaajustedamount" runat="server" Text='<%# Eval("AdjustedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Discount 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldiscountedamount" runat="server" Text='<%# Eval("DiscountedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Paid</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Due</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_dueamount" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy | hh:mm:ss tt}")%>'></asp:Label>
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
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Total Bill(₹)  </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalbillamount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Total Adjusted(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtajusted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span27" class="input-group-addon cusspan" runat="server">Total Discount(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotaldiscounted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span28" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalpaid"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" />
                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel3" runat="server" CssClass="Tab2" HeaderText="Request List">
                    <ContentTemplate>
                        <div class="custab-panel" id="div6">
                            <div class="row fixeddiv" id="div3" runat="server">
                                <asp:Label ID="lbl_mesaage3" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Req No</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_reqno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span34" class="input-group-addon cusspan" runat="server">From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_reqdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_reqdatefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_reqdatefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span35" class="input-group-addon cusspan" runat="server">To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_reqdateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_reqdateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_reqdateto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span36" class="input-group-addon cusspan" runat="server">Status </span>
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
                                        <span id="Span37" class="input-group-addon cusspan" runat="server">Request Status </span>
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
                                        <asp:Button ID="btnreserREQ" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnreserREQ_Click" Text="Reset" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div7" runat="server">
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
                                                        <div id="DIV9" class="text-center loading" runat="server">
                                                            <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvDiscountRequest" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowCommand="GvDiscountRequest_RowCommand" AllowPaging="true" OnRowDataBound="Gvdiscountreq_RowDataBound" PageSize="10"
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
