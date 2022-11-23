<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OTRegistration.aspx.cs" Inherits="Mediqura.Web.MedOT.OTRegistration" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function pageLoad() {
            var options = {
                now: "0:01", //hh:mm 24 hour format only, defaults to current time
                twentyFour: false,  //Display 24 hour format, defaults to false
                upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
                downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS
                hoverState: 'hover-state', //The hover state class to use, for custom CSS
                title: 'Timepicker', //The Wickedpicker's title,
                showSeconds: false, //Whether or not to show seconds,
                timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
                secondsInterval: 1, //Change interval for seconds, defaults to 1,
                minutesInterval: 1, //Change interval for minutes, defaults to 1
                beforeShow: null, //A function to be called before the Wickedpicker is shown
                afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
                show: null, //A function to be called when the Wickedpicker is shown
                clearable: false, //Make the picker's input clearable (has clickable "x")
            };
            var options1 = {
                now: "23:59", //hh:mm 24 hour format only, defaults to current time
                twentyFour: false,  //Display 24 hour format, defaults to false
                upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
                downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
                close: 'wickedpicker__close', //The close class selector to use, for custom CSS
                hoverState: 'hover-state', //The hover state class to use, for custom CSS
                title: 'Timepicker', //The Wickedpicker's title,
                showSeconds: false, //Whether or not to show seconds,
                timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
                secondsInterval: 1, //Change interval for seconds, defaults to 1,
                minutesInterval: 1, //Change interval for minutes, defaults to 1
                beforeShow: null, //A function to be called before the Wickedpicker is shown
                afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
                show: null, //A function to be called when the Wickedpicker is shown
                clearable: false, //Make the picker's input clearable (has clickable "x")
            };
            $('.timepicker').wickedpicker(options);
            $('.timepicker1').wickedpicker(options1);


        }

        function PrintDuplicateOT_Registration(OTNo) {
            window.open("../MedOT/Report/ReportViewer.aspx?option=OT_RegnProfile&OTNo=" + OTNo)
        }
        function PrintOT_Registration() {
            objotno = document.getElementById("<%=txt_OTno.ClientID %>")
            window.open("../MedOT/Report/ReportViewer.aspx?option=OT_RegnProfile&OTNo=" + objotno.value)
        }
        function PrintOT_regnList() {
            objIPNo = document.getElementById("<%=txt_Ipno.ClientID %>")
            objnames = document.getElementById("<%=txtpatientNames.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objOtpassNo = document.getElementById("<%=txt_Otpassnumber.ClientID %>")
            objOttheater = document.getElementById("<%=dd_ottheaters.ClientID %>")
            objOtStatus = document.getElementById("<%=ddl_otstatus.ClientID %>")
            objOtDoc = document.getElementById("<%=ddl_doctor.ClientID %>")
            objOtactive = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedOT/Report/ReportViewer.aspx?option=OTList&IPNo=" + objIPNo.value + "&PatientName=" + objnames.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Otpass=" + objOtpassNo.value + "&OtTheater=" + objOttheater.value + "&OtStatus=" + objOtStatus.value + "&OtDoc=" + objOtDoc.value + "&Otactive=" + objOtactive.value)
        }
    </script>
    <style>
        .table th {
            position: relative;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" CssClass="Tab" runat="server" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabOTregnDetails" runat="server" HeaderText="OT Registration">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panelPatientList">

                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" style="color: red" runat="server">OT Pass <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_otpass" AutoPostBack="True" OnTextChanged="txt_otpass_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                ServiceMethod="GetOTpassNumber" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_otpass"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" style="color: red" runat="server">IPNo  <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtautoIPNo" AutoPostBack="True" OnTextChanged="txtautoIPNo_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblName" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                                ID="txt_patientNames"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" Placeholder="           OT number" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                                ID="txt_OTno"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="lblAddress" class="input-group-addon cusspan" runat="server">Address <span
                                                style="color: red"></span></span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                                ID="txt_address"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Operation Theater <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="ddl_ottheater">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Admission DT </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_admissiondatetime"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <label class="gridview-label">Role Detail</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <label class="gridview-label">Procedure Detail</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <label class="gridview-label">Anaesthesia Detail</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_asstSurg" class="input-group-addon cusspan" runat="server">OT Roles <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3; top: 0px; left: 0px;"
                                                ID="ddl_otrole" OnSelectedIndexChanged="ddl_otrole_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Start Date & time <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                ID="txt_Otdate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_Otdate" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_Otdate" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Consent Status</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_consentstatus"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblnurse" class="input-group-addon cusspan" runat="server">Employee <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged"
                                                ID="ddl_employee">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="lblcase" class="input-group-addon cusspan" runat="server">Procedure <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_case_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                                ID="ddl_case">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Anaesthesia type <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_anestesiatype_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                                ID="ddl_anestesiatype">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-4">
                                        <div class="gridview-container-ML">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:GridView ID="GvOTregn" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnPageIndexChanging="GvOTregn_PageIndexChanging" AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center" OnRowDataBound="GvOTregn_RowDataBound" OnRowCommand="GvOTregn_RowCommand">
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
                                                                Employee Detail
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_employeetype" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTemployeeTypeID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_employeeID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTemployeeID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_employee" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTemployee") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Role
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblroleID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTroleID") %>'></asp:Label>
                                                                <asp:Label ID="lblrole" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTrole") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                M Srgn.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_main" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IsMainSurgeon") %>'></asp:Label>
                                                                <asp:CheckBox ID="chk_main" Style="text-align: left !important;" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
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
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="gridview-container-ML">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:GridView ID="Gv_procedure" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center" OnRowCommand="Gv_procedure_RowCommand">
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
                                                                Procedue
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_procedureID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("CaseID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_case" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("CaseName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="gridview-container-ML">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:GridView ID="Gv_Anaesthesia" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center" OnRowCommand="Gv_Anaesthesia_RowCommand">
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
                                                                Aneasthesia
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_anaesthesiaID" Visible="false" runat="server" Text='<%# Eval("AnaesthesiaType")%>'></asp:Label>
                                                                <asp:Label ID="lbl_anaesthesia" Text='<%# Eval("AnaesthesiaName") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="height: 5px">
                                </div>
                                <div class="row">
                                    <div class="col-sm-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" Class="btn  btn-sm cusbtn"
                                                OnClick="btnsave_Click" />
                                            <asp:Button ID="btnprint" runat="server" Visible="false" Text="Print" OnClientClick="return PrintOT_Registration();" Class="btn  btn-sm cusbtn" OnClick="btnprint_Click" />
                                            <asp:Button ID="btnreset" runat="server" Text="Add New" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="OT registration List">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepositlist">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">

                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">IPNo  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Ipno" AutoPostBack="True" OnTextChanged="txt_Ipno_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetOTIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_Ipno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpatientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender7"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                            FilterMode="ValidChars"
                                            ValidChars=" " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Doctor </span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged"
                                            ID="ddl_doctor">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">OT passnumber</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Otpassnumber" AutoPostBack="True"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetOTpassNumber" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_Otpassnumber"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Operation Theater </span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged"
                                            ID="dd_ottheaters">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">OT Status</span>
                                        <asp:DropDownList ID="ddl_otstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                            ID="txtdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdatefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
                                            style="color: red">*</span> </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Status</span>
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
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintOT_regnList();" Text="Print" OnClick="btnprints_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div2" runat="server">
                                            <asp:Label ID="lblresult2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                       <asp:GridView ID="Gv_Otregistrationlist" runat="server" CssClass="table-hover grid_table result-table grid"
                                                        EmptyDataText="No record found..."  DataKeyNames="OTRegnID" OnPageIndexChanging="Gv_Otregistrationlist_PageIndexChanging" AllowPaging="true" PageSize="10" AllowCustomPaging="true" OnRowDataBound="Gv_Otregistrationlist_RowDataBound" OnRowUpdating="Gv_Otregistrationlist_OnRowUpdating" AutoGenerateColumns="False" OnRowCommand="Gv_Otregistrationlist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center"> <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <%# (Container.DataItemIndex+1)+(Gv_Otregistrationlist.PageIndex)*Gv_Otregistrationlist.PageSize %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OT Pass.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_otpass" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTpassnumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OT No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblotno" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IPNo.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIPno" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IPNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Patient Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpatientname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("PatientName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OT
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblot" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OT Date  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladt" runat="server" Text='<%# Eval("OperationDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Start Time 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltime" runat="server" Text='<%# Eval("OperationTime")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                End Time 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_endtime" runat="server" Text='<%# Eval("OperationEndTime")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OT Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible="false" Text='<%# Eval("OT_status")%>' ID="lbl_otstatusID"></asp:Label>
                                                                <asp:Label runat="server" ID="lbl_status"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                   Doctor
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsurgeon" runat="server" Text='<%# Eval("OTDoc")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" Width="100px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
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
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_update" CommandName="Update" runat="server" Text="Update" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings Mode="NumericFirstLast"  PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click1" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
