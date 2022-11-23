<%@ Page Title="Add Employee" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmployeeDetail.aspx.cs" EnableEventValidation="false" Inherits="Mediqura.Web.MedHR.EmployeeDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
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

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaemployee" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabemployeedeatils" runat="server" HeaderText="Employee Details">
                    <ContentTemplate>
                        <asp:Panel ID="panel1" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panelemployee">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblsalutation" class="input-group-addon cusspan" runat="server">Salutation <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlsalute" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblname" class="input-group-addon cusspan" runat="server">Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtname"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span28" class="input-group-addon cusspan" runat="server">Alias Name </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_alias"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span32" class="input-group-addon cusspan" runat="server">Aadhaar No. </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="12"
                                                ID="txt_aadhaarno"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_aadhaarno" ID="FilteredTextBoxExtender4"
                                                runat="server" FilterType="Custom,Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars=" " Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbluhid" class="input-group-addon cusspan" style="color: red" runat="server">Employee No <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtempno"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">C/O <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_guardianname"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Staff Category <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_staffcategory" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span21" class="input-group-addon cusspan" runat="server">Blood Group <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_bloodgrp" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Work Experience <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_wrk"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbldob" class="input-group-addon cusspan" runat="server">DOB <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdob"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdob" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdob" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">DOJ <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdoj"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdoj" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdoj" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblrelationship" class="input-group-addon cusspan" runat="server">Designation <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddldesignation" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Department <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddldepartment" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblgender" class="input-group-addon cusspan" runat="server">Emp. Type <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlemployeetype" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblreligion" class="input-group-addon cusspan" runat="server">Qualification<span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtqulaification"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblmaritalstatus" class="input-group-addon cusspan" runat="server">Marital Status <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlmarital" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlmarital_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Spouse Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="True"
                                                ID="txt_spousename"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblidmark" class="input-group-addon cusspan" runat="server">ID Mark</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtidmark"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblnationality" class="input-group-addon cusspan" runat="server">Nationality </span>
                                            <asp:DropDownList ID="ddlnationality" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbladdress" class="input-group-addon cusspan" runat="server">Present Address <span style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtaddress"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblcountry" class="input-group-addon cusspan" runat="server">Country <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlcountry" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblstate" class="input-group-addon cusspan" runat="server">State <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlstate" AutoPostBack="true" runat="server" class="form-control input-sm col-sm custextbox cus_ddl" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbldistrict" class="input-group-addon cusspan" runat="server">District <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddldistrict" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblpin" class="input-group-addon cusspan" runat="server">Pin <span
                                                style="color: red">*</span>
                                            </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpin" MaxLength="6"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpin" ID="FilteredTextBoxExtender3"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <asp:CheckBox ID="chekbox" runat="server" AutoPostBack="True" OnCheckedChanged="chekbox_CheckedChanged" />
                                            <label style="color: #808080">same as present address</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Permanent Address <span style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_address1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Country <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_country1" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">State <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_state1" runat="server" class="form-control input-sm col-sm custextbox cus_ddl">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">District <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_district1" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span20" class="input-group-addon cusspan" runat="server">Pin <span
                                                style="color: red">*</span>
                                            </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtpin1" MaxLength="6"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpin1" ID="FilteredTextBoxExtender7"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblemail" class="input-group-addon cusspan" runat="server">Email</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtemail"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblcontactno" class="input-group-addon cusspan" runat="server">Contact No. <span
                                                style="color: red">*</span></span>
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
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Emerg. No <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtemergencyno" MaxLength="10"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtemergencyno" ID="FilteredTextBoxExtender2"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Religion</span>
                                            <asp:DropDownList ID="ddlreligion" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span15" class="input-group-addon cusspan" runat="server">Caste <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_caste" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Grade <span
                                                style="color: red">*</span> </span>
                                            <asp:DropDownList ID="ddl_emp_grade" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span23" class="input-group-addon cusspan" runat="server">Photo Upload</span>
                                            <asp:FileUpload ID="FileUploadImage" runat="server"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span24" class="input-group-addon cusspan" runat="server">Signature Upload</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span29" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddl_status" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span30" class="input-group-addon cusspan" style="color: red" runat="server">Remarks</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                                ID="txtremarks" AutoPostBack="True"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span31" class="input-group-addon cusspan" runat="server">Exclude MSB</span>
                                            <asp:DropDownList ID="ddl_exclude_msb" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="FPtemplate" runat="server" />
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="javascript: return confirm('Are you sure to save ?');" OnClick="btnsave_Click" Text="Save" />
                                            <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                            <asp:Button ID="btnFPScan" runat="server" Visible="false" Class="btn  btn-sm cusbtn" Text="FP Scan" OnClick="btnFPScan_Click" />
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
                <asp:TabPanel ID="tabemployeelist" runat="server" CssClass="Tab2" HeaderText="Employee List">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="panelemployeelist">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" style="color: red" runat="server">Employee No</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txtempnos" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtContactsSearch_AutoCompleteExtender" runat="server"
                                                ServiceMethod="GetEmpNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtempnos"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtemployeename" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtemployeename"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Contact No.</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtcontactnos" AutoPostBack="True"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetContactno" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtcontactnos"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtcontactnos" ID="FilteredTextBoxExtender5"
                                                runat="server" ValidChars="0123456789"
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">Department  </span>
                                            <asp:DropDownList ID="ddl_departments" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span25" class="input-group-addon cusspan" runat="server">Staff Category  </span>
                                            <asp:DropDownList ID="ddl_staffcategorys" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span27" class="input-group-addon cusspan" runat="server">Employee Type  </span>
                                            <asp:DropDownList ID="ddl_employeetypes" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
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
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Date To</span>
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
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Status</span>
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
                                <div class="row cusrow ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvemployeeList" runat="server" ShowFooter="true" AllowPaging="true" PageSize="10" CssClass="table-hover grid_table result-table" AllowCustomPaging="true"
                                                        EmptyDataText="No record found..." OnPageIndexChanging="GvemployeeList_PageIndexChanging" AutoGenerateColumns="False" OnRowCommand="GvemployeeList_RowCommand" 
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)+(GvemployeeList.PageIndex)*GvemployeeList.PageSize %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Photo">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="Image1" Height="100px" runat="server" ImageUrl='<%# "ImageHandler.ashx?ImID="+ Eval("EmployeeID") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Emp.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblempno" runat="server" Text='<%# Eval("EmployeeNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" class="hidden" Text='<%# Eval("EmployeeID")%>'></asp:Label>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcontact" runat="server" Text='<%# Eval("MobileNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Department
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldept" runat="server" Text='<%# Eval("Department")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("CurrentAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName1")%>'></asp:Label>
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
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />

                                                        <HeaderStyle BackColor="#D8EBF5" />
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
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
