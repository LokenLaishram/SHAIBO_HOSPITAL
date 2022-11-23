<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="LabTestComment.aspx.cs" inherits="Mediqura.Web.MedLab.LabTestComment" validaterequest="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
    <asp:TabContainer ID="tabcontainerTestComment" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tab1" runat="server" CssClass="Tab1" HeaderText="Test List">
            <ContentTemplate>
                <div class="custab-panel" id="Div3">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span11" class="input-group-addon cusspan" runat="server">Patient Type</span>
                                <asp:DropDownList runat="server" ID="ddl_patienttype" AutoPostBack="true" Class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span12" class="input-group-addon cusspan" runat="server">UHID</span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="True"
                                    ID="txt_UHIDS"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                    ServiceMethod="GetUHID" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_UHIDS"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                                <asp:FilteredTextBoxExtender TargetControlID="txt_UHIDS" ID="FilteredTextBoxExtender2"
                                    runat="server" ValidChars="0123456789"
                                    Enabled="True">
                                </asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span13" class="input-group-addon cusspan" runat="server">IPNo</span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="True"
                                    ID="txt_ipnumber"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                    ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_ipnumber"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span14" class="input-group-addon cusspan" runat="server">INV Number </span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="txt_invnumber"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                    ServiceMethod="GetInv" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_invnumber"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group input-group">
                                <span id="Span19" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" Style="z-index: 3"
                                    ID="txt_patientnames"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                    ServiceMethod="GetLabPatientName" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_patientnames"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group input-group">
                                <span id="Span20" class="input-group-addon cusspan" runat="server">Test Name </span>
                                <asp:TextBox runat="server" AutoPostBack="true" Class="form-control input-sm col-sm "
                                    ID="txt_testnames"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                    ServiceMethod="GetTestNames" MinimumPrefixLength="1"
                                    CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_testnames"
                                    UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span7" class="input-group-addon cusspan" runat="server">Consultant</span>
                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                    ID="ddl_referal">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span15" class="input-group-addon cusspan" runat="server">Date From <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="txtdate_from"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                    TargetControlID="txtdate_from" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate_from" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span16" class="input-group-addon cusspan" runat="server">Date To <span
                                    style="color: red">*</span></span>
                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="txtdate_to"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                    TargetControlID="txtdate_to" />
                                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                    Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdate_to" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span17" class="input-group-addon cusspan" runat="server">Status</span>
                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                    ID="ddl_status">
                                    <asp:ListItem Value="1">No Entry</asp:ListItem>
                                    <asp:ListItem Value="2">Entry Done</asp:ListItem>
                                    <asp:ListItem Value="3">Verified</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                    <asp:Button ID="btn_search" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnsearch_Click" Text="Search" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div6" runat="server">
                                    <asp:Label ID="lbl_result" runat="server" Height="13px"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row cusrow pad-top ">
                        <div class="col-sm-12">
                            <div>
                                <div class="pbody">
                                    <div class="grid" style="float: left; width: 100%; height: 50vh; overflow: auto">
                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:GridView ID="gv_labtestlist" runat="server" CssClass="table-hover grid_table result-table"
                                            EmptyDataText="No record found..." OnRowUpdating="gv_labtestlist_OnRowUpdating"
                                            OnRowDataBound="gv_labtestlist_RowDataBound" OnRowCommand="gv_labtestlist_RowCommand" DataKeyNames="ID"
                                            AutoGenerateColumns="False"
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
                                                        Inv.No.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lbl_urgencyid" Visible="false" CssClass="cusDropDown" Font-Size="Small" Height="22px" Text='<%# Eval("UrgencyID") %>' Width="250px"></asp:Label>
                                                        <asp:Label ID="lvl_inv" ForeColor="#ffccff" runat="server" Text='<%# Eval("Investigationumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Test Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTestID" Visible="false" runat="server" Text='<%# Eval("LabServiceID")%>'></asp:Label>
                                                        <asp:Label ID="lbl_reportType" Visible="false" runat="server" Text='<%# Eval("ReportTypeID")%>'></asp:Label>
                                                        <asp:Label ID="lbl_UHID" Visible="false" runat="server" Text='<%# Eval("UHID")%>'></asp:Label>
                                                        <asp:LinkButton ID="lbl_test" Font-Underline="true" ForeColor="Red" CommandName="Search" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none" runat="server" Style="text-align: left !important;" Text='<%# Eval("TestName") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("PatientName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="12%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Recving Time
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_reciestatus" Visible="false" Style="text-align: left !important;" runat="server" Text='<%# Eval("IsMLTreceived") %>'></asp:Label>
                                                        <asp:Label ID="lbl_recvTime" Style="text-align: left !important;" runat="server" Text='<%# Eval("MLTrecievingDatetime","{0:dd-MM-yyyy:hh:mm:ss tt}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                </asp:TemplateField>
                                                <asp:ButtonField CommandName="Update" HeaderText="Update Recieving Time" ItemStyle-Width="1%" ButtonType="Button" Text="Update" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tab3" runat="server" CssClass="Tab1" HeaderText="Report Editor">
            <ContentTemplate>
                <div class="row cusrow pad-top ">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="div4" runat="server">
                            <asp:Label ID="lbl_message1" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="pbody">
                            <div class="grid" style="float: left; width: 100%; height: 75vh; overflow: auto">
                                     <asp:HiddenField ID="hdntestID" runat="server" />
                                     <asp:HiddenField ID="hdnUHID" runat="server" />
                                     <asp:HiddenField ID="hdnInvNumber" runat="server" />
                                <textarea style="width: 99.5%;" id="txt_template" class="txtLabReport" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="height: 5px">
                    </div>
                    <div class="col-sm-12">
                        <div class="col-sm-5">
                            <div class="form-group input-group">
                            </div>
                        </div>
                        <div class="col-sm-3" id="div2" runat="server">
                            <div class="form-group input-group">
                                <span id="Span9" class="input-group-addon cusspan" runat="server">Verified By</span>
                                <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox"
                                    ID="ddl_cverifiedby">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="Cbtn_save" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" OnClick="btnsave_Click" Text="Save" />
                                <asp:Button ID="Cbtn_print" Visible="false" runat="server" Class="btn  btn-sm cusbtn" UseSubmitBehavior="False" Text="Print" OnClick="Cbtn_print_Click" />
                                <asp:Button ID="Cbtn_reset" runat="server" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" Text="Reset" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>

