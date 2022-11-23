<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PHRAccountTransaction.aspx.cs" Inherits="Mediqura.Web.MedPhr.PHRAccountTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<link href="../Styles/jquery.timepicker.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery.timepicker.min.js"></script>
	<script type="text/javascript">
		var Page
		function pageLoad() {


			var options = { 'timeFormat': 'h:i A', 'step': 15 }
			$("#<%=txttimepickerfrom.ClientID %>").timepicker(options);
			$("#<%=txttimepickerto.ClientID %>").timepicker(options);


		}
		</script>
	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:TabContainer ID="tabContainerAccountGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tabPanelAccountGroup" runat="server" HeaderText="Account Transaction">
					<ContentTemplate>
						<div class="custab-panel" id="depositdetaildiv">
							<div class="fixeddiv">
								<div class="row fixeddiv" id="div1" runat="server">
									<asp:Label ID="lblmessage" runat="server"></asp:Label>
								</div>
							</div>

							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span1" class="input-group-addon cusspan" runat="server">Payment Type </span>
										<asp:DropDownList ID="ddl_payment_type" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_payment_SelectedIndexChanged" >
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span3" class="input-group-addon cusspan" runat="server">Payment Mode </span>
										<asp:DropDownList ID="ddl_payment_mode" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_payment_mode_SelectedIndexChanged">
											<asp:ListItem Value="0">-Select-</asp:ListItem>
											<asp:ListItem Value="1">Cash</asp:ListItem>
											<asp:ListItem Value="2">Bank</asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span20" class="input-group-addon cusspan" runat="server">Acount </span>
										<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
											ID="ddl_ledger"></asp:DropDownList>

									</div>
								</div>
							</div>

							<div class="row pad-top">
								<asp:Panel runat="server" ID="pnl_bank" Visible="false">

									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span6" class="input-group-addon cusspan" runat="server">Instrument Name</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_instrument_name"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span7" class="input-group-addon cusspan" runat="server">Instrument Date</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_intrumentDate"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
												TargetControlID="txt_intrumentDate" />
											<asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_intrumentDate" />
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span5" class="input-group-addon cusspan" runat="server">Bank Payee Name</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_bank_payee_name"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span9" class="input-group-addon cusspan" runat="server">Bank Branch Name</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_bank_branch_name"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span10" class="input-group-addon cusspan" runat="server">Cross Inst</span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
												ID="txt_cross_inst"></asp:TextBox>
										</div>
									</div>

								</asp:Panel>
							</div>
							<div class="row">
							<div class="col-sm-4">
								<div class="form-group input-group">
									<span id="Span4" class="input-group-addon cusspan" runat="server">Amount</span>
									<asp:TextBox runat="server"  Class="form-control input-sm col-sm custextbox"
										ID="txt_amount"></asp:TextBox>
									<asp:FilteredTextBoxExtender runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="txt_amount" ></asp:FilteredTextBoxExtender>

								</div>
							</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span2" class="input-group-addon cusspan" runat="server">Voucher No.</span>
										<asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
											ID="txt_voucher"></asp:TextBox>

									</div>
								</div>
								</div>
							<div class="row">
							<div class="col-sm-12">
								<div class="form-group input-group">
									<span id="Span11" class="input-group-addon cusspan" runat="server">Naration</span>
									<asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
										ID="txt_naration"></asp:TextBox>
								</div>
							</div>
								</div>

							<div class="row">
								<div class="col-sm-12">
									<div class="col-lg-8">
									</div>

									<div class="col-sm-4">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" OnClick="btnsave_Click" />
											<asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" UseSubmitBehavior="False" />
											<asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnprints_Click" UseSubmitBehavior="False" Text="Print" />
										</div>
									</div>
								</div>
							</div>
						</div>
						</div>
					</ContentTemplate>
				</asp:TabPanel>
				<asp:TabPanel ID="tabPanel1" runat="server" HeaderText="Transaction List">
					<ContentTemplate>
						<div class="custab-panel" id="Div2">
							<div class="fixeddiv">
								<div class="row fixeddiv" id="div3" runat="server">
									<asp:Label ID="lblmessage2" runat="server"></asp:Label>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span8" class="input-group-addon cusspan" runat="server">Payment</span>
										<asp:DropDownList ID="ddl_transaction" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged ="ddl_transaction_SelectedIndexChanged">
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-8">
									<div class="form-group input-group">
										<span id="Span12" class="input-group-addon cusspan" runat="server">Account </span>
										<asp:DropDownList ID="ddl_ledgers" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ledgers_SelectedIndexChanged">
										</asp:DropDownList>
									</div>
								</div>

							</div>
							<div class="row">
								<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span14" class="input-group-addon cusspan" runat="server">Date From <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
												ID="txtdatefrom"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
												TargetControlID="txtdatefrom" />
											<asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
											<asp:TextBox ID="txttimepickerfrom" runat="server" onfocus="this.select();" class="time form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span15" class="input-group-addon cusspan" runat="server">Date To <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
												ID="txtto"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
												TargetControlID="txtto" />
											<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
											<asp:TextBox ID="txttimepickerto" runat="server" onfocus="this.select();" class="time form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
										</div>
									</div>
								<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span16" class="input-group-addon cusspan" runat="server">Account State</span>
										<asp:DropDownList ID="ddl_account_close" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_account_close_SelectedIndexChanged" AutoPostBack="True" >
											<asp:ListItem Value="0">Open</asp:ListItem>
											<asp:ListItem Value="1">Closed</asp:ListItem>
											<asp:ListItem Value="3">All</asp:ListItem>
										</asp:DropDownList>
									</div>
								</div>
								<div class="col-sm-12">
									<div class="form-group input-group cuspanelbtngrp  pull-right">
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
								
								<asp:Label ID="lbldescription" runat="server" class="SucessAlert" ></asp:Label>
									
								<div class="row cusrow pad-top ">
									<label class="gridview-label" id="lblgridincome" runat="server"  style="color:black;font-weight:bold;">Recieved(₹)</label>
									<div class="col-sm-12">
										<div>
											<div class="pbody">
												<div class="grid" style="float: left; width: 100%; ">
													<asp:UpdateProgress ID="updateProgress2" runat="server">
														<ProgressTemplate>
															<div id="DIVloading" class="text-center loading" runat="server">
																<asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
																	AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
															</div>
														</ProgressTemplate>
													</asp:UpdateProgress>
													<asp:GridView ID="Gv_incomereport" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..." 
														AutoGenerateColumns="False" 
														Width="100%" HorizontalAlign="Center">
														<Columns>
															<asp:TemplateField>
																<HeaderTemplate>
																	SlNo.
																</HeaderTemplate>
																<ItemTemplate>
																	  <%# (Container.DataItemIndex+1) %>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="2px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Voucher No
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomeVoucher" runat="server"
																		Text='<%# Eval("VoucherNo") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="5px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Bill No
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomebillno" runat="server"
																		Text='<%# Eval("BillNo") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="5px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Date
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomeDate" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("AddedDTM") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="10px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Particular
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomeParticulars" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("Particulars") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="195px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Payment Mode
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomePaymentMode" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("Payment") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="2px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	User
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomeEmpName" runat="server"
																		Text='<%# Eval("EmpName")%>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="6px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Amt(₹).
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblincomeTransactionAmount" Style="text-align: right !important;" runat="server"
																		Text='<%# Eval("amount","{0:0#.##}") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Right" Width="5px" />
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
												</div>
											</div>

										</div>
									</div>
								</div>
								<br />
								<div class="row cusrow pad-top ">
									<label class="gridview-label" id="lblgridexpenses" runat="server" style="color:black;font-weight:bold;">Payment(₹)</label>
									
									<div class="col-sm-12">
										<div>
											<div class="pbody">
												<div class="grid" style="float: left; width: 100%;">
													
													<asp:GridView ID="GV_expensesreport" runat="server" CssClass="table-hover grid_table result-table"
														EmptyDataText="No record found..."
														AutoGenerateColumns="False" 
														Width="100%" HorizontalAlign="Center">
														<Columns>
															<asp:TemplateField>
																<HeaderTemplate>
																	SlNo.
																</HeaderTemplate>
																<ItemTemplate>
																	<%# (Container.DataItemIndex+1) %>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="2px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Voucher No
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesVoucher" runat="server"
																		Text='<%# Eval("VoucherNo") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="5px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Bill No
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesbillno" runat="server"
																		Text='<%# Eval("BillNo") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Left" Width="5px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Date
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesDate" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("AddedDTM") %>'></asp:Label>

																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="10px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Particular
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesParticulars" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("Particulars") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="195px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Payment Mode
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesPaymentMode" Style="text-align: center !important;" runat="server"
																		Text='<%# Eval("Payment") %>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="2px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	User
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpensesEmpName" runat="server"
																		Text='<%# Eval("EmpName")%>'></asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Left" Width="6px" />
															</asp:TemplateField>
															<asp:TemplateField>
																<HeaderTemplate>
																	Amt(₹).
																</HeaderTemplate>
																<ItemTemplate>
																	<asp:Label ID="lblexpenseTransactionAmount" Style="text-align: right !important;" runat="server"
																		Text='<%# Eval("amount","{0:0#.##}") %>'></asp:Label>
																</ItemTemplate>

																<ItemStyle HorizontalAlign="Right" Width="5px" />
															</asp:TemplateField>
														</Columns>
														 
													</asp:GridView>
												</div>
											</div>

										</div>
									</div>
								</div>
								<br />
								<div class="row">
									<div class="col-sm-4">
										</div>
									
									<div  class="col-sm-4" id="cashincome" runat="server">
										<div class="form-group input-group  pull-right">
											<span id="Span13" class="input-group-addon cusspan" runat="server">Cash Recieved(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_CashIncome"></asp:TextBox>
										</div>
									</div>
									<div  class="col-sm-4" id="cashexpenses" runat="server" >
										<div class="form-group input-group  pull-right">
											<span id="Span17" class="input-group-addon cusspan" runat="server">Cash Payment(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_CashExpenses"></asp:TextBox>
										</div>
									</div>
										
									</div>
								<div class="row  ">
									<div class="col-sm-4">
										</div>
									
									<div  class="col-sm-4" id="bankincome" runat="server" >
                                        <div class="form-group input-group  pull-right">
                                            <span id="Span18" class="input-group-addon cusspan" runat="server">Bank Recieved(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_BankIncome"></asp:TextBox>
                                        </div>
                                    </div>
									<div  class="col-sm-4" id="bankexpenses" runat="server">
                                        <div class="form-group input-group  pull-right">
                                            <span id="Span19" class="input-group-addon cusspan" runat="server">Bank Payment(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_BankExpenses"></asp:TextBox>
                                        </div>
                                    </div>

									
									</div>
								
								<div class="row ">
									<div class="col-sm-4">
										</div>
									
									<div  class="col-sm-4" id="totalincome" runat="server">
										<div class="form-group input-group  pull-right">
											<span id="Span21" class="input-group-addon cusspan" runat="server">Total Recieved(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_TotalIncome"></asp:TextBox>
										</div>
									</div>
									<div  class="col-sm-4" id="totalexpenses" runat="server" >
										<div class="form-group input-group  pull-right">
											<span id="Span22" class="input-group-addon cusspan" runat="server">Total Payment(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_TotalExpenses"></asp:TextBox>
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
