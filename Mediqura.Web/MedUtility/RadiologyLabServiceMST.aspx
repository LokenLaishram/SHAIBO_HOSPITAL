<%@ Page Title="Test Activation" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="RadiologyLabServiceMST.aspx.cs" Inherits="Mediqura.Web.MedUtility.RadiologyLabServiceMST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <h2 class="breadcumb_cus">Service Manager</h2>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fixeddiv">
                <div class="row fixeddiv" id="divmsg1" runat="server">
                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span10" class="input-group-addon cusspan" runat="server">Group</span>
                        <asp:DropDownList ID="ddl_labgroup" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labgroup_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span3" class="input-group-addon cusspan" runat="server">Sub-Group </span>
                        <asp:DropDownList runat="server" ID="ddl_labsubgroup" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_labsubgroup_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group input-group">
                        <span id="Span2" class="input-group-addon cusspan" runat="server">Test <span
                            style="color: red">*</span></span>
                        <asp:TextBox runat="server" ID="txt_test" AutoPostBack="true" OnTextChanged="txt_test_TextChanged" class="form-control input-sm col-sm custextbox"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                            ServiceMethod="GetServices" MinimumPrefixLength="1"
                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_test"
                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                        </asp:AutoCompleteExtender>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span5" class="input-group-addon cusspan" runat="server">Test Center <span
                            style="color: red">*</span></span>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddl_testcenter" OnSelectedIndexChanged="ddl_testcenter_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3" style="visibility: hidden">
                    <div class="form-group input-group">
                        <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Type </span>
                        <asp:DropDownList runat="server" ID="ddl_patienttype" class="form-control input-sm col-sm custextbox">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="2">OPD</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-2" style="visibility: hidden">
                    <div class="form-group input-group">
                        <span id="Span1" class="input-group-addon cusspan" runat="server">Bed Class(Room) </span>
                        <asp:DropDownList runat="server" ID="ddl_BedClass" class="form-control input-sm col-sm custextbox">
                            <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="1" Text="All the same"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                        <asp:Button ID="btn_add" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Add" OnClick="btn_add_Click" />
                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Search" OnClick="btnsearch_Click" />
                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Reset" OnClick="btnreset_Click" />
                    </div>
                </div>


            </div>
            <div class="row cusrow pad-top ">
                <div class="col-sm-12">
                    <div>
                        <div class="pbody">
                            <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                <asp:GridView ID="GvRadioLab" runat="server" CssClass="table-hover grid_table result-table" EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center" OnRowDataBound="GvOPDEmergency_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Sl.No
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Test Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_activate" Visible="false" runat="server" Text='<%# Eval("Activate")%>'></asp:Label>
                                                <asp:Label ID="Lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                <asp:Label ID="lbl_serviceID" Visible="false" runat="server" Text='<%# Eval("ServiceID")%>'></asp:Label>
                                                <asp:Label ID="lbltesceterID" Visible="false" runat="server" Text='<%# Eval("TestcenterID")%>'></asp:Label>
                                                <asp:Label ID="lbl_servicename" Style="text-align: left !important;" runat="server" Text='<%# Eval("ServiceName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Test Centre
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_testcentre" Style="text-align: left !important;" runat="server" Text='<%# Eval("TestCenterName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Rate
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtrate" Visible="true" runat="server" Style="text-align: left !important;" Width="70px" MaxLength="7"
                                                    Text='<%# Eval("ServiceCharge", "{0:0#.##}") %>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txtrate" ID="FilteredTextBoxExtender3"
                                                    runat="server" FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=". " Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Share Type
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sharetype" Visible="false" runat="server" Text='<%# Eval("ShareType")%>'></asp:Label>
                                                <asp:DropDownList ID="ddl_sharetype" runat="server">
                                                    <asp:ListItem Value="1" Text="--Select--"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PC"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Value"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Lab Share
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_hosshare" Style="text-align: left !important;" Width="70px" runat="server" MaxLength="7" Text='<%# Eval("HospitalShare", "{0:0#.##}")%>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_hosshare" ID="FilteredTextBoxExtender2"
                                                    runat="server" FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=". " Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Consultant Share
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_consultshare" Style="text-align: left !important;" MaxLength="7" Width="70px" runat="server" Text='<%# Eval("ConsultantShare", "{0:0#.##}")%>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_consultshare" ID="FilteredTextBoxExtender1"
                                                    runat="server" FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=". " Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Reporting Share
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_reportingshare" Style="text-align: left !important;" MaxLength="7" Width="70px" runat="server" Text='<%# Eval("Reportingshare", "{0:0#.##}")%>'></asp:TextBox>
                                                <asp:FilteredTextBoxExtender TargetControlID="txt_reportingshare" ID="FilteredTextBoxExtender8"
                                                    runat="server" FilterType="Custom,Numbers"
                                                    FilterMode="ValidChars"
                                                    ValidChars=". " Enabled="True">
                                                </asp:FilteredTextBoxExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Activate?
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chekbox" runat="server" onclick="Check_Click(this);" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                <div class="col-md-4">
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
                <div class="col-sm-5">
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                        <asp:Button ID="btnsave" runat="server" Visible="false" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Update" OnClick="btnsave_Click" />
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="updateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
