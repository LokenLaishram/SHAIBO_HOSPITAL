<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DutyRoster.aspx.cs" Inherits="Mediqura.Web.MedHR.DutyRoster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<script src="../Scripts/fullcalendar.min.js"></script>
	<link href="../Styles/fullcalendar.min.css" rel="stylesheet" />
	<script type="text/javascript">
		var Page
		function pageLoad() {

			Page = Sys.WebForms.PageRequestManager.getInstance()
			Page.add_initializeRequest(OnInitializeRequest)

			var gridheader = $('#<%=GvRosterList.ClientID%>').clone(true);
			var Freezecolumn = $('#<%=GvRosterList.ClientID%>').clone(true);
			var gridwidth = $('#<%=GvRosterList.ClientID%>').outerWidth();
			var firstcolwidth = $('#<%=GvRosterList.ClientID%> th :first-child').outerWidth();
			Freezecolumn.width(firstcolwidth);
			gridheader.width(gridwidth - firstcolwidth);
			Freezecolumn.find("th:gt(0)").remove();
			Freezecolumn.find("td:not(:first-child)").remove();
			gridheader.find("th:first-child").remove();
			gridheader.find("td:first-child").remove();

			var container = $('<table border="0" cellpadding="0" cellspacing="0"><tr><td valign="top"><div id="FCol" style="width:25%; min-height:60vh; position:absolute; margin-left: -305px; "></div></td><td valign="top"><div id="Col" style=" overflow:auto; min-height:60vh; "></div></td></tr></table>');
			$('#FCol', container).html($(Freezecolumn));
			$('#Col', container).html($(gridheader));
			$("#gridcontainer").html('');
			$("#gridcontainer").append(container);

		}

		function OnInitializeRequest(sender, args) {

			var postBackElement = args.get_postBackElement()

			if (Page.get_isInAsyncPostBack()) {
				ddl_department_SelectedIndexChanged
				alert('One request is already in progress....')
				args.set_cancel(true)
			}

		}
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

		//function fullcalender(EmpID) {
		//	$.ajax({
				
		//		type: "POST",
		//		contentType: "application/json",
		//        data: "{}",
		//        url: "../MedHR/DutyRoster/GetRoster(EmpID)",
		//		dataType: "json",
		//		success: function (data) {
		//			$('div[id*=Fullcal]').fullCalendar({
		//				header: {
		//					left: 'prev,next today',
		//					center: 'title',
		//					right: 'month,agendaWeek,agendaDay'
		//				},
		//				editable: true,
		//				events: $.map(data.d, function (item, i) 
		//				{
		//					var event = new Object();
		//					event.id = item.EventID;
		//					event.start = new Date(item.StartDate);
		//					event.end = new Date(item.EndDate);
		//					event.title = item.EventName;
		//					event.url = item.Url;
		//					event.ImageType = item.ImageType;
		//					return event;
		//				}), 
		//				eventRender: function (event, eventElement) 
		//				{
							
		//				},
		//				loading: function (bool) 
		//				{
		//					if (bool) $('#loading').show();
		//					else $('#loading').hide();}});
		//		},
		//		error: function (XMLHttpRequest, textStatus, errorThrown) 
		//		{debugger;}});$('#loading').hide();$('div[id*=fullcal]').show();
		//}
		


	</script>

	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<contenttemplate>
			<asp:TabContainer ID="tabcontaRoster" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabRosterdeatils" runat="server" HeaderText="Duty Roster ">
					<ContentTemplate>
						<asp:Panel ID="panel1" runat="server"  DefaultButton="btnsave">
							<div class="custab-panel" id="panelRoster">
								<div class="fixeddiv">

									<div class="row fixeddiv" id="divmsg1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="lbldepartment" class="input-group-addon cusspan" runat="server">Department <span
												style="color: red">*</span></span>
											<asp:DropDownList ID="ddl_Department" runat="server" class="form-control input-sm col-sm custextbox">
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="lblmonth" class="input-group-addon cusspan" runat="server">Month <span
												style="color: red">*</span></span>
											<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
												ID="ddl_Month">
												<asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
												<asp:ListItem Text="January" Value="1"></asp:ListItem>
												<asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
												<asp:ListItem Text="March" Value="3"></asp:ListItem>
												<asp:ListItem Text="April" Value="4"></asp:ListItem>
												<asp:ListItem Text="May" Value="5"></asp:ListItem>
												<asp:ListItem Text="June" Value="6"></asp:ListItem>
												<asp:ListItem Text="July" Value="7"></asp:ListItem>
												<asp:ListItem Text="August" Value="8"></asp:ListItem>
												<asp:ListItem Text="September" Value="9"></asp:ListItem>
												<asp:ListItem Text="October" Value="10"></asp:ListItem>
												<asp:ListItem Text="November" Value="11"></asp:ListItem>
												<asp:ListItem Text="December" Value="12"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Year <span
												style="color: red">*</span></span>
											<asp:TextBox ID="txt_year" runat="server" Class="form-control input-sm col-sm custextbox"  ></asp:TextBox>
											<asp:CalendarExtender ID="tbxsearchyear_CalendarExtender" runat="server" OnClientHidden="onCalendarHidden" 
												OnClientShown="onCalendarShown" BehaviorID="calendar1" Enabled="True" TargetControlID="txt_year" Format="yyyy">

											</asp:CalendarExtender>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn"
												 Text="Search" OnClick="btnsearch_Click"  />
											<asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />

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
                                <div class="row cusrow ">
                                    <div class="col-sm-12">
                                        <div>
											
                                            <div class="pbody">
                                                <div id="gridcontainer" class="grid" style="float: left; width: 75%; min-height:60vh !important; overflow:auto; margin-left: 288px;">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
													
                                                    <asp:GridView ID="GvRosterList" runat="server"  CssClass=" table-hover grid_table result-table" 
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"  ShowHeader="false" 
														OnRowDataBound="GvRosterList_RowDataBound"  HorizontalAlign="Center">
                                                        <Columns>
                                                            
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:label ID="lblempname" runat="server" Text='<%#Eval("EmpName")%>'  style="font-weight:bold;" ></asp:label>
																	 <asp:HyperLink ID="lnkempname" Text='<%#Eval("EmpName")%>' href="javascript: void(null);" 
																		 runat="server" Style="text-align: center !important;" visible="false"
                                                                        OnClick='<%#string.Format("return fullcalender({0}\");", Eval("EmpID"))%>'  >
                                                                        
                                                                    </asp:HyperLink>
                                                                    
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="400" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
																	<asp:Label ID="lblheading" runat="server" visible="false" Text='<%# Eval("heading")%>'></asp:Label>
																	<asp:Label ID="lbldepartment" runat="server" visible="false" Text='<%# Eval("DepartmentID")%>'></asp:Label>
																	<asp:Label ID="lblempID" runat="server" visible="false" Text='<%# Eval("EmpID")%>'></asp:Label>
																	<asp:Label ID="lblyear" runat="server" visible="false" Text='<%# Eval("Year")%>'></asp:Label>
																	<asp:Label ID="lblSeasonID" runat="server" visible="false" Text='<%# Eval("SeasonID")%>'></asp:Label>
																	<asp:Label ID="lblmonth" runat="server" visible="false" Text='<%# Eval("Month")%>'></asp:Label>
																	<asp:Label ID="lblnoofdays" runat="server" visible="false" Text='<%# Eval("No_Of_Days")%>'></asp:Label>
																	<asp:Label ID="lbldate_1" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day1")%>' ></asp:Label>
																	
																	<asp:DropDownList ID="ddl_Rosterdate_1" Class="form-control input-sm col-sm custextbox" style="width:100% !important" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_2" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day2")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_2" Class="form-control input-sm col-sm custextbox" style="width:100% !important" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_3" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day3")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_3" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_4" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day4")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_4" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_5" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day5")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_5" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_6" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day6")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_6" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_7" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day7")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_7" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_8" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day8")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_8" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_9" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day9")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_9" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_10" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day10")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_10" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_11" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day11")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_11" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_12" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day12")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_12" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_13" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day13")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_13" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_14" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day14")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_14" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_15" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day15")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_15" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_16" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day16")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_16" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_17" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day17")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_17" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_18" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day18")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_18" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_19" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day19")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_19" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_20" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day20")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_20" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_21" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day21")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_21" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_22" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day22")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_22" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_23" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day23")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_23" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_24" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day24")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_24" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_25" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day25")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_25" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_26" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day26")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_26" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_27" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day27")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_27" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_28" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day28")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_28" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_29" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day29")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_29" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_30" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day30")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_30" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <ItemTemplate>
																	 <asp:Label ID="lbldate_31" runat="server" visible="false" Text='<%# Eval("RosterDetails_Day31")%>' ></asp:Label>
																	<asp:DropDownList ID="ddl_Rosterdate_31" runat="server" Class="form-control input-sm col-sm custextbox" style="width:100% !important"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"  Width="300" Wrap="false"  Height="35" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                           <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
								<div class="row">	
									<div class="col-sm-12">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn"
												OnClientClick="javascript: return confirm('Are you sure to update ?');" Text="Save" OnClick="btnsave_Click" />
											
										</div>
									</div>
								</div>
							   </div>
						</asp:Panel>
					</ContentTemplate>
				</asp:TabPanel>
				<%--<asp:TabPanel ID="TabPanelleaveadjust" runat="server" CssClass="Tab2" HeaderText="Duty">
					<ContentTemplate>
						<asp:Panel ID="panel2" runat="server" >
							<div class="custab-panel" id="Div2">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="tap2div" runat="server">
										<asp:Label ID="tap2lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">  
								 <div class="col-xs-9 col-xs-push-2">  
								 <div class="box box-primary">  
								<div class="box-body no-padding">  
									 <!-- THE CALENDAR -->  
									<div id="Fullcal"></div>  
							 </div><!-- /.box-body -->  
								</div><!-- /. box -->  
								 </div><!-- /.col -->  
								</div>  
								</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:TabPanel>--%>
			</asp:TabContainer>
		</contenttemplate>
	</asp:UpdatePanel>
</asp:Content>
