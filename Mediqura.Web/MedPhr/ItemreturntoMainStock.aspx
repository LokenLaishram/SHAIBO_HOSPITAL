<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="ItemreturntoMainStock.aspx.cs" Inherits="Mediqura.Web.MedPhr.ItemreturntoMainStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<script type="text/javascript">
		function PrintReturn() {
			objReturnNo = document.getElementById("<%=txt_returnNo.ClientID %>")
		    window.open("../MedGenStore/Reports/ReportViewer.aspx?option=PhrItemReturnToMain&ReturnNo=" + objReturnNo.value)
        }
        function PrintDuplicateReturn(ReturnNo) {        	
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=PhrItemReturnToMain&ReturnNo=" + ReturnNo)
        }
        function StockReturnList() {
        	objRetrnNo = document.getElementById("<%=txt_returnNoList.ClientID %>")
        	objuser = document.getElementById("<%=ddl_user.ClientID %>")
        	objdatefrom = document.getElementById("<%=txt_from.ClientID %>")
        	objdateto = document.getElementById("<%=txt_To.ClientID %>")
        	objstock = document.getElementById("<%=ddl_substocklist.ClientID %>")
        	window.open("../MedGenStore/Reports/ReportViewer.aspx?option=StockReurnList&ReturnNo=" + objRetrnNo.value + "&user=" + objuser.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&StockID=" + objstock.value)
        }
	</script>
	<asp:TabContainer ID="tabcontainerpatient" runat="server" CssClass="Tab" ActiveTabIndex="0"
		Width="100%">
		<asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Item Return">
			<ContentTemplate>
				<div class="custab-panel" id="ipdservicerecorddetaildiv">
					<asp:UpdatePanel ID="UpdatePanel20" runat="server">
						<ContentTemplate>
							<div class="fixeddiv">
								<div class="row fixeddiv" id="divmsg1" runat="server">
									<asp:Label ID="lblmessage" runat="server"></asp:Label>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span10" class="input-group-addon cusspan" runat="server">Sub Stock<span
											style="color: red">*</span></span>
										<asp:DropDownList ID="ddl_substock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_substock_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-8">
									<div class="form-group input-group">
										<span id="Span16" class="input-group-addon cus-long-span" runat="server">Item Name <span
											style="color: red">*</span></span>
										<asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-span"
											ID="txtItemName" OnTextChanged="txtItemName_TextChanged" AutoPostBack="true"></asp:TextBox>
										<asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
											ServiceMethod="GetItemDetails" MinimumPrefixLength="1"
											CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtItemName"
											UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
										</asp:AutoCompleteExtender>
										<asp:Label ID="lblservicename" runat="server" Visible="false"></asp:Label>
									</div>
								</div>

							</div>
							<asp:Panel runat="server" ID="panel3" DefaultButton="btnadd">
								<div class="row">
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span2" class="input-group-addon cusspan" runat="server">Available</span>
											<asp:Label ID="lblItemname" runat="server" Visible="false"></asp:Label>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_available"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server" style="color: red">Quantity <span
												style="color: red">*</span> </span>

											<asp:TextBox runat="server" MaxLength="5" Class="form-control input-sm col-sm custextbox"
												ID="txtquantity"></asp:TextBox>
											<asp:FilteredTextBoxExtender TargetControlID="txtquantity" ID="FilteredTextBoxExtender7"
												runat="server" ValidChars="0123456789"
												Enabled="True">
											</asp:FilteredTextBoxExtender>
										</div>
									</div>
									<div class="col-sm-3">
										<div class="form-group input-group">
											<span id="lbl_RtnNo" class="input-group-addon cusspan" runat="server">Return Number</span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_returnNo"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-1">
										<div class="form-group input-group">
											<asp:Button ID="btnadd" UseSubmitBehavior="false" runat="server" Text="Add" Class="btn  btn-sm scusbtn" OnClick="btnadd_Click" />
										</div>
									</div>

								</div>
							</asp:Panel>
							<div class="row">
								<div class="col-sm-12">
									<div class="fixeddiv">
										<div class="row fixeddiv" id="div1" runat="server">
											<asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
										</div>
									</div>
								</div>
							</div>
							<div class="row cusrow pad-top ">
								<label class="gridview-label">Item List:</label>
								<div class="col-sm-12">
									<div>
										<div class="pbody">
											<div class="gridview-container-M">
												<div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
													<asp:UpdateProgress ID="updateProgress2" runat="server">
														<ProgressTemplate>
															<div id="DIVloading" class="text-center loading" runat="server">
																<asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
																	AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
															</div>
														</ProgressTemplate>
													</asp:UpdateProgress>
													<asp:GridView ID="gvStockReturn" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowCommand="gvStockReturn_RowCommand">
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
																	Item Name
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
																	<asp:Label ID="lblsubStockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
																	<asp:Label ID="lbl_itemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
																	<asp:Label ID="lbllabparticulars" Style="text-align: left !important;" runat="server"
																		Text='<%# Eval("ItemName") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="7%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Return Qty
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_return" runat="server" Text='<%# Eval("ReturnQty")%>'></asp:Label>
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
														<HeaderStyle BackColor="#D8EBF5" />
													</asp:GridView>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span14" class="input-group-addon cusspan" runat="server">Total Return Quantity</span>
										<asp:TextBox runat="server" ReadOnly="true" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
											ID="txt_totReturnQty"></asp:TextBox>
									</div>
								</div>

								<div class="col-sm-4">
									<div class="form-group input-group cuspanelbtngrp  pull-right">
										<asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Return" OnClick="btn_save_Click" />
										<asp:Button ID="btnprint" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClientClick="return PrintReturn();" />
										<asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
									</div>
								</div>
							</div>
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</ContentTemplate>
		</asp:TabPanel>
		<asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Return List">
			<ContentTemplate>
				<div class="custab-panel" id="Div2">
					<asp:UpdatePanel ID="UpdatePanel5" runat="server">
						<ContentTemplate>
							<div class="fixeddiv">
								<div class="row fixeddiv" id="divmsg2" runat="server">
									<asp:Label ID="lblmessage2" runat="server"></asp:Label>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span6" class="input-group-addon cusspan" runat="server">Sub Stock<span
											style="color: red">*</span></span>
										<asp:DropDownList ID="ddl_substocklist" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span12" class="input-group-addon cusspan" runat="server">Return No.</span>
										<asp:TextBox runat="server" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
											ID="txt_returnNoList"></asp:TextBox>
									</div>
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span13" class="input-group-addon cusspan" runat="server">Return By </span>

										<asp:DropDownList ID="ddl_user" runat="server" class="form-control input-sm col-sm custextbox">
										</asp:DropDownList>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
										<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
											ID="txt_from"></asp:TextBox>
										<asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
											TargetControlID="txt_from" />
										<asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
											CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
											CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
											Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_from" />
									</div>
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span5" class="input-group-addon cusspan" runat="server">Date To  </span>
										<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
											ID="txt_To"></asp:TextBox>
										<asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
											TargetControlID="txt_To" />
										<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
											CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
											CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
											Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_To" />
									</div>
								</div>
								<div class="col-sm-2">
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group cuspanelbtngrp  pull-right">
										<asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
										<asp:Button ID="btn_print" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm  cusbtn" Text="Print" OnClientClick="return StockReturnList();" />
										<asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
									</div>
								</div>

							</div>
							<div class="row">
								<div class="col-sm-12">
									<div class="fixeddiv">
										<div class="row fixeddiv" id="div3" runat="server">
											<asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
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
													<asp:GridView ID="gvreturnlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." AllowPaging="True" OnPageIndexChanging="gvreturnlist_PageIndexChanging" OnRowCommand="gvreturnlist_RowCommand"
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
																	Return No
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
																	<asp:Label ID="lbl_ReturnNo" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("ReturnNo") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Return Qty
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_TotRetnqty" runat="server" Style="text-align: center !important;" Text='<%# Eval("TotReturnQty")%>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Return Date
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_Returndate" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Return By
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lbl_returnBy" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("EmpName") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="3%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Remarks
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:TextBox ID="txtremarks" Width="200px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="1%" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Print
																</HeaderTemplate>
																<ItemTemplate>
																	<a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateReturn('<%# Eval("ReturnNo")%>'); return false;">Print</a>
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
														<PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
													</asp:GridView>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-3">
									<div class="form-group input-group">
										<span id="Span32" class="input-group-addon cusspan" runat="server">Total Return</span>
										<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
											ID="txt_TotreturnList"></asp:TextBox>
										<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_TotreturnList" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col-md-12">
									<asp:Button ID="btnexport" Visible="False" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
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
						</ContentTemplate>
					</asp:UpdatePanel>
				</div>
			</ContentTemplate>
		</asp:TabPanel>
	</asp:TabContainer>
</asp:Content>
