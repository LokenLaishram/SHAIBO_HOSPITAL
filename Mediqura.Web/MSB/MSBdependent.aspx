<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="MSBdependent.aspx.cs" Inherits="Mediqura.Web.MSB.MSBdependent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabDependent" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabemployeedeatils" runat="server" HeaderText="Add Dependent">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div3">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span class="custom-heading cusspan" runat="server">Employee Details</span>
                                    </div>
                                </div>
                                <hr />
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Employee Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox AutoPostBack="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtEmpName" OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="a1" runat="server"
                                            ServiceMethod="GetEmpName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtEmpName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbldob" class="input-group-addon cusspan" runat="server">Age </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtEmpAge"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtEmpAge" ID="FilteredTextBoxExtender7"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Sex </span>
                                        <asp:DropDownList ID="ddl_emp_sex" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbladdress" class="input-group-addon cusspan" runat="server">Address </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtaddress"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblmaritalstatus" class="input-group-addon cusspan" runat="server">Marital Status </span>
                                        <asp:DropDownList ID="ddl_emp_marital" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblrelationship" class="input-group-addon cusspan" runat="server">Designation </span>
                                        <asp:DropDownList ID="ddldesignation" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Department </span>
                                        <asp:DropDownList ID="ddldepartment" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblgender" class="input-group-addon cusspan" runat="server">Grade </span>
                                        <asp:DropDownList ID="ddl_emp_grade" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
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
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="custom-heading cusspan" runat="server">Dependent Details</span>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div2" runat="server">
                                            <asp:Label ID="lblMessage4" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">UHID <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox AutoPostBack="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_UHID" OnTextChanged="txt_dp_UHID_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_dp_UHID" ID="FilteredTextBoxExtender4"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="a2" runat="server"
                                            ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_dp_UHID"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Name </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Address </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_address"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">DOB </span>
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
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Age </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_age"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Sex </span>
                                        <asp:DropDownList ID="ddl_dp_sex" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Relation with Emp <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList AutoPostBack="true" ID="ddl_dp_relation" OnSelectedIndexChanged="ddl_dp_relation_SelectedIndexChanged" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Marital Status </span>
                                        <asp:DropDownList ID="ddl_dp_marital" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Contact No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_contact" MaxLength="10"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_dp_contact" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblstate" class="input-group-addon cusspan" runat="server">State </span>
                                        <asp:DropDownList ID="ddl_dp_state" runat="server" class="form-control input-sm col-sm custextbox cus_ddl">
                                        </asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbldistrict" class="input-group-addon cusspan" runat="server">District </span>

                                        <asp:DropDownList ID="ddl_dp_district" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblpin" class="input-group-addon cusspan" runat="server">Pin 
                                        </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_pin" MaxLength="6"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_dp_pin" ID="FilteredTextBoxExtender3"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Occupation <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_occupation"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Monthly Income <span
                                            style="color: red">*</span>
                                        </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_income"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_dp_income" ID="FilteredTextBoxExtender6"
                                            runat="server" ValidChars="0123456789."
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Apply Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_apply_date"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_dp_apply_date" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dp_apply_date" />

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Issue Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_issue_date"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_dp_issue_date" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dp_issue_date" />

                                    </div>
                                </div>


                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Valid Upto <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dp_valid_upto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_dp_valid_upto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dp_valid_upto" />

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Activation <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_activate" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="0">No</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Condition </span>

                                        <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddl_condition_SelectedIndexChanged" ID="ddl_condition" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Normal</asp:ListItem>
                                            <asp:ListItem Value="1">Special</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Msb Type <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_msb_type" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4" id="Remarks" runat="server">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Remarks </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_remarks"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">

                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" Text="Reset" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabemployeelist" runat="server" CssClass="Tab2" HeaderText="Dependent List">
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
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Employee Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox AutoPostBack="true" runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtEmpNameList" OnTextChanged="txtEmpName_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetEmpName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtEmpNameList"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Dependent Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtDependentName"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Status <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_active" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
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
                                                <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVdependentList" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GVdependentList_RowCommand"
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
                                                                    Emp Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" class="hidden" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblEmpID" runat="server" class="hidden" Text='<%# Eval("EmpID")%>'></asp:Label>
                                                                    <asp:Label ID="lblUHID" runat="server" class="hidden" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                    <asp:Label ID="lblRelation" runat="server" class="hidden" Text='<%# Eval("DependentRelation")%>'></asp:Label>
                                                                    <asp:Label ID="lblOccupation" runat="server" class="hidden" Text='<%# Eval("DependentOccupation")%>'></asp:Label>
                                                                    <asp:Label ID="lblIncome" runat="server" class="hidden" Text='<%# Eval("DependentIncome")%>'></asp:Label>
                                                                    <asp:Label ID="lblApplydate" runat="server" class="hidden" Text='<%# Eval("sApplydate")%>'></asp:Label>
                                                                    <asp:Label ID="lblIssueDate" runat="server" class="hidden" Text='<%# Eval("sIssueDate")%>'></asp:Label>
                                                                    <asp:Label ID="lblValidUpto" runat="server" class="hidden" Text='<%# Eval("sValidUpto")%>'></asp:Label>
                                                                    <asp:Label ID="lblActivation" runat="server" class="hidden" Text='<%# Eval("Activation")%>'></asp:Label>
                                                                    <asp:Label ID="lblCondition" runat="server" class="hidden" Text='<%# Eval("isSpecial")%>'></asp:Label>
                                                                    <asp:Label ID="lblMsbType" runat="server" class="hidden" Text='<%# Eval("MsbType")%>'></asp:Label>
                                                                    <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Dependent
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDependentName" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("DependentName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDependentAddress" runat="server" Text='<%# Eval("DependentAddress")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                                                        </Columns>
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
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
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
