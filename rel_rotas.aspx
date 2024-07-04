<%@ Page Title="Rotas Operação | STT - GAT Link" MasterPageFile="~/MasterPageGATLink.master" Language="C#" AutoEventWireup="true" CodeFile="rel_rotas.aspx.cs" Inherits="rel_rotas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Rotas Operação</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Rotas Operação</li>
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

        <!-- RELATORIO -->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8">

                        <!-- exportar -->
                         <div class="panel-body" style="padding:0px">
                            <div class="text-right">
                                <asp:Button ID="cmdexportar" runat="server" CssClass="btn btn-xs btn-success" Text="Exportar" OnClick="cmdexportar_Click" />
                            </div>
                        </div>

                        <div class="row">&nbsp;</div>

                        <!-- grid -->
                        <div class="row" style="padding:8px">
                            <div class="col-lg-12">
                                <br />
                                <asp:Panel ID="panel_grd" Width="100%" ScrollBars="Auto" runat="server">

                                    <asp:GridView ID="gdw_dados" CssClass="table table-striped table-hover"
                                        runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                        GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                        HorizontalAlign="Left" Font-Names="Calibri"
                                        Width="100%" AutoGenerateColumns="False"
                                        PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

                                        <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                            PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                            NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                            FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                            LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Image runat="server" ID="img" ImageUrl="imagens/mapa.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Rotas" HeaderText="Rotas" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Aguardando" HeaderText="Aguardando" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Checkin" HeaderText="Check-In" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Agendamento" HeaderText="Agendadas" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="porc" HeaderText="%" ItemStyle-HorizontalAlign="Left" />

                                            
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



</asp:Content>