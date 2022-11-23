<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="IPService.aspx.cs" Inherits="Mediqura.Web.MedAdmission.IPService" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:TabContainer ID="tabcontaineripdservice" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%" Style="display: block; visibility: visible;">
        <asp:TabPanel ID="tabipdservicerecord" runat="server" HeaderText="IP Service Record">
            <ContentTemplate>
                 <div class="custab-panel" id="ipdservicerecorddetaildiv">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div class="row" id="div1" runat="server" style="margin-top: -9px; margin-bottom: 5px; padding-bottom: 3px; padding-top: -3px;">
                                <asp:Label ID="lblmessage" Height="13px" Style="margin-right: 950px" runat="server"></asp:Label>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IPNo</span>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_autoipno" AutoPostBack="True" OnTextChanged="txt_autoipno_TextChanged"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                    ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_autoipno"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                </asp:AutoCompleteExtender>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_autoipno" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtname"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtname" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Gender</span>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_gender"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_gender" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_age"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txt_age" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_department" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddldepartment" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_doctor" class="input-group-addon cusspan" runat="server">Doctor</span>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddldoctor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddldoctor" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel runat="server" ID="panel3" DefaultButton="btnadd">
                                <div class="row">

                                    <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Services</span>
                                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtservices" AutoPostBack="True" OnTextChanged="txtservices_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                        ServiceMethod="GetServices" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtservices"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:Label ID="lblservicename" runat="server" Visible="false"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtservices" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Quantity</span>
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtquantity"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtquantity" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Amount</span>
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                        ID="txtservicecharge"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender TargetControlID="txtservicecharge" ID="FilteredTextBoxExtender3"
                                                        runat="server" ValidChars="0123456789."
                                                        Enabled="True">
                                                    </asp:FilteredTextBoxExtender>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtservicecharge" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group input-group">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" Class="btn  btn-sm cusbtn" OnClick="btnadd_Click" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnadd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row cusrow ">
                                <div class="col-sm-12">
                                    <div class="row" style="height: 35vh !important; overflow: auto;">
                                        <asp:UpdateProgress ID="updateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    <%-- <center class="white">
                                        Loading...
                                    </center>--%>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvipservicerecordlist" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowDataBound="gvipservicerecordlist_RowDataBound" AutoGenerateColumns="False" OnRowCommand="gvipservicerecordlist_RowCommand"
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
                                                                Services
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ServiceID") %>'></asp:Label>
                                                                <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("TestName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Charges
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("ServiceCharge")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Net Charges
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetServiceCharge")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header">Delete</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" ValidationGroup="none" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"
                                                                    OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-edit-delete-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="gvipservicerecordlist" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height: 15px">
                            </div>
                            <div class="row">
                                <asp:Panel runat="server" ID="panel4">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Total Quantity</span>
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttotalquantity"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txttotalquantity" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm cusbtn" />
                                            <%--<asp:Button ID="Btnprintcs" runat="server" Text="Print CS"  Class="btn  btn-sm cusbtn" />
                                            --%>
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                        </div>
                                    </div>
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
      <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Lab Collection">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2" style="visibility:visible">
                            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                                <div class="custab-panel" id="paneldepositlist">
                                    <div class="row" id="divmsg2" runat="server" style="margin-top: -9px; margin-bottom: 5px; padding-bottom: 3px; padding-top: -3px;">
                                        <asp:Label ID="lblmessage2" Height="13px" runat="server"></asp:Label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">

                                            <div class="form-group input-group">
                                                <span id="Span4" class="input-group-addon cusspan" style="color: red" runat="server">UHID</span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txtautoUHID" AutoPostBack="True" ></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                    ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoUHID"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                </asp:AutoCompleteExtender>
                                                <asp:FilteredTextBoxExtender TargetControlID="txtautoUHID" ID="FilteredTextBoxExtender4"
                                                    runat="server" ValidChars="0123456789"
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </div>

                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtpatientNames" ></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                    ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span7" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                                <asp:DropDownList ID="ddlpaymentmodes" runat="server" class="form-control input-sm col-sm custextbox">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtdatefrom"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtdatefrom" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
                                                    style="color: red">*</span> </span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtto"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtto" />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span11" class="input-group-addon cusspan" runat="server">Status</span>
                                                <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-8"></div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search"/>
                                                <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" Text="Print" />
                                                <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" id="divmsg3" runat="server" style="padding-bottom: 2px; margin-left: 30px; margin-right: 30px; margin-bottom: 0px;">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row cusrow ">
                                        <div class="col-sm-12">
                                            <div class="panel panel-info cuspanel">
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
                                                        <asp:GridView ID="gvdepositlist" runat="server" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." AutoGenerateColumns="False" 
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
                                                                        Bill No.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        UHID
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbluhid" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("UHID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Address
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Total Bill Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("TotalBillAmount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Adjusted Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaajustedamount" runat="server" Text='<%# Eval("AdjustedAmount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Discounted Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbldiscountedamount" runat="server" Text='<%# Eval("DiscountedAmount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <span class="cus-Delete-header">Paid Amount</span>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblamount" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="3%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Added By
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Added On
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
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

                                                                        <span class="cus-Delete-header">Delete</span>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                            OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                    <i class="fa fa-trash-o cus-edit-delete-color"></i>
                                        
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Print
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("BillNo")%>'); return false;">Print</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BackColor="#D8EBF5" />
                                                        </asp:GridView>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span12" class="input-group-addon cusspan" runat="server">Total Bill  </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txttotalbillamount"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span13" class="input-group-addon cusspan" runat="server">Total Adjusted </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txtajusted"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span22" class="input-group-addon cusspan" runat="server">Total Discounted </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txttotaldiscounted"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Total Paid </span>
                                                <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                    ID="txttotalpaid"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="text-align: right!important; margin-right: -10px;">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right" style="padding-right: 16px; margin-top: 8px;">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox2"
                                                        runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn" Text="Export"  />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:TabPanel>  
    </asp:TabContainer>
</asp:Content>
