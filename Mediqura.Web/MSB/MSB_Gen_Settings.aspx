<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="MSB_Gen_Settings.aspx.cs" Inherits="Mediqura.Web.MSB.MSB_Gen_Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "white";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "white";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function txtDependentKeyUp() {
            var x = 1;
            var y = document.getElementById('<%= txt_dependent.ClientID %>').value;
            if (y == "") { y = 0;}
            var z = parseInt(x) + parseInt(y);
            document.getElementById('<%= txt_numberdependent.ClientID %>').value = z;
        }
    </script>
    <asp:TabContainer ID="tabcontainerbedtransfer" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabipdbedtransfer" runat="server" HeaderText="MSB General Settings">
            <ContentTemplate>
                <div class="custab-panel" id="ipdadmissiondetaildiv">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_bedassign" class="custom-heading cusspan" runat="server">Eligibility</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress2" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="Gvemployee" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False" OnRowDataBound="Gvemployee_RowDataBound"
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
                                                                Employee Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_employeetypeID" Visible="false" runat="server"
                                                                    Text='<%# Eval("EmplyeetypeID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_IsMSB" Visible="false" runat="server"
                                                                    Text='<%# Eval("IsMSB") %>'></asp:Label>
                                                                <asp:Label ID="lbl_employeetype" runat="server"
                                                                    Text='<%# Eval("EmployeeType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Check
                                                                <asp:CheckBox ID="chekboxall" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chekboxselect" runat="server" onclick="Check_Click(this);" />
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
                                <div class="col-lg-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btn_saveemp" runat="server" UseSubmitBehavior="False" OnClick="btn_saveemp_Click" Text="Save" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btn_resertemp" OnClick="btn_resertemp_Click" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="custom-heading cusspan" runat="server">Applicability</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server">Employee</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Text="1" ReadOnly="true" Style="z-index: 3"
                                            ID="txt_employee"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_dependent" class="input-group-addon cusspan" runat="server">Number of dependent</span>
                                        <asp:TextBox onkeyup="txtDependentKeyUp()" runat="server" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_dependent" ></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txt_dependent" ID="FilteredTextBoxExtender3"
                                            runat="server" ValidChars="0123456789"
                                            Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Total</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Text="1" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_numberdependent"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-10">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="custom-heading cusspan" runat="server">Exclusion of Benifits: There will be no discount on consultant round/Procedure/OT performed by consultant on call </span>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group input-group">
                                        <asp:CheckBox runat="server"
                                            ID="CheckBoxExclution"></asp:CheckBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnsave" runat="server" UseSubmitBehavior="False" Text="Save" Class="btn  btn-sm cusbtn" OnClick="btnsave_Click" />
                                        <asp:Button ID="btnreset" OnClick="btn_resertemp_Click" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" />
                                    </div>
                                </div>
                            </div>
                                           <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="custom-heading cusspan" runat="server">Benefit</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; height: 15vh; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress1" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="GVBenifits" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." AutoGenerateColumns="False"
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
                                                                Beneficiary Type
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_beneficaryID" Visible="false" runat="server"
                                                                    Text='<%# Eval("BeneficiaryTypeID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_beneficary" runat="server"
                                                                    Text='<%# Eval("BeneficiaryType") %>'></asp:Label>  
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                OPD %
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                               <asp:TextBox  runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_opd" Text='<%# Eval("opd") %>' ></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_opd" ID="FilteredTextBoxExtender3"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Investigation %
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                               <asp:TextBox  runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_investigation" Text='<%# Eval("investigation") %>' ></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_investigation" ID="FilteredTextBoxExtender1"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IPD %
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                               <asp:TextBox  runat="server" Class="form-control input-sm col-sm custextbox"
                                                                    ID="txt_ipd" Text='<%# Eval("ipd") %>' ></asp:TextBox>
                                                                <asp:FilteredTextBoxExtender TargetControlID="txt_ipd" ID="FilteredTextBoxExtender2"
                                                                    runat="server" ValidChars="0123456789."
                                                                    Enabled="True">
                                                                </asp:FilteredTextBoxExtender>
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
                                <div class="col-lg-9"></div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <asp:Button ID="btnBenefitSave" OnClick="btnBenefitSave_Click" runat="server" UseSubmitBehavior="False"  Text="Save" Class="btn  btn-sm cusbtn" />
                                        <asp:Button ID="btnBenefitReset" OnClick="btnBenefitReset_Click" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
</asp:Content>
