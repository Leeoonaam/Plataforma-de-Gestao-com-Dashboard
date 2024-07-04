<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usuarios_dados - Copy.aspx.cs" Inherits="usuarios_dados" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dados do Usuário | STT - GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <meta name="description" content="Lourens" />
    <meta name="keywords" content="admin,dashboard,lourens" />
    <meta name="author" content="lourens" />

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

    <style>
        body{
                 background-color:white;
            }

        row{
                 background-color:white;
        }

        .page-title{
            background-color:#6B1846;
        }

        .new{
                background-color:#34425A;
                color:white;
            }
    </style>

</head>
<body>

    <form id="frmdetalhes" runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title">
            <h3 style="color:white">Dados do Usuário</h3>
        </div>

        <div class="row">&nbsp;</div>

        <!-- corpo -->
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-body"style="border-radius:10px;box-shadow: 0px 0px 1em #BBC0C8">
                    <div class="form-row">
                        <div class="form-group">
                            <label>ID:</label>
                            <asp:TextBox ID="txtid" ReadOnly="true" CssClass="form-control" Width="25%" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <h2>Dados do Usuário</h2>

                    <div class="form-group">
                        <label>Nome: *</label>
                        <asp:TextBox ID="txtnome" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Login: *</label>
                        <asp:TextBox ID="txtlogin" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Senha: *</label>
                        <asp:TextBox ID="txtsenha" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                    </div>


                    <h2>Nível de Acesso</h2>

                    <div class="form-group">
                        <label>Permissão:</label><p style="margin:0px"></p>
                        <asp:ListBox ID="cmbpermissao" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple" OnSelectedIndexChanged="cmbpermissao_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                    </div>

                    <div class="form-group">
                        <label>Equipe:</label><p style="margin:0px"></p>
                        <asp:ListBox ID="cmbequipe"  Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single"></asp:ListBox>
                    </div>

                    <div class="form-group">
                        <label>Produto:</label><p style="margin:0px"></p>
                        <asp:ListBox ID="cmbproduto" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single"></asp:ListBox>
                    </div>

                    <h2>Dados Localização</h2>

                    <div class="form-group">
                        <label>Latitude: *</label>
                        <asp:TextBox ID="txtLatitude" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Longitude: *</label>
                        <asp:TextBox ID="txtLongitude" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Endereco: *</label>
                        <asp:TextBox ID="txtEndereco" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <div id="mapa" style="width:100%">
                        <script async defer
                            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=initMap">
                        </script>
                    </div>


                    <div class="form-group">
                        &nbsp;
                            <label>* Campos Obrigatórios</label>
                    </div>

                    <asp:Button ID="cmdsalvar" CssClass="btn btn-success new" runat="server" Text="Salvar" OnClick="cmbenviar_Click" />
                    <asp:Button ID="cmdVoltar" CssClass="btn btn-success new" runat="server" Text="Voltar" OnClick="cmdVoltar_Click" />

                </div>
                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
            </div>
        </div>

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

        toastr["<%=Session["tipo_alerta"].ToString() %>"]("<%=Session["mensagem_alerta"].ToString().Replace(Environment.NewLine,"") %>", "<%=Session["titulo_alerta"].ToString() %>");

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


