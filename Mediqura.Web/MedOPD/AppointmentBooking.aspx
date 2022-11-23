<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="AppointmentBooking.aspx.cs" inherits="Mediqura.Web.MedOPD.AppointmentBooking" enableeventvalidation="false" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        
        function PrintAppoinmentList() {
            objdoc = document.getElementById("<%=ddlconsultant.ClientID %>")
            objdate = document.getElementById("<%=txtdate.ClientID %>")
            window.open("../MedOPD/Reports/ReportViewer.aspx?option=AppoinmentBookingList&Consultant=" + objdoc.value + "&BookingDate=" + objdate.value)
        }

    </script>
    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontvisithistory" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpatienthistory" runat="server" CssClass="Tab2" HeaderText="Appointment Booking">
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
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Consultant <span
                                            style="color: red">*</span></span>
                                        <asp:HiddenField runat="server" ID="hdndoctorID" />
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" ID="ddlconsultant" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Booking Date <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtdate"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txtdate" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnadd" runat="server" Text="Add New Row" Class="btn  btn-sm cusbtn" OnClick="btnadd_Click" />
                                        <asp:Button ID="btnprint" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintAppoinmentList();" Text="Print" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
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
                                                    <asp:GridView ID="gvbookingdetails" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."  OnRowCommand="gvbookingdetails_RowCommand" OnRowDataBound="gvbookingdetails_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center">
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
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Visible="False" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:TextBox ID="txt_name" Style="text-align: left !important;" runat="server" MaxLength="30"   Class="form-control input-sm col-sm cus-long-textbox"
                                                                        Text='<%# Eval("PatientName") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_name" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 " />

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Address
                                                                </HeaderTemplate>
                                                                <ItemTemplate>

                                                                    <asp:TextBox ID="txtaddress" Style="text-align: left !important;" runat="server" MaxLength="50"  Class="form-control input-sm col-sm cus-long-textbox"
                                                                        Text='<%# Eval("Address") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtaddress" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " />

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Contact No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_contact" MaxLength="10" Style="text-align: left !important;" runat="server"  Class="form-control input-sm col-sm cus-long-textbox"
                                                                        Text='<%# Eval("ContactNo") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_contact" ID="FilteredTextBoxExtender1"
                                                                        runat="server" ValidChars="0123456789"
                                                                        Enabled="True">
                                                                      </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Age
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_age" runat="server"  Class="form-control input-sm col-sm cus-long-textbox" Text='<%# Eval("Age") %>'></asp:TextBox>
                                                                     <asp:FilteredTextBoxExtender TargetControlID="txt_age" ID="FilteredTextBoxExtender2"
                                                                    runat="server" ValidChars="0123456789"
                                                                    Enabled="True"> </asp:FilteredTextBoxExtender>
                                                            
                                                                </ItemTemplate>
                                                                   <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_time" runat="server"  Class="form-control input-sm col-sm cus-long-textbox" Text='<%# Eval("Time") %>'></asp:TextBox>
                                                                     <asp:FilteredTextBoxExtender TargetControlID="txt_time" ID="FilteredTextBoxExtende6"
                                                                    runat="server" ValidChars="0123456789: PAMpam"
                                                                    Enabled="True">
                                                                  </asp:FilteredTextBoxExtender>
                                                            
                                                                </ItemTemplate>
                                                                   <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>  
                                                            
                                                               <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_remarks" runat="server"  Class="form-control input-sm col-sm cus-long-textbox" MaxLength="30" AutoPostBack="true" Text='<%# Eval("Remarks") %>' OnTextChanged="txt_remarks_TextChanged"  ></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                                    
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Cancel</span>
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
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="true" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" OnClick="btnexport_Click" Text="Export" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlexport" Visible="true" Class="form-control input-sm col-sm cusmidiumtxtbox"
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
    </asp:updatepanel>

</asp:Content>
