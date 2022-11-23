<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="YearlyInvoice.aspx.cs" Inherits="Mediqura.Web.Reports.YearlyInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <style>
        .dash_bg {
            width: 100%;
            padding: 10px 17px;
            display: inline-block;
            background: #03a9f4;
            color: #fff;
            border: 1px solid #E6E9ED;
            -webkit-column-break-inside: avoid;
            -moz-column-break-inside: avoid;
            column-break-inside: avoid;
            opacity: 1;
            transition: all .2s ease;
        }

        .dash_bg_dark {
            width: 100%;
            padding: 10px 17px;
            display: inline-block;
            background: #009688;
            color: #fff;
            border: 1px solid #E6E9ED;
            -webkit-column-break-inside: avoid;
            -moz-column-break-inside: avoid;
            column-break-inside: avoid;
            opacity: 1;
            transition: all .2s ease;
        }

            .dash_bg_dark:hover {
                box-shadow: 0 30px 70px rgba(0,0,0,.2);
            }

        .num_size {
            position: relative;
            animation: num_size 2s;
            animation-iteration-count: 1;
            font-size: 2em;
        }

        @keyframes num_size {
            from {
                bottom: 100px;
            }

            to {
                bottom: 0px;
            }
        }
    </style>
    <h2 class="breadcumb_cus">Yearly Invoice Report</h2>
    <asp:UpdatePanel runat="server" ID="panleinvoice">
        <ContentTemplate>
            <asp:UpdateProgress ID="updateProgress1" runat="server">
                <ProgressTemplate>
                    <div id="DIVloading" runat="server" class="Pageloader">
                        <asp:Image ID="imgUpdateProgress" class="loaderStyle" Width="70px" ImageUrl="~/Images/ShorttermUntidyHamadryas-max-1mb.gif" runat="server"
                            AlternateText="Loading ..." ToolTip="Loading ..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="fixeddiv">
                <div class="row fixeddiv" id="divmsg" runat="server">
                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span1" class="input-group-addon cusspan" runat="server">Centre</span>
                        <asp:DropDownList runat="server" ID="ddl_centre" AutoPostBack="true" OnSelectedIndexChanged="ddl_centre_SelectedIndexChanged" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span6" class="input-group-addon cusspan" runat="server">Year <span
                            style="color: red">*</span></span>
                        <asp:DropDownList runat="server" ID="ddl_year" AutoPostBack="true" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span4" class="input-group-addon cusspan" runat="server">Date From </span>
                        <asp:TextBox runat="server" ID="txt_from" AutoPostBack="true" OnTextChanged="txt_from_TextChanged" class="form-control input-sm col-sm custextbox"> </asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                            TargetControlID="txt_from" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_from" />
                    </div>
                </div>
                 <div class="col-sm-3">
                    <div class="form-group input-group">
                        <span id="Span5" class="input-group-addon cusspan" runat="server">Date To</span>
                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txt_to_TextChanged" class="form-control input-sm col-sm custextbox" ID="txt_to"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                            TargetControlID="txt_to" />
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_to" />
                    </div>
                </div>
            </div>
            <div class="row">
        
                <div class="col-lg-3">
                    <div class="form-group input-group">
                        <span id="Span17" class="input-group-addon cusspan" runat="server">Show Entries </span>
                        <asp:DropDownList ID="ddl_show" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_show_SelectedIndexChanged" class="form-control input-sm col-sm custextbox">
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="1000">All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group input-group">
                        <asp:Button runat="server" ID="btn_search" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btn_search_Click" Text="Search" />
                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm scusbtn" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnresets_Click" Text="Reset" />
                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm scusbtn" OnClick="btnprint_Click" Text="Print" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-9">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="divmsg3" runat="server">
                            <asp:Label ID="lblresult" runat="server" Height="13px"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
           <div class="row">
                <div class="col-sm-9">
                    <div class="pbody">
                        <div class="grid" style="float: left; max-height: 65vh; width: 100%; overflow: auto">
                            <asp:GridView ID="Gvinvoicelist" runat="server" AllowPaging="true" AllowCustomPaging="true" OnPageIndexChanging="Gvinvoicelist_PageIndexChanging" CssClass="table-hover grid_table result-table grid"
                                EmptyDataText="No record found..."
                                AutoGenerateColumns="False"
                                Width="100%" HorizontalAlign="Center">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sl.No
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# (Container.DataItemIndex+1)+(Gvinvoicelist.PageIndex)*Gvinvoicelist.PageSize %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Patient Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_patientname" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Invoice No. 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_billno" Style="text-align: left !important;" runat="server"
                                                Text='<%# Eval("BillNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Invoice Date 
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_billdate" Style="text-align: left !important;" runat="server"
                                                Text='<%# Eval("InvoiceDate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Paid Amount
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_amount" Style="text-align: left !important;" runat="server"
                                                Text='<%# Eval("PaidAmount", "{0:0#.##}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            No. Test
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_test" Style="text-align: left !important;" runat="server"
                                                Text='<%# Eval("TestCount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Test Name
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_test" Style="text-align: left !important;" runat="server"
                                                Text='<%# Eval("TestName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Left" Height="1em" Width="2%" />

                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="row" style="height: 3vh">
                    </div>
                    <div class="row">
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-10">
                            <div class="dash_bg_dark">
                                <div class="x_content" style="height: 12vh; width: 100%">
                                    <asp:Label runat="server" Text="Total Invoice"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" CssClass=" num_size" ID="lbl_totalinvoice" Style="height: 8vh; width: 100%"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-10">
                            <div class="dash_bg_dark">
                                <div class="x_content" style="height: 12vh; width: 100%">
                                    <asp:Label runat="server" Text="Total Test"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" CssClass=" num_size" ID="lbl_totaltest" Style="height: 8vh; width: 100%"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>
                    <div class="row" runat="server" id="idnetamount">
                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-10">
                            <div class="dash_bg_dark">
                                <div class="x_content" style="height: 12vh; width: 100%">
                                    <asp:Label runat="server" Text="Net Amount"></asp:Label>
                                    <br />
                                    <asp:Label runat="server" CssClass=" num_size" ID="lbl_netamount" Style="height: 8vh; width: 100%"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1">
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
