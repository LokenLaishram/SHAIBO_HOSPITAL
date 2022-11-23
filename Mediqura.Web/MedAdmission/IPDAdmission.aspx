<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="IPDAdmission.aspx.cs" Inherits="Mediqura.Web.MedAdmission.IPDAdmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <style>
        .table th {
            position: relative;
        }
    </style>
    <script type="text/javascript">
        function PrintAdmissionProfile() {
            objadmissionno = document.getElementById("<%=txt_AdmissionNo.ClientID %>")
            window.open("../MedAdmission/Reports/ReportViewer.aspx?option=AdmissionProfile&IPNo=" + objadmissionno.value)
        }
        function PrintDuplicateAdmissionProfile(IPNo) {
            window.open("../MedAdmission/Reports/ReportViewer.aspx?option=AdmissionProfile&IPNo=" + IPNo)
        }
        function PrintAdmissionList() {
            objUHID = document.getElementById("<%=txtUHID.ClientID %>")
            objnames = document.getElementById("<%=txt_patientNames.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objdischarstatus = document.getElementById("<%=ddldeischargestatus.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedAdmission/Reports/ReportViewer.aspx?option=AdmissionList&UHID=" + objUHID.value + "&PatientName=" + objnames.value + "&DisStatus=" + objdischarstatus.value + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)
        }


    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineradmission" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabipdadmission" runat="server" HeaderText="IPD Admission">
                    <ContentTemplate>
                        <div class="custab-panel" id="ipdadmissiondetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_uhid" class="input-group-addon cusspan" runat="server" style="color: red">UHID</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_UHID" AutoPostBack="True" OnTextChanged="txt_UHID_TextChanged" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_UHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_UHID" ID="FilteredTextBoxExtender1"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_name" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblco" class="input-group-addon cusspan" runat="server">Admission No.</span>
                                        <asp:HiddenField ID="hdnemrgnumber" runat="server" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_AdmissionNo" ValidChars="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_gender" class="input-group-addon cusspan" runat="server">Gender</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_gender"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_age" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_age"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Contact no.</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_contactnumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblsalutation" class="input-group-addon cusspan" runat="server">Department <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Admission Doctor <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_doctor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Second Doctor </span>
                                        <asp:DropDownList ID="ddl_seconddoctor" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Third Doctor </span>
                                        <asp:DropDownList ID="ddl_thirddoctor" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Referal Doctor </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_referal"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Case<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox" MaxLength="50"
                                            ID="txt_case"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_case" ID="FilteredTextBoxExtender9"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                            FilterMode="ValidChars"
                                            ValidChars=" " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Block </span>
                                        <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Floor</span>
                                        <asp:DropDownList ID="ddl_floor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Ward <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Informed HK <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_housekeepingstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Inforemed Ward <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_wardstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddl_status" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1" style="background-color: Green !important; color: #fff;">Free</asp:ListItem>
                                            <asp:ListItem Value="2" style="background-color: Red  !important; color: #fff;">Occupied</asp:ListItem>
                                            <asp:ListItem Value="3" style="background-color: yellow  !important; color: black;">Ready to discharge</asp:ListItem>
                                            <asp:ListItem Value="4" style="background-color: blue  !important; color: #fff;">Under Maintemnance</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Bed List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 28vh; overflow: auto">
                                                    <asp:GridView ID="GvBedAssign" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowDataBound="GvBedAssign_RowDataBound" AutoGenerateColumns="False"
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
                                                                    Ward
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ward" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Ward") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Room
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("BedID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_room" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Room") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bedno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_charges" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Charges","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bedstatus" Visible="false" Text='<%# Eval("BedStatus") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_status" Style="text-align: left !important; color: silver" runat="server"
                                                                        Text='<%# Eval("BedetailStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Occupied By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_occuopiedby" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("OccupiedBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Check
                                                                <asp:CheckBox Visible="false" ID="chekboxall" runat="server" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekboxselect" runat="server" />
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
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span38" class="input-group-addon cusspan" runat="server">Source <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_source" AutoPostBack="true" OnSelectedIndexChanged="ddl_source_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Walkin</asp:ListItem>
                                            <asp:ListItem Value="2">Referal Hospital</asp:ListItem>
                                            <asp:ListItem Value="3">Referal Doctor</asp:ListItem>
                                            <asp:ListItem Value="4">Referal Employee</asp:ListItem>
                                            <asp:ListItem Value="5">Other Referals</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Referred By <span
                                            style="color: red">*</span> </span>
                                        <asp:DropDownList ID="ddl_referal" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" OnClientClick="javascript: return confirm('Are you sure to save ?');" Text="Save" Class="btn  btn-sm cusbtn"
                                            OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" OnClientClick="return PrintAdmissionProfile();" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnreset" runat="server" Text="Add New" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="ipdadmissionlist" runat="server" HeaderText="Admission List">
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
                                        <span id="Span3" class="input-group-addon cusspan" runat="server" style="color: red">UHID</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" Placeholder="Search all IPNO of UHID  "
                                            ID="txtUHID" AutoPostBack="True" OnTextChanged="txt_UHID_TextChanged" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtUHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtUHID" ID="FilteredTextBoxExtender4"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Patient Details</span>

                                        <%-- Before advance search--%>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="35" Visible="false"
                                            ID="txtautoIPNo" AutoPostBack="True"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtautoIPNo" ID="FilteredTextBoxExtender3"
                                            runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>

                                        <%-- Adding advance search--%>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="50" Placeholder=" Search By Patient Name | UHID | IP Number "
                                            ID="txt_patientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_patientNames" ID="FilteredTextBoxExtender2"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :-/|" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetIPPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Date From </span>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To  </span>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Discharge Status</span>
                                        <asp:DropDownList ID="ddldeischargestatus" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
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
                                <div class="col-sm-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm scusbtn" OnClientClick="return PrintAdmissionList();" Text="Print" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                            <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvadmissionlist" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10" AllowCustomPaging="true"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnPageIndexChanging="GvAdmissionList_PageIndexChanging" OnRowCommand="gvadmissionlist_RowCommand"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)+(gvadmissionlist.PageIndex)*gvadmissionlist.PageSize %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IP No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IPNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Department
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldept" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Department") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Admission Doctor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladmissiondoc" runat="server" Text='<%# Eval("AdmissionDoctor")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Admitted On
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladmittedon" runat="server" Text='<%# Eval("AdmissionDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
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
                                                                Print
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateAdmissionProfile('<%# Eval("IPNo")%>'); return false;">Print</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
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
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
