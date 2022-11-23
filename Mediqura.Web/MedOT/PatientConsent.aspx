﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="PatientConsent.aspx.cs" ValidateRequest="false" Inherits="Mediqura.Web.MedOT.PatientConsent" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
 
    <script src='<%= this.ResolveClientUrl("/Scripts/tinymce/tinymce.min.js") %>'></script>
    <script>

        tinymce.init({
            init_instance_callback: function (editor) {
                editor.on('KeyUp', function (e) {
                    checkForSpecial(tinymce.get('<%=txtReport.ClientID%>').getContent());
                    if (e.keyCode == '32') {
                        removeSearch();
                    }
                });
                editor.on('KeyDown', function (e) {
                    if (e.keyCode == '13') {
                        //  removeSearch();
                    }

                });
            },
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

            function checkForSpecial(string) {

                if (string.match(/@[a-zA-Z0-9]*/)) {
                    var string = string.match(/@[a-zA-Z0-9]*/);
                    var cursorIndex = getCursorPosition(tinyMCE.activeEditor);
                    // alert(cursorIndex);
                    if (string != null || string != "") {
                        var searchParameter = string[0].substr(1);
                        $('#autohelper').show();
                        laodautocomplete(searchParameter);
                    } else {
                        $('#autohelper').hide();
                    }
                } else {
                    if (string.match(/#[a-zA-Z0-9]*/)) {
                        var strings = string.match(/#[a-zA-Z0-9]*/);

                        if (strings != null || strings != "") {
                            var searchParameters = strings[0].replace("#", "");
                            $('#autohelper').show();
                            laodMedicineautocomplete(searchParameters);
                        } else {
                            $('#autohelper').hide();
                        }
                    }
                }
            }
            function getCursorPosition(editor) {
                var bm = editor.selection.getBookmark(0);

                //select the bookmark element
                var selector = "[data-mce-type=bookmark]";
                var bmElements = editor.dom.select(selector);

                //put the cursor in front of that element
                editor.selection.select(bmElements[0]);
                editor.selection.collapse();

                //add in my special span to get the index...
                //we won't be able to use the bookmark element for this because each browser will put id and class attributes in different orders.
                var elementID = ("######cursor######");
                var positionString = '<span id="' + elementID + '"></span>';
                editor.selection.setContent(positionString);

                //get the content with the special span but without the bookmark meta tag
                var content = editor.getContent({ format: "html" });
                //find the index of the span we placed earlier
                var index = content.indexOf(positionString);

                //remove my special span from the content
                editor.dom.remove(elementID, false);

                //move back to the bookmark
                editor.selection.moveToBookmark(bm);

                return index;
            }
            function setCursorPosition(editor, index) {
                //use the format: html to strip out any existing meta tags
                var content = editor.getContent({ format: "html" });

                //split the content at the given index
                var part1 = content.substr(0, index);
                var part2 = content.substr(index);

                //create a bookmark... bookmark is an object with the id of the bookmark
                var bookmark = editor.selection.getBookmark(0);

                //this is a meta span tag that looks like the one the bookmark added... just make sure the ID is the same
                var positionString = '<span id="' + bookmark.id + '_start" data-mce-type="bookmark" data-mce-style="overflow:hidden;line-height:0px"></span>';
                //cram the position string inbetween the two parts of the content we got earlier
                var contentWithString = part1 + positionString + part2;

                //replace the content of the editor with the content with the special span
                //use format: raw so that the bookmark meta tag will remain in the content
                editor.setContent(contentWithString, ({ format: "raw" }));

                //move the cursor back to the bookmark
                //this will also strip out the bookmark metatag from the html
                editor.selection.moveToBookmark(bookmark);

                //return the bookmark just because
                return bookmark;
            }
            function removeSearch() {
                string = tinymce.get('<%=txtReport.ClientID%>').getContent();
            console.log(string);
            var cursorIndex = getCursorPosition(tinyMCE.activeEditor);
            if (string.match(/@[a-zA-Z0-9]*/)) {
                var value = string.match(/@[a-zA-Z0-9]*/);

                if (value != null || value != "") {

                    var string = string.replace(value[0], "").toString();
                    tinyMCE.activeEditor.setContent(string);

                    setCursorPosition(tinyMCE.activeEditor, cursorIndex);
                    $('#autohelper').hide();


                }
            } else {
                if (string.match(/#[a-zA-Z0-9]*/)) {
                    var valueNew = string.match(/#[a-zA-Z0-9]*/);
                    if (valueNew != null || valueNew != "") {
                        var string = string.replace(valueNew[0], "");
                        tinyMCE.activeEditor.setContent(string);
                        setCursorPosition(tinyMCE.activeEditor, cursorIndex);
                        $('#autohelper').hide();

                    }
                }
            }


        }
        function loaddata(desease, code) {
            var cursorIndex = getCursorPosition(tinyMCE.activeEditor);
            var string = tinymce.get('<%=txtReport.ClientID%>').getContent();
            var value = string.match(/@[a-zA-Z0-9]*/);
            if (value != null || value != "") {
                var string = string.replace(value[0], desease);
                var string = string.replace("[ICDCODE]", code + ", [ICDCODE]");
                tinyMCE.activeEditor.setContent(string);
                tinyMCE.activeEditor.focus();
                setCursorPosition(tinyMCE.activeEditor, cursorIndex);
                $('#autohelper').hide();

            }
        }
        function loadMedData(Medicine) {
            var cursorIndex = getCursorPosition(tinyMCE.activeEditor);
            var string = tinymce.get('<%=txtReport.ClientID%>').getContent();
            var value = string.match(/#[a-zA-Z0-9]*/);
            if (value != null || value != "") {
                var string = string.replace(value[0], Medicine);
                tinyMCE.activeEditor.setContent(string);
                tinyMCE.activeEditor.focus();
                setCursorPosition(tinyMCE.activeEditor, cursorIndex);
                $('#autohelper').hide();

            }
        }

        function laodautocomplete(keyword) {
            if (keyword != "") {
                var url = '<%= this.ResolveClientUrl("/MedIPD/GetICD.ashx?key=") %>' + keyword;
                callServiceToFetchData(url, serverReply);
            } else {
                $('#autohelper').hide();
            }


        }

        function serverReply(response) {
            try {
                var data = "";
                var jsonData = JSON.parse(response);
                for (var i = 0; i < jsonData.length; i++) {
                    var row = jsonData[i];
                    data = data + "<a onclick=\"loaddata('" + row.DeseaseName + "','" + row.ICDCODE + "')\" href='#'>" + row.DeseaseName + "</a>";
                }
                document.getElementById('autohelper').innerHTML = data
            } catch (e) {


            }
        }
        function laodMedicineautocomplete(keyword) {
            if (keyword != "") {
                var url = '<%= this.ResolveClientUrl("/MedIPD/GetMedicine.ashx?key=") %>' + keyword;
                callServiceToFetchData(url, serverReplyMed);
            } else {
                $('#autohelper').hide();
            }
        }

        function serverReplyMed(response) {
            try {
                var data = "";
                var jsonData = JSON.parse(response);
                for (var i = 0; i < jsonData.length; i++) {
                    var row = jsonData[i];
                    data = data + "<a onclick=\"loadMedData('" + row.MedName + "')\" href='#'>" + row.MedName + "</a>";
                }
                document.getElementById('autohelper').innerHTML = data
            } catch (e) {


            }
        }
    </script>
    <asp:TabContainer ID="tabContainerPatientConsent" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabPatientConsent" runat="server" CssClass="Tab2" HeaderText="Patient Consent">
            <ContentTemplate>
                <div id="autohelper" class="dropdown-content"> 
                </div>
                <asp:Panel ID="panel2" runat="server">
                    <div class="custab-panel" id="panelPatientConsent">

                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="div1" runat="server">
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row">
                             <div class="col-sm-4">

                                        <div class="form-group input-group">
                                            <span id="Span5" class="input-group-addon cusspan" style="color: red" runat="server">IPNo  <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_IPNo" AutoPostBack="True" OnTextChanged="txt_IPNo_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                ServiceMethod="GetOTIPNo" MinimumPrefixLength="1"
                                                CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IPNo"
                                                UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                            </asp:AutoCompleteExtender>

                                        </div>

                                    </div>
                           <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="lbl_name" class="input-group-addon cusspan" runat="server">Name</span>
                                    <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_name"></asp:TextBox>
                                </div>
                            </div>
                             <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblnurse" class="input-group-addon cusspan" runat="server">Employee <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3" AutoPostBack="True"
                                                ID="ddl_employee" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                        </div>
                        <div class="row">
                             <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblco" class="input-group-addon cusspan" runat="server">Relative Name <span
                                                style="color: red">*</span></span>
                                            <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                ID="txt_relative"></asp:TextBox>
                                        </div>
                            </div>
                          <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lblrelationship" class="input-group-addon cusspan" runat="server">Relationship <span
                                                style="color: red">*</span></span>
                                            <asp:DropDownList ID="ddlrelationship" runat="server" class="form-control input-sm col-sm custextbox" AutoPostBack="True" OnSelectedIndexChanged="ddlrelationship_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                          </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span4" class="input-group-addon cusspan" runat="server">Consent Type </span>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_consentType" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_consentType_SelectedIndexChanged"></asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ddl_consentType" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            </div>
                         <div class="row">
                            <asp:Label ID="lblIP" Visible="False" runat="server"></asp:Label>
                             <div class="col-sm-8">
                             </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">

                                    <asp:Button ID="btnSearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
                                    <asp:Button ID="btnsave" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />
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
                                    <div class="row cusrow pad-top ">
                                        <div class="col-sm-12">
                                            <div>
                                                <div class="pbody">
                                                    <div class="grid" style="float: left; width: 100%; height: 48vh; overflow: auto">
                                                        <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                            <ProgressTemplate>
                                                                <div id="DIVloading" class="text-center loading" runat="server">
                                                                    <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                        AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                                </div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                        <asp:GridView ID="gvConsent" runat="server" CssClass="table-hover grid_table result-table" AllowPaging="True"
                                                            EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                            Width="100%" HorizontalAlign="Center" OnRowCommand="gvConsent_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <span class="cus-Delete-header">View</span>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" ValidationGroup="none">
                                                                    View
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="1%" Font-Underline="True" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        IPNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_recordID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                        <%--<asp:Label ID="lbl_serialID" Visible="false" runat="server" Text='<%# Eval("SerialID") %>'></asp:Label>--%>
                                                                        <asp:Label ID="lblIPNo" Style="text-align: left !important;" runat="server"
                                                                            Text='<%# Eval("IPNo") %>'></asp:Label>
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
                                                                    <ItemStyle HorizontalAlign="Left" Width="4%" />
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
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
      <%--  <asp:TabPanel ID="tabpanel2" runat="server" HeaderText="Consent  List">
            <ContentTemplate>
                <asp:Panel ID="panel1" runat="server">
                    <div class="custab-panel" id="paneldepositlist">
                        <div class="fixeddiv">
                            <div class="row fixeddiv" id="divmsg2" runat="server">
                                <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Issue From <span
                                        style="color: red">*</span></span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txtdatefromList"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtdatefromList" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtdatefromList" />

                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span7" class="input-group-addon cusspan" runat="server">Issue To <span
                                        style="color: red">*</span> </span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txttoList"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txttoList" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txttoList" />

                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="lbl_uhid" class="input-group-addon cusspan" runat="server" style="color: red">IPNo  <span
                                        style="color: red"></span></span>

                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                        ID="txt_IPNoList" AutoPostBack="True" OnTextChanged="txt_IPNoList_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                        CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_IPNoList"
                                        UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group input-group">
                                    <span id="Span2" class="input-group-addon cusspan" runat="server">Discharge Type </span>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_ConsentTypeList" class="form-control input-sm col-sm custextbox"></asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_ConsentTypeList" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">

                                    <asp:Button ID="btnsearchList" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearchList_Click" />
                                    <asp:Button ID="btnresetList" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresetList_Click" />

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
                                            <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="DIVloading" class="text-center loading" runat="server">
                                                        <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                            AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <asp:GridView ID="gvConsentList" runat="server" CssClass="table-hover grid_table result-table" OnPageIndexChanging="gvConsentList_PageIndexChanging" AllowPaging="True"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowCommand="gvConsentList_RowCommand"
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
                                                            IPNo.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            <asp:Label ID="lblIPNo" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("IPNo") %>'></asp:Label>
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
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Consent Type
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDischargeBy" runat="server" Text='<%# Eval("DischargeTypedescp")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Added By
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="4%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Added On
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtremarks" Width="100px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <span class="cus-Delete-header">View</span>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex  %>" CommandName="View" ForeColor="Blue">
                                                <i class="fa fa-pencil-square-o  cus-edit-color"></i>
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
                            <div class="col-md-4"></div>
                            <div class="col-md-8">
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
                    </div>
                </asp:Panel>
            </ContentTemplate>



        </asp:TabPanel>--%>

    </asp:TabContainer>
</asp:Content>

