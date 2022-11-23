<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="StockReturn.aspx.cs" Inherits="Mediqura.Web.MedPhr.StockReturn" %>

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
    </script>

    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="TabContainerStockReturn" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="TabStockReturnList" runat="server" CssClass="Tab2" HeaderText="Stock List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelitemgroupmaster">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Supplier<span style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_Supplier" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Filter Type<span style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_FilterType" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Expired"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Near Expired"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Dump"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span24" class="input-group-addon cusspan" runat="server">Return No. </span>
                                        <asp:Label runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="lbl_ReturnNo"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Expired From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ExpiredFrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_ExpiredFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ExpiredFrom" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Expired To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ExpiredTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txt_ExpiredTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ExpiredTo" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm  cusbtn" OnClick="btnsearch_Click" Text="Search" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblResult" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%;" >
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GvStockReturn" runat="server" CssClass="table-hover grid_table result-table" ShowFooter="true"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="GvStockReturn_RowDataBound"
                                                    Width="100%" HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                Stock Number
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStockNumber" Style="text-align: left !important;" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
																 <asp:Label ID="lblstockid" Style="text-align: left !important;" runat="server" Text='<%# Eval("StockID")%>'></asp:Label>
																<asp:Label ID="lblrtnserialID" Visible="false" Text="<%# Container.DataItemIndex+1%>" runat="server"></asp:Label>
                                                                                                                                      
                                                                    <asp:Label ID="lbl_rtnreceiptNo" Visible="false" Text='<%# Eval("ReceiptNo") %>' runat="server"></asp:Label>
                                                                   
                                                                    <asp:Label ID="lbl_rtnCompanyID" Visible="false" Text='<%# Eval("CompanyID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_rtnSupplierID" Visible="false" Text='<%# Eval("SupplierID") %>' runat="server"></asp:Label>
                                                                    
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Item Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemID" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemID")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblItemName" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                        </asp:TemplateField>
														<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Batch no
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   
                                                                   <asp:Label ID="lbl_rtnbatchno"  Text='<%# Eval("BatchNo") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CP/Qty
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCPPerUnit" Style="text-align: center !important;" runat="server" Text='<%# Eval("CPPerQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Expired 
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpiredDate" runat="server" Text='<%# Eval("ExpiryDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Available Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailableQuantity" runat="server" Text='<%# Eval("TotalBalanceQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Return Quantity
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReturnQuantity" onfocus="this.select();" Width="50px" MaxLength="5" Text='<%# Eval("TotalBalanceQty")%>' OnTextChanged="txtReturnQuantity_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" TargetControlID="txtReturnQuantity" ValidChars="1234567890" runat="server"></asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                 Taxable(₹)
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblReturnPricePerItem" Text="0"  Width="50px"   onfocus="this.select();" OnTextChanged="Taxable_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" TargetControlID="lblReturnPricePerItem" ValidChars="1234567890." runat="server"></asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                        </asp:TemplateField>
														<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    SGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_rtnsgst"  Width="50px" runat="server" Text='<%# Eval("SGST")%>' OnTextChanged="Taxable_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" TargetControlID="lbl_rtnsgst" ValidChars="1234567890." runat="server"></asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    CGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_rtncgst"  Width="50px" runat="server" Text='<%# Eval("CGST")%>' OnTextChanged="Taxable_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender411" TargetControlID="lbl_rtncgst" ValidChars="1234567890." runat="server"></asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    IGST(%)
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lbl_rtnigst" runat="server"  Width="50px" Text='<%# Eval("IGST")%>' OnTextChanged="Taxable_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender114" TargetControlID="lbl_rtnigst" ValidChars="1234567890." runat="server"></asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="0.5%" />
                                                            </asp:TemplateField>
														<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    AfterTax
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_rtncpAfterTax" runat="server" Text="0"   Width="70px"> </asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Is Return
                                                                <asp:CheckBox ID="CheckBoxAll" AutoPostBack="true" Checked="true" OnCheckedChanged="CheckBoxAll_CheckedChanged" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBoxIsReturn" AutoPostBack="true" Checked="true" OnCheckedChanged="CheckBoxIsReturn_CheckedChanged" runat="server" onclick="Check_Click(this);" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="1%" />
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
                            <div class="row"></div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Total Quantity </span>
                                        <asp:Label ID="lblTotalReturnQuantity" Class="form-control input-sm col-sm custextbox" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Total Price </span>
                                        <asp:Label runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="lblTotalReturnPrice"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-2"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnSave" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnSave_Click" Text="Save" />
                                        <asp:Button ID="btnPrint" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnPrint_Click" Text="Print" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <%--------------------------------END TAB 1-------------------------------%>

                <%--------------------------------START TAB 2-------------------------------%>
                <asp:TabPanel ID="TabPanelReturnList" runat="server" CssClass="Tab2" HeaderText="Return List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div1">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessageTab2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Return No.</span>
                                        <asp:TextBox runat="server" MaxLength="20" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ReturnNo"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Supplier Name</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlTab2_SupplierName">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Filter Type</span>
                                        <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="ddlTab2_FilterType">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Return From</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ReturnFrom"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtTab2_ReturnFrom" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTab2_ReturnFrom" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Return To</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txtTab2_ReturnTo"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy,dd-MM-yyyy"
                                            TargetControlID="txtTab2_ReturnTo" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtTab2_ReturnTo" />
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnTab2Search" runat="server" Class="btn  btn-sm cusbtn" Text="Search" />
                                        <asp:Button ID="btnTab2Reset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="div2" runat="server">
                                                <asp:Label ID="lblResultTab2" runat="server" Height="13px"></asp:Label>
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
                                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:GridView ID="GvReturnList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                            EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Sl.No
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# (Container.DataItemIndex+1)%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Stock Number
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2StockNumber" Style="text-align: left !important;" runat="server" Text='<%# Eval("StockNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Item Name
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ItemID" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemID")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblTab2ItemName" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        CP Per Qnty
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2CPPerUnit" Style="text-align: center !important;" runat="server" Text='<%# Eval("CPPerQty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Available Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2AvailableQuantity" runat="server" Text='<%# Eval("TotalBalanceQty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Return Quantity
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ReturnQuantity" Text='<%# Eval("ReturnQuantity")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Return Price
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ReturnPricePerItem" Text='<%# Eval("ReturnPrice")%>' runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Returned Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ReturnDate" runat="server" Text='<%# Eval("ReturnDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Returned By
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTab2ReturnBy" runat="server" Text='<%# Eval("ReturnBy","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <span class="cus-Delete-header">Delete</span>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTab2Delete" runat="server" CommandName="Print" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">                                                                         
                                                                                  <i class="fa fa-print-o cus-delete-color"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
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
                <%--------------------------------END TAB 2-------------------------------%>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
