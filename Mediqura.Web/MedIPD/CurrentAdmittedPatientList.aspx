<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="CurrentAdmittedPatientList.aspx.cs" Inherits="Mediqura.Web.MedIPD.CurrentAdmittedPatientList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src="../Scripts/jQuery.print.js"></script>
    <script>
        function PrintDuplicatebills(billno) {
            if (document.getElementById("<%=hdnroldeID.ClientID%>").value != 34) {
                if (billno.charAt(0) == "i" || billno.charAt(0) == "I") {
                    window.open("../MedBills/Reports/ReportViewer.aspx?option=InterimBill&IPNo=" + billno);
                }
                else {
                    window.open("../MedEmergency/Reports/ReportViewer.aspx?option=EmrgInterimBill&Emrgno=" + billno);
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
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ward" class="input-group-addon cusspan" runat="server">Ward</span>
                                        <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hdnroldeID" />
                                    </div>
                                </div>
                                <div class="col-lg-4"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btn_print" runat="server" Class="btn  btn-sm cusbtn" Text="Print" OnClick="btn_print_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group pull-right">
                                        <input type="text" class="searchs form-control" placeholder="search.." style="font-size: 12px; width: 400px;">
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
                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:GridView ID="gvadmissionlist" runat="server" CssClass="table-hover grid_table result-table"
                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                        Width="100%" HorizontalAlign="Center" OnRowDataBound="gvadmissionlist_RowDataBound">
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
                                                    UHID
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbluhid" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                    <asp:Label ID="lblSubHeading" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("IsSubHeading") %>'></asp:Label>
                                                    <asp:Label ID="lblIsRelease" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("isReleased") %>'></asp:Label>
                                                    <asp:Label ID="lblPatientActive" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatientActive") %>'></asp:Label>
                                                    <asp:Label ID="lblWard" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("wardName") %>'></asp:Label>
                                                    <asp:Label ID="lblDisChargeReady" Visible="false" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("IsDischargeReady") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Emrg/IPNumber
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <a style="color: #01c; text-decoration-line: underline; cursor: pointer;" onclick="PrintDuplicatebills('<%# Eval("RegNo") %>')">
                                                        <asp:Label ID="lblRegId" Style="text-align: left !important;" runat="server"
                                                            Text='<%# Eval("RegNo") %>'></asp:Label></a>

                                                    <asp:Label ID="lblWardTotal" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Agecount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Admitted On
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladmittedon" runat="server" Text='<%# Eval("AdmitedOn","{0:dd-MM-yyyy:hh:mm:ss tt}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("PatName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                <HeaderTemplate>
                                                    Sex
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgender" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("gender") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Address
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("patAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Age
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblage" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Agecount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Bed No.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbed" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Room
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblroom" Style="text-align: left !important;" runat="server"
                                                        Text='<%# Eval("Room") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Consultant
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbladmissiondoc" runat="server" Text='<%# Eval("Doctor")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Diagnosis
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiagnosis" runat="server" Text='<%# Eval("Diagnosis")%>'></asp:Label>
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
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


