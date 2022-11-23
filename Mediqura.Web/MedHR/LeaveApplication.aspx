<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="Mediqura.Web.MedHR.LeaveApplication" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
	<link href="../Scripts/bootstrap-multiselect.css" rel="stylesheet" />
	<script src="../Styles/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)
            $('[id*=ddl_leaveapprover]').multiselect({ includeSelectAllOption: false });

        }

        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()

            if (Page.get_isInAsyncPostBack()) {
                ddl_department_SelectedIndexChanged
                alert('One request is already in progress....')
                args.set_cancel(true)
            }
        }
        function show()
        {
        	$("#<%=ddl_leaveapprover.ClientID %>").visible = true;
        }
    </script>


    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <asp:TabContainer ID="tabcontainerLeaveAppMST" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabLeaveAppMST" runat="server" CssClass="Tab2" HeaderText="Leave Application">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panelleaveMST">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
									<div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">To</span>
                                            <asp:DropDownList ID="ddl_department" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="true"
												OnSelectedIndexChanged="ddl_department_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
									<div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">CC:</span>
										
											<asp:ListBox ID="ddl_leaveapprover" runat="server" SelectionMode="Multiple"  class="form-control input-sm col-sm custextbox dropdown"  >
                                            </asp:ListBox>
                                            
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">leave Type</span>
                                            <asp:DropDownList ID="ddlleavetype" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlleavetype_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
											 <asp:Label ID="lblleaverecord" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lbladvanceavailable" runat="server" Visible="false"></asp:Label>
                                             <asp:Label ID="lbleaveconsumed" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblmaxleavemonth" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblmaxleaveyear" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblhalfday" runat="server" Visible="false"></asp:Label>
											<asp:Label ID="lblleaverequestenable" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="lblleavecarriedforward" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
									<div class="col-sm-4 hidden" >
										 <div class="form-group input-group">
                                            <span id="Consumed" class="input-group-addon cusspan" runat="server">Consumed</span>
											 <asp:TextBox runat="server" ID="txt_leaveConsumed" class="form-control input-sm col-sm custextbox" readonly="true"  AutoPostBack="true" Style="color:red; font-weight:bold;">
												 </asp:TextBox>
											 <span id="Balanced" class="input-group-addon cusspan" runat="server">Balance</span>
											 <asp:TextBox runat="server" ID="txt_leavebalance" class="form-control input-sm col-sm custextbox" readonly="true" AutoPostBack="true" Style="color:blue; font-weight:bold;"></asp:TextBox>
											 </div>
										</div>
									</div>
								<div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="txtdatefrom_TextChanged"  
                                                ID="txtdatefrom" AutoPostBack="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />

                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Date To</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                               OnTextChanged="txtto_TextChanged" ID="txtto" AutoPostBack="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                                TargetControlID="txtto" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server" style="text-align:left">Leave Category</span>
                                            <asp:DropDownList ID="ddlleavecategory" runat="server" class="form-control input-sm col-sm custextbox" 
											OnSelectedIndexChanged="ddlleavecategory_SelectedIndexChanged" AutoPostBack="true"	style="min-width:80px !important">
                                                <asp:ListItem Value="1">Full Day</asp:ListItem>
												<asp:ListItem Value="2">Half Day</asp:ListItem>
                                            </asp:DropDownList>
											<span id="Span7" class="input-group-addon cusspan" runat="server" style="text-align:left;min-width: 94px  !important;">No. of Days</span>
											<asp:TextBox ID="txt_NoofDays" runat="server" AutoPostBack="true" ReadOnly="true" class="form-control input-sm col-sm custextbox" Style="color:red; font-weight:bold;"></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                </div>

                                <div class="row">
                                    <div class="col-lg-8">
                                        <div class="form-group input-group" style="height: 60px;">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Comments:<span style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="updatepanelreason" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" 
                                                ID="txt_reason" MaxLength="500" TextMode="MultiLine" Style=" min-height:60px; max-height:60px; min-width:500px; max-width:500px;"  OnTextChanged="txt_reason_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                        </div>
                                    </div>
                                    <div class="col-sm-4" runat="server" id="divrequest">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right" style="margin-top: 10%">
											
											 <asp:Button ID="btnsend" runat="server" Class="btn  btn-sm cusbtn" Text="Send Request" 
												OnClientClick="javascript: return confirm('Do you confirm to send request?');" OnClick="btnsend_Click" />
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click"/>
                                                
                                        </div>
                                    </div>
                                </div>
								<div class="row" runat="server" id="divapproved">
								<div class="col-sm-4">
                                        <div class="form-group input-group" >
										<span id="Span8" class="input-group-addon cusspan" runat="server">Remark:<span style="color: red">*</span></span>	
                                      <asp:TextBox ID="txt_leaveremark" runat="server" Visible="false" 
										  class="form-control input-sm col-sm custextbox" placeholder="Remark"></asp:TextBox>
											</div>
									</div>
									<div class="col-sm-4">
									<div class="form-group input-group cuspanelbtngrp  ">
											<asp:Button ID="btnapproved" runat="server" Class="btn  btn-sm cusbtn" Text="Approved " visible="false"
												OnClientClick="javascript: return confirm('Do you confirm to approved request?');" OnClick="btnapproved_Click" />
											<asp:Button ID="btnforward" runat="server" Class="btn  btn-sm cusbtn" Text="Forward " visible="false"
												OnClientClick="javascript: return confirm('Do you confirm to forward request?');" OnClick="btnforward_Click" />
										<asp:Button ID="btnredirect" runat="server" Class="btn  btn-sm cusbtn" Text="Reset " visible="false"
												 OnClick="btnredirect_Click" />
                                        </div>
										</div>
                                   
									</div>
                                <div class="row">
                                    <div class="col- sm-12">
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
                                            <h4 runat="server" id="hdrpastleave" style="color: black">Past Leave</h4>
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
													<div id="gridContainer">
                                                    <asp:GridView ID="GvLeave" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" 
														OnRowDataBound="GvLeave_RowDataBound" 
														OnRowCommand="GvLeave_RowCommand" 
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl.No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                     <asp:Label ID="lblleaveRecordID" Style="text-align: left !important;" runat="server" visible="false" Text='<%# Eval("LeaveRecordID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Start_Date                                                   
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLeavedate"  runat="server" Text='<%# Eval("LeaveStartDate")%>' Style="text-align:left !important; "></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
															<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    End_Date                                                   
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLeaveenddate"  runat="server" Text='<%# Eval("LeaveEndDate")%>' Style="text-align:left !important; "></asp:Label>
                                                                   
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
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
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                     Send To
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsentto" Style="text-align: left !important; text-decoration:underline; color:black;" runat="server"
                                                                        Text='<%# Eval("Department") %>'></asp:Label><br />
																	<span runat="server" id="span111" >CC :</span>
																	<asp:Label ID="lblcc" Style="text-align: left !important; " runat="server"
                                                                        Text='<%# Eval("CC_TO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
															
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleaveapproval" Style="text-align: center !important;" runat="server"
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
                                                                    <asp:Label ID="lblleavereason" Style="text-align: left !important; word-break:break-all;word-wrap:break-word; " runat="server"
                                                                        Text='<%# Eval("Leavereason") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblleaveapprover" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("LeaveApproverName") %>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblactionremarks"  runat="server" Text='<%# Eval("ApproverRemarks")%>'></asp:Label>
                                                                     <asp:TextBox ID="txtremarks"  class="form-control input-sm col-sm custextbox" 
																		  runat="server" Visible="false"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldisableEdit" runat="server" Visible="false">
																		 <i class="fa fa-pencil-square-o  cus-edit-color"></i></asp:Label> 
                                                                    <asp:LinkButton ID="lnkEdit" runat="server"
																		 CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>"
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
                                                                            <asp:Label ID="lbldisableDelete" runat="server" Visible="false">
																				 <i class="fa fa-trash-o cus-delete-color"></i></asp:Label> 
                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes"
																				 CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                                OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                       <i class="fa fa-trash-o cus-delete-color"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="0.1%" />
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
								</div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>

