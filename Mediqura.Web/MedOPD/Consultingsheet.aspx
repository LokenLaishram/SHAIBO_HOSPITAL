<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Consultingsheet.aspx.cs" Inherits="Mediqura.Web.MedOPD.Consultingsheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintConsultantSheet() {
            objbillno = document.getElementById("<%=hdnopno.ClientID %>")
            window.open("Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + objbillno.value)
        }
        function PrintDuplicateConsultantSheet(billno) {
            window.open("Reports/ReportViewer.aspx?option=OPDConsultantSheet&BillNo=" + billno)
        }
        function PrintConsultantSheetList() {
            objuhid = document.getElementById("<%=txtUHID.ClientID %>")
            objpatname = document.getElementById("<%=txtname.ClientID %>")
            objdoctortype = document.getElementById("<%=ddldoctortype.ClientID %>")
            objdept = document.getElementById("<%=ddldepartment.ClientID %>")
            objdoctor = document.getElementById("<%=ddlconsultant.ClientID %>")
            objfrom = document.getElementById("<%=txtdate.ClientID %>")
            objto = document.getElementById("<%=txtdateto.ClientID %>")
            window.open("Reports/ReportViewer.aspx?option=OPpatientList&UHID=" + objuhid.value + "&PatientName=" + objpatname.value + "&DoctorType=" + objdoctortype.value + "&DepartmentID=" + objdept.value + "&DoctorID=" + objdoctor.value + "&Datefrom=" + objfrom.value + "&Dateto=" + objto.value)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontvisithistory" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpatienthistory" runat="server" CssClass="Tab2" HeaderText="Patient Consulting Sheet">
                    <ContentTemplate>


                        <div class="custab-panel" id="paneldepartmentmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" style="color: red" runat="server">Bill No</span>
                                        <asp:TextBox runat="server" MaxLength="10" AutoPostBack="true" OnTextChanged="txtbillno_TextChanged" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtbill"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="Getopdbillauto" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtbill"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtbill" ID="FilteredTextBoxExtender3"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">UHID</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txtUHID"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtname"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:HiddenField runat="server" ID="hdnopno" />
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtAge"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Gender</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtGender"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtAddress"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Height <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" placeholder="height in cm" MaxLength="3" Class="form-control input-sm col-sm custextbox"
                                            ID="txtHeight"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtHeight" ID="FilteredTextBoxExtender1"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Weight <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" placeholder="weight in kg" MaxLength="3" Class="form-control input-sm col-sm custextbox"
                                            ID="txtWeight"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtWeight" ID="FilteredTextBoxExtender2"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">BP <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" placeholder="units in mmHg" MaxLength="10" Class="form-control input-sm col-sm custextbox"
                                            ID="txtBP"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtBP" ID="FilteredTextBoxExtender5"
                                            runat="server" ValidChars="0123456789/"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Pulse Rate <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="3" placeholder="per minute" Class="form-control input-sm col-sm custextbox"
                                            ID="txtpulse"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpulse" ID="FilteredTextBoxExtender4"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Doctor Type <span
                                            style="color: red">*</span></span>
                                        <asp:HiddenField runat="server" ID="hdndoctortype" />
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddldoctortye_SelectedIndexChanged" ID="ddldoctortype"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Department <span
                                            style="color: red">*</span></span>
                                        <asp:HiddenField runat="server" ID="hdndepartmentID" />
                                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" ID="ddldepartment"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Consultant <span
                                            style="color: red">*</span></span>
                                        <asp:HiddenField runat="server" ID="hdndoctorID" />
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" ID="ddlconsultant"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Date from</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Date To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtdateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdateto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Remarks </span>
                                        <asp:TextBox runat="server" placeholder="Enter remarks...." Class="form-control input-sm col-sm custextbox"
                                            ID="txtremarks"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6"></div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnprintcs" runat="server" OnClientClick="return PrintConsultantSheet();" Class="btn  btn-sm cusbtn" Text="Print CS" />
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" Visible="false" OnClientClick="return PrintConsultantSheetList();" Text="Print" />
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
                            <div id="Div1" class="row cusrow " runat="server">
                                <div id="Div2" class="col-sm-12" runat="server">
                                    <div id="Div3" runat="server">
                                        <div id="Div4" class="pbody" runat="server">
                                            <div id="Div5" class="grid" runat="server" style="float: left; width: 100%; height: 30vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvOpdPatientConsulting" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblserial" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Serial") %>'></asp:Label>
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
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Address
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("Address") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Height(cm)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblheight" Style="text-align: center !important;" runat="server" Text='<%# Eval("Height")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Weight(kg)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblweight" Style="text-align: center !important;" runat="server" Text='<%# Eval("Weight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                BP(mmHg)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbp" runat="server" Style="text-align: center !important;" Text='<%# Eval("BP")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Pulse/min
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpulserate" Style="text-align: center !important;" runat="server" Text='<%# Eval("PulseRate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Date 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy hh:mm:ss tt}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Print
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateConsultantSheet('<%# Eval("ID")%>'); return false;">Print</a>
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
                        </div>

                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
