<%@ Page Language="C#" AutoEventWireup="true" CodeFile="telauser.aspx.cs" Inherits="telauser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Telas Usuários | STT GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <meta name="description" content="STT GAT Link" />
    <meta name="keywords" content="admin,dashboard,stt" />
    <meta name="author" content="sergiolourenco" />

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
    <link href="files/summernote.css" rel="stylesheet" type="text/css">
    <link href="files/datepicker3.css" rel="stylesheet" type="text/css">
    <link href="files/colorpicker.css" rel="stylesheet" type="text/css">
    <link href="files/bootstrap-tagsinput.css" rel="stylesheet" type="text/css">
    <link href="files/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css">

    <!-- Theme Styles -->
    <link href="files/modern.min.css" rel="stylesheet" type="text/css" />
    <link href="files/green.css" class="theme-color" rel="stylesheet" type="text/css" />
    <link href="files/custom.css" rel="stylesheet" type="text/css" />

    <script src="files/modernizr.js"></script>
    <script src="files/snap.svg-min.js"></script>
    <script type="text/javascript" src="js/canvasjs.min.js"></script>

</head>


<body onunload="executar()">

    <form id="frmmonitor" runat="server">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
                <asp:HiddenField ID="h_iduser" runat="server" />

                <asp:Timer ID="Timer1" Enabled="true" runat="server" OnTick="Timer1_Tick" Interval="250"></asp:Timer>

                <div class="col-lg-12">
                    &nbsp;
                </div>

                <script type="text/javascript">

                    function executar() {
                        //  aqui vai ser PageMethods mais o nome do seu metodo
                        PageMethods.fechou();
                    }

                </script>

                <!-- corpo -->
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-body">

                           

                            <!-- relatorio -->
                            <div class="col-lg-12">
                                <b>
                                    <asp:Label ID="lblusuario" runat="server" Text=""></asp:Label></b>
                                <br />
                                <asp:Image ID="Image1" runat="server" />

                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    &nbsp;
                </div>

            </ContentTemplate>

        </asp:UpdatePanel>
    </form>

    <div class="cd-overlay"></div>

    <!-- Javascripts -->
    <script src="files/jquery-2.1.4.min.js"></script>
    <script src="files/jquery-ui.min.js"></script>
    <script src="files/pace.min.js"></script>
    <script src="files/jquery.blockui.js"></script>
    <script src="files/bootstrap.min.js"></script>
    <script src="files/jquery.slimscroll.min.js"></script>
    <script src="files/switchery.min.js"></script>
    <script src="files/jquery.uniform.min.js"></script>
    <script src="files/classie.js"></script>
    <script src="files/main.js"></script>
    <script src="files/waves.min.js"></script>
    <script src="files/main2.js"></script>

    <script src="files/jquery.waypoints.min.js"></script>
    <script src="files/jquery.counterup.js"></script>
    <script src="files/toastr.min.js"></script>
    <script src="files/jquery.flot.min.js"></script>
    <script src="files/jquery.flot.time.min.js"></script>
    <script src="files/jquery.flot.symbol.min.js"></script>
    <script src="files/jquery.flot.resize.min.js"></script>
    <script src="files/jquery.flot.tooltip.min.js"></script>
    <script src="files/curvedLines.js"></script>
    <script src="files/MetroJs.min.js"></script>
    <script src="files/select2.min.js"></script>

    <script src="files/summernote.min.js"></script>
    <script src="files/bootstrap-datepicker.js"></script>
    <script src="files/bootstrap-colorpicker.js"></script>
    <script src="files/bootstrap-tagsinput.min.js"></script>
    <script src="files/bootstrap-timepicker.min.js"></script>

    <script src="files/modern.js"></script>
    <script src="files/form-elements.js"></script>

    <script src="files/form-select2.js"></script>
    <script src="files/dashboard.js"></script>

    <div id="flotTip" style="display: none; position: absolute;"></div>

    <div class="colorpicker dropdown-menu">
        <div class="colorpicker-saturation" style="background-color: rgb(255, 0, 0);">
            <i style="left: 70.7547px; top: 16.8627px;"><b></b></i>
        </div>
        <div class="colorpicker-hue">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-alpha">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-color">
            <div style="background-color: rgb(212, 62, 62);"></div>
        </div>
    </div>
    <div class="colorpicker dropdown-menu alpha">
        <div class="colorpicker-saturation" style="background-color: rgb(0, 39, 255);">
            <i style="left: 66.6667px; top: 20px;"><b></b></i>
        </div>
        <div class="colorpicker-hue">
            <i style="top: 35.9069px;"></i>
        </div>
        <div class="colorpicker-alpha" style="background-color: rgb(68, 89, 204);">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-color">
            <div style="background-color: rgb(68, 89, 204);"></div>
        </div>
    </div>

    <!-- mensagem de alerta -->
    <script>

        //mensagem de erro
            <% 
        string smsg = Session["mensagem_alerta"].ToString();


        if (Session["titulo_alerta"].ToString() != "")
        {%>
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr["<%=Session["tipo_alerta"].ToString() %>"]("<%=Session["mensagem_alerta"].ToString() %>", "<%=Session["titulo_alerta"].ToString() %>");

        <%
            //zera os valores
            Session["titulo_alerta"] = "";
            Session["mensagem_alerta"] = "";
            Session["tipo_alerta"] = "";

        }
              %>


    </script>
</body>
</html>
