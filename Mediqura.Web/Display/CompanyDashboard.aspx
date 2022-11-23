<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyDashboard.aspx.cs" Inherits="Mediqura.Web.Display.CompanyDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />


    <title>Mediqura</title>
    <!-- Bootstrap core CSS -->
    <link href="../dashboardAssets/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="../dashboardAssets/css/medi.css" rel="stylesheet" />
    <link href="../dashboardAssets/css/font-awesome.css" rel="stylesheet" />
</head>
<body>
    <div class="wrapper">

        <header class="main-header">
            <!-- Logo -->
            <a class="logo">

                <!-- logo-->
                <div class="logo-lg">

                    <span class="dark-logo">
                        <img style="margin-top: 3px;" src="../dashboardAssets/img/logo.png" alt="logo" width="130" /></span>
                </div>
            </a>
            <!-- Header Navbar -->

        </header>

        <!-- Left side column. contains the logo and sidebar -->


        <!-- Content Wrapper. Contains page content -->
        <div class="">
            <div class="content-header">
                <div class="d-flex align-items-center">
                    <div class="ml-auto">
                        <div id="date"></div>
                        <div id="time"></div>
                    </div>

                </div>
            </div>
            <!-- Main content -->
            <section class="content">
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <div class="row">
                            <div class="col-12">
                                <div class="box bg-img box-inverse">

                                    <div class="box-body mt-20">

                                        <h1 style="margin-top: 9px;" class="font-weight-300 mb-4"><span style="font-size: 18px;">INCOME</span><span id="income">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rs. 000000.00&nbsp;&nbsp;<i class="fa fa-caret-up text-success"></i></span></h1>

                                        <ul class="flexbox flex-justified mt-20">
                                            <li>
                                                <img style="margin-left: 4em; margin-bottom: 1em;" src="../dashboardAssets/img/advance.svg" width="60" />
                                                <p class="font-size-20  mb-0 font-weight-300">
                                                    <small id="advance" class="font-size-12">000000</small> <span id="advancepc"><i id="advanceClass" class="fa fa-caret-up text-success"></i>+00% </span>

                                                </p>
                                                <p style="color: #8FEAB0; font-weight: 600; font-size: 18px;">ADVANCE</p>
                                            </li>

                                            <li>
                                                <img style="margin-left: 4em; margin-bottom: 1em;" src="../dashboardAssets/img/expense.svg" width="60" />
                                                <p class="font-size-20  mb-0 font-weight-300">
                                                    <small class="font-size-12" id="expenses">000000</small> <span id="expensepc"><i class="fa fa-caret-down text-danger"></i>+00</span>

                                                </p>
                                                <p style="color: #FDB6C8; font-weight: 600; font-size: 18px;">EXPENSES</p>
                                            </li>

                                            <li>
                                                <img style="margin-left: 4em; margin-bottom: 1em;" src="../dashboardAssets/img/balance.svg" width="60" />
                                                <p class="font-size-20  mb-0 font-weight-300">
                                                    <small id="balance" class="font-size-12">000000</small> <span id="balancepc"><i id="balanceClass" class="fa fa-caret-up text-success"></i>+00% </span>

                                                </p>
                                                <p style="color: #EFC94B; font-weight: 600; font-size: 18px;">BALANCE</p>
                                            </li>
                                        </ul>
                                        <hr>
                                        <h3 style="text-align: center;" class="font-weight-300"><span style="font-size: 18px;">DISCOUNT</span><span id="discount">&nbsp;&nbsp;&nbsp;&nbsp;Rs. 000000</span></h3>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                    <div class="col-12 col-lg-6">
                        <div class="row">
                            <div class="col-12">
                                <div class="box bg-img box-inverse">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">Top Earning Departments</h4>

                                    </div>
                                    <div class="box-body mt-20">
                                        <h1 class="font-weight-300 mb-4"><span style="font-size: 18px;">SURGERY</span><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rs. 23,87,850.00&nbsp;&nbsp;<i class="fa fa-caret-up text-success"></i></span></h1>
                                        <ul class="flexbox flex-justified mt-20">
                                            <li>

                                                <p class="font-size-20 text-success mb-0 font-weight-300">
                                                    <i class="fa fa-caret-up text-success"></i>+1.92% 
                                     
                                                    <small class="font-size-12">41,425.81</small>
                                                </p>
                                                <p style="font-weight: 600; font-size: 18px;">ORTHO</p>
                                            </li>

                                            <li>

                                                <p class="font-size-20 text-danger mb-0 font-weight-300">
                                                    <i class="fa fa-caret-down text-danger"></i>-0.92% 
                                     
                                                    <small class="font-size-12">54,425.81</small>
                                                </p>
                                                <p style="font-weight: 600; font-size: 18px;">MEDICINE</p>
                                            </li>

                                            <li>

                                                <p class="font-size-20 text-success mb-0 font-weight-300">
                                                    <i class="fa fa-caret-up text-success"></i>+1.12% 
                                     
                                                    <small class="font-size-12">85,425.81</small>
                                                </p>
                                                <p style="font-weight: 600; font-size: 18px;">EMERGENCY</p>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>


                </div>


            </section>


            <section class="contents">
                <div class="row">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-3 mx-auto">

                                <ul class="flexbox flex-justified mt-20">
                                    <li>

                                        <p class="font-size-20 text-danger mb-0 font-weight-300"><span id="occupied">00</span>
                                            </p><p><i class="fa fa-square text-danger"></i>&nbsp;&nbsp;<small class="font-size-12" style="font-weight: bold; color: #fff;">OCCUPIED</small></p>

                                    </li>

                                    <li>
                                        <img style="margin-top: -15px;" src="../dashboardAssets/img/bed.svg" width="100" />
                                    </li>

                                    <li>

                                        <p class="font-size-20 text-success mb-0 font-weight-300"><span id="vacant">00</span></p><p> <i class="fa fa-square text-success "></i>&nbsp;&nbsp;&nbsp;<small class="font-size-12" style="font-weight: bold; color: #fff;">VACANT</small> </p>

                                    </li>
                                </ul>


                            </div>

                        </div>

                    </div>



                </div>


            </section>

            <section class="content">
                <div class="row">
                    <div class="col-12 col-lg-6">
                        <div class="row">
                            <div class="col-12">
                                <div>

                                    <div class="box-body mt-20">

                                        <div class="counter col_fourth">

                                            <h2 class="timer count-title count-number mb-4" id="opd" data-to="300" data-speed="1500"></h2>
                                            <img src="../dashboardAssets/img/opd.svg" width="60">
                                            <p class="count-text ">OPD</p>
                                        </div>

                                        <div class="counter col_fourth">

                                            <h2 class="timer count-title count-number mb-4" id="ot" data-to="1700" data-speed="1500"></h2>
                                            <img src="../dashboardAssets/img/ot.svg" width="60">
                                            <p class="count-text ">OT</p>
                                        </div>

                                        <div class="counter col_fourth">

                                            <h2 class="timer count-title count-number mb-4" id="er" data-to="11900" data-speed="1500"></h2>
                                            <img src="../dashboardAssets/img/emergency.svg" width="60">
                                            <p class="count-text ">EMERGENCY</p>
                                        </div>

                                        <div class="counter col_fourth end">

                                            <h2 class="timer count-title count-number mb-4" id="ipd" data-to="157" data-speed="1500"></h2>
                                            <img src="../dashboardAssets/img/ipd.svg" width="60">
                                            <p class="count-text ">IPD</p>
                                        </div>
                                    </div>

                                </div>

                            </div>

                        </div>
                        </div>
                        <div class="col-12 col-lg-6">
                            <div class="row">
                                <div class="col-12">

                                    <div class="box-header" style="text-align: center;">
                                        <h4 class="box-title with-border" style="color: #fff;">PATIENT STATS</h4>

                                    </div>
                                    <div class="horizontal-bar-graph" id="my-graph"></div>
                                </div>
                            </div>

                        </div>

                    


                </div>


            </section>
            <!-- /.content -->
        </div>
    </div>


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="../dashboardAssets/js/jquery-3.3.1.min.js"></script>
    <script src="../dashboardAssets/js/d3.v3.min.js"></script>
    <script src="../dashboardAssets/js/custom.js"></script>
    <script src="../dashboardAssets/assets/js/popper.js"></script>
    <script src="../dashboardAssets/bootstrap/js/bootstrap.min.js"></script>
    <script src="../dashboardAssets/assets/js/ie10-viewport-bug-workaround.js"></script>
    <script src="../Scripts/service.js"></script>
    <script type="text/JavaScript">
        $(document).ready(function () {
            fetchData();
        });
        setInterval(function () {
            fetchData();
        }, 60000);

        function fetchData() {
            var url = "../MedWebservices/DashboardWebservices.aspx";
            var status = callServiceToFetchData(url, serverReply);

            return status;
        }

        function serverReply(response) {

            var json = JSON.parse(response);
            var jsonData = json[0];
            var IncomeCurrent = jsonData.IncomeCurrent;
            var IncomePrevious = jsonData.IncomePrevious;
            var AdvanceCurrent = jsonData.AdvanceCurrent;
            var AdvancePrevious = jsonData.AdvancePrevious;
            var ExpensesCurrent = jsonData.ExpensesCurrent;
            var ExpensesPrevious = jsonData.ExpensesPrevious;
            var BalanceCurrent = jsonData.BalanceCurrent;
            var BalancePrevious = jsonData.BalancePrevious;
            var Discount = jsonData.Discount;

            var TotalBed = jsonData.TotalBed;
            var BedOccupied = jsonData.BedOccupied;
            var BedVacant = jsonData.BedVacant;
            var OPD = jsonData.OPD;
            var OT = jsonData.OT;
            var ER = jsonData.ER;
            var IPD = jsonData.IPD;
            var NewPatient = jsonData.NewPatient;
            var OldPatient = jsonData.OldPatient;
            var Admitted = jsonData.Admitted;
            var Discharge = jsonData.Discharge;
            var incomecarat;

            if (parseFloat(IncomeCurrent) > parseFloat(IncomePrevious)) {
                incomecarat = " <i class=\"fa fa-caret-up text-success\"></i>";
            } else { incomecarat = " <i class=\"fa fa-caret-down text-danger\"></i>"; }
            document.getElementById('income').innerHTML = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rs. " + IncomeCurrent + "&nbsp;&nbsp;" + incomecarat;

            if (parseFloat(AdvanceCurrent) > parseFloat(AdvancePrevious)) {
                var advdiff = AdvanceCurrent - AdvancePrevious;
                var advpc = parseFloat((advdiff / AdvanceCurrent) * 100).toFixed(2);

                document.getElementById('advancepc').innerHTML = "<i  class=\"fa fa-caret-up text-success\"></i><span class=\"text-success\">+" + advpc + "%</span>";

            } else {
                var advdiff = AdvancePrevious - AdvanceCurrent;
                var advpc = parseFloat((advdiff / AdvanceCurrent) * 100).toFixed(2);

                document.getElementById('advancepc').innerHTML = "<i  class=\"fa fa-caret-down text-danger\"></i><span class=\"text-danger\">-" + advpc + "%</span>";
            }
            if (parseFloat(ExpensesCurrent) > parseFloat(ExpensesPrevious)) {
                var expensesdiff = ExpensesCurrent - ExpensesPrevious;
                var expc = parseFloat((expensesdiff / ExpensesCurrent) * 100).toFixed(2);

                document.getElementById('expensepc').innerHTML = "<i  class=\"fa fa-caret-up text-success\"></i><span class=\"text-success\">+" + expc + "%</span>";

            } else {
                var expensesdiff = ExpensesPrevious - ExpensesCurrent;
                var expc = parseFloat((expensesdiff / ExpensesPrevious) * 100).toFixed(2);
                document.getElementById('expensepc').innerHTML = "<i  class=\"fa fa-caret-down text-danger\"></i><span class=\"text-danger\">-" + expc + "%</span>";
            }
            if (parseFloat(BalanceCurrent) > parseFloat(BalancePrevious)) {
                var baldiff = BalanceCurrent - BalancePrevious;
                var balpc = parseFloat((baldiff / BalanceCurrent) * 100).toFixed(2);

                document.getElementById('balancepc').innerHTML = "<i  class=\"fa fa-caret-up text-success\"></i><span class=\"text-success\">+" + balpc + "%</span>";

            } else {
                var baldiff = BalancePrevious - BalanceCurrent;
                var balpc = parseFloat((baldiff / BalanceCurrent) * 100).toFixed(2);

                document.getElementById('balancepc').innerHTML = "<i  class=\"fa fa-caret-down text-danger\"></i><span class=\"text-danger\">-" + balpc + "%</span>";
            }

            document.getElementById('expenses').innerHTML = ExpensesCurrent;
            document.getElementById('advance').innerHTML = AdvanceCurrent;
            document.getElementById('balance').innerHTML = BalanceCurrent;
            document.getElementById('discount').innerHTML = " &nbsp;&nbsp;&nbsp;&nbsp;Rs. " + Discount;

            document.getElementById('occupied').innerHTML = BedOccupied;
            document.getElementById('vacant').innerHTML = BedVacant;

            document.getElementById('opd').innerHTML = OPD;
            document.getElementById('ot').innerHTML = OT;

            document.getElementById('er').innerHTML = ER;
            document.getElementById('ipd').innerHTML = IPD;

            var graph = new HorizontalBarGraph('#my-graph', [
              { label: "NEW", inner_label: NewPatient + "", value: NewPatient / 100, color: "#0FA798" },
              { label: "OLD", inner_label: OldPatient + "", value: OldPatient / 100, color: "#F2C02B" },
              { label: "DISCHARGED", inner_label: Discharge + "", value: Discharge / 100, color: "#F39D36" },
              { label: "ADMITTED", inner_label: Admitted + "", value: Admitted / 100, color: "#F3764A" }
            ]);
            graph.draw();
        }
    </script>

</body>
</html>


