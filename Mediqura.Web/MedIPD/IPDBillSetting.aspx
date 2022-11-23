<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="IPDBillSetting.aspx.cs" Inherits="Mediqura.Web.MedIPD.IPDBillSetting" %>
<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
     <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerbillSetting" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabbillSetting" runat="server" CssClass="Tab2" HeaderText="IPD Bill Setting">
                    <contenttemplate>
                        <div class="custab-panel" id="ipdbillSettingiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3">
                                  <div class="form-group input-group">
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Type<span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" ID="ddlpatienttype" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlpatienttype_SelectedIndexChanged" ></asp:DropDownList>
                                   </div>
                                </div>
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="35"
                                            ID="txtautoIPNo" AutoPostBack="True" ></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtautoIPNo" ID="FilteredTextBoxExtender3"
                                            runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            ValidChars="->,:/ " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetIPNoWithName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            
                                 
                             </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Service Category <span
                                            style="color: red"></span></span>
                                        <asp:DropDownList ID="ddl_servicecategory" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicecategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Bed</asp:ListItem>
                                             <asp:ListItem Value="2">Doctor Round</asp:ListItem>
                                            <asp:ListItem Value="3">Extra Attendant</asp:ListItem>
                                            <asp:ListItem Value="4">Investigation</asp:ListItem>
                                            <asp:ListItem Value="5">OT</asp:ListItem>
                                            <asp:ListItem Value="6">Advance</asp:ListItem>
                                            <asp:ListItem Value="7">Refund</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-5"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                         <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
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
                                  <label class="gridview-label">IP Service Detail</label>
                                <div class="col-sm-12">
                                    <div>
                                       <div class="pbody">
                                              <div class="gridview-container-M">
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                      <asp:GridView ID="Gv_servicelist" runat="server" CssClass="table-hover grid_table result-table" OnRowCommand="Gv_servicelist_RowCommand" 
                                                EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>SL No.</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Service Date</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_recordID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            <asp:Label ID="lbl_servicedate" runat="server" Text='<%# Eval("ServiceDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Serv.No</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_servicenumber" runat="server" Text='<%# Eval("ServiceNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Inv Number</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_invnumber" runat="server" Text='<%# Eval("InvNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Category</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_servicecatg" runat="server" Text='<%# Eval("ServiceCategory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Service Name</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_servicename" runat="server" Text='<%# Eval("ServiceName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Charge</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblservicecharge" runat="server" Text='<%# Eval("ServiceCharge","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="right" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Qty</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div>
                                                                <asp:Label ID="lbl_totalqty" runat="server" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Net Charge</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnetcharge" runat="server" Text='<%# Eval("NetServiceCharge","{0:0#.##}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div>
                                                                <asp:Label ID="lbl_subtotal" runat="server" Font-Bold="true" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>Doctor</HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_doctor" runat="server" Text='<%# Eval("DoctorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_actualcategoryID" Visible="false" runat="server" Text='<%# Eval("ActualCategoryID") %>'></asp:Label>
                                                            <asp:TextBox ID="txtremarks" Width="100px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
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
                        <div class="row" style="height: 15px">
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span18" class="input-group-addon cusspan" runat="server">Total Bill(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txttotalamount"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Balance(A/C)(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbalanceinac"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Adjusted(A/C)(₹)</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtadjustedamount"></asp:TextBox>
                                    </div>
                                </div>
                               <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span33" class="input-group-addon cusspan" runat="server">Payable(₹)</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_payableamount" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                           
                            </div>
                        </contenttemplate>
                </asp:TabPanel>
             <%--   <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="IPD Bill Setting List">
                <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsearchList">
                            <div class="custab-panel" id="paneldepositlist">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="divmsg2" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="35"
                                            ID="txtautoIPNoList" AutoPostBack="True" ></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtautoIPNoList" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                            ValidChars="->,:/ " Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetIPNoWithName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNoList"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                            
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Service Category <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_servicecategoryList" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_servicecategoryList_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Bed</asp:ListItem>
                                             <asp:ListItem Value="2">Doctor Round</asp:ListItem>
                                            <asp:ListItem Value="3">Extra Attendant</asp:ListItem>
                                            <asp:ListItem Value="4">Investigation</asp:ListItem>
                                            <asp:ListItem Value="5">OT</asp:ListItem>
                                            <asp:ListItem Value="6">Advance</asp:ListItem>
                                            <asp:ListItem Value="7">Refund</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                             </div>
                            <div class="row">
                                <div class="col-lg-8"></div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                       <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg4" runat="server">
                                            <asp:Label ID="lblresult2" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                       <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:GridView ID="GvIpdBillSettingList" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."  AutoGenerateColumns="False"  
                                                        Width="100%" HorizontalAlign="Center" OnRowCommand="GvIpdBillSettingList_RowCommand"  >
                                                        <Columns>
                                                            <asp:TemplateField >
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
                                                                    IPNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <asp:Label ID="lblIPNo" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("IPNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>  
                                                              <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Service Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" Style="text-align: center !important;" runat="server" Visible="false"
                                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                                   <asp:Label ID="lblServiceName" Style="text-align: center !important;" runat="server"
                                                                        Text='<%# Eval("") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>  
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <asp:Label ID="lblrate" Style="text-align: center !important;" runat="server" 
                                                                        Text='<%# Eval("", "{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                          <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Quantity
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Net Charges
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblnetcharges" runat="server" Text='<%# Eval("", "{0:0#.##}")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                                    <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
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

                                                              <asp:ButtonField CommandName="Update" HeaderText="Update Status" ButtonType="Button" Text="Update" CausesValidation="True">
                                                            <ItemStyle Width="1%" CssClass="updateLink"/>
                                                            </asp:ButtonField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                    </div>
                                </div>
                            </div>
                                                           
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </ContentTemplate>


        </asp:TabPanel>--%>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
