<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LeaveApproval.aspx.cs" Inherits="Mediqura.Web.MedHR.LeaveApproval" %>

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

	</script>
	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:TabContainer ID="tabcontainerLeave" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabLeaveMST" runat="server" CssClass="Tab2" HeaderText="Leave Approval">
					<ContentTemplate>
						<asp:Panel ID="panel2" runat="server" DefaultButton="btnsearch">
							<div class="custab-panel" id="panelleaveMST">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="div1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-6">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Employee</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
												ID="txt_employeeDetails" MaxLength="100" onfocus="this.select();"></asp:TextBox>
											<asp:FilteredTextBoxExtender TargetControlID="txt_employeeDetails" ID="FilteredTextBoxExtender1"
												runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
												ValidChars=" ():|" Enabled="True">
											</asp:FilteredTextBoxExtender>
											<asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
												ServiceMethod="GetEmployeeDetails" MinimumPrefixLength="1"
												CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_employeeDetails"
												UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList"
												CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
											</asp:AutoCompleteExtender>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span2" class="input-group-addon cusspan" runat="server">Leave Type</span>
											<asp:DropDownList ID="ddlleavetype" runat="server" class="form-control input-sm col-sm custextbox">
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span6" class="input-group-addon cusspan" runat="server">Leave Category</span>
											<asp:DropDownList ID="ddlleavecategory" runat="server" class="form-control input-sm col-sm custextbox">
												<asp:ListItem Value="0">--Select--</asp:ListItem>
												<asp:ListItem Value="1">Full Day</asp:ListItem>
												<asp:ListItem Value="2">Half Day</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>

								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txtdatefrom" AutoPostBack="true"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
												TargetControlID="txtdatefrom" />
											<asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />

										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span5" class="input-group-addon cusspan" runat="server">Date To</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txtto" AutoPostBack="true"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
												TargetControlID="txtto" />
											<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span3" class="input-group-addon cusspan" runat="server">Status</span>
											<asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
												<asp:ListItem Value="2" >Pending</asp:ListItem>
												<asp:ListItem Value="1" >Approved</asp:ListItem>
												<asp:ListItem Value="3" >Rejected</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group cuspanelbtngrp  pull-right">

											<asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
											<asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
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
														EmptyDataText="No record found..." AutoGenerateColumns="False" 
														OnRowDataBound="GvLeave_RowDataBound" OnRowCommand="GvLeave_RowCommand" 
														Width="100%" HorizontalAlign="Center">
														<Columns>
															<asp:TemplateField visible="false">
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                     <asp:Label ID="lblleaveRecordID"
																		  Style="text-align: left !important;" runat="server" visible="false"
																		  Text='<%# Eval("LeaveRecordID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Employee                                                   
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblrequestemp" Style="text-align: left !important;" runat="server"
																		 Text='<%# Eval("EmpName") %>'></asp:Label>
																	<asp:Label ID="lblrequestempid" visible="false" runat="server"
																		 Text='<%# Eval("EmployeeID") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="6%" />
															</asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Date                                                   
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
																	<span runat="server" id="span100" style="text-decoration:underline;color:black">From : </span>
                                                                    <asp:Label ID="lblLeavedate"  runat="server" Text='<%# Eval("LeaveStartDate")%>'
																		 Style="text-align:left !important; "></asp:Label><br/>
                                                                   <span runat="server" id="span7" style="text-decoration:underline;color:black">To : </span>
																	<asp:Label ID="lblLeaveenddate"  runat="server" 
																		Text='<%# Eval("LeaveEndDate")%>' Style="text-align:left !important; "></asp:Label><br />
																	<span runat="server" id="span8" style="text-decoration:underline;color:black">No of Days : </span>
																	<asp:Label ID="lblnoofdays" Style="text-align: left !important;" runat="server"
																		 Text='<%# Eval("Noofdays") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                            </asp:TemplateField>
															
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Leave                                                     
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                          <asp:Label ID="lblleavetype" Style="text-align: left !important;" runat="server"
															   Text='<%# Eval("Leavetype") %>'></asp:Label><br />
																	<span runat="server" id="span1111">(</span>
																	<asp:Label ID="lblleavecategory" Style="text-align: left !important; color:black;" runat="server"
                                                                        Text='<%# Eval("Leavecategory") %>'></asp:Label><span runat="server" id="span10">)</span>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Send To
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsentto" 
																		Style="text-align: left !important; text-decoration:underline; color:black;" runat="server"
                                                                        Text='<%# Eval("Department") %>'></asp:Label><br />
																	<span runat="server" id="span111" >CC :</span>
																	<asp:Label ID="lblcc" Style="text-align: left !important; " runat="server"
                                                                        Text='<%# Eval("CC_Tooltips") %>' ToolTip='<%# Eval("CC_TO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                            </asp:TemplateField>
															
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleaveaction" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Leavestatus") %>'></asp:Label>
                                                                    <asp:Label ID="lblLeaveStatus" visible="false" runat="server"
                                                                        Text='<%# Eval("Status") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Reason
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleavereason" 
																		Style="text-align: left !important; word-break:break-all;word-wrap:break-word; " runat="server"
                                                                        Text='<%# Eval("Reason_Tooltips") %>' ToolTip='<%# Eval("Leavereason") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleaveapprover" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("LeaveApproverName") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>

															<asp:TemplateField>
																<HeaderTemplate>
																	Remarks
                                                                
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblactionremarks" Width="100%" runat="server" 
																		Text='<%# Eval("ApprovedRemarks_Tooltips")%>' ToolTip='<%# Eval("ApproverRemarks")%>'>>
																	</asp:Label>
																	<asp:TextBox ID="txtremarks" Width="100%" Height="100%" runat="server" visible="false"></asp:TextBox>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="10%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	<span class="cus-Edit-header">Edit</span>
																</HeaderTemplate>
																<ItemTemplate>
																	 <asp:Label ID="lbldisableEdit" runat="server" Visible="false">
																		  <i class="fa fa-edit  cus-edit-color"></i></asp:Label> 
																	<asp:LinkButton ID="lnkEdit" runat="server"
																		 CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                <i class="fa fa-edit  cus-edit-color"></i>
																	</asp:LinkButton>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	<span class="cus-Delete-header">Reject</span>
																</HeaderTemplate>
																<ItemTemplate>
																	 <asp:Label ID="lbldisableDelete" runat="server" Visible="false"> 
																		 <i class="fa fa-calendar-times-o cus-delete-color"></i></asp:Label> 
																	<asp:LinkButton ID="lnkreject" runat="server" CommandName="Reject" 
																		CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
																		OnClientClick="javascript: return confirm('Are you sure to reject the application ?');">
                                                                       <i class="fa fa-calendar-times-o  cus-delete-color"></i>
																	</asp:LinkButton>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	<span class="cus-Delete-header">Forward</span>
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbldisableforward" runat="server" Visible="false">  
																		<i class="fa fa-sign-out cus-delete-color" style="color:blue;"></i></asp:Label> 
																	<asp:LinkButton ID="lnkforward" runat="server" CommandName="Forward" 
																		CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
																		OnClientClick="javascript: return confirm('Are you sure to forward the application ?');">
                                                                       <i class="fa fa-sign-out cus-delete-color" style="color:blue;"></i>
																	</asp:LinkButton>
																	
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	<span class="cus-Delete-header">Approve</span>
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:LinkButton ID="lnkadjust" runat="server" CommandName="Adjust" visible="false"
																		 CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                       <i class="fa fa-clone cus-edit-color" style="color:orange "></i>
																	</asp:LinkButton>
																	<asp:LinkButton ID="lnkapproved" runat="server" CommandName="Approved" 
																		CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
																		OnClientClick="javascript: return confirm('Are you sure to approve the application ?');">
                                                                       <i class="fa fa-calendar-check-o cus-delete-color" style="color:green "></i>
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
								</div>
						</asp:Panel>
					</ContentTemplate>
				</asp:TabPanel>
				<asp:TabPanel ID="TabPanelleaveadjust" runat="server" CssClass="Tab2" HeaderText="Leave Adjust">
					<ContentTemplate>
						<asp:Panel ID="panel1" runat="server" >
							<div class="custab-panel" id="Div2">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="tap2div" runat="server">
										<asp:Label ID="tap2lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row cusrow pad-top ">
									<div class="col-sm-12">
										<div>

											<div class="pbody">
												<div class="grid" style="float: left; width: 100%; min-height:20vh; ">
													<asp:UpdateProgress ID="updateProgress3" runat="server">
														<ProgressTemplate>
															<div id="DIVloading" class="text-center loading" runat="server">
																<asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
																	AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
															</div>
														</ProgressTemplate>
													</asp:UpdateProgress>
													<asp:GridView ID="GvLeave2" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GvLeave2_RowDataBound"
														Width="100%" HorizontalAlign="Center">
														<Columns>
															<asp:TemplateField visible="false">
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                     <asp:Label ID="tap2lblleaveRecordID"
																		  Style="text-align: left !important;"  runat="server" visible="false"
																		  Text='<%# Eval("LeaveRecordID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Employee                                                   
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2lblrequestemp" Style="text-align: left !important;" runat="server"
																		 Text='<%# Eval("EmpName") %>'></asp:Label>
																	<asp:Label ID="tap2lblrequestempid" visible="false" runat="server"
																		 Text='<%# Eval("EmployeeID") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="6%" />
															</asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Date                                                   
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
																	<span runat="server" id="tap2span100" style="text-decoration:underline;color:black">From : </span>
                                                                    <asp:Label ID="tap2lblLeavedate"  runat="server" Text='<%# Eval("LeaveStartDate")%>'
																		 Style="text-align:left !important; "></asp:Label><br/>
                                                                   <span runat="server" id="tap2span7" style="text-decoration:underline;color:black">To : </span>
																	<asp:Label ID="tap2lblLeaveenddate"  runat="server" 
																		Text='<%# Eval("LeaveEndDate")%>' Style="text-align:left !important; "></asp:Label><br />
																	<span runat="server" id="tap2span8" style="text-decoration:underline;color:black">No of Days : </span>
																	<asp:Label ID="tap2lblnoofdays" Style="text-align: left !important;" runat="server"
																		 Text='<%# Eval("Noofdays") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                            </asp:TemplateField>
															
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Leave                                                     
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                          <asp:Label ID="tap2lblleavetype" Style="text-align: left !important;" runat="server"
															   Text='<%# Eval("Leavetype") %>'></asp:Label><br />
																	<span runat="server" id="span1111">(</span>
																	<asp:Label ID="tap2lblleavecategory" Style="text-align: left !important; color:black;" runat="server"
                                                                        Text='<%# Eval("Leavecategory") %>'></asp:Label><span runat="server" id="span10">)</span>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Send To
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="tap2lblsentto" 
																		Style="text-align: left !important; text-decoration:underline; color:black;" runat="server"
                                                                        Text='<%# Eval("Department") %>'></asp:Label><br />
																	<span runat="server" id="tap2span111" >CC :</span>
																	<asp:Label ID="tap2lblcc" Style="text-align: left !important; " runat="server"
                                                                        Text='<%# Eval("CC_Tooltips") %>' ToolTip='<%# Eval("CC_TO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
															
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="tap2lblleaveaction" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("Leavestatus") %>'></asp:Label>
                                                                    <asp:Label ID="tap2lblLeaveStatus" visible="false" runat="server"
                                                                        Text='<%# Eval("Status") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Reason
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="tap2lblleavereason" 
																		Style="text-align: left !important; word-break:break-all;word-wrap:break-word; " runat="server"
                                                                        Text='<%# Eval("Leavereason") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Remarks
                                                                
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2lblactionremarks" Width="100%" runat="server" 
																		Text='<%# Eval("ApprovedRemarks_Tooltips")%>' ToolTip='<%# Eval("ApproverRemarks")%>'>
																	</asp:Label>
																	</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="tap2lblleaveapprover" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("LeaveApproverName") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="tap2lblleaveapprovedate" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ApprovedDate") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>

														</Columns>
																												
													</asp:GridView>


												</div>
											</div>

										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="Span8" class="input-group-addon cusspan" runat="server">Adjust Mode</span>
											<asp:DropDownList ID="ddladjustment" runat="server" class="form-control input-sm col-sm custextbox"
												OnSelectedIndexChanged="ddladjustment_SelectedIndexChanged" AutoPostBack="true">				
											<asp:ListItem Value="0">--Select--</asp:ListItem>
												<asp:ListItem Value="1">Leave Type</asp:ListItem>
												<asp:ListItem Value="2">Leave Category</asp:ListItem>
												<asp:ListItem Value="3">Leave Status</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-3 hidden">
										<div >
											<asp:Label ID="tap2lblleaveRecordID" runat="server"></asp:Label> 
											<asp:Label ID="tap2lblrequestedemployeeID" runat="server"></asp:Label> 
										</div>
									</div>

								</div>

								<div class="row">
									<div class="col-sm-12">
										<div class="fixeddiv">
											<div class="row fixeddiv" id="div4" runat="server">
												<asp:Label ID="tap2result" runat="server" Height="13px"></asp:Label>
											</div>

										</div>
									</div>
								</div>
								<div class="row cusrow pad-top ">
									<div class="col-sm-12">
										<div>

											<div class="pbody">
												<div class="grid" style="float: left; width: 100%;" >
													<asp:UpdateProgress ID="updateProgress1" runat="server">
														<ProgressTemplate>
															<div id="DIVloading" class="text-center loading" runat="server">
																<asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
																	AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
															</div>
														</ProgressTemplate>
													</asp:UpdateProgress>
													<asp:GridView ID="GvLeave2result" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." AutoGenerateColumns="False"
														OnRowDataBound="GvLeave2result_RowDataBound"
														Width="100%" HorizontalAlign="Center">
														<Columns>
															<asp:TemplateField>
																<HeaderTemplate>
																	Sl.No
																</HeaderTemplate>
																<ItemTemplate>
																	<%# Container.DataItemIndex+1%>
																	<asp:Label ID="tap2resultlblleaveRecordID" Style="text-align: left !important;" runat="server"
																		 Visible="false" Text='<%# Eval("LeaveRecordID") %>'>
																		</asp:Label>
																		
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="0.1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Emp Name                                                   
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2resultlblEmpname" runat="server" Text='<%# Eval("EmpName")%>'
																		 Style="text-align: left !important;"></asp:Label>
																	<asp:Label ID="tap2resultlblempID" runat="server" Text='<%# Eval("LeaveEmployeeID")%>' 
																		visible="false" Style="text-align: left !important;"></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Date                                                   
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2resultlblLeavedate" runat="server" Text='<%# Eval("leavedate")%>'
																		Style="text-align: left !important;"></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Leave                                                     
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:DropDownList ID="tap2resultddlleavetype" runat="server"
																		class="form-control input-sm col-sm custextbox">
																	</asp:DropDownList>
																	<asp:Label ID="tap2resultlblleavetype" Style="text-align: left !important;" 
																		Visible="false" runat="server"
																		Text='<%# Eval("LeaveID") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Category
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:DropDownList ID="tap2resultddlleavecat" runat="server" 
																		class="form-control input-sm col-sm custextbox">
																		<asp:ListItem Value="0">--Select--</asp:ListItem>
																		<asp:ListItem Value="1">Full Day</asp:ListItem>
																		<asp:ListItem Value="2">Half Day</asp:ListItem>
																	</asp:DropDownList>
																	<asp:Label ID="tap2resultlblleavecategory" 
																		Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("LeaveCategoryID") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="2%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Status
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:DropDownList ID="tap2resultddllaction" runat="server" 
																		class="form-control input-sm col-sm custextbox"
																		OnSelectedIndexChanged="tap2resultddllaction_SelectedIndexChanged" AutoPostBack="true">
																		<asp:ListItem Value="1">Approved</asp:ListItem>
																		<asp:ListItem Value="3">Rejected</asp:ListItem>
																	</asp:DropDownList>
																	<asp:Label ID="tap2resultlblLeaveStatus" Visible="false" runat="server"
																		Text='<%# Eval("Status") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Remarks
                                                                
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2resultlblactionremarks" Width="200px" runat="server"
																		Text='<%# Eval("AdjustedRemarks")%>'></asp:Label>
																	
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>

															<asp:TemplateField>
																<HeaderTemplate>
																	Adjust By
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2resultlblleaveadjust" Style="text-align: left !important;" runat="server"
																		Text='<%# Eval("AdjustedBy") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Added on
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="tap2resultlbladjustdate" Style="text-align: left !important;" runat="server"
																		Text='<%# Eval("AdjustedDate") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="5%" />
															</asp:TemplateField>
														</Columns>

													</asp:GridView>


												</div>
											</div>

										</div>
									</div>
								</div>
								<div class="row">	
									<div class="col-sm-9">
										<div class="form-group input-group">
											<span id="Span9" class="input-group-addon cusspan" runat="server">Remark</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="tap2adjustremark" ></asp:TextBox>
											</div>
										</div>
									<div class="col-sm-3">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="tap2btnupdate" runat="server" Class="btn  btn-sm cusbtn"
												OnClientClick="javascript: return confirm('Are you sure to update ?');" Text="update" OnClick="tap2btnupdate_Click" />
											
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
