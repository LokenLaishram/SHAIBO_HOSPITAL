<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="InsuranceReceivable.aspx.cs" inherits="Mediqura.Web.MedAccount.InsuranceReceivable" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <asp:updatepanel id="upMains" runat="server" updatemode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabContainerAccountGroup" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabPanelAccountGroup" runat="server" HeaderText="Insurance Receivable">
                    <contenttemplate>
                        <div class="custab-panel" id="depositdetaildiv">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">Service Type </span>
                                        <asp:DropDownList ID="ddlservicetype"  runat="server" class="form-control input-sm col-sm custextbox" >
                                             <asp:ListItem Value="0">-select-</asp:ListItem>
                                            <asp:ListItem Value="1">OP Services</asp:ListItem>
                                            <asp:ListItem Value="2">OP INV</asp:ListItem>
                                            <asp:ListItem Value="3">IP Services</asp:ListItem>
                                            <asp:ListItem Value="4">Emergency</asp:ListItem>
                                          </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                  <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Patient Name </span>
                                        <asp:TextBox ID="txtpatientname" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                         </asp:TextBox>
                                       <asp:FilteredTextBoxExtender TargetControlID="txtpatientname" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetDiscountedPatientName" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientname"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                   </div>            
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Bill No </span>
                                        <asp:TextBox ID="txt_billno" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox">
                                         </asp:TextBox>
                                    <asp:FilteredTextBoxExtender TargetControlID="txt_billno" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars"
                                            ValidChars=" :/" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                            ServiceMethod="GetBillNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_billno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender> </div>
                                  </div>
                             </div>
                             <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Category </span>
                                        <asp:DropDownList ID="ddl_patcategory" AutoPostBack="True" runat="server" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_patcategory_SelectedIndexChanged" >
                                      </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">Patient Sub Category </span>
                                        <asp:DropDownList ID="ddl_subcategory"  runat="server" class="form-control input-sm col-sm custextbox" >
                                         </asp:DropDownList>
                                    </div>
                                </div>   
                              <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Receivable</span>
                                        <asp:DropDownList ID="ddl_receivable"  runat="server" class="form-control input-sm col-sm custextbox" >
                                            <asp:ListItem Value="3">-- Select --</asp:ListItem> 
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                </div> 
                                 <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddl_status"  runat="server" class="form-control input-sm col-sm custextbox" >
                                            <asp:ListItem Value="3">-- Select --</asp:ListItem> 
                                            <asp:ListItem Value="0">Not Receive</asp:ListItem>
                                            <asp:ListItem Value="1">Received</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-sm-12">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click"/>
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click"/>
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
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIVloading1" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>

                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                        <ContentTemplate>
                                                            <asp:HiddenField runat="server" ID="lblEntryType" />
                                                            <asp:GridView ID="GVInsuranceReceivable" runat="server" CssClass="table-hover grid_table result-table" 
                                                                EmptyDataText="No record found..." AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center" OnRowDataBound="GVInsuranceReceivable_RowDataBound">
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
                                                                            UHID
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" Visible="false" Style="text-align: center !important;" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                                       <asp:Label ID="lblUHID" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("UHID")%>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Patient Name
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblpatientName" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("PatientName") %>'></asp:Label>
                                                                           </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Bill No.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblbillno" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("BillNo") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Patient Category
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblpatcategory" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("PatientCategory") %>'></asp:Label>
                                                                             <asp:Label ID="lblPatCatId" Visible="false" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("PatientCategoryID") %>'></asp:Label>
                                                                               <asp:Label ID="lblServicetypeID" Visible="false" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("ServiceTypeID") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            Sub Category
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblsubcategory" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("SubCategory") %>'></asp:Label>
                                                                             <asp:Label ID="lblPatSubCatId" Visible="false" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("SubCategoryID") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>  <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           Discount Amt.
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lbldiscamt" Style="text-align: center !important;" runat="server"
                                                                       Text='<%# Eval("DiscountAmount","{0:0#.##}") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           Discounted By
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lbldiscountedby" Style="text-align: center !important;" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%"/>
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           Is Received
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lblisreceived" Style="text-align: center !important;" runat="server"></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%"/>
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                           Is Receivable
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                       <asp:Label ID="lbl_isreceivable" Style="text-align: center !important;" runat="server" ></asp:Label>
                                                                       </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" Width="3%"/>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField>
                                                                       <HeaderTemplate>
                                                                         Check
                                                                     <asp:CheckBox Visible="false" ID="chekboxall" runat="server" />
                                                                      </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                             <asp:Label ID="lbl_IsReceived" Style="text-align: center !important;" runat="server" Visible="false"
                                                                                Text='<%# Eval("IsReceived") %>'></asp:Label>
                                                                         <asp:Label ID="lblReceivable" Style="text-align: center !important;" runat="server" Visible="false"
                                                                                Text='<%# Eval("Receivable") %>'></asp:Label>
                                                                          <asp:CheckBox ID="chekboxselect" runat="server" />
                                                                        </ItemTemplate>
                                                                     <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                 </asp:TemplateField>
                                                                                                 
                                                                </Columns>
                                                                    <PagerSettings Mode="NumericFirstLast"  PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                            
                                                               </asp:GridView>

                                                        </ContentTemplate>

                                                    </asp:UpdatePanel>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                               <div class="row">
                                
                                    <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Payment Mode</span>
                                        <asp:DropDownList ID="ddlpaymentmode" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span9" class="input-group-addon cusspan" runat="server">Bank Name</span>
                                        <asp:HiddenField runat="server" ID="hdnbankID" />
                                        <asp:TextBox runat="server" ReadOnly="True" MaxLength="25" Class="form-control input-sm col-sm custextbox"
                                            ID="txtbank"></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnUpdate" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Update" OnClick="btupdate_Click" />
                                     </div>
                                </div>
                            </div>
                               <div class="row">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click"/>
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
                                   </contenttemplate>

                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
