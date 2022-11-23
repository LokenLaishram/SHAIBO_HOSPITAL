<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OTScheduler.aspx.cs" Inherits="Mediqura.Web.MedOT.OTScheduler" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function PrintOTScheduleList() {
            objPatientName = document.getElementById("<%=txtpatient.ClientID %>")
            objConsultant = document.getElementById("<%=ddl_consultant.ClientID %>")
            objOTtheater = document.getElementById("<%=ddlottheater.ClientID %>")
            objdatefrom = document.getElementById("<%=txtotfrom.ClientID %>")
            objdateto = document.getElementById("<%=txtotto.ClientID %>")
            objOTStatus = document.getElementById("<%=ddlOTstatus.ClientID %>")

            window.open("../MedOT/Report/ReportViewer.aspx?option=OTScheduleList&PatientName=" + objPatientName.value + "&Consultant=" + objConsultant.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&OtTheater=" + objOTtheater.value + "&OTStatus=" + objOTStatus.value)
        }

    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabOTschedule" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpatienthistory" runat="server" CssClass="Tab2" HeaderText="OT Schedule">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepartmentmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txtUHID_TextChanged"
                                            ID="txtpatientNames"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnuhid" />
                                        <asp:HiddenField runat="server" ID="hdnemrgnumber" />
                                        <asp:HiddenField runat="server" ID="hdnipnumber" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblwardbedno" class="input-group-addon cusspan" runat="server">Ward/Bed No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtwardbedno"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltheather" class="input-group-addon cusspan" runat="server">Operation Theater</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="true" OnTextChanged="txtotdate_TextChanged"
                                            ID="ddl_ottheater">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">OT Date </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true" OnTextChanged="txtotdate_TextChanged"
                                            ID="txtotdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtotdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtotdate" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server" style="height: 10px !important;">Start Time</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="margin-left: 13px; width: 60px;"
                                                    ID="ddlstarthour">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3; width: 60px;"
                                                    ID="ddlstartminute">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3; margin-left: -10px; width: 60px;"
                                                    ID="ddlstartmeridiem">
                                                    <asp:ListItem Value="1" Text="AM"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PM"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server" style="height: 10px !important;">End Time</span>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="margin-left: 10px; width: 60px;"
                                                    ID="ddlendhour">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3; width: 60px;"
                                                    ID="ddlendminute">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group input-group">
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3; margin-left: -10px; width: 60px;"
                                                    ID="ddlendmeridiem">
                                                    <asp:ListItem Value="1" Text="AM"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PM"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblsurgeon" class="input-group-addon cusspan" runat="server">Surgeon</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtsurgeon"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetDoctorName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtSurgeon"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblanaesthetist" class="input-group-addon cusspan" runat="server">Anaesthetist</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlanaesthetist">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_pac" class="input-group-addon cusspan" runat="server">PAC </span>
                                        <asp:HiddenField runat="server" ID="hdndoctorID" />
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" ID="ddlpac">
                                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Done"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Not Done"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">OT Status</span>
                                        <asp:DropDownList ID="ddlOTStatuss" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbloperationname" class="input-group-addon cusspan" runat="server">Operation Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" TextMode="MultiLine" Rows="5" Height="50px"
                                            ID="txtoperationname"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblremark" class="input-group-addon cusspan" runat="server">Remark</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" TextMode="MultiLine" Rows="5" Height="50px"
                                            ID="txtremark"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnaddnew" runat="server" Text="Add New" Class="btn  btn-sm scusbtn" OnClick="btnresets_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsgs3" runat="server" style="padding-left: 10px;">
                                            <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
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
                                                    <asp:GridView ID="gvtodayotdetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowDataBound="Gvtodayotdetails_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    OT Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubHeading1" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IsSubHeading") %>'></asp:Label>
                                                                    <asp:Label ID="lblOTNo1" Visible="false" runat="server" Text='<%# Eval("OTNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblotdate1" Visible="true" runat="server" Text='<%# Eval("OpernDate","{0:dd-dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Start Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstarttime1" Visible="true" runat="server" Text='<%# Eval("OTStartTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    End Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEndtime1" Visible="true" runat="server" Text='<%# Eval("OTEndTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Patient Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID1" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ID1" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblpatientname1" Visible="true" Text='<%# Eval("PatientName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Ward/Bed No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWardbedno1" Visible="true" runat="server" Text='<%# Eval("WardBedName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Operation Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloperationName1" Visible="true" Text='<%# Eval("OperationName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Surgeon
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldoctorname1" Visible="true" Text='<%# Eval("Surgeon") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    OT Theatre
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltheatrename1" Visible="true" runat="server" Text='<%# Eval("Theatre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    PAC
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpac1" Visible="true" runat="server" Text='<%# Eval("PAC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Anaesthetist
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblanaesthtist1" Visible="true" runat="server" Text='<%# Eval("AnaesthtistName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblotstatus1" Visible="true" runat="server" Text='<%# Eval("OTStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Remark
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblremarks1" Visible="true" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
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
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabOTscheduleList" runat="server" CssClass="Tab2" HeaderText="OT Scheduled List">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepartmentmaster2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblpatient" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpatient"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatient" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetPatientNames" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatient"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblconsultant" class="input-group-addon cusspan" runat="server">Consultant </span>
                                        <asp:HiddenField runat="server" ID="hdnconsultant" />
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" ID="ddl_consultant"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Operation Theater</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddlottheater">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblotfrom" class="input-group-addon cusspan" runat="server">Date From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                            ID="txtotfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtotfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtotfrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblotto" class="input-group-addon cusspan" runat="server">Date To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                            ID="txtotto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtotto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtotto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">OT Status</span>
                                        <asp:DropDownList ID="ddlOTstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_search" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" Text="Search" OnClick="btnsearchs_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClientClick="return PrintOTScheduleList();" />
                                        <asp:Button ID="btn_clear" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnclear_Click" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg" runat="server">
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
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvSchedulelist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="20" OnRowCommand="GvSchedulelist_RowCommand" OnRowDataBound="GvSchedulelist_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>

                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    OT Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubHeading" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IsSubHeading") %>'></asp:Label>
                                                                    <asp:Label ID="lblOTNo" Visible="false" runat="server" Text='<%# Eval("OTNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblotdate" Visible="true" runat="server" Text='<%# Eval("OpernDate","{0:dd-dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Start Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstarttime" Visible="true" runat="server" Text='<%# Eval("OTStartTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    End Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEndtime" Visible="true" runat="server" Text='<%# Eval("OTEndTime") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Patient Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblpatientname" Visible="true" Text='<%# Eval("PatientName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Ward/Bed No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWardbedno" Visible="true" runat="server" Text='<%# Eval("WardBedName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Operation Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbloperationName" Visible="true" Text='<%# Eval("OperationName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Surgeon
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldoctorname" Visible="true" Text='<%# Eval("Surgeon") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    OT Theatre
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltheatrename" Visible="true" runat="server" Text='<%# Eval("Theatre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    PAC
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpac" Visible="true" runat="server" Text='<%# Eval("PAC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Anaesthetist
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblanaesthtist" Visible="true" runat="server" Text='<%# Eval("AnaesthtistName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblotstatus" Visible="true" runat="server" Text='<%# Eval("OTStatus") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Remark
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblremarks" Visible="true" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                                    <asp:TextBox ID="txtremarks" runat="server" Width="120px" Visible="false" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" Visible="false" HeaderStyle-Font-Size="Smaller">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Cancel</span>
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
