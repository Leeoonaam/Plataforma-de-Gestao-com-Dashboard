<%@ Page Title="Workforce | STT - GAT Link" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="carga_workforce.aspx.cs" Inherits="carga_workforce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>WorkForce</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">WorkForce</li>
            </ol>
        </div>
    </div>

    <!-- CORPO -->
    <div id="main-wrapper">

        <!-- CARGA -->
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title">Importação de Arquivo de Escala de Trabalho</h3>
                    </div>
                    <div class="panel-body">

                        <p></p>

                        <div class="form-group">
                            <label>Selecione o Arquivo:</label>
                            <asp:FileUpload ID="FileUpload_IMP" runat="server" CssClass="form-control" />
                        </div>

                        <div class="form-group">

                            <asp:Button ID="cmdImportar" class="btn btn-primary" runat="server" Text="Importar" OnClick="cmdImportar_Click" />
                        </div>

                        <asp:Panel ID="Panel_resposta" Visible="false" runat="server">

                            <div class="form-group">
                                <div class="alert <%=sstyle%>" role="alert">
                                    <asp:Label ID="lblMensagemCarga" runat="server"></asp:Label>
                                </div>
                            </div>

                        </asp:Panel>


                        <div class="form-group">
                            <div class="alert alert-warning alert-dismissible" role="alert">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                Atenção! Ao iniciar a importação aguarde até o termino da mesma para acessar outras funções.
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">&nbsp;</div>

        <!-- CARGA -->
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title">Base Importada</h3>

                    </div>

                    <div class="panel-body">
                        <p>
                            <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label></p>

                        <asp:Panel ID="panel_grd" Width="100%" ScrollBars="Auto" Visible="true" runat="server">

                            <asp:GridView ID="gdw_dados" CssClass="table table-striped table-hover"
                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                HorizontalAlign="Left" Font-Names="Calibri"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="IDCODWORKFOCE"
                                OnRowDataBound="gdw_dados_RowDataBound"
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

                                    <asp:BoundField DataField="IDCODWORKFOCE" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="MATRICULA" HeaderText="Matricula" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="ORGANIZACAO" HeaderText="Organizacao" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="HENTRADA" HeaderText="HENTRADA" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="HSAIDA" HeaderText="HSAIDA" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DURACAO" HeaderText="DURACAO" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TURNO" HeaderText="TURNO" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TSEM" HeaderText="TSEM" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TSAB" HeaderText="TSAB" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TDOM" HeaderText="TDOM" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="TFER" HeaderText="TFER" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="LOGIN" HeaderText="Login" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DACLOGIN" HeaderText="DACLOGIN" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SUPERVISOR" HeaderText="Supervisor" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SITE" HeaderText="SITE" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="NOME_ARQUIVO" HeaderText="Arquivo" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DATA_CARGA" HeaderText="Data.Carga" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="usuariocarga" HeaderText="Usuario.Carga" ItemStyle-HorizontalAlign="Left" />

                                </Columns>

                            </asp:GridView>

                        </asp:Panel>


                    </div>
                </div>
            </div>

        </div>

    </div>



</asp:Content>

