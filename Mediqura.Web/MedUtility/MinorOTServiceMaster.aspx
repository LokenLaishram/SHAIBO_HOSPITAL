<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="MinorOTServiceMaster.aspx.cs" Inherits="Mediqura.Web.MedUtility.MinorOTServiceMaster" %>

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
            <asp:TabContainer ID="tabcontainerlabsubtestmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tablabsubtestpmaster" runat="server" CssClass="Tab2" HeaderText="Minor OT Service Master">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panellabsubtestmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Service Type<span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_servicetype" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlservicetype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Service Name</span>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="35"
                                                        ID="txtservice">
                                                    </asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                        ServiceMethod="GetServices" MinimumPrefixLength="1"
                                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtservice"
                                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </asp:AutoCompleteExtender>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Type <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" ID="ddl_patienttype" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">IPD</asp:ListItem>
                                                <asp:ListItem Value="2">OPD</asp:ListItem>
                                                <asp:ListItem Value="3">Emergency</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Bed Class(Room) <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" ID="ddl_BedClass" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="All the same"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:GridView ID="GvMinorOT" runat="server" CssClass="table-hover grid_table result-table" EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center" OnRowDataBound="GvMinorOT_RowDataBound">
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
                                                                    Code
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Style="text-align: left !important;" runat="server" Width="150px" Text='<%# Eval("ID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_code" Style="text-align: left !important;" runat="server" Width="150px" Text='<%# Eval("ServiceCode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Service Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_activate" Visible="false" runat="server" Text='<%# Eval("Activate")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_serviceID" Visible="false" runat="server" Text='<%# Eval("ServiceID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_servicename" Style="text-align: left !important;" runat="server" Width="160px" Text='<%# Eval("ServiceName")%>'></asp:Label>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Left" Width="7%" />
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
                                                                    Total Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrate" Visible="true" runat="server" Style="text-align: left !important;" Width="50px" MaxLength="7"
                                                                        Text='<%# Eval("ServiceCharge", "{0:0#.##}") %>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txtrate" ID="FilteredTextBoxExtender3"
                                                                        runat="server" FilterType="Custom,Numbers"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars=". " Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Staff Fund
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_stafffund" Style="text-align: left !important;" Width="50px" runat="server" MaxLength="7" Text='<%# Eval("StaffFund", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_stafffund" ID="FilteredTextBoxExtender2"
                                                                        runat="server" FilterType="Custom,Numbers"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars=". " Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Corpus Fund
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_corpusfund" Style="text-align: left !important;" Width="50px" runat="server" MaxLength="7" Text='<%# Eval("CorpusFund", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_corpusfund" ID="FilteredTextBoxExtender1"
                                                                        runat="server" FilterType="Custom,Numbers"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars=". " Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Hospital Share
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_hosshare" Style="text-align: left !important;" Width="50px" runat="server" MaxLength="7" Text='<%# Eval("HospitalShare", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_hosshare" ID="FilteredTextBoxExtender4"
                                                                        runat="server" FilterType="Custom,Numbers"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars=". " Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Consultant Share
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_consultshare" Style="text-align: left !important;" Width="50px" runat="server" MaxLength="7" Text='<%# Eval("ConsultantShare", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_consultshare" ID="FilteredTextBoxExtender5"
                                                                        runat="server" FilterType="Custom,Numbers"
                                                                        FilterMode="ValidChars"
                                                                        ValidChars=". " Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
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
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
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



