<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="InvestigationChart.aspx.cs" Inherits="Mediqura.Web.MedNurse.InvestigationChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">

        function onCalendarShown() {

            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
            <asp:TabContainer ID="tabcontinvestchart" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabInvestChart" runat="server" CssClass="Tab2" HeaderText="Investigation Chart">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelInvestChart">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
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
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtage"></asp:TextBox>
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
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">RoomNo/BedNo<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtbedroom"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">IP No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtipno"></asp:TextBox>
                                        <asp:Label runat="server" ID="lblUHID" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">DOA<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtdoa"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Diagnosis<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtdiagnosis"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Consultant<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" ReadOnly="true"
                                            ID="txtconsultant"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Year <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox ID="txt_year" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        <asp:CalendarExtender ID="tbxsearchyear_CalendarExtender" runat="server" OnClientHidden="onCalendarHidden"
                                            OnClientShown="onCalendarShown" BehaviorID="calendar1" Enabled="True" TargetControlID="txt_year" Format="yyyy">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Month<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_month" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" Class="btn  btn-sm cusbtn" OnClick="btn_print_Click" Text="Print" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GVInvestigation" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowDataBound="GVInvestigation_RowDataBound" ShowHeader="false"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltest" runat="server" ForeColor="Blue" Text='<%#CheckIfTitleExists(Eval("TestName").ToString())%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpara" runat="server" Text='<%# Eval("SubTestName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblheading" runat="server" Visible="false" Text='<%# Eval("heading")%>'></asp:Label>
                                                                    <asp:Label ID="lblyear" runat="server" Visible="false" Text='<%# Eval("Year")%>'></asp:Label>
                                                                    <asp:Label ID="lbltestid" runat="server" Visible="false" Text='<%# Eval("TestID")%>'></asp:Label>
                                                                    <asp:Label ID="lblparaid" runat="server" Visible="false" Text='<%# Eval("ParameterID")%>'></asp:Label>
                                                                    <asp:Label ID="lblmonth" runat="server" Visible="false" Text='<%# Eval("MonthID")%>'></asp:Label>
                                                                    <asp:Label ID="lblnoofdays" runat="server" Visible="false" Text='<%# Eval("No_Of_Days")%>'></asp:Label>
                                                                    <asp:Label ID="lbldate_1" runat="server" Visible="false" Text='<%# Eval("date_Day1")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount1" runat="server" Visible="false" Text='<%# Eval("DayCount1")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_2" runat="server" Text='<%# Eval("date_Day2")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount2" runat="server" Visible="false" Text='<%# Eval("DayCount2")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_3" runat="server" Text='<%# Eval("date_Day3")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount3" runat="server" Visible="false" Text='<%# Eval("DayCount3")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_4" runat="server" Text='<%# Eval("date_Day4")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount4" runat="server" Visible="false" Text='<%# Eval("DayCount4")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_5" runat="server" Text='<%# Eval("date_Day5")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount5" runat="server" Visible="false" Text='<%# Eval("DayCount5")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_6" runat="server" Text='<%# Eval("date_Day6")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount6" runat="server" Visible="false" Text='<%# Eval("DayCount6")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_7" runat="server" Text='<%# Eval("date_Day7")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount7" runat="server" Visible="false" Text='<%# Eval("DayCount7")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_8" runat="server" Text='<%# Eval("date_Day8")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount8" runat="server" Visible="false" Text='<%# Eval("DayCount8")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_9" runat="server" Text='<%# Eval("date_Day9")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount9" runat="server" Visible="false" Text='<%# Eval("DayCount9")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_10" runat="server" Text='<%# Eval("date_Day10")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount10" runat="server" Visible="false" Text='<%# Eval("DayCount10")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_11" runat="server" Text='<%# Eval("date_Day11")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount11" runat="server" Visible="false" Text='<%# Eval("DayCount11")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_12" runat="server" Text='<%# Eval("date_Day12")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount12" runat="server" Visible="false" Text='<%# Eval("DayCount12")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_13" runat="server" Text='<%# Eval("date_Day13")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount13" runat="server" Visible="false" Text='<%# Eval("DayCount13")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_14" runat="server" Text='<%# Eval("date_Day14")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount14" runat="server" Visible="false" Text='<%# Eval("DayCount14")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_15" runat="server" Text='<%# Eval("date_Day15")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount15" runat="server" Visible="false" Text='<%# Eval("DayCount15")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_16" runat="server" Text='<%# Eval("date_Day16")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount16" runat="server" Visible="false" Text='<%# Eval("DayCount16")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_17" runat="server" Text='<%# Eval("date_Day17")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount17" runat="server" Visible="false" Text='<%# Eval("DayCount17")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_18" runat="server" Text='<%# Eval("date_Day18")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount18" runat="server" Visible="false" Text='<%# Eval("DayCount18")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_19" runat="server" Text='<%# Eval("date_Day19")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount19" runat="server" Visible="false" Text='<%# Eval("DayCount19")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_20" runat="server" Text='<%# Eval("date_Day20")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount20" runat="server" Visible="false" Text='<%# Eval("DayCount20")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_21" runat="server" Text='<%# Eval("date_Day21")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount21" runat="server" Visible="false" Text='<%# Eval("DayCount21")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_22" runat="server" Text='<%# Eval("date_Day22")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount22" runat="server" Visible="false" Text='<%# Eval("DayCount22")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_23" runat="server" Text='<%# Eval("date_Day23")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount23" runat="server" Visible="false" Text='<%# Eval("DayCount23")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_24" runat="server" Text='<%# Eval("date_Day24")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount24" runat="server" Visible="false" Text='<%# Eval("DayCount24")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_25" runat="server" Text='<%# Eval("date_Day25")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount25" runat="server" Visible="false" Text='<%# Eval("DayCount25")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_26" runat="server" Text='<%# Eval("date_Day26")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount26" runat="server" Visible="false" Text='<%# Eval("DayCount26")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_27" runat="server" Text='<%# Eval("date_Day27")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount27" runat="server" Visible="false" Text='<%# Eval("DayCount27")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_28" runat="server" Text='<%# Eval("date_Day28")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount28" runat="server" Visible="false" Text='<%# Eval("DayCount28")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_29" runat="server" Text='<%# Eval("date_Day29")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount29" runat="server" Visible="false" Text='<%# Eval("DayCount29")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_30" runat="server" Text='<%# Eval("date_Day30")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount30" runat="server" Visible="false" Text='<%# Eval("DayCount30")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldate_31" runat="server" Text='<%# Eval("date_Day31")%>'></asp:Label>
                                                                    <asp:Label ID="lblcount31" runat="server" Visible="false" Text='<%# Eval("DayCount31")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false" Height="35" />
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
