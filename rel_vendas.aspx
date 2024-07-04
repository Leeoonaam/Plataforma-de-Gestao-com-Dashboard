<%@ Page Title="Relatório de Vendas | STT - GAT Link" MasterPageFile="~/MasterPageGATLink.master" Language="C#" AutoEventWireup="true" CodeFile="rel_vendas.aspx.cs" Inherits="rel_vendas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Relatório de Vendas</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Relatório de Vendas</li>
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

        <div class="row">&nbsp;</div>

        <!-- RELATORIO: VISÃO SITE-->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8">

                        <div role="tabpanel">

                                <!-- Tab 1 -->
                                <div role="tabpanel" class="tab-pane fade active in" id="tab1">

                                    <!-- exportar -->
                                     <div class="panel-body" style="padding:0px">
                                        <div class="text-right">
                                            <asp:Button ID="cmdexportar" runat="server" CssClass="btn btn-xs btn-success" Text="Exportar" OnClick="cmdexportar_Click" />
                                        </div>
                                    </div>

                                    <div class="row">&nbsp;</div>

                                    <!-- grid -->
                                    <div class="row">
                                        
                                        <div class="col-lg-12">
                                            <asp:Panel ID="panel_grd_visaosite" Width="100%" ScrollBars="Auto" Visible="true" runat="server">

                                                <asp:GridView ID="gdw_dados" CssClass="table table-striped table-hover"
                                                    runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                    GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                    HorizontalAlign="Left" Font-Names="Calibri"
                                                    Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
                                                    PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

                                                    <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                                        PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                                        NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                                        FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                                        LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="20px">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="img" ImageUrl="imagens/finalizacoes.png" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Data" HeaderText="Data Venda" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="TipoCliente" HeaderText="Tipo Cliente" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="TipoSegmento" HeaderText="Segmento" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="CPF_CNPJ" HeaderText="CPF/CNPJ" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Endereco" HeaderText="Endereço" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Decisor" HeaderText="Decisor" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Contato" HeaderText="Contato" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="TipoVenda" HeaderText="Tipo Venda" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Check" HeaderText="Check Venda" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="TipoMaquina_Point" HeaderText="Tipo Máquina Point" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="NumeroControle_Point" HeaderText="Número Controle Point" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="FormaPagamento_Point" HeaderText="Forma Pagamento Point" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Foto_Point" HeaderText="Foto Point" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="StatusVenda_QR" HeaderText="Status Venda QR" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="CadatroPix_QR" HeaderText="Cadastro Pix QR" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="PossuiConta_QR" HeaderText="Conta QR" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Email_QR" HeaderText="E-mail QR" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="TPV_Prometido" HeaderText="TPV Prometido" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Nome_Responsavel" HeaderText="Nome Responsável" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Telefone_Responsavel" HeaderText="Telefone Responsável" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Email_MercadoPago" HeaderText="E-mail Mercado Pago" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Numero_Operacao1" HeaderText="Número Operação 1" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Numero_Operacao2" HeaderText="Número Operação 2" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Numero_Operacao3" HeaderText="Número Operação 3" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Numero_Card" HeaderText="Número card" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Chip_Anterior" HeaderText="Chip Anterior" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Chip_Atual" HeaderText="Chip Atual" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Observacao" HeaderText="Obervação" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Perfil" HeaderText="Perfil" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Equipe" HeaderText="Equipe" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="Promotor" HeaderText="Promotor" ItemStyle-HorizontalAlign="Left" />
                                                        <asp:BoundField DataField="DataAlteracao" HeaderText="Data Alteração" ItemStyle-HorizontalAlign="Left" />

                                                    </Columns>

                                                </asp:GridView>

                                            </asp:Panel>

                                        </div>

                                    </div>

                                    <!-- ESPAÇO -->
                                    <div class="row">&nbsp;</div>

                                </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

