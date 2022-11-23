<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmployeeDiscount.aspx.cs" Inherits="Mediqura.Web.MedHR.EmployeeDiscount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel1" runat="server" HeaderText="Employee Discount">
                    <ContentTemplate>
                        <div class="custab-panel" id="ipdservicerecorddetaildiv">

                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Department <span
                                            style="color: red">*</span></span>
                                        <asp:HiddenField runat="server" ID="hdndepartmentID" />
                                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" ID="ddldepartment"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">Employee Name <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                            ID="txt_empname" AutoPostBack="True" OnTextChanged="txt_empname_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetEmpdetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_empname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>

                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div2" runat="server">
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
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvemployeedetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowCommand="gvemployeedetails_RowCommand"
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
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_name" Style="text-align: left !important;" runat="server" MaxLength="30"
                                                                        Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IPD(Discount %)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtdiscountipd" Style="text-align: left !important;" runat="server" MaxLength="50"
                                                                        Text='<%# Eval("DiscountIPD") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtdiscountipd" ValidChars="0123456789. " />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    OPD(Discount %)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtdiscountopd" MaxLength="10" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("DiscountOPD") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txtdiscountopd" ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="0123456789."
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    OPD Lab(Discount %)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtdiscountopdlab" runat="server" Text='<%# Eval("DiscountOPDLab") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txtdiscountopdlab" ID="FilteredTextBoxExtender2"
                                                                        runat="server" ValidChars="0123456789."
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    View Dependent
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" CommandName="Select" ID="btnlnkpopdependent" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">     <i class="fa fa-plus"></i> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="4%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
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

                                                    <asp:Panel ID="pnlGridViewDetails" runat="server" Width="2px" Height="200px" Style="display: none; margin-left: -500px"
                                                        CssClass="pnlBackGround">
                                                        <div class="fixeddiv">
                                                            <div class="row fixeddiv" id="div1" runat="server">
                                                                <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="Gridview1_RowCommand"
                                                            Width="268px" HorizontalAlign="Left">
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
                                                                        Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="hdnID" Visible="false" runat="server" Text='<%# Eval("DependentID") %>'></asp:Label>
                                                                        <asp:TextBox ID="txt_name" Style="text-align: left !important;" runat="server" MaxLength="30" Width="230px"
                                                                            Text='<%# Eval("DependentName") %>'></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_name" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " />

                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Relation
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddl_relationship" runat="server">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Gender
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddl_gender" runat="server">
                                                                        </asp:DropDownList>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        DOB
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_DOB" MaxLength="10" Style="text-align: left !important;" runat="server" Width="80px"
                                                                            Text='<%# Eval("DOB") %>'></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender5" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                            runat="server" TargetControlID="txt_DOB" />
                                                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_DOB" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Age
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtage" runat="server" Text='<%# Eval("Age") %>' Width="30px"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender TargetControlID="txtage" ID="FilteredTextBoxExtender4"
                                                                            runat="server" ValidChars="0123456789."
                                                                            Enabled="True">
                                                                        </asp:FilteredTextBoxExtender>

                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Appl Dt. 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_app" MaxLength="10" Style="text-align: left !important;" runat="server" Width="80px"
                                                                            Text='<%# Eval("AppDt") %>'></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender4" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                            runat="server" TargetControlID="txt_app" />
                                                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_app" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2 %" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Issue Dt. 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_IssueDt" MaxLength="10" Style="text-align: left !important;" runat="server" Width="80px"
                                                                            Text='<%# Eval("IssueDt") %>'></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                            runat="server" TargetControlID="txt_IssueDt" />
                                                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_IssueDt" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2 %" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Valid Dt. 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_val" MaxLength="10" Style="text-align: left !important;" runat="server" Width="80px"
                                                                            Text='<%# Eval("ValidDt") %>'></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                            runat="server" TargetControlID="txt_val" />
                                                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_val" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2 %" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Surr Dt. 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txt_sur" MaxLength="10" Style="text-align: left !important;" runat="server" Width="80px"
                                                                            Text='<%# Eval("SurDt") %>'></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                            runat="server" TargetControlID="txt_sur" />
                                                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_sur" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="2 %" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        IPD
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdiscountipd" Style="text-align: left !important;" runat="server" MaxLength="50" Width="60px"
                                                                            Text='<%# Eval("DiscountIPD") %>'></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtdiscountipd" ValidChars="0123456789. " />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        OPD
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdiscountopd" MaxLength="10" Style="text-align: left !important;" runat="server" Width="60px"
                                                                            Text='<%# Eval("DiscountOPD") %>'></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender TargetControlID="txtdiscountopd" ID="FilteredTextBoxExtender6"
                                                                            runat="server" ValidChars="0123456789."
                                                                            Enabled="True">
                                                                        </asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        INV
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtdiscountopdlab" runat="server" Text='<%# Eval("DiscountOPDLab") %>' Width="60px"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender TargetControlID="txtdiscountopdlab" ID="FilteredTextBoxExtender7"
                                                                            runat="server" ValidChars="0123456789."
                                                                            Enabled="True">
                                                                        </asp:FilteredTextBoxExtender>

                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Add
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="lnkAdd" runat="server" CommandName="Add" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" Text="Add Row"></asp:Button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:Button ID="btnclose" runat="server" Text="X" OnClick="btnclose_Click" Style="color: red;" />
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
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <div class="form-group input-group cuspanelbtngrp">
                                                                    <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </asp:Panel>


                                                    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                                                    <asp:ModalPopupExtender ID="GridViewDetails" runat="server" TargetControlID="btnDummy"
                                                        PopupControlID="pnlGridViewDetails" BackgroundCssClass="modalBackground" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group pull-right">
                                        <asp:Button ID="btn_save" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm cusbtn"
                                            OnClick="btn_save_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
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
