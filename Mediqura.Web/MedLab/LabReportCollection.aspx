<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabReportCollection.aspx.cs" Inherits="Mediqura.Web.MedLab.LabReportCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function printreport(InvNu, UHID, TestID, Template) {
            window.open("Report/ReportViewer.aspx?option=ReportTemplate&Inv=" + InvNu + "&UHID=" + UHID + "&TestID=" + TestID + "&Template=" + Template)
        }

        function btnConfirm() {
            var sresult = confirm("Do you want to deliver the report now?");
            if (sresult == true) {
                document.getElementById("<%=hdnValue.ClientID %>").value = 2;

            }
            else {
                document.getElementById("<%=hdnValue.ClientID %>").value = 1;

            }
        }
    </script>
    <h2 class="breadcumb_cus">Report Delivery</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fixeddiv">
                <div class="row fixeddiv" id="divmsg1" runat="server">
                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="hidepanel" runat="server" Visible="false">
                <div id="confirmBox" runat="server" visible="false">
                    <div class="message"></div>
                    <span class="yes">Yes</span>
                    <span class="no">No</span>
                </div>
                <div class="row">
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span5" class="input-group-addon cusspan" runat="server">Patient Type</span>
                            <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddl_patient_type_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span10" class="input-group-addon cusspan" runat="server">Lab Group</span>
                            <asp:DropDownList ID="ddl_labgroup" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span3" class="input-group-addon cusspan" runat="server">Lab Sub-Group </span>
                            <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_labsubgroup" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group input-group">
                            <span id="Span1" class="input-group-addon cusspan" runat="server">Test Name</span>
                            <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_labTestName" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span7" class="input-group-addon cusspan" runat="server">Inv. No.</span>
                        <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="txt_InvNumber_TextChanged" Class="form-control input-sm col-sm custextbox"
                            ID="txt_InvNumber"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                            ServiceMethod="GetInvNo" MinimumPrefixLength="1"
                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_InvNumber"
                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group input-group">
                        <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Name</span>
                        <asp:TextBox runat="server" AutoPostBack="True" OnTextChanged="txt_patientName_TextChanged" Class="form-control input-sm col-sm custextbox"
                            ID="txt_patientName"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                            ServiceMethod="GetLabPatientName" MinimumPrefixLength="1"
                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientName"
                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span6" class="input-group-addon cusspan" runat="server">Delivery Status</span>
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" OnTextChanged="ddlstatus_TextChanged" class="form-control input-sm col-sm custextbox">
                            <asp:ListItem Value="0">Pending</asp:ListItem>
                            <asp:ListItem Value="1">Delivered</asp:ListItem>
                            <asp:ListItem Value="3">Both</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">

                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span2" class="input-group-addon cusspan" runat="server">Date From</span>
                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtdatefrom_TextChanged" Class="form-control input-sm col-sm custextbox"
                            ID="txtdatefrom"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                            TargetControlID="txtdatefrom" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />

                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span8" class="input-group-addon cusspan" runat="server">Date To</span>
                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtDateto_TextChanged" Class="form-control input-sm col-sm custextbox"
                            ID="txtDateto"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                            TargetControlID="txtDateto" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateto" />
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span9" class="input-group-addon cusspan" runat="server">Centre</span>
                        <asp:DropDownList ID="ddl_centre" runat="server" AutoPostBack="true" OnTextChanged="ddl_centre_TextChanged" class="form-control input-sm col-sm custextbox">
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
            </div>
            <div class="row">
                <div class="col-sm-9">
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                        <asp:Button ID="btnsearch" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                        <asp:Button ID="btnresets" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnValue" runat="server" />
            <div class="row cusrow pad-top ">
                <!------ PATIENT NAME LIST --------->
                <div class="col-sm-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg3" runat="server">
                                    <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                                    <asp:Label ID="lbl_totalrecords" Visible="false" runat="server" Height="13px"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="pbody">
                                <div class="grid" style="float: left; width: 100%; min-height: 55vh; overflow: auto">
                                    <asp:GridView ID="GV_PatientList" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" AllowCustomPaging="true"
                                        EmptyDataText="No any test result entry..... " OnPageIndexChanging="GV_PatientList_PageIndexChanging" OnRowCommand="gv_PatientTestlist_RowCommand"
                                        DataKeyNames="ID"  AutoGenerateColumns="False" OnRowDataBound="GV_PatientList_RowDataBound"
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
                                                    <asp:Label ID="lvl_LabInv"  Visible="false" runat="server" Text='<%# Eval("InVnumber")%>'></asp:Label>
                                                    <asp:Label ID="lblPID" Font-Underline="true" ForeColor="Red" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                    <asp:Label ID="lblHeaderID" Visible="false" runat="server" Text='<%# Eval("HeaderID")%>'></asp:Label>
                                                    <asp:Label ID="lblLabGroup" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("LabGrpID") %>'></asp:Label>
                                                    <asp:Label ID="lbl_subgroup" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("LabSubGrpID") %>'></asp:Label>
                                                    <asp:Label ID="lblreporttypeid" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("ReportTypeID") %>'></asp:Label>
                                                    <asp:Label ID="lblID" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                    <asp:Label ID="lbl_id" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("LabID") %>'></asp:Label>
                                                    <asp:Label ID="lbl_BillID" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("BillID") %>'></asp:Label>
                                                    <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                    <asp:Label ID="lblIsReportPrinted" Visible="false" runat="server" Text='<%# Eval("isPrinted")%>'></asp:Label>
                                                    <asp:Label ID="lbldeliverystatus" Visible="false" runat="server" Text='<%# Eval("DeliveryStatus")%>'></asp:Label>
                                                    <asp:Label ID="lblLabTestID" Visible="false" runat="server" Text='<%# Eval("LabTestID") %>'></asp:Label>
                                                    <asp:Label ID="lbl_invnumber" runat="server" Text='<%# Eval("InVnumber") %>'></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Patient details
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_PatUHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                    <asp:Label ID="lbl_PatientName"  runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Test Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID") %>'></asp:Label>
                                                    <asp:Label ID="lbltemplate" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("Template") %>'></asp:Label>
                                                     <asp:Label ID="lbltemplateID" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("TemplateID") %>'></asp:Label>
                                                    <asp:Label ID="lblTestID" Style="text-align: left !important;" runat="server" Visible="false"
                                                        Text='<%# Eval("TestID") %>'></asp:Label>
                                                    <asp:Label ID="lblurgency" Visible="false" runat="server" Text='<%# Eval("Urgency")%>'></asp:Label>
                                                    <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("TestName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Test On
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltestOn" runat="server" Text='<%# Eval("TestDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Bill Status
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillStatusID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                    <asp:Label ID="lblBillStatus"  runat="server" Text='<%# Eval("PageName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <HeaderTemplate>
                                                    View
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkview" Visible="true" runat="server" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"><span style="color:red">View</span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Print
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsVerified" Visible="false" runat="server" Text='<%# Eval("isVerified")%>'></asp:Label>
                                                    <asp:Label ID="lblNotEntry" runat="server" Text='Not Verified'></asp:Label>
                                                    <asp:LinkButton ID="lnkprint" Font-Underline="true" Visible="false" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">Print</span></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkreprint" Font-Underline="true" Visible="false" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">Delivered/Print</span></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                              <asp:TemplateField>
                                                <HeaderTemplate>
                                                    HPrint
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                     <asp:Label ID="lblNotEntr2" runat="server" Text='Not Verified'></asp:Label>
                                                    <asp:LinkButton ID="lnkEmailPrint" Font-Underline="true" Visible="false" runat="server" CommandName="EmailPrint" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" OnClientClick="btnConfirm();"><span style="color:red">HPrint</span></asp:LinkButton>
                                                      </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:ButtonField Visible="false" CommandName="Update" HeaderText="Update Receiving Time" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group input-group">
                                <asp:TextBox runat="server" Visible="false" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="txt_PatientDetails" ForeColor="DarkBlue" Font-Bold="true" Width="562px" ReadOnly="True"></asp:TextBox>
                                <asp:Label runat="server" ID="hdnpath" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="hdnuhid" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="hdntestid" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="hdnemail" Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="hdninvnumber" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="grid" style="float: left; width: 100%; overflow: auto">
                                <asp:GridView ID="GvPatientTestList" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="True" AllowCustomPaging="True"
                                   OnRowCommand="GvPatientTestList_RowCommand"
                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                    Width="100%" HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Sl.No
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# (Container.DataItemIndex+1) %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Inv. Number
                                            </HeaderTemplate>
                                            <ItemTemplate>
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
                    <div class="row" style="height: 10px"></div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Button ID="btn_send" Visible="false" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" runat="server" Class="btn btn-sm cusbtn pull-right" Text="Send mail" OnClick="btnsendmail_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField runat="server" ID="txtreportTemp" />
            <asp:UpdateProgress ID="updateProgress2" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
