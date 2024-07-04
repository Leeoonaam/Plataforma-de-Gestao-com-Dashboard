<%@ Page Title="Controle de Usuário | STT - GAT Link" MasterPageFile="~/MasterPageGATLink.master" Language="C#" AutoEventWireup="true" CodeFile="~/controle_usuarios.aspx.cs" Inherits="controle_usuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Controle de Usúario </h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li class="active">Tela que permite realizar o controle de usuário.</li>
            </ol>
        </div>
    </div>

    <!-- CORPO -->
    <div id="main-wrapper">

        <!-- EXCLUSÃO - ORDEM -->
       <%-- <asp:Panel ID="panel_remover_ordem" Visible="false" runat="server">
            <div style="z-index: 1; width: 100%; height: 400px; position: absolute; top: 0; left: 0;">
                <div style="z-index: 2; position: absolute; left: 30%; top: 50%; margin-left: -80px; margin-top: -40px;">
                    <div class="alert alert-warning">
                        <br />
                        <table>
                            <tr>
                                <td colspan="2" align="center">
                                    <h3>Deseja realmente Excluir esse Registro ?</h3>
                                    <p>
                                        <asp:HiddenField ID="hfTipoDeDelete" runat="server" />
                                        <asp:HiddenField ID="hfDelIdDelete" runat="server" />
                                    </p>
                                    <br />
                                </td>
                            </tr>

                            <tr>
                                <td align="left">
                                    <asp:Button ID="cmdconfir_del_registro" class="btn btn-success" runat="server"
                                        Text="Confirmar Exclusão" OnClick="cmdconfir_del_registro_Click"></asp:Button>
                                </td>

                                <td align="right">
                                    <asp:Button ID="cmdcancel_del_registro" class="btn btn-danger" runat="server"
                                        Text="Cancelar Exclusão" OnClick="cmdcancel_del_registro_Click"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                   <%-- <Scripts>
                       <asp:ScriptReference Name="jquery" />
                    </Scripts>--%>
                </asp:ScriptManager>
            </div>
        </asp:Panel>

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
                            <label class="col-sm-2 control-label">Produtos:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbprodutos_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
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
                    <asp:Button ID="cmdNovoUsuario" runat="server" CssClass="btn btn-primary" PostBackUrl="~/ger_usuarios.aspx" Text="Novo Usuário" />
                    <asp:Button ID="cmdAtualizar" runat="server" CssClass="btn btn-primary" Text="Atualizar" OnClick="cmdatualizar_Click"/>
                </div>

            </div>
        </div>

        <!-- ESPACO -->
        <div class="row">
            &nbsp;
        </div>

        <!-- TABELA -->
        <div class="row">
            <div class="col-md-12">

                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title">USÚARIOS</h3>
                    </div>

                    <div class="panel-body">
                        <p>
                            <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                        </p>

                        <asp:Panel ID="panel_grd" Width="100%" ScrollBars="Auto" Visible="true" runat="server">

                            <asp:GridView ID="gdw_dados" CssClass="table table-striped table-hover"
                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                HorizontalAlign="Left" Font-Names="Calibri"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="Nome"
                                OnRowDataBound="gdw_dados_RowDataBound" OnRowCommand="gdw_dados_RowCommand"
                                PageSize="25">

                                <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                    PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                    NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                    FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                    LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="img" ImageUrl="imagens/usuario.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Login" HeaderText="Login" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="STATUS_LOGIN" HeaderText="Status Login" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Bloqueio" HeaderText="Bloqueio" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="NumRamal" HeaderText="Ramal" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="CodVendedor" HeaderText="Cod. Vendedor" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="CPFVendedor" HeaderText="CPF Vendedor" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="IdCodUsuario" HeaderText="Código" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TIPO_LOGIN" HeaderText="Tipo Login" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Recicla" HeaderText="Reciclagem" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Conectado" HeaderText="Conectado" ItemStyle-HorizontalAlign="Left" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton PostBackUrl="~/ger_usuarios.aspx" runat="server" ID="cmdAlt" ImageUrl="imagens/alterar.png" ToolTip="Alterar" CommandName="alterar"
                                                CommandArgument='<%#Eval("IDCODUSUARIO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="cmdDel" ImageUrl="imagens/delete.png" ToolTip="Deletar" CommandName="deletar"
                                                CommandArgument='<%#Eval("IDCODUSUARIO") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>

                        </asp:Panel>

                    </div>
                </div>
            </div>

        </div>
</asp:Content>
