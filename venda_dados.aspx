<%@ Page Language="C#" AutoEventWireup="true" CodeFile="venda_dados.aspx.cs" Inherits="venda_dados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dados da Venda | STT - GAT Link</title>

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

                //loop para captura do tipo
                for (var i = 0; i <= qtdParametrosParaLer; i++) {
                    if (parametros[i] !== undefined) {
                        captura = parametros[i].split('=');

                        if (i == 1) {
                            parametroEncontrado[i] = captura[0];
                            valorParametro[i] = captura[1];
                            var tipocliente = valorParametro[i]; //valor
                        }
                    }
                }
            }

            //verifica tipo na url
            if (tipocliente != undefined) {
                //verifica tipo do cliente para exibir/ocultar campos
                if (tipocliente == 'Point') {
                    mostra_oculta("Point");
                }
                else if (tipocliente == 'QR') {
                    mostra_oculta("QR");
                }
                else {

                    //farmer
                    mostra_oculta("Farmer");
                    
                    //oculta/ixibe divs de formularios
                    if (document.getElementById('cmbpossuiconta').value == 'div_experiencia') {
                        document.getElementById('div_experiencia').style.display = 'block';
                    }

                    if (document.getElementById('cmbexperiencia').value == 'div_beneficio') {
                        document.getElementById('div_beneficio').style.display = 'block';
                    }
                    else {
                        document.getElementById('div_detalhar').style.display = 'block';
                    }
                }
            }













        }

        //Oculta dives
        function mostra_oculta(sdiv) {
            
            //verifica parametro
            if (sdiv != "") {
                //verifica e força valores das divs
                if (sdiv == 1) {
                    sdiv = 'Point';
                }
                if (sdiv == 2) {
                    sdiv = 'QR';
                }
                if (sdiv == 3) {
                    sdiv = 'Farmer';
                }
                
                var x = document.getElementById(sdiv);
                
                //verifica tipo
                if (sdiv == "Point") {

                    //exibe/oculta
                    if (x.style.display == "none") {
                        document.getElementById('Farmer').style.display = 'none';
                        document.getElementById('QR').style.display = 'none';
                        document.getElementById('Point').style.display = 'block';
                    }
                }

                if (sdiv == "QR") {
                    //exibe/oculta
                    if (x.style.display == "none") {
                        document.getElementById('Point').style.display = 'none';
                        document.getElementById('QR').style.display = 'block';
                        document.getElementById('Farmer').style.display = 'none';
                    }
                }

                if (sdiv == "Farmer") {
                    //exibe/oculta
                    if (x.style.display == "none") {
                        document.getElementById('Point').style.display = 'none';
                        document.getElementById('QR').style.display = 'none';
                        document.getElementById('Farmer').style.display = 'block';
                    }
                }
                
            }
            else {
                //oculta
                document.getElementById('Point').style.display = 'none';
                document.getElementById('QR').style.display = 'none';
                document.getElementById('Farmer').style.display = 'none';
            }
        }

        //apresenta imagem selecionada do campo point
        function previewImages() {
            alert("entrou");
            var preview = document.querySelector('#preview');
            var input = document.querySelector('input[type=file]');

            if (input.files) {
                [].forEach.call(input.files, readAndPreview);
            }

            function readAndPreview(file) {

                reader = new FileReader();

                reader.addEventListener("load", function () {
                    var image = new Image();
                    image.height = 100;
                    image.title = file.name;
                    image.src = this.result;
                    preview.appendChild(image);
                });

                reader.readAsDataURL(file);

                alert("entoru");
                document.getElementById('img64').value = "";
                var imgs = "";

                var ssrc = preview.getElementsByTagName("img");

                imgs = ssrc[0].src.toString().split(',')[1];
                document.getElementById('txtimg64').value = imgs;
                imgs = ssrc[1].src.toString().split(',')[1];
                document.getElementById('txtimg64_2').value = imgs;
                imgs = ssrc[2].src.toString().split(',')[1];
                document.getElementById('txtimg64_3').value = imgs;
            }

            
        }

        //Oculta divs do formulario
        function questionario(sdiv) {
            var x = document.getElementById(sdiv);
            
            if (x.style.display == 'none') {
                x.style.display = 'block';
            } else {
                x.style.display = 'none';
            }

            if (sdiv == 'div_detalhar') {
                document.getElementById('div_beneficio').style.display = 'none';
            }

            if (sdiv == 'div_beneficio') {
                document.getElementById('div_detalhar').style.display = 'none';
            }
        }

        //Oculta campos
        function mostra_oculta_check(svalor) {
            
            if (document.getElementById("cmbCheck").value == "") {
                document.getElementById("check").style.display = "none";
                return;
            }
            document.getElementById("check").style.display = "block";

            switch (svalor) {

                //Ativação
                case "1":
                    document.getElementById("div_tpv").style.display = "block";
                    document.getElementById("div_resp").style.display = "block";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "none";
                    document.getElementById("div_chipatual").style.display = "none";
                    break;

                    //Negociação fria
                case "2":
                    document.getElementById("div_tpv").style.display = "none";
                    document.getElementById("div_resp").style.display = "none";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "none";
                    document.getElementById("div_chipatual").style.display = "none";
                    break;

                    //Negociação morna
                case "3":
                    document.getElementById("div_tpv").style.display = "none";
                    document.getElementById("div_resp").style.display = "none";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "none";
                    document.getElementById("div_chipatual").style.display = "none";
                    break;

                    //Negociação quente
                case "4":
                    document.getElementById("div_tpv").style.display = "none";
                    document.getElementById("div_resp").style.display = "none";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "none";
                    document.getElementById("div_chipatual").style.display = "none";
                    break;

                    //Pós venda
                case "5":
                    document.getElementById("div_tpv").style.display = "block";
                    document.getElementById("div_resp").style.display = "block";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "block";
                    document.getElementById("div_chipatual").style.display = "block";
                    break;

                    //Troca de maquina
                case "6":
                    document.getElementById("div_tpv").style.display = "block";
                    document.getElementById("div_resp").style.display = "block";
                    document.getElementById("div_op1").style.display = "none";
                    document.getElementById("div_op2").style.display = "none";
                    document.getElementById("div_op3").style.display = "none";
                    document.getElementById("div_numcard").style.display = "none";
                    document.getElementById("div_chipant").style.display = "block";
                    document.getElementById("div_chipatual").style.display = "block";
                    break;

                    //Vendido
                case "7":
                    document.getElementById("div_tpv").style.display = "block";
                    document.getElementById("div_email").style.display = "block";
                    document.getElementById("div_op1").style.display = "block";
                    document.getElementById("div_op2").style.display = "block";
                    document.getElementById("div_op3").style.display = "block";
                    document.getElementById("div_numcard").style.display = "block";
                    document.getElementById("div_chipant").style.display = "block";
                    document.getElementById("div_chipatual").style.display = "block";
                    break;

            }
        }

    </script>

    <style>
        body{
                 background-color:white;
            }

        .new{
            background-color:#34425A;
        }

        .page-title{
            background-color:#6B1846;
        }


    </style>

</head>
<body>

    <form id="frmdetalhes" runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title">
            <h3 style=" color:white;">Dados da Venda</h3>
        </div>

        <div class="row" style="background-color:white">&nbsp;</div>
        

        <!-- corpo -->
        <div class="col-md-12" style="background-color:white">
            <div class="panel panel-default">
                <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8">
                    <!-- ID -->
                    <div class="form-row">
                        <div class="form-group">
                            <label>ID:</label>
                            <asp:TextBox ID="txtid" ReadOnly="true" CssClass="form-control" Width="25%" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <asp:HiddenField id="txtimg64" runat="server"/>
                    <asp:HiddenField id="txtimg64_2" runat="server"/>
                    <asp:HiddenField id="txtimg64_3" runat="server"/>

                    <h2>Dados da Venda</h2>
                     <br />
                    <!-- Tipo cliente -->
                    <div class="form-group">
                        <label><b>Cliente: *</b></label>
                        <asp:ListBox ID="cmbcliente" runat="server" Width="100%" CssClass="show-tick form-control" SelectionMode="Single"></asp:ListBox>
                    </div>

                    <!-- Tipo venda -->
                    <div class="form-group">
                        <label><b>Tipo de Venda: *</b></label>
                        <asp:ListBox ID="cmbtipovenda" runat="server" Width="100%" CssClass="show-tick form-control" onchange="mostra_oculta(this.value); return false;" SelectionMode="Single" AutoPostBack="true">
                            <asp:ListItem Value="">Selecione:</asp:ListItem>
                            <asp:ListItem Value="Point">Point</asp:ListItem>
                            <asp:ListItem Value="QR">QR</asp:ListItem>
                        </asp:ListBox>
                    </div>

                    <!-- dados point -->
                    <div id="Point" class="well well-lg" style="display: none; box-shadow: 0px 0px 1em #BBC0C8; background-color:white; border-radius: 10px;">
                        <h2 style="margin-top:5px;">Dados Point</h2>
                        <div class="form-group">
                            <label>Tipo Máquina:</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbtipomaquina" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="SMART">SMART</asp:ListItem>
                                <asp:ListItem Value="PRO">PRO</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>Número de Controle: *</label>
                            <asp:TextBox ID="txtnumcontrole" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label>Forma Pagamento:</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbformapagamento" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="Credito">Crédito</asp:ListItem>
                                <asp:ListItem Value="Debito">Débito</asp:ListItem>
                                <asp:ListItem Value="Link_de_Pagamento">Link de Pagamento</asp:ListItem>
                                <asp:ListItem Value="QR">QR</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>Enviar Foto:</label>
                            <asp:FileUpload ID="filefoto" onchange="previewImages(this.value); return false;" runat="server" multiple="multiple" CssClass="btn btn-block" Style="border-radius: 10px; background-color:#34425A; color:white; width: 100%; height: 37px; font-size: 14px" />
                            <p></p>
                            <div id="preview"></div>
                        </div>

                    </div>

                    <!-- dados qr -->
                    <div id="QR" class="well well-lg" style="display: none;box-shadow: 0px 0px 1em gray; background-color:white; border-radius: 10px;">
                        <h2 style="margin-top:5px;">Dados QR</h2>
                        <div class="form-group">
                            <label>Status Venda:</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbstatusvenda" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="1">QUENTE</asp:ListItem>
                                <asp:ListItem Value="2">FRIA</asp:ListItem>
                                <asp:ListItem Value="3">SIM</asp:ListItem>
                                <asp:ListItem Value="4">NÃO</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>Cadastro do Pix:</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbcadastropix" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="Sim">SIM</asp:ListItem>
                                <asp:ListItem Value="Nao">NÃO</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>Cliente já possui conta Mercado Pago?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbclientepossui" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="Sim">SIM</asp:ListItem>
                                <asp:ListItem Value="Nao">NÃO</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div class="form-group">
                            <label>E-mail: *</label>
                            <asp:TextBox ID="txtemailQR" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>

                    <!-- Tipo Check -->
                    <div class="form-group">
                        <label><b>Check Venda: *</b></label>
                        <asp:ListBox ID="cmbCheck" runat="server" Width="100%" CssClass="show-tick form-control" onchange="mostra_oculta_check(this.value); return false;" SelectionMode="Single" AutoPostBack="true">
                            <asp:ListItem Value="">Selecione:</asp:ListItem>
                            <asp:ListItem Value="1">Ativação QR</asp:ListItem>
                            <asp:ListItem Value="2">Negociação Fria</asp:ListItem>
                            <asp:ListItem Value="3">Negociação Morna</asp:ListItem>
                            <asp:ListItem Value="4">Negociação Quente</asp:ListItem>
                            <asp:ListItem Value="5">Pós Venda</asp:ListItem>
                            <asp:ListItem Value="6">Troca de Máquina</asp:ListItem>
                            <asp:ListItem Value="7">Vendido</asp:ListItem>
                        </asp:ListBox>
                    </div>

                    <!-- dados check -->
                    <div id="check" class="well well-lg" style="display: none; box-shadow: 0px 0px 1em #BBC0C8; background-color:white; border-radius: 10px;">
                        <h2 style="margin-top:5px;">Dados Check</h2>
                        <div id="div_tpv" class="form-group">
                            <label>TPV Prometido: *</label>
                            <asp:TextBox ID="txttpv" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_resp" class="form-group">
                            <label>Nome Responsável: *</label>
                            <asp:TextBox ID="txtnomeresp" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_telresp" class="form-group">
                            <label>Telefone Responsável: *</label>
                            <asp:TextBox ID="txttelefoneresp" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_email" class="form-group">
                            <label>E-mail Mercado Pago: </label>
                            <asp:TextBox ID="txtemailmercado" CssClass="form-control" runat="server" ></asp:TextBox>
                        </div>

                        <div id="div_op1" class="form-group">
                            <label>Número de Operação 1: </label>
                            <asp:TextBox ID="txtnumop1" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_op2" class="form-group">
                            <label>Número de Operação 2: </label>
                            <asp:TextBox ID="txtnumop2" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_op3" class="form-group">
                            <label>Número de Operação 3: </label>
                            <asp:TextBox ID="txtnumop3" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_numcard" class="form-group">
                            <label>Número Card: </label>
                            <asp:TextBox ID="txtnumcard" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_chipant" class="form-group">
                            <label>Chip Anterior: </label>
                            <asp:TextBox ID="txtchipanterior" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div id="div_chipatual" class="form-group">
                            <label>Chip Atual: </label>
                            <asp:TextBox ID="txtchipatual" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                    </div>

                    <div class="form-group">
                        <label>Observação: *</label>
                        <asp:TextBox ID="txtObservacao" TextMode="MultiLine" style="resize:none; height:100px" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    
                    <div class="form-group">
                        &nbsp;
                            <label>* Campos Obrigatórios</label>
                    </div>

                    <!-- dados farmer -->
                    <div id="Farmer" style="display: none">

                        <div class="form-group">
                            <label>Possui conta Mercado Pago?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbpossuiconta" onchange="questionario(this.value)" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="div_experiencia">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div id="div_experiencia" style="display:none" class="form-group" >
                            <label>Teve uma experiência Positiva?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbexperiencia" onchange="questionario(this.value)" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="div_beneficio">Sim</asp:ListItem>
                                <asp:ListItem Value="div_detalhar">Não</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div id="div_beneficio" style="display:none" class="form-group">
                            <label>Já utilizou algum benefício?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbutilizou" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="div_beneficio">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:ListBox>
                        
                            <div>&nbsp;</div>

                            <label>Indicaria o Produto?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbindicaria" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="Sim">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:ListBox>
                        </div>

                        <div id="div_detalhar" style="display:none" class="form-group">
                            <label>Detalhar:</label>
                            <textarea id="txtdetalhar" runat="server" class="form-control" style="border-radius: 10px;width:100%; height:80px; resize:none; background-color:#ffffff"></textarea>
                        </div>

                        <div class="form-group">
                            <label>Cadastro do Pix?</label><p style="margin:0px"></p>
                            <asp:ListBox ID="cmbcadastropixFARMER" Width="100%" runat="server" CssClass="show-tick form-control" SelectionMode="Single">
                                <asp:ListItem Value="Sim">Sim</asp:ListItem>
                                <asp:ListItem Value="Nao">Não</asp:ListItem>
                            </asp:ListBox>
                        </div>

                    </div>

                    <asp:Button ID="cmdsalvar" CssClass="btn btn-success new" runat="server" Text="Salvar" OnClick="cmdsalvar_Click" />
                    <asp:Button ID="cmdVoltar" CssClass="btn btn-success new" runat="server" Text="Voltar" OnClick="cmdVoltar_Click" />

                </div>
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
