<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="GenIndentApprovedHandover.aspx.cs" Inherits="Mediqura.Web.MedGenStore.GenIndentApprovedHandover" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">
        function PrintHandOverList() {
            objstock = document.getElementById("<%=ddl_stockList.ClientID %>")

            objApprv = document.getElementById("<%=ddl_approvedBy.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_from.ClientID %>")
            objdateto = document.getElementById("<%=txt_To.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=HandOverList&SubStock=" + objstock.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Approved=" + objApprv.value)
        }
        function PrintIndent_RegnDuplicate(indentno) {
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=Indent_RegnProfile&IndentNo=" + indentno)
        }
        function PrintIndent_Regn() {
            objindno = document.getElementById("<%=txt_indentNumber.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=Indent_RegnProfile&IndentNo=" + objindno.value)
        }
        function PrintIndentHndOv_Regn() {
            objIndentNo = document.getElementById("<%=hdnIndNo.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=Handover&IndentNo=" + objIndentNo.value)
        }

    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerIndent" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabindentRequest" runat="server" CssClass="Tab2" HeaderText="GEN Indent Approved">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    <asp:Label ID="lblIndNo" Visible="false" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_from"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_from" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_from" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Date To  <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_To"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_To" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_To" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Sub Stock</span>
                                        <asp:DropDownList ID="ddl_stock" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Status(Request) </span>
                                        <asp:HiddenField ID="hdnIndNo" runat="server" />
                                        <asp:DropDownList ID="ddl_requestType" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnitemid" runat="server" />

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Indent Status </span>
                                        <asp:DropDownList ID="ddlindentstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="1"> Request</asp:ListItem>
                                            <asp:ListItem Value="2"> Approved </asp:ListItem>
                                            <asp:ListItem Value="3"> Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm  cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Indent List</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container-M">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:GridView ID="gvIndentRequest" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvIndentRequest_RowDataBound" OnRowCommand="gvIndentRequest_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Indent No.</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" Text='<%# Eval("IndentNo") %>' CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" Width="1%" Font-Underline="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Indentno" Visible="false" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                    <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_qty" runat="server" Style="text-align: center !important;" Text='<%# Eval("TotalRqty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_approvedqty" runat="server" Text='<%# Eval("ApprovedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sub Stock
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_SubstcokID" Visible="false" runat="server" Style="text-align: center !important;" Text='<%# Eval("GenStockID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_substock" runat="server" Style="text-align: center !important;" Text='<%# Eval("StockTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Indentdate" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AddedBy" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("RecdBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent Type
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReqTypestatus" Visible="true" runat="server" Text='<%# Eval("RequestStat") %>'></asp:Label>
                                                                    <asp:Label ID="lbllReqTypeID" Visible="false" runat="server" Text='<%# Eval("IndentRequestID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstatus" Visible="true" runat="server"
                                                                        Text='<%# Eval("IndentStatus") %>'></asp:Label>
                                                                    <asp:Label ID="lblStID" Visible="false" runat="server"
                                                                        Text='<%# Eval("StatusID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintIndent_RegnDuplicate('<%# Eval("IndentNo")%>'); return false;">Print</a>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Indent HandOver">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div4">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Indent Date<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txt_fromHand"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_fromHand" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fromHand" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Sub Stock</span>
                                        <asp:DropDownList ID="ddl_stockList" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Approved By<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_approvedBy" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_approvedBy_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span16" class="input-group-addon cusspan" runat="server">Indent No.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                            ID="txt_indentNumber"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div3" runat="server">
                                            <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Indent Details </label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                    <asp:GridView ID="gvIndentDetail" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvIndentDetail_RowDataBound" OnRowCommand="gvIndentDetail_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Sl No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_Indentno" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ItemID 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ItemID" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_issueDate" Visible="false" Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_reqID" Visible="false" Text='<%# Eval("IndentRequestID")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_dept" runat="server" Style="text-align: center !important;" Text='<%# Eval("DeptID")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_stockID" Visible="false" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Visible="false" Text='<%# Eval("ItemName") %>' runat="server"></asp:Label>
                                                                    <asp:LinkButton ID="lnkSelect" BackColor="Yellow" runat="server" Text='<%# Eval("ItemName") %>' CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" Font-Underline="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ReqQty" runat="server" Text='<%# Eval("ReqdQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_approvedqty" runat="server" Text='<%# Eval("ApprovedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblremarks" Width="200px" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                    <asp:Panel ID="pnlGridViewDetails" runat="server" Width="400px" Height="400px" Style="display: none;"
                                                        CssClass="pnlBackGround">
                                                        <%--Add other controls here--%>
                                                        <asp:Label Font-Bold="true" ID="Label4" runat="server" Text="Details"></asp:Label>
                                                        <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" />
                                                    </asp:Panel>
                                                    <asp:Button ID="btnDummy" runat="server" Style="display: none;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow ">
                                <div class="col-sm-12 ">
                                    <asp:ModalPopupExtender ID="MdItemAvailability" runat="server" TargetControlID="btnDummy" PopupControlID="popupApproval"
                                        BackgroundCssClass="modalBackground" Enabled="True">
                                    </asp:ModalPopupExtender>
                                    <asp:Panel runat="server" ID="popupApproval" Style="display: none;">
                                        <div style="margin-left: 180px; margin-right: 150px;">
                                            <div style="width: 100%" class="row">
                                                <div style="background-color: #ffffff; padding-top: 0px" class="col-sm-12">
                                                    <asp:Button ID="btnSample" Style="display: none" runat="server" />
                                                    <div class="row">
                                                        <div style="background-color: #3a9" class="col-sm-12">
                                                            <h5 class="">Item Availability</h5>
                                                        </div>
                                                    </div>
                                                    <asp:Panel runat="server" ID="popupwindow" DefaultButton="btnadd">
                                                        <div class="row ">
                                                            <div class="col-sm-12">
                                                                <div class="fixeddiv" style="margin-top: 5px;">
                                                                    <div class="row fixeddiv" id="div1" runat="server">
                                                                        <asp:Label ID="lblmsg1" Visible="false" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-12 ">
                                                                    <div class="col-md-3">
                                                                        <div class="form-group input-group">
                                                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Item Name</span>
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                                                                ID="txt_itemname"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-9">
                                                                        <div class="form-group input-group">
                                                                            <span id="Span8" class="input-group-addon cusspan" runat="server">Search Stock</span>
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                                                ID="txt_stockNO"></asp:TextBox>
                                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                                                ServiceMethod="GetAutoStockAvailability" MinimumPrefixLength="1"
                                                                                CompletionInterval="100" CompletionSetCount="0" TargetControlID="txt_stockNO"
                                                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                                                            </asp:AutoCompleteExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group input-group">
                                                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Indent No.</span>
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                                                                ID="txt_ItemIndentNos"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group input-group">
                                                                            <span id="Span11" class="input-group-addon cusspan" runat="server">Indent Qty</span>
                                                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                                                                ID="txt_reqdqty"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group input-group">
                                                                            <span id="Span9" class="input-group-addon cusspan" runat="server">Approve Qty</span>
                                                                            <asp:TextBox runat="server" MaxLength="5" Class="form-control input-sm col-sm custextbox"
                                                                                ID="txt_qty"></asp:TextBox>
                                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_qty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-1">
                                                                        <asp:Button runat="server" CssClass="btn btn-sm cusbtn scus-sm-btn" ID="btnadd" OnClick="btnadd_Click" Text="Add" />
                                                                    </div>
                                                                    <div class="col-sm-1" style="margin-left: 20px;">
                                                                        <asp:Button runat="server" CssClass="btn btn-sm cusbtn scus-sm-btn" ID="btnrefe_close" OnClick="btnclose_Click" Text="Close" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row ">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-12">
                                                                        <div class="pbody">

                                                                            <div class="grid" style="width: 100%; height: 27vh; overflow: auto">
                                                                                <asp:GridView ID="GVStockAvailable" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table" OnRowCommand="GVStockAvailable_RowCommand"
                                                                                    EmptyDataText="Please add items.." OnRowDataBound="GVStockAvailable_RowDataBound" Width="100%" HorizontalAlign="Center">
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
                                                                                                Stock No
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblindentnumber" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                                                <asp:Label ID="lblsubtockID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("SubStockID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblStockNo" Style="text-align: left !important;" runat="server" Text='<%# Eval("StockNo") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                Item Name
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblitemID" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemID") %>'></asp:Label>
                                                                                                <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                Approved 
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_approved" Style="text-align: left !important;" runat="server" Text='<%# Eval("ApprovedQty") %>'></asp:Label>
                                                                                                <asp:Label ID="lblisapproved" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IsApproved") %>'></asp:Label>
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
                                                                                    <HeaderStyle BackColor="#D8EBF5" />
                                                                                </asp:GridView>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <br />
                                                    <div class="col-sm-12 ">
                                                        <div class="row">
                                                            <div class="col-md-8">
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group input-group">
                                                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Total Approved</span>
                                                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="true"
                                                                        ID="txt_totappd"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Total Approved Quantity</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ReadOnly="True"
                                            ID="txt_tolappqty"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_tolappqty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Handover To <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_handover" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_approvedBy_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Reject" OnClick="btn_save_Click" />
                                        <asp:Button ID="btnprint" runat="server" Class="btn  btn-sm cusbtn" Visible="false" UseSubmitBehavior="False" Text="Print" OnClientClick="return PrintIndent_Regn();" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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


