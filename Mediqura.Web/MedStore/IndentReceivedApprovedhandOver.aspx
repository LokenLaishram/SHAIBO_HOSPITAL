<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="IndentReceivedApprovedhandOver.aspx.cs" Inherits="Mediqura.Web.MedStore.IndentReceivedApprovedhandOver" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">
        function PrintHandOverList()
        {
            objSubStock = document.getElementById("<%=ddl_substock.ClientID %>")
            objhand = document.getElementById("<%=ddl_HandOver.ClientID %>")
            objApprv = document.getElementById("<%=ddl_approvedBy.ClientID %>")
           objdatefrom = document.getElementById("<%=txt_from.ClientID %>")
           objdateto = document.getElementById("<%=txt_To.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=HandOverList&SubStock=" + objSubStock.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&HandOverTo=" + objhand.value + "&Approved=" + objApprv.value )
        }
        function PrintDuplicateIndents(IndentNo)
        {
            window.open("../MedStore/Reports/ReportViewer.aspx?option=Handover&IndentNo=" + IndentNo)
        }
        function PrintIndentHndOv_Regn() {
            objIndentNo = document.getElementById("<%=hdnIndNo.ClientID %>")
            window.open("../MedStore/Reports/ReportViewer.aspx?option=Handover&IndentNo=" + objIndentNo.value)
         }
        
     </script>
            <asp:TabContainer ID="tabcontainerindentRequest" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabindentRequest" runat="server" CssClass="Tab2" HeaderText="Indent Approved">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelindentRequest">
                             <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                               <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                     <asp:Label ID="lblIndNo" Visible="false" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Indent Date From <span
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
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Indent Date To  <span
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
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Sub-Stock <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_substock" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                               
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Status(Request) <span
                                            style="color: red"></span></span>
                                           <asp:HiddenField ID="hdnIndNo" runat="server" />
                                        <asp:DropDownList ID="ddl_requestType" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                    </div>
                                </div>
                           </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">RequestList:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress2" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvIndentRequest" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvIndentRequest_RowDataBound" OnRowCommand="gvIndentRequest_RowCommand">
                                                           <Columns>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">View</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    View
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="left" Width="1%" Font-Underline="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Indent No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   
                                                                     <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_Indentno" Style="text-align:center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                           
                                                          <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Request Total Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qty" runat="server" Style="text-align:center !important;" Text='<%# Eval("TotalRqty")%>'></asp:Label>
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
                                                                    Request Type
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
                                                            
                                                        </Columns>
                                                        <HeaderStyle BackColor="#D8EBF5" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                          <div class="row cusrow pad-top ">
                                <label class="gridview-label">RequestDetail:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading1" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress3" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvIndentDetail" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvIndentDetail_RowDataBound" OnRowCommand="gvIndentDetail_RowCommand">
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
                                                                    Indent No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_Indentno" Style="text-align:center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ItemID 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbl_ItemID"  Text='<%# Eval("ItemID") %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ItemName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lblID" Visible="false" Text='<%# Eval("ID") %>' runat="server"></asp:Label>
                                                                      <asp:Label ID="lbl_issueDate" Visible="false" Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}")%>' runat="server"></asp:Label>
                                                                     <asp:Label ID="lbl_reqID" Visible="false" Text='<%# Eval("IndentRequestID")%>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_stockID" Visible="false" Text='<%# Eval("StockID") %>' runat="server"></asp:Label>
                                                                     <asp:Label ID="lbl_subStock" Visible="false" Text='<%# Eval("StockTypeID") %>' runat="server"></asp:Label>
                                                                   
                                                                    <asp:Label ID="lblitemname" Style="text-align: left !important;" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
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
                                                                    Request Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lbl_ReqQty" runat="server" Text='<%# Eval("ReqdQty")%>'></asp:Label>
                                                                    <%--<asp:TextBox ID="txt_ReqQty" runat="server" Text='<%# Eval("ReqdQty")%>'></asp:TextBox>--%>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_approvedqty" AutoPostBack="True" runat="server" Text='<%# Eval("TotApproved")%>' OnTextChanged="txt_approvedqty_TextChanged" MaxLength="5"></asp:TextBox>
                                                                     <asp:FilteredTextBoxExtender TargetControlID="txt_approvedqty" ID="FilteredTextBoxExtender8" 
                                                                            runat="server"
                                                                            FilterMode="ValidChars"
                                                                            ValidChars="0123456789" Enabled="True">
                                                                        </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                        <%-- <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Check
                                                                <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekboxselect" runat="server" onclick="Check_Click(this);" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>--%>
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
                                 <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span32" class="input-group-addon cusspan" runat="server">Total Approved</span>
                                       
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_TotApprv"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txt_TotApprv" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                           
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Approved By <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_userAppBy" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Handover To <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_userHndovTo" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                             </div>
                             <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="HandOver" OnClick="btn_save_Click" />
                                        <asp:Button ID="btn_print" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm  cusbtn" Text="Print" OnClientClick="return PrintIndentHndOv_Regn();"  />
                                        <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                    </div>
                                </div>
                            </div>
                            </ContentTemplate>
                          </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
             
           
     <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Indent HandOver List">
                    <ContentTemplate>
                        <div class="custab-panel" id="Div2">
                             <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                               <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Indent Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
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
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Indent Date To  <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ToHand"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_ToHand" />
                                          <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ToHand" />
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Sub-Stock Name<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_subStockList" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox"  >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">HandOver To<span
                                            style="color: red"></span></span>

                                        <asp:DropDownList ID="ddl_HandOver" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                          <div class="row">

                               <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Approved By<span
                                            style="color: red"></span></span>

                                        <asp:DropDownList ID="ddl_approvedBy" runat="server"  class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_approvedBy_SelectedIndexChanged" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                </div>
                               <div class="col-sm-2">
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print"  Class="btn  btn-sm cusbtn" OnClientClick="return PrintHandOverList();"  />
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
                                 <label class="gridview-label">HandOverDetail:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading3" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvHandoverlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="True" OnRowCommand="gvHandoverlist_RowCommand" OnPageIndexChanging="gvHandoverlist_PageIndexChanging"
                                                        Width="100%" HorizontalAlign="Center" OnRowDataBound="gvHandoverlist_RowDataBound">
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
                                                                    Indent No
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_Indentno" Style="text-align:center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                             
                                                          <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                   Total Approved
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_AppQty" runat="server" Style="text-align:center !important;" Text='<%# Eval("TotalRqty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Apprvdate" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ApprovedBy" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("ApprovedBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    HandOver To
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_HndOvTo" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("RecdBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField>
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
                                                            </asp:TemplateField>--%>
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
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateIndents('<%# Eval("IndentNo")%>'); return false;">Print</a>
                                                                                                                               </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
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
                            </div>
                                 
                              <div class="row">
                               <div class="col-md-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Total HandOver</span>
                                      
                                                <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_InHandover"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_InHandover" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>
                                           
                                    </div>
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
                                                        <asp:ListItem Value="2" Text="PDF"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                    </div>
                                    </div>
                            </div>
                          
                        
                                     </ContentTemplate>
                          </asp:UpdatePanel>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>



            </asp:TabContainer>

</asp:Content>

