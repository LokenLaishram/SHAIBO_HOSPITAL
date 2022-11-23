<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DepositRefund.aspx.cs" Inherits="Mediqura.Web.MedBills.DepositRefund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function updatePanelPostback() {
            __doPostBack("<%=upHiddenPostback.ClientID%>", "");
        }

        function Printreciept() {
            objdepositno = document.getElementById("<%=txtrefundno.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=RefundtReceipt&RefundNo=" + objdepositno.value)
        }
        function PrintDuplicatebills(billno) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=RefundtReceipt&RefundNo=" + billno)
        }
        function PrintRefundList() {
            objuhid = document.getElementById("<%=txtautoUHID.ClientID %>")
            objnames = document.getElementById("<%=txtpatientNames.ClientID %>")
            objpaymode = document.getElementById("<%=ddlpaymentmodes.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=RefundList&UHID=" + objuhid.value + "&PatientName=" + objnames.value + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Paymode=" + objpaymode.value)
        }
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false;
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabadvancedeposit" runat="server" HeaderText="Deposit Refund">
                    <ContentTemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbluhid" class="input-group-addon cusspan" runat="server" style="color: red">UHID</span>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txtUHID" MaxLength="10" AutoPostBack="True" OnTextChanged="txtUHID_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                    ServiceMethod="GetAutoUHID" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtUHID"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                                <asp:FilteredTextBoxExtender TargetControlID="txtUHID" ID="FilteredTextBoxExtender1"
                                                    runat="server" ValidChars="0123456789"
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtUHID" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtname"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtname" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblco" class="input-group-addon cusspan" runat="server">Refund No.</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtrefundno"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel runat="server" ID="panel1">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbladdress" class="input-group-addon cusspan" runat="server">Address</span>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtaddress"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtaddress" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Total Deposited </span>
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttotaldeposied"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txttotaldeposied" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Total Adjusted </span>
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttotaladjusted"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txttotaladjusted" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Last Refunded </span>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtlastrefund"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtlastrefund" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Total Balance </span>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txttotalbalance"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txttotalbalance" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Refund Amount </span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtrefundamount" MaxLength="10" AutoPostBack="true" OnKeyUp="javascript:updatePanelPostback();" OnTextChanged="txtrefundamount_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtrefundamount" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">

                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlpaymentmode" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtbank"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtbank" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Cheque No</span>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtcheque"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtcheque" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Total Due </span>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtdue"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtdue" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Remarks </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtremarks"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <asp:HiddenField ID="hdnverifystatus" runat="server" />
                                        <asp:Button ID="btnverify" runat="server" OnClientClick="javascript: return confirm('Are you sure to verify ?');" Text="Verify" Class="btn  btn-sm scusbtn" OnClick="btnverify_Click" />
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Refund" Class="btn  btn-sm scusbtn" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return Printreciept();" Class="btn  btn-sm scusbtn" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" Class="btn  btn-sm scusbtn" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Refund List">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                                    <div class="custab-panel" id="paneldepositlist">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="divmsg2" runat="server">
                                                <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">

                                                <div class="form-group input-group">
                                                    <span id="Span4" class="input-group-addon cusspan" style="color: red" runat="server">UHID</span>
                                                    <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                        ID="txtautoUHID" AutoPostBack="True" OnTextChanged="txtautoUHID_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                        ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoUHID"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:FilteredTextBoxExtender TargetControlID="txtautoUHID" ID="FilteredTextBoxExtender4"
                                                        runat="server" ValidChars="0123456789"
                                                        Enabled="True">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>

                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtpatientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                        ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
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
                                                    <span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
                                                        style="color: red">*</span></span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtdatefrom"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                        TargetControlID="txtdatefrom" />
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />

                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
                                                        style="color: red">*</span> </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtto"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                        TargetControlID="txtto" />
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
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
                                                    <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                                    <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintRefundList();" Text="Print" />
                                                    <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                            <div class="col-sm-12">
                                                <div>
                                                    <div class="pbody">
                                                        <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                            <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                                <ProgressTemplate>
                                                                    <div id="DIV3" class="text-center loading" runat="server">
                                                                        <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:GridView ID="gvrefundlist" runat="server" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnPageIndexChanging="gvrefundlist_PageIndexChanging" AutoGenerateColumns="False" OnRowCommand="gvdepositlist_RowCommand"
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
                                                                            Refund No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("RefundNo") %>'></asp:Label>
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
                                                                            Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblpatientname" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Address
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <span class="cus-Delete-header">Amount</span>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllasterefund" Visible="false" runat="server" Text='<%# Eval("LastRefundedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                            <asp:Label ID="lblrefundamount" runat="server" Text='<%# Eval("RefundAmount", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="2%" />

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Added By
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
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
                                                                            <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                 <%--   <asp:TemplateField>
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
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Print
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("RefundNo")%>'); return false;">Print</a>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8EBF5" />
                                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />


                                                            </asp:GridView>

                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span12" class="input-group-addon cusspan" runat="server">Total Deposited </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttoraldeposited"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span13" class="input-group-addon cusspan" runat="server">Total Refund </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttotalrefundamount"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-10">

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

                                            <div class="col-md-2">

                                                <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
