<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="DailyNursingAssessment.aspx.cs" Inherits="Mediqura.Web.MedNurse.DailyNursingAssessment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
     <script type="text/javascript">
         function PrintDailyNursingAssessment(ID) {
            
             window.open("../MedNurse/Reports/ReportViewer.aspx?option=DailyNursingAssessment&ID=" + ID)
            
      }

      </script>
    <asp:UpdatePanel ID="upMains" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:TabContainer ID="tabcontainerNursingAssesment" runat="server" CssClass="Tab" ActiveTabIndex="0"
                Width="100%">
                <asp:TabPanel ID="tabpatienthistory" runat="server" CssClass="Tab2" HeaderText="Daily Nursing Assessment">
                    <ContentTemplate>
                        <div class="custab-panel" id="PanelDailyNursingAssessment">
                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg1" runat="server">
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span6" class="input-group-addon cusspan" runat="server">Patient's Name<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" AutoPostBack="true" OnTextChanged="txtpatientNames_TextChanged"
                                            ID="txtpatientNames"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientNames" ID="FilteredTextBoxExtender5"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars" ValidChars=" ():/|" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="GetAdmittedPatientDetails" runat="server"
                                            ServiceMethod="GetAdmittedPatientsDetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientNames"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>

                                        <asp:HiddenField ID="HiddenIPNo" runat="server"></asp:HiddenField>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span1" class="input-group-addon cusspan" runat="server">IP No<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtipno" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtuhid" Visible="false"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtid" Visible="false"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtdischargestatus" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group input-group">
                                        <span id="Span4" class="input-group-addon cusspan" runat="server">Room/BedNo.<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtbedroom" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span2" class="input-group-addon cusspan" runat="server">Age<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtage" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span3" class="input-group-addon cusspan" runat="server">Sex<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtsex" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span5" class="input-group-addon cusspan" runat="server">DOA<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtdoa" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span8" class="input-group-addon cusspan" runat="server">Nurse Shift</span>
                                        <asp:DropDownList ID="ddlnurseshift" runat="server" AutoPostBack="true" class="form-control input-sm col-sm custextbox" OnSelectedIndexChanged="ddlnurseshift_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Morning</asp:ListItem>
                                            <asp:ListItem Value="2">Evening</asp:ListItem>
                                            <asp:ListItem Value="3">Night</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span style="margin: 0px 10px 5px 10px;" runat="server">Emotional State</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="divemotion">
                                    <div class="form-group input-group">
                                        <span id="Span7" class="input-group-addon cusspan" runat="server">Emotional State</span>
                                        <asp:DropDownList ID="ddlemotionalstate" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Anxious</asp:ListItem>
                                            <asp:ListItem Value="2">Calm</asp:ListItem>
                                            <asp:ListItem Value="3">Angry</asp:ListItem>
                                            <asp:ListItem Value="4">Co-opeartive</asp:ListItem>
                                            <asp:ListItem Value="5">Fearful</asp:ListItem>
                                            <asp:ListItem Value="6">Restless</asp:ListItem>
                                            <asp:ListItem Value="7">Withdrawn</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span9" style="margin: 0px 10px 5px 10px;" runat="server">Central Nervous System</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" runat="server" id="div1">
                                    <div class="form-group input-group">
                                        <span id="Span10" class="input-group-addon cusspan" runat="server">Level of consciousness</span>
                                        <asp:DropDownList ID="ddlconsciousness" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Alert</asp:ListItem>
                                            <asp:ListItem Value="2">Drowsy</asp:ListItem>
                                            <asp:ListItem Value="3">Confused</asp:ListItem>
                                            <asp:ListItem Value="4">Semi-conscious</asp:ListItem>
                                            <asp:ListItem Value="5">Comatose</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div4">
                                    <div class="form-group input-group">
                                        <span id="Span14" class="input-group-addon cusspan" runat="server">Speech</span>
                                        <asp:DropDownList ID="ddlspeech" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Relevant</asp:ListItem>
                                            <asp:ListItem Value="2">Irrelevant</asp:ListItem>
                                            <asp:ListItem Value="3">Slurred</asp:ListItem>
                                            <asp:ListItem Value="4">Aphasia</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div5">
                                    <div class="form-group input-group">
                                        <span id="Span15" class="input-group-addon cusspan" runat="server">Physical Type</span>
                                        <asp:DropDownList ID="ddlphysicaltype" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Hemiplegia</asp:ListItem>
                                            <asp:ListItem Value="2">Paraplegia</asp:ListItem>
                                            <asp:ListItem Value="3">Quadriplegia</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" runat="server" id="div3">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span13" style="width: 300px; color: red; font-weight: bold;" class="input-group-addon cusspan" runat="server">Oriented to</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div47">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">

                                            <span id="Span89" class="input-group-addon cusspan" runat="server">Time</span>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rborientedtimeyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="OrientedTime"></asp:RadioButton>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="rborientedtimeno" Text=" No" runat="server" GroupName="OrientedTime"></asp:RadioButton>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div48">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span16" class="input-group-addon cusspan" runat="server">Place</span>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="Rborientedplaceyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="OrientedPlace"></asp:RadioButton>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="Rborientedplaceno" Text=" No" runat="server" GroupName="OrientedPlace"></asp:RadioButton>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div49">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span17" class="input-group-addon cusspan" runat="server">Person</span>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="Rborientedpersonyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="OrientedPerson"></asp:RadioButton>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="Rborientedpersonno" Text=" No" runat="server" GroupName="OrientedPerson"></asp:RadioButton>
                                            </label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span11" style="margin: 0px 10px 5px 10px;" runat="server">Respiratory System</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div2">
                                    <div class="form-group input-group">
                                        <span id="Span12" class="input-group-addon cusspan" runat="server">Respiratory Pattern</span>
                                        <asp:DropDownList ID="ddlrespiratory" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Normal</asp:ListItem>
                                            <asp:ListItem Value="2">Abnormal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span19" class="input-group-addon cusspan" runat="server">Pulse</span>
                                        <asp:TextBox ID="txtpulse" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span20" class="input-group-addon cusspan" runat="server">Breath sound</span>
                                        <asp:TextBox ID="txtbreath" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span21" class="input-group-addon cusspan" runat="server">Cough</span>
                                        <asp:TextBox ID="txtcough" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span22" class="input-group-addon cusspan" runat="server">Oxygen on flow</span>
                                        <asp:TextBox ID="txtoxygen" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div6">
                                    <div class="form-group input-group">
                                        <span id="Span23" class="input-group-addon cusspan" runat="server">Chest drain</span>
                                        <asp:DropDownList ID="ddlchestdrain" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">L-Plueral</asp:ListItem>
                                            <asp:ListItem Value="2">R-Plueral</asp:ListItem>
                                            <asp:ListItem Value="3">Mediastinal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3" runat="server" id="div7">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span18" style="width: 300px; color: red; font-weight: bold;" class="input-group-addon cusspan" runat="server">Fluid column fluctuating</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div51">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span24" class="input-group-addon cusspan" runat="server">(L)Plueral</span>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="RbL_Plueralyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="LPlueral"></asp:RadioButton>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="RbL_Plueralno" Text=" No" runat="server" GroupName="LPlueral"></asp:RadioButton>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div52">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group" style="color: red;">
                                            <span id="Span26" class="input-group-addon cusspan" runat="server">(R)Plueral</span>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="RbR_Plueralyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="RPlueral"></asp:RadioButton>
                                            </label>
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="RbR_Plueralno" Text=" No" runat="server" GroupName="RPlueral"></asp:RadioButton>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span27" style="margin: 0px 10px 5px 10px;" runat="server">Cardiovascular System</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div8">
                                    <div class="form-group input-group">
                                        <span id="Span28" class="input-group-addon cusspan" runat="server">Cardio</span>
                                        <asp:DropDownList ID="ddlcardio" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Normal</asp:ListItem>
                                            <asp:ListItem Value="2">Tachyarrhythmia</asp:ListItem>
                                            <asp:ListItem Value="3">Bradyarrhythmia</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div9">
                                    <div class="form-group input-group">
                                        <span id="Span29" class="input-group-addon cusspan" runat="server">Tension</span>
                                        <asp:DropDownList ID="ddltension" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Normotension</asp:ListItem>
                                            <asp:ListItem Value="2">Hypertension</asp:ListItem>
                                            <asp:ListItem Value="3">Hypotension</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div10">
                                    <div class="form-group input-group">
                                        <span id="Span30" class="input-group-addon cusspan" runat="server">Peripheral pulses</span>
                                        <asp:DropDownList ID="ddlperipheral" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Present</asp:ListItem>
                                            <asp:ListItem Value="2">Feeble</asp:ListItem>
                                            <asp:ListItem Value="3">Absent</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div11">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span31" class="input-group-addon cusspan" runat="server">Neck vein distension</span>
                                        <asp:RadioButton ID="rbneckdistensionyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Distension"></asp:RadioButton>
                                        <asp:RadioButton ID="rbneckdistensionno" Text=" No" runat="server" GroupName="Distension"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div12">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span32" class="input-group-addon cusspan" runat="server">Chest pain</span>
                                        <asp:RadioButton ID="rbchestpainyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Chestpain"></asp:RadioButton>
                                        <asp:RadioButton ID="rbchestpainno" Text=" No" runat="server" GroupName="Chestpain"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span33" style="margin: 0px 10px 5px 10px;" runat="server">Gastrointestinal system</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div13">
                                    <div class="form-group input-group">
                                        <span id="Span34" class="input-group-addon cusspan" runat="server">Mouth</span>
                                        <asp:DropDownList ID="ddlgastrointestinalmouth" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Sores</asp:ListItem>
                                            <asp:ListItem Value="3">Halitosis</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div14">
                                    <div class="form-group input-group">
                                        <span id="Span35" class="input-group-addon cusspan" runat="server">Teeth</span>
                                        <asp:DropDownList ID="ddlgastrointestinalteeth" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Plaque</asp:ListItem>
                                            <asp:ListItem Value="3">Loose</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div15">
                                    <div class="form-group input-group">
                                        <span id="Span36" class="input-group-addon cusspan" runat="server">Tongue</span>
                                        <asp:DropDownList ID="ddlgastrointestinaltongue" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Coated</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div16">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span37" class="input-group-addon cusspan" runat="server">Oral ulcers</span>
                                        <asp:RadioButton ID="rboralulcersyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Ulcers"></asp:RadioButton>
                                        <asp:RadioButton ID="rboralulcersno" Text=" No" runat="server" GroupName="Ulcers"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div17">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span38" class="input-group-addon cusspan" runat="server">Abdominal distension</span>
                                        <asp:RadioButton ID="rbdistensionyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Abdistension"></asp:RadioButton>
                                        <asp:RadioButton ID="rbdistensionno" Text=" No" runat="server" GroupName="Abdistension"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div18">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span39" class="input-group-addon cusspan" runat="server">Nausea</span>
                                        <asp:RadioButton ID="rbnauseayes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Nausea"></asp:RadioButton>
                                        <asp:RadioButton ID="rbnauseano" Text=" No" runat="server" GroupName="Nausea"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div19">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span40" class="input-group-addon cusspan" runat="server">Vomitting</span>
                                        <asp:RadioButton ID="rbvomittingyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Vomitting"></asp:RadioButton>
                                        <asp:RadioButton ID="rbvomittingno" Text=" No" runat="server" GroupName="Vomitting"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div20">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span41" class="input-group-addon cusspan" runat="server">NPO</span>
                                        <asp:RadioButton ID="rbnpoyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="NPO"></asp:RadioButton>
                                        <asp:RadioButton ID="rbnpono" Text=" No" runat="server" GroupName="NPO"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div21">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span42" class="input-group-addon cusspan" runat="server">Nutrition route</span>
                                        <asp:DropDownList ID="ddlnutrition" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Oral</asp:ListItem>
                                            <asp:ListItem Value="2">Tube feeding</asp:ListItem>
                                            <asp:ListItem Value="3">Parenteral</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RadioButton ID="rbnutritionyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Nutrition"></asp:RadioButton>
                                        <asp:RadioButton ID="rbnutritionno" Text=" No" runat="server" GroupName="Nutrition"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div22">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span43" class="input-group-addon cusspan" runat="server">Bowel</span>
                                        <asp:RadioButton ID="rbbowelyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Bowel"></asp:RadioButton>
                                        <asp:RadioButton ID="rbbowelno" Text=" No" runat="server" GroupName="Bowel"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div23">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span44" class="input-group-addon cusspan" runat="server">Constipation</span>
                                        <asp:RadioButton ID="rbconstipationyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Constipation"></asp:RadioButton>
                                        <asp:RadioButton ID="rbconstipationno" Text=" No" runat="server" GroupName="Constipation"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div24">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span45" class="input-group-addon cusspan" runat="server">Diarrhoea</span>
                                        <asp:RadioButton ID="rbdiarrhoeayes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Diarrhoea"></asp:RadioButton>
                                        <asp:RadioButton ID="rbdiarrhoeano" Text=" No" runat="server" GroupName="Diarrhoea"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div25">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span46" class="input-group-addon cusspan" runat="server">Malena</span>
                                        <asp:RadioButton ID="rbmalenayes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Malena"></asp:RadioButton>
                                        <asp:RadioButton ID="rbmalenano" Text=" No" runat="server" GroupName="Malena"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span47" style="margin: 0px 10px 5px 10px;" runat="server">Genitourinary system</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div26">
                                    <div class="form-group input-group">
                                        <span id="Span48" class="input-group-addon cusspan" runat="server">Mouth</span>
                                        <asp:DropDownList ID="ddlmouth" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Voids freely</asp:ListItem>
                                            <asp:ListItem Value="2">Catheter</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div28">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span50" class="input-group-addon cusspan" runat="server">Hematuria</span>
                                        <asp:RadioButton ID="rbHematuriayes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Hematuria"></asp:RadioButton>
                                        <asp:RadioButton ID="rbHematuriano" Text=" No" runat="server" GroupName="Hematuria"></asp:RadioButton>
                                    </div>
                                </div>
                                
                                  
                                    <div class="col-sm-3" runat="server" id="div53">
                                        <div class="form-group input-group">
                                            <div class="form-group input-group" style="color: red;">
                                                <span id="Span49" class="input-group-addon cusspan" runat="server"> Urine Colour Sediments</span>
                                                <label class="radio-inline">
                                                    <asp:RadioButton ID="rbUrineyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="UrineColour"></asp:RadioButton>
                                                </label>
                                                <label class="radio-inline">
                                                    <asp:RadioButton ID="rbUrineno" Text=" No" runat="server" GroupName="UrineColour"></asp:RadioButton>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                       
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span51" style="margin: 0px 10px 5px 10px;" runat="server">Integumentary system</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div29">
                                    <div class="form-group input-group">
                                        <span id="Span52" class="input-group-addon cusspan" runat="server">Skin</span>
                                        <asp:DropDownList ID="ddlskin" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Inact</asp:ListItem>
                                            <asp:ListItem Value="2">Break down</asp:ListItem>
                                            <asp:ListItem Value="3">Rash</asp:ListItem>
                                            <asp:ListItem Value="4">Blister</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div30">
                                    <div class="form-group input-group">
                                        <span id="Span53" class="input-group-addon cusspan" runat="server">Cyanosis</span>
                                        <asp:DropDownList ID="ddlcyanosis" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Peripheral</asp:ListItem>
                                            <asp:ListItem Value="2">Central</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div31">
                                    <div class="form-group input-group">
                                        <span id="Span54" class="input-group-addon cusspan" runat="server">Peripheries</span>
                                        <asp:DropDownList ID="ddlperipheries" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Warm</asp:ListItem>
                                            <asp:ListItem Value="2">Cold</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span55" class="input-group-addon cusspan" runat="server">Oedema-site</span>
                                        <asp:TextBox ID="txtoedemasite" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div32">
                                    <div class="form-group input-group">
                                        <span id="Span56" class="input-group-addon cusspan" runat="server">Temperature</span>
                                        <asp:DropDownList ID="ddltemperature" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Afebrile</asp:ListItem>
                                            <asp:ListItem Value="2">Febrile</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div33">
                                    <div class="form-group input-group">
                                        <span id="Span57" class="input-group-addon cusspan" runat="server">Scalp</span>
                                        <asp:DropDownList ID="ddlscalp" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Pediculi</asp:ListItem>
                                            <asp:ListItem Value="3">dandruff</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div34">
                                    <div class="form-group input-group">
                                        <span id="Span58" class="input-group-addon cusspan" runat="server">Eyes</span>
                                        <asp:DropDownList ID="ddleyes" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Discharge</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div35">
                                    <div class="form-group input-group">
                                        <span id="Span59" class="input-group-addon cusspan" runat="server">Nose</span>
                                        <asp:DropDownList ID="ddlnose" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Discharge</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div36">
                                    <div class="form-group input-group">
                                        <span id="Span60" class="input-group-addon cusspan" runat="server">Ear</span>
                                        <asp:DropDownList ID="ddlear" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Clean</asp:ListItem>
                                            <asp:ListItem Value="2">Discharge</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div37">
                                    <div class="form-group input-group">
                                        <span id="Span61" class="input-group-addon cusspan" runat="server">Sleep</span>
                                        <asp:DropDownList ID="ddlsleep" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Adequate</asp:ListItem>
                                            <asp:ListItem Value="2">Disturbed</asp:ListItem>
                                            <asp:ListItem Value="3">Not applicable</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span62" style="margin: 0px 10px 5px 10px;" runat="server">Musculoskeletal system</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div38">
                                    <div class="form-group input-group">
                                        <span id="Span63" class="input-group-addon cusspan" runat="server">Joint</span>
                                        <asp:DropDownList ID="ddljoint" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Mobile</asp:ListItem>
                                            <asp:ListItem Value="2">Stiff down</asp:ListItem>
                                            <asp:ListItem Value="3">Painful</asp:ListItem>
                                            <asp:ListItem Value="4">Contractures</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div39">
                                    <div class="form-group input-group">
                                        <span id="Span64" class="input-group-addon cusspan" runat="server">Ambulate</span>
                                        <asp:DropDownList ID="ddlambulate" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Ambulate</asp:ListItem>
                                            <asp:ListItem Value="2">Bed to chair</asp:ListItem>
                                            <asp:ListItem Value="3">Bed ridden</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span65" style="margin: 0px 10px 5px 10px;" runat="server">Invasive lines</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div40">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group">
                                            <span id="Span66" class="input-group-addon cusspan" runat="server">Central line:</span>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span67" class="input-group-addon cusspan" runat="server">Site</span>
                                            <asp:TextBox ID="txtsitecentralline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span68" class="input-group-addon cusspan" runat="server">Condition</span>
                                            <asp:TextBox ID="txtconditioncentralline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div41">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group">
                                            <span id="Span69" class="input-group-addon cusspan" runat="server">Peripheral line:</span>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span70" class="input-group-addon cusspan" runat="server">Site</span>
                                            <asp:TextBox ID="txtsiteperipheralline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span71" class="input-group-addon cusspan" runat="server">Condition</span>
                                            <asp:TextBox ID="txtconditionperipheralline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div42">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group">
                                            <span id="Span72" class="input-group-addon cusspan" runat="server">Arterial line:</span>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span73" class="input-group-addon cusspan" runat="server">Site</span>
                                            <asp:TextBox ID="txtsitearterialline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span74" class="input-group-addon cusspan" runat="server">Condition</span>
                                            <asp:TextBox ID="txtconditionarterialline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div43">
                                    <div class="form-group input-group">
                                        <div class="form-group input-group">
                                            <span id="Span75" class="input-group-addon cusspan" runat="server">Any other line:</span>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span76" class="input-group-addon cusspan" runat="server">Site</span>
                                            <asp:TextBox ID="txtsiteanyotherline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                        <div class="form-group input-group">
                                            <span id="Span77" class="input-group-addon cusspan" runat="server">Condition</span>
                                            <asp:TextBox ID="txtconditionanyotherline" runat="server" Class="form-control input-sm col-sm custextbox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span78" style="margin: 0px 10px 5px 10px;" runat="server">Invasive lines</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div44">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span79" class="input-group-addon cusspan" runat="server">Healthy</span>
                                        <asp:RadioButton ID="rbhealthyyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Healthy"></asp:RadioButton>
                                        <asp:RadioButton ID="rbhealthyno" Text=" No" runat="server" GroupName="Healthy"></asp:RadioButton>
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" id="div45">
                                    <div class="form-group input-group" style="color: red;">
                                        <span id="Span80" class="input-group-addon cusspan" runat="server">Dressing</span>
                                        <asp:RadioButton ID="rbdressingyes" Style="margin-left: 20px;" Text=" Yes" runat="server" GroupName="Dressing"></asp:RadioButton>
                                        <asp:RadioButton ID="rbdressingno" Text=" No" runat="server" GroupName="Dressing"></asp:RadioButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                    <span id="Span81" style="margin: 0px 10px 5px 10px;" runat="server">Numeric Pain Rating Scale</span>
                                </div>
                                <div class="col-sm-3" runat="server" id="div46">
                                    <div class="form-group input-group">
                                        <span id="Span82" class="input-group-addon cusspan" runat="server">Pain Scale</span>
                                        <asp:DropDownList ID="ddlPainScale" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">No Hurt</asp:ListItem>
                                            <asp:ListItem Value="2">Hurts Little Bit</asp:ListItem>
                                            <asp:ListItem Value="3">Hurts Little More</asp:ListItem>
                                            <asp:ListItem Value="4">Hurts Even More</asp:ListItem>
                                            <asp:ListItem Value="5">Hurts Whole Lot</asp:ListItem>
                                            <asp:ListItem Value="6">Hurts Worst</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div style="color: #0e8674; background-color: #cfede3; font-weight: bold; font-size: medium; margin: 0px 10px 5px 10px;">
                                <span id="Span83" style="margin: 0px 10px 5px 10px;" runat="server">Investigations</span>
                            </div>
                            <div class="row cusrow pad-top ">
                                <div class="col-sm-4">
                                    <div class="gridview-container-ML">
                                        <span id="Span84" class="input-group-addon cusspan" runat="server">Due</span>
                                        <asp:TextBox ID="txtParticulars" runat="server" OnTextChanged="txtParticular_TextChanged" AutoPostBack="true" Class="form-control input-sm col-sm custextbox" placeholder="text here"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                            ServiceMethod="GetLabServices" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtParticulars"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                        <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                            <asp:GridView ID="GvDue" runat="server" CssClass="table-hover grid_table result-table"
                                                EmptyDataText="No record found..." AutoGenerateColumns="False"
                                                Width="100%" HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Sl. No
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Test Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("ID") %>'></asp:Label>
                                                            <asp:Label ID="lbl_Particular" Style="text-align: left !important;" runat="server"
                                                                Text='<%# Eval("Particular") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
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
                                <div class="col-sm-4">
                                    <div class="gridview-container-ML">
                                        <span id="Span85" class="input-group-addon cusspan" runat="server">Reports Awaited</span>
                                        <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                            <asp:GridView ID="GVReportsAwaited" runat="server" CssClass="table-hover grid_table result-table"
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
                                                            Role
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblroleID" Visible="false" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTroleID") %>'></asp:Label>
                                                                <asp:Label ID="lblrole" Style="text-align: left !important;" runat="server"
                                                                    Text='<%# Eval("OTrole") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
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
                                <div class="col-sm-4">
                                    <div class="gridview-container-ML">
                                        <span id="Span86" class="input-group-addon cusspan" runat="server">Done</span>
                                        <div class="grid" style="float: left; width: 100%; height: 40vh; overflow: auto">
                                            <asp:GridView ID="GvDone" runat="server" CssClass="table-hover grid_table result-table"
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
                            <div class="row">
                                <div class="form-group input-group cuspanelbtngrp  pull-right">
                                    <asp:Button ID="btnsave" UseSubmitBehaviour="false" runat="server" Class="btn  btn-sm  cusbtn" Text="Save" OnClick="btnadd_Click" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" Class="btn  btn-sm cusbtn" OnClick="btnresets_Click" />
                                    <asp:Button ID="btnprints" runat="server" Class="btn  btn-sm cusbtn" Text="Print" OnClick="btn_print_Click" />
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
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabpatientlist" runat="server" HeaderText="Nursing Assessment List">
                    <ContentTemplate>
                        <div class="custab-panel" id="panelPatientRegistration">

                            <div class="fixeddiv">
                                <div class="row fixeddiv" id="divmsg2" runat="server">
                                    <asp:Label ID="lblmessage2" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group input-group">
                                        <span id="Span87" class="input-group-addon cusspan" runat="server">Patient's Name</span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" OnTextChanged="txtpatientsNames_TextChanged"
                                            ID="txtpatientdetails"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender TargetControlID="txtpatientdetails" ID="FilteredTextBoxExtender1"
                                            runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                            FilterMode="ValidChars" ValidChars=" ():/|" Enabled="True">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                            ServiceMethod="GetAdmittedPatientsDetails" MinimumPrefixLength="1"
                                            CompletionInterval="100" CompletionSetCount="1" TargetControlID="txtpatientdetails"
                                            UseContextKey="True" DelimiterCharacters="" Enabled="True" ServicePath="" CompletionListCssClass="completionList" CompletionListItemCssClass="listItem" CompletionListHighlightedItemCssClass="itemHighlighted">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span88" class="input-group-addon cusspan" runat="server">IP No<span
                                            style="color: red">*</span></span>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtipnos" ReadOnly="true"></asp:TextBox>
                                        <asp:TextBox runat="server" Class="form-control input-sm col-sm custextbox" ID="txtuhids" Visible="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span90" class="input-group-addon cusspan" runat="server">Date From </span>
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
                                        <span id="Span91" class="input-group-addon cusspan" runat="server">Date To  </span>
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
                             <%--   <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span93" class="input-group-addon cusspan" runat="server">Added By <span
                                            style="color: red">*</span></span>
                                        <asp:DropDownList ID="ddl_user" runat="server" class="form-control input-sm col-sm custextbox">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-sm-3">
                                    <div class="form-group input-group">
                                        <span id="Span92" class="input-group-addon cusspan" runat="server">Status</span>
                                        <asp:DropDownList ID="ddlstatus" runat="server" class="form-control input-sm col-sm custextbox">
                                            <asp:ListItem Value="0">Active</asp:ListItem>
                                            <asp:ListItem Value="1">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4"></div>
                            <div class="form-group input-group cuspanelbtngrp  pull-right">
                                <asp:Button ID="btnsearch" runat="server" Class="btn  btn-sm cusbtn" Text="Search" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnresets" runat="server" Class="btn  btn-sm cusbtn" Text="Reset" />
                               
                            </div>



                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="fixeddiv">
                                        <div class="row fixeddiv" id="div50" runat="server">
                                            <asp:Label ID="Label1" runat="server" Height="13px"></asp:Label>
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
                                                <asp:GridView ID="GvNursingAssessment" runat="server" CssClass="table-hover grid_table result-table grid"
                                                    ShowFooter="true"
                                                    EmptyDataText="No record found..." OnPageIndexChanging="GvNursingAssessment_PageIndexChanging"
                                                    AllowPaging="true" PageSize="10" AllowCustomPaging="true" AutoGenerateColumns="False"
                                                    OnRowCommand="GvNursingAssessment_RowCommand"
                                                    Width="100%" HorizontalAlign="Center">

                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Sl.No
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# (Container.DataItemIndex+1)+(GvNursingAssessment.PageIndex)*GvNursingAssessment.PageSize %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                               Assessment No.
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>  
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                IPNo
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNurseShiftID" runat="server" class="hidden" Text='<%# Eval("NurseShift")%>'></asp:Label>
                                                                <asp:Label ID="lblIPNo" runat="server" Text='<%# Eval("IPNo")%>'></asp:Label>
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
                                                                Added By
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdedBy" runat="server" Text='<%# Eval("EmpName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Added On
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladt" runat="server" Text='<%# Eval("AddedDate","{0:dd-MM-yyyy}")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>
                                                         <%--<asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Remarks
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" Width="170px" Height="18px" runat="server" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                                                        </asp:TemplateField>--%>
                                                         <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Print
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href="javascript: void(null);" style="color: red; font-size: 12px" onclick="PrintDailyNursingAssessment('<%# Eval("ID")%>'); return false;">Print</a>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="1%" />
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
                                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                                                    <PagerStyle BackColor="#CFEDE3" CssClass="gridpager" HorizontalAlign="left" Height="1em" Width="2%" />

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
                                    <asp:Button ID="btnexport" Visible="false" Style="margin-left: 8px" runat="server" Class="btn  btn-sm cusbtn exprt" Text="Export" />
                                    <div class="form-group input-group cuspanelbtngrp drop-dwn">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlexport" Visible="false" Class="form-control input-sm col-sm cusmidiumtxtbox"
                                                    runat="server">
                                                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Excel"></asp:ListItem>
                                                    <%-- <asp:ListItem Value="2" Text="PDF"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <%--   <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnexport" />
                                                </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
