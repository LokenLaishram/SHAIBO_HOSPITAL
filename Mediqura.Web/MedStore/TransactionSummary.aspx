<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="TransactionSummary.aspx.cs" Inherits="Mediqura.Web.MedStore.TransactionSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<link href="../Styles/jquery.timepicker.min.css" rel="stylesheet" />
	<script src="../Scripts/jquery.timepicker.min.js"></script>
	<script type="text/javascript">
		var Page
		function pageLoad() {

			
			var options = { 'timeFormat': 'H:i:s', 'step': 1 }
			$("#<%=txttimepickerfrom.ClientID %>").timepicker(options);
			$("#<%=txttimepickerto.ClientID %>").timepicker(options);
			
			
		}
		</script>
	<asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<asp:TabContainer ID="reportcontainer" runat="server" CssClass="Tab" ActiveTabIndex="0"
				Width="100%">
				<asp:TabPanel ID="tab1" runat="server" CssClass="Tab2" HeaderText="Transaction Report">
					<ContentTemplate>
						<asp:Panel ID="panel2" runat="server">
							<div class="custab-panel" id="panellabgroupmaster">
								<div class="fixeddiv">
									<div class="row fixeddiv" id="divmsg1" runat="server">
										<asp:Label ID="lblmessage" runat="server"></asp:Label>
									</div>
								</div>
								<div class="row">

									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span1" class="input-group-addon cusspan" runat="server">Transaction</span>
											<asp:DropDownList ID="ddl_transaction" runat="server" class="form-control input-sm col-sm custextbox" 
												AutoPostBack="True" OnSelectedIndexChanged ="ddl_transaction_SelectedIndexChanged">
												
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
												style="color: red">*</span></span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
												ID="txtdatefrom"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
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
											<span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
												style="color: red">*</span> </span>
											<asp:TextBox runat="server" Class="form-control input-sm col-sm cusmidiumtxtbox6"
												ID="txtto"></asp:TextBox>
											<asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
												TargetControlID="txtto" />
											<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
												CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
												CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
												Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
											<asp:TextBox ID="txttimepickerto" runat="server" onfocus="this.select();" class="time form-control input-sm col-sm cusmidiumtxtbox3"></asp:TextBox>
										</div>
									</div>
									</div>
								<div class="row">
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span6" class="input-group-addon cusspan" runat="server">Collected By</span>
											<asp:DropDownList ID="ddl_collectedby" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_collectedby_SelectedIndexChanged" AutoPostBack="true">
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span12" class="input-group-addon cusspan" runat="server">Payment Mode</span>
											<asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_paymentmode_SelectedIndexChanged" AutoPostBack="True" >
												<asp:ListItem Value="0">All</asp:ListItem>
												<asp:ListItem Value="1">Cash</asp:ListItem>
												<asp:ListItem Value="3">Bank</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group">
											<span id="Span14" class="input-group-addon cusspan" runat="server">Account State</span>
											<asp:DropDownList ID="ddl_account_close" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_account_close_SelectedIndexChanged" AutoPostBack="True" >
												<asp:ListItem Value="0">Open</asp:ListItem>
												<asp:ListItem Value="1">Closed</asp:ListItem>
												<asp:ListItem Value="3">All</asp:ListItem>
											</asp:DropDownList>
										</div>
									</div>
								</div>
								<div class="row">
									<div class="col-sm-4">
									<div class="form-group input-group">
										<span id="Span20" class="input-group-addon cusspan" runat="server">Acount </span>
										<asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
											ID="ddl_ledger" AutoPostBack="true" OnSelectedIndexChanged="ddl_ledger_SelectedIndexChanged"></asp:DropDownList>

									</div>
								</div>
									<div class="col-sm-2"></div>
									<div class="col-sm-6">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnSample" Style="display: none" runat="server"/>
											<asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
											<asp:Button ID="btn_print" runat="server" Class="btn  btn-sm cusbtn" Text="Print" OnClick="btn_print_Click" />
											<asp:Button ID="btn_Summary" runat="server" Class="btn  btn-sm cusbtn" Text="Summary" OnClick="btn_Summary_Click" />
											<asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
										</div>
									</div>
								</div>
								<div class="row cusrow ">
									<div class="col-sm-6 col-sm-offset-3 ">

										<asp:ModalPopupExtender ID="MDResponse" runat="server" TargetControlID="btnSample" PopupControlID="popupwindow"
											BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="">
										</asp:ModalPopupExtender>
										<asp:Panel runat="server" ID="popupwindow" Style="display: none;">
											<div style="width: 100%" class="row">
												<div style="background-color: #ffffff; padding-top: 20px" class="col-sm-12">
													<div class="row">
														<div class="col-sm-12 text-center">
															<asp:Label ForeColor="Red" ID="lblAuthenticationMsg" runat="server"></asp:Label>
														</div>
													</div>
													<br />
													<div class="row">
														<div class="col-sm-8 col-sm-offset-2 text-center">
															<asp:Label ForeColor="Red" ID="lblResponse" runat="server">Authenticate User</asp:Label>
														</div>
													</div>
													<div class="row">
														<div class="col-sm-12 text-center">
															<asp:Image ImageUrl="../Images/fingerprint.jpg" Style="height: 200px; border: 1px solid #009d90;" ID="FPImage" runat="server" />
														</div>
													</div>
													<br />
													<div class="row">
														<div class="col-sm-8">
															<button class="btn btn-sm cusbtn">Close</button>
														</div>
														<div class="col-sm-6">
															<asp:Button ID="btnVerify" runat="server" Class="btn  btn-sm cusbtn" Text="Verify"  />
														</div>
													</div>


												</div>
											</div>
										</asp:Panel>
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
									<label class="gridview-label" id="lblgridincome" runat="server"  style="color:black;font-weight:bold;">Income(₹)</label>
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
																		Text='<%# Eval("PaymentMode") %>'></asp:Label>
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
									<label class="gridview-label" id="lblgridexpenses" runat="server" style="color:black;font-weight:bold;">Expenses(₹)</label>
									
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
																		Text='<%# Eval("PaymentMode") %>'></asp:Label>
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
											<span id="Span4" class="input-group-addon cusspan" runat="server">Cash Income(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_CashIncome"></asp:TextBox>
										</div>
									</div>
									<div  class="col-sm-4" id="cashexpenses" runat="server" >
										<div class="form-group input-group  pull-right">
											<span id="Span7" class="input-group-addon cusspan" runat="server">Cash Expenses(₹) </span>
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
                                            <span id="Span13" class="input-group-addon cusspan" runat="server">Bank Income(₹) </span>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_BankIncome"></asp:TextBox>
                                        </div>
                                    </div>
									<div  class="col-sm-4" id="bankexpenses" runat="server">
                                        <div class="form-group input-group  pull-right">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Bank Expenses(₹) </span>
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
											<span id="Span11" class="input-group-addon cusspan" runat="server">Total Income(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_TotalIncome"></asp:TextBox>
										</div>
									</div>
									<div  class="col-sm-4" id="totalexpenses" runat="server" >
										<div class="form-group input-group  pull-right">
											<span id="Span2" class="input-group-addon cusspan" runat="server">Total Expenses(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_TotalExpenses"></asp:TextBox>
										</div>
									</div>
										
								</div>
								
								<br />
								<div class="row ">
									<div class="col-sm-4">
										</div>
									
									<div class="col-sm-4" id="totalbalance" runat="server" >
										<div class="form-group input-group  pull-right">
											<span id="Span10" class="input-group-addon cusspan" runat="server">Balance(₹) </span>
											<asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
												ID="txt_Balance"></asp:TextBox>
										</div>
									</div>
									<div class="col-sm-4">
										<div class="form-group input-group cuspanelbtngrp  pull-right">
											<asp:Button ID="btnClosedAccount" runat="server" Class="btn  btn-danger" Text="Account Close" OnClick="btnClosedAccount_Click"  />
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
