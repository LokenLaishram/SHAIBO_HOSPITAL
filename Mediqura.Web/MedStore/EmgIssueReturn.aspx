<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmgIssueReturn.aspx.cs" Inherits="Mediqura.Web.MedStore.EmgIssueReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel runat="server" ID="upmain" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerEmergIssueReturn" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabEmergIssueReturn" runat="server" CssClass="Tab1" HeaderText="Emerg Return">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelIPIssueReturn">

                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cus-long-span" runat="server">Patient Details <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_EmergNo" onfocus="this.select();" AutoPostBack="True" OnTextChanged="txt_EmergNo_Textchange"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtEmergreturnitemSearch_AutoCompleteExtender" runat="server"
                                            ServiceMethod="GetadvanceSearchEmgNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_EmergNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_EmergNo" ID="FilteredTextBoxExtender1"
                                            runat="server" ValidChars=". |:-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:TextBox runat="server" Visible="False" ID="txtEmergno"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group   pull-right">
                                        <span id="Span1" class="input-group-addon cus-long-span" runat="server">Return No</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_returnNo" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                          
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cus-long-span" runat="server">Return Item</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_returnitem" OnTextChanged="txt_returnitem_TextChanged" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="EmergreturnItem_AutoCompleteExtender" runat="server"
                                                ServiceMethod="Getreturnitem" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_returnitem"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_returnitem" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars=". |:-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                </div>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnadd">
                                <div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group  ">
                                        <span id="Span11" class="input-group-addon cus-long-span" runat="server">MRP/Qty(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtIPDrgIssueNo" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtUHID" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtID" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtSubStockID" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtItemID" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtItemName" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtUnit" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtmrpperqty" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    </div>
									<div class="col-sm-3">
									<div class="form-group input-group  ">
                                        <span id="Span9" class="input-group-addon cus-long-span" runat="server">Lst Return Qty</span>
                                       <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtequvqty" visible="false"  ReadOnly="True"></asp:TextBox>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtlstrtnqty" ReadOnly="True"></asp:TextBox>
                                    </div>
										</div>
									<div class="col-sm-3">
									<div class="form-group input-group  ">
                                        <span id="Span5" class="input-group-addon cus-long-span" runat="server">Return Qty</span>
                                         <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtreturnqty"></asp:TextBox>
										 
                                    </div>
										</div>
                                    <div class="col-sm-3">
										<div class="form-group input-group  ">
                                        <span id="Span6" class="input-group-addon cus-long-span" runat="server">Return Amt(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtreturnamt" ReadOnly="True"></asp:TextBox>
										
                                    </div>
                                    </div>
									</div>
							<div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnadd" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" Text="Add" OnClick="btnadd_Click" />
                                            <asp:Button ID="btnreset" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                            <asp:Button ID="btnprint" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Print" OnClick="btnprint_Click" />
                                        </div>
                                    </div>
                                </div>
                         </asp:Panel>
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
                                                    <asp:GridView ID="gvEmergreturn" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowDataBound="gvEmergreturn_RowDataBound" OnRowCommand="gvEmergreturn_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.11%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_EmergDrgIssueNo" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("EmergDrgIssueNo") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_UHID" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_substockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField visible="false">
                                                                <HeaderTemplate>
                                                                    Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_unit" runat="server" Text='<%# Eval("Unit", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    MRP/Qty(₹)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_mrp" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate >
                                                                    Issued Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Return Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_rtnqty" runat="server" Width="70px" onfocus="this.select();"  Text='<%# Eval("RtnQuantity")%>' disabled="disabled"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Return Amt(₹)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_charge" runat="server" Text='<%# Eval("ReturnAmt", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle CssClass="text-center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                    </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Last RQty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_lastqty" runat="server" Text='<%# Eval("LreturnQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
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
                                                        <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="panelsave" DefaultButton="btnsave">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Total Qty. Return</span>
                                            <asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_returnqty"></asp:TextBox>
                                        </div>
                                    </div>
									<div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Total Amount </span>
                                            <asp:TextBox runat="server" ReadOnly="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_returnAmount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Remarks <span
                                                style="color: red">*</span></span>

                                            <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_remarks"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnsave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Emerg Return List">
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
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cus-long-span" runat="server">Patient Details <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                            ID="txt_tap2_EmergNo" onfocus="this.select();" AutoPostBack="True" OnTextChanged="txt_tap2_EmergNo_Textchange"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetadvanceSearchEmgPatient" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_tap2_EmergNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_tap2_EmergNo" ID="FilteredTextBoxExtender3"
                                            runat="server" ValidChars=". |:-" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:TextBox runat="server" Visible="False" ID="txt_tap2_EmergNos"></asp:TextBox>

                                    </div>
                                </div>
                                
                            </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span2" class="input-group-addon cusspan" runat="server">Return No.</span>
                                                <asp:TextBox runat="server" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_returnNum"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                    ServiceMethod="GetReturnNo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_returnNum"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>


                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span23" class="input-group-addon cusspan" runat="server">From <span
                                                    style="color: red">*</span></span>


                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_retdatefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                    TargetControlID="txt_retdatefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_retdatefrom" />


                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <span id="Span24" class="input-group-addon cusspan" runat="server">To <span
                                                    style="color: red">*</span></span>

                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_returndateTo"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                    TargetControlID="txt_returndateTo" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_returndateTo" />

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
                                                <asp:Button ID="btnsearch1" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch1_Click" />
                                                <asp:Button ID="btnprint1" runat="server" Text="Print" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn" />
                                                <asp:Button ID="btnreset1" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset1_Click" />
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
                                                        <asp:GridView ID="gvEmergreturnlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." OnRowCommand="gvEmergreturnlist_RowCommand"
                                                            Width="100%" HorizontalAlign="Center">
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
                                                                        Item Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
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
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Total return Qty. </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_totalreturnqty"></asp:TextBox>
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
