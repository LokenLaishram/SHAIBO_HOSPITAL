<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="CondemnRequest.aspx.cs" Inherits="Mediqura.Web.MedGenStore.CondemnRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function ItemChildGridview(input) {
            var displayIcon = "img" + input;
            if ($("#" + displayIcon).attr("src") == "../Images/plus.gif") {
                $("#" + displayIcon).closest("tr")
                    .after("<tr><td></td><td colspan = '100%'>" + $("#" + input)
                    .html() + "</td></tr>");
                $("#" + displayIcon).attr("src", "../Images/minus.gif");
            } else {
                $("#" + displayIcon).closest("tr").next().remove();
                $("#" + displayIcon).attr("src", "../Images/plus.gif");
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <style type="text/css">
                .gridViewHeader th {
                    background-color: #c4d3ee;
                }
            </style>
            <%-- End of Style --%>
            <%-- Style Child Gridview --%>
            <style type="text/css">
                .Grid th {
                    background-color: #DF7401;
                    color: White;
                    font-size: 10pt;
                    line-height: 200%;
                }

                .Grid td {
                    background-color: #F3E2A9;
                    color: black;
                    font-size: 10pt;
                    line-height: 200%;
                    text-align: center;
                }

                .ChildGrid th {
                    background-color: #F3E2A9;
                    color: Black;
                    font-size: 10pt;
                    border: 1px solid green;
                }

                .ChildGrid td {
                    background-color: #e2f5d9;
                    color: black;
                    font-size: 10pt;
                    text-align: center;
                    border: 1px solid green;
                }
            </style>
            <asp:TabContainer ID="tabcontainerGRN" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabstockGRN" runat="server" CssClass="Tab1" HeaderText="Condemn Request">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelGRN">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblsubstock" class="input-group-addon cusspan" runat="server">Substock <span
                                            style="color: red;">*</span></span>
                                        <asp:DropDownList ID="ddl_substock" runat="server" OnSelectedIndexChanged="ddl_substock_SelectedIndexChanged" AutoPostBack="True" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="lblitemname" class="input-group-addon cusspan" runat="server">Item Name <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="True"
                                            ID="txt_itemname" OnTextChanged="txt_itemname_TextChanged"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Visible="false"
                                            ID="txt_itemNametoAdd"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_itemname" ID="FilteredTextBoxExtender3"
                                            runat="server" FilterType="UppercaseLetters,Numbers,LowercaseLetters,Custom"
                                            FilterMode="ValidChars"
                                            ValidChars="-,#,:|  " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Avail <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_Avail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblqty" class="input-group-addon cusspan" runat="server">Qty <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" AutoPostBack="True" MaxLength="5" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_condemnqty" OnTextChanged="txt_condemnqty_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txt_condemnqty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblcondemndetails" class="input-group-addon cusspan" runat="server">Remark  <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="200" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_condemnremarks"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" OnClick="btnadd_Click" Class="btn  btn-sm scusbtn" />
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" Enabled="False" Width="150px" placeholder="   Condemn Number" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_CondemnRegNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Condemn Item List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-M">
                                                <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto;">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvsubstocklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."
                                                        Width="100%" HorizontalAlign="Center" OnRowCommand="gvsubstocklistt_RowCommand">
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
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_SubstockID" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_stockNo" Visible="false" Text='<%# Eval("StockNo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_condemnqty" runat="server" Text='<%# Eval("CondemnQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Condemn Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_condemnremark" runat="server" Text='<%# Eval("CondemnRemark")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                    <i class="fa fa-trash-o cus-delete-color"></i>
                                                                    </asp:LinkButton>
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
                            <div class="row">
                                <div class="col-md-5"></div>
                                <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Total Qty</span>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totalCondemnqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="text-align: right;">
                                    <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" Text="Save" Class="btn  btn-sm cusbtn"
                                        OnClick="btnsave_Click" />
                                    <asp:Button ID="btn_print" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClientClick="btnresets_Click" />
                                    <asp:Button ID="btnresets" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Condemn Request List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_substocks" class="input-group-addon cusspan" runat="server">SubStock </span>
                                        <asp:DropDownList runat="server" ID="ddl_substocks" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_condemnrequestno" class="input-group-addon cusspan" runat="server">Condemn Reqt No. </span>
                                        <asp:TextBox runat="server" AutoPostBack="true" MaxLength="5" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_condemnrequestno"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_condemnrequestno" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span37" class="input-group-addon cusspan" runat="server">Requested By</span>
                                        <asp:DropDownList ID="ddl_condemnrequestby" runat="server" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_status" class="input-group-addon cusspan" runat="server">Status </span>
                                        <asp:DropDownList runat="server" ID="ddl_Status" CssClass="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="InActive"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_requestfrom" class="input-group-addon cusspan" runat="server">Date From  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_requestfrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_requestfrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_requestfrom" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_requestto" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_requestTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_requestTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_requestTo" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Condemn Status </span>
                                        <asp:DropDownList runat="server" ID="ddlcondemnStatus" CssClass="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Request"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Verified"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Approved"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Rejected"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" Visible="true" Class="btn  btn-sm scusbtn" Text="Print" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btnreset_Click" />
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
                                        <%--<div class="pbody">--%>
                                        <div class="grid" style="float: left; width: 100%; overflow: auto;">
                                            <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gvcondemnsubstocklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." ShowFooter="true" OnRowCommand="gvcondemnsubstocklist_RowCommand"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Request No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                            <asp:Label ID="lbl_requestno" runat="server" Text='<%# Eval("CondemnRequestNo")%>'></asp:Label>
                                                            <asp:Label ID="lbl_substockid" Visible="false" Text='<%# Eval("SubStockID") %>' runat="server"></asp:Label>
                                                            <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Requested By
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_requestby" runat="server" Text='<%# Eval("CondemnRequestByName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Condemn Qty
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_totalcondemnqty" runat="server" Text='<%# Eval("TotalCondemnQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_grandtotalcondemnqty" runat="server" Text='<%# Eval("GrandTotalCondemnQty")%>'></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remark
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_condemremark" MaxLength="200" runat="server" Text='<%# Eval("CondemnRemark")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Request Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_requestdate" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Status
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_condemnStatus" runat="server" Text='<%# Eval("CondemnRequestStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Print
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintIndent_RegnDuplicate('<%# Eval("CondemnRequestNo")%>'); return false;">Print</a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <span class="cus-Delete-header">Delete</span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                               <i class="fa fa-trash-o cus-delete-color"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#e8e8e8" />
                                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px;" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
