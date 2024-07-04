<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="monitorar_servicos_tab_netsms.aspx.cs" Inherits="monitorar_servicos_tab_netsms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <!-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>-->


    <!-- auto refresh -->
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="30000"></asp:Timer>

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Serviço RPA Tabulação NET SMS</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Serviço RPA Tabulação NET SMS</li>
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
                            <label class="col-sm-2 control-label">Skill:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbperfil_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
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

        <div class="row">&nbsp;</div>

        <br />

        <div>

            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <!-- relatorio -->
                        <div class="col-lg-12">

                            <div class="col-lg-12">
                                <br />
                            </div>

                            <!--BLOCOS GERENCIAIS-->
                            <div class="col-lg-3 col-md-6">
                                <div class="panel info-box panel-white">
                                    <div class="panel-body">
                                        <div class="info-box-stats">
                                            <p>
                                                <a style="text-decoration: none; color: black;" href="detalhes_rpa.aspx?_scampanha=<%=_scampanha%>&_sperfil=<%=_sperfil%>&_sequipe=<%=_sequipe%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_socorrencia=<%=_socorrencia%>&_scontrato=<%=_scontrato%>&_scampanhaBKO=<%=_scampanhabko%>&_sstatusbpm=10&_sidstatusnetsms=0,4" target="_blank">
                                                    <span class="counter">
                                                        <asp:Label ID="lblTotRegistros" runat="server"></asp:Label><asp:Label ID="lblTotPendentes_calc" Visible="false" runat="server"></asp:Label></span>
                                                </a>
                                            </p>
                                            <span class="info-box-title">Total de Registros</span>
                                        </div>
                                        <div class="info-box-icon">
                                            <i class="icon-folder"></i>
                                        </div>
                                        <div class="info-box-progress">
                                            <div class="progress progress-xs progress-squared bs-n">
                                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6">
                                <div class="panel info-box panel-white">
                                    <div class="panel-body">
                                        <div class="info-box-stats">
                                            <p>
                                                <a style="text-decoration: none; color: black;" href="detalhes_rpa.aspx?_scampanha=<%=_scampanha%>&_sperfil=<%=_sperfil%>&_sequipe=<%=_sequipe%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_socorrencia=<%=_socorrencia%>&_scontrato=<%=_scontrato%>&_scampanhaBKO=<%=_scampanhabko%>&_sstatusbpm=10&_sidstatusnetsms=11" target="_blank">
                                                    <span class="counter">
                                                        <asp:Label ID="lblTotEmProcessamento" runat="server"></asp:Label><asp:Label ID="Label2" Visible="false" runat="server"></asp:Label></span>
                                                </a>
                                            </p>
                                            <span class="info-box-title">Em Processamento</span>
                                        </div>
                                        <div class="info-box-icon">
                                            <i class="icon-reload"></i>
                                        </div>
                                        <div class="info-box-progress">
                                            <div class="progress progress-xs progress-squared bs-n">
                                                <div class="progress-bar progress-bar-warning" role="progressbar" aria-valuenow="80" aria-valuemin="0" aria-valuemax="100" style="width: 80%">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-6">
                                <div class="panel info-box panel-white">
                                    <div class="panel-body">
                                        <div class="info-box-stats">
                                            <p>
                                                <a style="text-decoration: none; color: black;" href="detalhes_rpa.aspx?_scampanha=<%=_scampanha%>&_sperfil=<%=_sperfil%>&_sequipe=<%=_sequipe%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_socorrencia=<%=_socorrencia%>&_scontrato=<%=_scontrato%>&_scampanhaBKO=<%=_scampanhabko%>&_sstatusbpm=10&_sidstatusnetsms=12" target="_blank">
                                                    <span class="counter">
                                                        <asp:Label ID="lblTotProcess" runat="server"></asp:Label></span>
                                                </a>
                                            </p>
                                            <span class="info-box-title">Processados</span>
                                        </div>
                                        <div class="info-box-icon">
                                            <i class="icon-like"></i>
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

                            <div class="col-lg-3 col-md-6">
                                <div class="panel info-box panel-white">
                                    <div class="panel-body">
                                        <div class="info-box-stats">
                                            <p>
                                                <a style="text-decoration: none; color: black;" href="detalhes_rpa.aspx?_scampanha=<%=_scampanha%>&_sperfil=<%=_sperfil%>&_sequipe=<%=_sequipe%>&_susuario=<%=_susuario%>&_sdtini=<%=_sdtini%>&_sdtfim=<%=_sdtfim%>&_socorrencia=<%=_socorrencia%>&_scontrato=<%=_scontrato%>&_scampanhaBKO=<%=_scampanhabko%>&_sstatusbpm=10&_sidstatusnetsms=13" target="_blank">
                                                    <span class="counter">
                                                        <asp:Label ID="lblTotErroProcess" runat="server"></asp:Label></span>
                                                </a>
                                            </p>
                                            <span class="info-box-title">Erro Processo</span>
                                        </div>
                                        <div class="info-box-icon">
                                            <i class="icon-dislike"></i>
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


                            <!-- BOTOES DE ACAO -->
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="text-left">
                                        <font size="4">
                                            <span class="label label-info">
                                                
                                                <i class="icon-reload"></i>

                                                &nbsp;Tempo Médio de Processamento:
                                                <asp:Label ID="lblTMT" Text="0" runat="server"></asp:Label>
                                                    
                                            </span>
                                                </font>
                                    </div>

                                    <div class="text-right">
                                        <asp:Button ID="cmdretrab_emprocess" runat="server" CssClass="btn btn-warning" Text="Reprocessar - Em Processamento" OnClick="cmdretrab_emprocess_Click" />
                                        &nbsp;
                                            <asp:Button ID="cmdretrab_erro" runat="server" CssClass="btn btn-danger" Text="Reprocessar - Erros" OnClick="cmdretrab_erro_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12">
                                <br />
                            </div>

                            <!-- grid -->
                            <asp:GridView ID="gdw_dados" CssClass="table table-striped  table-hover"
                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                HorizontalAlign="Left" Font-Names="Calibri"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="idcodrobo"
                                OnRowCommand="gdw_dados_RowCommand"
                                PageSize="25">

                                <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                    PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                    NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                    FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                    LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="img" ImageUrl="imagens/service_mini.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="robo" HeaderText="Robo" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="STATUS" HeaderText="Ação" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="dataSTATUS" HeaderText="Data" ItemStyle-HorizontalAlign="Left" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="cmdremover" ImageUrl="imagens/delete.png" ToolTip="Parar Robo" CommandName="remover"
                                                CommandArgument='<%#Eval("idcodrobo") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <div>

            <div class="col-lg-12">
                <div class="panel panel-white">

                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="icon-equalizer"></i>&nbsp;&nbsp;Detalhes do Processamento</h3>

                    </div>

                    <div class="panel-body">

                        <!-- grid -->
                        <asp:GridView ID="gdw_dados_operadores" CssClass="table table-striped table-hover"
                            runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                            GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                            HorizontalAlign="Left" Font-Names="Calibri"
                            Width="100%" AutoGenerateColumns="False"
                            PageSize="25" OnRowDataBound="gdw_dados_operadores_RowDataBound" OnRowCommand="gdw_dados_operadores_RowCommand">

                            <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="img" ImageUrl="imagens/status_lista.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="desc" HeaderText="Descrição" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Left" />
                                <asp:BoundField DataField="perc" HeaderText="%" ItemStyle-HorizontalAlign="Left" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="cmdliberar" ImageUrl="imagens/retrab.png" ToolTip="Reprocessar Registro" CommandName="liberar"
                                            CommandArgument='<%#Eval("desc") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <!--</ContentTemplate>
    </asp:UpdatePanel>-->

</asp:Content>

