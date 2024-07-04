<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mapa.aspx.cs" Inherits="Mapa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mapa</title>
    <link rel="shortcut icon" type="image/png" href="favicon.png" />

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

    <style>
        #mapa {
            width: 1280px;
            height: 500px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
        }
    </style>

    <script>

        // inicializa o mapa
        function initMap() {
            <%Response.Write(sPlace_Map);%>
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <!-- CORPO -->
            <div id="main-wrapper">

                <!-- RELATORIOS -->
                <div class="row">
                    <div class="col-lg-12">

                        <div class="panel panel-default">

                            <div class="panel-body">

                                <div id="mapa">
                                    <script async defer
                                        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCcPuAQ7HlOZbFjefs379aUWWL9Af0pndo&callback=initMap">
                                    </script>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>
        
    </form>
</body>
</html>
