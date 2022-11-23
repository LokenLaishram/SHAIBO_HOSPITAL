<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ServiceList.aspx.cs" Inherits="Mediqura.Web.MedBills.ServiceList" %>

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
    </script>
    <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabpanel1" runat="server" HeaderText="IP Service Record">
            <ContentTemplate>
                <div class="custab-panel" id="ipdservicerecorddetaildiv">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IPNo <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                            ID="txt_autoipno" AutoPostBack="True" OnTextChanged="txt_autoipno_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_autoipno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Name</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtname"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Category</span>
                                        <asp:HiddenField runat="server" ID="hdnpatcategory" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_category"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:DropDownList ID="ddldepartment" AutoPostBack="true" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_doctor" class="input-group-addon cusspan" runat="server">Doctor</span>
                                        <asp:DropDownList ID="ddldoctor" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddldoctor_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="panel3" DefaultButton="btnadd">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Service Category <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_servicecategory" runat="server" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged" class="form-control input-sm col-sm custextbox" AutoPostBack="true">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Doctor Round</asp:ListItem>
                                                <asp:ListItem Value="2">Other Services </asp:ListItem>
                                                <asp:ListItem Value="3">Other Services With Doctor </asp:ListItem>
                                                <asp:ListItem Value="4">Lab/Radiology Services</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-9">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Services</span>
                                            <asp:HiddenField ID="hdngroupID" runat="server" />
                                            <asp:HiddenField ID="hdnsubgroupID" runat="server" />
                                            <asp:HiddenField ID="hdntescenterID" runat="server" />
                                            <asp:HiddenField ID="hdnurgencyID" runat="server" />
                                            <asp:HiddenField ID="hdnUHID" runat="server" />
                                            <asp:HiddenField ID="hdMsbPc" runat="server" />
                                            <asp:HiddenField ID="hdIsMsbDoctor" runat="server" />
                                            <asp:HiddenField ID="hdngroupnumber" runat="server" />
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtservices" AutoPostBack="True" OnTextChanged="txtservices_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetServices" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtservices"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblservicename" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Quantity</span>

                                            <asp:TextBox runat="server" MaxLength="5" Class="form-control input-sm col-sm custextbox"
                                                ID="txtquantity"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtquantity" ID="FilteredTextBoxExtender1"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Amount</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                ID="txtservicecharge"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtservicecharge" ID="FilteredTextBoxExtender3"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Service Date  <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txt_servicedate"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_servicedate" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_servicedate" />
                                            <asp:TextBox ID="txt_servicetimepicker" runat="server" class="timepicker form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-1">
                                        <div class="form-group input-group">
                                            <asp:Button ID="btnadd" UseSubmitBehavior="false" runat="server" Text="Add" Class="btn  btn-sm scusbtn" OnClick="btnadd_Click" />
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="form-group input-group">
                                            <asp:TextBox runat="server" ReadOnly="true" placeholder="                Inv Number" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                ID="txt_innumber"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Service List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-M">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
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
                                                                            <asp:Label ID="lbl_docID" Visible="false" Text='<%# Eval("DocID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ServiceID") %>'></asp:Label>
                                                                            <asp:Label ID="lblservicecategory" Visible="false" Text='<%# Eval("ServiceCategoryID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblservicetype" Visible="false" Text='<%# Eval("ServiceTypeID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblsubgroupID" Visible="false" Text='<%# Eval("SubGroupID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Charges
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblamount" runat="server" Text='<%# Eval("ServiceCharge","{0:0#.##}")%>'></asp:Label>
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
                                                                            <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetServiceCharge","{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Test Center
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_testcenterID" Visible="false" runat="server" Text='<%# Eval("TestCenterID")%>'></asp:Label>
                                                                            <asp:DropDownList runat="server" ID="ddl_testcenter" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="120px"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Urgency
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUrgency" Visible="false" runat="server" Text='<%# Eval("UrgencyID")%>'></asp:Label>
                                                                            <asp:DropDownList runat="server" ID="ddl_urgency" CssClass="cusDropDown" Font-Size="Small" Height="22px" Width="100px"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Date
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_serviceDate" runat="server" Text='<%# Eval("ServiceDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Remark
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_remarks" runat="server"></asp:TextBox>
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
                                    </div>
                                    <div class="row" style="height: 15px">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
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
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4" style="text-align: right">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnsave_Click" />
                                        <asp:Button ID="btn_print" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="IP Service List">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="paneldepositlist">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" style="color: red" runat="server">IPNo <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txtautoIPNo" AutoPostBack="True" OnTextChanged="txtautoIPNo_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>

                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpatientNames" ReadOnly="true" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender4"
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
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Service Category </span>
                                            <asp:DropDownList ID="ddl_servicetypes" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">Doctor Round</asp:ListItem>
                                                <asp:ListItem Value="2">Other Services </asp:ListItem>
                                                <asp:ListItem Value="3">Other Services With Doctor </asp:ListItem>
                                                <asp:ListItem Value="4">Lab/Radiology Services</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Service Number </span>
                                            <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_servicenumber"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="GetServiceNumber" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_servicenumber"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Service Name </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_searchsearivces"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Inv Number </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_searchinvnumber"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                ServiceMethod="GetInvNumber" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_searchinvnumber"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
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
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Date To 
                                            </span>
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
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvipservicerecord" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="gvipservicerecord_PageIndexChanging" AllowPaging="false" PageSize="10"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="gvipservicerecord_RowDataBound" OnRowCommand="gvipservicerecord_RowCommand"
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
                                                                    IP No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_isdeviceinitiated" Visible="false" runat="server" Text='<%# Eval("IsDeviceInitiated") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_recordID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_serialID" Visible="false" runat="server" Text='<%# Eval("SerialID") %>'></asp:Label>
                                                                    <asp:Label ID="lblIPNo" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IPNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    SV Number
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_servicenumber" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ServiceNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Inv Number
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinvnumber" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("InvNumber") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_groupnumber" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("GroupNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Doctor
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Doctor") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Service Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblservices" runat="server" Text='<%# Eval("ServiceName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Charge
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcharges" runat="server" Text='<%# Eval("ServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Net Charge</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("NetServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy | hh:mm:ss tt}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                                                    <asp:LinkButton ID="lbl_print" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                     Print
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
                                    <div class="col-md-4"></div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
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
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </ContentTemplate>


        </asp:TabPanel>
    </asp:TabContainer>


</asp:Content>
