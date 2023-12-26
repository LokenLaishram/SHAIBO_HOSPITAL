<%@ Page Title="Lab Billing" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OPLabBilling.aspx.cs" Inherits="Mediqura.Web.MedBills.OPLabBilling" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function pageLoad() {
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
        function updatePanelPostback() {
            __doPostBack("<%=upHiddenPostback.ClientID%>", "");
        }
        function onListPopulated() {

            var completionList = $find("AutoCompleteExtender3").get_completionList();
            completionList.style.width = 'auto';
        }
    </script>
    <script type="text/javascript">
        function Printreciept() {
            objbillno = document.getElementById("<%=txtbillNo.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDLabBillReceipt&BillNo=" + objbillno.value)
        }
        function PrintDuplicatebills(billno, Package) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDLabBillReceipt&BillNo=" + billno + "&Ispacakge=" + Package)
        }
    </script>
    <script type="text/javascript">
        function PrintLabCollectionList() {
            alert('This Bill List Print Will be Update Soon.');
            objnames = '';
            objpaymode = document.getElementById("<%=ddlpaymentmodes.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objfromtimepicker = document.getElementById("<%=txttimepickerfrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objtotimepicker = document.getElementById("<%=txttimepickerto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            objcollectedby = document.getElementById("<%=ddlcollectedby.ClientID %>")
            objinvnumer = document.getElementById("<%=txtinvoicenumber.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=LabCollection&PatientName=0" + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Paymode=" + objpaymode.value + "&Fromtimepicker=" + objfromtimepicker.value + "&Totimepicker=" + objtotimepicker.value + "&Collectedby=" + objcollectedby.value + "&Invnumber=" + objinvnumer.value)

        }
    </script>
    <script type="text/javascript">
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
        function saveConfirm() {
            var sresult = confirm("Are you sure to save?");
            if (sresult == true) {
                $('#<%=btnsave.ClientID %>').hide();
                $('#<%=lblStatus.ClientID %>').val("Saving...");
                $('#<%=lblStatus.ClientID %>').show();
                return true;
            } else {
                return false;
            }

        }
        function print() {

            $.print("#barcode");
        }
        function printDuplicate() {

            $.print("#barcodeList");
        }

    </script>
    <script type="text/javascript">

        function DiscountCal() {

            var txt_TotalBill = document.getElementById("<%=txt_totalbill.ClientID%>").value;
            var txt_DiscountValue = document.getElementById("<%=txtDiscount.ClientID%>").value;
            var txt_Payable = document.getElementById("<%=txt_payableamnt.ClientID%>").value; 
                      var txt_Paid = document.getElementById("<%=txt_paidamount.ClientID%>").value;
            var txt_Due = document.getElementById("<%=txt_due.ClientID%>").value;
            if ((+txt_TotalBill) >= (+txt_DiscountValue)) {
                document.getElementById("<%=txt_payableamnt.ClientID%>").value = ((+txt_TotalBill) - (+txt_DiscountValue)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                document.getElementById("<%=txt_paidamount.ClientID%>").value = ((+txt_TotalBill) - (+txt_DiscountValue)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            }
            else {
                document.getElementById("<%=txtDiscount.ClientID%>").value = 0;
                document.getElementById("<%=txt_payableamnt.ClientID%>").value = (+txt_TotalBill);
                document.getElementById("<%=txt_paidamount.ClientID%>").value = (+txt_TotalBill);

                alert("Dicount amount could not be greater than  bill amount.");
            }
            PaidCal(); //funtion call
        }
        function PaidCal() {

            var txt_TotalBill = document.getElementById("<%=txt_totalbill.ClientID%>").value;
            var txt_DiscountValue = document.getElementById("<%=txtDiscount.ClientID%>").value;
            var txt_Payable = document.getElementById("<%=txt_payableamnt.ClientID%>").value;
            var txt_Paid = document.getElementById("<%=txt_paidamount.ClientID%>").value;
            var txt_Due = document.getElementById("<%=txt_due.ClientID%>").value;
            if ((+txt_Payable) >= (+txt_Paid)) {
                document.getElementById("<%=txt_due.ClientID%>").value = ((+txt_Payable) - (+txt_Paid)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            }
            else {
                document.getElementById("<%=txt_paidamount.ClientID%>").value = (+txt_TotalBill);
                document.getElementById("<%=txt_due.ClientID%>").value = 0;
                alert("Paid amount could not be greater than  payable amount.");
            }
        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabopdbillingdetails" runat="server" HeaderText="Lab Billing">
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
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Patient Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_patienttype" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_patienttype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="spn_PtypeNo" runat="server" class="input-group-addon cusspan">
                                            <asp:Label runat="server" ID="lbl_PtypeNo" Text="OP|IP|Emg No:"></asp:Label>
                                        </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_refno"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span38" class="input-group-addon cusspan" runat="server">Source <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_source" AutoPostBack="true" OnSelectedIndexChanged="ddl_source_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Walkin</asp:ListItem>
                                            <asp:ListItem Value="2">Referal Hospital</asp:ListItem>
                                            <asp:ListItem Value="3">Referal Doctor</asp:ListItem>
                                            <%-- <asp:ListItem Value="4">Referal Employee</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="5">Other Referals</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="6">Runners</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Referred By <span
                                            style="color: red">*</span> </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_referal" AutoPostBack="true" OnTextChanged="txt_referal_TextChanged" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                            ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_referal"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="lbluhid" class="input-group-addon cusspan" runat="server" style="color: red">
                                            <asp:Label runat="server" ID="lbl_patnumber" Text="UHID" runat="server"></asp:Label>
                                            <span
                                                style="color: red">*</span></span>
                                        <asp:HiddenField ID="hdnuhid" runat="server" />
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtUHID" MaxLength="10" AutoPostBack="True" OnTextChanged="txtUHID_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetAutoUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtUHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:LinkButton Visible="false" runat="server" ID="btnlnkpopreferal" OnClick="btnreferal_Click">     <i class="fa fa-plus"></i> </asp:LinkButton>
                                        <span id="Span32" class="input-group-addon cusspan" runat="server">Bill No.</span>
                                        <asp:TextBox runat="server" placeholder="  XXXX XXXX XXXX XXXX " ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbillNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Patient Category</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_patcatgeory"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdMsbPc" />
                                        <asp:HiddenField runat="server" ID="hdPatCat" />
                                        <asp:HiddenField runat="server" ID="hdisMsbDoctor" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Company</span>
                                        <asp:HiddenField runat="server" ID="hdntpacompanyID" />
                                        <asp:HiddenField runat="server" ID="hdnpackageID" />
                                        <asp:HiddenField runat="server" ID="hdnIsPachageCompany" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_tpacompany"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:DropDownList ID="ddldepartment" runat="server" ReadOnly="True" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblamount" class="input-group-addon cusspan" runat="server">Consultant</span>
                                        <asp:DropDownList ID="ddldoctor" AutoPostBack="true" ReadOnly="True" OnSelectedIndexChanged="ddldoctor_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="panel3" DefaultButton="btnadd">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span30" class="input-group-addon cusspan" runat="server">Service Category</span>
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                                ID="ddl_servicecategory">
                                                <asp:ListItem Value="1">Lab Service</asp:ListItem>
                                                <asp:ListItem Value="2">TPA Package Service</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cus-long-span" runat="server">Lab Services</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_labservices" AutoPostBack="True" OnTextChanged="txt_labservices_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetLabServices" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_labservices"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblservicename" runat="server" Visible="False"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3" visible="false">
                                        <div class="form-group input-group">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">OP No. / Ext. No.</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_opnumber"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cus-sm-span" runat="server">Quantity</span>
                                            <asp:TextBox runat="server" MaxLength="3" Class="form-control input-sm col-sm cus-sm-textbox"
                                                ID="txtquantity"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtquantity" ID="FilteredTextBoxExtender10"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" MaxLength="5" placeholder="Amount" ReadOnly="True" Class="form-control input-sm col-sm cus-sm-textbox"
                                                ID="txt_labservicecharge"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnservicecharge" />
                                            <asp:HiddenField runat="server" ID="hdntestcnetrID" />
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_labservicecharge" ID="FilteredTextBoxExtender3"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Test Center <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm cus-sm-textbox"
                                                OnSelectedIndexChanged="ddl_TestCenter_SelectedIndexChanged" AutoPostBack="true" ID="ddl_TestCenter">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnadd" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Add" OnClick="btnadd_Click" Class="btn   btn-sm scusbtn cus-sm-btn" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group input-group">

                                            <asp:TextBox runat="server" placeholder="Inv.Number" Width="170px" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_investigationno"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row cusrow">
                                <div class="col-sm-12 ">
                                    <asp:ModalPopupExtender ID="MdreferalDoctors" runat="server" TargetControlID="btnlnkpopreferal" PopupControlID="popupwindow"
                                        BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupwindow" BackColor="#D1EEEE" Style="display: none;" DefaultButton="btn_refsave">

                                        <div class="row popup">
                                            <div class="col-sm-12 popup-header">
                                                <h5 class="popup-heading">Referal Details</h5>
                                            </div>
                                            <div class="col-sm-12 popup-div-msg" id="div_message" runat="server">
                                                <asp:Label ID="message" CssClass="popup-lbl-msg" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <%--<div class="row poprow">
                                                    <div class="col-sm-3">Doctor code</div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtcode" CssClass="cuspopuptext" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>--%>
                                                <div class="row poprow">
                                                    <div class="col-sm-3">Doctor Name</div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtdoctor" CssClass="cuspopuptext" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-3">Contact No</div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtcontactno" CssClass="cuspopuptext" MaxLength="10" runat="server"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender TargetControlID="txtcontactno" ID="FilteredTextBoxExtender6"
                                                            runat="server" ValidChars="0123456789."
                                                            Enabled="True">
                                                        </asp:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-3">Address</div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox runat="server" CssClass="cuspopuptextarea" TextMode="MultiLine" ID="txtrefaddress"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row poprow popupbtn">
                                                    <div class="col-sm-2 popup-btn-gap"></div>
                                                    <div class="col-sm-3">
                                                        <asp:Button runat="server" CssClass="btn btn-sm cusbtn cus-sm-btn" ID="btnrefe_close" OnClick="btnclose_Click" Text="Close" />
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Button runat="server" CssClass="btn btn-sm cusbtn cus-sm-btn" ID="btn_refsave" OnClick="btn_refsave_Onclick" Text="Save" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Service List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">

                                                    <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                    <asp:GridView ID="gvoplabservicelist" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="gvoplabservicelist_RowDataBound"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="gvoplabservicelist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
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
                                                                    Lab Services
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_packageID" Visible="false" runat="server" Text='<%# Eval("PackageID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_testcenterID" Visible="false" Text='<%# Eval("TestCenterID")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbldeprtmentID" Visible="false" runat="server" Text='<%# Eval("DeptID")%>'></asp:Label>
                                                                    <asp:Label ID="lbldoctorID" Visible="false" runat="server" Text='<%# Eval("DocID")%>'></asp:Label>
                                                                    <asp:Label ID="lbldoctortype" Visible="false" runat="server" Text='<%# Eval("DoctorTypeID")%>'></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbllabparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("TestName") %>'></asp:Label>
                                                                    <asp:Label ID="lblremarks" Visible="false" Text='<%# Eval("Remarks") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_labcharges" runat="server" Text='<%# Eval("LabServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Quantity
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Net Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetLabServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test Center
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" Readonly="true" ID="ddl_testcenter" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="120px"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Urgency
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList runat="server" ID="ddl_urgency" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="100px"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="true">
                                                                <HeaderTemplate>
                                                                    Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_discount_type" runat="server" OnSelectedIndexChanged="ddl_discount_type_SelectedIndexChanged" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                                        <asp:ListItem Value="0">Fix</asp:ListItem>
                                                                        <asp:ListItem Value="1">PC</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="true">
                                                                <HeaderTemplate>
                                                                    Value
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                        ID="txt_dis_value" OnTextChanged="txt_dis_value_TextChanged" MaxLength="4" AutoPostBack="True"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_dis_value" ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="012345678.9"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="true">
                                                                <HeaderTemplate>
                                                                    Discount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_discount_amt" Style="text-align: left !important;" runat="server"></asp:Label>
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
                                                    <asp:GridView ID="GVDiscountApprovalList" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="GVDiscountApprovalList_RowDataBound"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center">
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
                                                                    Lab Services
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lbldoctorID" Visible="false" runat="server" Text='<%# Eval("DocID")%>'></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbllabparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("TestName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_labcharges" runat="server" Text='<%# Eval("LabServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Quantity
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Net Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetLabServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test Center
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestCenter" Visible="false" runat="server" Text='<%# Eval("TestCenterID")%>'></asp:Label>
                                                                    <asp:DropDownList runat="server" ID="ddl_testcenter" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="120px"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Urgency
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUrgency" Visible="false" runat="server" Text='<%# Eval("UrgencyID")%>'></asp:Label>
                                                                    <asp:DropDownList runat="server" ID="ddl_urgency" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="100px"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Discount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("DisAmount", "{0:0#.##}")%>' ID="lbl_discount_amt" Style="text-align: left !important;" runat="server"></asp:Label>
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
                            <div class="row" style="height: 15px">
                            </div>
                            <div class="row">
                                <asp:Panel runat="server" ID="panel4">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Total Bill(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_totalbill"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span40" class="input-group-addon cusspan" runat="server">Discount(₹)</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtDiscount" onkeyup="return DiscountCal();"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtDiscount" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">
                                                <asp:Label runat="server" ID="Label1" Text="Payable Amnt (₹)"></asp:Label></span>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ReadOnly="true" MaxLength="10" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_payableamnt"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txt_payableamnt" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">
                                                <asp:Label runat="server" ID="lblpaidamount" Text="Paid Amount (₹)"></asp:Label>
                                                <span
                                                    style="color: red">*</span></span>
                                            <asp:TextBox runat="server" MaxLength="10" onkeyup="return PaidCal();" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_paidamount"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="hidepanel" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Discount(₹)</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_discount" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">Balance(A/C)(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_balanceinac"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span20" class="input-group-addon cusspan" runat="server">Adjusted(A/C)(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_adjustedamount"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:LinkButton runat="server" ID="btnlinkdiscount" OnClick="btnlinkdiscount_Click">Lum Sum Discount <i class="fa fa-plus"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="row">

                                <asp:Panel runat="server" ID="panel5">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span25" class="input-group-addon cusspan" runat="server">
                                                <asp:Label runat="server" ID="Label2" Text="Due Amount (₹)"></asp:Label></span>
                                            <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_due"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                            <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Bank Name</span>

                                            <asp:HiddenField runat="server" ID="hdnbankID" />
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_bank" MaxLength="25"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
                                            <asp:Label ID="lbl_accno" Visible="False" runat="server"></asp:Label>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_chequenumber"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span33" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtinvoicenumber"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span27" class="input-group-addon cusspan" runat="server">Remarks</span>
                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtremarkdisc"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtremarkdisc" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span39" class="input-group-addon cusspan" runat="server">Collection Centre <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlRunnerID" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="row">
                                <asp:Panel runat="server" ID="panel6">
                                    <div class="col-sm-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnDisSave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Visible="false" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnDisSave_Click" />
                                            <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" Class="btn  btn-sm cusbtn"
                                                OnClick="btnsave_Click" />
                                            <asp:Button ID="btn_token" Visible="false" runat="server" Text="TOKEN" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btn_token_Click" Class="btn  btn-sm cusbtn" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" Class="btn  btn-sm cusbtn" />
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                            <asp:Button ID="btnModel" Style="display: none" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>

                            <div class="row cusrow ">
                                <div class="col-sm-12 ">

                                    <asp:ModalPopupExtender ID="MDBarcode" runat="server" TargetControlID="btnModel" PopupControlID="popupwindowbarcode"
                                        BackgroundCssClass="modalBackground" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupwindowbarcode" Style="display: none;">

                                        <div style="width: 100%" style="width: 189%; margin-left: -51%;" class="row">
                                            <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">
                                                <div id="barcode">
                                                    <table style="color: black;">
                                                        <asp:Literal runat="server" ID="ltBarcode"></asp:Literal>

                                                    </table>

                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnClose" runat="server" class="btn  btn-sm cusbtn" Text="Close" OnClick="btnClose_Click1" /></td>
                                                        <td>
                                                            <button class="btn  btn-sm cusbtn" onclick="print();">Print</button></td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">

                                    <asp:ModalPopupExtender ID="MDTPA" runat="server" TargetControlID="btnSample" PopupControlID="popupApproval"
                                        BackgroundCssClass="modalBackground" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupApproval" Style="display: none;" DefaultButton="btnAddTpa">

                                        <div style="width: 189%; margin-left: -51%;" class="row">
                                            <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-8 col-sm-offset-2">

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span37" class="input-group-addon cusspan" runat="server">Category </span>
                                                            <asp:DropDownList class="form-control input-sm col-sm custextbox" ID="ddl_tpa_patient_cat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_tpa_patient_cat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span35" class="input-group-addon cusspan" runat="server">Sub Category </span>
                                                            <asp:DropDownList class="form-control input-sm col-sm custextbox" ID="ddl_tpa_patient_sub_cat" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span36" class="input-group-addon cusspan" runat="server">Discount Amount </span>
                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_tpa_amount"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_tpa_amount" ID="FilteredTextBoxExtender1"
                                                                runat="server" ValidChars="0123456789."
                                                                Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <asp:Button ID="btnAddTpa" runat="server" Class="btn  btn-sm cusbtn" Text="Add" OnClick="btnAddTpa_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="fixeddiv">
                                                            <div class="row fixeddiv" id="divPopMsg" runat="server">
                                                                <asp:Label ID="lblPopUpmsg" runat="server" Height="13px"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="grid" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress4" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloadingPopUp1" class="text-center loading" runat="server">
                                                                <asp:Image ID="DIVloadingPopUpImage1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                    <asp:GridView ID="GVTPAList" runat="server" CssClass="table-hover grid_table result-table"
                                                        AllowPaging="false"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GVTPAList_RowCommand"
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
                                                                    Patient Category
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCat" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatCat") %>'></asp:Label>
                                                                    <asp:Label ID="lblCatID" Visible="false" runat="server"
                                                                        Text='<%# Eval("PatientCatID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Company
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubCat" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("subCat") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubCatID" Visible="false" runat="server"
                                                                        Text='<%# Eval("SubCatID") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDisAmount" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("DiscountAmount") %>'></asp:Label>
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
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>

                                                <br />
                                                <div class="row">
                                                    <div class="col-lg-6"></div>
                                                    <div class="col-sm-6">
                                                        <div>
                                                            <asp:Button ID="btnTpaClose" Style="margin-left: 8px" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn exprt" Text="Close" />
                                                            <asp:Button ID="btnSample" Style="display: none" runat="server" />


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Lab Collection">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="paneldepositlist">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">

                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>

                                            <%-- <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="10"
                                                ID="txtautoUHID" Visible="false" AutoPostBack="False" OnTextChanged="txtautoUHID_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoUHID"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtautoUHID" ID="FilteredTextBoxExtender4"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>--%>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="30" Placeholder="Search by Patient Name | Address | UHID "
                                                ID="txtpatientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender7"
                                                runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars="0123456789|. :" Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:HiddenField ID="hdnpatientName" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span34" class="input-group-addon cusspan" runat="server">Inv. Number</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_invnumber" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                ServiceMethod="GetInv" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_invnumber"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan mandbg" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
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
                                            <span id="Span9" class="input-group-addon cusspan mandbg" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
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
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                            <asp:DropDownList ID="ddlpaymentmodes" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span28" class="input-group-addon cusspan" runat="server">Collected By</span>
                                            <asp:DropDownList ID="ddlcollectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span41" class="input-group-addon cusspan" runat="server">Collection centre</span>
                                            <asp:DropDownList ID="ddlRunnerBy" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" Visible="false" OnClientClick="return PrintLabCollectionList();" Text="Print" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnresets_Click" />
                                            <asp:Button ID="btnmodelList" Style="display: none" runat="server" />
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
                                <div class="row cusrow ">
                                    <div class="col-sm-12 ">

                                        <asp:ModalPopupExtender ID="ModelListBarcode" runat="server" TargetControlID="btnmodelList" PopupControlID="PanelListWindow"
                                            BackgroundCssClass="modalBackground" Enabled="True">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel runat="server" ID="PanelListWindow" Style="display: none;">

                                            <div style="width: 100%" class="row">
                                                <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">
                                                    <div id="barcodeList">
                                                        <table style="color: black;">
                                                            <asp:Literal runat="server" ID="LitBarcodelist"></asp:Literal>
                                                        </table>
                                                    </div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnClosedList" runat="server" class="btn  btn-sm cusbtn" Text="Close" OnClick="btnClose_Click1" /></td>
                                                            <td>
                                                                <button class="btn  btn-sm cusbtn" onclick="printDuplicate();">Print</button></td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:GridView ID="gvdepositlist" runat="server" CssClass="table-hover grid_table result-table" AllowCustomPaging="true"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnPageIndexChanging="gvdepositlist_PageIndexChanging" AllowPaging="True"
                                                        OnRowCommand="gvdepositlist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)+(gvdepositlist.PageIndex)*gvdepositlist.PageSize %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bill No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Ref No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrefno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("RefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Inv No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvNo" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("InvestigationNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblIsVerified" Style="text-align: left !important;" runat="server"
                                                                        Visible="false" Text='<%# Eval("IsVerified") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UHID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluhid" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    TB  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("TotalBillAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    AD
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaajustedamount" runat="server" Text='<%# Eval("AdjustedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Dis
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldiscountedamount" runat="server" Text='<%# Eval("DiscountedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    P
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Due
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_due" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Collection Centre
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRunnerName" runat="server" Text='<%# Eval("RunnerName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Referal
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReferalName" runat="server" Text='<%# Eval("ReferalName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    RTax
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRunnerTaxAmt" runat="server" Text='<%# Eval("RunnerTaxAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Del</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                       <i class="fa fa-trash-o cus-delete-color"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("BillNo")%>','<%# Eval("IspackageCompany")%>'); return false;">Print</i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Left" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 20px;"></div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Total Bill(₹)  </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalbillamount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Total Discount(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotaldiscounted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalpaid"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Total Due(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_totaldue"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span42" class="input-group-addon cusspan" runat="server">Total RTax(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtTotalRunnerAmt"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-9">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnexport" Visible="False" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                            <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <asp:UpdateProgress ID="updateProgress2" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

