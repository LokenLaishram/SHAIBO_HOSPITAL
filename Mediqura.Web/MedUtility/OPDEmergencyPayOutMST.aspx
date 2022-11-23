<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="OPDEmergencyPayOutMST.aspx.cs" inherits="Mediqura.Web.MedUtility.OPDEmergencyPayOutMST" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
                <asp:TabPanel ID="tablabsubtestpmaster" runat="server" CssClass="Tab2" HeaderText="OPD Payout Master">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panellabsubtestmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span10" class="input-group-addon cusspan" runat="server">Service Group<span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_group" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_group_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Sub Group<span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_subgroup" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddl_subgroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Service Name</span>
                                            <asp:DropDownList ID="ddl_service" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Doctor Type <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" ID="ddl_doctortype" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_doctortype_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Department <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" ID="ddl_department" AutoPostBack="True" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_department_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Consultant <span
                                                style="color: red">*</span></span>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddl_doctor" AutoPostBack="true" OnSelectedIndexChanged="ddl_doctor_SelectedIndexChanged" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
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
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:GridView ID="GvOPDEmergency" runat="server" CssClass="table-hover grid_table result-table" EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center" OnRowDataBound="GvOPDEmergency_RowDataBound">
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
                                                                    Service Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_activate" Visible="false" runat="server" Text='<%# Eval("Activate")%>'></asp:Label>
                                                                    <asp:Label ID="lblDoctorID" Visible="false" runat="server" Text='<%# Eval("DoctorID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_serviceID" Visible="false" runat="server" Text='<%# Eval("ServiceID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_servicename" Style="text-align: left !important;" runat="server" Width="160px" Text='<%# Eval("ServiceName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="9%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtrate" Visible="true" runat="server" Style="text-align: left !important;" Width="130px" MaxLength="7"
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
                                                                    <asp:TextBox ID="txt_hosshare" Style="text-align: left !important;" Width="130px" runat="server" MaxLength="7" Text='<%# Eval("HospitalShare", "{0:0#.##}")%>'></asp:TextBox>
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
                                                                    <asp:TextBox ID="txt_consultshare" Style="text-align: left !important;" Width="130px" runat="server" MaxLength="7" Text='<%# Eval("ConsultantShare", "{0:0#.##}")%>'></asp:TextBox>
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
                                                                    Vaild From
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_validfrom" Style="text-align: left !important;" Width="100px" runat="server" MaxLength="3" Text='<%# Eval("Validfrom", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_validfrom" ID="FilteredTextBoxExtender7"
                                                                        runat="server" FilterType="Numbers"
                                                                        FilterMode="ValidChars"
                                                                        Enabled="True">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Vaild To
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_validto" Style="text-align: left !important;" Width="100px" runat="server" MaxLength="3" Text='<%# Eval("Validto", "{0:0#.##}")%>'></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender TargetControlID="txt_validto" ID="FilteredTextBoxExtender8"
                                                                        runat="server" FilterType="Numbers"
                                                                        FilterMode="ValidChars"
                                                                        Enabled="True">
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
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-5">
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                        </div>
                                    </div>
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
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
