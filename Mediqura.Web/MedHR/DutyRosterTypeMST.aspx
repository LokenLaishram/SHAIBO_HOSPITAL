<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DutyRosterTypeMST.aspx.cs" Inherits="Mediqura.Web.MedHR.DutyRosterTypeMST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<link href="../Styles/jquery.timepicker.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery.timepicker.min.js"></script>
	
	<script type="text/javascript">
		var Page
		function pageLoad() {

			Page = Sys.WebForms.PageRequestManager.getInstance()
			Page.add_initializeRequest(OnInitializeRequest)
			var options = { 'timeFormat': 'h:i A', 'step': 15 }
			var option = { 'timeFormat': 'H:i:s', 'step': 1 }
			$("#<%=txt_Shift_I_SummerStartTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_I_SummerEndTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_I_SummerInrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_I_SummerOutrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_II_SummerStartTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_II_SummerEndTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_II_SummerInrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_II_SummerOutrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_I_WinterStartTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_I_WinterEndTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_I_WinterInrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_I_WinterOutrelaxtion.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_II_WinterStartTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_II_WinterEndTime.ClientID %>").timepicker(options);
			$("#<%=txt_Shift_II_WinterInrelaxation.ClientID %>").timepicker(option);
			$("#<%=txt_Shift_II_WinterOutrelaxtion.ClientID %>").timepicker(option);
			//-------------Double shift with same timing  summer---------------------
			$("#<%=txt_Shift_I_SummerEndTime.ClientID %>").on('changeTime', function () {

				if (document.getElementById("<%=ddl_shifttype.ClientID %>").value == "2") {

					$("#<%=txt_Shift_II_SummerStartTime.ClientID %>").val($(this).val());
				}
			})
			//-------------Double shift with same timing  winter---------------------
			$("#<%=txt_Shift_I_WinterEndTime.ClientID %>").on('changeTime', function () {

				if (document.getElementById("<%=ddl_shifttype.ClientID %>").value == "2") {

					$("#<%=txt_Shift_II_WinterStartTime.ClientID %>").val($(this).val());
				}
			})
		}
		function OnInitializeRequest(sender, args) {

			var postBackElement = args.get_postBackElement()

			if (Page.get_isInAsyncPostBack()) {
				ddl_department_SelectedIndexChanged
				alert('One request is already in progress....')
				args.set_cancel(true)
			}

		}



	</script>
	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:TabContainer ID="tabcontaRoster" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabRosterdeatils" runat="server" HeaderText="Roster Master">
					<ContentTemplate>
						<asp:Panel ID="panel1" runat="server">
							<div class="custab-panel" id="panelRoster"  style="height:500px;">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="divmsg1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="lblcode" class="input-group-addon cusspan" runat="server">Code <span
												style="color: red">*</span></span>
											<asp:TextBox ID="txtcode" runat="server" class="form-control input-sm col-sm custextbox">
											</asp:TextBox>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="lbldescp" class="input-group-addon cusspan" runat="server">Description <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txtdescp"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Double Shift <span
												style="color: red">*</span></span>
											<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
												ID="ddl_shifttype" OnSelectedIndexChanged="ddl_shifttype_SelectedIndexChanged" AutoPostBack="true">
												
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span20" class="input-group-addon cusspan" runat="server">Status </span>
											<asp:DropDownList ID="ddl_status" runat="server"  Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Active" Value="1"></asp:ListItem>
												<asp:ListItem Text="InActive" Value="2"></asp:ListItem>
										</asp:DropDownList>
										</div>
									</div>
								</div>
								<h5 style="background-color:#33aa99;text-align:center;width:100%";font-family: 'Cabin', sans-serif;  font-size: 15px;"> SUMMER</h5>
								<div class="row" id="divsummer1" runat="server">
									<div class="col-sm-5">
										<div class="form-group input-group" id="summer_I" runat="server">
											<span id="lblstart" class="input-group-addon cusspan" runat="server"> Start Time <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="time start form-control input-sm col-sm custextbox" ID="txt_Shift_I_SummerStartTime"  
												onfocus="this.select();" MaxLength ="8">
											</asp:TextBox>
												<span id="lblend" class="input-group-addon cusspan" runat="server"> End Time <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="time end form-control input-sm col-sm custextbox" ID="txt_Shift_I_SummerEndTime" 
												onfocus="this.select();"   MaxLength="8"></asp:TextBox>

										</div>
									</div>
									<div class="col-sm-2">
										<div class="form-group input-group">
											<span id="Span7" class="input-group-addon cusspan" runat="server" style="min-width:20px !important;">Next Day <span
												style="color: red">*</span></span>
											<asp:DropDownList ID="ddl_Shift_I_SummerNextDay" runat="server" Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
												<asp:ListItem Text="No" Value="0"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>				
									<div class="col-sm-5">
										<div class="form-group input-group">
											<span id="Span18" class="input-group-addon cusspan" runat="server">In Relaxation </span>
											<asp:TextBox ID="txt_Shift_I_SummerInrelaxation" runat="server" class="form-control input-sm col-sm custextbox" 
												onfocus="this.select();" MaxLength="8">
											</asp:TextBox>
											<span id="Span19" class="input-group-addon cusspan" runat="server">Out Relaxation </span>
											<asp:TextBox ID="txt_Shift_I_SummerOutrelaxation" runat="server" class="form-control input-sm col-sm custextbox" 
												onfocus="this.select();"   MaxLength="8" >
											</asp:TextBox>
										</div>
									</div>
								
									</div>
								<div class="row" id="divsummer2" runat="server">
									<div class="col-sm-5">
										<div class="form-group input-group" >
											<span id="Span8" class="input-group-addon cusspan" runat="server"> Start Time <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"  ID="txt_Shift_II_SummerStartTime" 
												onfocus="this.select();"   MaxLength="8"></asp:TextBox>
										<span id="Span9" class="input-group-addon cusspan" runat="server"> End Time <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"  ID="txt_Shift_II_SummerEndTime" 
												onfocus="this.select();"   MaxLength="8"></asp:TextBox>

										</div>
									</div>
									<div class="col-sm-2">
										<div class="form-group input-group">
											<span id="Span10" class="input-group-addon cusspan" runat="server" style="min-width:20px !important;">Next Day <span
												style="color: red">*</span></span>
											<asp:DropDownList ID="ddl_Shift_II_SummerNextDay" runat="server" Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
												<asp:ListItem Text="No" Value="0"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>				
									<div class="col-sm-5">
										<div class="form-group input-group">
											<span id="Span11" class="input-group-addon cusspan" runat="server">In Relaxation </span>
											<asp:TextBox ID="txt_Shift_II_SummerInrelaxation" runat="server" class="form-control input-sm col-sm custextbox" 
												onfocus="this.select();"   MaxLength="8" >
											</asp:TextBox>
											<span id="Span12" class="input-group-addon cusspan" runat="server">Out Relaxation </span>
											<asp:TextBox ID="txt_Shift_II_SummerOutrelaxation" runat="server" class="form-control input-sm col-sm custextbox"
												 onfocus="this.select();"   MaxLength="8" >
											</asp:TextBox>
										</div>
									</div>
								
			 						</div>
									<h5 style=" padding-right:121px;background-color:#33aa99;text-align:center;width:100%";font-family: 'Cabin', sans-serif;  font-size: 15px;" >
										<span id="spamchk" runat="server" style="float:left;margin-top:-4px !important;">Same as Summer
											 <asp:CheckBox ID="checksame" runat="server" OnCheckedChanged="checksame_CheckedChanged" AutoPostBack="true" /> </span>Winter
									</h5>
								<div class="row" id="divwinter1" runat="server">
									<div class="col-sm-5" >
										<div class="form-group input-group">
											<span id="Span3" class="input-group-addon cusspan" runat="server"> Start Time <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"  ID="txt_Shift_I_WinterStartTime" 
												onfocus="this.select();"   MaxLength="8"></asp:TextBox>
										<span id="Span4" class="input-group-addon cusspan" runat="server">  End Time <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"  ID="txt_Shift_I_WinterEndTime"
												 onfocus="this.select();"   MaxLength="8"></asp:TextBox>	
										</div>
									</div>
													
									<div class="col-sm-2">
										<div class="form-group input-group">
											<span id="Span2" class="input-group-addon cusspan" runat="server" style="min-width:20px !important;">Next Day <span
												style="color: red">*</span></span>
											<asp:DropDownList ID="ddl_Shift_I_WinterNextDay" runat="server" Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
												<asp:ListItem Text="No" Value="0"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>										
									<div class="col-sm-5">
										<div class="form-group input-group">
											<span id="Span5" class="input-group-addon cusspan" runat="server">In Relaxation </span>
											<asp:TextBox ID="txt_Shift_I_WinterInrelaxation" runat="server" class="form-control input-sm col-sm custextbox"
												 onfocus="this.select();"  MaxLength="8">
											</asp:TextBox>
											<span id="Span6" class="input-group-addon cusspan" runat="server">Out Relaxation </span>
											<asp:TextBox ID="txt_Shift_I_WinterOutrelaxtion" runat="server" class="form-control input-sm col-sm custextbox" 
												onfocus="this.select();"   MaxLength="8">
											</asp:TextBox>
										</div>
									</div>
									
			 						
								</div>
								<div class="row" id="divwinter2" runat="server">
									<div class="col-sm-5">
										<div class="form-group input-group">
											<span id="Span13" class="input-group-addon cusspan" runat="server"> Start Time <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txt_Shift_II_WinterStartTime" 
												onfocus="this.select();"   MaxLength="8"></asp:TextBox>
										<span id="Span14" class="input-group-addon cusspan" runat="server">  End Time <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"  ID="txt_Shift_II_WinterEndTime"
												 onfocus="this.select();"   MaxLength="8"></asp:TextBox>	
										</div>
									</div>
													
									<div class="col-sm-2">
										<div class="form-group input-group">
											<span id="Span15" class="input-group-addon cusspan" runat="server" style="min-width:20px !important;">Next Day <span
												style="color: red">*</span></span>
											<asp:DropDownList ID="ddl_Shift_II_WinterNextDay" runat="server" Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Yes" Value="1"></asp:ListItem>
												<asp:ListItem Text="No" Value="0"></asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>										
									<div class="col-sm-5">
										<div class="form-group input-group">
											<span id="Span16" class="input-group-addon cusspan" runat="server">In Relaxation </span>
											<asp:TextBox ID="txt_Shift_II_WinterInrelaxation" runat="server" class="form-control input-sm col-sm custextbox"
												 onfocus="this.select();"   MaxLength="8">
											</asp:TextBox>
											<span id="Span17" class="input-group-addon cusspan" runat="server">Out Relaxation </span>
											<asp:TextBox ID="txt_Shift_II_WinterOutrelaxtion" runat="server" class="form-control input-sm col-sm custextbox"
												 onfocus="this.select();"   MaxLength="8">
											</asp:TextBox>
										</div>
									</div>
									
									
								</div>
								
								<div class="row">	
									<div class="col-sm-12">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn"
												OnClientClick="javascript: return confirm('Are you sure to save ?');" Text="Save" OnClick="btnsave_Click" />
											<asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Add New" OnClick="btnreset_Click"/>

										</div>
									</div>
								</div>
								

							</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:TabPanel>
				<asp:TabPanel ID="tabrosterlist" runat="server" CssClass="Tab2" HeaderText="Roster List">
                    <ContentTemplate>
						<asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch_II">
							<div class="custab-panel" id="Div1" >
								<div class="fixeddiv">
									<div class="row fixeddiv" id="div2" runat="server">
										<asp:Label ID="lblmessage_II" runat="server"></asp:Label>
									</div>
								</div>
						<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span21" class="input-group-addon cusspan" runat="server">Roster </span>
											<asp:DropDownList ID="ddl_roster_II" runat="server" class="form-control input-sm col-sm custextbox">
											</asp:DropDownList>
										</div>
									</div>
									
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span23" class="input-group-addon cusspan" runat="server"> Shift Type <span
												style="color: red">*</span></span>
											<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
												ID="ddl_shifttype_II" >
												
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span24" class="input-group-addon cusspan" runat="server">Status </span>
											<asp:DropDownList ID="ddl_status_II" runat="server" Class="form-control input-sm col-sm custextbox">
												<asp:ListItem Text="Active" Value="1"></asp:ListItem>
												<asp:ListItem Text="InActive" Value="0"></asp:ListItem>
										</asp:DropDownList>
										</div>
									</div>
							<div class="col-sm-3">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsearch_II" runat="server" Class="btn  btn-sm cusbtn"
												 Text="Search" OnClick="btnsearch_II_Click" />
											<asp:Button ID="btnreset_II" runat="server" Class="btn  btn-sm cusbtn" Text="Refresh" OnClick="btnreset_II_Click" />

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
                                                <div class="grid" style="float: left; width: 100%; height: 60vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvRosterList" runat="server" ShowFooter="true" CssClass="table-hover grid_table result-table" 
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"  OnRowCommand="GvRosterList_RowCommand" 
														OnRowDataBound="GvRosterList_RowDataBound" Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Code
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcode" runat="server" Text='<%# Eval("Rostercode")%>' style="text-align:justify;">

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.01%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Roster
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
																	<asp:Label ID="lblID" runat="server" class="hidden" Text='<%# Eval("RosterID")%>'></asp:Label>
                                                                    <asp:Label ID="lblShift" runat="server" Text='<%# Eval("ShiftType")%>' style="text-align:justify;">

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1.4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Start-Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSummerstart" runat="server" Text='<%# Eval("Summer_Start")%>' style="text-align:left;">

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.7%" />
                                                            </asp:TemplateField>
															
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     End-Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsummerend" runat="server" Text='<%# Eval("Summer_End")%>' style="text-align:justify;">

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.7%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Next Day
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsummernextday" runat="server" Text='<%# Eval("SummerNext")%>' style="text-align:justify;">

                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
														<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     In-relaxation
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsummerinrelaxation" runat="server" Text='<%# Eval("Summer_Inrelaxation")%>' 
																		style="text-align:justify;">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.5%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Out-relaxation
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsummeroutrelaxation" runat="server" Text='<%# Eval("Summer_Outrelaxation")%>'
																		 style="text-align:justify;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.5%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Start-Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWinterstart" runat="server" Text='<%# Eval("Winter_Start")%>'
																		style="text-align:justify;" ></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.7%" />
                                                            </asp:TemplateField>
															
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     End-Time
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwinterend" runat="server" Text='<%# Eval("Winter_End")%>' 
																		style="text-align:justify;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.7%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Next Day
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwinternextday" runat="server" Text='<%# Eval("WinterNext")%>' 
																		style="text-align:left;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
															
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     In-relaxation
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwinterinrelaxation" runat="server" Text='<%# Eval("Winter_Inrelaxation")%>' 
																		style="text-align:justify;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.5%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Out-relaxation
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwinteroutrelaxation" runat="server" Text='<%# Eval("Winter_Outrelaxation")%>'
																		 style="text-align:justify;"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4.5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" 
																		CommandName="Edits" ForeColor="Blue">
																		  <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <span class="cus-Delete-header">Delete</span>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                               <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes"
																				 CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                                OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                       <i class="fa fa-trash-o cus-delete-color"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                              </asp:TemplateField>
                                                        </Columns>
                                                           <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
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
