<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ImportacaoCargaNova.aspx.cs" Inherits="ImportacaoCargaNova" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Localizador e Importação de Rotas | STT - GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <meta name="description" content="Lourens" />
    <meta name="keywords" content="admin,dashboard,Leonam" />
    <meta name="author" content="leonam" />

    <link rel="shortcut icon" type="image/png" href="favicon.png" />

    <!-- Styles -->
    <!-- <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600' rel='stylesheet' type='text/css' /> -->
    <%--<link href="files/css" rel="stylesheet" type="text/css" />--%>
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

    <%--MAPA--%>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:600" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="js/jquery-ui.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/mapa.js"></script>

    <!-- Estilos Mapa -->
    <style>
        #mapa {
            width: 100%;
            height: 140px;
            border: 2px solid #ccc;
            ;
            margin-bottom: 10px;
        }

        /* =============== Estilos do autocomplete =============== */
        .ui-autocomplete {
            background: #fff;
            border-top: 1px solid #ccc;
            cursor: pointer;
            font: 15px 'Open Sans',Arial;
            margin-left: 3px;
            width: 493px !important;
            position: fixed;
        }

            .ui-autocomplete .ui-menu-item {
                list-style: none outside none;
                padding: 7px 0 9px 10px;
            }

                .ui-autocomplete .ui-menu-item:hover {
                    background: #eee;
                }

            .ui-autocomplete .ui-corner-all {
                color: #666 !important;
                display: block;
            }

            #cmdRotas{
                margin-left:5px;
            }

            #cmdremoveer{
                margin-left:5px;
            }

            body{
                 background-color:white;
            }

            .new{
                background-color:white;
                color:#6B1846;
                border-radius:10px;
                border-color:#6B1846;
                border-style:groove;
                
            }

            p.groove {border-style: solid;
                      border-color:#6B1846;
                      border-radius:10px;
                      padding:10px;
                      color:#6B1846;
            }
    </style>

    <!-- Funções -->
    <script>

        //variaveis
        var geocoder;
        var map;
        var marker;
        var array = [];
        var arr10 = new Array();
        var arr = new Array();
        var sendereco = "";
        var slat = "";
        var slong = "";
        var str;
        var dvCSV = document.getElementById("resultado");

        //Executa script
        function ExecuteScripts() {

            initialize();
        }

        //inicializa os mapas
        function initialize() {

            var latlng = new google.maps.LatLng(-23.496851, -46.8828867, 17);
            var options = {
                zoom: 15,
                center: latlng
            };

            map = new google.maps.Map(document.getElementById("mapa"), options);

            geocoder = new google.maps.Geocoder();

            marker = new google.maps.Marker({
                map: map,
                draggable: true,
            });

            marker.setPosition(latlng);
        }

        //Page load
        window.onload = function () {

            //Check the support for the File API support
            if (window.File && window.FileReader && window.FileList && window.Blob) {

                var fileSelected = document.getElementById('FileUpload_IMP');

                //assim que selecionado o arquivo o processo é iniciado
                fileSelected.addEventListener('change', function (e) {

                    

                    var fileExtension = /text.*/; //define extensão do arquivo
                    var fileTobeRead = fileSelected.files[0]; //objeto do arquivo

                    //Verifica extensão
                    if (fileTobeRead.type.match(fileExtension)) {

                        //Inicia o objeto para ler o arquivo
                        var fileReader = new FileReader();

                        fileReader.onload = function (e) {
                            document.getElementById("lblstatus").innerText = "Status: Aguardando solicitação...";
                        }
                        fileReader.readAsText(fileTobeRead);
                    }
                    else {
                        alert("Por favor selecione um Arquivo!");
                    }

                }, false);
            }
            else {
                alert("Arquivo(s) não suportado(s)!");
            }
        }

        //Inicia importação
        function Upload() {
            document.getElementById("lblstatus").innerText = "Status: Localizando...";
            //limpa campos
            arr = [];

            //arquivo
            var fileUpload = document.getElementById("FileUpload_IMP");
            //instancia
            var reader = new FileReader();

            reader.onload = function (e) {
                //cria Linha
                var rows = e.target.result.split("\n");
                //loop
                for (var i = 1; i < rows.length; i++) {
                    //cria coluna
                    var cells = rows[i].split(";");
                    var x = 1;

                    //Pesquisa
                    geocoder.geocode({ 'address': cells[0] }, function (results, status) {
                        //Verifica retorno
                        if (status == google.maps.GeocoderStatus.OK) {
                            if (results[0]) {
                                //Captura dados
                                slat = results[0].geometry.location.lat();
                                slong = results[0].geometry.location.lng();
                                sendereco = results[0].formatted_address;

                                //cria array
                                arr.push(slat + ";" + slong + ";" + sendereco + ";;\n");

                                //verifica valor da linha para exportação
                                if (x != i) {
                                    x++;
                                    if (x == i) {
                                        //var divr = document.getElementById("resultado");
                                        //divr.innerText = arr;
                                        exportar(); //chama a função Exportar

                                    }
                                }
                            }
                        }
                    })
                }
            }
            //retorna
            reader.readAsText(fileUpload.files[0]);
        }

        //Inicia exportação 
        function exportar() {

            var a = window.document.createElement('a');
            a.href = window.URL.createObjectURL(new Blob([arr.sort()], { type: 'text/txt' })); //metodo .sort() é para ordenar a sequencia no arquivo
            a.download = 'import.txt';

            // Append anchor to body.
            document.body.appendChild(a);
            a.click();

            // Remove anchor from body
            document.body.removeChild(a);

            document.getElementById("lblstatus").innerText = "Status: Exportação Concluída!";
        }

    </script>

</head>

<body>

    <form id="frmdados" runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title" style="background-color:#6B1846;">
            <h3 style=" color:white;">Localização e Importação de Rotas</h3>
        </div>

        <div class="row" style="background-color:white">&nbsp;</div>

        <!-- CORPO -->
        <div class="col-md-12"  style="background-color:white">
            <div class="panel panel-default">
                <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8;">

                    <h2>Localização e Importação de Rotas</h2>

                    <div class="row">&nbsp;</div>

                    <div class="form-group">
                        <label style="font-size:15px;margin-bottom:14px"> 1 º Selecione o Arquivo para Localizar e Roteirizar os Endereços:</label>
                        <input type="file" id="FileUpload_IMP" runat="server" class="btn btn-success btn-block" style="border-radius: 10px; width: 100%; height: 37px; font-size: 14px" />
                    </div>

                    <p id="lblstatus" class="groove">Status: Aguardando solicitação...</p>

                    <div>
                        <button id="cmdlocalizar" class="btn btn-success col-lg-2" runat="server" onclick="Upload(); return false;">Localizar / Roteirizar</button>
                    </div>

                    <br />
                    <div class="row">&nbsp;</div>
                    <div class="row">&nbsp;</div>

                    <div class="form-group">
                        <label style="font-size:15px;margin-bottom:14px">2 º Selecione a quantidade de Rotas para esse Arquivo:</label>
                        <p style="margin:0px"></p>
                        <asp:ListBox ID="cmbqtdrotas" runat="server" CssClass="col-lg-4" AutoPostBack="false" SelectionMode="Single">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                        </asp:ListBox>
                    </div>

                    <div class="row">&nbsp;</div>

                    <div class="form-group">
                        <label style="font-size:15px;margin-bottom:14px">3 º Selecione o Arquivo Localizado para Importação:</label>
                        <asp:FileUpload ID="FileUpload_IMP2" runat="server" CssClass="btn btn-success btn-block" Style="border-radius: 10px; width: 100%; height: 37px; font-size: 14px" />
                        <%--<input type="file" id="FileUpload_IMP2" runat="server" class="btn btn-success btn-block" style="border-radius: 10px; width: 100%; height: 37px; font-size: 14px" />--%>
                    </div>

                    <div>
                        <asp:Button ID="cmdImportar" CssClass="btn btn-success col-lg-2" runat="server" Text="Importar" OnClick="cmdImportar_Click" />
                        
                        <asp:Button ID="cmdRotas" CssClass="btn btn-success col-lg-2" runat="server" Text="Rotas" OnClick="cmdRotas_Click" />
                    </div>

                    <div class="row">&nbsp;</div>
                    <div class="row">&nbsp;</div>
                    <div class="row">&nbsp;</div>
                    <div class="row">&nbsp;</div>

                    <div class="form-group">
                        <label style="font-size:15px;margin-bottom:14px">Selecione o Arquivo para Alteração ou Remoção dos Endereços:</label>
                        <asp:FileUpload ID="FileRemove" runat="server" CssClass="btn btn-vk btn-block" Style="border-radius: 10px; width: 100%; height: 37px; font-size: 14px" />
                    </div>

                    <div>
                        <asp:Button ID="cmdalterar" CssClass="btn btn-vk col-lg-2" runat="server" Text="Alterar" OnClick="cmdalterar_Click" />
                        <asp:Button ID="cmdremoveer" CssClass="btn btn-vk col-lg-2" runat="server" Text="Remover" OnClick="cmdremoveer_Click" />
                    </div>

                    <div class="row">&nbsp;</div>
                    <div class="row">&nbsp;</div>

                    <div style="text-align:left">
                        <asp:Button ID="cmdVOltar" CssClass="btn btn-primary col-lg-2" runat="server" Text="Voltar" OnClick="cmdVOltar_Click" />
                    </div>

                    <div id="mapa" style="display: none;"></div>
                    <script async defer
                        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=ExecuteScripts">
                    </script>
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






