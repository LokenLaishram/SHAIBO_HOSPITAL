<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="RadioHeaderMaster.aspx.cs" Inherits="Mediqura.Web.MedUtility.RadioHeaderMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src='<%= this.ResolveClientUrl("/Scripts/tinymce/tinymce.min.js") %>'></script>
    <script>

        tinymce.init({
            selector: 'textarea',
            plugins: 'image code ',
            height: '330',
            theme: 'modern',
            paste_data_images: true,
            images_upload_handler: function (blobInfo, success, failure) {
                success("data:" + blobInfo.blob().type + ";base64," + blobInfo.base64());
            },
            toolbar: 'undo redo | link image | code ',
            image_title: true,

            automatic_uploads: true,
            plugins: [
                'advlist autolink lists link charmap print preview hr anchor pagebreak spellchecker uploadimage',
                'searchreplace wordcount visualblocks visualchars code fullscreen ',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'template paste textcolor colorpicker textpattern imagetools codesample toc help emoticons hr'
            ],
            toolbar1: 'newdocument | print preview searchreplace | spellchecker | undo redo | insert uploadimage | bullist numlist outdent indent |   visualblocks fullscreen ',
            toolbar2: 'styleselect | fontselect | fontsizeselect | bold italic underline hr  | alignleft aligncenter alignright alignjustify | forecolor backcolor | removeformat',
            image_advtab: true,

        });



    </script>
    <asp:TabContainer ID="tabcontainerradioheadermaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabheader" runat="server" CssClass="Tab2" HeaderText="Radiology Header Master">
            <ContentTemplate>
                <asp:Panel ID="panel2" runat="server">
                    <div class="custab-panel" id="panelassignbed">

                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div1" runat="server">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Group</span>
                                    <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                        ID="ddl_group">
                                    </asp:DropDownList>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txt_code" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" Enabled="True"></asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Code</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" MaxLength="50"
                                        ID="txt_code"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txt_code" ValidChars="0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" Enabled="True"></asp:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span2" class="input-group-addon cusspan" runat="server">Header Name</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="50"
                                        ID="txt_headername"></asp:TextBox>

                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txt_headername" ValidChars="()0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ " Enabled="True"></asp:FilteredTextBoxExtender>

                                </div>
                            </div>
                            <div class="col-sm-3">
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
                            <div class="col-sm-12">
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
                                <div class="pbody col-sm-9">
                                    <div class="grid" style="float: left; width: 100%; height: 75vh; overflow: auto">
                                        <textarea style="width: 99.5%;" id="txtReport" runat="server"></textarea>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="row">
                                        <div class="grid" style="float: left; width: 100%; height: 75vh; overflow: auto">
                                            <asp:GridView ID="GvRadioHeader" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="GvRadioHeader_RowCommand"
                                                Width="100%" HorizontalAlign="Center">
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
                                                            Code
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("ID") %>'></asp:Label>
                                                            <asp:Label ID="lbl_code" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("HeaderCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Header Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_headername" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("HeaderName") %>'></asp:Label>
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
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

