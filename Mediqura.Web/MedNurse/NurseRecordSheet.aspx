<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="NurseRecordSheet.aspx.cs" Inherits="Mediqura.Web.MedNurse.NurseRecordSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <link href="../Scripts/jquery.timepicker.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.timepicker.min.js"></script>
    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)
            var options = { 'timeFormat': 'h:i:s A', 'step': 15 }
            var option = { 'timeFormat': 'H:i:s', 'step': 1 }
            $("#<%=txttime.ClientID %>").timepicker(options);
        }

        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()
            if (Page.get_isInAsyncPostBack()) {

                args.set_cancel(true)
            }
        }

        function PrintNurseRecordSheet() {
            objPatientName = document.getElementById("<%=txtpatientNames.ClientID %>")
            objdatefrom = document.getElementById("<%=txtfrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")

            window.open("../MedNurse/Reports/ReportViewer.aspx?option=NurseRecordSheet&PatientName=" + objPatientName.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)
        }
    </script>
    <style type="text/css">
        .ui-timepicker-wrapper {
            width: 10.5em;
        }
    </style>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabNurseRecordSheet" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanelNurseRecordSheet" runat="server" CssClass="Tab2" HeaderText="Nurse Record Sheet">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelMedicationChart">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">IP Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtpatientNames" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnuhid" />
                                        <asp:HiddenField runat="server" ID="hdnemrgnumber" />
                                        <asp:HiddenField runat="server" ID="hdnipnumber" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/|-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetIPPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblwardbedno" class="input-group-addon cusspan" runat="server">Ward / Bed No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtwardbedno"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblage" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtage"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblgender" class="input-group-addon cusspan" runat="server">Sex</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtgender"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldoa" class="input-group-addon cusspan" runat="server">D.O.A </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdoa"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblweight" class="input-group-addon cusspan" runat="server">Weight</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtweight"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblIPNO" class="input-group-addon cusspan" runat="server">IPNO</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtIPNO"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblstartdate" class="input-group-addon cusspan" runat="server">Date </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtEntryDate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtEntryDate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtEntryDate" />

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltime" class="input-group-addon cusspan" runat="server">Time</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="10"
                                            ID="txttime"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txttime" ID="FilteredTextBoxExtender2"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890 :AMP" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltemp" class="input-group-addon cusspan" runat="server">Temp, 'C</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="3"
                                            ID="txttempe"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txttempe" ID="FilteredTextBoxExtender1"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890.C" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblpulse" class="input-group-addon cusspan" runat="server">Pulse/min</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="10"
                                            ID="txtpulse"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpulse" ID="FilteredTextBoxExtender3"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890./" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblbp" class="input-group-addon cusspan" runat="server">B.P min Hg</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="10"
                                            ID="txtbp"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtbp" ID="FilteredTextBoxExtender4"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890./" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblrrmin" class="input-group-addon cusspan" runat="server">R.R/min</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="10"
                                            ID="txtrrmin"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtbp" ID="FilteredTextBoxExtender6"
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890./" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblSpO2" class="input-group-addon cusspan" runat="server">SpO2</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtSpO2"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtbp" ID="FilteredTextBoxExtender7"
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            runat="server"
                                            FilterMode="ValidChars"
                                            ValidChars="1234567890./" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblgcs" class="input-group-addon" style="width: 258px; margin: 0px;" runat="server">G.C.S</span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblGCSEyeOpening" class="input-group-addon cusspan" runat="server">Eye Opening (E)</span>
                                      
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlGCSEyeOpening">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblGCSEVerbal" class="input-group-addon cusspan" runat="server">Verbal Response (V)</span>
                                        
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlGCSEVerbal">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblmotorResponse" class="input-group-addon cusspan" runat="server">Motor Response (M)</span>
                                      
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlGCSMotorResponse">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblpupil" class="input-group-addon" style="width: 258px; margin: 0px;" runat="server">Pupil Reaction</span>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblrtpupil" class="input-group-addon cusspan" runat="server">Right Pupil Reaction</span>
                                     
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlrightpupilreaction">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblLeftPupil" class="input-group-addon cusspan" runat="server">Left Pupil Reaction</span>
                                       
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlleftpupilreaction">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblinvasiveline" class="input-group-addon cusspan" runat="server">Other Invasive Line</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtinvasiveline" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldrugname" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtfrom" Placeholder="Use in Search only"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Start Date </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtto" Placeholder="Use in Search only"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />

                                    </div>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" Text="Add" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btnsearchs_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintNurseRecordSheet();" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsgs3" runat="server" style="padding-left: 10px;">
                                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-vs">
                                                <div class="grid" style="float: left; width: 100%; height: 28vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvNurseRecorddetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowCommand="gvNurseRecorddetails_RowCommand"
                                                        Width="150%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Entry Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblEDate" runat="server" Text='<%# Eval("EntryDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    EntryTime
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEntryTime" Visible="true" runat="server" Text='<%# Eval("EntryTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Start Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTemperature" Visible="true" runat="server" Text='<%# Eval("Temperature") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Pulse
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPulse" Visible="true" Text='<%# Eval("Pulse") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    RR
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRR" Visible="true" runat="server" Text='<%# Eval("RR") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    SpO2
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSpO2" Visible="true" runat="server" Text='<%# Eval("SpO2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Eye Opening(E)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGCSEyeOpening" Visible="true" runat="server" Text='<%# Eval("GCSEyeOpening") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Verbal Response(V)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblverbalresponse" Visible="true" runat="server" Text='<%# Eval("GCSEVerbal") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Motor Response (M) 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmotorresponse" Visible="true" runat="server" Text='<%# Eval("GCSMotorResponse") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Right Pupil
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRightPupil" Visible="true" runat="server" Text='<%# Eval("RightPupil") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Right Pupil
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLeftPupil" Visible="true" runat="server" Text='<%# Eval("LeftPupil") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Invasive Line
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvasiveLine" Visible="true" runat="server" Text='<%# Eval("InvasiveLine") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedby" Visible="true" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Added Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdeddate" Visible="true" runat="server" Text='<%# Eval("AddedDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
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
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
