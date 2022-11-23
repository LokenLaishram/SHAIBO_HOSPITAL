<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="StaffFundReport.aspx.cs" inherits="Mediqura.Web.MedReport.StaffFundReport" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:content id="Content1" contentplaceholderid="Mediquraplaceholder" runat="server">
   <script type="text/javascript">
       function PrintStaffFundList() {
           objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
           objdateto = document.getElementById("<%=txtto.ClientID %>")
           window.open("../MedReport/Reports/ReportViewer.aspx?option=StaffFundList&DateFrom=" + objdatefrom.value + "&DateTo=" + objdateto.value)
       }
    function pageLoad() {
           var options = {
               now: "0:01", //hh:mm 24 hour format only, defaults to current time
               twentyFour: false,  //Display 24 hour format, defaults to false
               upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
               downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
               close: 'wickedpicker__close', //The close class selector to use, for custom CSS
               hoverState: 'hover-state', //The hover state class to use, for custom CSS
               title: 'Timepicker', //The Wickedpicker's title,
               showSeconds: false, //Whether or not to show seconds,
               timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
               secondsInterval: 1, //Change interval for seconds, defaults to 1,
               minutesInterval: 1, //Change interval for minutes, defaults to 1
               beforeShow: null, //A function to be called before the Wickedpicker is shown
               afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
               show: null, //A function to be called when the Wickedpicker is shown
               clearable: false, //Make the picker's input clearable (has clickable "x")
           };
           var options1 = {
               now: "23:59", //hh:mm 24 hour format only, defaults to current time
               twentyFour: false,  //Display 24 hour format, defaults to false
               upArrow: 'wickedpicker__controls__control-up',  //The up arrow class selector to use, for custom CSS
               downArrow: 'wickedpicker__controls__control-down', //The down arrow class selector to use, for custom CSS
               close: 'wickedpicker__close', //The close class selector to use, for custom CSS
               hoverState: 'hover-state', //The hover state class to use, for custom CSS
               title: 'Timepicker', //The Wickedpicker's title,
               showSeconds: false, //Whether or not to show seconds,
               timeSeparator: ' : ', // The string to put in between hours and minutes (and seconds)
               secondsInterval: 1, //Change interval for seconds, defaults to 1,
               minutesInterval: 1, //Change interval for minutes, defaults to 1
               beforeShow: null, //A function to be called before the Wickedpicker is shown
               afterShow: null, //A function to be called after the Wickedpicker is closed/hidden
               show: null, //A function to be called when the Wickedpicker is shown
               clearable: false, //Make the picker's input clearable (has clickable "x")
           };
           $('.timepicker').wickedpicker(options);
           $('.timepicker1').wickedpicker(options1);


       }
    </script>
    <style>
        .table th {
            position: relative;
        }
    </style> 
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
     <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Staff Fund Report">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                  <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Date From </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                            <asp:TextBox ID="txttimepickerfrom" runat="server" class="timepicker form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                  <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Date To </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                            <asp:TextBox ID="txttimepickerto" runat="server" class="timepicker1 form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
                                        </div>
                                    </div>
                                  <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintStaffFundList();" Text="Print" />
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
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvstafffund" runat="server" CssClass="table-hover grid_table result-table" 
                                                        EmptyDataText="No record found..."  AutoGenerateColumns="False" 
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# (Container.DataItemIndex+1)%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IPNo./EMGNo./BillNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPNo" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("IPNo") %>'></asp:Label>
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
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Service Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsername" runat="server" Text='<%# Eval("ServiceName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    PatientType
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpattype" runat="server" Text='<%# Eval("PatientType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Added Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblaaddate" runat="server" Text='<%# Eval("AddedDTM", "{0:dd-MM-yyyy hh:mm tt}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Doctor
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldoctor" runat="server" Text='<%# Eval("Doctor")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    NetAmount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnetamount" runat="server" Text='<%# Eval("NetAmount", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Justify" Width="1%" />
                                                            </asp:TemplateField>
                                                                                  
                                                         
                                                           
                                                        </Columns>
                                            
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
                             <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Total Amount(₹)  </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txttotalamount"></asp:TextBox>
                                        </div>
                                    </div>
                              </div>
                    
                            </asp:Panel>
                        </div>

                    </ContentTemplate>


                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>


