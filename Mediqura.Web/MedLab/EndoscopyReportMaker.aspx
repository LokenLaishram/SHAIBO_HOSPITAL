<%@ page title="" language="C#" masterpagefile="~/Mediqura.Master" autoeventwireup="true" codebehind="EndoscopyReportMaker.aspx.cs" validaterequest="false" inherits="Mediqura.Web.MedLab.EndoscopyReportMaker" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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

        function loadPatient(testId, testType, testLabSub, UHID, ID, PatientType) {
            document.getElementById('<%=txt_selected_test_ID.ClientID %>').value = testId;
            document.getElementById('<%=txt_selected_test_type.ClientID %>').value = testType;
            document.getElementById('<%=txt_selected_lab_sub.ClientID %>').value = testLabSub;
            document.getElementById('<%=txt_selected_UHID.ClientID %>').value = UHID;
            document.getElementById('<%=txt_selected_ID.ClientID %>').value = ID;
            document.getElementById('<%=txt_selected_patientType.ClientID %>').value = PatientType;

            var clickButton = document.getElementById("<%= btn_selected_postback.ClientID %>");
            clickButton.click();
        }
    </script>
    <asp:tabcontainer id="tabcontainer" runat="server" cssclass="Tab" activetabindex="0"
        width="100%">
        <asp:TabPanel ID="tabbedassign" runat="server" CssClass="Tab2" HeaderText="Endoscopy Report Maker">
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
                                    <span id="Span1" class="input-group-addon cusspan" runat="server">Patient Type<span style="color: red"> *</span></span>

                                    <asp:DropDownList runat="server" ID="ddl_patient_type" class="form-control input-sm col-sm custextbox">
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span3" class="input-group-addon cusspan" runat="server">Lab Sub-Group </span>

                                    <asp:DropDownList AutoPostBack="True" runat="server" ID="ddl_labsubgroup" class="form-control input-sm col-sm custextbox"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span2" class="input-group-addon cusspan" runat="server">UHID</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_UHID"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span4" class="input-group-addon cusspan" runat="server">Patient Name</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txt_patientName"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span12" class="input-group-addon cusspan" runat="server">Date From</span>
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
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span13" class="input-group-addon cusspan" runat="server">Date To </span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txtto"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy, dd-MM-yyyy"
                                        TargetControlID="txtto" />
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureAMPMPlaceholder=""
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                        CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                        Enabled="True" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="txtto" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span5" class="input-group-addon cusspan" runat="server">Header Type<span
                                                style="color: red">*</span></span>
                                   <asp:DropDownList runat="server" ID="ddl_header" class="form-control input-sm col-sm custextbox">
                                    </asp:DropDownList>
                                </div>
                            </div>
                               <div class="col-sm-3">
                                <div class="form-group input-group">
                                    <span id="Span6" class="input-group-addon cusspan" runat="server">Doctor</span>
                                    <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                        ID="txtDoctor"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">

                                    <asp:Button ID="btnSearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />
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
                                    <div class="row"  >
                                        <asp:TabContainer ID="tabradioReport" runat="server" CssClass="Tab" ActiveTabIndex="0" Width="100%">
                                            <asp:TabPanel ID="TabPanel1" runat="server" CssClass="Tab2" HeaderText="New">
                                                <ContentTemplate>
                                                    <asp:Panel ID="panel1" runat="server">
                                                        <div class="custab-panel" id="Div2">
                                                            <ul id="menu1" class="list-unstyled msg_list" role="menu" style="max-height: 45vh; overflow: auto">
                                                                <asp:Literal ID="LiteralsChecked" runat="server"></asp:Literal>
                                                            </ul>
                                                        </div>
                                                    </asp:Panel>

                                                </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanel2" runat="server" CssClass="Tab2" HeaderText="Checked">
                                                <ContentTemplate>
                                                    <asp:Panel ID="panel3" runat="server">
                                                        <div class="custab-panel" id="Div3">
                                                            <ul id="Ul1" class="list-unstyled msg_list" role="menu" style="max-height: 45vh; overflow: auto">
                                                                <asp:Literal ID="LiteralsUnChecked" runat="server"></asp:Literal>
                                                            </ul>
                                                        </div>
                                                    </asp:Panel>

                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right" style="margin-top:-50px">
                                                <asp:Button ID="btnsave" runat="server"  Class="btn  btn-sm cusbtn" Text="Save" OnClick="btnsave_Click" />

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_test_ID" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_test_type" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_lab_sub" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_UHID" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_ID" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox"
                                                    ID="txt_selected_patientType" ClientIDMode="Inherit"></asp:TextBox>
                                                <asp:Button ID="btn_selected_postback" runat="server" Class="btn  btn-sm cusbtn" Text="Save" OnClick="Getpatient_Load" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:tabcontainer>
</asp:Content>
