<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="pontos_aproximidade_dados.aspx.cs" Inherits="pontos_aproximidade_dados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dados do Pontos Proximidade | STT - GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta name="description" content="Lourens" />
    <meta name="keywords" content="admin,dashboard,lourens,Leonam" />
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

    <%--MAPA--%>
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:600" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
    <script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>
    <script type="text/javascript" src="js/jquery-ui.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/mapa.js"></script>
    <script type="text/javascript" src="js/ScriptsRotas.js"></script>



    <%--Mapa--%>
    <style>
        /* Set the size of the div element that contains the map */

        #mapa {
            width: 900px;
            height: 400px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
        }

        #map2 {
            width: 100%;
            height: 500px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
        }

        #mapa3 {
            width: 100%;
            height: 500px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
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
            body{
                 background-color:white;
            }

            .new{
                background-color: #34425A;
            }
    </style>

    <%-- script dos mapas --%>
    <script>

        function initMap() {
            
            var uluru1 = { lat: -23.686, lng: -46.619 };

            var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 8, center: uluru1 });

        }

        function toggleBounce() {
            if (marker.getAnimation() !== null) {
                marker.setAnimation(null);
            } else {
                marker.setAnimation(google.maps.Animation.BOUNCE);
            }
        }

        // Initialize and add the map
        function initMap2() {

            const labels = '123456789';

            let labelIndex = 0;

            var uluruMap = { lat: -23.4944863, lng: -46.8537396 };

            var map2 = new google.maps.Map(document.getElementById('map2'), { zoom: 8, center: uluruMap });

                 <%Response.Write(sPlace_Map_Roteiro);%>
        }

        function initMapGrid() {

            var uluruMap = { lat: -23.4944863, lng: -46.8537396 };

            var map2 = new google.maps.Map(document.getElementById('map2'), { zoom: 15, center: uluruMap });

            <%Response.Write(txtRenderizaMapa.Value);%>

        }

        function ExecuteScripts(){

            initialize();
            initMap2();
        }

        function initMap3() {

            var uluru1 = { lat: -23.686, lng: -46.619 };

            var map3 = new google.maps.Map(document.getElementById('mapa3'), { zoom: 8, center: uluru1 });// { zoom: 4, center: uluru });
        }

        function deleta(url) {
            var r = confirm("Deseja Realmente deletar esse Registro?");

            if (r == true) {
                window.open(url, '_self');
            }
        }

    </script>

    <%-- script da pagina --%>
    <script>

        function RenderizaMapa() {

            var sValor = document.getElementById('<%=txtRenderizaMapa.ClientID%>');
            var gridID = document.getElementById("<%=gdw_dados.ClientID %>");
            var tr = gridID.getElementsByTagName('tr');
            var imap = 1;
            var snome = '';
            var slati = '';
            var slong = '';
            var Endereco = '';
            var sequencia = '';
            var stringMap = '';

            sValor.value = '';

            var uluruMap = { lat: -23.4939793, lng: -46.85099779999999 };

            var map2 = new google.maps.Map(document.getElementById('map2'), { zoom: 8, center: uluruMap });

            for (var i = 1; i < tr.length; i++) {

                for (var x = 0; x < tr.item(i).getElementsByTagName('td').length; x++) {

                    if (sValor.value != "") {
                        sValor.value = sValor.value + "|"
                    }

                    sValor.value = sValor.value + tr.item(i).getElementsByTagName('td').item(x).innerHTML;

                    if (x == 1) {
                        Endereco = tr.item(i).getElementsByTagName('td').item(x).innerHTML;
                    } else if (x == 2) {
                        slati = tr.item(i).getElementsByTagName('td').item(x).innerHTML;
                    } else if (x == 3) {
                        slong = tr.item(i).getElementsByTagName('td').item(x).innerHTML;
                    } else {
                        sequencia = tr.item(i).getElementsByTagName('td').item(x).innerHTML;
                    }

                    if (x == 4) {
                        break;
                    }
                }

                var uluru1 = new google.maps.LatLng(parseFloat(slati), parseFloat(slong));

                var marker = new google.maps.Marker({ position: uluru1, map: map2, title: Endereco, animation: google.maps.Animation.DROP, label: sequencia - 1 });
                marker.addListener('click', toggleBounce);
                marker.setPosition(uluru1);

                clearFields();

            }

        }

        function AddRow(){
            var iLinha = 0;

            var table = document.getElementById('<%=gdw_dados.ClientID%>');
            var tr = table.getElementsByTagName('tr');
            var ordem = document.getElementById('txtordemAlter');

            if (ordem.value != "") {
                for (var i = 1; i < tr.length; i++) {
                    if (tr.item(i).getElementsByTagName('td').item(4).innerHTML == ordem.value) {
                        tr.item(i).getElementsByTagName('td').item(0).innerHTML = document.getElementById('txtnomeendereco').value;
                        tr.item(i).getElementsByTagName('td').item(1).innerHTML = document.getElementById('txtEndereco').value;
                        tr.item(i).getElementsByTagName('td').item(2).innerHTML = document.getElementById('txtLatitude').value;
                        tr.item(i).getElementsByTagName('td').item(3).innerHTML = document.getElementById('txtLongitude').value;

                        ordem.value = '';

                        RenderizaMapa();

                        document.getElementById("txtnomeendereco").value = '';
                        document.getElementById("txtEndereco").value = '';
                        document.getElementById("txtLatitude").value = '';
                        document.getElementById("txtLongitude").value = '';

                        clearFields();

                        return;
                    }
                }
            }


            var newRow = table.insertRow();

            var newCell0 = newRow.insertCell();
            newCell0.innerHTML = document.getElementById('txtnomeendereco').value;

            var newCell = newRow.insertCell();
            newCell.innerHTML = document.getElementById('txtEndereco').value;

            var newCell1 = newRow.insertCell();
            newCell1.innerHTML = document.getElementById('txtLatitude').value;

            var newCell2 = newRow.insertCell();
            newCell2.innerHTML = document.getElementById('txtLongitude').value;

            var newCell3 = newRow.insertCell();
            newCell3.innerHTML = '';

            for (var i = 1; i < tr.length; i++) {
                if (tr.item(i).getElementsByTagName('td').item(4).innerHTML == i) {
                    // esta correto
                    iLinha = i; 
                } else {
                    tr.item(i).getElementsByTagName('td').item(4).innerHTML = i;
                    iLinha = i;
                }
            }

            var newCell4 = newRow.insertCell();
            newCell4.innerHTML = "Aguardado";

            var newCell5 = newRow.insertCell();
            newCell5.innerHTML = "<input type='image' src='Imagens/delete.png' onclick='delrow(" + iLinha + "); return false;'>";

            var newCell6 = newRow.insertCell();
            newCell6.innerHTML = "<input type='image' src='Imagens/Alterar.png' onclick='carregaDadosAlter(" + iLinha + "); return false;'>";

            var newCell7 = newRow.insertCell();
            newCell7.innerHTML = "";

            RenderizaMapa();

            clearFields();

            return false;
        }

        function carregaDadosAlter(svalor){
            var gridID = document.getElementById("<%=gdw_dados.ClientID%>");

            var nome = document.getElementById("txtnomeendereco");
            var endereco = document.getElementById("txtEndereco");
            var latitude = document.getElementById("txtLatitude");
            var longitude = document.getElementById("txtLongitude");

            var tr = gridID.getElementsByTagName('tr');
            var iLinha = 0;
            var txt;
            var hidden = document.getElementById('txtordemAlter');

            var resposta = confirm("Você deseja alterar o roteiro selecionado ?");

            if (resposta == true) {
                for (var i = 1; i < tr.length; i++) {
                    if (tr.item(i).getElementsByTagName('td').item(4).innerHTML == svalor) {
                        nome.value = tr.item(i).getElementsByTagName('td').item(0).innerHTML;
                        endereco.value = tr.item(i).getElementsByTagName('td').item(1).innerHTML;
                        latitude.value = tr.item(i).getElementsByTagName('td').item(2).innerHTML;
                        longitude.value = tr.item(i).getElementsByTagName('td').item(3).innerHTML;
                        hidden.value = tr.item(i).getElementsByTagName('td').item(4).innerHTML;
                    }
                }

            } else {
                return false;
            }

            return false;
        }

        function delrow(svalor){

            var gridID = document.getElementById("<%=gdw_dados.ClientID%>");
            var tr = gridID.getElementsByTagName('tr');
            var iLinha = 0;
            var txt;

            var resposta = confirm("Você deseja remover os roteiros selecionados ?");

            if (resposta == true) {
                for (var i = 1; i < tr.length; i++) {
                    if (tr.item(i).getElementsByTagName('td').item(4).innerHTML == svalor) {
                        gridID.deleteRow(i);
                        document.getElementById("<%=gdw_dados.ClientID%>").value = gridID.rows.length - 1;
                    }
                }

                for (var i = 1; i < tr.length; i++) {
                    if (tr.item(i).getElementsByTagName('td').item(4).innerHTML == i) {

                        iLinha = i;
                        tr.item(i).getElementsByTagName('td').item(6).innerHTML = "<input type='image' src='Imagens/delete.png' onclick='delrow(" + iLinha + "); return false;'>";
                        tr.item(i).getElementsByTagName('td').item(7).innerHTML = "<input type='image' src='Imagens/Alterar.png' onclick='carregaDadosAlter(" + iLinha + "); return false;'>";


                    } else {
                        iLinha = i;
                        tr.item(i).getElementsByTagName('td').item(4).innerHTML = i;
                        tr.item(i).getElementsByTagName('td').item(6).innerHTML = "<input type='image' src='Imagens/delete.png' onclick='delrow(" + iLinha + "); return false;'>";
                        tr.item(i).getElementsByTagName('td').item(7).innerHTML = "<input type='image' src='Imagens/Alterar.png' onclick='carregaDadosAlter(" + iLinha + "); return false;'>";

                    }
                }

                RenderizaMapa();

                clearFields();

            } else {
                return false;
            }

            return false;
        }

        function clearFields(){
            document.getElementById("txtnomeendereco").value = '';
            document.getElementById("txtEndereco").value = '';
            document.getElementById("txtLatitude").value = '';
            document.getElementById("txtLongitude").value = '';
        }

        function PegaDadosGrid_E_UsuarioRelacionados(){

            // VARIAVEIS DOS USUARIOS RELACIONADOS
            var gridID = document.getElementById("<%=gdw_dados.ClientID %>");
            var tr = gridID.getElementsByTagName('tr');
            var txt = document.getElementById('txtDadosGrid');

            for (var i = 1; i < tr.length; i++) {
                for (var x = 0; x < tr.item(i).getElementsByTagName('td').length; x++) {

                    if (x > 0) {
                        txt.value = txt.value + ";"
                    }

                    if (x == 5) {
                        x = x + 3;
                    }
                        txt.value = txt.value + tr.item(i).getElementsByTagName('td').item(x).innerHTML;
                    
                }
                txt.value = txt.value + "|";
            }
        }

    </script>

</head>
<body>

    <form id="frmrotas_dados" runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title" style="background-color:#6B1846;">
            <h3 style="color:white;">Dados do Ponto Proximidade</h3>
        </div>

        <div class="row">&nbsp;</div>

        <!-- CORPO -->
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8";>
                    <div class="form-row">
                        <div class="form-group">
                            <label>ID:</label>
                            <asp:TextBox ID="txtid" ReadOnly="true" CssClass="form-control" Width="25%" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    &nbsp;

                    <asp:HiddenField ID="txtDadosGrid" runat="server" />
                    <asp:HiddenField ID="txtordemAlter" runat="server" />
                    <asp:HiddenField ID="txtRenderizaMapa" runat="server" />

                    <h2>Dados do Ponto Proximidade</h2>
                    &nbsp;
                    <div class="form-group">
                        <label>Descrição: *</label>
                        <asp:TextBox ID="txtnome" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                    <p></p>

                    &nbsp;
                    <h2>Pontos</h2>
                    &nbsp;

                    <!-- Painel de Roteiro -->
                    <div class="form-group">
                        <div role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true">Dados</a></li>
                                <li role="presentation" class=""><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false">Mapa</a></li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">

                                <!-- Dados  -->
                                <div role="tabpanel" class="tab-pane fade active in" id="tab1">
                                    <table>
                                        <tr>
                                            <td class="col-md-6">
                                                <div class="form-group">
                                                    <label>Nome: </label>
                                                    <asp:TextBox ID="txtnomeendereco" runat="server" placeholder="Digite o Nome para o Endereço" CssClass="show-tick form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Endereço: *</label>
                                                    <asp:TextBox ID="txtEndereco" runat="server" placeholder="Digite o Endereço" CssClass="show-tick form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Latitude:</label>
                                                    <asp:TextBox ID="txtLatitude" runat="server" ReadOnly="true" placeholder="Latitude" CssClass="show-tick form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Longitude</label>
                                                    <asp:TextBox ID="txtLongitude" runat="server" ReadOnly="true" placeholder="Longitude" CssClass="show-tick form-control"></asp:TextBox>
                                                </div>

                                                &nbsp;
                                            <div class="text-center">
                                                <asp:Button ID="cmdAdicionar_roteiro" Style="width: 120px" ToolTip="Adicionar Rota." CssClass="btn btn-success new" runat="server" Text="Adicionar" OnClientClick="AddRow(); return false;" />
                                                <asp:Button ID="cmdLimparCampos" Style="width: 120px" ToolTip="Limpar Campos." CssClass="btn btn-success new" runat="server" Text="Limpar Campos" OnClientClick="clearFields(); return false;" />
                                            </div>
                                            </td>



                                            <td class="col-md-12">
                                                <div id="mapa">
                                                </div>


                                                <script async defer
                                                    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=ExecuteScripts">
                                                </script>

                                            </td>
                                        </tr>

                                    </table>

                                </div>

                                <!-- Mapa roteiro  -->
                                <div role="tabpanel" class="tab-pane fade" id="tab2">
                                    <div id="map2"></div>
                                </div>
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>

                        <div class="col-md-12" style="border-radius:10px;box-shadow: 0px 0px 1em #BBC0C8">
                            <br />
                            <!-- grid -->
                            <asp:Panel ID="panel_dados" Width="100%" ScrollBars="Auto" runat="server" Height="500px">
                                <asp:GridView ID="gdw_dados" CssClass="table table-hover table-striped text-sm"
                                    runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                    GridLines="None" ShowFooter="true" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                    HorizontalAlign="Left" Font-Names="Calibri"
                                    Width="100%" AutoGenerateColumns="false"
                                    PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

                                    <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                        PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                        NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                        FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                        LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />
                                    <Columns>
                                        <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Endereco" HeaderText="Endereço" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Latitude" HeaderText="Latitude" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Longitude" HeaderText="Longitude" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Sequencia" HeaderText="Ordem" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="remover" HeaderText="Remover" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Alterar" HeaderText="Alterar" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        &nbsp;

                    </div>

                    <div class="form-group">
                        &nbsp;
                            <label>* Campos Obrigatórios</label>
                    </div>

                    <div class="text-left">
                        <asp:button id="cmdsalvar" cssclass="btn btn-success new" runat="server" text="Salvar" onclientclick="PegaDadosGrid_E_UsuarioRelacionados();" xmlns:asp="#unknown" onclick="cmdsalvar_Click" />
                        <asp:Button ID="cmdCancelar" CssClass="btn btn-success new" runat="server" Text="Cancelar" OnClick="cmdCancelar_Click" />
                    </div>
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

