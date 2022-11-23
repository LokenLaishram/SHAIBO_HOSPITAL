<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="StockIssue.aspx.cs" Inherits="Mediqura.Web.MedStore.StockIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function Printreciept() {
            objissueno = document.getElementById("<%=txtIssueNo.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=StockIssueReceipt&IssueNo=" + objissueno.value)
        }
        function Printduplicatereciept(issueno) {
            objissueno = document.getElementById("<%=txtIssueNo.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=StockIssueReceipt&IssueNo=" + objissueno.value)
        }
        function PrintStockIssueList() {
            objissueno = document.getElementById("<%=txt_issueNo.ClientID %>")
            objindentno = document.getElementById("<%=txt_indentnos.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_datefrom.ClientID %>")
            objdateto = document.getElementById("<%=txt_dateto.ClientID %>")
            objstocktype = document.getElementById("<%=ddl_stocktype.ClientID %>")
            objissuedby = document.getElementById("<%=ddlissuedby.ClientID %>")
            objhandedto = document.getElementById("<%=ddlhandedto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=StockIssuedList&IssueNo=" + objindentno.value + "&IndentNo=" + objindentno.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&StockType=" + objstocktype.value + "&IssuedBy=" + objissuedby.value + "&Handedto=" + objhandedto.value + "&Status=" + objstatus.value)
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerGRN" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabstockGRN" runat="server" CssClass="Tab1" HeaderText="Stock Issued">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelGRN">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="panel2" DefaultButton="btnadd">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Issue To <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_issueto" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span20" class="input-group-addon cusspan" runat="server">Indent No. <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_indentno"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Group <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" OnSelectedIndexChanged="ddl_itemgroup_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddl_group">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" ReadOnly="true" placeholder="XXX XXXX XXX XXX XX" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txtIssueNo"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Sub Group <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" OnSelectedIndexChanged="ddl_itemsubgroup_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddl_subgroup">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cus-long-span" runat="server">Item Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-span"
                                                ID="txtItemName" OnTextChanged="txtservices_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetItemDetails" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtItemName"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblservicename" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Class="btn  btn-sm scusbtn" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Item List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-Large ">
                                                <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvstockissuelist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."
                                                        Width="100%" HorizontalAlign="Center" OnRowDataBound="gvstockissuelist_RowDataBound" OnRowCommand="gvstockissuelist_RowCommand">
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
                                                                    Stock No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_stockID" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Available
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totalbalqty" Text='<%# Eval("TotalBalanceQty") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    No. Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_unit" runat="server" Text='<%# Eval("Unit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Qty/Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qtyperunit" runat="server" Text='<%# Eval("QtyPerUnit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CP/Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcpperqty" runat="server" Text='<%# Eval("CPPerQty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    MRP/Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_MRP_Qty" runat="server" Text='<%# Eval("MRPperqty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Qty.(To issue)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" AutoPostBack="true" MaxLength="5" Text='<%# Eval("IssueQuantity")%>' Width="100" OnTextChanged="txtissueqty_TextChanged" ID="txtqty"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txtqty" ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="0123456789"
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
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Total Qty.</span>

                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_total_qty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_total_qty" ValidChars="0123456789"></asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Total CP(₹) </span>

                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totcp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_totcp" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Total MRP(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totmrp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_totmrp" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Handed To <span
                                            style="color: red">*</span> </span>
                                        <asp:DropDownList runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_handedto">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group" style="text-align: right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm cusbtn"
                                                OnClick="btnsave_Click" />
                                            <asp:Button ID="btnprints" runat="server" Text="Print" OnClientClick="return Printreciept();" Class="btn  btn-sm cusbtn" />
                                            <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Stock Issued List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg2" runat="server">
                                            <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span6" class="input-group-addon cusspan" runat="server">Issue No.</span>

                                                <asp:TextBox runat="server" AutoPostBack="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_issueNo"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_issueNo" ID="FilteredTextBoxExtender4"
                                                    runat="server" ValidChars="0123456789"
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                    ServiceMethod="GetIssueNo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_issueNo"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span5" class="input-group-addon cusspan" runat="server">Indent No.</span>
                                                <asp:TextBox runat="server" AutoPostBack="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_indentnos"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                    ServiceMethod="GetIndentNos" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_indentnos"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>

                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span7" class="input-group-addon cusspan" runat="server">Date From <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_datefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                    TargetControlID="txt_datefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender15" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_datefrom" />

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span8" class="input-group-addon cusspan" runat="server">Date To <span
                                                        style="color: red">*</span></span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_dateto"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                        TargetControlID="txt_dateto" />
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender41" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dateto" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span30" class="input-group-addon cusspan" runat="server">Stock Type <span
                                                    style="color: red">*</span></span>
                                                <asp:DropDownList ID="ddl_stocktype" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span15" class="input-group-addon cusspan" runat="server">Issued By</span>
                                                <asp:DropDownList ID="ddlissuedby" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Handed To</span>
                                                <asp:DropDownList ID="ddlhandedto" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span17" class="input-group-addon cusspan" runat="server">Status </span>
                                                <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8"></div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                                <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return PrintStockIssueList();" Class="btn  btn-sm cusbtn" />
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
                                                <div class="pbody">
                                                    <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
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
                                                                <asp:GridView ID="gvstockissuelist1" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." OnPageIndexChanging="gvstockissuelist1_PageIndexChanging" AllowPaging="true" PageSize="10"
                                                                    Width="100%" HorizontalAlign="Center" OnRowCommand="gvstockissuelist1_RowCommand">
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
                                                                                IssueNo.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("IssueID")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_issueno" runat="server" Text='<%# Eval("IssueNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Indent No.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_indent" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Issued By
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblissuedby" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Handed To
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblhandedto" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("HandedTo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Total Qty. Issued
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_totalunit" runat="server" Text='<%# Eval("TotalQty")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Total CP
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("TotalCP", "{0:0#.##}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Total MRP
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_totalmrp" runat="server" Text='<%# Eval("TotalMRP", "{0:0#.##}")%>'></asp:Label>
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
                                                                                Date
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_issuedate" runat="server" Text='<%# Eval("IssueDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Print
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="Printduplicatereciept('<%# Eval("IssueNo")%>'); return false;">Print</a>
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
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="gvstockissuelist1" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span12" class="input-group-addon cusspan" runat="server">Total Quantity </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totaquatityissued"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span13" class="input-group-addon cusspan" runat="server">Total C.P(₹) </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalcp_issued"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span22" class="input-group-addon cusspan" runat="server">Total M.R.P(₹)</span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalmrp_issued"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4"></div>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
