<%@ Page Title="Formulário do Registro Não Tabulado" MasterPageFile="~/MasterPageGATLink.master" Language="C#" AutoEventWireup="true" CodeFile="ger_naotabulados.aspx.cs" Inherits="ger_naotabulados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Formulário Não Tabulado</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Controle Não Tabulados</li>
                <li class="active">Formulário Não Tabulado</li>
            </ol>
        </div>
    </div>

    <br />
    <div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-body">

                    <!--ERRO-->
                    <asp:Panel ID="panel_erro" Width="100%" Visible="false" runat="server">
                        <div class="panel panel-danger">
                            <div class="panel-heading">
                                <i class="fa fa-warning"></i>&nbsp;<asp:Label ID="lblerro" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </asp:Panel>

                    <!-- Cliente -->
                    <asp:Panel ID="panel_cadastrar" Width="100%" Visible="true" runat="server">
                        <h2 class="page-header"><i class="fa fa-clipboard"></i>&nbsp;Dados do Cliente
                                <small>informações do cliente no registro</small>
                        </h2>
                    </asp:Panel>

                    <!-- Formulario -->
                    <div class="col-lg-12">
                        <table class="table table-hover">
                            <!-- DADOS DO USUARIO - Primeira Linha -->
                            <tr>
                                <td>
                                    <h3><strong>Nome do Cliente:</strong></h3>
                                    <asp:HiddenField ID="txtidcodregistro" runat="server" />

                                    <asp:TextBox ID="txtNome" Enabled="false" required="" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>CPF:</strong></h3>

                                    <asp:TextBox ID="txtCPF" Enabled="false" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Cidade:</strong></h3>

                                    <asp:TextBox ID="txtCidade" Enabled="false" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="300px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <!-- Segunda Linha -->
                            <tr>

                                <td>
                                    <h3><strong>Contrato:</strong></h3>

                                    <asp:TextBox ID="txtContrato" Enabled="false" required=""  CssClass="form-control" Style="resize: none" Rows="5" Font-Size="Medium" runat="server" Width="360"></asp:TextBox>
                                </td>

                                <td></td>
                                <td></td>

                            </tr>

                        </table>

                        <!-- Operador -->
                        <asp:Panel ID="panel2" Width="100%" runat="server">
                            <h2 class="page-header"><i class="fa fa-user"></i>&nbsp;Dados do Operador
                                <small>Informações do operador responsável pelo Registro.</small>
                            </h2>
                        </asp:Panel>

                        <table class="table table-hover">
                             <tr>
                                <td>
                                    <h3><strong>Nome do Operador:</strong></h3>

                                    <asp:TextBox ID="txtNomeOperador" Enabled="false" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Login:</strong></h3>

                                    <asp:TextBox ID="txtLogin" Enabled="false" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                 <td>
                                    <h3><strong>Matrícula:</strong></h3>

                                    <asp:TextBox ID="txtMatricula" Enabled="false" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="300px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </table>

                        <!-- Perfil -->
                        <%--<asp:Panel ID="panel1" Width="100%" runat="server">
                            <h2 class="page-header"><i class="fa fa-phone"></i>&nbsp;Atendimento
                                <small>Informações do atendimento do registro não tabulado</small>
                            </h2>
                        </asp:Panel>--%>

                       <%-- <table class="table table-hover">
                            <!--  Primeira Linha -->
                            <tr>
                                <td>
                                    <h3><strong>Observação Não Tabulação:</strong></h3>
                                    <asp:TextBox ID="txtObs" CssClass="form-control" TextMode="MultiLine" Style="resize: none" Width="1510px" Font-Size="Medium" Rows="5" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>--%>

                    </div>

                    <div class="col-lg-12"></div>

                </div>
            </div>

            <!-- BOTÕES DE AÇÃO -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="text-right">
                        <asp:LinkButton ID="cmdCancelar" OnClick="cmdCancelar_Click" PostBackUrl="~/controle_naotabulados.aspx" runat="server" >
                            <button type="button" class="btn btn-info btn-md" style="width:12%" type="button"><i class="fa fa-arrow-left"></i>&nbsp;Voltar</button>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-lg-12">
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
</asp:Content>

