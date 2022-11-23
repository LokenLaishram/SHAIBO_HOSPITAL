<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="LabDeviceInitiation.aspx.cs" Inherits="Mediqura.Web.MedLab.LabDeviceInitiation" %>

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
        function PrintWorksheet(InvNu, UHID, Subgroup) {
            window.open("Report/ReportViewer.aspx?option=worksheet&Inv=" + InvNu + "&UHID=" + UHID + "&SubgrpID=" + Subgroup)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerSampleCollection" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabSampleCollection" runat="server" CssClass="Tab1" HeaderText="Lab Device Initiation">

                    <ContentTemplate>
                        <div class="custab-panel" id="panelSample_collection">

                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_patienttype" AutoPostBack="true" OnSelectedIndexChanged="ddl_patienttype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbluhid" class="input-group-addon cusspan" runat="server">Inv. No </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="20"
                                            ID="txt_invno" AutoPostBack="True" OnTextChanged="txt_invno_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetInv" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_invno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" AutoPostBack="True" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_name"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetLabPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_name"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txt_datefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txt_datefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_datefrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Date To <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txt_dateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="txt_dateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_dateto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Consultant</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_referal">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_status">
                                            <asp:ListItem Value="6">--Select--</asp:ListItem>
                                            <asp:ListItem Value="0">Not Recieved</asp:ListItem>
                                            <asp:ListItem Value="1">Recieved</asp:ListItem>
                                            <asp:ListItem Value="2">Initiated</asp:ListItem>
                                            <asp:ListItem Value="3">Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" Text="Search" />
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
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvlabdeciceinitiation" runat="server" CssClass="table-hover grid_table result-table" OnRowCommand="gvlabdeciceinitiation_RowCommand" OnRowUpdating="gvlabdeciceinitiation_OnRowUpdating" OnRowDataBound="gvlabdeciceinitiation_RowDataBound"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    DataKeyNames="ID" Width="100%" HorizontalAlign="Center">
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
                                                                Inv.Number
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_urgencyid" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("UrgencyID") %>' Width="250px"></asp:Label>
                                                                <asp:Label ID="lbl_invnumber" ForeColor="#ffccff" runat="server" Text='<%# Eval("Investigationumber") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sub Group
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lbl_subgroupID" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("SubGroupID") %>' Width="250px"></asp:Label>
                                                                <asp:Label ID="lblsubgroup" runat="server" Text='<%# Eval("Subgroup") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_testname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("TestName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Patient Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_patientName" runat="server" Text='<%# Eval("PatientName") %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Machine Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_machine" runat="server" Text='<%# Eval("Device") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_devicestatus" Visible="false" runat="server" Text='<%# Eval("IsDeviceInitiated") %>'></asp:Label>
                                                                <asp:DropDownList runat="server" Width="100px" Class="form-control input-sm col-sm custextbox"
                                                                    ID="ddl_devicestatus">
                                                                    <asp:ListItem Value="0">Not Recieved</asp:ListItem>
                                                                    <asp:ListItem Value="1">Recieve</asp:ListItem>
                                                                    <asp:ListItem Value="2">Initiate</asp:ListItem>
                                                                    <asp:ListItem Value="3">Complete</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%# Eval("InvSampleCollectedOn","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                Rcv.Time
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_recvstatus" Visible="false" runat="server" Text='<%# Eval("IsSampleRecieve") %>'></asp:Label>
                                                                <asp:Label ID="lbl_recvtime" runat="server" Text='<%# Eval("recievedtime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                Init.Time
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_initiatedtime" runat="server" Text='<%# Eval("initiationtime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="true">
                                                            <HeaderTemplate>
                                                                Compl.Time
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_completiontime" runat="server" Text='<%# Eval("completiontime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:ButtonField CommandName="Update" Visible="true" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                                                      <%--  <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Work Sheet
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintWorksheet('<%# Eval("Investigationumber")%>','<%# Eval("UHID")%>','<%# Eval("SubGroupID")%>'); return false;">Print</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>
                                                          <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Work Sheet
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                   <asp:Label ID="lbl_Inv" Visible="false" runat="server" Text='<%# Eval("Investigationumber") %>'></asp:Label>
                                                                   <asp:Label ID="lbl_uhid" Visible="false" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                                                   <asp:Label ID="lbl_subgroup" Visible="false" runat="server" Text='<%# Eval("SubGroupID") %>'></asp:Label>      
                                                                    <asp:LinkButton ID="lnkview" Visible="true" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"><span style="color:red">Print</span></asp:LinkButton>
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
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

