<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LeaveTypeMaster.aspx.cs" Inherits="Mediqura.Web.MedHR.LeaveTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
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

	</script>
	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:TabContainer ID="tabcontainerLeaveMST" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabLeaveMST" runat="server" HeaderText="Leave Policy ">
					<ContentTemplate>
						<asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
							<div class="custab-panel" id="panelleaveMST">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="div1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Code</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
												ID="txt_Leavecode" MaxLength="2"></asp:TextBox>
											<asp:FilteredTextBoxExtender TargetControlID="txt_Leavecode" ID="FilteredTextBoxExtender1"
												runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
												ValidChars=" " Enabled="True">
											</asp:FilteredTextBoxExtender>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span2" class="input-group-addon cusspan" runat="server">Description</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_LeaveDescp" MaxLength="20"></asp:TextBox>
											<asp:FilteredTextBoxExtender TargetControlID="txt_LeaveDescp" ID="FilteredTextBoxExtender2"
												runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
												ValidChars=" " Enabled="True">
											</asp:FilteredTextBoxExtender>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span6" class="input-group-addon cusspan" runat="server">Max Leaves/Month</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_MaxLeaveMonth"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
												runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_MaxLeaveMonth">
											</asp:FilteredTextBoxExtender>


										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span4" class="input-group-addon cusspan" runat="server">Max Leaves/Year</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_MaxLeaveYear"></asp:TextBox>
											<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
												runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txt_MaxLeaveYear">
											</asp:FilteredTextBoxExtender>

										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span3" class="input-group-addon cusspan" runat="server">Status</span>
											<asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
												<asp:ListItem Value="0">Active</asp:ListItem>
												<asp:ListItem Value="1">Inactive</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="hidden  col-sm-1">
										<asp:DropDownCheckBoxes runat="server" Class="form-control input-sm col-sm custextbox"
											ID="ddl_designation" RepeatDirection="Horizontal" UseSelectAllNode="true" RepeatLayout="Table" UseButtons="false">
											<Style DropDownBoxBoxHeight="200" />
										</asp:DropDownCheckBoxes>
									</div>
									<div class="col-sm-8">
										<div class="form-group input-group cuspanelbtngrp  pull-right">

											<asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
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
													<asp:GridView ID="GvLeave" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVLeave_RowDataBound"
														Width="100%" HorizontalAlign="Center" OnPageIndexChanging="GvLeave_PageIndexChanging"
														OnRowCommand="GvLeave_RowCommand" AllowPaging="True">
														<Columns>

															<asp:TemplateField>
																<HeaderTemplate>
																	Leave Type                                                    
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="LeaveID" Visible="false" runat="server" Text='<%# Eval("LeaveID")%>'></asp:Label>
																	<asp:Label ID="lbl_leavecode" Style="text-align: left !important;" runat="server"
																		Text='<%# Eval("Leavedescp") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="6%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Max Leaves /Month
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_leavepermonth" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("MaxLeaveMonth") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Max Leaves /Year
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_leaveperyear" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("MaxLeaveYear") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>

																<HeaderTemplate>
																	Leave Eligibility
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:DropDownCheckBoxes runat="server" Class="form-control input-sm col-sm custextbox"
																		ID="ddl_Eligibility" RepeatDirection="Horizontal" UseSelectAllNode="true" RepeatLayout="Table" UseButtons="false">
																		<Style DropDownBoxBoxHeight="200" />
																	</asp:DropDownCheckBoxes>
																	<asp:Label ID="lbl_leaveeligible" Visible="false" runat="server"
																		Text='<%# Eval("LeaveEligible") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>
															<asp:TemplateField HeaderText="How to countDays" HeaderStyle-Wrap="true">

																<ItemTemplate>
																	<asp:DropDownList ID="ddlleavecountDay" Class="form-control input-sm col-sm custextbox" runat="server">
																		<asp:ListItem Text="Calender Day" Value="1"></asp:ListItem>
																		<asp:ListItem Text="Working Day" Enabled="false" Value="2"></asp:ListItem>
																	</asp:DropDownList>
																	<asp:Label ID="lbl_leavecounttype" Visible="false" runat="server"
																		Text='<%# Eval("LeaveCountID") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	 Is Combined with 
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:DropDownCheckBoxes runat="server" Class="form-control input-sm col-sm custextbox"
																		ID="ddl_Combined" RepeatDirection="Horizontal" UseSelectAllNode="true" RepeatLayout="Table" UseButtons="false">
																		<Style DropDownBoxBoxHeight="200" />
																	</asp:DropDownCheckBoxes>
																	<asp:Label ID="lbl_combinedleave" Visible="false" runat="server"
																		Text='<%# Eval("leavecombined") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="8%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Availed in Advance
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox ID="chekboxleaveavailadvance" runat="server" Style="margin-left: 50% !important;" />
																	<asp:Label ID="lbl_leaveavailadvance" Visible="false" runat="server"
																		Text='<%# Eval("LeaveAvailinAdvance") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Allow Half Day
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox ID="chekboxleaveHalfday" runat="server" Style="margin-left: 50% !important;" />
																	<asp:Label ID="lbl_leaveHalfday" Visible="false" runat="server"
																		Text='<%# Eval("leaveHalfday") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	IS carrried Forward
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox ID="chekboxleaveforward" runat="server" Style="margin-left: 50% !important;" />
																	<asp:Label ID="lbl_leaveforward" Visible="false" runat="server"
																		Text='<%# Eval("Leavecarriedforward") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Need Document
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:CheckBox ID="chekboxleavedocument" runat="server" Style="margin-left: 50%!important;" />
																	<asp:Label ID="lbl_leavedocument" Visible="false" runat="server"
																		Text='<%# Eval("LeaveDocument") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Remarks
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:TextBox ID="txtremarks" Width="200px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
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
																<ItemStyle HorizontalAlign="Left" Width="1%" />
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
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
														</Columns>
														<PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
														<PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
													</asp:GridView>
												</div>
											</div>

										</div>
									</div>
								</div>
								<div class="row">

									<div class="col-sm-12">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnupdate" runat="server" Text="Update" Class="btn  btn-sm cusbtn"
												OnClientClick="javascript: return confirm('Do you confirm to update?');"
												OnClick="btnupdate_Click" />
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

