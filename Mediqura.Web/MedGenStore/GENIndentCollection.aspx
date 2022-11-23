<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="GENIndentCollection.aspx.cs" Inherits="Mediqura.Web.MedGenStore.GENIndentCollection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

    <script type="text/javascript">
        function PrintDuplicateReceived(IndentNo) {
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=Received&IndentNo=" + IndentNo)
        }
        function PrintIndentReceived_Regn() {
            objIndentNo = document.getElementById("<%=hdnIndNo.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=Received&IndentNo=" + objIndentNo.value)
        }
        function PrintReceivedList() {
            objsubstock = document.getElementById("<%=ddl_deptList.ClientID %>")
            objrecBy = document.getElementById("<%=ddl_rcvBy.ClientID %>")
            objstat = document.getElementById("<%=ddl_status.ClientID %>")
            objdatefrom = document.getElementById("<%=txt_fromRecvList.ClientID %>")
            objdateto = document.getElementById("<%=txt_ToRecvList.ClientID %>")
            window.open("../MedGenStore/Reports/ReportViewer.aspx?option=ReceivedList&SubStock=" + objsubstock.value + "&Datefrom=" + objdatefrom.value + "&Dateto=" + objdateto.value + "&Received=" + objrecBy.value + "&Status=" + objstat.value)
        }
    </script>
    <asp:TabContainer ID="tabcontainerIndentCollection" runat="server" CssClass="Tab" ActiveTabIndex="0"    
        Width="100%">
        <asp:TabPanel ID="TabIndentCollection" runat="server" CssClass="Tab2" HeaderText="Indent Collection">
            <ContentTemplate>
                <div class="custab-panel" id="Div5">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div6" runat="server">
                                    <asp:Label ID="lblmessage6" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span17" class="input-group-addon cusspan" runat="server">Indent Date From <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_fromRecv"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_fromRecv" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fromRecv" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Indent Date To  <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ToRecv"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_ToRecv" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ToRecv" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Department<span
                                            style="color: red">*</span></span>
                                          <asp:HiddenField ID="hdnIndNo" runat="server" />
                                        <asp:DropDownList ID="ddl_dept" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Status(Request) <span
                                            style="color: red"></span></span>

                                        <asp:DropDownList ID="ddl_RequestTypeRecv" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearcgRecv" runat="server" Class="btn  btn-sm  cusbtn" Text="Search" OnClick="btnsearcgRecv_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">Approved List:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress6" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress2" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvHndOvList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvHndOvList_RowDataBound" OnRowCommand="gvHndOvList_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Delete-header">View</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSelect" runat="server" CommandName="lnkSelectRecv" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
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
                                                                    <asp:Label ID="codeHndOv" Visible="false" runat="server" Text='<%# Eval("IndentID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_IndnoHndOv" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Department
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                      <asp:Label ID="lbl_deptID" runat="server" Visible="false" Style="text-align:center !important;" Text='<%# Eval("DeptID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_dept" runat="server" Style="text-align:center !important;" Text='<%# Eval("Department")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Request Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_qtyHndOv" runat="server" Style="text-align: center !important;" Text='<%# Eval("TotalRqty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    HandOver Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_hndovdate" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("HndOverDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Request Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_IndentdateHndOv" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_apprvdate" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("ApprovdDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                   Total  Approved Qty
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_totApprv" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("TotApproved") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ApprvBy" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("ApprovdBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                           <%-- <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    HandOver By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_hndBy" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("RecdBy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>--%>
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
                                                                    <asp:Label ID="lblstatusHndOv" Visible="true" runat="server"
                                                                        Text='<%# Eval("IndentStatus") %>'></asp:Label>
                                                                    <asp:Label ID="lblStIDHndOv" Visible="false" runat="server"
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
                                <label class="gridview-label">Approved Detail:</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress7" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading1" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress3" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="gvHndetail" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." Width="100%" HorizontalAlign="Center" OnRowDataBound="gvHndetail_RowDataBound" OnRowCommand="gvHndetail_RowCommand">
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
                                                                    <asp:Label ID="lbl_IndentnoRecv" Style="text-align: center !important;" runat="server"
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

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Approved Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ApprvQty" runat="server" Text='<%# Eval("ApprvQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    HandOver Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_HndQty" runat="server" Text='<%# Eval("ApprvQty")%>'></asp:Label>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Delivered Qty 
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                       <asp:Label ID="lbl_RecvQty" runat="server" Text='<%# Eval("ApprvQty")%>'></asp:Label>
                                                                    <%--<asp:TextBox ID="txt_RecvQTY" AutoPostBack="True" runat="server" Text='<%# Eval("ApprvQty")%>' OnTextChanged="txt_RecvQTY_TextChanged"></asp:TextBox>--%>
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
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Total Delivered</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totRecv"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txt_totRecv" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Total Approved</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totApprvRecv"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txt_totApprvRecv" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Total Request</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totHandOvRecv"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_totHandOvRecv" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>

                                    </div>
                                </div>
                         </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Collected By <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_user" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Handover By <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_HndovTo" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnSaveRecv" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Save" OnClick="btnSaveRecv_Click" />
                                        <asp:Button ID="btnPrintRecv" runat="server" UseSubmitBehavior="false" Class="btn  btn-sm  cusbtn" Text="Preview" OnClientClick="return PrintIndentReceived_Regn();"  />
                                        <asp:Button ID="btnresetRecv" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresetRecv_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>


        <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="Delivered Indent List">
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
                                            ID="txt_fromRecvList"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_fromRecvList" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_fromRecvList" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Indent Date To  <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_ToRecvList"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                            TargetControlID="txt_ToRecvList" />
                                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txt_ToRecvList" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Department<span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_deptList" runat="server" AutoPostBack="True" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span13" class="input-group-addon cusspan" runat="server">Collected By<span
                                            style="color: red"></span></span>

                                        <asp:DropDownList ID="ddl_rcvBy" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Handover By <span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_HndovToList" runat="server"  class="form-control input-sm col-sm custextbox" >
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Status<span
                                            style="color: red">*</span></span>

                                        <asp:DropDownList ID="ddl_status" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                               <%-- <div class="col-sm-3">
                                </div>--%>
                                <div class="col-sm-2">
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                        <asp:Button ID="btnprint" runat="server" Text="Print"  Class="btn  btn-sm cusbtn" OnClientClick="return PrintReceivedList();"   />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divresult1" runat="server">
                                            <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label">CollectedList:</label>
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
                                                    <asp:GridView ID="gvReceivedlist" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AllowPaging="True" OnRowCommand="gvReceivedlist_RowCommand"
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
                                                                    <asp:Label ID="lbl_Indentno" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Total Collected
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_TotRecvQty" runat="server" Style="text-align: center !important;" Text='<%# Eval("TotReceived")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Collected Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Apprvdate" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IndentRaiseDate","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Collected By
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_ApprovedBy" Style="text-align: center !important;" runat="server"
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
                                                                    Print
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDuplicateReceived('<%# Eval("IndentNo")%>'); return false;">Print</a>

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
                                <div class="col-md-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Total Collected</span>

                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_totRecvList"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_totRecvList" ValidChars="0123456789" Enabled="True"></asp:FilteredTextBoxExtender>

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
