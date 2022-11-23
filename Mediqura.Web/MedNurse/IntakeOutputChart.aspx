<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="IntakeOutputChart.aspx.cs" inherits="Mediqura.Web.MedNurse.IntakeOutputChart" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:content id="Content1" contentplaceholderid="Mediquraplaceholder" runat="server">
   <link href="../Styles/jquery.timepicker.min.css" rel="stylesheet" />
	<script src="../Styles/jquery.timepicker.min.js"></script>
    <script type="text/javascript">
        function PrintInvestChart() {
            obj = document.getElementById("<%=Hiddenipno.ClientID %>")
            window.open("../MedNurse/Reports/ReportViewer.aspx?option=NurseNotesList&IPNo=" + obj.value)
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
            }


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

        function time(objref) {
            var timeoption = { 'timeFormat': 'h:i A', 'step': 5 }
            var txtendtime
            $("#<%=GVIntakeOutput.ClientID %>").timepicker(options);
            var row = objref.parentNode.parentNode;
            var GridView = row.parentNode;
            //Get all input elements in Gridview

            var controls = row.getElementsByTagName("input");
            //Loop through the fetched controls.
            for (var i = 0; i < controls.length; i++) {

                //Find the TextBox control.
                if (controls[i].id.indexOf("txtendtime") != -1) {
                    txtendtime = controls[i];
                }

            }
            txtendtime.timepicker(timeoption);

        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
            <asp:TabContainer ID="tabcontinvestchart" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabInvestChart" runat="server" CssClass="Tab2" HeaderText="Intake and Output Chart">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelInvestChart">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div1" runat="server">
                                            <asp:Label ID="lblupmessage" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Patient's Name<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" onfocus="this.select();" OnTextChanged="txtpatientNames_TextChanged"
                                            ID="txtpatientNames"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5" runat="server" 
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers" FilterMode="ValidChars" ValidChars=" ():/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="Hiddenipno" runat="server"></asp:HiddenField>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Age<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtage" ReadOnly="true"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Sex<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtsex" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Room No/Bed No<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtbedroom" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">IP No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtipno" ReadOnly="true"></asp:TextBox>
                                        <asp:label runat="server" ID="lblUHID" visible="false"></asp:label>
                                        <asp:label runat="server" ID="lblname" visible="false"></asp:label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">DOA<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtdoa" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Doctor.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtdocter" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server"> Date From </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtdatefrom" />
                                       
                                    </div>
                                </div>  
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server"> Date To </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true"
                                            ID="txtdateto"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtdateto" />
                                       
                                    </div>
                                </div>                              

                            </div>
                       
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnadd" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Add New Row" OnClick="btnadd_Click" />
                                        <asp:Button ID="btnsearch" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Print" OnClick="btnprint_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVIntakeOutput" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="GVIntakeOutput_RowCommand"  OnRowDataBound="GVIntakeOutput_RowDataBound"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                      <asp:Label ID="lblpatname" Visible="false" Text='<%# Eval("PatientName") %>' runat="server" CssClass="lbl_IPNo"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
                                                                                                                       
                                                             <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                       Date  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                  <asp:Label ID="lblIPNo" Visible="false" Text='<%# Eval("IPNo") %>' runat="server" CssClass="lbl_IPNo"></asp:Label>
                                                                    <asp:Label ID="lblgvUHID"  Text='<%# Eval("UHID") %>' runat="server" Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblgvName" Visible="false" Text='<%# Eval("PatientName") %>' runat="server"></asp:Label>
                                                                  <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>

                                                                 <asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidium" CssClass="txtdate" Width="65px"  Text='<%# Eval("IntakeOutputDate","{0:dd-dd/MM/yyyy}") %>' 
                                                                    ID="txtdate"></asp:TextBox>
                                                                  <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                                    TargetControlID="txtdate" />
                                                                  <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                                      CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                     CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                     Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" />
                                                                   </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>


                                                               <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    Start Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txtfluidsstart"  Width="90px"  runat="server" Text='<%# Eval("fluidsstart","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                               <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    End Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txtfluidsend"  Width="90px"  runat="server" Text='<%# Eval("fluidsend","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>  
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Fluids(ML)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFluids" runat="server"  Width="50px" MaxLength="5"
                                                                        Text='<%# Eval("Fluids") %>' AutoPostBack="true" OnTextChanged="txtothers_TextChanged"></asp:TextBox>
                                                                     <asp:FilteredTextBoxExtender TargetControlID="txtFluids" ID="FilteredTextBoxExtender1"
                                                                    runat="server" ValidChars=".0123456789"                                                                      Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>  
                                                              <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    Start Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txtoralstart"  Width="90px"  runat="server" Text='<%# Eval("oralstart","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    End Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txtoralend" Width="90px"  runat="server" Text='<%# Eval("oralend","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Oral RT Feed
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtoral" runat="server"  Width="50px" MaxLength="5"  Enabled="True"
                                                                        Text='<%# Eval("oral") %>' AutoPostBack="true" OnTextChanged="txtothers_TextChanged"> </asp:TextBox>
                                                                       <asp:FilteredTextBoxExtender TargetControlID="txtoral" ID="FilteredTextBoxExtender2"
                                                                          runat="server" ValidChars=".0123456789"  Enabled="True">
                                                                       </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>    <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    Start Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txturinestart"  Width="90px"  runat="server" Text='<%# Eval("urinestart","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    End Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:TextBox ID="txturineend" Width="90px"  runat="server" Text='<%# Eval("urineend","{0:hh:mm:ss tt}") %>'  class="form-control input-sm col-sm cusmidium"></asp:TextBox>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Urine Output (MI) (C)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txturine" runat="server"  Width="50px"  MaxLength="5" 
                                                                        Text='<%# Eval("urine") %>' AutoPostBack="true" OnTextChanged="txtothers_TextChanged"> </asp:TextBox>
                                                                      <asp:FilteredTextBoxExtender TargetControlID="txturine" ID="FilteredTextBoxExtender3"
                                                                        runat="server" ValidChars=".0123456789" Enabled="True">
                                                                      </asp:FilteredTextBoxExtender>
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Others
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtothers" runat="server"  Width="50px" MaxLength="5" ValidChars=".0123456789" Enabled="True"
                                                                        Text='<%# Eval("others") %>' AutoPostBack="true" OnTextChanged="txtothers_TextChanged" ></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txtothers" ID="FilteredTextBoxExtender5"
                                                                        runat="server" ValidChars=".0123456789" Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>                                                                    
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>   
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" runat="server"  Width="50px" 
                                                                        Text='<%# Eval("remarks") %>'></asp:TextBox>

                                                                  <asp:TextBox ID="txtTotalIntake" runat="server"  Width="50px" Visible="false" 
                                                                        Text='<%# Eval("totalintakechart") %>'></asp:TextBox>

                                                                    <asp:TextBox ID="txtTotalOutput" runat="server"  Width="50px" Visible="false" 
                                                                        Text='<%# Eval("totaloutputchart") %>'></asp:TextBox>
                                                                   
                                                                <asp:TextBox ID="txttotalbalance" runat="server"  Width="50px" Visible="false" 
                                                                        Text='<%# Eval("totalbalancechart") %>'></asp:TextBox>
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
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
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="1%" />
                                                    </asp:GridView>
                                                </div>

                                 <%-- <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server"> Total Fluids </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                              ID="txttotalfluids"></asp:TextBox>                                           
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server"> Total Oral </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true" 
                                              ID="txttotaloral"></asp:TextBox>                                           
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server"> Total Urine </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                              ID="txttotalurine"></asp:TextBox>                                            
                                    </div>
                                  </div>

                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server"> Total Urine </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                              ID="txttotalothes"></asp:TextBox>                                            
                                    </div>
                                  </div>--%>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server"> Total Others </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true" 
                                              ID="txttotalintakechart"></asp:TextBox>                                           
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server"> 24 Hrs Total Output - (C+D)</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true" AutoPostBack="true"
                                              ID="txttotaloutputchart"></asp:TextBox>                                           
                                    </div>
                                </div>
                                  <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server"> 24 Hrs Total Balance</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true" AutoPostBack="true"
                                              ID="txttotalbalancechart"></asp:TextBox>                                            
                                    </div>
                                </div>

                                                 <div class="col-sm-12">
                                         <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="javascript: return confirm('Are you sure to save ?');" Text="Save" OnClick="btnsave_Click" />
                                        </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            

                                             </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:content>
