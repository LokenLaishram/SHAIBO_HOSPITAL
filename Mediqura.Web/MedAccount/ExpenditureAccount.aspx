<%@ Page Title="Manual Transaction" Language="C#" MasterPageFile="~/Mediqura.Master"  AutoEventWireup="true" CodeBehind="ExpenditureAccount.aspx.cs" Inherits="Mediqura.Web.MedAccount.ExpenditureAccount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function Printreciept() {
            objvoucherno = document.getElementById("<%=txt_voucher.ClientID %>")
            window.open("../MedAccount/Reports/ReportViewer.aspx?option=ManualTransaction&VoucherNo=" + objvoucherno.value)
        }
        function DuplicatePrintreciept(voucherno) {
            objvoucherno = document.getElementById("<%=txt_voucher.ClientID %>")
            window.open("../MedAccount/Reports/ReportViewer.aspx?option=ManualTransaction&VoucherNo=" + voucherno)
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Transaction Type </span>
                                        <asp:DropDownList ID="ddl_transactionType" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Amount"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="txt_amount"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Transaction Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_TransactionDate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_TransactionDate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_TransactionDate" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Voucher No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_voucher"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Naration</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" TextMode="MultiLine" Rows="5"
                                            ID="txt_naration"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" UseSubmitBehavior="False" OnClick="btnResrt_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Print" OnClientClick="return Printreciept();"/>
                                    </div>
                                </div>

                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabPanel1" runat="server" HeaderText="Account Transaction List">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="Div2">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div3" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Transaction Type</span>
                                            <asp:DropDownList ID="ddl_transaction" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm "
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm "
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Account State</span>
                                            <asp:DropDownList ID="ddl_account_close" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True">
                                                <asp:ListItem Value="0">Open</asp:ListItem>
                                                <asp:ListItem Value="1">Closed</asp:ListItem>
                                                <asp:ListItem Value="2">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnResrt_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="divmsg3" runat="server">
                                                <asp:Label ID="lblresult"  runat="server" Height="13px" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Label ID="lbldescription" runat="server" class="SucessAlert"></asp:Label>

                                <div class="row cusrow pad-top ">
                                    <label class="gridview-label" id="lblgridincome" runat="server" style="color: black; font-weight: bold;">Recieved(₹)</label>
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; overflow:auto">
                                                    <%--                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>  --%>

                                                    <asp:GridView ID="Gv_incomereport" runat="server" CssClass="table-hover grid_table result-table" OnRowDataBound="Gv_incomereport_RowDataBound"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="Gv_incomereport_RowCommand1" OnPageIndexChanging="Gv_incomereport_PageIndexChanging" AllowCustomPaging="true" AllowPaging="true"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1) %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Voucher No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblincomeVoucher" runat="server"
                                                                        Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIncAmount" runat="server"
                                                                        Text='<%# Eval("TransactionAmount","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Particular
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIncParticular" runat="server"
                                                                        Text='<%# Eval("TransactionNaration") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_incomeDate" runat="server"
                                                                        Text='<%# Eval("TransactionDate","{0:dd / MM / yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Height="20px" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="DuplicatePrintreciept('<%# Eval("VoucherNo")%>'); return false;">Print</i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Delete
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
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Left" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row cusrow pad-top ">
                                    <label class="gridview-label" id="lblgridexpenses" runat="server" style="color: black; font-weight: bold;">Payment(₹)</label>
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%;">
                                                    <asp:GridView ID="GV_expensesreport" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="GV_expensesreport_RowCommand" AllowCustomPaging="true"
                                                        AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"  OnPageIndexChanging="GV_expensesreport_PageIndexChanging" AllowPaging="true">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1) %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Voucher No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblexpensesVoucher" runat="server"
                                                                        Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpAmount" runat="server"
                                                                        Text='<%# Eval("TransactionAmount","{0:00.00}") %>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Particular
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpParticular" runat="server"
                                                                        Text='<%# Eval("TransactionNaration") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTransactionDate" runat="server"
                                                                        Text='<%# Eval("TransactionDate","{0:dd / MM / yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Height="20px" TextMode="MultiLine" Rows="1" CssClass="form-control" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="DuplicatePrintreciept('<%# Eval("VoucherNo")%>'); return false;">Print</i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Delete
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_Delete" Style="color: red; text-align: center !important; font-size: 12px" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete record. ?');">
                                                                        <i class="fa fa-trash-o" style="text-align:center;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <br />

                                <div class="row ">
                                    <div class="col-sm-6" id="totalincome" runat="server">
                                        <div class="form-group input-group  pull-right">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Total Recieved(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_TotalIncome"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" id="totalexpenses" runat="server">
                                        <div class="form-group input-group  pull-right">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Total Payment(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_TotalExpenses"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
