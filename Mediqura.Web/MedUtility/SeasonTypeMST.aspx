<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="SeasonTypeMST.aspx.cs" Inherits="Mediqura.Web.MedUtility.SeasonTypeMST" %>

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
		<contenttemplate>
			<asp:TabContainer ID="tabcontainerSeasonMST" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabSeasonMST" runat="server" CssClass="Tab2" HeaderText="Season Master">
					<ContentTemplate>
						<asp:Panel ID="panel2" runat="server" DefaultButton="btnsave" >
							<div class="custab-panel" id="panelSeasonMST" >
								<div class="fixeddiv">
									<div class="row fixeddiv" id="div1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Season</span>
											<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
												ID="ddl_season"  ></asp:DropDownList>

										</div>
									</div>
									
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span2" class="input-group-addon cusspan" runat="server">Month</span>
											<asp:DropDownCheckBoxes runat="server" Class="form-control input-sm col-sm custextbox"
												ID="ddl_Month" RepeatDirection="Horizontal" UseSelectAllNode="true" RepeatLayout="Table" UseButtons="false">
												<Style DropDownBoxBoxHeight="200" />
											</asp:DropDownCheckBoxes>


										</div>
									</div>
										<div class="col-sm-4">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
											<asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
											<asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click"/>
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
													<asp:GridView ID="GvSeason" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GvSeason_RowDataBound"
														Width="100%" HorizontalAlign="Center" AllowPaging="true" PageSize="10" OnRowCommand="GvSeason_RowCommand">
														<Columns>
															<asp:TemplateField>
																<HeaderTemplate>
																	Sl.No
																</HeaderTemplate>
																<ItemTemplate>
																	<%# Container.DataItemIndex+1%>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Season                                                   
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblseasonID" Visible="false" runat="server" Text='<%# Eval("SeasonID")%>'></asp:Label>
																	<asp:Label ID="lbl_Seasoncode" Style="text-align: left !important;" runat="server" Text='<%# Eval("Season") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	January
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_january" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("January") %>'></asp:Label>
																	<asp:Label ID="janstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	February
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_february" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("February") %>'></asp:Label>
																	<asp:Label ID="febstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	March
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_march" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("March") %>'></asp:Label>
																	<asp:Label ID="marstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	April
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_april" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("April") %>'></asp:Label>
																	<asp:Label ID="aprilstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	May
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_may" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("May") %>'></asp:Label>
																	<asp:Label ID="maystatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	June
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_june" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("June") %>'></asp:Label>
																	<asp:Label ID="junestatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	July
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_july" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("July") %>'></asp:Label>
																	<asp:Label ID="julystatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	August
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_august" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("August") %>'></asp:Label>
																	<asp:Label ID="augstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	September
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_september" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("September") %>'></asp:Label>
																	<asp:Label ID="septstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	October
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_october" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("October") %>'></asp:Label>
																	<asp:Label ID="octstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	November
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_november" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("November") %>'></asp:Label>
																	<asp:Label ID="novstatus" runat="server"></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	December
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_december" Style="text-align: left !important;" Visible="false" runat="server"
																		Text='<%# Eval("December") %>'></asp:Label>
																	<asp:Label ID="decstatus" runat="server"></asp:Label>
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
															
														</Columns>

														<PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />

														<PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
													</asp:GridView>


												</div>
											</div>

										</div>
									</div>
								</div>
								
						</asp:Panel>
					</ContentTemplate>
				</asp:TabPanel>
			</asp:TabContainer>
		</contenttemplate>
	</asp:UpdatePanel>
</asp:Content>
