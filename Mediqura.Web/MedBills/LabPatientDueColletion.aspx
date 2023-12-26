<%@ Page Title="Due Collection" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabPatientDueColletion.aspx.cs" Inherits="Mediqura.Web.MedBills.LabPatientDueColletion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <link href="../Scripts/jquery.timepicker.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.timepicker.min.js"></script>
    <script type="text/javascript">
        function calculate1() {
            var DueBal = document.getElementById("<%=txt_DueBal.ClientID%>").value;
            var Paid = document.getElementById("<%=txt_Paid.ClientID%>").value
            var discountcheck = document.getElementById("<%=chkduediscount.ClientID%>");

            if (+Paid > +DueBal) {
                document.getElementById("<%=txtdue.ClientID%>").value = 0;
                document.getElementById("<%=txtDiscountAmount.ClientID%>").value = 0;
                alert("Paid amount cannot be greater than due balance.");
                return false;
            }
            else if (discountcheck.checked) {
                document.getElementById("<%=txtdue.ClientID%>").value =0;
                document.getElementById("<%=txtDiscountAmount.ClientID%>").value = (+DueBal - +Paid).toFixed(2);;
            } else {
                document.getElementById("<%=txtdue.ClientID%>").value = (+DueBal - +Paid).toFixed(2);
                document.getElementById("<%=txtDiscountAmount.ClientID%>").value = 0;
            }
        }
    </script>
    <script type="text/javascript">
        function PrintPatientDueList() {
            objPtypeID = "0";
            objRespBy = document.getElementById("<%=ddlResponsibleBy.ClientID %>")
            objfrom = document.getElementById("<%=txtfrom.ClientID %>")
            objto = document.getElementById("<%=txtto.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=LabDuePatientList&PatientTypeID=0" + "&DueRespBy=" + objRespBy.value + "&DateFrom=" + objfrom.value + "&DateTo=" + objto.value)
        }

        function PrintDuePaidReciept() {
            objreceipt = document.getElementById("<%=txtDueColNo.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=LabDuePaidReceipt&RecieptNo=" + objreceipt.value)
        }
        function PrintDueReciept(recieptno) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=LabDuePaidReceipt&RecieptNo=" + recieptno)
        }

        function PrintDueCollectionList() {

            objPtypeID = "0";
            objCusDetails = document.getElementById("<%=txtcustomerDetails.ClientID %>")
            objRespBy = document.getElementById("<%=ddl_DueResponsibleBy.ClientID %>")
            objfrom = document.getElementById("<%=txt_DateFrom.ClientID %>")
            objto = document.getElementById("<%=txt_DateTo.ClientID %>")
            objstatus = document.getElementById("<%=ddl_status.ClientID %>")

            window.open("../MedBills/Reports/ReportViewer.aspx?option=LabDueCollectionList&PatientTypeID=0" + "&DueRespBy=" + objRespBy.value + "&CusDetails=" + objCusDetails.value + "&DateFrom=" + objfrom.value + "&DateTo=" + objto.value + "&Status=" + objstatus.value)

        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabDueCollection" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanelDueCollection" runat="server" CssClass="Tab2" HeaderText="Due Customer  ">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelDueCollection">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblPatientType" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddlPatientType">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldatefrom" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldateto" class="input-group-addon cusspan" runat="server">Date To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblResponsibleBy" class="input-group-addon cusspan" runat="server">Due Responsible By</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlResponsibleBy">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btnsearchs_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintPatientDueList();" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm scusbtn" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsgs3" runat="server" style="padding-left: 10px;">
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
                                                <div class="grid" style="float: left; width: 100%; max-height: 48vh; overflow: auto">
                                                    <asp:GridView ID="gvDueCustomer" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="gvDueCustomer_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Bill No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPatientType" Visible="false" runat="server" Text='<%# Eval("PatientTypeID") %>'></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblbillno" Visible="true" runat="server" Text='<%# Eval("BillNo") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Customer Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                                                    <asp:Label ID="lblCustomerName" Visible="true" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Total Bill 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalBillAmount" Visible="true" runat="server" Text='<%# Eval("TotalBillAmount","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Discount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscount" Visible="true" Text='<%# Eval("Discount","{0:0#.##}") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Paid Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaidAmount" Visible="true" runat="server" Text='<%# Eval("PaidAmount","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Due Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDueAmount" Visible="true" runat="server" Text='<%# Eval("DueAmount","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Total Due Paid
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastDuePaid" Visible="true" runat="server" Text='<%# Eval("LastDuePaid","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Due Balance
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDueBalance" Visible="true" runat="server" Text='<%# Eval("DueBalance","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Responsibility By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDueResponse" Visible="true" runat="server" Text='<%# Eval("DueReponsibleName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" CssClass="btn scusbtn" runat="server" Text='Collect' CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    </asp:LinkButton>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" Font-Underline="true" />
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
                            </div>
                            <div class="row" style="height: 10px">
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblTotalBill" class="input-group-addon cusspan" runat="server">Total Bill </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtTotalBill"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblTotalDiscount" class="input-group-addon cusspan" runat="server">Total Discount </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtTotalDiscount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblTotalPaid" class="input-group-addon cusspan" runat="server">Total Paid</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtTotalPaid"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblTotaldue" class="input-group-addon cusspan" runat="server">Total Due</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtTotalDue"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblLastDuePaid" class="input-group-addon cusspan" runat="server">Total Last Paid </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtLastDuePaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblTotalDueBal" class="input-group-addon cusspan" runat="server">Total Due Balance </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txtTotalDueBal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabDueDetails" runat="server" CssClass="Tab2" HeaderText="Due Details">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelDueDetails">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="lbl_PatientName" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_PatientName"></asp:TextBox>
                                        <asp:HiddenField ID="hdnpatienttypeID" runat="server" />
                                        <asp:HiddenField ID="hdnBillNo" runat="server" />
                                        <asp:HiddenField ID="hdnUHID2" runat="server" />
                                        <asp:HiddenField ID="hdnIPNO2" runat="server" />
                                        <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox"
                                            ID="txtuhid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblDueColNo" class="input-group-addon cusspan" runat="server">Reciept No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDueColNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbl_ResponsibleBy" class="input-group-addon cusspan" runat="server">ResponsibleBy</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtResponsibleBy"></asp:TextBox>
                                        <asp:HiddenField ID="hdnResponsibleID" runat="server" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_billno" class="input-group-addon cusspan" runat="server">Bill No</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbillno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_billdate" class="input-group-addon cusspan" runat="server">Bill Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbilldate"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblbillAmount" class="input-group-addon cusspan" runat="server">Total Bill Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbillamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldiscount" class="input-group-addon cusspan" runat="server">Discount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdiscount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblpaidamount" class="input-group-addon cusspan" runat="server">Paid Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpaidamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblDueAmount" class="input-group-addon cusspan" runat="server">Due Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDueAmount"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_LastPaid" class="input-group-addon cusspan" runat="server">Last Due Paid</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_LastDuePaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_DueBal" class="input-group-addon cusspan" runat="server">Due Balance</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_DueBal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 50px;">
                            </div>
                            <div class="row">
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
                                        <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbank"></asp:TextBox>
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
                                        <span id="lbl_DueBalance" class="input-group-addon cusspan" runat="server">Due Balance</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_DueBalance"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtinvoicenumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblDisRemark" class="input-group-addon cusspan" runat="server">Discount Remark</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDisRemark"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_Paid" class="input-group-addon cusspan" runat="server">Paid</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="Paid_OnTextChanged" AutoPostBack="true"
                                            ID="txt_Paid" onkeyup="return calculate1();"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_Paid" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="012345678.9"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Discount Due ?</span>
                                        <asp:CheckBox ID="chkduediscount" runat="server" CssClass="chkbox" OnCheckedChanged="OnCheckDiscount" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldue" class="input-group-addon cusspan" runat="server">Due</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdue"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldisAmt" class="input-group-addon cusspan" runat="server">Discount Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDiscountAmount"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_save" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" runat="server" Class="btn  btn-sm  scusbtn" Text="Pay" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnPrint2" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintDuePaidReciept();" />
                                        <asp:Button ID="btn_Reset" runat="server" Text="Reset" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm scusbtn" OnClick="btn_Reset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabDueCollectionList" runat="server" CssClass="Tab2" HeaderText="Due Collection List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                    <asp:Label ID="lbl_message3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel runat="server" Visible="false">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lbl_PatientType" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddl_PatientType">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="lblcustomerDetails" class="input-group-addon cusspan" runat="server">Customer Details</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Placeholder="Search by Customer Name | Bill No. | Reciept No."
                                            ID="txtcustomerDetails"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtcustomerDetails" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/|%-()" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetDuePaidCustomer" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtcustomerDetails"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_DueResponsibleBy" class="input-group-addon cusspan" runat="server">Collected By</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_DueResponsibleBy">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_DateFrom" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txt_DateFrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_DateFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DateFrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_DateTo" class="input-group-addon cusspan" runat="server">Date To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txt_DateTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_DateTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DateTo" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_status" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_Search3" runat="server" Text="Search" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm scusbtn" OnClick="btn_Searchs3_Click" />
                                        <asp:Button ID="btn_print3" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintDueCollectionList();" />
                                        <asp:Button ID="btn_Clear3" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btn_Tab3Clear_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg4" runat="server" style="padding-left: 10px;">
                                            <asp:Label ID="lbl_Results" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:GridView ID="gvDueCollectionList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="gvDueCollection_OnRowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Reciept No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_PatientType" Visible="false" runat="server" Text='<%# Eval("PatientTypeID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_RecieptNo" Visible="true" runat="server" Text='<%# Eval("RecieptNo") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Customer Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                                                    <asp:Label ID="lblCustomerName" Visible="true" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Last Due Paid
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastDuePaid" Visible="true" runat="server" Text='<%# Eval("LastDuePaid","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Bill No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_billno3" runat="server" Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Due Balance
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDueBalance" Visible="true" runat="server" Text='<%# Eval("DueBalance","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Paid
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Paid" Visible="true" runat="server" Text='<%# Eval("Paid","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Due Discount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_DueDiscount" Visible="true" runat="server" Text='<%# Eval("DueDiscount","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Due
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Due3" Visible="true" runat="server" Text='<%# Eval("Due","{0:00.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Remarks                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="100px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDueReciept('<%# Eval("RecieptNo")%>'); return false;">Print</a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                        <i class="fa fa-trash-o cus-delete-color"></i>                                        
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <asp:UpdateProgress ID="updateProgress3" runat="server">
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
