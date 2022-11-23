<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="NurseNotes.aspx.cs" inherits="Mediqura.Web.MedNurse.NurseNotes" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:content id="Content1" contentplaceholderid="Mediquraplaceholder" runat="server">
    <link href="../Styles/jquery.timepicker.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery.timepicker.min.js"></script>

    <script type="text/javascript">

        function pageLoad() {
            var options = { 'timeFormat': 'h:i:s A', 'step': 5 }
            //var option = { 'timeFormat': 'H:i:s', 'step': 1 }
             $("#<%=txttime.ClientID %>").timepicker(options);
            //$(document).ready(function () {
            //    $('input.timepicker').timepicker({ timeFormat: 'h:mm:ss p' });
            //});
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
    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
 
        <ContentTemplate>
            <asp:TabContainer ID="tabcontvisithistory" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpatienthistory" runat="server" CssClass="Tab2" HeaderText="Nurse Progress Sheet">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepartmentmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                           
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Patient's Name<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txtpatientNames_TextChanged"
                                            ID="txtpatientNames"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"  
                                            FilterMode="ValidChars" ValidChars=" ():/|-" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="GetAdmittedPatientDetails" runat="server"
                                            ServiceMethod="GetAdmittedPatientsDetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        
                                   <asp:HiddenField ID="HiddenIPNo" runat="server" ></asp:HiddenField>  

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">IP No<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtipno" ReadOnly="true"  ></asp:TextBox>         
                                    </div>
                                </div>
                           </div>
                            <div class="row">
                              <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Room/BedNo.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtbedroom" ReadOnly="true" ></asp:TextBox>         
                                    </div>
                                </div>
                            <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Age<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtage" ReadOnly="true" ></asp:TextBox>         
                                    </div>
                                </div>
                              <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Sex<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtsex" ReadOnly="true"  ></asp:TextBox>         
                                    </div>
                                </div>
                           </div>
                            <div class="row">
                              <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">DOA<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtdoa" ReadOnly="true"></asp:TextBox>         
                                    </div>
                                </div>
                              <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Date From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtdatefrom"  ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />         
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Date To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtdateto"  ></asp:TextBox>     
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdateto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />    
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group" >
                                        <span id="Span7" class="input-group-addon cusspan" runat="server" >Date</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtdate" ReadOnly="true"></asp:TextBox> 
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdate"/>
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate" />          
                                    </div>
                                </div>
                           </div>
                            <div class="row">
                            <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">QRN<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txtqrn" ></asp:TextBox>         
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Time</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ID="txttime" ReadOnly="true" ></asp:TextBox> 
                                            <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                 AcceptAMPM ="true" CultureTimePlaceholder="" Enabled="True" ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="time" TargetControlID="txttime" />         
                                    </div>
                                </div>
                                </div>

                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnadd" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Save" OnClick="btnadd_Click" />
                                        <asp:Button ID="btnsearch" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" Class="btn  btn-sm cusbtn" OnClick="btn_print_Click" Text="Print" />
                                       <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 58vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVNurseNotes" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="GVNurseNotes_RowCommand" 
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
                                                                    Date  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                  <asp:Label ID="lblID" runat="server" class="hidden" Text='<%# Eval("ID")%>'></asp:Label>
                                                                  <asp:Label ID="lblIPNo" Visible="false" Text='<%# Eval("IPNo") %>' runat="server" CssClass="lbl_IPNo"></asp:Label>
                                                                  <asp:Label ID="lblUHID" Visible="false" Text='<%# Eval("UHID") %>' runat="server"></asp:Label>
                                                                 <asp:Label runat="server" Class="form-control input-sm col-sm cusmidium" CssClass="txtdate" Width="80px"  Text='<%# Eval("NotedDate","{0:dd/MM/yyyy}") %>' 
                                                                    ID="lbl_date"></asp:Label>
                                                                   </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                                <asp:TemplateField>
                                                               <HeaderTemplate>
                                                                    Time  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                 <asp:Label ID="lbl_picker"  Width="120px"  runat="server" Text='<%# Eval("NotedTime","{0:hh:mm:ss tt}") %>'  ></asp:Label>
                                                                  </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Particular
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParticular" runat="server"  Width="480px" Text='<%# Eval("Particular") %>' ></asp:Label>
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Signature
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsignature" runat="server"  Width="120px" Text='<%# Eval("AddedBy") %>' ></asp:Label>
                                                                     </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                                   <i  class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                             <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
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
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>

</asp:content>
