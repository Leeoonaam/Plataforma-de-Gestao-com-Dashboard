<%@ Page Title="Gerencialmento de Usuário" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="ger_usuarios.aspx.cs" Inherits="ger_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

                    <!-- Título da Página -->
                    <asp:Panel ID="panel_cadastrar" Width="100%" Visible="true" runat="server">
                        <h2 class="page-header"><i class="fa fa-clipboard"></i>&nbsp;Cadastrar Usuário
                                <small>Cadastro de Usuários GAT Link</small>
                        </h2>
                    </asp:Panel>

                    <!-- titulo da pagina -->
                    <asp:Panel ID="panel_alterar" Width="100%" Visible="false" runat="server">
                        <h2 class="page-header"><i class="fa fa-clipboard"></i>&nbsp;Alterar Usuário
                                <small>Alteração de Usuários GAT Link</small>
                        </h2>
                    </asp:Panel>

                    <!-- Formulario -->
                    <div class="col-lg-12">
                        <table class="table table-hover">
                            <!-- DADOS DO USUARIO - Primeira Linha -->
                            <tr>
                                <td>
                                    <h3><strong>Nome:</strong></h3>
                                    <asp:HiddenField ID="txtidcodusuario" runat="server" />

                                    <asp:TextBox ID="txtNome_Usuario"  required="" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Apelido:</strong></h3>

                                    <asp:TextBox ID="txtApelido" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Turno:</strong></h3>

                                        <asp:DropDownList ID="cmbTurno" required=""  Style="width: 300px" Font-Size="Medium" CssClass="form-control" runat="server"></asp:DropDownList>
                                </td>
                            </tr>

                            <!-- Segunda Linha -->
                            <tr>

                                <td>
                                    <h3><strong>Login:</strong></h3>

                                    <asp:TextBox ID="txtCad_Login_Usuario" required=""  CssClass="form-control" Style="resize: none" Rows="5" Font-Size="Medium" runat="server" Width="360"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Senha:</strong></h3>

                                    <asp:TextBox ID="txtCad_Senha_Usuario"  required="" CssClass="form-control" Style="resize: none" Width="360" Font-Size="Medium" Rows="5" TextMode="Password" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Função:</strong></h3>

                                    <asp:DropDownList ID="cmbFuncao" required=""  Style="width: 300px" CssClass="form-control" Font-Size="Medium" runat="server"></asp:DropDownList>
                                </td>

                            </tr>

                            <!-- Terceria Linha -->
                            <tr>
                                <td>
                                    <h3><strong>RG:</strong></h3>

                                    <asp:TextBox ID="txtRG"  required="" CssClass="form-control" Style="resize: none" Width="250px" Font-Size="Medium" Rows="5" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>CPF:</strong></h3>

                                    <asp:TextBox ID="txtCPF" required=""  CssClass="form-control" Style="resize: none" Width="250" Rows="5" runat="server" Font-Size="Medium"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Cód. Vendedor:</strong></h3>

                                    <asp:TextBox ID="txtCodVendedor" required=""  CssClass="form-control" Style="resize: none" Width="250" Rows="5" Font-Size="Medium" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <!-- Quarta Linha -->
                            <tr>
                                <td>
                                    <h3><strong>Ramal:</strong></h3>

                                    <asp:TextBox ID="txtRamal" required=""  CssClass="form-control" Style="resize: none" Width="250" Rows="5" runat="server" Font-Size="Medium"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Data de Nascimento:</strong></h3>

                                    <asp:TextBox ID="txtDataNascimento" required=""  CssClass="form-control date-picker" Width="236" runat="server" Font-Size="Medium"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Sexo:</strong></h3>

                                    <asp:DropDownList ID="cmbSexo" required=""  Style="width: 300px" CssClass="form-control" Font-Size="Medium" runat="server">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                        <asp:ListItem Value="Feminino">Feminino</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <h3><strong>Tipo Login:</strong></h3>

                                    <asp:DropDownList ID="cmbTipoLogin" required=""  Style="width: 400px" CssClass="form-control" Font-Size="Medium" runat="server"></asp:DropDownList>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>

                        </table>

                        <!-- Perfil -->
                        <asp:Panel ID="panel2" Width="100%" runat="server">
                            <h2 class="page-header"><i class="fa fa-cogs"></i>&nbsp;Dados de Integração
                                <small>Informações necessárias para vinculação dos dados do usuário.</small>
                            </h2>
                        </asp:Panel>

                        <table class="table table-hover">
                             <tr>
                                <td>
                                    <h3><strong>Login NET Sales:</strong></h3>

                                    <asp:TextBox ID="txtLoginNETSales" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Senha NET Sales:</strong></h3>

                                    <asp:TextBox ID="txtSenhaNETSales" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <h3><strong>Login NET SMS:</strong></h3>

                                    <asp:TextBox ID="txtLoginNETSMS" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <h3><strong>Senha NET SMS:</strong></h3>

                                    <asp:TextBox ID="txtSenhaNETSMS" CssClass="form-control" Style="resize: none" Font-Size="Medium" Rows="5" Width="500px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                        </table>

                        <!-- Perfil -->
                        <asp:Panel ID="panel1" Width="100%" runat="server">
                            <h2 class="page-header"><i class="fa fa-lock"></i>&nbsp;Perfil
                                <small>Disponibiliza ao usuário as configurações de segurança, onde o mesmo poderá definir 3 níveis de acesso, 1-Permissão, 2-Equipe, 3-Produto que o usuário irá trabalhar.</small>
                            </h2>
                        </asp:Panel>

                        <table class="table table-hover">
                            <!--  Primeira Linha -->
                            <tr>
                                <td>
                                    <h3><strong>Permissão:</strong></h3>
                                    <asp:CheckBox ID="chkPermissao" AutoPostBack="true" runat="server" OnCheckedChanged="chkPermissao_CheckedChanged" />
                                    <asp:DropDownList ID="cmbPermissao" required="" Style="width: 355px" CssClass="form-control" Font-Size="Medium" runat="server"></asp:DropDownList>
                                </td>

                                <td>
                                    <h3><strong>Equipe:</strong></h3>
                                    <asp:CheckBox ID="chkEquipe" runat="server" OnCheckedChanged="chkEquipe_CheckedChanged" />
                                    <asp:DropDownList ID="cmbEquipe" AutoPostBack="true" required="" Style="width: 355px" CssClass="form-control" Font-Size="Medium" runat="server"></asp:DropDownList>
                                </td>

                                <td>
                                    <h3><strong>Produto:</strong></h3>
                                    <asp:CheckBox ID="chkProduto" AutoPostBack="true" runat="server" OnCheckedChanged="chkProduto_CheckedChanged" />
                                    <asp:DropDownList ID="cmbProduto" required="" Style="width: 355px" CssClass="form-control" Font-Size="Medium" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                    </div>

                    <!-- BOTÕES DE AÇÃO -->
                    <center>
                    <td>
                        <asp:LinkButton ID="cmdSalvar_Usuario" OnClick="cmdSalvar_Usuario_Click" runat="server" >
                            <button type="button" class="btn btn-info btn-md" style="width:10%" type="button"><i class="fa fa-floppy-o"></i>&nbsp;Salvar</button>
                        </asp:LinkButton>

                        <asp:LinkButton ID="cmdCancelar" OnClick="cmdCancelar_Click" PostBackUrl="~/controle_usuarios.aspx" runat="server" >
                            <button type="button" class="btn btn-info btn-md" style="width:10%" type="button"><i class="fa fa-times"></i>&nbsp;Cancelar</button>
                        </asp:LinkButton>
                    </td>
                        </center>
                    <div class="col-lg-12"></div>

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

