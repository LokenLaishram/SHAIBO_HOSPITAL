<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="StockStatus.aspx.cs" Inherits="Mediqura.Web.MedStore.StockStatus" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintStockStatus() {
            objItname = document.getElementById("<%=txt_itemname.ClientID %>")
            objbatch = document.getElementById("<%=txt_batchNo.ClientID %>")
            objst_type = document.getElementById("<%=ddl_StType.ClientID %>")
            objsrecvno = document.getElementById("<%=txt_recdno.ClientID %>")
            objpo = document.getElementById("<%=txtPONo.ClientID %>")
            objstockno = document.getElementById("<%=txtstockno.ClientID %>")
            objgroup = document.getElementById("<%=ddl_itemgroup.ClientID %>")
            objsubgroup = document.getElementById("<%=ddl_subgroup.ClientID %>")
            objavailbalfrom = document.getElementById("<%=txt_avaifro.ClientID %>")
            objavailto = document.getElementById("<%=txt_availto.ClientID %>")
            objexpirfro = document.getElementById("<%=txt_expdaysfro.ClientID %>")
            objexpirto = document.getElementById("<%=txt_expdaysto.ClientID %>")
            objrecvyear = document.getElementById("<%=ddlrecivedyear.ClientID %>")
            objrecvmnth = document.getElementById("<%=ddlmonth.ClientID %>")
            objrevfrom = document.getElementById("<%=txt_recdfrom.ClientID %>")
            objrevto = document.getElementById("<%=txt_recdTo.ClientID %>")
            objsuppl = document.getElementById("<%=ddlsupplier.ClientID %>")
            objcompany = document.getElementById("<%=ddlmfgcompany.ClientID %>")
            objstockstatus = document.getElementById("<%=ddl_stocstaus.ClientID %>")
            var year = objrecvyear.options[objrecvyear.selectedIndex].text;
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=StockStatus&BatchNo=" + objbatch.value
                + "&Item=" + objItname.value
                + "&StockType=" + objst_type.value
                + "&PO=" + objpo.value
                + "&StockNo=" + objstockno.value
                + "&ReceiptNo=" + objsrecvno.value
                + "&Group=" + objgroup.value
                + "&Subgroup=" + objsubgroup.value
                + "&Availbalancefrom=" + objavailbalfrom.value
                + "&Availbalanceto=" + objavailto.value
                + "&ExpiryDayfrom=" + objexpirfro.value
                + "&ExpiryDayto=" + objexpirto.value
                + "&Recievedyear=" + year
                + "&Recievedmonth=" + objrecvmnth.value
                + "&RecievedFrom=" + objrevfrom.value
                + "&RecievedTo=" + objrevto.value
                + "&MfgCompany=" + objcompany.value
                + "&Supplier=" + objsuppl.value
                + "&StockStatus=" + objstockstatus.value
         )
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerStockAtatus" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabstockStockAtatus" runat="server" CssClass="Tab1" HeaderText="Overall Stock Status">

                    <ContentTemplate>
                        <div class="custab-panel" id="panelSt_Status">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Stock Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_StType" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Reciept No.</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_recdno"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetrecdNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_recdno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">PO No.</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtPONo"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                            ServiceMethod="GetPONo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtPONo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Stock No</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtstockno"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server"
                                            ServiceMethod="GetPONo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtstockno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span35" class="input-group-addon cusspan" runat="server">Group <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" ID="ddl_itemgroup" AutoPostBack="true" OnSelectedIndexChanged="ddl_itemgroup_SelectedIndexChanged" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span36" class="input-group-addon cusspan" runat="server">Sub.Group </span>
                                        <asp:DropDownList runat="server" ID="ddl_subgroup" AutoPostBack="true" OnSelectedIndexChanged="ddl_itemsubgroup_SelectedIndexChanged" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True"
                                            ID="txt_itemname" OnTextChanged="txt_itemname_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Batch No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True"
                                            ID="txt_batchNo" OnTextChanged="txt_batchNo_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetBatchNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_batchNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_batchNo" ValidChars="123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Avail.Balance?</span>
                                        <asp:TextBox runat="server" placeholder="From" MaxLength="8" CssClass="form-control input-sm col-sm cusmidiumtxtbox9"
                                            ID="txt_avaifro"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_avaifro" ID="FilteredTextBoxExtender3"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789<>" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:TextBox runat="server" placeholder="To" MaxLength="8" CssClass="form-control input-sm col-sm cusmidiumtxtbox9"
                                            ID="txt_availto"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_availto" ID="FilteredTextBoxExtender5"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789<>" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">No.Days to Expire?</span>
                                        <asp:TextBox runat="server" placeholder="From" MaxLength="8" CssClass="form-control input-sm col-sm cusmidiumtxtbox9"
                                            ID="txt_expdaysfro"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_expdaysfro" ID="FilteredTextBoxExtender4"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789<>" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:TextBox runat="server" placeholder="To" MaxLength="8" CssClass="form-control input-sm col-sm cusmidiumtxtbox9"
                                            ID="txt_expdaysto"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_expdaysto" ID="FilteredTextBoxExtender6"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789<>" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Stock Status </span>
                                        <asp:DropDownList runat="server" ID="ddl_stocstaus" OnSelectedIndexChanged="ddl_itemsubgroup_SelectedIndexChanged" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Received Year <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True"
                                            ID="ddlrecivedyear">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Received Month </span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True"
                                            ID="ddlmonth">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Recieved From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_recdfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_recdfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_recdfrom" />

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Recieved To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_recdTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_recdTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_recdTo" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Mfg.Company </span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlmfgcompany">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Supplier</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlsupplier">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm cusbtn"
                                            OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" OnClientClick="return PrintStockStatus();" Text="Print" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
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
                                            <div class="grid" style="width: 100%; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvstockstatus" OnRowDataBound="gvstockstatus_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..."
                                                    Width="1700PX" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No.
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
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("StockID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_stocktype" Visible="false" runat="server" Text='<%# Eval("StockTypeID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_stkno" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
                                                                <asp:Label ID="lbl_stockstatus" Visible="false" runat="server" Text='<%# Eval("StockStatus")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Subheading" Visible="false" runat="server" Text='<%# Eval("SubHeading")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Stock Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_StockName" runat="server" Text='<%# Eval("StockType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Batch No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_batchNo" runat="server" Text='<%# Eval("BatchNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Item Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ItemName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                No.Unit Recv
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_nounitrecv" Width="50px" runat="server" Text='<%# Eval("NoUnit","{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_nounitrecv" ID="FilteredTextBoxExtender9"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789" Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                No.Free Qty 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_free" Width="50px" runat="server" Text='<%# Eval("FreeQty")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_free" ID="FilteredTextBoxExtender10"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789" Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Total Recv Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_totalrecv" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("TotalRecdQty", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_totalrecv" ID="FilteredTextBoxExtender111"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789" Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Rate Per Unit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_rateperunit" Width="50px" runat="server" Text='<%# Eval("CPperunit", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_rateperunit" ID="FilteredTextBoxExtender12"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Discount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_discount" Width="50px" runat="server" Text='<%# Eval("Discount", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_discount" ID="FilteredTextBoxExtender13"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Taxable Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_taxableamount" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("TaxableAmount", "{0:0#.##}")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                SGST
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_SGST" Width="50px" runat="server" Text='<%# Eval("SGST", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_SGST" ID="FilteredTextBoxExtender14"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CGST
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_CGST" Width="50px" runat="server" Text='<%# Eval("CGST", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_CGST" ID="FilteredTextBoxExtender21"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Total CP
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_totalCP" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("CPafterTax", "{0:0#.##}")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                MRP Per Unit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_mrpperunit" Width="50px" runat="server" Text='<%# Eval("MRPperUnit", "{0:0#.##}")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_mrpperunit" ID="FilteredTextBoxExtender15"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Total MRP
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_TotalMRP" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("TotalMRP", "{0:0#.##}")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CP / Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_cpperqty" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("CPPerQty", "{0:0#.##}")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                MRP / Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_MRPperqty" ReadOnly="true" Width="50px" runat="server" Text='<%# Eval("MRPperqty", "{0:0#.##}")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Issue Qty (Unit) 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_IssueQty" runat="server" Width="50px" Text='<%# Eval("TotalIssued")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_IssueQty" ID="FilteredTextBoxExtender16"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789" Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Cond.Qty (Unit) 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_totalcodemn" MaxLength="5" Width="40px" runat="server" Text='<%# Eval("TotalCondemnQty")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_totalcodemn" ID="FilteredTextBoxExtender8"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789" Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Return. Qty (Unit) 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_returnqty" runat="server" Width="50px" Text='<%# Eval("SubstockReturnQty")%>'></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_returnqty" ID="FilteredTextBoxExtender17"
                                                                    runat="server"
                                                                    FilterMode="ValidChars"
                                                                    ValidChars="0123456789." Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Avail. Qty (Unit) 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_avail" ReadOnly="true" runat="server" Width="50px" Text='<%# Eval("TotalUnitBalance")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Expiry Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lbl_expdate" Width="70px" runat="server" Text='<%# Eval("ExpDate")%>'></asp:TextBox>
                                                                <asp:Label ID="lbl_nodays" Visible="false" runat="server" Text='<%# Eval("Nodaystoexpire")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Recvd. Date 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblrecvddate" Width="70px" runat="server" Text='<%# Eval("ReceivedDate")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Supplier
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_supplier" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Company
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_company" runat="server" Text='<%# Eval("CompanyName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
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
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Total Recieved Qty.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totarecievedqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Total Sold/Issue Qty </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalsold"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Total Condm. Qty </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalcondemn"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Total Balance Qty. </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalbalance"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Total CP(R)(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalCP"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Total M.R.P(R)(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalMRP"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Total M.R.P(CD)(₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_MRPCD"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_update" runat="server" Text="Update" Class="btn  btn-sm cusbtn"
                                            OnClick="btn_update_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



