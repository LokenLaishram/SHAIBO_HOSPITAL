<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="RadreportMaker.aspx.cs" Inherits="Mediqura.Web.MedRadTemplate.RadreportMaker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxRichEdit.v17.2, Version=17.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRichEdit" TagPrefix="dx" %>

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
            <asp:TabContainer ID="tabradreportmaker" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tab1" runat="server" CssClass="Tab1" HeaderText="Test List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div3">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div2" runat="server">
                                    <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                        <asp:DropDownList runat="server" ID="ddl_patienttype" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" Style="z-index: 3"
                                            ID="txt_patientnames"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetLabPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientnames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">IPNo</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="True"
                                            ID="txt_ipnumber"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_ipnumber"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">INV Number </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_invnumber"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetInv" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_invnumber"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Test Name </span>
                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txt_testnames_TextChanged" Class="form-control input-sm col-sm "
                                            ID="txt_testnames"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                            ServiceMethod="GetTestNames" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_testnames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Consultant</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_referal">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Date From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtdate_from"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtdate_from" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate_from" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Date To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtdate_to"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtdate_to" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate_to" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_status">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">No Entry</asp:ListItem>
                                            <asp:ListItem Value="2">Entry Done</asp:ListItem>
                                            <asp:ListItem Value="3">Verified</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" Text="Search" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div6" runat="server">
                                            <asp:Label ID="lbl_result" runat="server" Height="13px"></asp:Label>
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
                                                <asp:GridView ID="gv_Radlabtestlist" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..."
                                                    DataKeyNames="ID" OnRowCommand="gv_Radlabtestlist_RowCommand" OnRowDataBound="gv_Radlabtestlist_RowDataBound" OnRowUpdating="gv_Radlabtestlist_OnRowUpdating"
                                                    AutoGenerateColumns="False"
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
                                                                Inv.No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_urgencyid" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("UrgencyID") %>' Width="250px"></asp:Label>
                                                                <asp:Label ID="lvl_inv" ForeColor="#ffccff" runat="server" Text='<%# Eval("Investigationumber")%>'></asp:Label>
                                                                <asp:Label ID="lblID" ForeColor="#ffccff" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_template" Visible="false" runat="server" Text='<%# Eval("TemplateTypeID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_UHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                <asp:LinkButton ID="lbl_test" Font-Underline="true" ForeColor="Red" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("TestName") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                                <asp:Label ID="lbl_sex" Visible="false" runat="server" Text='<%# Eval("GenderID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Receive Time
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_reciestatus" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IsMLTreceived") %>'></asp:Label>
                                                                <asp:Label ID="lbl_recvTime" Style="text-align: left !important;" runat="server" Text='<%# Eval("MLTrecievingDatetime","{0:dd-MM-yyyy:hh:mm:ss tt}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:ButtonField CommandName="Update" HeaderText="Update RT" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_statusID" Visible="false" runat="server" Text='<%# Eval("StatusID")%>'></asp:Label>
                                                                <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tab2" runat="server" CssClass="Tab1" HeaderText="Report Maker">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelSample_collection">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10">
                                    <dx:ASPxRichEdit ID="Richteditor" runat="server" WorkDirectory="~\App_Data\WorkDirectory"></dx:ASPxRichEdit>
                                </div>
                                <div class="col-sm-2" style="margin-left: -36px;">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:DropDownList runat="server" ID="ddl_templatetype" AutoPostBack="true" OnSelectedIndexChanged="ddl_gender_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0"> --Select Template--</asp:ListItem>
                                                <asp:ListItem Value="1"> Template - I </asp:ListItem>
                                                <asp:ListItem Value="2"> Template - II </asp:ListItem>
                                                <asp:ListItem Value="3"> Template - III </asp:ListItem>
                                                <asp:ListItem Value="4"> Template - IV </asp:ListItem>
                                                <asp:ListItem Value="5"> Template - V </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lbl_invnumber" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_uhids" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_testID" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_genderID" Visible="false" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12" runat="server" style="margin-top: 4px">
                                            <div class="form-group input-group">
                                                <span id="lbl_case" class="input-group-addon" runat="server" style="min-width: 60px !important;">Verified By</span>
                                                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_verifiedby_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox"
                                                    ID="ddl_verifiedby">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 4px">
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_save" runat="server" CssClass="btn btn-sm scusbtn" OnClick="btnsave_Click" Text="Save" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_print" Visible="false" CssClass="btn btn-sm scusbtn" OnClick="btnprint_Click" runat="server" Text="Print" />
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button ID="btn_reset" CssClass="btn btn-sm scusbtn" OnClick="btnresets_Click" runat="server" Text="Reset" />
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
