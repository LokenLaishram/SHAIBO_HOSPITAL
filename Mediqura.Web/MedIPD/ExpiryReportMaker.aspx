<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ExpiryReportMaker.aspx.cs" Inherits="Mediqura.Web.MedIPD.ExpiryReportMaker" %>

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
    <asp:TabContainer ID="tabexpiryrep" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabexpiry" runat="server" CssClass="Tab2" HeaderText="Expiry Report Maker">
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
                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                    <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddl_patient_type_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">IPD</asp:ListItem>
                                        <asp:ListItem Value="2">Emergency</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="lbl_uhid" class="input-group-addon cusspan" runat="server" style="color: red">IPNo</span>

                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="true"
                                        ID="txt_IPNo" AutoPostBack="True" OnTextChanged="txt_IPNo_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IPNo"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server" style="color: red">Emergency No.</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="true"
                                        ID="txt_emerno" AutoPostBack="True" OnTextChanged="txt_emerno_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                        ServiceMethod="GetEmrgNo" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_emerno"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="lbl_name" class="input-group-addon cusspan" runat="server">Name</span>
                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_name"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span2" class="input-group-addon cusspan" runat="server">Age</span>
                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_age"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="lbl_gender" class="input-group-addon cusspan" runat="server">Gender</span>

                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_gender"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>

                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_address"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span7" class="input-group-addon cusspan" runat="server">Nature of Death<span
                                        style="color: red">*</span></span>
                                    <asp:DropDownList runat="server" Class="form-control input-sm col-sm cus-long-textbox" MaxLength="50"
                                        ID="ddl_manner">
                                        <asp:ListItem Value="1">Normal</asp:ListItem>
                                        <asp:ListItem Value="2">Accident</asp:ListItem>
                                    </asp:DropDownList>

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
                        <div class="row cusrow pad-top ">
                            <div class="col-sm-12">
                                <div class="pbody">
                                    <div class="grid" style="float: left; width: 100%; height: 65vh; overflow: auto">

                                        <textarea style="width: 99.5%;" id="txtReport" runat="server"></textarea>

                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>


                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>

        <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab1" HeaderText="Expiry Report List">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server">
                    <div class="custab-panel" id="Div2">
                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div3" runat="server">
                                <asp:Label ID="lblmessage1" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList runat="server" ID="ddl_patienttype" class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnSelectedIndexChanged="ddl_patienttype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">IPD</asp:ListItem>
                                                <asp:ListItem Value="2">Emergency</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_patienttype" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span5" class="input-group-addon cusspan" runat="server" style="color: red">IPNo</span>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="true"
                                                ID="txtIPNo" AutoPostBack="True" OnTextChanged="txtIPNo_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtIPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span6" class="input-group-addon cusspan" runat="server" style="color: red">Emergency No.</span>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" ReadOnly="true"
                                                ID="txtEmergno" AutoPostBack="True" OnTextChanged="txtEmergno_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                                ServiceMethod="GetEmrgNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtEmergno"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span8" class="input-group-addon cusspan" runat="server">Name</span>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txtname1"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span9" class="input-group-addon cusspan" runat="server">Age</span>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>

                                            <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_Age1"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span10" class="input-group-addon cusspan" runat="server">Gender</span>
                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txtgen"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span11" class="input-group-addon cusspan" runat="server">Address</span>
                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txtaddress"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span12" class="input-group-addon cusspan" runat="server">Date From </span>

                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txtdatefrom"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtdatefrom" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefrom" />
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span13" class="input-group-addon cusspan" runat="server">Date To </span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txtdateto"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtdateto" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdateto" />
                                </div>
                            </div>

                            <div class="col-sm-9">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                    <asp:Button ID="btnsearch1" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                    <asp:Button ID="btnreset" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnreset_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div4" runat="server">
                                <asp:Label ID="lblresult" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row cusrow ">
                            <div class="col-sm-12">
                                <div>
                                    <div class="pbody">
                                        <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="GvreportList" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="gGvreportList_RowCommand"
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
                                                            IPNo/Emergency No.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                                                            <asp:Label ID="lblipno" runat="server" Text='<%# Eval("IPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblname" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("PatientName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Address
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Report On
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldept" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Report By
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <span class="cus-Edit-header">View</span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="View" ForeColor="Blue">
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
                                                <HeaderStyle BackColor="#D8EBF5" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-8">
                                <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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


                    </div>


                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>

    </asp:TabContainer>

</asp:Content>
