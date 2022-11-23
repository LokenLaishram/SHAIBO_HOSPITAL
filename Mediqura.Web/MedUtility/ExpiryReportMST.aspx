<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" validateRequest="false" CodeBehind="ExpiryReportMST.aspx.cs" Inherits="Mediqura.Web.MedUtility.ExpiryReportMST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
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
    <asp:TabContainer ID="tabcontainerlabgroupmaster" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabbedassign" runat="server" CssClass="Tab2" HeaderText="Expiry Report MST">
            <ContentTemplate>
                <asp:Panel ID="panel2" runat="server">
                    <div class="custab-panel" id="panelassignbed">

                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div1" runat="server">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </div>
                        </div>
                         <div class="row cusrow pad-top ">
                            <div class="col-sm-12">
                                <div class="pbody">
                                    <div class="grid" style="float: left; width: 100%; height: 65vh; overflow: auto">

                                        <textarea style="width: 99.5%;" id="txtReport" runat="server"></textarea>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                    <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
                                    <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                </div>
                            </div>
                        </div>

                    </div>


                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
