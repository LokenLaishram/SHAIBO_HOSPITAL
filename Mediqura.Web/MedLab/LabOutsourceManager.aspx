<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabOutsourceManager.aspx.cs" Inherits="Mediqura.Web.MedLab.LabOutsourceManager" %>
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
    <asp:TabContainer ID="tabcontaineradmission" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabEmrgAdmission" runat="server" HeaderText="Lab Outsource Manager">
            <ContentTemplate>
                <div class="custab-panel" id="Emrgadmissiondetaildiv">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>

                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                           <%--  <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                    <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox" AutoPostBack="True">
                                        
                                    </asp:DropDownList>
                                </div>
                            </div>--%>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_uhid" class="input-group-addon cusspan" runat="server" style="color: red">UHID</span>
                                           <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                    ID="txt_UHID" AutoPostBack="True" OnTextChanged="txt_UHID_TextChanged" MaxLength="10" ></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                    ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_UHID"
                                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                </asp:AutoCompleteExtender>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_UHID" ID="FilteredTextBoxExtender1"
                                                    runat="server" ValidChars="0123456789"
                                                    Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                   </div>
                                </div>
                              
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="100"
                                            ID="txt_patientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged" ></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_patientNames" ID="FilteredTextBoxExtender2"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                            FilterMode="ValidChars"
                                            ValidChars=" " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cus-long-span" runat="server">Test Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_labservices" AutoPostBack="True" OnTextChanged="txt_labservices_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetLabServices" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_labservices"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:Label ID="lblservicename" runat="server" Visible="False"></asp:Label>
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
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To  </span>
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
                                <div class="col-lg-2"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintAdmissionList();" Text="Print" />
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
                                            <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvLabTestList" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnPageIndexChanging="gvLabTestList_PageIndexChanging"
                                                    Width="100%" HorizontalAlign="Center" OnRowDataBound="gvLabTestList_RowDataBound" OnRowCommand="gvLabTestList_RowCommand" OnRowUpdating="gvLabTestList_RowUpdating">
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
                                                                Patient Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname"  runat="server" Text='<%# Eval("PatientName")%>' Style="text-align: left !important;"></asp:Label>
                                                             </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                 Test Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lbl_IsSampleCollcteded" Visible="false" runat="server" Text='<%# Eval("IsSampleCollcteded")%>'></asp:Label>
                                                                  <asp:Label ID="lbl_IsOutsourcedSampleSend" Visible="false" runat="server" Text='<%# Eval("IsOutsourcedSampleSend")%>'></asp:Label>
                                                                  <asp:Label ID="lbl_IsOutsourcedReportReceived" Visible="false" runat="server" Text='<%# Eval("IsOutsourcedReportReceived")%>'></asp:Label>
                                                                 <asp:Label ID="lbl_IsOutsourcedTest" Visible="false" runat="server" Text='<%# Eval("IsOutsourcedTest")%>'></asp:Label>
                                                                 <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                <asp:Label ID="lblUHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                             
                                                                <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("TestID")%>'></asp:Label>
                                                                <asp:Label ID="lbl_testname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("TestName") %>'></asp:Label>
                                                          
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Doctor Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                  <asp:Label ID="lblReferdoc"  runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                               Is Sample Collected?
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%-- <asp:Label ID="lbl_IsSampleCollcteded" Visible="false" runat="server" Text='<%# Eval("IsSampleCollcteded")%>'></asp:Label>--%>
                                                               <asp:CheckBox ID="chkselectIsSampleCollcteded" runat="server" onclick="Check_Click(this);" />
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>

                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Is Outsource Test?
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
<%--                                                                 <asp:Label ID="lbl_IsOutsourcedTest" Visible="false" runat="server" Text='<%# Eval("IsOutsourcedTest")%>'></asp:Label>--%>
                                                                 <asp:CheckBox ID="chkselectIsOutsourcedTest" runat="server" onclick="Check_Click(this);" />
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Is Sample Send?
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <%--  <asp:Label ID="lbl_IsOutsourcedSampleSend" Visible="false" runat="server" Text='<%# Eval("IsOutsourcedSampleSend")%>'></asp:Label>--%>
                                                                 <asp:CheckBox ID="chkselectIsOutsourcedSampleSend" runat="server" onclick="Check_Click(this);" />
                                                            
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Is Report Received?
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                 <%--<asp:Label ID="lbl_ISReportDelivered" Visible="false" runat="server" Text='<%# Eval("ISReportDelivered")%>'></asp:Label>--%>
                                                                 <asp:CheckBox ID="chkselectISReportDelivered" runat="server" onclick="Check_Click(this);" />
                                                              
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header"></span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Button ID="btnSave" runat="server" CommandName="save" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Save" class="btn  btn-sm cusbtn" OnClientClick="javascript: return confirm('Are you sure to save the outsource status ?');" />
                                                                </ItemTemplate>
                                                               <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
       

       
    </asp:TabContainer>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

