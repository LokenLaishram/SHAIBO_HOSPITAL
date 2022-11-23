<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PhrDepositRefund.aspx.cs" Inherits="Mediqura.Web.MedPhr.PhrDepositRefund" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function Printreciept() {
            objdepositno = document.getElementById("<%=txtrefundno.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrRefundReceipt&RefundNo=" + objdepositno.value)
        }
        function PrintDuplicatebills(billno) {
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrRefundReceipt&RefundNo=" + billno)
        }
        function PrintRefundList() {
            objnames = document.getElementById("<%=txtpatientNames.ClientID %>")
            objpaymode = document.getElementById("<%=ddlpaymentmodes.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrRefundList&PatientName=" + objnames.value + "&Status=" + objstatus.value + "&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value + "&Paymode=" + objpaymode.value)
        }

        function calculate1() {
            var txt_totalbalance = document.getElementById("<%=txttotalbalance.ClientID%>").value;
            var txt_refundamnt = document.getElementById("<%=txtrefundamount.ClientID%>").value;

            if (+(txt_totalbalance) >= +(txt_refundamnt)) {
                document.getElementById("<%=txtdue.ClientID%>").value = (+(txt_totalbalance) - +(txt_refundamnt)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            }
            else {
                document.getElementById("<%=txtdue.ClientID%>").value = "";
                document.getElementById("<%=txtrefundamount.ClientID%>").value = "";
                alert("Please enter correct refund amount.");
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabadvancedeposit" runat="server" HeaderText="PHR Deposit Refund">
                    <ContentTemplate>
                        <asp:Panel runat="server" DefaultButton="btnverify">
                            <div class="custab-panel" id="depositdetaildiv">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="lbluhid" class="input-group-addon cusspan" runat="server" style="color: red">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txtUHID" AutoPostBack="True" OnTextChanged="txtUHID_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtUHID"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
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
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtaddress"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span1" class="input-group-addon cusspan" runat="server">Total Deposited </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txttotaldeposied"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span3" class="input-group-addon cusspan" runat="server">Last Refunded </span>
                                                <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtlastrefund"></asp:TextBox>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Total Balance </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalbalance"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Refund Amount </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtrefundamount" onkeyup="return calculate1();" MaxLength="10"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtrefundamount" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                            <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtbank"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">Cheque No</span>
                                            <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtcheque"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Total Due </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdue"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Remarks </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtremarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <asp:HiddenField ID="hdnverifystatus" runat="server" />
                                            <asp:Button ID="btnverify" runat="server" UseSubmitBehavior="False" Text="Verify" Class="btn  btn-sm scusbtn" OnClick="btnverify_Click" />
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Process..'" Text="Refund" Class="btn  btn-sm scusbtn" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return Printreciept();" Class="btn  btn-sm scusbtn" />
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" Class="btn  btn-sm scusbtn" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
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
                                            <div class="col-sm-8">
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
                                                    <span id="Span8" class="input-group-addon cusspan" runat="server">Date From </span>
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
                                                    <span id="Span9" class="input-group-addon cusspan" runat="server">Date To </span>
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
                                                                   <%-- <asp:TemplateField>
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
                                            <div class="col-md-4">
                                            </div>
                                            <div class="col-md-8">
                                                <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                                <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                                runat="server">
                                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                                <%-- <asp:ListItem Value="2" Text="PDF"></asp:ListItem>--%>
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
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
