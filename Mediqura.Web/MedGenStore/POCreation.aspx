<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="POCreation.aspx.cs" Inherits="Mediqura.Web.MedGenStore.POCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "white";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "white";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function PrintPurchaseItemCheckList() {
            objitemname= document.getElementById("<%=txtitemName.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_retdatefrom.ClientID %>")
            objdateto = document.getElementById("<%=txt_returndateTo.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=PurchaseItemCheckList&ItemName=" + objitemname.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value)
        }
    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontaineritemgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabitemgroupmaster" runat="server" CssClass="Tab2" HeaderText="Purchase Item CheckList">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelitemgroupmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan"  runat="server">Store Type</span>
                                        <asp:DropDownList ID="ddlstoretype" runat="server" ReadOnly="true" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlstoretype_SelectedIndexChanged">
                                          </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan"  runat="server">Purchase Method</span>
                                        <asp:DropDownList ID="ddl_purchasemethod" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" >
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Local</asp:ListItem>
                                            <asp:ListItem Value="2">Supply</asp:ListItem>
                                       </asp:DropDownList>
                                    </div> 
                                  </div>   
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cus-long-span" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox" MaxLength="50"
                                            ID="txt_itemName" AutoPostBack="True" OnTextChanged="txt_itemName_TextChanged"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_itemName" ValidChars=" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ:0123456789,->"></asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                            ServiceMethod="GetItemNameWithID" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_itemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Approval List No</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_AppovalListNo"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">ItemList:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvitemchecklist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvitemchecklist_RowDataBound">
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
                                                                    StockNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstockno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("StockNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ItemName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_stockID" Visible="false" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_supplierID" Visible="false" Text='<%# Eval("SupplierID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_PO" Visible="false" Text='<%# Eval("PONo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Supplier
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_supplier" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Supplier") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Totl Recd Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totrecdqty" runat="server" Text='<%# Eval("TotalRecdQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Available 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_avail" runat="server" Text='<%# Eval("BalStock")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Condemned  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_condmqty" runat="server" Text='<%# Eval("TotalCondemned")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Expiry Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_expdate" runat="server" Text='<%# Eval("ExpDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Tax(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_tax" runat="server" Text='<%# Eval("Tax")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CPperQty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cpperqty" runat="server" Text='<%# Eval("CPPerQty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total CP 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Check
                                                                <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekboxselect" runat="server" onclick="Check_Click(this);" AutoPostBack="true" />
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
                            <div class="row">
                                <label class="label"></label>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Total Expiry</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totexpiry"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Total Condemned</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalcondemned"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Total Usable</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totalusable"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                         <asp:Button ID="btnaccept" runat="server" Class="btn  btn-sm cusbtn" Text="Accept" OnClick="btnaccept_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Item CheckList:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvitemselectedlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowCommand="gvitemselectedlist_RowCommand">
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
                                                                    ItemName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblstockID" Visible="false" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_supplierID" Visible="false" Text='<%# Eval("SupplierID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_PO" Visible="false" Text='<%# Eval("PONo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Supplier
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_supplier" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Supplier") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Totl Recd Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totrecdqty" runat="server" Text='<%# Eval("TotalRecdQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Available 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_avail" runat="server" Text='<%# Eval("BalStock")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Condemned  
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_condmqty" runat="server" Text='<%# Eval("TotalCondemned")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Quantity Reqd
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_qtyreqd" Width="170px" Height="18px" runat="server" AutoPostBack ="true" OnTextChanged="txt_qtyreqd_TextChanged"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Expiry Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_expdate" runat="server" Text='<%# Eval("ExpDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Tax(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_tax" runat="server" Text='<%# Eval("Tax")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CPperQty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cpperqty" runat="server" Text='<%# Eval("CPPerQty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total CP 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP","{0:0#.##}")%>'></asp:Label>
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
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label class="label"></label>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Total Expiry</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totexp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Total Condemned</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totcondemned"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Total Usable</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totusable"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Total Quantity Reqd</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totqtyreqd"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Total Cost Price</span>
                                        <asp:TextBox runat="server" ReadOnly="true" AutoPostBack="True" CssClass="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_totcp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btn_send" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Send For Approval" Width="50%" OnClick="btn_send_Click" />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Purchase ItemList">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cus-long-span" runat="server">Item Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-textbox" MaxLength="40"
                                            ID="txtitemName" AutoPostBack="True" OnTextChanged="txtitemName_TextChanged"></asp:TextBox>
                                          <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server"
                                            ServiceMethod="GetItemName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtitemName"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                     </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_retdatefrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_retdatefrom" />
                                        <%--    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_mfgdate" />
                                        --%>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span24" class="input-group-addon cusspan" runat="server">To <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_returndateTo"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                                TargetControlID="txt_returndateTo" />
                                            <%--  <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureAMPMPlaceholder=""
                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_mfgdate" />
                                            --%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch1" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch1_Click" />
                                        <asp:Button ID="btnprints" runat="server" Text="Print" OnClientClick="return PrintPurchaseItemCheckList();" class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div2" runat="server">
                                            <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
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
                                                    <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvpurchaseitemlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="true" PageSize="10" OnRowCommand="gvpurchaseitemlist_RowCommand" OnPageIndexChanging="gvpurchaseitemlist_PageIndexChanging"
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
                                                                    Approval List No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_approvallistno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ApprovalListNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ItemName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ItemID" Visible="false" Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_PO" Visible="false" Text='<%# Eval("PONo") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Supplier
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_supplier" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Supplier") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Available 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_avail" runat="server" Text='<%# Eval("BalStock")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Quantity Reqd 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("TotalQuantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Tax(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_tax" runat="server" Text='<%# Eval("Tax")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CPperQty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cpperqty" runat="server" Text='<%# Eval("CPPerQty","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total CP 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_cp" runat="server" Text='<%# Eval("CP","{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
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
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                    </asp:GridView>
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
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" OnClick="btnexport_Click" Text="Export" />
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