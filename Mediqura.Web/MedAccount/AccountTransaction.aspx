<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="AccountTransaction.aspx.cs" Inherits="Mediqura.Web.MedAccount.AccountTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                $('#img' + divname).addClass('fa-minus');
                $('#img' + divname).removeClass('fa-plus');
            } else {
                div.style.display = "none";
                $('#img' + divname).addClass('fa-plus');
                $('#img' + divname).removeClass('fa-minus');
            }
        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerAccountGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPanelAccountGroup" runat="server" HeaderText="Account Transaction">
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
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Payment Type </span>
                                        <asp:DropDownList ID="ddl_payment_type" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_payment_type_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Payment Mode </span>
                                        <asp:DropDownList ID="ddl_payment_mode" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_payment_mode_SelectedIndexChanged">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                            <asp:ListItem Value="2">Bank</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Voucher No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_voucher"></asp:TextBox>

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
                            <div class="row">
                                <div class="col-sm-4">
                                    <asp:Button ID="btn_add_Debit" runat="server" Class="btn  btn-sm cusbtn" Text="Add Rows" OnClick="btn_add_Debit_Click" UseSubmitBehavior="false" />
                                </div>
                            </div>
                            <asp:Panel runat="server">
                                <div class="row cusrow pad-top ">

                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="gridview-container">
                                                    <div class="grid" style="float: left; width: 100%; height: 22vh; overflow: auto">
                                                        <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading1" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>

                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>
                                                                <asp:HiddenField runat="server" ID="lblEntryType" />
                                                                <asp:GridView ID="GVDebit" runat="server" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVDebit_RowDataBound" OnRowCommand="GVDebit_RowCommand"
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
                                                                                Debit Account(BY)
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                                    ID="txt_debit_account" AutoPostBack="True" OnTextChanged="txt_debit_account_TextChanged"></asp:TextBox>
                                                                                <asp:AutoCompleteExtender ID="auto_debit_account" runat="server"
                                                                                    ServiceMethod="GetAccountName" MinimumPrefixLength="1"
                                                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_debit_account"
                                                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                                </asp:AutoCompleteExtender>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Debit Amount
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtDebitAmount" Width="150px" Height="18px" runat="server" Text='<%# Eval("DebitAmount")%>' OnTextChanged="txtDebitAmount_TextChanged"></asp:TextBox>
                                                                                <asp:FilteredTextBoxExtender TargetControlID="txtDebitAmount" ID="FilteredTextBoxExtender3"
                                                                                    runat="server" ValidChars="0123456789."
                                                                                    Enabled="True">
                                                                                </asp:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="cus-Delete-header">Delete</span>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                                <i class="fa fa-trash-o cus-delete-color"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:Button ID="btn_add_credit" runat="server" Class="btn  btn-sm cusbtn" Text="Add Rows" OnClick="btn_add_credit_Click" UseSubmitBehavior="false" />
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="gridview-container">
                                                    <div class="grid" style="float: left; width: 100%; height: 22vh; overflow: auto">
                                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading2" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress2" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                                                <asp:GridView ID="GVCredit" runat="server" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVCredit_RowDataBound" OnRowCommand="GVCredit_RowCommand"
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
                                                                                Credit Account (To)
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                                    ID="txt_credit_accnt" AutoPostBack="True" OnTextChanged="txt_credit_accnt_TextChanged"></asp:TextBox>
                                                                                <asp:AutoCompleteExtender ID="auto_credit_account" runat="server"
                                                                                    ServiceMethod="GetAccountName" MinimumPrefixLength="1"
                                                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_credit_accnt"
                                                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                                </asp:AutoCompleteExtender>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Credit Amount
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtCreditAmount" Width="150px" Height="18px" runat="server" Text='<%# Eval("CreditAmount")%>' OnTextChanged="txtCreditAmount_TextChanged"></asp:TextBox>
                                                                                <asp:FilteredTextBoxExtender TargetControlID="txtCreditAmount" ID="FilteredTextBoxExtender3"
                                                                                    runat="server" ValidChars="0123456789."
                                                                                    Enabled="True">
                                                                                </asp:FilteredTextBoxExtender>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="cus-Delete-header">Delete</span>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDeleteCredit" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                       <i class="fa fa-trash-o cus-delete-color"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                            </asp:Panel>
                            <div class="row pad-top">
                                <asp:Panel runat="server" ID="pnl_bank" Visible="false">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Transaction Type </span>
                                            <asp:DropDownList ID="ddl_transaction_type" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Instrument Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_instrument_name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Instrument Date</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_intrumentDate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_intrumentDate" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_intrumentDate" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Bank Payee Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_bank_payee_name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Bank Branch Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_bank_branch_name"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Cross Inst</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_cross_inst"></asp:TextBox>
                                        </div>
                                    </div>

                                </asp:Panel>
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Naration</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_naration"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel runat="server" ID="panel1">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4">
                                                        <div class="form-group input-group">
                                                            <span id="Span29" class="input-group-addon cusspan" runat="server">Total Credit Amt.</span>

                                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_total_credit_amount"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group input-group">
                                                            <span id="Span30" class="input-group-addon cusspan" runat="server">Total Debit Amt.</span>

                                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_total_debit_amount"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group input-group">
                                                            <span id="Span31" class="input-group-addon cusspan" runat="server">Difference Amt.</span>

                                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_difference_amt"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-lg-8"></div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="btnsave_Click" />
                                                <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
                                                <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnprints_Click" Text="Print" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabPanel1" runat="server" HeaderText="Transaction List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Transaction</span>
                                        <asp:DropDownList ID="ddl_transaction" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Account </span>
                                        <asp:DropDownList ID="ddl_ledgers" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Date From <span
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
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Date To <span
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
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Account</span>
                                        <asp:DropDownList ID="ddl_account_close" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Open</asp:ListItem>
                                            <asp:ListItem Value="1">Closed</asp:ListItem>
                                            <asp:ListItem Value="3">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div4" runat="server">
                                            <asp:Label ID="lblresult2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="Div5" class="row cusrow " runat="server">
                                <div id="Div6" class="col-sm-12" runat="server">

                                    <div id="Div7" class="pbody" runat="server">
                                        <div id="Div8" class="grid" runat="server" style="float: left; width: 100%; overflow: auto">
                                            <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading1" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>

                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="GVRecdTransaction" DataKeyNames="VoucherNo" CssClass="table-hover grid_table result-table" OnRowDataBound="GVRecdTransaction_RowDataBound" runat="server" EmptyDataText="No record found..."
                                                AutoGenerateColumns="False" Width="100%" OnRowCommand="GVRecdTransaction_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Transaction
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionType" runat="server"
                                                                Text='<%# Eval("TransactionType") %>'></asp:Label>
                                                            <asp:Label ID="lblTransTypeID" Visible="false" runat="server"
                                                                Text='<%# Eval("TransactionTypeID")%>'></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Receipt No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherNo" Style="text-align: center !important;" runat="server"
                                                                Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                            <a style="text-align: center !important;" href="JavaScript:divexpandcollapse('div<%# Eval("VoucherNo") %>');">
                                                                <i id='imgdiv<%# Eval("VoucherNo") %>' class="fa fa-plus"></i></a>
                                                            <div id='div<%# Eval("VoucherNo") %>' style="overflow: auto; display: none;">
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <asp:GridView ID="GvChild" runat="server" EmptyDataText="No record found..." DataKeyNames="VoucherNo" CssClass="ChildGrid"
                                                                        AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>SL No.</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>Ledger Type</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblledgertype" runat="server" Text='<%# Eval("LedgerType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>Ledger Name</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblledgername" runat="server" Text='<%# Eval("LedgerName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>Amount</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblamt" runat="server" Text='<%# Eval("Amount","{0:0#.##}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>Cur Balance</HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCurbalance" runat="server" Text='<%# Eval("CurBalance","{0:0#.##}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>

                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" Style="text-align: center !important;" runat="server"
                                                                Text='<%# Eval("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Account
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAcount" Style="text-align: center !important;" runat="server"
                                                                Text='<%# Eval("Partlyledger") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Timestamp
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionTime" Style="text-align: center !important;" runat="server"
                                                                Text='<%# Eval("AddedDate")%>'></asp:Label>
                                                            <asp:Label ID="lblVoucherDate" Visible="false" runat="server"
                                                                Text='<%# Eval("VoucherDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Amt.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionAmount" Style="text-align: center !important;" runat="server"
                                                                Text='<%# Eval("Amount","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox CssClass="form-control" ID="txtremarks" runat="server"></asp:TextBox>
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
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <span class="cus-Delete-header">Print</span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbl_print" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                 <i class="fa fa-print cus-delete-color"></i> 
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>

                                                </Columns>

                                                <HeaderStyle BackColor="#CFFF9F" />
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
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Total Transaction(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamt"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamtpaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Cash Payment(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamtcash"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Cash Receive(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamtreceive"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Contra Cash Inward(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtContraRecieve"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Contra Cash Outward(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtContrapaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Total Cash Outward(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTotalCashOutward"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Cash In Hand(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtCashInHand"></asp:TextBox>
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
