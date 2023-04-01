<%@ Page Title="Doctor Payment" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DoctorPayment.aspx.cs" Inherits="Mediqura.Web.MedBills.DoctorPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tab1" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tab_payment" runat="server" CssClass="Tab1" HeaderText="Doctor Payment">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelDistrictMST">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Payable Category <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_doctortype" OnSelectedIndexChanged="ddl_doctortype_SelectedIndexChanged" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="4">Internal Consultant</asp:ListItem> <%----to avoid self select need to fix later ----%>
                                            <asp:ListItem Value="3">Ext. Referal Doctor</asp:ListItem>
                                            <asp:ListItem Value="2">Referal Hospital </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Referal Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_referal" Width="430px" AutoPostBack="True" OnTextChanged="txt_referal_TextChanged" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                            ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_referal"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Service Category <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_servicecategory" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                           <%-- <asp:ListItem Value="1">OPD Services</asp:ListItem>--%>
                                            <asp:ListItem Value="4">Investigation Referal</asp:ListItem>
                                      <%--      <asp:ListItem Value="5">Investigation Reporting</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Due From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdatefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Due To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Voucher</span>
                                        <asp:TextBox runat="server" placeholder="Voucher No." ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_paymentnumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-left">
                                        <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" Class="btn  btn-sm scusbtn" Text="Search" OnClientClick="btnsearch_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn btn-sm scusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                    <div class="pbody">
                                        <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIV5" class="text-center loading" runat="server">
                                                        <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="Gv_Collectionlist" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center">
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
                                                            Date 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            UHID                                    
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_uhid" Style="text-align: left !important;" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Bill No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("BillID") %>'></asp:Label>
                                                            <asp:Label ID="lbl_bill" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("BillNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Patient Name 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_name" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("PatientName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <HeaderTemplate>
                                                            Address 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_address" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("PatAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="true">
                                                        <HeaderTemplate>
                                                            Test List
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_service" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Total Bill
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_billamnt" Width="50px" AutoPostBack="true" OnTextChanged="txt_pc_TextChanged" Text='<%# Eval("TotalAmount","{0:0#.##}") %>' Style="text-align: left !important;" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_billamnt" ID="FilteredTextBoxExtender"
                                                                runat="server"
                                                                ValidChars="0123456789." Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Dis.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>                                                           
                                                               <asp:TextBox ID="txt_discount" Width="50px" MaxLength="3" AutoPostBack="true" OnTextChanged="txt_pc_TextChanged" Text='<%# Eval("DiscountAmnt","{0:0#.##}") %>' Style="text-align: left !important;" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_discount" ID="FilteredTextBoxExtender1"
                                                                runat="server"
                                                                ValidChars="0123456789." Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Net Bill.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>  
                                                            <asp:Label ID="lbl_netbill" Style="text-align: left !important;" runat="server"
                                                               Text='<%# Eval("NetAmount","{0:0#.##}") %>'></asp:Label>                                                            
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Referral %
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_ReferralPC" Width="50px" MaxLength="3" AutoPostBack="true" OnTextChanged="txt_pc_TextChanged" Text='<%# Eval("ReferralPC","{0:0#.##}") %>' Style="text-align: left !important;" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_ReferralPC" ID="FilteredTextBoxExtender3"
                                                                runat="server"
                                                                ValidChars="0123456789." Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Runner %
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_Runnerpc" Width="50px" MaxLength="3" AutoPostBack="true" OnTextChanged="txt_pc_TextChanged" Text='<%# Eval("RunnerPC","{0:0#.##}") %>' Style="text-align: left !important;" runat="server"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_Runnerpc" ID="FilteredTextBoxExtender4"
                                                                runat="server"
                                                                ValidChars="0123456789." Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Referral Payable
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ReferralPayable" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("ReferralPayable","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Runner Payable
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_RunnerPayable" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("RunnerPayable","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height: 10PX"></div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Total Amount (₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Total Discount (₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_discountamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Net Amount (₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_NetAmount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Doctor Payable<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDoctorPayable"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Doctor Discount (₹) </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoCompleteType="Disabled"
                                            ID="txt_DocDiscount" onkeyup="return  DiscountCal();"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_DocDiscount" ID="FilteredTextBoxExtender1"
                                            runat="server"
                                            ValidChars="0123456789." Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Paid Amount (₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_paidamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Remarks <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_remarks"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnCaseID" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_pay" runat="server" Class="btn  btn-sm scusbtn" Text="Pay" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btn_pay_Click" />
                                        <asp:Button ID="btn_printrecv" runat="server" OnClick="btnprint_Click" Class="btn  btn-sm scusbtn" Text="Print R" />
                                        <asp:Button ID="btnexport" runat="server" Class="btn  btn-sm scusbtn" Text="Excel" OnClick="btnexport_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnexport" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tab2" runat="server" CssClass="Tab2" HeaderText="Payment History">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div3" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Payable Category <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl2DoctorType" OnSelectedIndexChanged="ddl2doctortype_SelectedIndexChanged" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Internal Consultant</asp:ListItem>
                                            <asp:ListItem Value="3">Ext. Referal Doctor</asp:ListItem>
                                            <asp:ListItem Value="2">Referal Hospital </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Referal Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt2Referal" Width="430px" AutoPostBack="True" OnTextChanged="txt_referal_TextChanged" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt2Referal"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Service Category <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl2servicecategory" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">OPD Services</asp:ListItem>
                                            <asp:ListItem Value="4">Investigation Referal</asp:ListItem>
                                            <asp:ListItem Value="5">Investigation Reporting</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Paid From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_paidfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_paidfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_paidfrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Paid To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_paidto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_paidto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_paidto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Voucher Number</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_paymentnumbers"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp">
                                        <asp:Button ID="btn_searchhistory" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btn_searchhistory_Click" />
                                        <asp:Button ID="btn_prinhistory" runat="server" Class="btn  btn-sm scusbtn" Text="Print" OnClick="btn_prinhistory_Click" />
                                        <asp:Button ID="btn_historyreset" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btn_historyreset_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div4" runat="server">
                                            <asp:Label ID="lbl_result2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-ML">
                                                <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="Gv_paymenthistory" OnRowCommand="Gv_paymenthistory_RowCommand" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDeleting="PendingRecordsGridview_RowDeleting"
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
                                                                    Voucher No.                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_paymentnumber" Style="text-align: left !important;" runat="server" Text='<%# Eval("PaymentNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Category                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_categoryID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("ServiceCategory") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_category" Style="text-align: left !important;" runat="server" Text='<%# Eval("PaymentCatgory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Doctor Name 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_name" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Doctor") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Ref PC
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdPC" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ReferralPC","{0:0#.##}") %>'></asp:Label>
                                                                    %
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bill From
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillFrom" runat="server" Text='<%# Eval("DateFrom","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bill To 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillTo" runat="server" Text='<%# Eval("Dateto","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Bill 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdTotalBillAmountt" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdTotalBillAmount","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Dis
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdTotalDiscount" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdTotalDiscount","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    NetAmount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdNetAmount" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdNetAmount","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Referral Payable
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdReferralPayable" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdReferralPayable","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Ref Dis
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdRefDiscount" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdRefDiscount","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Paid
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGdPaid" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GdPaid","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Paid On 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_doctorID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("DoctorID") %>'></asp:Label>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("PaidOn","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_printvoucher" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                     <i class="fa fa-print" style="text-align:center;"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Delete
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_Delete" Style="color: red; text-align: center !important; font-size: 12px" runat="server" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
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
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Amount (₹) <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalpayableamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Total Discount (₹) </span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totaldiscount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Total PaidAmount (₹)<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalpaidamount"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>

    </asp:UpdatePanel>

    <script type="text/javascript">

        function DiscountCal() {
            var DoctorPayable = document.getElementById("<%=txtDoctorPayable.ClientID %>").value;
            var DocDiscount = document.getElementById("<%=txt_DocDiscount.ClientID %>").value;

            if (+DoctorPayable >= (+DocDiscount)) {
                document.getElementById("<%=txt_paidamount.ClientID%>").value = (+DoctorPayable - (+DocDiscount)).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            }
            else {
                document.getElementById("<%=txt_DocDiscount.ClientID%>").value = "";
                document.getElementById("<%=txt_paidamount.ClientID%>").value = DoctorPayable;
                alert("Discount amount could not be greater than doctor payable.");
            }
        }
    </script>
</asp:Content>


