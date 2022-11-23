<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LabTestWiseCollecction.aspx.cs" Inherits="Mediqura.Web.MedBills.LabTestWiseCollecction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)

        }

        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()

            if (Page.get_isInAsyncPostBack()) {
                ddl_department_SelectedIndexChanged
                alert('One request is already in progress....')
                args.set_cancel(true)
            }

        }
        function PrintTestWise() {
            objname = document.getElementById("<%=txt_servName.ClientID %>")
            objdatefrom = document.getElementById("<%=txtdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txtto.ClientID %>")
            window.open("../MedBills/Reports/ReportViewer.aspx?option=TestWiseList&ServiceName=" + objname.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)
        }
        function PrintServices(services) {
            window.open("../MedBills/Reports/ReportViewer.aspx?option=ServiceWise&service=" + services)
        }
        //function divexpandcollapse(divname) {

        //    var img = "img" + divname;
        //    if ($("#" + img).attr("src") == "../Images/plus.png") {
        //        $("#" + img)
        //		.closest("tr")
        //		.after("<tr><td></td><td colspan = '50%'>" + $("#" + divname)
        //		.html() + "</td></tr>");
        //        $("#" + img).attr("src", "../Images/minus.png");
        //    } else {
        //        $("#" + img).closest("tr").next().remove();
        //        $("#" + img).attr("src", "../Images/plus.png");
        //    }
        //}
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../Images/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "../Images/plus.gif";
            }
        }

    </script>



    <style>
        .table th {
            position: relative;
        }

        .gvHeader th {
            padding: 7px;
            background-color: #1A4C1A;
            color: #fff;
            border: 1px solid #bbb;
            font-weight: normal;
        }

        .gvChildHeader th {
            padding: 4px;
            background-color: #999966;
            color: #fff;
            border: 1px solid #bbb;
            font-weight: normal;
        }

        .gvRow td {
            padding: 7px;
            background-color: #ffffff;
            border: 1px solid #bbb;
        }

        .gvAltRow td {
            padding: 7px;
            background-color: #f1f1f1;
            border: 1px solid #bbb;
        }

        .collapsed-row {
            display: none;
            padding: 0px;
            margin: 0px;
        }
    </style>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerTestwiseCollection" CssClass="Tab" runat="server" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabTesttWiseCollectionDetails" runat="server" HeaderText="Lab TestWise Collection">
                    <ContentTemplate>
                            <div class="custab-panel" id="panelPatientList">
                                  <asp:Panel ID="panel1" runat="server" DefaultButton="btnsearch">
                      
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div 
                                <div class="row">
                                    <div class="col-sm-4">
                                          <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Service Type</span>
                                              <asp:DropDownList ID="ddl_servicetype"  runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicetype_SelectedIndexChanged" AutoPostBack="True">
                                                  <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                  <asp:ListItem Value="1">OPD</asp:ListItem>
                                                  <asp:ListItem Value="2">IPD</asp:ListItem>
                                                  <asp:ListItem Value="3">Outsider</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                     </div>
                                    <div class="col-sm-4">
                                       <div class="form-group input-group">
                                           <span id="Span29" class="input-group-addon cusspan" runat="server" >Doctor Type</span>
                                           <asp:DropDownList ID="ddldoctortype"  AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddldoctortype_SelectedIndexChanged"></asp:DropDownList>
                                       </div>
                                  </div>
                                    <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblamount" class="input-group-addon cusspan" runat="server">Consultant</span>
                                         <asp:DropDownList ID="ddldoctor" runat="server" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                     </div>
                                    </div>
                                  </div>                                        
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_sevcName" class="input-group-addon cusspan" runat="server">Service Name</span>
                                              <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_servName" AutoPostBack="True" OnTextChanged="txt_servName_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                        ServiceMethod="GetServiceName" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_servName"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                                    </asp:AutoCompleteExtender>
                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_servName" ID="FilteredTextBoxExtender2"
                                                        runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                        FilterMode="ValidChars"
                                                        ValidChars=" " Enabled="True">
                                                    </asp:FilteredTextBoxExtender>
                                               
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtto"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
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
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvTestWiseCollection" AllowPaging="True" PageSize="12" runat="server" CssClass="table-hover grid_table result-table grid"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center" DataKeyNames="Services" OnRowDataBound="GvTestWiseCollection_RowDataBound" OnPageIndexChanging="GvTestWiseCollection_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href="JavaScript:divexpandcollapse('div<%# Eval("Services") %>');">
                                                                        <img id='imgdiv<%# Eval("Services") %>' width="20" height="20" border="0" src="../Images/plus.gif" /></a>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ServiceName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label3" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Services") %>'></asp:Label>

                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    QTY
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("QTY") %>'></asp:Label>

                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="100px" />

                                                               </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        Charges
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label4" Style="text-align: left !important;" runat="server" Text='<%# Eval("Netamt", "{0:0#.##}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                  <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                                                </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    TotalAmount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltotAmt" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("TotalAmt", "{0:0#.##}") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <div style="font-weight: bold">
                                                                        <asp:Label ID="lblTotal" runat="server" />
                                                                    </div>
                                                                </FooterTemplate>

                                                               <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                            </asp:TemplateField>
                                                
                                                            <asp:TemplateField HeaderText="TestWise Detail List">
                                                                <ItemTemplate>
                                                                    <div id='div<%# Eval("Services") %>' style="display: none; position: relative;">
                                                                       <asp:GridView ID="GvTestWiseCollection_nested" DataKeyNames="Services" CssClass="table-hover grid_table result-table" runat="server" EmptyDataText="No record found..."
                                                                            AutoGenerateColumns="False" Width="100%">
                                                                            <Columns>
                                                                                 <asp:TemplateField>
                                                                                    <HeaderTemplate>SL No.</HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex+1%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                                </asp:TemplateField>
                                                                                 <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        Name
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" Style="text-align: left !important;" runat="server" Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        ServiceName
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblserv" Style="text-align: left !important;" runat="server" Text='<%# Eval("Services") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        QTY
                                                                                    </HeaderTemplate>
                                                                                   <ItemTemplate>
                                                                                 <asp:Label ID="lblqty" Style="text-align: left !important;" runat="server"
                                                                                      Text='<%# Eval("QTY") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                               <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                               </asp:TemplateField>
                                                                                 <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        Charges
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblinnerCharge" Style="text-align: left !important;" runat="server" Text='<%# Eval("charges", "{0:0#.##}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        NetCharges
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblinnerNetamt" Style="text-align: left !important;" runat="server" Text='<%# Eval("Netamt", "{0:0#.##}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        Date
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldate" Style="text-align: left !important;" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <pagersettings mode="NumericFirstLast" pagebuttoncount="5" firstpagetext="<<" lastpagetext=">>" />
                                                                        <pagerstyle backcolor="#CFEDE3" cssclass="gridpager" horizontalalign="right" height="1em" width="2%" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" VerticalAlign="Middle"></ItemStyle>
                                                            </asp:TemplateField>
                                                            

                                                        </Columns>
                                                        <HeaderStyle BackColor="#3E3E3E" ForeColor="White" Font-Names="Calibri" />
                                                        <RowStyle Font-Names="Calibri" />

                                                    </asp:GridView>
                                                    <pagersettings mode="NumericFirstLast" pagebuttoncount="5" firstpagetext="<<" lastpagetext=">>" />
                                                    <pagerstyle backcolor="#CFEDE3" cssclass="gridpager" horizontalalign="right" height="1em" width="2%" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group" id="divTestTotal" runat="server" visible="False">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Total <span
                                                style="color: red"></span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_total"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">


                                        <asp:Button ID="btnexport" Visible="False" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                        <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                        runat="server">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>
                                            </asp:UpdatePanel>

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
</asp:Content>
