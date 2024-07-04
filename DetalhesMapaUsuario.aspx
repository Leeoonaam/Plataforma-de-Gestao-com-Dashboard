<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalhesMapaUsuario.aspx.cs" Inherits="DetalhesMapaUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalhes Mapa - Usuário</title>

    <link rel="shortcut icon" type="image/png" href="favicon.png" />

    <!-- Styles -->
    <!-- <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600' rel='stylesheet' type='text/css' /> -->
    <link href="files/css" rel="stylesheet" type="text/css" />
    <link href="files/pace-theme-flash.css" rel="stylesheet" />
    <link href="files/uniform.default.min.css" rel="stylesheet" />
    <link href="files/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="./weather-icons-master/css/weather-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="./simple-line-icons-master/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="files/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="files/menu_cornerbox.css" rel="stylesheet" type="text/css" />
    <link href="files/waves.min.css" rel="stylesheet" type="text/css" />
    <link href="files/switchery.min.css" rel="stylesheet" type="text/css" />
    <link href="files/style.css" rel="stylesheet" type="text/css" />
    <link href="files/component.css" rel="stylesheet" type="text/css" />
    <link href="files/MetroJs.min.css" rel="stylesheet" type="text/css" />
    <link href="files/toastr.min.css" rel="stylesheet" type="text/css" />
    <link href="files/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="files/summernote.css" rel="stylesheet" type="text/css" />
    <link href="files/datepicker3.css" rel="stylesheet" type="text/css" />
    <link href="files/colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="files/bootstrap-tagsinput.css" rel="stylesheet" type="text/css" />
    <link href="files/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />

    <!-- Theme Styles -->
    <link href="files/modern.min.css" rel="stylesheet" type="text/css" />
    <link href="files/green.css" class="theme-color" rel="stylesheet" type="text/css" />
    <link href="files/custom.css" rel="stylesheet" type="text/css" />

    <!-- clock -->
    <link rel="stylesheet" type="text/css" href="assets_clock/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="dist_clock/bootstrap-clockpicker.css" />
    <link rel="stylesheet" type="text/css" href="assets_clock/css/github.min.css" />

    <script src="files/modernizr.js"></script>
    <script src="files/snap.svg-min.js"></script>
    <script type="text/javascript" src="js/canvasjs.min.js"></script>

    <%-- FAZ O ESTILO DO MAPA --%>
    <style>
        #mapa {
            width: 1270px;
            height: 500px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
        }

        body{
            background-color:white;
        }
    </style>

    <%-- JAVASCRIPT --%>

    <script>

        // inicializa o mapa
        function initMap() {

            //var uluru1 = { lat: -23.686, lng: -46.619 };
            //var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 8, center: uluru1 });

            <%Response.Write(sPlace_Map);%>

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:Timer ID="Timer1" runat="server" Interval="15000" OnTick="Timer1_Tick"></asp:Timer>

        <div>

            <!-- TITULO DA PAGINA -->
            <div class="page-title" style="background-color:#6B1846;">
                <h3 style=" color:white;">Mapa Em Tempo Real</h3>
            </div>

            <!-- CORPO -->
            <div id="main-wrapper" style="border-radius:10px; box-shadow:0px 0px 1em #BBC0C8">

                <!-- RELATORIOS -->
                <div class="row">
                    <div class="col-lg-12">

                        <div class="panel panel-default">

                            <div class="panel-body">

                                <asp:Literal ID="ltrHistorico" runat="server"></asp:Literal>

                                <div id="mapa" style="width:100%">
                                    <script async
                                        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=initMap">
                                    </script>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>


        </div>
    </form>
</body>
</html>
