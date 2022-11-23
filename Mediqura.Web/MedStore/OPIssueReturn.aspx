<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OPIssueReturn.aspx.cs" Inherits="Mediqura.Web.MedStore.OPIssueReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function calculate() {
            var totalreturnamount = document.getElementById("<%=txt_totalamount.ClientID%>").value;
            var DeductAmount = document.getElementById("<%=txt_deductedamount.ClientID%>").value;
            if (+DeductAmount > +0) {
                var DeductedAmt = (totalreturnamount * (DeductAmount / 100))
                if (+totalreturnamount >= +DeductedAmt) {
                    document.getElementById("<%=txt_returnamount.ClientID%>").value = (totalreturnamount - DeductedAmt).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                }
                else {
                    alert("Deducted amount is greater than total amount.");
                }
            }
            else {
                document.getElementById("<%=txt_returnamount.ClientID%>").value = totalreturnamount;
            }
        }
    </script>
    <script type="text/javascript">
        function PrintOPReturn() {
            objreturnno = document.getElementById("<%=txt_returnNo.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=OPReturnList&ReturnNo=" + objreturnno.value)
        }
        function PrintduplicateOPReturn(returnno) {
            window.open("../MedStore/Reports/ReportViewer.aspx?option=OPReturnList&ReturnNo=" + returnno)
        }
        function PrintOPReturnList() {
            objreturnno = document.getElementById("<%=txt_returnNum.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_retdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txt_returndateTo.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=OPReturnList1&ReturnNo=" + objreturnno.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Status=" + objstatus.value)
        }

    </script>
    <asp:TabContainer ID="tabcontainerOPIssueReturn" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabOPIssueReturn" runat="server" CssClass="Tab1" HeaderText="OP Return">
            <ContentTemplate>
                <div class="custab-panel" id="panelOPIssueReturn">
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cus-long-span" runat="server">Bill No. <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_BillNo" MaxLength="10" OnTextChanged="txt_BillNo_Textchange" AutoPostBack="True"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_BillNo" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars="/|-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetBillNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_BillNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cus-long-span" runat="server">Patient name</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_custommer"></asp:TextBox>
                                        <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnsearch" Visible="false" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cus-long-span" runat="server">Bill Date</span>
                                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                    ID="txt_billdate"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_billdate" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cus-long-span" runat="server">Total Bill Amount(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                    ID="txt_totalbill"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_totalbill" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cus-long-span" runat="server">Total Paid Amount(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_totalpaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cus-long-span" runat="server">Return Ref.No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" placeholder="Return No." CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_returnNo"></asp:TextBox>
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
                                <label class="gridview-label">Return Item List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-Lab">
                                                <div class="grid" style="float: left; width: 100%; height: 35vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvopreturn1" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." RowDataBound="gvopreturn1_RowDataBound"
                                                                Width="100%" HorizontalAlign="Center">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Sl.No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Item Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_billno" Visible="false" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_UHID" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_substockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Batch No
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_batch" runat="server" Text='<%# Eval("BatchNo")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Exp.Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_expdate" runat="server" Text='<%# Eval("ExpiryDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            MRP/Qty
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_charge" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Qty.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Dis.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_discount" runat="server" Text='<%# Eval("ItemWiseDiscount", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Net Charge
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_netcharges" runat="server" Text='<%# Eval("NetCharges", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Return Qty.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_return1" AutoPostBack="true" Width="70px" OnTextChanged="txtreturnqty_TextChanged" Height="18px" runat="server"> </asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_return1" ID="FilteredTextBoxExtender7"
                                                                                runat="server" ValidChars="0123456789"
                                                                                Enabled="True">
                                                                            </asp:FilteredTextBoxExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Retrun Amt
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_returnAmt" runat="server" Text='0'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField  HeaderStyle-CssClass="text-center">
                                                                        <HeaderTemplate>
                                                                            Last RQty.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_lastqty" runat="server" Text='<%# Eval("LreturnQty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8EBF5" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="gvopreturn1" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Total Qty. Return</span>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_returnqty"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_returnqty" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Total Amount(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_totalamount"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_totalamount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Deducted(%)</span>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" MaxLength="3" onkeyup="return calculate();" OnTextChanged="txt_deducted_Textchange" AutoPostBack="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_deductedamount"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_deductedamount" ID="FilteredTextBoxExtender7"
                                                    runat="server" ValidChars="0123456789."
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_deductedamount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Return Amount(₹)</span>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_returnamount"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_returnamount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Remarks <span
                                            style="color: red">*</span></span>
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_remarks"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_remarks" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Recieved By <span
                                            style="color: red">*</span></span>
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_handover" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddl_handover" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm scusbtn"
                                            OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return PrintOPReturn();" Class="btn  btn-sm scusbtn" />
                                        <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="OP Return List">
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
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Return No.</span>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_returnNum"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                    ServiceMethod="GetReturnNo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_returnNum"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_returnNum" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">From <span
                                            style="color: red">*</span></span>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                            <ContentTemplate>

                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_retdatefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                    TargetControlID="txt_retdatefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_retdatefrom" />

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">To <span
                                            style="color: red">*</span></span>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_returndateTo"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                    TargetControlID="txt_returndateTo" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_returndateTo" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
                                        <asp:Button ID="btnsearch1" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btnsearch1_Click" />
                                        <asp:Button ID="btnprints" runat="server" Text="Print" OnClientClick="return PrintOPReturnList();" Class="btn  btn-sm scusbtn" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btnreset_Click" />
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
                                            <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="gvopreturnlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." OnPageIndexChanging="gvopreturnlist_PageIndexChanging" AllowPaging="true" PageSize="10"
                                                            Width="100%" HorizontalAlign="Center" OnRowDataBound="gvopreturnlist_RowDataBound" OnRowCommand="gvopreturnlist_RowCommand">
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
                                                                        Return No.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_returnno" runat="server" Text='<%# Eval("ReturnNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Return By
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblreturnby" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("ReturnBy") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Return Qty.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_totalreturn" runat="server" Text='<%# Eval("TotalReturnQty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Return Amnt. 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_returnamnt" runat="server" Text='<%# Eval("TotalReturnAmount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Deducted Amnt.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_deductedamnt" runat="server" Text='<%# Eval("DeductedAmount","{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Net Amnt.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_netamnt" runat="server" Text='<%# Eval("TotalActualReturnAmount","{0:0#.##}")%>'></asp:Label>
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
                                                                        Return Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_returndate" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("ReturnDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Print
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintduplicateOPReturn('<%# Eval("ReturnNo")%>'); return false;">Print</a>
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
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="gvopreturnlist" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Total return Qty. </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalreturnqty"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltotalamount" class="input-group-addon cusspan" runat="server">Total Amount. </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Total Deducted(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totaldeducted"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Total Return Amount(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_returnamounts"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>


</asp:Content>
