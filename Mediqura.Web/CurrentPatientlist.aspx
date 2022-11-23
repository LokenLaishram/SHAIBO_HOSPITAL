<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="CurrentPatientlist.aspx.cs" Inherits="Mediqura.Web.MedPhr.CurrentPatientlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src="../Scripts/jQuery.print.js"></script>
    <script>
        function PrintDuplicatebills(PatNumer) {
            if (document.getElementById("<%=hdnroldeID.ClientID%>").value != 34) {
                if (PatNumer.charAt(0) == "i" || PatNumer.charAt(0) == "I") {
                    window.open("../MedPhr/Reports/ReportViewer.aspx?option=IPinterimBill&IPNo=" + PatNumer);
                }
                else {
                    window.open("../MedPhr/Reports/ReportViewer.aspx?option=EmrgInterimBill&EmrgNumber=" + PatNumer);
                }
            }
        }
        $(function () {
            $.contextMenu({
                selector: '#patientList',
                callback: function (key, options) {

                    switch (key) {

                        case "1":
                            $.print("#patientList");
                            break;
                    }
                },
                items: {
                    "1": { name: "Print" }
                }
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $('.searchs').on('keyup', function () {
                var searchTerm = $(this).val().toLowerCase();
                $('#patientList table tbody tr').each(function () {
                    var lineStr = $(this).text().toLowerCase();
                    if (lineStr.indexOf(searchTerm) === -1) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });
            });
        });
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabCurrentPatientList" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpanelCurrentPatientList" runat="server" CssClass="Tab2" HeaderText="Existing Patient List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelCurrentPatientList">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group pull-right">
                                        <span id="lbl_ward" class="input-group-addon cusspan" runat="server">Ward</span>
                                        <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hdnroldeID" />
                                    </div>

                                </div>
                                <div class="col-sm-5">
                                    <div class="form-group input-group pull-right">
                                        <input type="text" class="searchs form-control" placeholder="search patient.." style="font-size: 12px; width: 450px;">
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_pay" runat="server" Text="OP Sale" Class="btn  btn-sm cusbtn" OnClick="btn_pay_Click" />
                                        <asp:Button ID="btn_return" runat="server" Text="OP Return" Class="btn  btn-sm cusbtn" OnClick="btn_return_Click" />
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
                            <div class="row ">
                                <div id="patientList" class="col-sm-12">
                                    <asp:GridView ID="gvadmissionlist" runat="server" CssClass="table-hover grid_table result-table" AllowCustomPaging="false"
                                        EmptyDataText="No record found..." OnRowCommand="gvadmissionlist_RowCommand" OnRowDataBound="gvadmissionlist_RowDataBound" AutoGenerateColumns="False"
                                        Width="100%" HorizontalAlign="Center">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Sl.No
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSlNo" Style="text-align: left !important;" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Emrg / IP No
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluhid" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                    <asp:Label ID="lblSubHeading" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("IsSubHeading") %>'></asp:Label>
                                                    <asp:Label ID="lblIsRelease" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("isReleased") %>'></asp:Label>
                                                    <asp:Label ID="lblPatientActive" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatientActive") %>'></asp:Label>
                                                    <asp:Label ID="lblDisChargeReady" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("IsDischargeReady") %>'></asp:Label>
                                                    <asp:Label ID="lblRegId" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("RegNo") %>'></asp:Label></a>
                                                     <asp:Label ID="lbl_patient" Visible="false" Style="text-align: left !important;" runat="server"
                                                         Text='<%# Eval("PatientName") %>'></asp:Label></a>
                                                     <asp:Label ID="lblpatcatg" Visible="false" Style="text-align: left !important;" runat="server"
                                                         Text='<%# Eval("PatCategory") %>'></asp:Label></a>
                                                      <asp:Label ID="lbl_phbillclear" Visible="false" Style="text-align: left !important;" runat="server"
                                                          Text='<%# Eval("IsPhrBillClear") %>'></asp:Label></a>
                                                       <asp:Label ID="lbl_upperlimit" Visible="false" Style="text-align: left !important;" runat="server"
                                                           Text='<%# Eval("PHRupperLimit") %>'></asp:Label>
                                                    <asp:Label ID="lbl_lowerlimit" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PHRloweLimit") %>'></asp:Label>
                                                        <asp:Label ID="chkcreditstaus" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("ChkCredit") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Patient Details
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Consultant
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladmissiondoc" runat="server" Text='<%# Eval("Doctor")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Pay Adv.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_advancepay" Style="color: #01c; text-decoration-line: underline; cursor: pointer;" runat="server" Text="PayAdv." CommandName="AdvancePay" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Total Bill
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_totalBill" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("TotalBill","{0:0#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Tot.Adv.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_deposit" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("TotalAdvance","{0:0#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Payable
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_payable" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Payable","{0:0#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Refundable
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refundable" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Refundable","{0:0#.##}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Intrm. Bill
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <a style="color: #01c; text-decoration-line: underline; cursor: pointer;" onclick="PrintDuplicatebills('<%# Eval("RegNo") %>')">
                                                        <asp:Label ID="lblprint" Style="text-align: left !important;" runat="server"
                                                            Text="View"></asp:Label></a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Sales
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_sales" Style="color: #01c; text-decoration-line: underline; cursor: pointer;" runat="server" Text="Sale" CommandName="Sale" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Sales Return
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_salesReturn" Style="color: #01c; text-decoration-line: underline; cursor: pointer;" runat="server" Text="Return" CommandName="SaleReturn" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Ref.Adv.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblreund" Style="color: #01c; text-decoration-line: underline; cursor: pointer;" runat="server" Text="Refund" CommandName="Refund" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Bill
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbl_paybil" Style="color: #01c; text-decoration-line: underline; cursor: pointer;" runat="server" Text="Pay" CommandName="BillPay" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                   CrAlow?
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCredit" AutoPostBack="true" OnCheckedChanged="chkCredit_CheckedChanged" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
