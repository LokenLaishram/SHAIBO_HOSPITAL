<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DischargeReadyList.aspx.cs" Inherits="Mediqura.Web.MedReport.DischargeReadyList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
     <script type="text/javascript">
         function PrintDischargeReadyList() {
             objdatefrom = document.getElementById("<%=txtdatefromList.ClientID %>")
            objdateto = document.getElementById("<%=txttoList.ClientID %>")
            objdischargeby = document.getElementById("<%=ddlDischargeBy.ClientID %>")
            objward = document.getElementById("<%=ddl_ward.ClientID %>")
             window.open("../MedReport/Reports/ReportViewer.aspx?option=DischargeReadyList&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value + "&DischargeBy=" + objdischargeby.value + "&Ward=" + objward.value)
        }
     </script>
   <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <asp:TabContainer ID="tabdisSummary" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
      <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Discharge Ready List">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server">
                    <div class="custab-panel" id="paneldepositlist">
                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="divmsg2" runat="server">
                                <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Issue From <span
                                        style="color: red">*</span></span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
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
                                    <span id="Span7" class="input-group-addon cusspan" runat="server">Issue To <span
                                        style="color: red">*</span> </span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txttoList"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txttoList" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttoList" />

                                </div>
                            </div>
                            <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Intimated By  </span>
                                          
                                                    <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="ddlDischargeBy">
                                                    </asp:DropDownList>
                                            
                                        </div>
                                    </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Ward </span>
                                        <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">

                                    <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                      <asp:Button ID="btnprints" runat="server" Text="Print" OnClientClick="return PrintDischargeReadyList();" class="btn  btn-sm cusbtn" />
                                    <asp:Button ID="btnresetList" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresetList_Click" />

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
                                        <div class="grid" style="float: left; width: 100%;  overflow: auto">
                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gvDischargeList" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="gvDischargeList_PageIndexChanging" AllowPaging="true" PageSize="10"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="gvDischargeList_RowCommand"
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
                                                            IPNo.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                       
                                                            <asp:Label ID="lblIPNo" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("IPNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("PatientName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Ward.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblwardID" Visible="false" runat="server" Text='<%# Eval("wardID") %>'></asp:Label>
                                                       
                                                            <asp:Label ID="lblward" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("Ward") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                              
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Intimated By
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("DoctorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Intimated On
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy hh:mm tt}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                </asp:Panel>
            </ContentTemplate>

        </asp:TabPanel>

    </asp:TabContainer>
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>
