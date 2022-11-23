<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="LedgerBook.aspx.cs" inherits="Mediqura.Web.MedAccount.LedgerBook" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script>
        function LedgerBookList() {
            objbranchid = document.getElementById("<%=ddlbranches.ClientID %>")
            objledgerid = document.getElementById("<%=ddl_ledgers.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")

            window.open("../MedAccount/Reports/ReportViewer.aspx?option=LedgerBookList&Branch=" + objbranchid.value + "&Ledger=" + objledgerid.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)
        }
    </script>
    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerAccountGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPanelAccountGroup" runat="server" HeaderText="Ledger Book">
                    <contenttemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Branch </span>
                                        <asp:DropDownList ID="ddlbranches" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" >
                                         </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Ledgers </span>
                                        <asp:DropDownList ID="ddl_ledgers" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                         </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdatefrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                    </div>
                                </div>            
                                </div>
                             <div class="row">
                                 <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Date To <span
                                            style="color: red">*</span> </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtto" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                    </div>
                                </div>
                                 <div class="col-lg-4"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch1" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch1_Click"  />
                                          <asp:Button ID="btnprints" runat="server" Text="Print" Style="margin-left: 8px" Visible="true" OnClientClick="return LedgerBookList();" Class="btn  btn-sm cusbtn" />
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
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading1" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <asp:HiddenField runat="server" ID="lblEntryType" />
                                                            <asp:GridView ID="GVLedgers" runat="server" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            SlNo.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex+1%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Group
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblGroup" runat="server"
                                                                       Text='<%# Eval("GroupName") %>'></asp:Label>
                                                                           </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Ledger
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblLedgername" runat="server"
                                                                       Text='<%# Eval("Ledgername") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           Amount
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblAmount"  runat="server"
                                                                       Text='<%# Eval("Amount","{0:0#.##}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="right" Width="1%" />
                                                                    </asp:TemplateField>
                                                                     
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8EBF5" />
                                                            </asp:GridView>

                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                               <div class="row">
                                    <div class="col-md-4">
                                    </div>
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
                                   </contenttemplate>

                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
