<%@ Page Title="Relatório Gerencial - Não Tabulado | STT - GAT Link" MasterPageFile="~/MasterPageGATLink.master" Language="C#" AutoEventWireup="true" CodeFile="rel_gerencial_naotabulados.aspx.cs" Inherits="rel_gerencial_naotabulados" %>

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

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <%--<asp:Timer ID="Timer1" runat="server" Enabled="true" Interval="20000" OnTick="Timer1_Tick"></asp:Timer>--%>

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Relatório Gerencial Não Tabulado</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Relatório Gerencial Não Tabulado</li>
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

                        <!-- campanha -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Campanha:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbcampanhas_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>

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


                        <!-- produtos -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Tabulado:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:DropDownList ID="cmbTabulado" Style="width: 300px" CssClass="form-control" Font-Size="Medium" runat="server">
                                        <asp:ListItem Value=""> </asp:ListItem>
                                        <asp:ListItem Value="1">SIM</asp:ListItem>
                                        <asp:ListItem Value="0">NÃO</asp:ListItem>
                                    </asp:DropDownList>
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

        <!-- NAO TABULADOS -->
        <div class="row">
            <div class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div role="tabpanel">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">

                                <li role="presentation" class="active"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true">Não Tabulados</a></li>
                                <li role="presentation" class=""><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false">Equipe</a></li>
                                <li role="presentation" class=""><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false">Operador</a></li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content">
                                <!-- Tab 1 -->
                                <div role="tabpanel" class="tab-pane fade active in" id="tab1">
                                    <!-- grid -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Panel ID="panel_grd" Width="100%" Height="500px" ScrollBars="Auto" Visible="true" runat="server">
                                                <!-- grid -->
                                                <asp:GridView ID="gdw_dados" CssClass="table table-striped table-hover"
                                                    runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                    GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                    HorizontalAlign="Left" Font-Names="Calibri"
                                                    Width="100%" AutoGenerateColumns="true"
                                                    PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

                                                    <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                                        PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                                        NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                                        FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                                        LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="imagens/os.png" /></ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                            </asp:Panel>
                                        </div>
                                    </div>

                                     <!-- EVOLUCAO HORA A HORA : IMPORTACAO -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">

                                                <div class="panel-body">

                                                    <div id="div_graf_diadia" style="height: 350px; width: 100%;"></div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <!-- Tab 2 -->
                                <div role="tabpanel" class="tab-pane fade" id="tab2">
                                    <!-- grid -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Panel ID="panel_grd_equipe" Width="100%" Height="500px" ScrollBars="Auto" Visible="true" runat="server">
                                                <!-- grid -->
                                                <asp:GridView ID="gdw_dados_equipe" CssClass="table table-striped table-hover"
                                                    runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                    GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                    HorizontalAlign="Left" Font-Names="Calibri"
                                                    Width="100%" AutoGenerateColumns="true"
                                                    PageSize="25" OnRowDataBound="gdw_dados_equipe_RowDataBound">

                                                    <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                                        PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                                        NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                                        FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                                        LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="imagens/os.png" /></ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                            </asp:Panel>
                                        </div>
                                    </div>

                                     <!-- EVOLUCAO HORA A HORA : IMPORTACAO -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">

                                                <div class="panel-body">

                                                    <div id="div_graf_equipe_diadia" style="height: 350px; width: 100%;"></div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>    

                                </div>

                                <!-- Tab 3 -->
                                <div role="tabpanel" class="tab-pane fade" id="tab3">
                                    <!-- grid -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:Panel ID="panel_grd_operador" Width="100%" Height="500px" ScrollBars="Auto" Visible="true" runat="server">
                                                <!-- grid -->
                                                <asp:GridView ID="gdw_dados_operador" CssClass="table table-striped table-hover"
                                                    runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                    GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                    HorizontalAlign="Left" Font-Names="Calibri"
                                                    Width="100%" AutoGenerateColumns="true"
                                                    PageSize="25" OnRowDataBound="gdw_dados_operador_RowDataBound" >

                                                    <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                                        PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                                        NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                                        FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                                        LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton runat="server" ImageUrl="imagens/os.png" /></ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>

                                                </asp:GridView>

                                            </asp:Panel>
                                        </div>
                                    </div>

                                     <!-- EVOLUCAO HORA A HORA : IMPORTACAO -->
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="panel panel-default">

                                                <div class="panel-body">

                                                    <div id="div_graf_operador_diadia" style="height: 350px; width: 100%;"></div>

                                                </div>
                                            </div>
                                        </div>
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