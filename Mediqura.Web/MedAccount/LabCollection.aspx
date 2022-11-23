<%@ Page Title="Transaction Account" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabCollection.aspx.cs" Inherits="Mediqura.Web.MedAccount.LabCollection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintLabCollectionList() {

            objTransType = document.getElementById("<%=ddl_transactionType.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            objcollectedby = document.getElementById("<%=ddlcollectedby.ClientID %>")

            window.open("../MedAccount/Reports/ReportViewer.aspx?option=LabIncomeDetails&TransactionTypeID=" + objTransType.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Status=" + objstatus.value + "&Collectedby=" + objcollectedby.value)
        }
        function PrintDuplicatebills(billno, Package) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=OPDLabBillReceipt&BillNo=" + billno + "&Ispacakge=" + Package)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Lab Account Details">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                            <div class="custab-panel" id="paneldepositlist">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm "
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
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm "
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span28" class="input-group-addon cusspan" runat="server">Collected By</span>
                                            <asp:DropDownList ID="ddlcollectedby" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>


                                </div>
                                <div class="row">

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">TransationType</span>
                                            <asp:DropDownList ID="ddl_transactionType" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintLabCollectionList();" Text="Print" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 fixeddiv" style="color: blue; background-color: lightgray;">Lab Income </div>
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
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="img_UpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvdepositlist" runat="server" CssClass="table-hover grid_table result-table" AllowCustomPaging="false"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" AllowPaging="false"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1) %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Ref No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("RefNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Bill No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Patient Details
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("TransactionNaration") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test Name List
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("TestNameList")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Bill  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotalbillamount" runat="server" Text='<%# Eval("TotalBillAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Dis
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldiscountedamount" runat="server" Text='<%# Eval("DiscountedAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Paid
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("PaidAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Due
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_due" runat="server" Text='<%# Eval("DueAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    Details
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicatebills('<%# Eval("BillNo")%>','<%# Eval("IspackageCompany")%>'); return false;">Details</i></a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="height: 15px">
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span12" class="input-group-addon cusspan" runat="server">Total Bill(₹)  </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalbillamount"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span22" class="input-group-addon cusspan" runat="server">Total Discount(₹)</span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotaldiscounted"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span14" class="input-group-addon cusspan" runat="server">Total Paid(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalpaid"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Total Due(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_totaldue"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <!--------- END OF FIRST GRID VIEW------------- -->

                                <div class="row" style="height: 15px">
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 fixeddiv" style="color: blue; background-color: lightgray;">Lab Expenditure </div>
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="divmsg1" runat="server">
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
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIV_loading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVExpditure" runat="server" CssClass="table-hover grid_table result-table" AllowCustomPaging="false"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" AllowPaging="false"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1) %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Bill No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Particular
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Particular" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("TransactionNaration") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%# Eval("TransactionAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Added By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_addedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Added On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("TransactionDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--------- END OF SECOND GRID VIEW------------- -->
                                <div class="row" style="height: 15px">
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lbltotalincome" class="input-group-addon cusspan" runat="server">Total Income(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalincome"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lbltotalexp" class="input-group-addon cusspan" runat="server">Total Expenditure(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalexp"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="lblbalance" class="input-group-addon cusspan" runat="server">Balance(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtbalance"></asp:TextBox>
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
