<%@ Page Language="C#" Title="Rotas | STT - GAT Link " MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="rotas.aspx.cs" Inherits="rotas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <style>
        .col_acao {
            width: 5%;
        }
    </style>
    <script>

        function deleta(url)
        {


            var r = confirm("Deseja Realmente Desativar esse Registro?");

            if (r == true) {
                window.open(url,'_self');
            }
        }


        function acao(url) {


            var r = confirm("Deseja Realmente Realizar essa Ação ?");

            if (r == true) {
                window.open(url, '_self');
            }
        }

    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Gerenciador de Rotas</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Gerenciador de Rotas</li>
            </ol>
        </div>
    </div>


      <!-- CORPO -->
    <div id="main-wrapper">

        <!-- FILTRO -->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-white ui-sortable-handle" style="opacity: 1;">

                    <!--titulo -->
                    <div class="panel-heading">
                        <h3 class="panel-title"><i class="icon-filter"></i>&nbsp;Filtrar</h3>
                        <div class="panel-control">
                            <a href="javascript:void(0);" id="linkfiltro" data-toggle="tooltip" data-placement="top" title="" class="panel-collapse" data-original-title="Expandir / Reduzir"><i class="icon-arrow-down"></i></a>
                        </div>
                    </div>

                    <!-- corpo -->
                    <div class="panel-body" style="display: block;">

                        <!-- Rota -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Rotas:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:ListBox ID="cmbRota_filtro" runat="server" CssClass="show-tick form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <!-- Nome do endereço -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Nome do Endereço:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
                                    <asp:TextBox ID="txtnome_filtro" runat="server" CssClass="show-tick form-control"></asp:TextBox>
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
                    <asp:Button ID="cmdimportar" runat="server" CssClass="btn btn-success" Text="Importar" OnClick="cmdimportar_Click" />
                    <asp:Button ID="cmdexportar" runat="server" CssClass="btn btn-success" Text="Exportar" OnClick="cmdexportar_Click" />
                    <asp:Button ID="cmdnovo" runat="server" CssClass="btn btn-primary" Text="Novo" OnClick="cmdnovo_Click" />
                    <asp:Button ID="cmdatualizar" runat="server" CssClass="btn btn-primary" Text="Atualizar" OnClick="cmdatualizar_Click" />
                </div>
            </div>
        </div>

        <!-- ESPACO -->
        <div class="row">&nbsp;</div>

        <!-- RELATORIO -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row">

                    <div class="col-lg-12">

                        <div class="panel panel-default">

                            <div class="panel-body"style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8">

                                <!-- Row -->
                               <div class="row">
                                    <div class="col-lg-12">
                                        <span class="label label-default text-xs"><font color="black">Total de Registros: &nbsp; <asp:Label runat="server" ID="lbl_tot" /></font></span>                                        
                                    </div>
                                </div>


                                <!-- ESPACO -->
                                <div class="row">&nbsp;</div>
                                <!-- ESPACO -->
                                <div class="row">&nbsp;</div>

                                <!-- grid -->
                                <asp:Panel ID="panel_dados" Width="100%" ScrollBars="Auto" runat="server" Height="500px">
                                    <asp:GridView ID="gdw_dados" CssClass="table table-hover table-striped text-sm "
                                        runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                        GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                        HorizontalAlign="Left" Font-Names="Calibri"
                                        Width="100%" AutoGenerateColumns="true" DataKeyNames="id"
                                        PageSize="25" OnRowDataBound="gdw_dados_RowDataBound" OnRowCommand="gdw_dados_RowCommand"
                                        
                                        >
                                         
                                        <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                            PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                            NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                            FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                            LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />


                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                        </div>

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>


    </div>




</asp:Content>
