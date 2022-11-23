<%@ page title="OPD Lab Billing" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="OpdBilling.aspx.cs" inherits="Mediqura.Web.MedBills.OpdBilling" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        function updatePanelPostback() {
            __doPostBack("<%=upHiddenPostback.ClientID%>", "");
        }

        function Printreciept() {
            objbillno = document.getElementById("<%=txtbillNo.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDBillReceipt&BillNo=" + objbillno.value)
        }
        function PrintConsultantSheet() {
            objbillno = document.getElementById("<%=txtbillNo.ClientID %>")
            objDept = document.getElementById("<%=hdnDepartmentID.ClientID%>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + objbillno.value + "&Dept=" + objDept.value)
        }
        function PrintRegdCard() {
            objUHID = document.getElementById("<%=hdnUHID.ClientID %>")
            window.open("../Reports/ReportViewer.aspx?option=RegdCard&UHID=" + objUHID.value)
        }
        function print() {

            $.print("#barcode");
        }
        function printDuplicate() {

            $.print("#barcodeList");
        }
        function PrintDuplicatebills(billno) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDBillReceipt&BillNo=" + billno)
        }
        function PrintduplicateConsultantSheet(billno, DepartmentID) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + billno + "&Dept=" + DepartmentID)
        }
        function PrintCollectionList() {

            objnames = document.getElementById("<%=txtpatientNames.ClientID %>")
            objpaymode = document.getElementById("<%=ddlpaymentmodes.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objfromtimepicker = document.getElementById("<%=txttimepickerfrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objtotimepicker = document.getElementById("<%=txttimepickerto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            objcollectedby = document.getElementById("<%=ddlcollectedby.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDcollection&PatientName=" + objnames.value + "&Status=" + objstatus.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Paymode=" + objpaymode.value + "&Fromtimepicker=" + objfromtimepicker.value + "&Totimepicker=" + objtotimepicker.value + "&Collectedby=" + objcollectedby.value)
        }
        var submit = 0;
        function CheckIsRepeat() {
            if (++submit > 1) {
                alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                return false; z
            }
        }
    </script>

    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabopdbillingdetails" runat="server" HeaderText="OP Billing">
                    <ContentTemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="lbluhid" class="input-group-addon cusspan mandbg" runat="server">UHID<span
                                            style="color: red"> *</span></span>
                                        <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtUHID" AutoPostBack="True" OnTextChanged="txtUHID_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetAutoUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="0" TargetControlID="txtUHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Patient Category</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_patientcategory"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdnnumberdays" />
                                        <asp:HiddenField runat="server" ID="hdMsbPc" />
                                        <asp:HiddenField runat="server" ID="hdPatCat" />
                                        <asp:HiddenField runat="server" ID="hdisMsbDoctor" />
                                        <asp:HiddenField runat="server" ID="hdnUHID" />
                                        <asp:HiddenField runat="server" ID="hdnDepartmentID" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Company</span>
                                        <asp:HiddenField runat="server" ID="hdnprintID" />
                                        <asp:HiddenField runat="server" ID="hdnlastvisitdoctorID" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_tpacompany"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan mandbg" runat="server">Source <span
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_referby" Width="262px" placeholder="Refer by" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                            ServiceMethod="Getautoreferals" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_referby"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span27" class="input-group-addon cusspan mandbg" runat="server">Service Type  <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddlservicetype" OnSelectedIndexChanged="ddlservicetype_SelectedIndexChanged" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblamount" class="input-group-addon cusspan" runat="server">Consultant</span>
                                        <asp:DropDownList ID="ddldoctor" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddldoctor_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblco" class="input-group-addon cusspan" runat="server">Bill No.</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbillNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Services</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtservices" AutoPostBack="True" OnTextChanged="txtservices_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetServices" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtservices"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:Label ID="lblservicename" runat="server" Visible="False"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Quantity</span>
                                        <asp:TextBox MaxLength="5" runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtquantity"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtquantity" ID="FilteredTextBoxExtender1"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Amount</span>
                                        <asp:HiddenField ID="hdncharges" runat="server" />
                                        <asp:HiddenField ID="hdnsubgroupID" runat="server" />
                                        <asp:TextBox runat="server" MaxLength="10" ReadOnly="True" OnTextChanged="txtservicecharge_TextChanged" AutoPostBack="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtservicecharge"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtservicecharge" ID="FilteredTextBoxExtender3"
                                            runat="server" ValidChars="0123456789."
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Remarks</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtremarks"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Class="btn  btn-sm scusbtn" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Service List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                    <asp:GridView ID="gvopservicelist" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="gvopservicelist_RowCommand" AutoGenerateColumns="False" OnRowDataBound="gvopservicelist_RowDataBound"
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
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_Defaulrservice" Visible="false" Text='<%# Eval("IsDefaultService") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                                    <asp:Label ID="lblremarks" Visible="false" Text='<%# Eval("Remarks") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Service Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblservicetypeID" Visible="false" runat="server" Text='<%# Eval("ServiceTypeID")%>'></asp:Label>
                                                                    <asp:Label ID="lbldeprtmentID" Visible="false" runat="server" Text='<%# Eval("DeptID")%>'></asp:Label>
                                                                    <asp:Label ID="lbldoctorID" Visible="false" runat="server" Text='<%# Eval("DocID")%>'></asp:Label>
                                                                    <asp:Label ID="lbldoctortype" Visible="false" runat="server" Text='<%# Eval("DoctorTypeID")%>'></asp:Label>
                                                                    <asp:Label ID="lblservicetype" runat="server" Text='<%# Eval("ServiceType")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_subgroupID" Visible="false" runat="server" Text='<%# Eval("SubGroupID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("ServiceCharge", "{0:0#.##}")%>'></asp:Label>
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
                                                                    <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddl_discount_type" runat="server" OnSelectedIndexChanged="ddl_discount_type_SelectedIndexChanged" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                                        <asp:ListItem Value="0">Fix</asp:ListItem>
                                                                        <asp:ListItem Value="1">PC</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Value
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                        ID="txt_dis_value" OnTextChanged="txt_dis_value_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_dis_value" ID="FilteredTextBoxExtender2"
                                                                        runat="server" ValidChars="012345678.9"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_discount_amt" Style="text-align: left !important;" runat="server"></asp:Label>
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
                                                    <asp:GridView ID="GVDiscountApprovalList" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center">
                                                         <Columns>
                                                        <asp:TemplateField>
                                                                   
                                                            <headertemplate>
                                                                    SlNo.
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <headertemplate>
                                                                    Services
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <asp:Label ID="lbldoctorID" Visible="false" runat="server" Text='<%# Eval("DocID")%>'></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblparticulars" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <headertemplate>
                                                                    Charges
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <asp:Label ID="lblservicetype" Visible="false" runat="server" Text='<%# Eval("ServiceTypeID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_subgroupID" Visible="false" runat="server" Text='<%# Eval("SubGroupID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_charges" runat="server" Text='<%# Eval("ServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <headertemplate>
                                                                    Quantity
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <headertemplate>
                                                                    Net Charges
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("NetServiceCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <headertemplate>
                                                                    Discount
                                                                </headertemplate>
                                                            <itemtemplate>
                                                                    <asp:Label Text='<%# Eval("DisAmount", "{0:0#.##}")%>' ID="lbl_discount_amt" Style="text-align: left !important;" runat="server"></asp:Label>
                                                                </itemtemplate>
                                                            <itemstyle horizontalalign="Left" width="1%" />
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
                            <div class="row" style="height: 15px">
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Bill(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Balance(A/C)(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbalanceinac"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Adjusted(A/C)(₹)</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtadjustedamount"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                        <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                        <asp:HiddenField runat="server" ID="hdnbankID" />
                                        <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbank"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
                                        <asp:Label ID="lbl_accno" Visible="False" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_chequenumber"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtinvoicenumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Discount(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdiscount" MaxLength="10" AutoPostBack="True" OnTextChanged="txtdiscount_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtdiscount" ID="FilteredTextBoxExtender4"
                                            runat="server" ValidChars="012345678.9"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:LinkButton runat="server" ID="btnlinkdiscount" OnClick="btnlinkdiscount_Click"><i class="fa fa-plus"></i> Discount</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Paid Amount(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpaidamount" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Remarks</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdiscremoarks"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnDisSave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Visible="false" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnDisSave_Click" />
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="false" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" Class="btn  btn-sm cusbtn"
                                            OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="RCV" OnClientClick="return Printreciept();" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btn_cs" runat="server" Text="OPD" OnClientClick="return PrintConsultantSheet();" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btn_redgcard" runat="server" Visible="false" Text="CARD" OnClientClick="return PrintRegdCard();" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btn_token" Visible="false" runat="server" Text="TOKEN" OnClick="btn_token_Click" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                        <asp:Button ID="btnModel" Style="display: none" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">
                                    <asp:ModalPopupExtender ID="MDBarcode" runat="server" TargetControlID="btnModel" PopupControlID="popupwindow"
                                        BackgroundCssClass="modalBackground" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupwindow" Style="display: none;">
                                        <div style="width: 100%" class="row">
                                            <div style="background-color: #ffffff; padding-top: 20px; padding-right: 500px;" class="col-sm-12">
                                                <div id="barcode">
                                                    <table style="color: black;">
                                                        <asp:Literal runat="server" ID="ltBarcode"></asp:Literal>
                                                    </table>
                                                </div>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnClose" runat="server" class="btn  btn-sm cusbtn" Text="Close" OnClick="btnClose_Click" /></td>
                                                        <td>
                                                            <button class="btn  btn-sm cusbtn" onclick="print();">Print</button></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">

                                    <asp:ModalPopupExtender ID="MDTPA" runat="server" TargetControlID="btnSample" PopupControlID="popupApproval"
                                        BackgroundCssClass="modalBackground" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupApproval" Style="display: none;" DefaultButton="btnAddTpa">
                                        <div style="width: 100%; padding-right: 250px;" class="row">
                                            <div style="background-color: #ffffff; width: 100%; padding: 20px" class="col-sm-8 col-sm-offset-2">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span37" class="input-group-addon cusspan" runat="server">Category </span>
                                                            <asp:DropDownList class="form-control input-sm col-sm custextbox" ID="ddl_tpa_patient_cat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_tpa_patient_cat_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span32" class="input-group-addon cusspan" runat="server">Sub Category </span>
                                                            <asp:DropDownList class="form-control input-sm col-sm custextbox" ID="ddl_tpa_patient_sub_cat" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Discount Amount </span>
                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                ID="txt_tpa_amount"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender TargetControlID="txt_tpa_amount" ID="FilteredTextBoxExtender6"
                                                                runat="server" ValidChars="0123456789."
                                                                Enabled="True">
                                                            </asp:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group input-group">
                                                            <asp:Button ID="btnAddTpa" runat="server" Class="btn  btn-sm cusbtn" Text="Add" OnClick="btnAddTpa_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="fixeddiv">
                                                            <div class="row fixeddiv" id="divPopMsg" runat="server">
                                                                <asp:Label ID="lblPopUpmsg" runat="server" Height="13px"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="grid" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress4" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloadingPopUp1" class="text-center loading" runat="server">
                                                                <asp:Image ID="DIVloadingPopUpImage1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVTPAList" runat="server" CssClass="table-hover grid_table result-table"
                                                        AllowPaging="false"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GVTPAList_RowCommand"
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
                                                                    Patient Category
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCat" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatCat") %>'></asp:Label>
                                                                    <asp:Label ID="lblCatID" Visible="false" runat="server"
                                                                        Text='<%# Eval("PatientCatID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Company
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubCat" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("subCat") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubCatID" Visible="false" runat="server"
                                                                        Text='<%# Eval("SubCatID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Discount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDisAmount" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("DiscountAmount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
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
                                                <br />
                                                <div class="row">
                                                    <div class="col-lg-6"></div>
                                                    <div class="col-sm-6">
                                                        <div>
                                                            <asp:Button ID="btnTpaClose" Style="margin-left: 8px" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm cusbtn exprt" Text="Close" />
                                                            <asp:Button ID="btnSample" Style="display: none" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="OP Collection">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:TextBox runat="server" Visible="false" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                        ID="txtautoUHID" AutoPostBack="True"></asp:TextBox>

                                    <div class="col-sm-8">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpatientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                                runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=" |:1234567890" Enabled="True">
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
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                            <asp:DropDownList ID="ddlpaymentmodes" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan mandbg" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                            <asp:TextBox ID="txttimepickerfrom" runat="server" class="timepicker form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan mandbg" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                            <asp:TextBox ID="txttimepickerto" runat="server" class="timepicker1 form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
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
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">Collected By</span>
                                            <asp:DropDownList ID="ddlcollectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintCollectionList();" Text="Print" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                            <asp:Button ID="btnmodelList" Style="display: none" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row cusrow ">
                                    <div class="col-sm-12">

                                        <asp:ModalPopupExtender ID="ModelListBarcode" runat="server" TargetControlID="btnmodelList" PopupControlID="PanelListWindow"
                                            BackgroundCssClass="modalBackground" Enabled="True">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel runat="server" ID="PanelListWindow" Style="display: none;">

                                            <div style="width: 2200%" class="row">
                                                <div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">
                                                    <div id="barcodeList">
                                                        <table style="color: black;">
                                                            <asp:Literal runat="server" ID="LitBarcodelist"></asp:Literal>

                                                        </table>

                                                    </div>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnClosedList" runat="server" class="btn  btn-sm cusbtn" Text="Close" OnClick="btnClose_Click" /></td>
                                                            <td>
                                                                <button class="btn  btn-sm cusbtn" onclick="printDuplicate();">Print</button></td>
                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
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
                                                            <div id="DIV3" class="text-center loading" runat="server">
                                                                <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvopcollection" runat="server" CssClass="table-hover grid_table result-table" AllowCustomPaging="true"
                                                        EmptyDataText="No record found..." OnRowDataBound="gvopcollection_RowDataBound" OnPageIndexChanging="gvdepositlist_PageIndexChanging" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" OnRowCommand="gvdepositlist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                       <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)+(gvopcollection.PageIndex)*gvopcollection.PageSize %>
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
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UHID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbluhid" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                                    <asp:Label ID="lblIsVerified" Visible="false" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IsVerified") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Doctor
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladmissiondoc" runat="server" Text='<%# Eval("DocName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    TB
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("TotalBillAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Ad
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaajustedamount" runat="server" Text='<%# Eval("AdjustedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Dis
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldiscountedamount" runat="server" Text='<%# Eval("DiscountedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    P
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="1%" />
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
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy | hh:mm:ss tt}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" CssClass="form-control" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Del</span>
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
                                                                    RCV
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("BillNo")%>'); return false;"><i class="fa fa-print cus-delete-color"></i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    OPD
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintduplicateConsultantSheet('<%# Eval("BillNo")%>','<%# Eval("DepartmentID")%>'); return false;"><i class="fa fa-print cus-delete-color"></i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CARD
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbl_cardprint" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to print duplicate  registration card ?');">
                                                                        <i class="fa fa-print cus-delete-color"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    TOKEN
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkToken" runat="server" CommandName="Token" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                        <i class="fa fa-print cus-delete-color"></i>
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
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Total Bill(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalbill"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Total Adjusted(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txtajusted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Total Discount(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotaldiscounted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalpaid"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
