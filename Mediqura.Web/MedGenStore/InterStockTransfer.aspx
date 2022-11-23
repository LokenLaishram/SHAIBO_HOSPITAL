7<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="InterStockTransfer.aspx.cs" Inherits="Mediqura.Web.MedGenStore.InterStockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintUtilizationList() {
            objTransferNo = document.getElementById("<%=txttransferno.ClientID %>")
            objFromGenStockID = document.getElementById("<%=ddlfromgenstock.ClientID %>")
            objToGenStockID = document.getElementById("<%=ddltogenstock.ClientID %>")
            objTransferByID = document.getElementById("<%=ddltransferby.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_from.ClientID %>")
            objdateto = document.getElementById("<%=txt_To.ClientID %>")

            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=TransferGenStockList&InterTransferNo=" + objTransferNo.value + "&FromGenStockID=" + objFromGenStockID.value + "&ToGenStockID=" + objToGenStockID.value + "&TransferByID=" + objTransferByID.value + " &Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)

        }
        function Print_InterTranfetItem() {
            objInterTransferNo = document.getElementById("<%=txt_intertransferNo.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=InterStockByTransferNo&InterTransferNo=" + objInterTransferNo.value)
        }
        function PrintStockByTransferNo(InterTransferNo) {
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=InterStockByTransferNo&InterTransferNo=" + InterTransferNo)
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerstocktransfer" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabinterstocktransfer" runat="server" CssClass="Tab1" HeaderText="Inter Stock Tranfer">
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
                                        <asp:HiddenField ID="hdnID" runat="server" />
                                        <asp:HiddenField ID="hdnItemID" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Available<span
                                            style="color: red;">*</span></span>
                                          <asp:TextBox runat="server" ReadOnly="true" Visible="false" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_itemID"></asp:TextBox>
                                        <asp:TextBox runat="server" ReadOnly="true" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txt_Avail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblqty" class="input-group-addon cusspan" runat="server">Qty <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" AutoPostBack="True" MaxLength="5" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txttransferqty" OnTextChanged="txt_transferqty_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txttransferqty" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltransferdetails" class="input-group-addon cusspan" runat="server">Remark  <span
                                            style="color: red;">*</span></span>
                                        <asp:TextBox runat="server" MaxLength="200" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_transferremarks" OnTextChanged="txt_Remark_TextChanged" AutoPostBack="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" Class="btn  btn-sm scusbtn" OnClick="btnadd_Click" />
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <asp:TextBox runat="server" Enabled="False" Width="150px" placeholder="   Transfer Number" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_intertransferNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Item List</label>
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
                                                    <asp:GridView ID="gvintersubstocklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." OnRowCommand="gvsubstocklist_RowCommand"
                                                        Width="100%" HorizontalAlign="Center">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    SlNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex+1%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Stock No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_GenstockID" Visible="false" Text='<%# Eval("GenStockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_StockNo" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Item Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    Qty.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_transferqty" runat="server" Text='<%# Eval("TransferQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Transfer Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_transferremark" runat="server" Text='<%# Eval("TransferRemarks")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">Delete</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                        OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                    <i class="fa fa-trash-o cus-delete-color"></i>
                                                                    </asp:LinkButton>
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
                                <div class="col-md-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_totaltransferqty" class="input-group-addon cusspan" runat="server">Total Qty</span>
                                        <asp:TextBox runat="server" MaxLength="16" ReadOnly="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totaltransferqty"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_transferto" class="input-group-addon cusspan" runat="server">Transfer To <span
                                            style="color: red;">*</span></span>
                                        <asp:DropDownList ID="ddl_transferto" runat="server" AutoPostBack="True" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" style="text-align: right;">
                                    <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" OnClientClick="this.disabled='true';this.value='Please wait..'" OnClick="btnsave_Click" Text="Save" Class="btn  btn-sm cusbtn" />
                                    <asp:Button ID="btn_print" runat="server" Text="Print" Class="btn  btn-sm cusbtn" OnClientClick="return Print_InterTranfetItem();" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabtranferlist" runat="server" CssClass="Tab1" HeaderText="Transfer Stock List">
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
                                        <span id="lbl_from" class="input-group-addon cusspan" runat="server">Date From  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_from"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_from" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender11" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_from" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbl_to" class="input-group-addon cusspan" runat="server">Date To  </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_To"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_To" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender12" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_To" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lblfromgenstock" class="input-group-addon cusspan" runat="server">From General Stock </span>
                                        <asp:DropDownList runat="server" ID="ddlfromgenstock" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltogenstock" class="input-group-addon cusspan" runat="server">To General Stock </span>
                                        <asp:DropDownList runat="server" ID="ddltogenstock" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltransferNo" class="input-group-addon cusspan" runat="server">Transfer No. </span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3;"
                                            ID="txttransferno"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="lbltransferby" class="input-group-addon cusspan" runat="server">Transfer By </span>
                                        <asp:DropDownList runat="server" ID="ddltransferby" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox"></asp:DropDownList>
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
                                <div class="col-sm-3">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm scusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprint" runat="server" Visible="true" Class="btn  btn-sm scusbtn" Text="Print" OnClientClick="return PrintUtilizationList();" />
                                        <asp:Button ID="btnclear" runat="server" Class="btn  btn-sm scusbtn" Text="Reset" OnClick="btnclear_Click" />
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
                                        <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto;">
                                            <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gvtransferstocklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." ShowFooter="true" OnRowCommand="gvtransferstocklist_RowCommand"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            SlNo.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Transfer No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>                                                         
                                                            <asp:Label ID="lbl_transferno" runat="server" Text='<%# Eval("InterTransferNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Transfer To
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_FromGenStockID"  Visible="false" runat="server"  Text='<%# Eval("TransferFromGenStockID")%>'></asp:Label>
                                                            <asp:Label ID="lbl_TogenStockID"  Visible="false" runat="server" Text='<%# Eval("TransferToGenStockID")%>'></asp:Label>
                                                            <asp:Label ID="lbl_transfertogenstock" runat="server" Text='<%# Eval("TransferToName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Total Qty
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_totaltransferqty" runat="server" Text='<%# Eval("TotalTransferQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_grandtotaltransferqty" runat="server" Text='<%# Eval("GrandTotalTransferQty")%>'></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Transfer By
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_transferbyName" runat="server" Text='<%# Eval("TransferByName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Transfer Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_transferdate" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remark
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lbl_remarks" MaxLength="200" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            Print
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintStockByTransferNo('<%# Eval("InterTransferNo")%>'); return false;">Print</a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <span class="cus-Delete-header">Delete</span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
                                                                OnClientClick="javascript: return confirm('Are you sure to delete ?');">
                                                                               <i class="fa fa-trash-o cus-delete-color"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
