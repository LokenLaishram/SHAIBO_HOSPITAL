<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DiscountApproval.aspx.cs" Inherits="Mediqura.Web.Admin.DiscountApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
   
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerreferal" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Discount Approval List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelDiscount">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Service Type </span>
                                        <asp:DropDownList ID="ddl_serviceType" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_serviceType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">-select-</asp:ListItem>
                                            <asp:ListItem Value="1">OP Services</asp:ListItem>
                                            <asp:ListItem Value="2">OP INV</asp:ListItem>
                                            <asp:ListItem Value="3">IP Services</asp:ListItem>
                                            <asp:ListItem Value="4">Emergency</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Bill No </span>
                                        <asp:Label Visible="false" ID="Label2" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_billNo"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="autoConpleteBill" runat="server"
                                            ServiceMethod="GetBillNoByServiceType" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_billNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">UHID</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_UHID"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                            ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_UHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_UHID" ID="FilteredTextBoxExtender4"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Name </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_name"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Address </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Discount Status</span>
                                        <asp:DropDownList ID="ddl_discount_status" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Date From</span>
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
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Date To </span>
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
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Requested By</span>
                                        <asp:DropDownList ID="ddl_requested_by" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">

                                        <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="false" Text="Search" OnClick="btn_search_Click" />
                                        <asp:Button ID="btn_reset" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="false" Text="Reset" OnClick="btn_reset_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div2" runat="server">
                                            <asp:Label ID="lblmessage2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 28vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                                <asp:GridView ID="GVDiscountList" runat="server" CssClass="table-hover grid_table result-table"
                                                    AllowPaging="false"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVDiscountList_RowDataBound" OnRowCommand="GVDiscountList_RowCommand"
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
                                                                Request No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestNo" Visible="false" runat="server"
                                                                    Text='<%# Eval("RequestNo") %>'></asp:Label>

                                                                <asp:LinkButton ID="lblRequest" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                      
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                UHID
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUHID" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("UHID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPatName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("PatName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Requested on
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReqOn" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("RequestOn") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Amount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Request Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReqAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Discount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Approve Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAppproveAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Approve", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisStatus" Visible="false" runat="server"
                                                                    Text='<%# Eval("DisStatus") %>'></asp:Label>
                                                                <asp:Label ID="lblDiscountStatus" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("DiscountStatus") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                User
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReqDoc" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ReqBy") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Total on Request</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_total_on_req"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group input-group">
                                                <span id="Span15" class="input-group-addon cusspan" runat="server">Total Request Amount</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_total_requested"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group input-group">
                                                <span id="Span16" class="input-group-addon cusspan" runat="server">Total Approval</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_total_approval"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group input-group">
                                                <span id="Span22" class="input-group-addon cusspan" runat="server">Total Approved Amount</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_total_approve_amount"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="row cusrow ">
                            <div class="col-sm-12 ">

                                <asp:ModalPopupExtender ID="MDDiscount" runat="server" TargetControlID="btnDefault" PopupControlID="popupwindow"
                                    BackgroundCssClass="modalBackground" Enabled="True">
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="popupwindow" Style="display: none;" DefaultButton="btnPopSave">

                                    <div style="width: 100%" class="row">
                                        <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">

                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span4" class="input-group-addon cusspan" runat="server">UHID </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_uhid"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Name </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_name"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Address </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_address"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group input-group">
                                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Remarks</span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_remarks"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="fixeddiv">
                                                        <div class="row fixeddiv" id="divpopup" runat="server">
                                                            <asp:Label ID="lblMessagePopup" runat="server" Height="13px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grid" style="float: left; width: 100%; height: 38vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloadingPopUp" class="text-center loading" runat="server">
                                                            <asp:Image ID="DIVloadingPopUpImage" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                                <asp:GridView ID="GVDiscount" runat="server" CssClass="table-hover grid_table result-table"
                                                    AllowPaging="false"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVDiscount_RowDataBound"
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
                                                                ServiceID
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServiceID" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceID") %>'></asp:Label>
                                                                <asp:Label ID="lblBillNo" Visible="false" runat="server"
                                                                    Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                <asp:Label ID="lblUHID" Visible="false" runat="server"
                                                                    Text='<%# Eval("UHID") %>'></asp:Label>
                                                                <asp:Label ID="lblisDis" Visible="false" runat="server"
                                                                    Text='<%# Eval("isDis") %>'></asp:Label>
                                                                <asp:Label ID="lblServiceType" Visible="false" runat="server"
                                                                    Text='<%# Eval("serviceTypeID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Service
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServiceName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQnty" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Quantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServiceCharge" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("amount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Net Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNetAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("NetAmount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDisType" Visible="false" runat="server"
                                                                    Text='<%# Eval("DisType") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_discount_type" runat="server" OnSelectedIndexChanged="ddl_discount_type_SelectedIndexChanged" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                                    <asp:ListItem Value="0">Fix</asp:ListItem>
                                                                    <asp:ListItem Value="1">PC</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Value
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_dis_value" Text='<%# Eval("DisValue", "{0:0#.##}") %>' MaxLength="4" OnTextChanged="txt_dis_value_TextChanged"></asp:TextBox>
                                                                 <asp:FilteredTextBoxExtender TargetControlID="txt_dis_value" ID="FilteredTextBoxExtender1"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_discount_amt" Text='<%# Eval("DisAmount", "{0:0#.##}") %>' Style="text-align: left !important;" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doctor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDoctorID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("DoctorID") %>'></asp:Label>
                                                                <asp:Label ID="lblReqDoc" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Doctor") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Discount</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="discountCheck" OnCheckedChanged="discountCheck_CheckedChanged" runat="server" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Amount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_total_Amount"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Total Discount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_total_discount"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Total Net Amount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_total_net_amount"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_remarks"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-6"></div>
                                                <div class="col-sm-6">
                                                    <div>
                                                        <asp:Button ID="btnPopReset" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Reset" OnClick="btnPopReset_Click" />
                                                        <asp:Button ID="btnClosed" runat="server" Class="btn  btn-sm cusbtn exprt" UseSubmitBehavior="false" Text="Close" />
                                                        <asp:Button ID="btnPopSave" Style="margin-left: 8px" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn exprt" Text="Save" OnClick="btnPopSave_Click" />
                                                        <asp:Button ID="btnDefault" Style="display:none" runat="server" OnClick="btnDefault_Click" />
                                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                                            <asp:DropDownList ID="ddl_set_discount_status" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                                runat="server">
                                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Verify"></asp:ListItem>
                                                                <asp:ListItem Value="4" Text="Reject"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-sm-12 ">

                                <asp:ModalPopupExtender ID="MDApproval" runat="server" TargetControlID="btnSample" PopupControlID="popupApproval"
                                    BackgroundCssClass="modalBackground" Enabled="True">
                                </asp:ModalPopupExtender>
                                <asp:Panel runat="server" ID="popupApproval" Style="display: none;" DefaultButton="btnPopApprSave">

                                    <div style="width: 100%" class="row">
                                        <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">

                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span5" class="input-group-addon cusspan" runat="server">UHID </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_appr_uhid"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Name </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_appr_name"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Address </span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_appr_address"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group input-group">
                                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Request Remarks</span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_appr_req_remarks"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group input-group">
                                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Approved Remarks</span>
                                                        <asp:TextBox ReadOnly="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_pop_appr_ap_remark"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="fixeddiv">
                                                        <div class="row fixeddiv" id="divpop1" runat="server">
                                                            <asp:Label ID="lblmessagepop1" runat="server" Height="13px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grid" style="float: left; width: 100%; height: 38vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloadingPopUp1" class="text-center loading" runat="server">
                                                            <asp:Image ID="DIVloadingPopUpImage1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                                <asp:GridView ID="GVDiscountApproveList" runat="server" CssClass="table-hover grid_table result-table"
                                                    AllowPaging="false"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVDiscountApproveList_RowDataBound"
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
                                                                Service
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprServiceName" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                                 <asp:Label ID="lblApprServiceID" Visible="false" runat="server"
                                                                    Text='<%# Eval("ServiceID") %>'></asp:Label>
                                                                <asp:Label ID="lblApprBillNo" Visible="false" runat="server"
                                                                    Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                <asp:Label ID="lblApprUHID" Visible="false" runat="server"
                                                                    Text='<%# Eval("UHID") %>'></asp:Label>
                                                                <asp:Label ID="lblApprisDis" Visible="false" runat="server"
                                                                    Text='<%# Eval("isDis") %>'></asp:Label>
                                                                <asp:Label ID="lblApprServiceType" Visible="false" runat="server"
                                                                    Text='<%# Eval("serviceTypeID") %>'></asp:Label>
                                                                <asp:Label ID="lblBilltype" Visible="false" runat="server"
                                                                    Text='<%# Eval("BillType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprQnty" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Quantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprServiceCharge" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("amount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Net Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprNetAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("NetAmount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Approve Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprAmount" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("DisAmount", "{0:0#.##}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprDisType" Visible="false" runat="server"
                                                                    Text='<%# Eval("DisType") %>'></asp:Label>
                                                                <asp:DropDownList ID="ddl_appr_discount_type" runat="server" OnSelectedIndexChanged="ddl_appr_discount_type_SelectedIndexChanged" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                                    <asp:ListItem Value="0">Fix</asp:ListItem>
                                                                    <asp:ListItem Value="1">PC</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Lab Share
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_appr_hos_value" MaxLength="4" OnTextChanged="txt_appr_hos_value_TextChanged"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_appr_hos_value" ID="FilteredTextBoxExtender2"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doc. Share
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_appr_doc_value" MaxLength="4" OnTextChanged="txt_appr_doc_value_TextChanged"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_appr_doc_value" ID="FilteredTextBoxExtender3"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Ref. Share
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_appr_ref_value" MaxLength="4"  OnTextChanged="txt_appr_ref_value_TextChanged"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_appr_ref_value" ID="FilteredTextBoxExtender5"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Amount
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_appr_discount_amt"  Style="text-align: left !important;" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doctor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblapprDoctorID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("DoctorID") %>'></asp:Label>
                                                              <asp:Label ID="lblapprReqDoc" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Doctor") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Total Amount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_amount"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span26" class="input-group-addon cusspan" runat="server">Total Discount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_discount"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <span id="Span27" class="input-group-addon cusspan" runat="server">Total Net Amount</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_net_amount"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="form-group input-group">
                                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Total Lab  Share</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_hos_share"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group input-group">
                                                        <span id="Span30" class="input-group-addon cusspan" runat="server">Total Doc Share</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_doc_share"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group input-group">
                                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Total Reporting Share</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_appr_tot_rpt_share"></asp:TextBox>
                                                    </div>
                                                </div>
                                              <div class="col-sm-3">
                                                    <div class="form-group input-group">
                                                        <span id="Span32" class="input-group-addon cusspan" runat="server">Total Share</span>
                                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                            ID="txt_total_share"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-lg-6"></div>
                                                <div class="col-sm-6">
                                                    <div>
                                                        <asp:Button ID="btnPopapprReset" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Reset" OnClick="btnPopapprReset_Click" />
                                                        <asp:Button ID="btnPopApprClose" runat="server" Class="btn  btn-sm cusbtn exprt" UseSubmitBehavior="false" Text="Close" />
                                                        <asp:Button ID="btnPopApprSave" Style="margin-left: 8px" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn exprt" Text="Save" OnClick="btnPopApprSave_Click" />
                                                        <asp:Button ID="btnSample" Style="display:none" runat="server" OnClick="btnSample_Click" />
                                                     

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <asp:Button ID="btnOnLoad" runat="server" OnClick="btnOnLoad_Click" style="display:none" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
     <script type="text/javascript">
         document.getElementById("<%=btnOnLoad.ClientID %>").click();
    </script>
</asp:Content>
