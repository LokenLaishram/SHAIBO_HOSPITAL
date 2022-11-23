<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="OTBreakUp.aspx.cs" Inherits="Mediqura.Web.MedOT.OTBreakUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineritemgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabitemgroupmaster" runat="server" CssClass="Tab2" HeaderText="OT Share Verification">
                    <ContentTemplate>
                        <div class="custab-panel" id="paneldepartmentmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div6" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div class="col-sm-6">
                                        <asp:Panel runat="server" DefaultButton="btn_search">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group input-group">
                                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
                                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txtdatefrom"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                            TargetControlID="txtdatefrom" />
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group input-group">
                                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Date To  </span>
                                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                            ID="txtto"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                            TargetControlID="txtto" />
                                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div class="form-group input-group">
                                                        <span id="Span2" class="input-group-addon cusspan" runat="server" style="color: red">Name <span
                                                            style="color: red">*</span></span>
                                                        <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                                            ID="txt_patientname"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                            ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientname"
                                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                        </asp:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-10">
                                                    <div class="form-group input-group">
                                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Doctor </span>
                                                        <asp:DropDownList ID="ddl_doctor" Class="form-control input-sm col-sm custextbox" runat="server"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <div class="form-group input-group">
                                                        <asp:Button ID="btn_search" OnClick="btn_search_Click" runat="server" Text="Search" Class="btn  btn-sm scusbtn" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <div>
                                                        <div class="pbody">
                                                            <div class="gridview-container-Large">
                                                                <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                                                    <asp:GridView ID="Gv_CompletedOtlist" runat="server" CssClass="table-hover grid_table result-table"
                                                                        EmptyDataText="No record found..." OnRowDataBound="Gv_CompletedOtlist_RowDataBound" OnRowCommand="Gv_CompletedOtlist_RowCommand" AutoGenerateColumns="False"
                                                                        Width="100%" HorizontalAlign="Center">
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
                                                                                    OT number
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbl_otnumber" Font-Underline="true" ForeColor="Red" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("OTNo") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    IP No.
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_ipnumber" Style="text-align: left !important;" runat="server"
                                                                                        Text='<%# Eval("IPNo") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    Procedure
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_CaseID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                                        Text='<%# Eval("CaseID") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_procedure" Style="text-align: left !important;" runat="server"
                                                                                        Text='<%# Eval("CaseName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    Status
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_otstatus" Visible="false" runat="server" Text='<%# Eval("OTstatus") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_veridystatus" runat="server"></asp:Label>
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
                                        </asp:Panel>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group input-group">
                                                    <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">Name <span
                                                        style="color: red">*</span></span>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"
                                                        ID="txt_name" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-9">
                                                <div class="form-group input-group">
                                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Procedure <span
                                                        style="color: red">*</span></span>
                                                    <asp:TextBox runat="server" ID="txt_case" ReadOnly="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="35"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group input-group">
                                                    <asp:TextBox runat="server" ReadOnly="true" Placeholder="Amount" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_totalamount"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group input-group">
                                                    <span id="Span7" class="input-group-addon cusspan" runat="server">Total Surgeon (₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_totalsurgeon"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group input-group">
                                                    <span id="Span6" class="input-group-addon cusspan" runat="server">Total Anaesthestics (₹) </span>
                                                    <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_totalanasthesia"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div>
                                                    <div class="pbody">
                                                        <div class="gridview-container-Large">
                                                            <div class="grid" style="float: left; width: 100%; height: 52vh; overflow: auto">
                                                                <asp:GridView ID="GvOTBreakUp" runat="server" CssClass="table-hover grid_table result-table"
                                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                                    Width="100%" HorizontalAlign="Center">
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
                                                                                Name
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_CaseID" Visible="false" runat="server" Text='<%# Eval("CaseID") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_employeeID" Visible="false" runat="server" Text='<%# Eval("OTemployeeID") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_OTNo" Visible="false" runat="server" Text='<%# Eval("OTNo") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_IPno" Visible="false" runat="server" Text='<%# Eval("IPNo") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_name" Style="text-align: left !important;" runat="server"
                                                                                    Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Role
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_roleID" Visible="false" runat="server" Text='<%# Eval("OTroleID") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_role" runat="server" Text='<%# Eval("OTRole") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                Share Amount(₹) 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtamount" OnTextChanged="txt_share_TextChanged" Width="70px" runat="server" Text='<%# Eval("Amount","{0:0#.##}") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                                            <div class="col-sm-12">
                                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                    <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn"  OnClientClick="javascript: return confirm('Are you sure to verify ?');" Text="Save" OnClick="btn_save_Click" />
                                                    <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                                </div>
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
