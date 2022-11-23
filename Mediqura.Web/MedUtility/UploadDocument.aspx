<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="UploadDocument.aspx.cs" Inherits="Mediqura.Web.MedUtility.UploadDocument" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">

     <asp:TabContainer ID="tabcontainerUpload" runat="server" CssClass="Tab" ActiveTabIndex="1"
        Width="100%">
        <asp:TabPanel ID="tabOTstatus" runat="server" CssClass="Tab1" HeaderText="Document Uploader">
            <ContentTemplate>
                <asp:Panel ID="panel2" runat="server" DefaultButton="btn_save">
                    <div class="custab-panel" id="panel_Upload">

                                <div class="fixeddiv">
                                    <div class="row fixeddiv" id="div1" runat="server">
                                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span1" class="input-group-addon cusspan" runat="server">IPNo</span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_IPNo" AutoPostBack="True" OnTextChanged="txt_IPNo_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_IPNo" ID="FilteredTextBoxExtender1"
                                                runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" - :" Enabled="True">
                                            </asp:FilteredTextBoxExtender>

                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span2" class="input-group-addon cusspan" runat="server">Name</span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_name" AutoPostBack="True" OnTextChanged="txt_name_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_name"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txt_name" ID="FilteredTextBoxExtender2"
                                                runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" " Enabled="True">
                                            </asp:FilteredTextBoxExtender>


                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                           
                                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_address"></asp:TextBox>
                                                
                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span3" class="input-group-addon cusspan" runat="server">Title</span>
                                           
                                                    <asp:TextBox runat="server"  Class="form-control input-sm col-sm custextbox"
                                                        ID="txt_tittle"></asp:TextBox>
                                                
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span7" class="input-group-addon cusspan" runat="server">Upload File <span
                                                style="color: red">*</span> </span>

                                             <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                          
                                          
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">
                                            <asp:Button ID="btnadd" runat="server" UseSubmitBehavior="False" Text="Add" Class="btn  btn-sm cusbtn" OnClick="btnadd_Click"/>
                                             
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
                                                        Width="100%" HorizontalAlign="Center" OnRowCommand="gvFileUpload_RowCommand" >
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
                                                                    IPNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIPNo" runat="server" Text='<%# Eval("IPNo")%>'></asp:Label>
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
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    FilePath
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfilePath" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("FilePath") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    ContentType
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcontentType" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("contentType") %>'></asp:Label>
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



                                <div class="row">
                                    <div class="col-lg-8"></div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group cuspanelbtngrp  pull-right">

                                            <asp:Button ID="btn_save" runat="server" Text="Save" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClick="btn_save_Click"  />
                                              <asp:Button ID="btn_reset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btn_reset_Click"  />

                                        </div>
                                    </div>
                                </div>
                                

                    </div>
                </asp:Panel>
            </ContentTemplate>

        </asp:TabPanel>
          <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab1" HeaderText="Document UploadList">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server" DefaultButton="btnsearch">
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
                                            <span id="Span4" class="input-group-addon cusspan" runat="server">IPNo</span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtIPNo" AutoPostBack="True" OnTextChanged="txtIPNo_TextChanged" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtIPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtIPNo" ID="FilteredTextBoxExtender3"
                                                runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" -" Enabled="True">
                                            </asp:FilteredTextBoxExtender>

                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" runat="server">Name</span>

                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txtName" AutoPostBack="True" OnTextChanged="txtName_TextChanged"  ></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                ServiceMethod="GetPatientName" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtName"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="">
                                            </asp:AutoCompleteExtender>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtName" ID="FilteredTextBoxExtender4"
                                                runat="server" FilterType="Custom, UppercaseLetters, LowercaseLetters"
                                                ValidChars=" " Enabled="True">
                                            </asp:FilteredTextBoxExtender>


                                        </div>
                                    </div>
                                    

                                </div>
                               <div class="row">
                                    <div class="col-sm-4">
                                    <div class="form-group input-group">
                                    <span id="Span6" class="input-group-addon cusspan" runat="server">Date From <span
                                    style="color: red">*</span></span>
                                   <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                    ID="txtdatefrom" ></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                    TargetControlID="txtdatefrom" />
                                </div>
                            </div>
                          <div class="col-sm-4">
                            <div class="form-group input-group">
                                <span id="Span8" class="input-group-addon cusspan" runat="server">Date To <span
                                    style="color: red">*</span> </span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                    ID="txtto"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                    TargetControlID="txtto" />
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-8"></div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />

                            </div>
                        </div>
                    </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="fixeddiv">
                                            <div class="row fixeddiv" id="div4" runat="server">
                                                <asp:Label ID="lblresultList" runat="server" Height="13px"></asp:Label>
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
                                                        Width="100%" HorizontalAlign="Center" OnRowCommand="gvUploadList_RowCommand"  >
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
                                                                    IPNo
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("IPNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Name
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Tittle
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                     <asp:Label ID="code" Visible="false" runat="server" Text='<%# Eval("fileID")%>'></asp:Label>
                                                                    <asp:Label ID="lbltitle" runat="server" Text='<%# Eval("Tittle")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>

                                                                <HeaderTemplate>
                                                                    FileName
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFname" runat="server" Text='<%# Eval("filename")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    FilePath
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblfilePath" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("FilePath") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    UploadedDate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUploadDate" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}") %>'></asp:Label>
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
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Deletes" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none"
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
