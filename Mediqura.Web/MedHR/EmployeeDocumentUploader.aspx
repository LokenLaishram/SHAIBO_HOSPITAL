<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="EmployeeDocumentUploader.aspx.cs" Inherits="Mediqura.Web.MedHR.EmployeeDocumentUploader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
  
    <asp:TabContainer ID="tabcontainerUploadEmpDoc" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabUploadEmpDoc" runat="server" CssClass="Tab1" HeaderText="Employee Document Uploader">
            <ContentTemplate>
                <asp:Panel ID="panel2" runat="server" DefaultButton="btnadd">
                    <div class="custab-panel" id="panel_UploadEmpDoc">
                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div1" runat="server">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-5">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Employee Name</span>

                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-span"
                                        ID="txtEmpName"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                        ServiceMethod="GetEmpdetails" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtEmpName"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span2" class="input-group-addon cusspan" runat="server">Document Type</span>
                                    <asp:DropDownList ID="ddlDocType" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                        runat="server">
                                        <asp:ListItem Value="0" Text="---Select--"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Certificate"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="MarkSheet"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span4" class="input-group-addon cusspan" runat="server">Title</span>

                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_tittle"></asp:TextBox>

                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="form-group input-group">
                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Upload File <span
                                        style="color: red">*</span> </span>
                                    
                                    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                                             


                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                 
                                    <asp:Button ID="btnadd" runat="server" UseSubmitBehavior="False" Text="Add" Class="btn  btn-sm cusbtn" OnClick="btnadd_Click" />
                                                          
                                                          

                                </div>
                            </div>
                        </div>


                        <div class="row cusrow pad-top ">
                            <label class="gridview-label">File List</label>
                            <div class="col-sm-12">
                                <div>
                                    <div class="pbody">
                                        <div class="gridview-container-Large ">
                                            <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                                <asp:GridView ID="gvFileUpload" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..."
                                                    Width="100%" HorizontalAlign="Center" OnRowCommand="gvFileUpload_RowCommand">
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
                                                                EmployeeID
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempno" runat="server" Text='<%# Eval("EmployeeID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Tittle
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Tittle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>

                                                            <HeaderTemplate>
                                                                FileName
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFname" runat="server" Text='<%# Eval("FileName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                FilePath
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblfilePath" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("FilePath") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                ContentType
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcontentType" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("contentType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <HeaderTemplate>
                                                                DocType
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                  <asp:Label ID="lbldocID" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("docID") %>'></asp:Label>
                                                                <asp:Label ID="lbldocType" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("docType") %>'></asp:Label>
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
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-8"></div>
                        </div>

                        <div class="row">
                            <div class="col-lg-8"></div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">

                                    <asp:Button ID="btn_save" runat="server" Text="Save" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClick="btn_save_Click" />

                                    <asp:Button ID="btn_reset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btn_reset_Click" />

                                </div>
                            </div>
                        </div>

                        <asp:HiddenField ID="hdnfile" runat="server" />
                    </div>
                </asp:Panel>
            </ContentTemplate>

        </asp:TabPanel>
        <asp:TabPanel ID="tabUploadEmpDoclist" runat="server" CssClass="Tab1" HeaderText="Employee Document List">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server" DefaultButton="btn_search">
                    <asp:UpdatePanel ID="cuspanel" runat="server">
                        <ContentTemplate>
                            <div class="custab-panel" id="Div2">
                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div3" runat="server">
                                        <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Employee Name <span
                                                style="color: red">*</span></span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm cus-long-span"
                                                ID="txt_EmpName"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetEmpdetails" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_EmpName"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span6" class="input-group-addon cusspan" runat="server">Document Type</span>
                                            <asp:DropDownList ID="ddl_DocType" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                runat="server">
                                                <asp:ListItem Value="0" Text="---Select--"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Certificate"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="MarkSheet"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btn_search" runat="server" Text="Search" Class="btn  btn-sm cusbtn" OnClick="btn_search_Click" />
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
                                                <div class="grid" style="float: left; width: 100%; height: 45vh; overflow: auto">
                                                    <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <div id="DIV5" class="text-center loading" runat="server">
                                                                <asp:Image ID="Image1" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                    AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>

                                                    <asp:GridView ID="gvUploadList" runat="server" AutoGenerateColumns="False" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="No record found..."
                                                        Width="100%" HorizontalAlign="Center" OnRowCommand="gvUploadList_RowCommand">
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
                                                                    EmployeeID
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("fileID")%>'></asp:Label>
                                                                    <asp:Label ID="lblfilePath" Style="text-align: left !important;" runat="server" Visible="false"
                                                                    Text='<%# Eval("FilePath") %>'></asp:Label>
                                                                    <asp:Label ID="lblempno" runat="server" Text='<%# Eval("EmployeeID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField >
                                                            <HeaderTemplate>
                                                                DocType
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldocType" Style="text-align: left !important;" runat="server" 
                                                                    Text='<%# Eval("docType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Tittle
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Tittle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <span class="cus-Edit-header">FileName</span>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                   <asp:LinkButton runat="server" ID="lnkView" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" Font-Underline="true" Text='<%# Eval("FileName")%>'
                                                                    CommandName="VIEW"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Remarks
                                                                
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtremarks" Width="200px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>

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
                                                        <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="Right" Height="1em" Width="2%" />
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </asp:Panel>
            </ContentTemplate>

        </asp:TabPanel>

    </asp:TabContainer>

</asp:Content>
