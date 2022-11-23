<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DueCollectionList.aspx.cs" Inherits="Mediqura.Web.MedIPD.DueCollectionList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintCollectionList() {
            objdatefrom = document.getElementById("<%=txtdatefromList.ClientID %>")
            objdateto = document.getElementById("<%=txttoList.ClientID %>")
            objpatientType = document.getElementById("<%=ddlPatientTypeList.ClientID %>")
            objIPEmrgNo = document.getElementById("<%=txt_IpnoEmrgnoList.ClientID %>")
            objpatient = document.getElementById("<%=txtpatientNamesList.ClientID %>")
            objbillNo = document.getElementById("<%=txt_billnosList.ClientID %>")
            window.open("../MedIPD/Reports/ReportViewer.aspx?option=DueCollectionList&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value + "&PatientType=" + objpatientType.value + "&IPNo=" + objIPEmrgNo.value + "&PatientName=" + objpatient.value + "&billNo=" + objbillNo.value)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerservicemaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabservicemaster" runat="server" CssClass="Tab2" HeaderText="Due Collection">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelservicemaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Type<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList runat="server" ID="ddlpatienttype" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlpatienttype_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                   <div class="col-sm-8">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpatientNames" AutoPostBack="True" OnTextChanged="txtpatientNames_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Bill No.</span>
                                        <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_billnos"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetDueBill" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_billnos"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
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
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-8"></div>
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
                                            <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvduecollectionlist" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="gvduecollectionlist_PageIndexChanging" AllowPaging="True" AllowCustomPaging="True"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center" OnRowCommand="gvduecollectionlist_RowCommand">
                                                    <Columns>
                                                       
                                                        <%--  <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IPNo/EmergNo.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbluhidDue" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IPNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Bill No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcategory" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("PatientCategory") %>'></asp:Label>
                                                                <asp:Label ID="lblBillNo" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("BillNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnameC" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("PatientName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Address
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdressB" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Amount  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotaldueamount" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Paid   
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotallastpaidamount" runat="server" Text='<%# Eval("LastDuePaid", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Last Discount  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_lastdiscount" runat="server" Text='<%# Eval("DiscountAfterDue", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Balance
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalduebal" runat="server" Text='<%# Eval("DueBalance", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <span class="cus-Delete-header"></span>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    Collect
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" Font-Underline="True" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row"></div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Total Due Amount</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_totaldueamt"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Total Last Due Paid</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_totlastduepaid"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Total Due Balance</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_Totduebalance"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" Visible="false" runat="server" CssClass="Tab2" HeaderText="Due Payment">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_bill" class="input-group-addon cusspan" runat="server">Bill Number</span>
                                        <asp:HiddenField ID="hdnbillnumber" runat="server" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_billnumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IP/EMRG Number <span
                                            style="color: red"></span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ipnumber" ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblnameD" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtname"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Age"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Admission Date</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_admissionDate"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <asp:Label ID="lbl_uhid" runat="server" Visible="false"></asp:Label>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">C/O</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_careof"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Department</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_department"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_doctor" class="input-group-addon cusspan" runat="server">Doctor</span>
                                        <asp:Label runat="server" ID="lbl_UHIDTemp" Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_doctor"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div3" runat="server">
                                            <asp:Label ID="Label2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">
                                    <asp:ModalPopupExtender ID="mddueresponsible" runat="server" TargetControlID="btnaddresponsibility" PopupControlID="popupwindow"
                                        BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupwindow" BackColor="#D1EEEE" Style="display: none;">

                                        <div class="row popup">
                                            <div class="col-sm-12 popup-header">
                                                <div class="col-xs-11">
                                                    <h5>Responsibilty Details</h5>
                                                </div>
                                                <div class="col-xs-1">
                                                    <h5>
                                                        <asp:LinkButton runat="server" ID="LinkButton2" OnClick="LinkButton2_Click"> <i class="fa fa-remove"></i> </asp:LinkButton></h5>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 popup-div-msg" id="div_message" runat="server">
                                                <asp:Label ID="message" CssClass="popup-lbl-msg" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="row poprow">
                                                    <div class="col-sm-3">Due Discount(₹)</div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txt_totaldueamount" ReadOnly="true" CssClass="cuspopuptext" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-3">Resp. Staff</div>
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddl_responsiblestaff" AutoPostBack="true" OnSelectedIndexChanged="dddl_responsiblestaff_SelectedIndexChanged" runat="server" class="cuspopuptext">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row poprow">
                                                    <div class="col-sm-12">
                                                        <div class="gridview-container-vs">
                                                            <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                                <asp:GridView ID="gv_responsibledetais" runat="server" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="gv_responsibledetais_RowCommand"
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
                                                                                Amount
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_employeeID" Style="text-align: left !important;" Visible="false" runat="server"
                                                                                    Text='<%# Eval("EmployeeID") %>'></asp:Label>
                                                                                <asp:TextBox ID="lblAmount" Width="50px" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("DiscountedAmount") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Employee Name
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_employeeName" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
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
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 38vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvduelist" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="True" AllowCustomPaging="True"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center">
                                                    <Columns>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Bill No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcategoryA" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("PatientCategory") %>'></asp:Label>
                                                                <asp:Label ID="lblBillNoB" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("BillNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IPNo/EmergNo.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbluhidList" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("IPNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnameE" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("PatientName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Address
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdressA" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Amount  
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotaldueamount" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Paid   
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotallastpaidamount" runat="server" Text='<%# Eval("LastDuePaid", "{0:0#.##}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Due Balance
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltotalduebal" runat="server" Text='<%# Eval("DueBalance", "{0:0#.##}")%>'></asp:Label>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Settlement Mode</span>
                                        <asp:DropDownList ID="ddl_settlementMode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_settlementMode_SelectedIndexChanged">
                                            <asp:ListItem Value="1">Due Payment</asp:ListItem>
                                            <asp:ListItem Value="2">Discount</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                        <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                        <asp:HiddenField runat="server" ID="hdnbankID" />
                                        <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbank"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Cheque No/UTR No.</span>
                                        <asp:Label ID="lbl_accno" Visible="False" runat="server"></asp:Label>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_chequenumber"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span25" class="input-group-addon cusspan" runat="server">Invoice No.</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtinvoicenumber"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Due Amount</span>
                                        <asp:TextBox Visible="false" runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_duelist"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_currentdue"></asp:TextBox>
                                        <asp:Label ID="lblhdnTotalDue" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">
                                            <asp:Label ID="lbl_pay_disc" Text="Pay" runat="server"></asp:Label><span
                                                style="color: red"> *</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtPaid" AutoPostBack="True" OnTextChanged="txtPaid_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtPaid" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="012345678.9"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:Label ID="lblHdnCurrentDue" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Last Due Paid</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35" ReadOnly="True"
                                            ID="txt_duepaidlast"></asp:TextBox>
                                        <asp:Label ID="lblHdnLastDuePaid" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>


                            </div>

                            <div class="row">

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_billDue" class="input-group-addon cusspan" runat="server">Due Receipt Number</span>
                                        <asp:HiddenField ID="hdnbillnumberDue" runat="server" />
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtDueBillNo"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <span id="Span27" class="input-group-addon cusspan" runat="server">Add Responsibilty</span>
                                        <div class="col-sm-1">
                                            <asp:LinkButton runat="server" ID="btnaddresponsibility" OnClick="btnaddresponsibility_Click"> <i class="fa fa-plus"></i> </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Remarks</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="True"
                                            ID="txtremarkdisc"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Pay Due" Class="btn  btn-sm scusbtn"
                                            OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>


                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Due Collection List" CssClass="Tab3">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="custab-panel" id="Div2">
                                    <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearchList">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="divmsg4" runat="server">
                                                <asp:Label ID="lblmessage4" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span15" class="input-group-addon cusspan" runat="server">Patient Type<span
                                                        style="color: red"></span></span>
                                                    <asp:DropDownList runat="server" ID="ddlPatientTypeList" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlPatientTypeList_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span18" class="input-group-addon cusspan" runat="server">IPNO/EMRGNO <span
                                                        style="color: red"></span></span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="20"
                                                        ID="txt_IpnoEmrgnoList"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                        ServiceMethod="GetIpnoEmrgNoList" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IpnoEmrgnoList"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span20" class="input-group-addon cusspan" runat="server">Name</span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtpatientNamesList" AutoPostBack="True"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                        ServiceMethod="GetPatientNameList" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNamesList"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>


                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span21" class="input-group-addon cusspan" runat="server">Due Bill No.</span>
                                                    <asp:TextBox runat="server" MaxLength="10" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                        ID="txt_billnosList"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"
                                                        ServiceMethod="GetDueBillList" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_billnosList"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span22" class="input-group-addon cusspan" runat="server">Date From  </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                                        ID="txtdatefromList"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                        TargetControlID="txtdatefromList" />
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefromList" />

                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span26" class="input-group-addon cusspan" runat="server">Date To  </span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium"
                                                        ID="txttoList"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                        TargetControlID="txttoList" />
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttoList" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-4">
                                                <div class="form-group input-group">
                                                    <span id="Span28" class="input-group-addon cusspan" runat="server">Collected By</span>
                                                    <asp:DropDownList ID="ddlcollectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-8"></div>
                                            <div class="col-sm-4">
                                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                    <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                                    <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintCollectionList();" Text="Print" />
                                                    <asp:Button ID="btnresetsList" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresetsList_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="fixeddiv">
                                                    <div class="row fixeddiv" id="div5" runat="server">
                                                        <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row cusrow pad-top ">
                                            <div class="col-sm-12">
                                                <div>
                                                    <div class="pbody">
                                                        <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                            <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                                <ProgressTemplate>
                                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                    </div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:GridView ID="GvDuebillist" runat="server" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." PageSize="10" OnRowCommand="GvDuebillist_RowCommand" AllowPaging="false" AutoGenerateColumns="False"
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
                                                                            DBill No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcategory" Style="text-align: left !important;" runat="server" Visible="false"
                                                                                Text='<%# Eval("PatientCategory") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_billno" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            UHID
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbluhidBill" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("UHID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblnameF" Style="text-align: left !important;" runat="server"
                                                                                Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Address
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbladdressA" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Last Due Amount
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Discount 
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_discount" runat="server" Text='<%# Eval("DiscountAmount", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Due Paid 
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblaajustedamount" runat="server" Text='<%# Eval("LastDuePaid", "{0:0#.##}")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Due Balance
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_dueamount" runat="server" Text='<%# Eval("DueBalance", "{0:0#.##}")%>'></asp:Label>
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
                                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Dis Remarks
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("DiscountRemarks")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Print
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbl_print" Style="color: red; font-size: 12px" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                     Print
                                                                            </asp:LinkButton>
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

                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span29" class="input-group-addon cusspan" runat="server">Total Due(₹)  </span>
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txttotatDueamount"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-sm-3" >
                                                <div class="form-group input-group">
                                                    <span id="Span32" class="input-group-addon cusspan" runat="server">Total Discount(₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_totaldiscountamnt"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <span id="Span30" class="input-group-addon cusspan" runat="server">Total Due Paid(₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txtTotaDuePaid"></asp:TextBox>
                                                </div>
                                            </div>
                                           
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4"></div>
                                            <div class="col-md-8">
                                                <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" OnClick="btnexport_Click" Class="btn  btn-sm cusbtn exprt" Text="Export" />
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
                                    </asp:Panel>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
