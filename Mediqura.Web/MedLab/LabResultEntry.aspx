<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabResultEntry.aspx.cs" Inherits="Mediqura.Web.MedLab.LabResultEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="CR" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function clickEnter(obj, event) {
            var keyCode;
            if (event.keyCode > 0) {
                keyCode = event.keyCode;
            }
            else if (event.which > 0) {
                keyCode = event.which;
            }
            else {
                keycode = event.charCode;
            }
            if (keyCode == 13) {
                document.getElementById(obj).focus();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerSampleCollection" runat="server" CssClass="Tab" ActiveTabIndex="0"
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
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="PatientName_OnTextChanged" AutoPostBack="true" Style="z-index: 3"
                                            ID="txt_patientnames"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetLabPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientnames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                            </div>
                            <asp:Panel ID="hidepanel" runat="server" Visible="false">
                                <div class="row">
                                    <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="True"
                                        ID="txt_ipnumber"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                        ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_ipnumber"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>

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
                                            <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm "
                                                ID="txt_testnames"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="GetTestNames" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_testnames"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
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
                                        <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="ddl_status">
                                            <asp:ListItem Value="1">Pending</asp:ListItem>
                                            <asp:ListItem Value="3">Approved</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Centre</span>
                                        <asp:DropDownList ID="ddl_centre" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Show Entries </span>
                                        <asp:DropDownList ID="ddl_show" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_show_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="1000">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsearch_Click" Text="Search" />
                                        <asp:Button ID="btn_refresh" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btn_refresh_Click" Text="Reset" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div6" runat="server">
                                            <asp:Label ID="lbl_result" runat="server" Height="13px"></asp:Label>
                                            <asp:Label ID="lbl_totalrecords" Visible="false" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="pbody">
                                        <div class="grid" style="float: left; width: 100%; min-height: 60vh; overflow: auto">
                                            <asp:GridView ID="GV_PatientList" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AllowPaging="true" AllowCustomPaging="true" OnRowCommand="gv_PatientTestlist_RowCommand" 
                                                OnPageIndexChanging="GV_PatientList_PageIndexChanging"
                                                DataKeyNames="ID" 
                                                AutoGenerateColumns="False"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Sl.No
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                             <%# (Container.DataItemIndex+1)+(GV_PatientList.PageIndex)*GV_PatientList.PageSize %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Inv.No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                             <asp:Label runat="server" ID="lbl1_urgencyid" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("UrgencyID") %>' Width="250px"></asp:Label>
                                                             <asp:Label runat="server" ID="lbl1_devicestatus" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("UrgencyID") %>' Width="250px"></asp:Label>
                                                              <asp:Label ID="lvl_LabInv" runat="server" Text='<%# Eval("Investigationumber")%>'></asp:Label>
                                                            <asp:Label ID="lblPID" Font-Underline="true" ForeColor="Red" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Click Patient Name 
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPatTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                            <asp:Label ID="lbl_PatUHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                            <asp:Label ID="Label1"  runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                            <asp:LinkButton ID="lbl_PatientName" Visible="false" ForeColor="Red" Font-Underline="true" CommandName="GetTest" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("PatientName") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                      <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Click Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1_subgrpID" ForeColor="#ffccff" Visible="false" runat="server" Text='<%# Eval("LabSubGroupID")%>'></asp:Label>
                                                                <asp:Label ID="lbl1ID" ForeColor="#ffccff" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lbl1TestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                                <asp:Label ID="lbl1_UHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                <asp:LinkButton ID="lbl1_test" Font-Underline="true" ForeColor="Red" CommandName="ResultEntry" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("TestName") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                        </asp:TemplateField>
                                                      <asp:TemplateField>
                                                            <HeaderTemplate>
                                                               Added Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddedDate" runat="server" Text='<%# Eval("AddedDate")%>'></asp:Label>
                                                               </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl1_reciestatus" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IsMLTreceived") %>'></asp:Label>
                                                                <asp:Label ID="lblrptEntryStatus" runat="server" Text='<%# Eval("ReportEntryStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                    <asp:ButtonField Visible="false" CommandName="Update" HeaderText="Update Receiving Time" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                                                </Columns>
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />

                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <!---- TEST LIST ----------->
                                <div class="col-sm-6 hidden">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group input-group">
                                                <asp:TextBox runat="server" placeholder="Test List.." Font-Bold="true" Width="543px" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_PatientDetails" ReadOnly="True"></asp:TextBox>
                                                <asp:Label ID="hdnuhid" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="hdninvnumber" runat="server" Visible="false"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                <asp:GridView ID="gv_labtestlist" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowUpdating="gv_labtestlist_OnRowUpdating"
                                                    OnRowDataBound="gv_labtestlist_RowDataBound" OnRowCommand="gv_labtestlist_RowCommand" DataKeyNames="ID"
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

                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Click Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_subgrpID" ForeColor="#ffccff" Visible="false" runat="server" Text='<%# Eval("LabSubGroupID")%>'></asp:Label>
                                                                <asp:Label ID="lblID" ForeColor="#ffccff" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_UHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                <asp:LinkButton ID="lbl_test" Font-Underline="true" ForeColor="Red" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("TestName") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Status
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrptEntryStatus" runat="server" Text='<%# Eval("ReportEntryStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Receive Time
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_reciestatus" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IsMLTreceived") %>'></asp:Label>
                                                                <asp:Label ID="lbl_recvTime" Style="text-align: left !important;" runat="server" Text='<%# Eval("MLTrecievingDatetime","{0:dd-MM-yyyy:hh:mm:ss tt}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:ButtonField Visible="false" CommandName="Update" HeaderText="Update Receiving Time" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
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
                <asp:TabPanel ID="tab2" runat="server" Visible="false" CssClass="Tab1" HeaderText="Report Maker">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelSample_collection">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_name" ReadOnly="True"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">UHID</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_UHID" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" Width="264px" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_patientnumber" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Address</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_address" ReadOnly="True"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Inv Number</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_invnumbers" ReadOnly="True"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Requested on</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtrequestedon" ReadOnly="True"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Referral</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                            ID="txt_referral"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Test Name </span>
                                        <asp:HiddenField ID="hdntestID" runat="server" />
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm " ReadOnly="True"
                                            ID="txt_testname"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <asp:RadioButton ID="rbtn_normal" ForeColor="Black" AutoPostBack="true" runat="server" GroupName="resulttype" Text=" Normal Result" OnCheckedChanged="rbtn_normal_CheckedChanged"></asp:RadioButton>
                                        <asp:RadioButton ID="rbtn_exceptional" ForeColor="Black" runat="server" AutoPostBack="true" GroupName="resulttype" OnCheckedChanged="rbtn_exceptional_CheckedChanged" Text=" Exceptional Result"></asp:RadioButton>
                                        <asp:LinkButton ID="Linknewload" ForeColor="Green" Font-Underline="true" Font-Italic="true" OnClientClick="javascript: return confirm('Are you sure to reload fresh Data ?');" OnClick="Linknewload_Click" Text="Refesh" runat="server"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:GridView ID="Gv_LabResult" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." OnRowDataBound="Gv_LabResult_RowDataBound" AutoGenerateColumns="False"
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
                                                                Parameter
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_patienttype" Visible="false" runat="server" Text='<%# Eval("PatientTypeID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_reporttype" Visible="false" runat="server" Text='<%# Eval("ReportType")%>'></asp:Label>
                                                                <asp:Label ID="lbl_urgencyID" Visible="false" runat="server" Text='<%# Eval("UrgencyID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_machineID" Visible="false" runat="server" Text='<%# Eval("MachineID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_methodID" Visible="false" runat="server" Text='<%# Eval("MethodID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_container" Visible="false" runat="server" Text='<%# Eval("ContainerID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_paramaterID" Visible="false" runat="server" Text='<%# Eval("ParameterID")%>'></asp:Label>
                                                                <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_parametr" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Parameter") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="25%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Result
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtresult" Style="text-align: left !important;" runat="server" Width="200px" Text='<%# Eval("LabResultValue")%>'></asp:TextBox>
                                                             
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Unit
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitID" Visible="false" runat="server" Text='<%# Eval("UnitID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_rowtypeID" Visible="false" runat="server" Text='<%# Eval("RowType")%>'></asp:Label>
                                                                <asp:TextBox ID="txt_unit" Style="text-align: left !important;" runat="server" Width="100px" Text='<%# Eval("Unit")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Biological Reference
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_range" Style="text-align: left !important;" Width="150px" runat="server" Text='<%# Eval("Ranges") %>'></asp:TextBox>
                                                                   <asp:TextBox ID="txt_rangewording" visible="false" runat="server" Width="100px" Text='<%# Eval("RangeWording")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Range Wording
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_remarks" Style="text-align: left !important;" TextMode="MultiLine" Height="20PX" Width="150px" runat="server" Text='<%# Eval("RangeWording") %>'></asp:TextBox>
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Is     Normal?
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_resultstatus" Visible="false" runat="server" Text='<%# Eval("IsNormal")%>'></asp:Label>
                                                                <asp:CheckBox ID="chknormal" runat="server" />
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
                            <div class="row" style="height: 5px">
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div>
                                        <asp:TextBox ID="txt_overallReamrks" placeholder="Remarks......" Height="60px" TextMode="MultiLine" runat="server" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="height: 5px">
                            </div>
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Report Template <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_template">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" id="divtaken" runat="server">
                                    <div class="form-group input-group">
                                        <span id="lbl_case" class="input-group-addon cusspan" runat="server">Verified By</span>
                                        <asp:DropDownList ID="ddl_verifiedby" runat="server" Class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Machine Name<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_machinename">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="Bulk_Update" />
                                        <asp:Button ID="btn_print" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnprint_Click" Text="Print" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tab3" runat="server" Visible="true" CssClass="Tab1" HeaderText="Report Maker">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div4">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div5" runat="server">
                                    <asp:Label ID="lblmessage3" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_patientname3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">UHID</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_uhid3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" Width="264px" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_patientnumber3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Address</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_address3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Inv Number</span>

                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_inv3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Requested on</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_requested3" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Referral</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="True"
                                            ID="txt_referal3"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Test Name </span>
                                        <asp:HiddenField ID="hdntestID3" runat="server" />
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm " ReadOnly="True"
                                            ID="txt_testname3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-6">
                                    <div class="row gridview-container-ML ">
                                        <div class="col-sm-8">
                                            <div class="form-group input-group">
                                                <label class="gridview-label">Test Result</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group input-group">
                                                <asp:RadioButton ID="rabtn_growth" ForeColor="Black" AutoPostBack="true" runat="server" GroupName="resulttype" Text="Growth" OnCheckedChanged="rabtn_growth_CheckedChanged"></asp:RadioButton>
                                                <asp:RadioButton ID="rabtn_nogrowth" ForeColor="Black" runat="server" AutoPostBack="true" GroupName="resulttype" Text="No Growth" OnCheckedChanged="rabtn_nogrowth_CheckedChanged"></asp:RadioButton>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="form-group input-group">
                                                <span id="Span27" class="input-group-addon cusspan mandbg" runat="server">sample <span
                                                    style="color: red">*</span></span>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_sample"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" id="idg" visible="false" runat="server">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="form-group input-group">
                                                        <asp:DropDownList ID="ddl_growthtype" Class="form-control input-sm col-sm mandbg" runat="server">
                                                            <asp:ListItem Value="0">-Select type-</asp:ListItem>
                                                            <asp:ListItem Value="1">Colony Count</asp:ListItem>
                                                            <asp:ListItem Value="2">Gram Stain</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-8" style="margin-left: -67px; width: 200px !important;">
                                                    <div class="form-group input-group">
                                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm "
                                                            ID="txt_colonycount"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group input-group">
                                                <span id="Span29" class="input-group-addon cusspan mandbg" runat="server">
                                                    <asp:Label ID="lbl_growthsttus" runat="server" Text="Result"></asp:Label>
                                                    <span
                                                        style="color: red">*</span></span>
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm "
                                                    ID="txt_organisimyeilded"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div>
                                                <asp:TextBox ID="txt_remark3" placeholder="Remarks......" Height="125px" TextMode="MultiLine" runat="server" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="form-group input-group">
                                                <span id="Span30" class="input-group-addon cusspan" runat="server">Method <span
                                                    style="color: red">*</span></span>
                                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" placeholder="Test Method"
                                                    ID="ddlTestMethod">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group">
                                            <label class="gridview-label">Antimicrobial Sensitivity</label>
                                        </div>
                                    </div>
                                    <div class="row gridview-container-ML">
                                        <div class="col-sm-12">
                                            <div>
                                                <div class="pbody">
                                                    <div class="grid" style="float: left; width: 100%; height: 35vh; overflow: auto">
                                                        <asp:GridView ID="Gv_antibioticlist" runat="server" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="Gv_antibioticlist_RowDataBound"
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
                                                                        Antibiotic
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_antibitic" runat="server" Text='<%# Eval("AntibioticName")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_antibiotocID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="18%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Sensitivity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sensitivityID" Visible="false" runat="server" Text='<%# Eval("AntibioticSensitiveTypeID")%>'></asp:Label>
                                                                        <asp:DropDownList ID="ddl_sensitivity" Width="100px" runat="server">
                                                                        </asp:DropDownList>
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
                            </div>
                            <div class="row" style="height: 5px">
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Report Template <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_template3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4" id="div7" runat="server">
                                    <div class="form-group input-group">
                                        <span id="Span26" class="input-group-addon cusspan" runat="server">Verified By</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_verified3">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave3" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint3" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnprint3_Click" Text="Print" />
                                        <asp:Button ID="btnreset3" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnresets3_Click" />
                                        <CR:CrystalReportViewer ID="MediReportViewer" Visible="false" runat="server" PrintMode="Pdf" AutoDataBind="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
            <asp:UpdateProgress ID="updateProgress2" runat="server" Visible="false">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Visible="false" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

