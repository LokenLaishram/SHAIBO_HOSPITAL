﻿<%@ Page Title="Bank Type" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="BankTypeMaster.aspx.cs" Inherits="Mediqura.Web.MedBank.BankTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        var Page
        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance()
            Page.add_initializeRequest(OnInitializeRequest)

        }

        function OnInitializeRequest(sender, args) {

            var postBackElement = args.get_postBackElement()

            if (Page.get_isInAsyncPostBack()) {
                ddl_department_SelectedIndexChanged
                alert('One request is already in progress....')
                args.set_cancel(true)
            }
        }
    </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerOTRolesmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabOTRolesmaster" runat="server" CssClass="Tab2" HeaderText="Bank Type Master">
                    <ContentTemplate>
                        <asp:Panel ID="panel2" runat="server" DefaultButton="btnsave">
                            <div class="custab-panel" id="panelOTRolesmaster">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">Code</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                                ID="txt_Bankcode"></asp:TextBox>
                                           <asp:FilteredTextBoxExtender TargetControlID="txt_Bankcode" ID="FilteredTextBoxExtender1"
                                                runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" " Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                          </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Description</span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_BankDescription"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_BankDescription" ID="FilteredTextBoxExtender2"
                                                runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" " Enabled="True">
                                            </asp:FilteredTextBoxExtender>
                                         </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Status</span>
                                            <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                                <asp:ListItem Value="0">Active</asp:ListItem>
                                                <asp:ListItem Value="1">Inactive</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                            <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress2" runat="server"><ProgressTemplate>
                                                            <div id="DIVloading" class="text-center loading" runat="server">
                                                                <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>                                                     
                                                              </ProgressTemplate>
                                                      </asp:UpdateProgress>
                                                      <asp:GridView ID="GvBankType" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                        Width="100%" HorizontalAlign="Center" OnPageIndexChanging="GvBankType_PageIndexChanging" OnRowCommand="GvBankType_RowCommand"   >
                                                          <Columns>
                                                             <asp:TemplateField>
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
                                                                        BankCode                                                    
                                                                       </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                    <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("BankTypeID")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_bankcode" Style="text-align: left !important;" runat="server" Text='<%# Eval("BankTypeCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField><HeaderTemplate>
                                                                    Description
                                                                 </HeaderTemplate>
                                                                   <ItemTemplate>
                                                                    <asp:Label ID="lbl_bankdescp" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BankTypedescp") %>'></asp:Label>
                                                                
                                                                      </ItemTemplate>
                                                                     <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField><HeaderTemplate>
                                                                    Added By
                                                                 </HeaderTemplate>
                                                                   <ItemTemplate>
                                                                    <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                                  </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                                  </asp:TemplateField>
                                                                  <asp:TemplateField><HeaderTemplate>
                                                                    Added On
                                                                
                                                                    </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                    <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                                
                                                                       </ItemTemplate>

                                                                      <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                      </asp:TemplateField>
                                                                      <asp:TemplateField><HeaderTemplate>
                                                                    Remarks
                                                                
                                                                     </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="200px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                
                                                                     </ItemTemplate>

                                                                  <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                   </asp:TemplateField>
                                                                   <asp:TemplateField><HeaderTemplate>
                                                                    <span class="cus-Edit-header">Edit</span>
                                                                       
                                                                        </HeaderTemplate>
                                                                     <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="Edits" ForeColor="Blue">
                                                <i class="fa fa-pencil-square-o  cus-edit-color"></i>
                                                                    </asp:LinkButton>
                                                                
                                                                      </ItemTemplate>

                                                                     <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                                     </asp:TemplateField>
                                                                     <asp:TemplateField><HeaderTemplate>
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

                                                            <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                       </asp:GridView>


                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
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
                               
                        </asp:Panel>
                      </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


