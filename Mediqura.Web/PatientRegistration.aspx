<%@ Page Title="Patient Registration" Language="C#" MasterPageFile="~/Mediqura.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="PatientRegistration.aspx.cs" Inherits="Mediqura.Web.PatientRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <style>
        .table th {
            position: relative;
        }
    </style>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" CssClass="Tab" runat="server"
                Width="100%">
                <asp:TabPanel ID="tabpatientlist" runat="server" HeaderText="Patient List">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="panelPatientRegistration">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span2" visible="true" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Visible="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpatientNames" placeholder="search your patient...." AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
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
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Date To  </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddlstatus" AutoPostBack="true" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Added By </span>
                                            <asp:DropDownList ID="ddl_user" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Show Entries </span>
                                            <asp:DropDownList ID="ddl_show" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_show_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="1000">All</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnresets_Click" />
                                            <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="return PrintPatinetList();" Text="Print" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="divmsg3" runat="server">
                                                <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                                <asp:Label ID="lbl_totalrecords" Visible="false" runat="server" Height="13px"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">
                                                    <asp:GridView ID="GvPatientList" runat="server" CssClass="table-hover grid_table result-table grid"
                                                        ShowFooter="true" EmptyDataText="No record found..." OnPageIndexChanging="GvPatientList_PageIndexChanging"
                                                        AllowPaging="true" PageSize="10" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                        OnRowCommand="GvPatientList_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">

                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)+(GvPatientList.PageIndex)*GvPatientList.PageSize %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UHID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUHID" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" class="hidden" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sex
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgender" runat="server" Text='<%# Eval("GenderName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Age
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblage" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Agecount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <%--  <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("ContactNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="60px" Height="18px" MaxLength="100" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                                    <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
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
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Billing
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btn_pay" Class="btn  btn-sm cusBtn2" runat="server" CommandName="Pay" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to bill.?');">
                                                                        Pay
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                        </Columns>

                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />

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
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                        runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        <%-- <asp:ListItem Value="2" Text="PDF"></asp:ListItem>--%>
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
                            </div>
                            <script type="text/javascript">
                                var Page
                                function pageLoad() {

                                    Page = Sys.WebForms.PageRequestManager.getInstance()
                                    Page.add_initializeRequest(OnInitializeRequest)

                                }

                                function OnInitializeRequest(sender, args) {

                                    var postBackElement = args.get_postBackElement()

                                    if (Page.get_isInAsyncPostBack()) {
                                        ddl_department_SelectedIndexChanged
                                        alert('One request is already in progress....')
                                        args.set_cancel(true)
                                    }

                                }

                                function GetBirthDate() {
                                    var now = new Date();
                                    var today = new Date(now.getFullYear(), now.getMonth(), now.getDate());
                                    var yearNow = now.getFullYear();
                                    var monthNow = now.getMonth() + 1;
                                    var dateNow = now.getDate();


                                    var ageDay = document.getElementById('<%=txtday.ClientID %>').value == "" ? "0" : document.getElementById('<%=txtday.ClientID %>').value;
                                    var ageMonth = document.getElementById('<%=txtmonth.ClientID %>').value == "" ? "0" : document.getElementById('<%=txtmonth.ClientID %>').value;
                                    var ageYear = document.getElementById('<%=txtage.ClientID %>').value == "" ? "0" : document.getElementById('<%=txtage.ClientID %>').value;
                                    var dayValidate;

                                    switch (parseInt(ageMonth)) {
                                        case 1:
                                            dayValidate = 31;
                                            break;
                                        case 2:
                                            dayValidate = 29;

                                            break;
                                        case 3:
                                            dayValidate = 31;
                                            break;
                                        case 4:
                                            dayValidate = 30;
                                            break;
                                        case 5:
                                            dayValidate = 31;
                                            break;
                                        case 6:
                                            dayValidate = 30;
                                            break;
                                        case 7:
                                            dayValidate = 31;
                                            break;
                                        case 8:
                                            dayValidate = 31;
                                            break;
                                        case 9:
                                            dayValidate = 30;
                                            break;
                                        case 10:
                                            dayValidate = 31;
                                            break;
                                        case 11:
                                            dayValidate = 30;
                                            break;
                                        case 12:
                                            dayValidate = 31;
                                            break;
                                        default:
                                            dayValidate = 31;

                                            break;

                                    }
                                    if (ageDay == "0" && ageMonth == "0" && ageYear == "0") {
                                        document.getElementById('<%=txtdob.ClientID %>').value = "";

                                    } else {
                                        if (ageMonth > 12) {
                                            document.getElementById('<%=txtmonth.ClientID %>').value = "";
                                            ageMonth = 0;
                                        }

                                        if (ageDay > dayValidate) {
                                            document.getElementById('<%=txtday.ClientID %>').value = "";
                                            ageDay = 0;

                                        }
                                        var dob = new Date(ageYear, ageMonth, ageDay);

                                        var yearDob = dob.getYear();
                                        var monthDob = dob.getMonth();
                                        var dateDob = dob.getDate();
                                        var age = {};


                                        var yearDiff = yearNow - ageYear;
                                        var monthDiff = monthNow - ageMonth;

                                        var dayDiff = dateNow - ageDay;
                                        var month = 0;
                                        if (monthDiff <= 0) {
                                            month = 12 + monthDiff;
                                            yearDiff--;
                                        } else {
                                            month = monthDiff;
                                        }
                                        day = 0;
                                        if (dayDiff <= 0) {

                                            switch (month) {
                                                case 1:
                                                    day = 31;
                                                    break;
                                                case 2:
                                                    if (yearDiff % 4 == 0) {
                                                        day = 29;
                                                    } else {
                                                        day = 28;
                                                    }

                                                    break;
                                                case 3:
                                                    day = 31;
                                                    break;
                                                case 4:
                                                    day = 30;
                                                    break;
                                                case 5:
                                                    day = 31;
                                                    break;
                                                case 6:
                                                    day = 30;
                                                    break;
                                                case 7:
                                                    day = 31;
                                                    break;
                                                case 8:
                                                    day = 31;
                                                    break;
                                                case 9:
                                                    day = 30;
                                                    break;
                                                case 10:
                                                    day = 31;
                                                    break;
                                                case 11:
                                                    day = 30;
                                                    break;
                                                case 12:
                                                    day = 31;
                                                    break;

                                            }
                                            day = day + dayDiff;
                                            month--;
                                            if (month <= 0) {
                                                month = 12;
                                                yearDiff--;

                                            }

                                        } else {
                                            day = dayDiff;
                                        }

                                        if (new String(day).length == 1) {
                                            day = "0" + day;
                                        }

                                        if (new String(month).length == 1) {
                                            month = "0" + month;
                                        }

                                        age = {
                                            years: yearDiff,
                                            months: month,
                                            days: day
                                        };
                                        document.getElementById('<%=txtdob.ClientID %>').value = age.days + "/" + age.months + "/" + age.years;
                                    }

                                }

                                function PrintPatinetList() {

                                    onjname = document.getElementById("<%=txtpatientNames.ClientID %>");
                                    objdatefrom = document.getElementById("<%= txtdatefrom.ClientID %>");
                                    objdateto = document.getElementById("<%=txtto.ClientID %>");
                                    objstatus = document.getElementById("<%=ddlstatus.ClientID %>");
                                    Objaddedby = document.getElementById("<%=ddl_user.ClientID %>");

                                    window.open("../Reports/ReportViewer.aspx?option=RegistrationList&PatientName=" + onjname.value + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&UserID=" + Objaddedby.value)

                                }
                            </script>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpatientDetails" runat="server" HeaderText="Add New">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panelPatientList">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">QR Scan</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtScan" ValidateRequestMode="Disabled" AutoPostBack="true" OnTextChanged="txtScan_TextChanged"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Registration Type </span>
                                            <asp:DropDownList ID="ddl_registrationtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_registrationtype_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="1">New</asp:ListItem>
                                                <asp:ListItem Value="2">Old</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbluhid" class="input-group-addon cusspan" style="color: red" runat="server">UHID</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtuhid"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblsalutation" class="input-group-addon cusspan mandbg" runat="server">Salutation <span
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsalute" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlsalute_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlsalute" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="lblname" class="input-group-addon cusspan mandbg" runat="server">Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" AutoPostBack="true" spellcheck="false" Class="form-control input-sm col-sm custextbox"
                                                ID="txtname" MaxLength="50" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                ServiceMethod="GetAutoUHID" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtname"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtname" ID="FilteredTextBoxExtender8"
                                                runat="server" ValidChars="(.)ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789:|-> " FilterMode="ValidChars"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblco" class="input-group-addon cusspan " runat="server">C/O </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="50"
                                                ID="txtco"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtco" ID="FilteredTextBoxExtender9"
                                                runat="server" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz "
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblrelationship" class="input-group-addon cusspan " runat="server">Relationship</span>
                                            <asp:DropDownList ID="ddlrelationship" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbldob" class="input-group-addon cusspan" runat="server">DOB </span>
                                            <asp:TextBox runat="server" OnTextChanged="txtdob_TextChanged" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdob"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdob" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdob" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblage" class="input-group-addon cusspan2 mandbg" runat="server">Age<span
                                                style="color: red">*</span></span>
                                            <asp:HiddenField runat="server" ID="hdndateofbirthcal" />
                                            <asp:TextBox runat="server" MaxLength="3" Class="form-control input-sm col-sm cusmidiumtxtbox3" onKeyUp="GetBirthDate();"
                                                ID="txtage" placeholder="Age"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtage" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:TextBox runat="server" MaxLength="3" Class="form-control input-sm col-sm cusmidiumtxtbox3" onKeyUp="GetBirthDate();"
                                                ID="txtmonth" placeholder="Month"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtmonth" ID="FilteredTextBoxExtender7"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:TextBox runat="server" MaxLength="3" Class="form-control input-sm col-sm cusmidiumtxtbox3" onKeyUp="GetBirthDate();"
                                                ID="txtday" placeholder="Day"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtday" ID="FilteredTextBoxExtender6"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblgender" class="input-group-addon cusspan mandbg" runat="server">Gender <span
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlgender" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlgender" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblreligion" class="input-group-addon cusspan" runat="server">Religion</span>
                                            <asp:DropDownList ID="ddlreligion" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblmaritalstatus" class="input-group-addon cusspan mandbg" runat="server">Marital Status <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlmarital" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Blood Group </span>
                                            <asp:DropDownList ID="ddl_bloodgrp" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblidmark" class="input-group-addon cusspan" runat="server">ID Mark</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="50"
                                                ID="txtidmark"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtidmark" ID="FilteredTextBoxExtender10"
                                                runat="server" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz "
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblcountry" class="input-group-addon cusspan mandbg" runat="server">Country <span
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlcountry" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlcountry" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblstate" class="input-group-addon cusspan mandbg" runat="server">State <span id="lblStateMandatory" runat="server"
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" AutoPostBack="true" class="form-control input-sm col-sm custextbox cus_ddl">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbldistrict" class="input-group-addon cusspan" runat="server">District</span>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddldistrict" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlstate" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbladdress" class="input-group-addon cusspan mandbg" runat="server">Address <span style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="100"
                                                ID="txtaddress"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtaddress" ID="FilteredTextBoxExtender11"
                                                runat="server" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblpin" class="input-group-addon cusspan" runat="server">Pin 
                                            </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpin" MaxLength="6"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpin" ID="FilteredTextBoxExtender3"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblemail" class="input-group-addon cusspan" runat="server">Email</span>
                                            <asp:TextBox runat="server" MaxLength="30" Class="form-control input-sm col-sm custextbox"
                                                ID="txtemail"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblcontactno" class="input-group-addon cusspan" runat="server">Contact No. </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtcontactno" MaxLength="10"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtcontactno" ID="FilteredTextBoxExtender1"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan mandbg" runat="server">Patient Category <span
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlpatientType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpatientType_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlpatientType" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Company</span>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_compnay" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_compnay" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="FPtemplate" runat="server" />
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Occupation</span>
                                            <asp:DropDownList ID="ddlprofession" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Aadhaar No.</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="12"
                                                ID="txt_aadhar"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_aadhar" ID="FilteredTextBoxExtender12"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Visit To</span>
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_visitto" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddl_visitto" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span11" visible="false" class="input-group-addon cusspan" runat="server">Refer By</span>
                                            <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_referby"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnreset_Click" />
                                            <asp:Button ID="btnFPScan" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="FP Scan" OnClick="btnCapture_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Image Style="height: 200px; margin-left: 10px; border: 1px solid #009d90;" ID="FPImage" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <asp:UpdateProgress ID="updateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
