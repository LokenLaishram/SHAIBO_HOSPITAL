<%@ Page Title="" Language="C#" MasterPageFile="~/Mediqura.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Mediqura.Web.Dashboard" %>


<asp:Content ID="Content2" ContentPlaceHolderID="Mediquraplaceholder" runat="server">
    <script src="../Scripts/Chart.bundle.min.js"></script>
    <script src="../Scripts/utils.js"></script>
    <style>
        canvas {
            -moz-user-select: none;
            -webkit-user-select: none;
            -ms-user-select: none;
        }

        .expand_graph {
            position: absolute;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100vh;
            background: #ffffff;
            z-index: 9;
            padding: 0px 200px;
        }

        #expand_btn {
            display: block;
        }

        #collapse_btn {
            display: none;
        }

        .collapse_btn_class, .expand_btn_class {
            position: absolute;
            top: 5px;
            right: 0px;
            border-radius: 3px;
            border: none;
        }

        .breadcumb_cus {
            background: #009587;
            padding: 10px;
            font-size: 14px;
            color: white;
        }

        .dash_bg {
            width: 100%;
            padding: 10px 10px;
            display: inline-block;
            background: #03a9f4;
            color: #fff;
            border: 1px solid #E6E9ED;
            -webkit-column-break-inside: avoid;
            -moz-column-break-inside: avoid;
            column-break-inside: avoid;
            opacity: 1;
            transition: all .2s ease;
        }

        .dash_bg_dark {
            width: 100%;
            padding: 10px 10px;
            display: inline-block;
            background: #009688;
            color: #fff;
            border: 1px solid #E6E9ED;
            -webkit-column-break-inside: avoid;
            -moz-column-break-inside: avoid;
            column-break-inside: avoid;
            opacity: 1;
            transition: all .2s ease;
        }

            .dash_bg_dark:hover {
                box-shadow: 0 30px 70px rgba(0,0,0,.2);
            }

        .nav-md .container.body .right_col {
            background-color: #ffffff;
        }

        .num_size {
            position: relative;
            animation: num_size 2s;
            animation-iteration-count: 1;
            font-size: 3em;
        }

        .num_size1 {
            position: relative;
            animation: num_size 2s;
            animation-iteration-count: 1;
            font-size: 3.2em;
        }

        @keyframes num_size {
            from {
                bottom: 100px;
            }

            to {
                bottom: 0px;
            }
        }

        .padlr2 {
            padding-left: 2px;
            padding-right: 2px;
        }
    </style>
    <div style="padding-left: 10px; padding-right: 10px;">
        <div class="row">
            <div class="col-sm-12" style="padding: 0; padding-left: 8px;">
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Daily Patient Registration"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass=" num_size" ID="lbl_totalcurrentdate" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Till Date Registration"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="num_size" ID="lbl_totalpatienttilldate" Text="90909" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Daily Invoice"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="num_size" ID="lbl_currentdayinvoice" Text="90909" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Till Date Invoice"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="num_size" ID="lbl_tilldateinvoice" Text="90909" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Daily Test"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="num_size" ID="lbl_currentdaytest" Text="90909" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 padlr2">
                    <div class="dash_bg_dark">
                        <div class="x_content" style="height: 12vh; width: 100%">
                            <asp:Label runat="server" Text="Till  Date Test"></asp:Label>
                            <br />
                            <asp:Label runat="server" CssClass="num_size" ID="lbl_tilldatetest" Text="90909" Style="height: 8vh; width: 100%"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-8" id="first_graph">
                <div class="x_panel" style="box-shadow: 0 3rem 6rem rgba(0, 0, 0, 0.1);">
                    <div class="x_content">
                        <canvas style="height: 62vh; width: 100%" id="myChart1"></canvas>
                    </div>
                    <button id="expand_btn" type="button" class="expand_btn_class" onclick="expand_graph('expand_btn','first_graph','collapse_btn')"><span class="fa fa-arrows-alt"></span></button>
                    <button id="collapse_btn" type="button" class="collapse_btn_class" onclick="collapse_graph('expand_btn','first_graph','collapse_btn')"><span class="fa fa-compress"></span></button>
                </div>
            </div>
            <div class="col-sm-4">
                <div class="row">
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Total Generated Report"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_totalgenerated" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Total Pending Report"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_totalpending" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Today's Generated Report"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_todaygenerated" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Today's Pending Report"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_todaypending" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Total Report Delivered"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_totaldeliverd" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Total Delivery Pending"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_totaldeliverypending" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                     <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Today's Delivery Pending"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_todaydeliverypending" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 padlr2">
                        <div class="dash_bg">
                            <div class="x_content" style="height: 12vh; width: 100%">
                                <asp:Label runat="server" Text="Total Centre"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_totalcentre" CssClass="num_size" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Literal ID="ArrayLiterals" runat="server"></asp:Literal>
    <script>

        var config = {
            type: 'line',
            data: {

                datasets: [{
                    label: "Hourly Patient Chart",
                    backgroundColor: window.chartColors.blue,
                    borderColor: window.chartColors.blue,
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
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
                        randomScalingFactor(),
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
                },]
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
                            labelString: '-------- Hours ---------'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: '--------- Patient Count ------------'
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

            config.type = "line";
            var ctx = document.getElementById("myChart1").getContext("2d");
            window.myLine = new Chart(ctx, config);

            return false;
        }

        function expand_graph(expand_btn, first_graph, collapse_btn) {
            document.getElementById(first_graph).classList.add("expand_graph");
            document.getElementById(expand_btn).style.display = "none";
            document.getElementById(collapse_btn).style.display = "block";
        }
        function collapse_graph(expand_btn, first_graph, collapse_btn) {
            document.getElementById(first_graph).classList.remove("expand_graph");
            document.getElementById(expand_btn).style.display = "block";
            document.getElementById(collapse_btn).style.display = "none";
        }
    </script>
</asp:Content>

