<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="EndoscopyReportVerification.aspx.cs" inherits="Mediqura.Web.MedLab.EndoscopyReportVerification" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerlabsubgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabsubgroupmaster" runat="server" CssClass="Tab2" HeaderText="Endo Report Verification">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panellabgroupmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                   <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                            <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Lab Sub-Group </span>
                                             <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_labsubgroup" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                                              
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Test Name</span>
                                             <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_labTestName" class="form-control input-sm col-sm custextbox" ></asp:DropDownList>
                             

                                        </div>
                                    </div>
                         
                                </div>
                                <div class="row">
                                       <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Lab Test ID</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="50"
                                                ID="txt_lab_test_id"></asp:TextBox>
                                        </div>
                                    </div>
                                       <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">UHID</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_UHID"></asp:TextBox>
                                        </div>
                                    </div>
                                      <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_patientName"></asp:TextBox>
                                        </div>
                                    </div>
                               
                                    </div>
                                 <div class="row">
                                     <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Verified</span>
                                            <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">NO</asp:ListItem>
                                                <asp:ListItem Value="1">YES</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span12" class="input-group-addon cusspan" runat="server">Date From</span>
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
                                    <span id="Span13" class="input-group-addon cusspan" runat="server">Date To </span>
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
                                    </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
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
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                     <asp:GridView ID="GvPatientList" runat="server" CssClass="table-hover grid_table result-table" PageSize="10" AllowPaging="true" AllowCustomPaging="true" OnRowDataBound="GvPatientList_RowDataBound"
                                                          OnRowCommand="GvPatientList_RowCommand"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" 
                                                    Width="100%" HorizontalAlign="Center" OnPageIndexChanging="GvPatientList_PageIndexChanging">
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
                                                                    Lab Test ID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillID" Visible="false" runat="server" Text='<%# Eval("BillID")%>'></asp:Label>
                                                                  <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lblLabTestID" runat="server"
                                                                        Text='<%# Eval("LabTestID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Inv Number
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInvNumber" runat="server"
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
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                                 <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Test Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lblurgency" Visible="false" runat="server" Text='<%# Eval("Urgency")%>'></asp:Label>
                                                                  
                                                                    <asp:Label ID="lblTestName" runat="server" Text='<%# Eval("TestName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">View</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <a target="_blank" href="RadioReportSample.aspx?id=<%# Eval("LabID")%>&billID=<%# Eval("billID")%>&GroupID=<%# Eval("LabGrpID")%>&HeaderID=<%# Eval("HeaderID")%>" >
                                                                        <label class="btn  btn-sm cusbtn"">View</label></a>
                                                
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField >
                                                                <HeaderTemplate>
                                                                   Verify By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblverifyby" runat="server" Visible ="false" Text='<%# Eval("VerifyBy")%>'></asp:Label>
                                                                    <asp:DropDownList ID="ddlverifyby" runat="server" class="form-control input-sm col-sm custextbox"  >
                                                                         </asp:DropDownList>
                                                                      </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                              </asp:TemplateField>
                                                      
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Verify</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblisVerified" Visible="false" runat="server" Text='<%# Eval("isVerified")%>'></asp:Label>
                                                                 
                                                                    <asp:LinkButton ID="lnkVerify" runat="server" CommandName="verify" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to Verify ?');">
                                                                   <label class="btn  btn-sm cusbtn"">Verify</label>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast"  PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                     
                                </div>
                                <asp:HiddenField runat="server" ID="txtreportTemp"/>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>

</asp:Content>
