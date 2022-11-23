<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="IPDBedTransfer.aspx.cs" Inherits="Mediqura.Web.MedAdmission.IPDBedTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script type="text/javascript">
        function PrintBedTransferList() {
            objadmissionno = document.getElementById("<%=txtautoIPNo.ClientID %>")
            objstatus = document.getElementById("<%=ddlstatus.ClientID %>")
            window.open("../MedAdmission/Reports/ReportViewer.aspx?option=BedTransferList&IPNo=" + objadmissionno.value + "&Status=" + objstatus.value)
        }
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
    </script>
    <asp:TabContainer ID="tabcontainerbedtransfer" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabipdbedtransfer" runat="server" HeaderText="IPD Bed Transfer">
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
                                        <span id="lbl_ipno" class="input-group-addon cusspan" runat="server" style="color: red">IPNo <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" Style="z-index: 3"
                                            ID="txt_autoipno" AutoPostBack="True" OnTextChanged="txt_autoipno_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txt_autoipno"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lblname" class="input-group-addon cusspan" runat="server">Name</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txtname"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Gender</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_gender"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_age" class="input-group-addon cusspan" runat="server">Age</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_age"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="lbl_address" class="input-group-addon cusspan" runat="server">Address</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Contact No.</span>
                                        <asp:TextBox runat="server" ReadOnly="True" Class="form-control input-sm col-sm custextbox"
                                            ID="txt_contactnumber"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <label class="gridview-label" style="color: red">From</label>
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="gridview-container">
                                                <div class="grid" style="float: left; width: 100%; height: 25vh; overflow: auto">
                                                    <asp:GridView ID="GvBedAssign" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="Please assign bed for this patient." DataKeyNames="ID" AutoGenerateColumns="False" OnRowUpdating="GvBedAssign_OnRowUpdating" OnRowDataBound="GvBedAssign_RowDataBound"
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
                                                                    Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBedStatus" runat="server"></asp:Label>
                                                                    <asp:DropDownList ID="ddl_patactive" AutoPostBack="true" OnSelectedIndexChanged="ddl_patactive_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" runat="server">
                                                                        <asp:ListItem Value="1"> Patient Active</asp:ListItem>
                                                                        <asp:ListItem Value="2"> Patient Inactive</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Entry Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_entryDate" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("AssignedDate","{0:yyyy-MM-dd hh:mm:ss}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed Details
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bedID" Visible="false" runat="server"
                                                                        Text='<%# Eval("BedID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_ward" Visible="false" runat="server"
                                                                        Text='<%# Eval("WardID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_active" Visible="false" runat="server"
                                                                        Text='<%# Eval("Patient_Active") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" runat="server"
                                                                        Text='<%# Eval("ID") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_bedno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_charges" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Charges","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed Status
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_releasedstatus" Visible="false" runat="server"
                                                                        Text='<%# Eval("IsReleased") %>'></asp:Label>
                                                                    <asp:DropDownList ID="ddl_status" AutoPostBack="true" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged" Class="form-control input-sm col-sm custextbox" runat="server">
                                                                        <asp:ListItem Value="0"> --Select --</asp:ListItem>
                                                                        <asp:ListItem Value="1"> Occuppied</asp:ListItem>
                                                                        <asp:ListItem Value="2"> Direct Release</asp:ListItem>
                                                                        <asp:ListItem Value="3"> Release with Transfer</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btn_update" OnClientClick="javascript: return confirm('Are you sure to release the current bed directly ?.');" Visible="false" CommandName="Update" runat="server" Text="Update" />
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
                            </div>
                            <div class="row" style="height: 20px">
                                <label class="gridview-label" style="color: red">To</label>
                            </div>
                            <div class="gridview-container">

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_block" class="input-group-addon cusspan" runat="server">Block</span>
                                            <asp:DropDownList ID="ddl_block" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_block_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_floor" class="input-group-addon cusspan" runat="server">Floor</span>
                                            <asp:DropDownList ID="ddl_floor" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_floor_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group input-group">
                                            <span id="lbl_ward" class="input-group-addon cusspan" runat="server">Ward</span>
                                            <asp:DropDownList ID="ddl_ward" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddl_ward_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                                <div class="row cusrow pad-top ">
                                    <div class="col-sm-12">
                                        <div>
                                            <div class="pbody">
                                                <div class="grid" style="float: left; width: 100%; height: 20vh; overflow: auto">
                                                    <asp:GridView ID="GvBedTransfer" runat="server" CssClass="table-hover grid_table result-table"
                                                        EmptyDataText="Bed not avaiable....... " AutoGenerateColumns="False"
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
                                                                    Room
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialID" Visible="false" runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_ID" Visible="false" Text='<%# Eval("BedID") %>' runat="server"></asp:Label>
                                                                    <asp:Label ID="lbl_room" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Room") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Bed No.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_bedno" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("BedNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    Rate
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_charges" Style="text-align: left !important;" runat="server"
                                                                        Text='<%# Eval("Charges","{0:0#.##}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select">
                                                                <HeaderTemplate>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chekboxselect" runat="server" />
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
                            </div>
                            <div class="row" style="height: 5PX">
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">
                                            <asp:Label runat="server" ID="lbl_transfer"></asp:Label>
                                            <span
                                                style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_transfertype" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Patient</asp:ListItem>
                                            <asp:ListItem Value="2">Party</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="btnsave" runat="server" OnClientClick="javascript: return confirm('Are you sure to save ?');" Text="Save" Class="btn  btn-sm cusbtn"
                                        OnClick="btnsave_Click" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnreset_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="ipdbedtransferlist" runat="server" HeaderText="Bed Transfer List">
            <ContentTemplate>
                <div class="custab-panel" id="Div13">
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="div14" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">IPNo <span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" MaxLength="35"
                                            ID="txtautoIPNo" AutoPostBack="True" OnTextChanged="txtautoIPNo_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetIPNo" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtautoIPNo"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group input-group">
                                        <span id="Span11" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group input-group cuspanelbtngrp  pull-right">
                                        <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                        <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" OnClientClick="return PrintBedTransferList();" Text="Print" />
                                        <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" OnClick="btnresets_Click" />

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="divmsg3" runat="server">
                                            <asp:Label ID="lblresult1" runat="server" Height="13px"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-12">
                                    <div>
                                        <div class="pbody">
                                            <div class="grid" style="float: left; width: 100%; overflow: auto">
                                                <asp:UpdateProgress ID="updateProgress3" runat="server">
                                                    <ProgressTemplate>
                                                        <div id="DIVloading" class="text-center loading" runat="server">
                                                            <asp:Image ID="imgUpdateProgress" ImageUrl="~/Images/loadingx.gif" runat="server"
                                                                AlternateText="Loading ..." ToolTip="Loading ..." CssClass="loadingText" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <asp:GridView ID="gvbedtransferlist" runat="server" CssClass="table-hover grid_table result-table"
                                                    EmptyDataText="No record found..." DataKeyNames="ID" OnPageIndexChanging="gvbedtransferlist_PageIndexChanging" OnRowUpdating="gvbedtransferlist_OnRowUpdating" OnRowDataBound="gvbedtransferlist_RowDataBound" AllowPaging="true" PageSize="10" AutoGenerateColumns="False" OnRowCommand="gvbedtransferlist_RowCommand"
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
                                                                IP No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("ID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_UHID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("UHID") %>'></asp:Label>
                                                                <asp:Label ID="lbl_ipno" Style="text-align: left !important;" runat="server"
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
                                                            <ItemStyle HorizontalAlign="Left" Width="6%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Bed Details
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_details" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("BedDetails") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="8%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Assign Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltransferdate" runat="server" Text='<%# Eval("AssignedDate","{0:dd-MM-yyyy:hh:mm:ss tt}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Exit Date
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_enddate" runat="server" Text='<%# Eval("EndDate","{0:dd-MM-yyyy:hh:mm:ss tt}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Hours
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_hr" runat="server" Text='<%# Eval("hrs")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Day Count
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_nodays" runat="server" Text='<%# Eval("NoDays")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
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
                                                        <asp:ButtonField CommandName="Update"  ItemStyle-Width="1%" ButtonType="Button" Text="Post" />
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
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="right" Height="1em" Width="2%" />
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" OnClick="btnexport_Click" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                    runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnexport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
