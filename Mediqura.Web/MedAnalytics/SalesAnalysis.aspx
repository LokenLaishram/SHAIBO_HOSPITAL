<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="SalesAnalysis.aspx.cs" Inherits="Mediqura.Web.MedAnalytics.SalesAnalysis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src="../Scripts/Chart.bundle.min.js"></script>
    <script src="../Scripts/utils.js"></script>
    <style>
        canvas {
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
        }
    </style>
    <asp:TabContainer ID="tabcontainerreferal" runat="server" CssClass="Tab" ActiveTabIndex="0"
        Width="100%">
        <asp:TabPanel ID="tabreferal" runat="server" CssClass="Tab2" HeaderText="Sales Analysis">
            <ContentTemplate>

                <div class="custab-panel" id="panellabgroupmaster">
                    <div class="fixeddiv">
                        <div class="row fixeddiv" id="divmsg1" runat="server">
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span1" class="input-group-addon cusspan" runat="server">Graph Type</span>
                                <select onchange="changeGraphType()" id="ddl_graph_type" class="form-control input-sm col-sm custextbox">
                                    <option value="line">Line Chart</option>
                                    <option value="bar">Bar Chart</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span9" class="input-group-addon cusspan" runat="server">Month</span>
                                <asp:DropDownList ID="ddl_month" runat="server"  class="form-control input-sm col-sm custextbox">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group input-group">
                                <span id="Span7" class="input-group-addon cusspan" runat="server">Date From</span>
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
                                <span id="Span8" class="input-group-addon cusspan" runat="server">Date To </span>
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
                                    <div class="grid" style="float: left; width: 100%; height: 57vh; overflow: auto">

                                        <canvas style="height: 57vh; width: 100%" id="canvas"></canvas>

                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>

            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>
            
     <asp:Literal ID="ArrayLiterals" runat="server"></asp:Literal>
    <script>
        var config = {
            type: 'line',
            data: {
                labels: ["January", "February", "March", "April", "May", "June", "July", "Aug", "Sept", "Oct"],
                datasets: [{
                    label: "Sales Quantity",
                    backgroundColor: window.chartColors.red,
                    borderColor: window.chartColors.red,
                    data: [
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor()
                    ],
                    fill: false,
                }, ]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: ''
                },

                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Item Name'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Quantity'
                        }
                    }]
                }
            }
        };

        function drawgrap() {
            
            
            var i = 0;
            var value = 0;
            config.data.datasets.forEach(function (dataset) {
                dataset.data = dataset.data.map(function () {
                    value = QtyArray[i];
                    i++;

                    return value;
                });

            });
            config.data.labels = ItemArray;

            config.type = document.getElementById('ddl_graph_type').value;
            config.options.title.text = "Sales Analystics Chart"
            var ctx = document.getElementById("canvas").getContext("2d");
            window.myLine = new Chart(ctx, config);

            return false;
        }
        function changeGraphType() {
            var i = 0;
            var value = 0;
            config.data.datasets.forEach(function (dataset) {
                dataset.data = dataset.data.map(function () {
                    value = QtyArray[i];
                    i++;

                    return value;
                });

            });
            config.data.labels = ItemArray;

            config.type = document.getElementById('ddl_graph_type').value;
            config.options.title.text = "Sales Analystics Chart"
            var ctx = document.getElementById("canvas").getContext("2d");
            window.myLine = new Chart(ctx, config);
        }
    </script>

</asp:Content>

