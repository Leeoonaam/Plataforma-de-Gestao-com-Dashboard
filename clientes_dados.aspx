<%@ Page Language="C#" AutoEventWireup="true" CodeFile="clientes_dados.aspx.cs" Inherits="clientes_dados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dados do CLiente | STT - GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <meta name="description" content="leonam" />
    <meta name="keywords" content="admin,dashboard,leonam" />
    <meta name="author" content="leonam" />

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

    <script src="files/modernizr.js"></script>
    <script src="files/snap.svg-min.js"></script>
    <script type="text/javascript" src="js/canvasjs.min.js"></script>

    <script>

        //Page load
        window.onload = function () {

            //captura a url da página
            var url = window.location.href;

            //tenta localizar o ?
            var res = url.split('?');

            if (res[1] !== undefined) {
                //tenta localizar os & (pode haver mais de 1)
                var parametros = res[1].split('&');

                //qtd. de parâmetros que serão tratados pelo laço.
                var qtdParametrosParaLer = 2;

                //guarda o nome dos parâmetros e os valores e, vetores.
                var parametroEncontrado = new Array();
                var valorParametro = new Array();

                for (var i = 0; i <= qtdParametrosParaLer; i++) {
                    if (parametros[i] !== undefined) {
                        captura = parametros[i].split('=');

                        if (i == 1) {
                            parametroEncontrado[i] = captura[0];
                            valorParametro[i] = captura[1];
                            var tipocliente = valorParametro[i];
                        }                            
                    }
                }
            }

            //verifica tipo na url
            if (tipocliente != undefined) {

                if (tipocliente == 'Point%20/%20QR%20Code') {

                    //Get select object
                    var objSelect = document.getElementById("cmbtipocliente");

                    objSelect.options[1].selected = true;

                    mostra_oculta("clienteqr");

                }
                else {
                    document.getElementById('cmbtipocliente').value = 'clientefarmer';
                    mostra_oculta('clientefarmer');
                }
            }            
        }


        //Oculta dives
        function mostra_oculta(sdiv) {
            //verifica parametro
            if (sdiv != "") {

                var x = document.getElementById(sdiv);
                //verifica tipo
                if (sdiv == "clienteqr") {
                    //exibe/oculta
                    if (x.style.display == "none") {
                        document.getElementById('clienteqr').style.display = 'block';
                        document.getElementById('clientefarmer').style.display = 'none';
                        document.getElementById('clientepointqr').style.display = 'block';
                    }
                }
                else {
                    //exibe/oculta
                    if (x.style.display == "none") {
                        document.getElementById('clienteqr').style.display = 'none';
                        document.getElementById('clientefarmer').style.display = 'block';
                    }
                }
            }
            else {
                //oculta
                document.getElementById('clienteqr').style.display = 'none';
                document.getElementById('clientefarmer').style.display = 'none';
                document.getElementById('clientepointqr').style.display = 'none';
            }
        }

    </script>

    <style>
        body{
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

    <form id="frmdetalhes"  runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title">
            <h3 style=" color:white;">Dados do CLiente</h3>
        </div>

        <div class="row"  style="background-color:white">&nbsp;</div>

        <!-- corpo -->
        <div class="col-md-12" style="background-color:white;">
            <div class="panel panel-default">
                <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8;">
                    <div class="form-row">
                        <div class="form-group">
                            <label>ID:</label>
                            <asp:TextBox ID="txtid" ReadOnly="true" CssClass="form-control" Width="25%" runat="server"></asp:TextBox>
                        </div>
                    </div>


                    <h2>Dados do Cliente</h2>
                    <br />
                    <div class="form-group">
                        <label style="color:#2D3277"><b>Promotor:</b></label><p style="margin:0px;"></p>
                        <asp:ListBox ID="cmbpromotor" runat="server" Width="100%" CssClass="show-tick form-control" SelectionMode="Single">
                            <asp:ListItem Value="">Selecione:</asp:ListItem>
                        </asp:ListBox>
                    </div>

                    <div class="form-group">
                        <label style="color:#2D3277"><b>Cliente: *</b></label><p style="margin:0px;"></p>
                        <asp:ListBox ID="cmbtipocliente" runat="server" Width="100%" CssClass="show-tick form-control" onchange="mostra_oculta(this.value); return false;" SelectionMode="Single" AutoPostBack="true">
                            <asp:ListItem Value="">Selecione:</asp:ListItem>
                            <asp:ListItem Value="clienteqr">Point / QR Code</asp:ListItem>
                        </asp:ListBox>
                    </div>

                    <!-- Cliente Point Point / QR -->
                    <div id="clientepointqr" class="well well-lg" style="display: none; box-shadow: 0px 0px 1em #BBC0C8; background-color:white; border-radius: 10px;">
                        <h2 style="margin-top:5px;">Dados Point / QR</h2>
                    <!-- Cliente Point Point / QR -->
                        <div id="clienteqr" style="display: none">

                        <div class="form-group">
                            <label>Tipo Segmento:</label><p style="margin:0px;"></p>
                            <asp:ListBox ID="cmbtiposegmento" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="">Selecione:</asp:ListItem>
                                <asp:ListItem value="Academias">Academias </asp:ListItem>
                                <asp:ListItem value="Acougue_e_Peixarias">Açougue e Peixarias</asp:ListItem>
                                <asp:ListItem value="Acougues_Peixarias">Açougues / Peixarias</asp:ListItem>
                                <asp:ListItem value="Alimentos_e_Bebidas">Alimentos & Bebidas</asp:ListItem>
                                <asp:ListItem value="Alimentos_Bebidas">Alimentos Bebidas</asp:ListItem>
                                <asp:ListItem value="Bares_e_Cafeterias">Bares e Cafeterias</asp:ListItem>
                                <asp:ListItem value="Casa_e_Decoração">Casa & Decoração</asp:ListItem>
                                <asp:ListItem value="Casa_Decoracao">Casa Decoracao</asp:ListItem>
                                <asp:ListItem value="Clinicas_Veterinarias">Clinicas Veterinrias</asp:ListItem>
                                <asp:ListItem value="Delivery">Delivery</asp:ListItem>
                                <asp:ListItem value="Eletronicos">Eletrônicos</asp:ListItem>
                                <asp:ListItem value="Farmacias">Farmacias</asp:ListItem>
                                <asp:ListItem value="Fast_Food">Fast Food</asp:ListItem>
                                <asp:ListItem value="Ferragens">Ferragens</asp:ListItem>
                                <asp:ListItem value="Fruteiras">Fruteiras</asp:ListItem>
                                <asp:ListItem value="Lavanderias">Lavanderias</asp:ListItem>
                                <asp:ListItem value="Livrarias">Livrarias</asp:ListItem>
                                <asp:ListItem value="Medicos_e_Dentistas">Médicos e Dentistas</asp:ListItem>
                                <asp:ListItem value="Motocicletas">Motocicletas</asp:ListItem>
                                <asp:ListItem value="Oficinas_Mecânicas">Oficinas Mecânicas</asp:ListItem>
                                <asp:ListItem value="Oftalmologistas">Oftalmologistas</asp:ListItem>
                                <asp:ListItem value="Outros">Outros</asp:ListItem>
                                <asp:ListItem value="Padarias">Padarias</asp:ListItem>
                                <asp:ListItem value="Petshops">Petshops</asp:ListItem>
                                <asp:ListItem value="Postos_de_Gasolina">Postos de Gasolina</asp:ListItem>
                                <asp:ListItem value="Restaurante">Restaurante</asp:ListItem>
                                <asp:ListItem value="Roupas_Acessorios">Roupas Acessórios</asp:ListItem>
                                <asp:ListItem value="Roupas_e_Acessorios">Roupas e Acessórios </asp:ListItem>
                                <asp:ListItem value="Saloes_de_Beleza">Salões de beleza</asp:ListItem>
                                <asp:ListItem value="Sorveterias">Sorveterias</asp:ListItem>
                                <asp:ListItem value="Supermercados">Supermercados</asp:ListItem>
                                <asp:ListItem value="Vinhos">Vinhos</asp:ListItem>
                                
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>Nome: *</label>
                            <asp:TextBox ID="txtnome" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>CPF / CNPJ: *</label>
                            <asp:TextBox ID="txtcnpj" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="alert alert-info new">
                          <strong>Atenção!</strong>  Preencher endereço corretamente! <strong>  Exemplo: Endereço, Nº - UF </strong>
                        </div>

                        <div class="form-group">
                            <label>Endereço: *</label>
                            <asp:TextBox ID="txtendereco" placeholder="Exemplo: Rua Teste, 00 - SP" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Contato: *</label>
                            <asp:TextBox ID="txtcontato" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Decisor:</label><p style="margin:0px;"></p>
                            <asp:TextBox ID="txtdecisor" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>
                    </div>
                    <!-- Cliente Farmer -->
                    <div id="clientefarmer" style="display: none">

                        <div class="form-group">
                            <label>Nome: *</label>
                            <asp:TextBox ID="txtnomefarmer" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>E-mail: *</label>
                            <asp:TextBox ID="txtemailfarmer" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Telefone (WhatsApp): *</label>
                            <asp:TextBox ID="txtcontatofarmer" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>

                    <!-- Label informativo -->
                    <div class="form-group">
                        &nbsp;
                            <label>* Campos Obrigatórios</label>
                    </div>

                    <asp:Button ID="cmdsalvar" CssClass="btn btn-success new" runat="server" Text="Salvar" OnClick="cmdsalvar_Click" />
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
