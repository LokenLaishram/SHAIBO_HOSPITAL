<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabReport.aspx.cs" Inherits="Mediqura.Web.MedReport.LabReport" %>
<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
  <script type="text/javascript">
      function printreport(InvNu, UHID, TestID, Template) {
          window.open("Report/ReportViewer.aspx?option=ReportTemplate&Inv=" + InvNu + "&UHID=" + UHID + "&TestID=" + TestID + "&Template=" + Template)
      }

     
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabsubgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabsubgroupmaster" runat="server" CssClass="Tab2" HeaderText="Lab Report Collection">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panellabgroupmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Patient Type<span
                                            style="color: red"></span></span>
                                            <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Lab Group<span
                                            style="color: red">*</span></span>

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
                                <div class="row">

                                   <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Date From</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
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
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtDateto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtDateto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDateto" />
                                        </div>
                                    </div>
                                     <div class="col-lg-2"></div>
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
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvPatientList" runat="server" CssClass="table-hover grid_table result-table" EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center" >
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)+(GvPatientList.PageIndex)*GvPatientList.PageSize %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Inv. Number
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLabGroup" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("LabGrpID") %>'></asp:Label>
                                                                    <asp:Label ID="lblreporttypeid" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("ReportTypeID") %>'></asp:Label>
                                                                      <asp:Label ID="lblID" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                                 <asp:Label ID="lbl_id" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("LabID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_BillID" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("BillID") %>'></asp:Label>
                                                                    <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                     <asp:Label ID="lbldeliverystatus" Visible="false" runat="server" Text='<%# Eval("DeliveryStatus")%>'></asp:Label>
                                                                   <asp:Label ID="lblLabTestID" Visible="false" runat="server"
                                                                        Text='<%# Eval("LabTestID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_invnumber" runat="server"
                                                                        Text='<%# Eval("InVnumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UHID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUHID" runat="server"
                                                                        Text='<%# Eval("UHID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Patient
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbltemplate" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("Template") %>'></asp:Label>
                                                                     <asp:Label ID="lblTestID" Style="text-align: left !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("TestID") %>'></asp:Label>
                                                                    <asp:Label ID="lblurgency" Visible="false" runat="server" Text='<%# Eval("Urgency")%>'></asp:Label>
                                                                    <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("TestName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test On
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltestOn" runat="server" Text='<%# Eval("TestDate","{0:dd-MM-yyyy}")%>'></asp:Label>
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
                                    </div>

                                </div>
                                          <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click"/>
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
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:content>

