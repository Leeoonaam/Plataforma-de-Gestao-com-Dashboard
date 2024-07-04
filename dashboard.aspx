<%@ Page Title="Dashboard | STT - GAT® Link" Language="C#" MasterPageFile="~/MasterPageGATLink.Master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript">

        function formatahora(hora) {

            if (hora.value.length == 2) {
                hora.value = hora.value + ':';
            }

            if (hora.value.length == 5) {
                Verifica_Hora(hora);
            }

            return true;
        }

        function Verifica_Hora(hora) {
            hrs = (hora.value.substring(0, 2));
            min = (hora.value.substring(3, 5));

            estado = "";
            if ((hrs < 00) || (hrs > 23) || (min < 00) || (min > 59)) {
                estado = "errada";
            }

            if (hora.value == "") {
                estado = "errada";
            }

            if (estado == "errada") {
                alert("Hora invalida!");
                hora.focus();
            }
        }

    </script>

    <!-- Estilo Mapa -->
    <style>
        /* Set the size of the div element that contains the map */
        #map {
            height: 500px; /* The height is 400 pixels */
            width: 100%; /* The width is the width of the web page */
        }

        .panel.info-box.panel-white{
            -webkit-box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
            -moz-box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
            box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
        }

        .panel.panel-default{
            -webkit-box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
            -moz-box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
            box-shadow: 0px 3px 5px 0px rgba(184,175,184,1);
        }
    </style>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%--<asp:Timer ID="Timer1" runat="server" Interval="20000" OnTick="Timer1_Tick"></asp:Timer>--%>

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Dashboard </h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </div>
    </div>

    <!-- CORPO -->
    <div id="main-wrapper">

        <!-- FILTRO -->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-white ui-sortable-handle" style="opacity: 1;">

                    <!-- titulo -->
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="icon-equalizer"></i>&nbsp;Filtrar</h3>
                        <div class="panel-control">
                            <a href="javascript:void(0);" id="linkfiltro" data-toggle="tooltip" data-placement="top" title="" class="panel-collapse" data-original-title="Expandir / Reduzir"><i class="icon-arrow-down"></i></a>
                        </div>
                    </div>

                    <!-- corpo -->
                    <div class="panel-body" style="display: block;">

                        <!-- Perfil -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Perfil:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbperfil_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <!-- equipe -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Equipe:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbequipe_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <!-- usuarios -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Usuários:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbusuarios_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>


                        <!-- periodo -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Período:</label>
                            <div class="col-sm-10">

                                <div class="form-inline">

                                    <div class="form-group">
                                        <asp:CheckBox ID="chkData_Filtro" runat="server" onclick="checkfiltro(this.checked);" />
                                    </div>

                                    <div class="form-group">
                                        <asp:TextBox ID="txtdataini" CssClass="form-control date-picker" runat="server" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <span class="clockpicker" data-autoclose="true">
                                            <asp:TextBox ID="txthoraini" size="6" MaxLength="5" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </span>
                                    </div>

                                    <div class="form-group">&nbsp;Até</div>

                                    <div class="form-group">
                                        <asp:TextBox ID="txtdatafim" CssClass="form-control date-picker" runat="server" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <span class="clockpicker" data-autoclose="true">
                                            <asp:TextBox ID="txthorafim" size="6" MaxLength="5" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                        </span>
                                    </div>

                                </div>

                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>

        <!-- BOTOES DE ACAO -->
        <div class="row">
            <div class="col-lg-12">
                <div class="text-right">
                    <asp:Button ID="cmdatualizar" runat="server" CssClass="btn btn-primary" Text="Atualizar" OnClick="cmdatualizar_Click" />
                </div>
            </div>
        </div>

        <!-- ESPACO -->
        <div class="row">
            &nbsp;
        </div>

        <!-- CORPO -->
        <div class="panel-body" style="border-radius:10px;padding:2px; box-shadow: 0px 0px 1em #BBC0C8;background-color:white">
            <br />

            <div class="col-lg-12">

                <div role="tabpanel">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#tab5" role="tab" data-toggle="tab" aria-expanded="true">Visão Geral</a></li>
                        <li role="presentation" class=""><a href="#tab6" role="tab" data-toggle="tab" aria-expanded="false">Mapa - Vendedores</a></li>
                        <li role="presentation" class=""><a href="#tab7" role="tab" data-toggle="tab" aria-expanded="false">Acompanhamento - Vendedores</a></li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">

                        <!-- Controle Visal Geral  -->
                        <div role="tabpanel" class="tab-pane fade active in" id="tab5">

                            <div class="row">&nbsp;</div>

                            <!-- TABELA DO LABEL TOTAL -->
                            <table width="30%">
                                <tr>
                                    <td>
                                        <h3>
                                            <span class="label label-danger">
                                                <asp:Label ID="lblQuantidadeVendedores" runat="server" Text=""></asp:Label>
                                            </span>
                                        </h3>
                                    </td>
                                </tr>
                            </table>

                            <!-- ESPACO -->
                            <div class="row">&nbsp;</div>

                            <!-- BLOCOS GERENCIAS -->
                            <div class="row">
                                <div class="col-lg-12">

                                    <div role="tabpanel">

                                        <!-- Nav tabs -->
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li role="presentation" class="active"><a href="#tab0" role="tab" data-toggle="tab" aria-expanded="true">Visão Login/Logout</a></li>
                                            <li role="presentation" class=""><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="false">Produtividade</a></li>
                                            <li role="presentation" class=""><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false">Acompanhamento - Rotas</a></li>
                                            <li role="presentation" class=""><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false">Acompanhamento - Pontos de Proximidade</a></li>
                                        </ul>

                                        <!-- Tab panes -->
                                        <div class="tab-content">

                                            <!-- BLOCOS STATUS CTI -->
                                            <div role="tabpanel" class="tab-pane fade active in" id="tab0">

                                                <div class="row">&nbsp;</div>

                                                <!-- BLOCO 1 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_sstatusbpm=1" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lblvendedoreslogados" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">LOGADOS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-globe"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 2 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_sstatusbpm=2" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lblcheckin" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">CHECK-IN</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-arrow-up"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 3 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_sstatusbpm=3" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lblcheckout" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">CHECK-OUT</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-arrow-down"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 4 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <!-- TITULO -->
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_sstatusbpm=4" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lblQtdLogout" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">LOGOUT</span>
                                                            </div>
                                                            <!-- ICONE -->
                                                            <div class="info-box-icon">
                                                                <i class="icon-power"></i>
                                                            </div>
                                                            <!-- BARRA DE PROGRESSO-->
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-primary" role="progressbar" aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                            <!-- BLOCOS DE VENDA -->
                                            <div role="tabpanel" class="tab-pane fade" id="tab1"> 

                                                <div class="row">&nbsp;</div>

                                                 <!-- BLOCO 1 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_venda.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_sstatusbpm=" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotvendas" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">VENDAS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-list"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 2 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_venda.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_stipovenda=1" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotvendas_point" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">VENDAS - POINT</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-picture"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 3 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_venda.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_stipovenda=2" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotvendas_qr" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">VENDAS - QR</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-folder"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <!-- BLOCOS DE STATUS ROTA -->
                                            <div role="tabpanel" class="tab-pane fade" id="tab2"> 

                                                <div class="row">&nbsp;</div>

                                                <!-- BLOCO 1 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_rotas.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusrota=" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotrotas" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">TOTAL DE ROTAS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-globe-alt"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-default" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 2 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_rotas.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusrota=1" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotrotaaguardando" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">ROTAS: AGUARDANDO VISITA</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-map"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 3 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_rotas.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusrota=2" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotrotaconcluida" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">ROTAS: CONCLUÍDAS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-check"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 4 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_rotas.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusrota=3" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotrotasagendada" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">ROTAS: AGENDADAS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-calendar"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <!-- BLOCOS DE STATUS PONTOS MARCADOS -->
                                            <div role="tabpanel" class="tab-pane fade" id="tab3"> 

                                                <div class="row">&nbsp;</div>

                                                <!-- BLOCO 1 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_pontos.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusrota=" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotpontos" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">TOTAL DE PONTOS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-globe-alt"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-default" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 2 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_pontos.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusponto=1" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotpontosaguardando" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">PONTOS: AGUARDANDO VISITA</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-map"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-danger" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <!-- BLOCO 3 -->
                                                <div class="col-lg-4 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_pontos.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusponto=2" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotpontosconcluidos" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">PONTOS: CONCLUÍDOS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-check"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

<%--                                                <!-- BLOCO 4 -->
                                                <div class="col-lg-3 col-md-6">
                                                    <div class="panel info-box panel-white">
                                                        <div class="panel-body">
                                                            <div class="info-box-stats">
                                                                <p>
                                                                    <a style="text-decoration: none" href="detalhes_pontos.aspx?_sperfil=<%=_sperfil%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_statusponto=3" target="_blank">
                                                                        <span class="counter">
                                                                            <asp:Label ID="lbltotpontosagendados" runat="server"></asp:Label></span></a>
                                                                </p>
                                                                <span class="info-box-title">PONTOS: AGENDADOS</span>
                                                            </div>
                                                            <div class="info-box-icon">
                                                                <i class="icon-calendar"></i>
                                                            </div>
                                                            <div class="info-box-progress">
                                                                <div class="progress progress-xs progress-squared bs-n">
                                                                    <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>


                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <!-- ESPACO -->
                            <div class="row">&nbsp;</div><div class="row">&nbsp;</div>

                            <!-- GRAFICOS -->
                            <div class="row">

                                <%--GRAFICO STATUS--%>
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_status" style="height: 350px; width: 100%;"></div>

                                            <strong><small>
                                                <asp:Label ID="lblqtdstatus" runat="server" Text="Total: 0"></asp:Label></small></strong>
                                        </div>
                                    </div>
                                </div>

                                <!-- GRAFICO STATUS EQUIPE-->
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_status_equipe" style="height: 350px; width: 100%;"></div>

                                            <strong><small>
                                                <asp:Label ID="lblqtdstatus_equipe" runat="server" Text="Total: 0"></asp:Label></small></strong>
                                        </div>
                                    </div>
                                </div>

                                <!-- GRAFICO TIPOS DE VENDA -->
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_tipo_venda" style="height: 350px; width: 100%;"></div>

                                            <strong><small>
                                                <asp:Label ID="lblqtdtipo_venda" runat="server" Text="Total: 0"></asp:Label></small></strong>
                                        </div>
                                    </div>
                                </div>

                                <!-- GRAFICO TIPOS DE CLIENTE -->
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_tipo_cliente" style="height: 350px; width: 100%;"></div>

                                            <strong><small>
                                                <asp:Label ID="lblqtdtipo_cliente" runat="server" Text="Total: 0"></asp:Label></small></strong>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <!-- ESPACO -->
                            <div class="row">
                                <br />
                            </div>

                            <!-- GRAFICO VENDA - HORA A HORA-->
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_hora" style="height: 350px; width: 100%;"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="panel panel-default">
                                        <div class="panel-body">
                                            <div id="div_graf_venda_hora" style="height: 350px; width: 100%;"></div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                        <!-- Mapa de Unidades -->
                        <div role="tabpanel" class="tab-pane fade" id="tab6">

                            <!-- ESPACO -->
                            <div class="row">&nbsp;</div>

                            <!-- Mapa -->
                            <div class="form-group">
                                <div id="map"></div>
                                <script>
                                    // Initialize and add the map
                                    function initMap() {

                                        // The location of Uluru
                                        var uluru = { lat: -23.686, lng: -46.619 };

                                        // The map, centered at Uluru
                                        var map = new google.maps.Map(document.getElementById('map'), { zoom: 8, center: uluru });

                                        var markers = locations.map(function (location, i) {
                                            return new google.maps.Marker({

                                                position: location,

                                                label: labls[i],
                                                title: titles[i]
                                            });
                                        });

                                        var markerCluster = new MarkerClusterer(map, markers, { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });

                                    }

                                    <%Response.Write(sPlace_Map);%>
                                    
                                </script>

                                <script src="https://unpkg.com/@google/markerclustererplus@4.0.1/dist/markerclustererplus.min.js"></script>

                                <script async defer
                                    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=initMap">
                                </script>


                            </div>


                        </div>

                        <!-- MONITORAMENTO USUARIO  -->
                        <div role="tabpanel" class="tab-pane fade" id="tab7">

                            <div class="row">&nbsp;</div>

                            <div class="form-group">
                                <!-- BLOCOS GERENCIAS -->
                                <div class="row">
                                    <div class="col-lg-12">

                                        <!-- grid -->
                                        <asp:Panel ID="panel_dados" Width="100%" ScrollBars="Auto" runat="server">

                                            <asp:GridView ID="gdw_dados" CssClass="table table-striped  table-hover"
                                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                HorizontalAlign="Left" Font-Names="Calibri"
                                                Width="100%" AutoGenerateColumns="true" DataKeyNames="ID"
                                                PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

                                                <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                                    PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                                    NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                                    FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                                    LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                                <Columns>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Image runat="server" ImageUrl="imagens/usuarios.png" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>


                                            </asp:GridView>

                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>

                </div>

            </div>

        </div>




    </div>

</asp:Content>

