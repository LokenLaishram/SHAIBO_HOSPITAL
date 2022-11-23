<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="AccountMappingMaster.aspx.cs" Inherits="Mediqura.Web.MedAccount.AccountMappingMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerAccountGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPanelAccountGroup" runat="server" HeaderText="Account Mapping Master">
                    <ContentTemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Group Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_group_type" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_group_type_SelectedIndexChanged">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem Value="1">Common</asp:ListItem>
                                                <asp:ListItem Value="2">Investigation</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Service Type <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_servicetype" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicetype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Mapping Type <span
                                            style="color: red">*</span></span>
                                                <asp:DropDownList runat="server" ID="ddl_map_type" class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddl_map_type_SelectedIndexChanged">
                                                    <asp:ListItem value="0">-Select-</asp:ListItem>
                                                    <asp:ListItem value="1">Group Wise</asp:ListItem>
                                                    <asp:ListItem value="2">Service Wise</asp:ListItem>
                                                </asp:DropDownList>
                                          
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Sub Group  </span>
                                                <asp:DropDownList runat="server" ID="ddl_subservicetype" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                          
                                    </div>
                                </div>



                            </div>

                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnSearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnSearch_Click" />
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
                            <div class="row cusrow pad-top ">

                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <asp:HiddenField runat="server" ID="upHiddenPostback" />
                                                            <asp:GridView ID="GVMapping" runat="server" CssClass="table-hover grid_table result-table"
                                                                EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GVMapping_RowDataBound"
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
                                                                            Service Type
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblServiceTypeID" Visible="false" Text='<%# Eval("ServiceType") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblServiceTypeName" Text='<%# Eval("ServiceTypeName") %>' runat="server"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:Label runat="server">Name</asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMappingType" Visible="false" Text='<%# Eval("MappingType") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblGroupType" Visible="false" Text='<%# Eval("GroupType") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblsubServiceTypeID" Visible="false" Text='<%# Eval("SubServiceType") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblSubServiceName" Text='<%# Eval("SubServiceTypeName") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblServiceID" Visible="false" Text='<%# Eval("ServiceID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblServiceName" Text='<%# Eval("ServiceName") %>' runat="server"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Debit Account (To)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDebitID" Visible="false" Text='<%# Eval("DebitID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblDebitMapping" Visible="false" Text='<%# Eval("DebitMappingStatus") %>' runat="server"></asp:Label>
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                                ID="txt_debit_account" AutoPostBack="True" Text='<%# Eval("DebitAccountdata") %>' OnTextChanged="txt_debit_account_TextChanged"></asp:TextBox>
                                                                            <asp:AutoCompleteExtender ID="auto_debit_account" runat="server"
                                                                                ServiceMethod="GetAccountName" MinimumPrefixLength="1"
                                                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_debit_account"
                                                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                            </asp:AutoCompleteExtender>

                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Credit Account (By)
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCreditID" Visible="false" Text='<%# Eval("CreditID") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="lblCreditMapping" Visible="false" Text='<%# Eval("CreditMappingStatus") %>' runat="server"></asp:Label>
                                                                            
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                                ID="txt_credit_accnt" AutoPostBack="True" Text='<%# Eval("CreditAccountdata") %>' OnTextChanged="txt_credit_accnt_TextChanged"></asp:TextBox>
                                                                            <asp:AutoCompleteExtender ID="auto_credit_account" runat="server"
                                                                                ServiceMethod="GetAccountName" MinimumPrefixLength="1"
                                                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_credit_accnt"
                                                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                            </asp:AutoCompleteExtender>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle BackColor="#D8EBF5" />
                                                            </asp:GridView>

                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">


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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>

            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
