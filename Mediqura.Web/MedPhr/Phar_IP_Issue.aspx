<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Phar_IP_Issue.aspx.cs" Inherits="Mediqura.Web.MedPhr.Phar_IP_Issue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function calculate1() {
            var hdn_mrp_per_Qty = document.getElementById("<%=hdnmrpperqty.ClientID%>").value;
            var hdnequivalent = document.getElementById("<%=hdnequivalentqty.ClientID%>").value;
            var nounit = document.getElementById("<%=txtNoUnit.ClientID%>").value
            document.getElementById("<%=txtequivalentqty.ClientID%>").value = (+nounit * hdnequivalent).toString();
            document.getElementById("<%=txtrate.ClientID%>").value = (+document.getElementById("<%=txtequivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var finalequiv = document.getElementById("<%=txtequivalentqty.ClientID%>").value;
            var Totalavail = document.getElementById("<%=txt_totalavail.ClientID%>").value;

            if (+finalequiv > +Totalavail) {
                document.getElementById("<%=txtNoUnit.ClientID%>").value = "1";
                document.getElementById("<%=txtequivalentqty.ClientID%>").value = (1 * hdnequivalent).toString();
                document.getElementById("<%=txtrate.ClientID%>").value = (+document.getElementById("<%=txtequivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                alert("Sales quantity exceeds Available Qty.");
            }
        } 
        function calculate2() {
            var hdn_mrp_per_Qty = document.getElementById("<%=hdnmrpperqty.ClientID%>").value;
            var hdnequivalent = document.getElementById("<%=hdnequivalentqty.ClientID%>").value;

            document.getElementById("<%=txtNoUnit.ClientID%>").value = (+document.getElementById("<%=txtequivalentqty.ClientID%>").value / hdnequivalent).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
            document.getElementById("<%=txtrate.ClientID%>").value = (+document.getElementById("<%=txtequivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];

            var finalequiv = document.getElementById("<%=txtequivalentqty.ClientID%>").value;
            var Totalavail = document.getElementById("<%=txt_totalavail.ClientID%>").value;

            if (+finalequiv > +Totalavail) {
                document.getElementById("<%=txtNoUnit.ClientID%>").value = "1";
                document.getElementById("<%=txtequivalentqty.ClientID%>").value = (1 * hdnequivalent).toString();
                document.getElementById("<%=txtrate.ClientID%>").value = (+document.getElementById("<%=txtequivalentqty.ClientID%>").value * hdn_mrp_per_Qty).toString().match(/^-?\d+(?:\.\d{0,2})?/)[0];
                alert("Sales quantity exceeds Available Qty.");
            }
        }
    </script>
    <script type="text/javascript">
        function PrintIPDrugRecordList() {
            objuhid = document.getElementById("<%=hdn_UHID.ClientID %>")
            objipno = document.getElementById("<%=hdn_IPNO.ClientID %>")
            objsubstock = document.getElementById("<%=txt_drug.ClientID %>")
            objdatefrom = document.getElementById("<%=txtfrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            window.open("../MedPhr/Reports/ReportViewer.aspx?option=PhrIPDrugIssueList&UHID=" + objuhid.value + "&IPNO=" + objipno.value + "&SubStockID=" + objsubstock.value + "&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabIPIssue" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanelIPIssue" runat="server" CssClass="Tab2" HeaderText="IP Drug Record">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelIPIssue">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="panel1" DefaultButton="btnsave">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">IP Patient Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txtpatientNames_TextChanged"
                                                ID="txtpatientNames"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdnuhid" />
                                            <asp:HiddenField runat="server" ID="hdnipnumber" />
                                            <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                                runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars=" :/|-" Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetIPPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="lblwardbedno" class="input-group-addon cusspan" runat="server">Ward / Bed No.</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtwardbedno"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="lbladdress" class="input-group-addon cusspan" runat="server">Address</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtaddress"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblage" class="input-group-addon cusspan" runat="server">Age</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtage"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblgender" class="input-group-addon cusspan" runat="server">Sex</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtgender"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-sm-9">
                                        <div class="form-group input-group">
                                            <span id="lbldrugname" class="input-group-addon cusspan" runat="server">Drug Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txt_drugsname_TextChanged"
                                                ID="txtdrugname"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdndrugID" />
                                            <asp:FilteredTextBoxExtender TargetControlID="txtdrugname" ID="FilteredTextBoxExtender1"
                                                runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars=" :/.|%-()" Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetDrugName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtdrugname"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cus-long-span" runat="server">Total Available Quantity</span>
                                            <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txt_totalavail"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_totalavail" ID="FilteredTextBoxExtender7"
                                                runat="server" ValidChars="0123456789."
                                                Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group input-group">
                                            <span id="lblComposition" class="input-group-addon cusspan" runat="server">Composition</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txt_searchcomposition_TextChanged" TextMode="MultiLine" Rows="6" Height="30px"
                                                ID="txtcomposition"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="Getsearchbycomposition" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtcomposition"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                            <asp:HiddenField runat="server" ID="hdnmrpperqty" />
                                            <asp:HiddenField runat="server" ID="hdnequivalentqty" />
                                            <asp:HiddenField runat="server" ID="hdnchkupdate" />

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblNoUnit" class="input-group-addon cusspan" runat="server">Sale in Unit</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" onkeyup="return calculate1();"
                                                ID="txtNoUnit"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtNoUnit" ID="FilteredTextBoxExtender2"
                                                runat="server" FilterType="Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars="." Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblequivalentqty" class="input-group-addon cusspan" runat="server">Sale in Qty</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" onkeyup="return calculate2();"
                                                ID="txtequivalentqty"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtequivalentqty" ID="FilteredTextBoxExtender3"
                                                runat="server" FilterType="Numbers"
                                                FilterMode="ValidChars"
                                                ValidChars="." Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblrate" class="input-group-addon cusspan" runat="server">Rate</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtrate"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group cuspanelbtngrp  pull-left">
                                            <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  scusbtn" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsgs3" runat="server" style="padding-left: 10px;">
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
                                                    <asp:GridView ID="gvDrugdetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblIssueNo" Visible="false" runat="server" Text='<%# Eval("IPDrgIssueNo") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_substockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Visible="true" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rate" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    No.Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_unit" runat="server" Text='<%# Eval("NoUnit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Equiv.Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("EquivalentQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Net Charge
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_netcharges" runat="server" Text='<%# Eval("NetCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
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
                            </div>

                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabmedicationdetails" runat="server" CssClass="Tab2" HeaderText="IP Drug Record List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelmedicationdetails">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblIPpatient2" class="input-group-addon cusspan" runat="server">Patient Name <span
                                                style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="txt_IPpatients_TextChanged" AutoPostBack="true"
                                            ID="txt_IPpatient"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hdn_IPNO" />
                                        <asp:HiddenField runat="server" ID="hdn_UHID" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_IPpatient" ID="FilteredTextBoxExtender4"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/|-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="Get_IPPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IPpatient"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lblwardbed" class="input-group-addon cusspan" runat="server">Ward / Bed No.</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_wardbedNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="lbl_Address" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_sex" class="input-group-addon cusspan" runat="server">Sex</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_sex"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_Age" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_Age"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group">
                                        <span id="lbldrug" class="input-group-addon cusspan" runat="server">Drug Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_drug"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_DrugID" runat="server" />
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_drug" ID="FilteredTextBoxExtender6"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/.|%-()" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetItemDetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_drug"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtfrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Date To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" AutoPostBack="true"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy.dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Satus</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddl_status">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">InActive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-left">
                                        <asp:Button ID="btn_search" runat="server" Text="Search" Class="btn  btn-sm scusbtn" OnClick="btn_searchs_Click" />
                                        <asp:Button ID="btn_print" runat="server" Text="Print" Class="btn  btn-sm scusbtn" OnClientClick="return PrintIPDrugRecordList();" />
                                        <asp:Button ID="btn_Reset" runat="server" Text="Reset" Class="btn  btn-sm scusbtn" OnClick="Reset_OnClick" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg" runat="server" style="padding-left: 10px;">
                                            <asp:Label ID="lblresults" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIV1" class="text-center loading" runat="server">
                                                                <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvIPDrugRecordlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="20" OnRowCommand="GvIPDrugRecordlist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Drug Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lblDrgRecNo" Visible="false" runat="server" Text='<%# Eval("IPDrgIssueNo") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubstockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemName" Visible="true" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rate" runat="server" Text='<%# Eval("MRPperQty", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    No.Unit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_unit" runat="server" Text='<%# Eval("NoUnit")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("EquivalentQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Net Charge
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_netcharges" runat="server" Text='<%# Eval("NetCharge", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="150px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDeletes" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                        <i class="fa fa-trash-o cus-delete-color"></i>                                        
                                                                    </asp:LinkButton>
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
                            </div>
                        </>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
