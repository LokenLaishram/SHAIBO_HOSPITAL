<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PhrItemLocationTracker.aspx.cs" Inherits="Mediqura.Web.MedStore.PhrItemLocationTracker" %>

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
            <asp:TabContainer ID="tabcontaineritemgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabitemgroupmaster" runat="server" CssClass="Tab2" HeaderText="PHR Item Location Tracker">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server">
                            <div class="custab-panel" id="panelitemgroupmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Store<span
                                                style="color: red">*</span></span>

                                            <asp:DropDownList ID="ddl_store" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_store_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Rack<span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddl_rack" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_rack_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Sub-Rack</span>
                                            <asp:DropDownList ID="ddl_subrack" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Item Search</span>
                                            <asp:TextBox ID="txt_itemname" runat="server" class="form-control input-sm col-sm custextbox">
                                            </asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemname"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Date From <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
                                                ID="txtdatefrom"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txtdatefrom" />
                                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Date To <span
                                                style="color: red">*</span> </span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox"
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
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvItemLocationType" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="true" PageSize="10"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GvItemLocationType_RowDataBound" OnPageIndexChanging="GvItemLocationType_PageIndexChanging"
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
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label Visible="false" ID="lblid" runat="server" Text='<%# Eval("StockID")%>'></asp:Label>
                                                                    <asp:Label Visible="false" ID="lbl_itemid" runat="server" Text='<%# Eval("ItemID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_item" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Batch No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_batch" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BatchNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Stock No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_stockno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("StockNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rack
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_RackID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("RackID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_racknumber" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("RackNumber") %>'></asp:Label>
                                                                    <asp:DropDownList ID="ddl_rack" Style="text-align: left !important;" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sub Rack
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SubRackID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("SubRackID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_subrack" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("SubRack") %>'></asp:Label>
                                                                    <asp:DropDownList ID="ddl_subrack" Style="text-align: left !important;" runat="server">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Location
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_itemlocation" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemLocation") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btn_save" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btn_save_Click" />
                                            <asp:Button ID="btn_reset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
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
