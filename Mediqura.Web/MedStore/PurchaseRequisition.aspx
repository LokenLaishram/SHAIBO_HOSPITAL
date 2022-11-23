<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PurchaseRequisition.aspx.cs" Inherits="Mediqura.Web.MedStore.PurchaseRequisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">

        function PrintRQList() {
            objPRTId = document.getElementById("<%=ddlTab2_RequisitionType.ClientID %>")
            objRQNo = document.getElementById("<%=txtTab2_RequisitionNo.ClientID%>")
            objItemName = document.getElementById("<%=txtTab2_ItemName.ClientID%>")
            objDateFrom = document.getElementById("<%=txtTab2_DateFrom.ClientID %>")
            objDateTo = document.getElementById("<%=txtTab2_DateTo.ClientID %>")
            objRQStatusID = document.getElementById("<%=ddlTab2_Status.ClientID%>")

            window.open("../MedStore/Reports/ReportViewer.aspx?option=PurchaseRequisitionList&PurchaseRequisitionTypeID=" + objPRTId.value + "&RQNumber=" + objRQNo.value + "&ItemName=" + objItemName.value + "&DateFrom=" + objDateFrom.value + "&DateTo=" + objDateTo.value + "&RQStatusID=" + objRQStatusID.value)
        }
    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainerPurchaseRequisition" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPurchaseRequisition" runat="server" CssClass="Tab2" HeaderText="Purchase Requisition">
                    <ContentTemplate>
                        <div class="custab-panel" id="DivPurchaseRequisition">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_RequisitionType" class="input-group-addon cusspan" runat="server" style="color: red">Purchase Requisition Type</span>
                                        <asp:DropDownList ID="ddl_RequisitionType" OnSelectedIndexChanged="ddl_RequisitionType_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Local</asp:ListItem>
                                            <asp:ListItem Value="2">Capital</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="form-group input-group">
                                        <span id="lbl_ItemNames" class="input-group-addon cusspan" runat="server">Item Name<span id="lbl_ItemName_Mandatory" runat="server" style="color: red;"> *</span></span>
                                        <asp:TextBox runat="server" MaxLength="50" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txt_ItemName_TextChanged"
                                            ID="txt_ItemName"></asp:TextBox>
                                        <asp:Label ID="lbl_HdnItemID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lbl_HdnItemName" runat="server" Visible="false"></asp:Label>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetItemNameAuto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_ItemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <div class="form-group input-group">
                                                <span id="lbl_RequisitionQuantity" class="input-group-addon cusspan" runat="server">Requisition Quantity<span id="lbl_ReqstQnty_Mandatory" runat="server" style="color: red;"> *</span></span>
                                                <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" OnTextChanged="txt_RequisitionQuantity_TextChanged" AutoPostBack="true"
                                                    ID="txt_RequisitionQuantity"></asp:TextBox>
                                                <asp:Label ID="lbl_HdnAvailableQuantity" runat="server" Visible="false"></asp:Label>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txt_RequisitionQuantity" ValidChars="1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_Unit"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_FileAttachment" class="input-group-addon cusspan" runat="server">File Attachment<span id="lbl_FileAttch_Mandatory" visible="false" runat="server" style="color: red;"> *</span></span>
                                        <asp:UpdatePanel ID="udpanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:FileUpload ID="FileUpload_FileAttachment" Class="" Style="max-width: 240px; color: #000; font-size: 12px; margin-top: 2px;" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>--%>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Purchase Requisition No.</span>
                                        <asp:Label ID="lbl_PurReqNo" Class="form-control input-sm col-sm custextbox" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnTab1Add" OnClick="btnTab1Add_Click" runat="server" Class="btn  btn-sm cusbtn" Style="margin-right: 15px;" Text="Add" />
                                    </div>
                                </div>
                                <%--<div class="row">
                                    
                                </div>--%>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Purchase Item List</label>
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
                                                <asp:GridView ID="GvPurReqList" CssClass="table-hover grid_table result-table" EmptyDataText="No record found..." OnRowDataBound="GvPurReqList_RowDataBound"
                                                    AutoGenerateColumns="false" Width="100%" HorizontalAlign="Center" OnRowCommand="GvPurReqList_RowCommand" runat="server" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Item Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID")%>' runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_ItemName" Style="text-align: left !important;" Text='<%# Eval("ItemName")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Unit Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_UnitName" Text='<%# Eval("UnitDescription")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Available
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_AvailableItem" Style="text-align: left !important;" Text='<%# Eval("TotalAvailableQuantity")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requisition Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_RQQuantity" Style="text-align: left !important;" Text='<%# Eval("PurchaseRequisitionQuantity")%>' runat="server"></asp:Label>
                                                                <asp:TextBox ID="txt_RQQuantity" Visible="false" Enabled="false" MaxLength="10" Style="text-align: left !important;" Text='<%# Eval("PurchaseRequisitionQuantity")%>' runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txt_RQQuantity" ValidChars="1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Probable Rate
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_ProbableRate" MaxLength="5" Style="text-align: left !important;" Text='<%# Eval("ProbableRate")%>' AutoPostBack="true" OnTextChanged="txt_ProbableRate_TextChanged" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txt_ProbableRate" ValidChars="1234567890." runat="server"></asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Delete
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="Remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');" ValidationGroup="none"><i class="fa fa-trash-o cus-delete-color"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnTab1Save" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab1Save_Click" Text="Save" />
                                        <asp:Button ID="btnTab1Print" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab1Print_Click" Text="Print" />
                                        <asp:Button ID="btnTab1Reset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab1Reset_Click" Text="Reset" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <%--------------------------------END TAB 1-------------------------------%>
                <%--------------------------------START TAB 2-------------------------------%>
                <asp:TabPanel ID="tabPurchaseRequisitionList" runat="server" HeaderText="Requisition List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="DivMsgTab2" runat="server">
                                    <asp:Label ID="lblmessageTab2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Purchase Requisition Type</span>
                                        <asp:DropDownList ID="ddlTab2_RequisitionType" AutoPostBack="true" OnSelectedIndexChanged="ddlTab2_RequisitionType_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Local</asp:ListItem>
                                            <asp:ListItem Value="2">Capital</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Requisition No.</span>
                                        <asp:TextBox runat="server" MaxLength="20" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_RequisitionNo" OnTextChanged="txtTab2_RequisitionNo_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetRQNumberAuto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtTab2_RequisitionNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtTab2_RequisitionNo" ValidChars="PQRNpqrn1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" MaxLength="50" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ItemName" OnTextChanged="txtTab2_ItemName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemNameTab2Auto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtTab2_ItemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:Label ID="lblTab2_HdnItemID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblTab2_HdnItemName" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Date From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_DateFrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtTab2_DateFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTab2_DateFrom" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_DateTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtTab2_DateTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTab2_DateTo" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddlTab2_Status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTab2_Status_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Request</asp:ListItem>
                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Choose Page Size</span>
                                        <asp:DropDownList ID="ddlTab2_PageSize" Width="60px" AutoPostBack="true" OnSelectedIndexChanged="ddlTab2_PageSize_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                    <asp:Button ID="btnTab2Search" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab2Search_Click" Text="Search" />
                                    <asp:Button ID="btnTab2Print" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintRQList()" OnClick="btnTab2Print_Click" Text="Print" />
                                    <asp:Button ID="btnTab2Reset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnTab2Reset_Click" Text="Reset" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblResultTab2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvRequisitionList" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GvRequisitionList_RowCommand" OnRowDataBound="GvRequisitionList_RowDataBound" OnPageIndexChanging="GvRequisitionList_PageIndexChanging"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex+1%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requisition No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequisitionNo" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RQNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Item ID
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemID" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ItemID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Item Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Available Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%# Eval("TotalAvailableQuantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requisition Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequisitionQuantity" runat="server" Text='<%# Eval("PurchaseRequisitionQuantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Approved Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprovedQuantity" runat="server" Text='<%# Eval("TotalApprovedQuantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Probable Rate
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProbableQuantity" runat="server" Text='<%# Eval("ProbableRate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requested By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestedBy" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RequestedBy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requested Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestedDate" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RequestedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ItemStatusID")%>'></asp:Label>
                                                                <asp:Label ID="lblStatus" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ItemStatusName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remark
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemark" Style="text-align: left !important;" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Edit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnEditTab2" runat="server" CommandName="Modify" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">                                                                   
                                                    <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Delete</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnDeleteTab2" runat="server" CommandName="Remove" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
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
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" OnClick="btnexport_Click" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                    </ContentTemplate>
                </asp:TabPanel>
                <%--------------------------------END TAB 2-------------------------------%>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
