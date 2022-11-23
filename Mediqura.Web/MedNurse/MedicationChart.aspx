<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="MedicationChart.aspx.cs" Inherits="Mediqura.Web.MedNurse.MedicationChart" %>


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
            $("#<%=txtmeditime.ClientID %>").timepicker(options);
       }

       function OnInitializeRequest(sender, args) {

           var postBackElement = args.get_postBackElement()
           if (Page.get_isInAsyncPostBack()) {

               args.set_cancel(true)
           }
       }
    </script>
    <script type="text/javascript">
        function PrintIPPatientDrug() {
            objIPPatientName = document.getElementById("<%=txtpatientNames.ClientID %>")           

            window.open("../MedNurse/Reports/ReportViewer.aspx?option=DrugMedicationList&PatientName=" + objIPPatientName.value)
        }
    </script>
    <style type="text/css">
        .ui-timepicker-wrapper {
            width: 10.5em;
        }
    </style>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabMedicationChart" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanelMedicationChart" runat="server" CssClass="Tab2" HeaderText="Medication Chart">
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
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txtpatientNames_TextChanged"
                                            ID="txtpatientNames"></asp:TextBox>
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
                                        <span id="lblweight" class="input-group-addon cusspan" runat="server">Weight</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtweight"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbldoa" class="input-group-addon cusspan" runat="server">D.O.A </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdoa"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbldrugname" class="input-group-addon cusspan" runat="server">Drug Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdrugname"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdndrugID" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txtdrugname" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/|%-()" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetDrugName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtdrugname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblstartdate" class="input-group-addon cusspan" runat="server">Start Date </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                            ID="txtstartdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtstartdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtstartdate" />

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblfrequency" class="input-group-addon cusspan" runat="server">Frequency</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtfrequency"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtfrequency" ID="FilteredTextBoxExtender2"
                                            runat="server" FilterType="Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars="" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                    <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbldoctorname" class="input-group-addon cusspan" runat="server">Advice Doctor</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtDoctorName"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdndocID" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txtDoctorName" ID="FilteredTextBoxExtender3"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/|-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetDoctorName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtDoctorName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblroute" class="input-group-addon cusspan" runat="server">Route</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlroute">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                    ID="txtfrom"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                    TargetControlID="txtfrom" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />
                                <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                    ID="txtto"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                    TargetControlID="txtto" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />

                             </div>
                            <div class="row">
                                 <div class="col-sm-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" Text="Add" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btnsearchs_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintIPPatientDrug();" />
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
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvmedichartdetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowCommand="gvmedichart_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
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
                                                                    MDNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblmcno" Visible="false" runat="server" Text='<%# Eval("MedCNo") %>'></asp:Label>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" Text='<%# Eval("MedCNo") %>' CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" Font-Underline="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Drug Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldrugID" Visible="false" runat="server" Text='<%# Eval("DrugID") %>'></asp:Label>
                                                                    <asp:Label ID="lbldrugname" Visible="true" runat="server" Text='<%# Eval("DrugName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Start Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstartdate" Visible="true" runat="server" Text='<%# Eval("StartDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Frequency
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfrequency" Visible="true" Text='<%# Eval("Frequency") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Route Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRouteName" Visible="true" runat="server" Text='<%# Eval("RouteName") %>'></asp:Label>
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
                <asp:TabPanel ID="tabmedicationdetails" runat="server" CssClass="Tab2" HeaderText="Medication Details">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelmedicationdetails">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbluhid" class="input-group-addon cusspan" runat="server">UHID</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtuhid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblipno" class="input-group-addon cusspan" runat="server">IPNO</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtipno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblIPpatient2" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtIPpatient2"></asp:TextBox>
                                        <asp:HiddenField ID="hdnMediCNo" runat="server" />
                                        <asp:HiddenField ID="hdnUHID2" runat="server" />
                                        <asp:HiddenField ID="hdnIPNO2" runat="server" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblwardbed" class="input-group-addon cusspan" runat="server">Ward / Bed No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtwardbedNo2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbldrug" class="input-group-addon cusspan" runat="server">Drug Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdrug"></asp:TextBox>
                                        <asp:HiddenField ID="hdnDrugID2" runat="server" />
                                    </div>
                                </div>


                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblstartdate2" class="input-group-addon cusspan" runat="server">Start Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtstartdate2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblfrequency2" class="input-group-addon cusspan" runat="server">Frequency</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtfrequency2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblroute2" class="input-group-addon cusspan" runat="server">Route</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtroute2"></asp:TextBox>
                                        <asp:HiddenField ID="hdnRouteID" runat="server" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblmonth" class="input-group-addon cusspan" runat="server">Month</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlmonth" OnSelectedIndexChanged="ddlmonth_OnSelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblmedicationdate" class="input-group-addon cusspan" runat="server">Medication Date </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtmedicationdate" Placeholder="Enter today date"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtmedicationdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtmedicationdate" />

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblmeditime" class="input-group-addon cusspan" runat="server">Medication Time</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            MaxLength="8" ID="txtmeditime"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-5">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_save" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" OnClick="btn_save_OnClick" />
                                        <asp:Button ID="btn_AddNew" runat="server" Text="Add New" Class="btn  btn-sm scusbtn" OnClick="btn_AddNew_Click" />
                                        <asp:Button ID="btnsearch2" runat="server" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btnsearchs2_Click" />
                                        <asp:Button ID="btn_Reset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btn_Reset_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg" runat="server" style="padding-left: 10px;">
                                            <asp:Label ID="lblresults" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 55vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvDrugMedicationlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="20" OnRowCommand="GvDrugMedicationlist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
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
                                                                    Drug Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMediID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblDrugName" runat="server" Text='<%# Eval("DrugName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Medication Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblmediNo" Visible="false" runat="server" Text='<%# Eval("MedicationNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblotdate" Visible="true" runat="server" Text='<%# Eval("MedicationDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Medication Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltime" runat="server" Text='<%# Eval("MedicationTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" />
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
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDeletes" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                        <i class="fa fa-trash-o cus-delete-color"></i>                                        
                                                                    </asp:LinkButton>
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
                        </>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
