<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PurchaseCrossChecking.aspx.cs" Inherits="Mediqura.Web.MedStore.PurchaseCrossChecking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainerPurchaseOrderCrossChecking" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="TabPurchaseOrderList" runat="server" CssClass="Tab2" HeaderText="P.O List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelitemgroupmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Requisition No.</span>
                                        <asp:TextBox runat="server" MaxLength="20" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_RequisitionNo"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetRQNumberAuto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_RequisitionNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txt_RequisitionNo" ValidChars="PQRNpqrn1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">P.O No.</span>
                                        <asp:TextBox runat="server" MaxLength="20" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_PONumber"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetPONumberAuto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_PONumber"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txt_PONumber" ValidChars="PONpon1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_DateFrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_DateFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DateFrom" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Date To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_DateTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_DateTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DateTo" />
                                    </div>
                                </div> 
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Supplier</span>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_Supplier" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Supplier</span>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddl_Status" runat="server" class="form-control input-sm col-sm custextbox">  
                                                    <asp:ListItem Value="0">UnRecieved</asp:ListItem>
                                                    <asp:ListItem Value="1">Recieved</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm  cusbtn" OnClick="btnsearch_Click" Text="Search" />
                                            <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" Text="Reset" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblResult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvPurchaseOrderList" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GvPurchaseOrderList_RowCommand"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requisition No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkRequisitionNo" Font-Underline="true" ForeColor="#6666ff" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"
                                                                    ValidationGroup="none" Style="text-align: left !important;" runat="server" Text='<%# Eval("RQNumber")%>'></asp:LinkButton>
                                                                <asp:Label ID="lblRequisitionNo" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RQNumber")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                P.O Number
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPONumber" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("PONumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requisition Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReqDate" runat="server" Text='<%# Eval("RequestedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Approved Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprovedDate" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ApprovedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                P.O Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPODate" runat="server" Text='<%# Eval("PurchaseOrderDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Supplier
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSupplierID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("SupplierID")%>'></asp:Label>
                                                                <asp:Label ID="lblSupplierName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("SupplierName")%>'></asp:Label>
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
                    </ContentTemplate>
                </asp:TabPanel>
                <%--------------------------------END TAB 1-------------------------------%>
                <%--------------------------------START TAB 2-------------------------------%>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="P.O Checking">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessageTab2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Requisition No.</span>
                                        <asp:TextBox runat="server" MaxLength="20" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ReqNo"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Requisition Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ReqDate"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Approved By</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ApprovedBy"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Approved Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ApprovedDate"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Supplier</span>
                                        <asp:TextBox ID="txtTab2_Supplier" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">P.O Number</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_PONumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="div2" runat="server">
                                                <asp:Label ID="lblResultTab2" runat="server" Height="13px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="gridview-container">
                                                    <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:GridView ID="GvPurchaseOrderChecking" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        SlNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# (Container.DataItemIndex+1) %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Item Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ItemID" Style="text-align: left !important;" Visible="false" runat="server"
                                                                            Text='<%# Eval("ItemID")%>'></asp:Label>
                                                                        <asp:Label ID="lblTab2ItemName" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Available Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2AvailableQuantity" runat="server" Text='<%# Eval("TotalAvailableQuantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Probable Rate
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ProbableQuantity" runat="server" Text='<%# Eval("ProbableRate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Requisition Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2RequisitionQuantity" runat="server" Text='<%# Eval("PurchaseRequisitionQuantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Approved Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ApprovedQuantity" runat="server" Text='<%# Eval("TotalApprovedQuantity")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Recieved Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTab2RecievedQuantity" MaxLength="5" runat="server" Text='<%# Eval("RecievedQuantity")%>'></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtTab2RecievedQuantity" ValidChars="1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnTab2Save" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab2Save_Click" Text="Save" />
                                            <asp:Button ID="btnTab2Print" runat="server" Class="btn  btn-sm cusbtn" Text="Print" />
                                            <asp:Button ID="btnTab2Reset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab2Reset_Click" Text="Reset" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <%--------------------------------END TAB 2-------------------------------%>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
