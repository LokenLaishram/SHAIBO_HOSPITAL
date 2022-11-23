<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmergencyRegistration.aspx.cs" Inherits="Mediqura.Web.MedEmergency.EmergencyRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintEmergencyProfile() {
            objEmrgno = document.getElementById("<%=txt_EmergencyNo.ClientID %>")
            window.open("../MedEmergency/Reports/ReportViewer.aspx?option=EmergencyProfile&EmergencyNo=" + objEmrgno.value)
        }
        function PrintDuplicateEmergencyProfile(EmrgNo) {
            window.open("../MedEmergency/Reports/ReportViewer.aspx?option=EmergencyProfile&EmergencyNo=" + EmrgNo)
        }
        function EmergencyList() {
            objEmrgNo = "";
            objnames = document.getElementById("<%=txt_patientNames.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objemrgDoc = document.getElementById("<%=ddl_emrgDocList.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            objdischargestatus = document.getElementById("<%=ddldeischargestatus.ClientID %>")
           
            window.open("../MedEmergency/Reports/ReportViewer.aspx?option=EmergencyList&EMRGNo=" + objEmrgNo.value + "&PatientName=" + objnames.value + "&EmrgDoc=" + objemrgDoc.value + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&DischargeStatus=" + objdischargestatus.value)
        }
        function PrintConsultantSheet() {
            objbillno = document.getElementById("<%=hdnbillnumber.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + objbillno.value)
        }
        function PrintduplicateConsultantSheet(billno) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + billno)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineradmission" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabEmrgAdmission" runat="server" HeaderText="Emergency Admission">
                    <ContentTemplate>
                        <div class="custab-panel" id="Emrgadmissiondetaildiv">
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
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
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_UHID" ID="FilteredTextBoxExtender7"
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
                                                <span id="lblco" class="input-group-addon cusspan" runat="server">Emergency No.</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_EmergencyNo"></asp:TextBox>
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
                                                <asp:HiddenField ID="hdnopnumber" runat="server" />
                                                <asp:HiddenField ID="hdnbillnumber" runat="server" />
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_age"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span2" class="input-group-addon cusspan" runat="server">Contact No.</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_contactnumber"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_address"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span1" class="input-group-addon cusspan" runat="server">Department <span
                                                    style="color: red">*</span></span>
                                                <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="lblamount" class="input-group-addon cusspan" runat="server">Emerg. Doctor <span
                                                    style="color: red">*</span></span>
                                                <asp:DropDownList ID="ddldoctor" runat="server" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group input-group">
                                                <span id="Span7" class="input-group-addon cusspan" runat="server">Case <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox" MaxLength="50"
                                                    ID="txt_case" OnTextChanged="txt_case_TextChanged"></asp:TextBox>
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
                                                <span id="Span10" class="input-group-addon cusspan" runat="server">Block</span>
                                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_block" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span4" class="input-group-addon cusspan" runat="server">Floor</span>
                                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_floor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_floor" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span5" class="input-group-addon cusspan" runat="server">Ward</span>
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_ward" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span14" class="input-group-addon cusspan" runat="server">Informed HK <span
                                                    style="color: red">*</span></span>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_housekeepingstatus" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_housekeepingstatus" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span16" class="input-group-addon cusspan" runat="server">Inforemed Ward <span
                                                    style="color: red">*</span></span>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_wardstatus" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_wardstatus" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <span id="Span15" class="input-group-addon cusspan" runat="server">Status</span>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddl_status" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="1" style="background-color: Green !important; color: #fff;">Free</asp:ListItem>
                                                            <asp:ListItem Value="2" style="background-color: Red  !important; color: #fff;">Occupied</asp:ListItem>
                                                            <asp:ListItem Value="3" style="background-color: yellow  !important; color: black;">Ready to discharge</asp:ListItem>
                                                            <asp:ListItem Value="4" style="background-color: blue  !important; color: #fff;">Under Maintemnance</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_status" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row cusrow pad-top ">
                                        <label class="gridview-label">Bed List</label>
                                        <div class="col-sm-12">
                                            <div>
                                                <div class="pbody">
                                                    <div class="gridview-container-M">
                                                        <div class="grid" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
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
                                                                            <asp:Label ID="lblbed" Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_bedno" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("BedNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Bed Charges
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_charges" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("Charges","{0:0#.##}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                                <asp:Button ID="btnsave" runat="server" OnClientClick="javascript: return confirm('Are you sure to save ?');" Text="Save" Class="btn  btn-sm scusbtn"
                                                    OnClick="btnsave_Click" />
                                                <asp:Button ID="btn_cs" runat="server" Text="Print(CS)" OnClientClick="PrintConsultantSheet()" Class="btn  btn-sm scusbtn" />
                                                <asp:Button ID="btnprint" runat="server" Text="Print(P)" OnClientClick="return PrintEmergencyProfile();" Class="btn  btn-sm scusbtn" />
                                                <asp:Button ID="btnreset" runat="server" Text="Add New" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="ipdadmissionlist" runat="server" HeaderText="Emergency List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div3" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">                               
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox" MaxLength="35"
                                            ID="txtEmrgNo" AutoPostBack="True" OnTextChanged="txtEmrgNo_TextChanged"></asp:TextBox>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="100"
                                            ID="txt_patientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_patientNames" ID="FilteredTextBoxExtender2"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" ./-:" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetEmgPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Emergency Doctor <span
                                            style="color: red"></span></span>
                                        <asp:DropDownList ID="ddl_emrgDocList" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
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
                            </div>
                            <div class="row">
                                <div class="col-sm-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return EmergencyList();" Text="Print" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <%--<div class="col-lg-8"></div>--%>
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
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvEmrglist" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnPageIndexChanging="gvEmrglist_PageIndexChanging" OnRowCommand="gvEmrglist_RowCommand"
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
                                                                Emrg. No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmrgID" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>
                                                                <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("EmrgNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                UHID
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUHID" Style="text-align: left !important;" runat="server"
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
                                                                Address
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("address")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doctor
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_admissiondoctor" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("AdmissionDoctor") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Admitted On
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_billnumber" Visible="false" runat="server" Text='<%# Eval("BillNo")%>'></asp:Label>
                                                                <asp:Label ID="lbladmittedon" runat="server" Text='<%# Eval("AdmissionDate","{0:dd-MM-yyyy | hh:mm:ss tt}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
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
                                                        <%--  <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Edit-header">Edit</span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                AP
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateEmergencyProfile('<%# Eval("EmrgNo")%>'); return false;">Print</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CS
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintduplicateConsultantSheet('<%# Eval("BillNo")%>'); return false;">Print</a>
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
