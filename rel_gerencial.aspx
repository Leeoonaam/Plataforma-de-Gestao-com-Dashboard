<%@ Page Title="GATLink - Gerencial" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="rel_gerencial.aspx.cs" Inherits="rel_gerencial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Gerencial Call Center</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Gerencial Operação</li>
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


                        <!-- perfil -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Perfil:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbPerfil_Filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
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

        <div class="panel-body"style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8;background-color:white">
            <br />
            <div class="col-lg-12">
                <div role="tabpanel">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#tab1" role="tab" data-toggle="tab" aria-expanded="true">Produtos</a></li>
                        <li role="presentation" class=""><a href="#tab2" role="tab" data-toggle="tab" aria-expanded="false">Equipes</a></li>
                        <li role="presentation" class=""><a href="#tab3" role="tab" data-toggle="tab" aria-expanded="false">Vendedores</a></li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">

                        <%-- RELATORIO POR PRODUTO --%>
                        <div role="tabpanel" class="tab-pane fade active in" id="tab1">

                            <!-- ESPACO -->
                            <div class="row">
                                &nbsp;
                            </div>


                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group" >

                                        <div class="row">
                                            <div class="col-lg-12">
                                                

                                                <!-- grid -->
                                                <asp:Panel ID="panel2" Width="100%" ScrollBars="Auto" runat="server">

                                                    <asp:GridView ID="gdw_Produto" CssClass="table table-striped  table-hover"
                                                        runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                        GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                        HorizontalAlign="Left" Font-Names="Calibri"
                                                        Width="100%" AutoGenerateColumns="false" DataKeyNames="IDCodPerfil"
                                                        PageSize="25" OnRowDataBound="gdw_Produto_RowDataBound">

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

                                                            <asp:BoundField DataField="IDCodPerfil" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="DESCRICAO" HeaderText="DESCRICAO" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_VENDEDORES" HeaderText="TOTAL_VENDEDORES" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_VENDEDORES_LOGADOS" HeaderText="Login" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_VENDEDORES_CHECKIN" HeaderText="Checkin" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_VENDEDORES_CHECKOUT" HeaderText="Chekout" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_VENDEDORES_LOGOUT" HeaderText="Logout" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:BoundField DataField="TOTAL_TRACKIN" HeaderText="Trackin" ItemStyle-HorizontalAlign="Left" />
                                                        </Columns>

                                                    </asp:GridView>

                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <%-- RELATORIO POR EQUIPE --%>
                        <div role="tabpanel" class="tab-pane fade" id="tab2">

                            <!-- ESPACO -->
                            <div class="row">&nbsp;</div>

                            <div class="form-group">
                                <div class="row"style="padding:8px;">
                                    <div class="col-lg-12">

                                        <!-- grid -->
                                        <asp:Panel ID="panel1" Width="100%" ScrollBars="Auto" runat="server">

                                            <asp:GridView ID="gdw_Equipe" CssClass="table table-striped  table-hover"
                                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                HorizontalAlign="Left" Font-Names="Calibri"
                                                Width="100%" AutoGenerateColumns="false" DataKeyNames="IDCodPerfil"
                                                PageSize="25" OnRowDataBound="gdw_Equipe_RowDataBound">

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

                                                    <asp:BoundField DataField="IDCodPerfil" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="DESCRICAO" HeaderText="DESCRICAO" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_VENDEDORES" HeaderText="TOTAL_VENDEDORES" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_VENDEDORES_LOGADOS" HeaderText="Login" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_VENDEDORES_CHECKIN" HeaderText="Checkin" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_VENDEDORES_CHECKOUT" HeaderText="Chekout" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_VENDEDORES_LOGOUT" HeaderText="Logout" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TOTAL_TRACKIN" HeaderText="Trackin" ItemStyle-HorizontalAlign="Left" />

                                                </Columns>

                                            </asp:GridView>

                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>

                        </div>

                        <%-- RELATORIO POR USUARIO --%>
                        <div role="tabpanel" class="tab-pane fade" id="tab3">

                            <div class="row">&nbsp;</div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-lg-12">

                                        <!-- grid -->
                                        <asp:Panel ID="panel3" Width="100%" ScrollBars="Auto" runat="server">

                                            <asp:GridView ID="gdw_Usuarios" CssClass="table table-striped  table-hover"
                                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                                HorizontalAlign="Left" Font-Names="Calibri"
                                                Width="100%" AutoGenerateColumns="false" DataKeyNames="IDCodUsuario"
                                                PageSize="25" OnRowDataBound="gdw_Usuarios_RowDataBound">

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

                                                    <asp:BoundField DataField="IDCodUsuario" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Login" HeaderText="Login" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Checkin" HeaderText="Checkin" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Chekout" HeaderText="Chekout" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Logout" HeaderText="Logout" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="Treckin" HeaderText="Trackin" ItemStyle-HorizontalAlign="Left" />

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
