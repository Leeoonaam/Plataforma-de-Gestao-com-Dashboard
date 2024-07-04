<%@ Page Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="whatsapp.aspx.cs" Inherits="whatsapp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .col_acao {
            width: 5%;
        }

        .alter:hover{
            background-color: #EDEDED;
        }



    </style>
    <script>

        function deleta(url) {


            var r = confirm("Deseja Realmente Desativar esse Registro?");

            if (r == true) {
                window.open(url, '_self');
            }
        }


        function acao(url) {


            var r = confirm("Deseja Realmente Realizar essa Ação ?");

            if (r == true) {
                window.open(url, '_self');
            }
        }

        function InsereMensagem() {

            if (document.getElementById("ContentPlaceHolder1_txtmensagem").value != "") {

                // Obtém a data/hora atual
                var data = new Date();
                var hora = data.getHours();
                var min = data.getMinutes();

                //monta hora
                var str_hora = hora + ':' + min;

                var elem = document.getElementById("mensagens");

                var str_mensagens = elem.innerHTML;

                //elem.innerHTML = "teste";
                elem.innerHTML = str_mensagens + "<div class='panel-body' style='position: relative;padding: 3px;border-bottom-left-radius: 0;border-bottom-right-radius: 0;'><div style='overflow:auto;height:auto;background-color:#056162;padding: 8px;padding-top: 5px;padding-bottom: 5px; border-radius: 10px;bottom:20px;float:right;'><span style='color:white'>" + document.getElementById("ContentPlaceHolder1_txtmensagem").value + "&nbsp;</span><br /><span style='color:white; font-size:12px' class='time-right'>" + str_hora + "</span></div></div>";

                document.getElementById("ContentPlaceHolder1_txtmensagem").value = "";
            }
        }
        

    </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>CHAT</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="vendas.aspx">Home</a></li>
                <li class="active">Chat -  Banco Inter</li>
            </ol>
        </div>
    </div>

    <!-- CORPO -->
    <div id="main-wrapper">

        <!-- DIV DA LINHA -->
        <div class="row">
            <!-- DIV NOMES -->
            <div class="col-lg-12">
                <div class="panel panel-default">

                    <div class="panel-body" style="height: 100%;">

                        <div class="col-lg-4" style="height: 100%; padding: 1px;">

                            <div style="background-color: #EDEDED; padding: 14px;">
                                <div class="info-box-icon">
                                    <i class="icon icon-book-open fa-1x"></i>
                                    <asp:Label Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Contatos</asp:Label>
                                </div>
                            </div>

                            <div class="col-lg-12" style="background-color: #F6F6F6; padding: 10px">
                                <div class="col-lg-10" style="padding-left: 0px">
                                    <asp:TextBox ID="txtfiltro" placeholder="Pesquisar contato" Style="border-radius: 10px;" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-2" style="padding-left: 0px; padding-right: 0px;">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Style="border-radius: 10px;" Width="100%" Text="Buscar" />
                                </div>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-lg-12" style="height: 500px; overflow-y: scroll;">

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label2" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Leonam Correia</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label3" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Sérgio Lourenço</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label4" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Allan Cavalcante</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label5" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Josué Rodrigues</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label6" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 1</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label7" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 2</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label8" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 3</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label9" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 4</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label10" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 5</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label11" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 6</asp:Label>
                                    </div>

                                    <div style="border-bottom: solid #808080; opacity: 0.1;"></div>

                                    <div class="alter" style="padding: 14px; cursor:pointer">
                                        <img src="files/avatar1.png" class="img-circle avatar" style="height: 20px" />
                                        <asp:Label ID="Label12" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Teste 7</asp:Label>
                                    </div>
                                        <p></p>
                                  </div>
                                </ContentTemplate>                            
                            </asp:UpdatePanel>

                            

                        </div>

                        <div class="col-lg-8" style="height: 100%; padding: 1px; border-left: 1px solid #d4d4d4;">

                            <div style="background-color: #EDEDED; padding: 14px; overflow-y: scroll; overflow-y: hidden">
                                <img src="files/avatar1.png" class="img-circle avatar" style="height: 39px" />
                                <asp:Label ID="lblcontato_mensagem" Font-Bold="true" Font-Size="13" runat="server">&nbsp;&nbsp;Leonam Correia</asp:Label>
                            </div>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>

                                    <div id="mensagens" onscroll="" style="padding: 10px; padding-top: 10px; width:100%; height:500px; background-image:url('files/fundo.jpg'); background-repeat:repeat; background-position:center; background-position-x:center;overflow:auto; ">

                                        <%--<div class='panel-body' style="position: relative;padding: 3px;border-bottom-left-radius: 0;border-bottom-right-radius: 0;">
                                            <div style="overflow:auto;height:auto;background-color:#056162;padding: 8px;padding-top: 5px;padding-bottom: 5px; border-radius: 10px;bottom:20px;float:right;">
                                                <span style="color:white">Olá, tudo bem?&nbsp;</span>
                                                <br />
                                                <span style="color:white; font-size:12px" class="time-right">18:00</span>
                                            </div>
                                        </div>

                                        <div class='panel-body' style="position: relative;padding: 3px;border-bottom-left-radius: 0;border-bottom-right-radius: 0;">
                                            <div style="overflow:auto;height:auto;background-color:#262D31;padding: 8px;padding-top: 5px;padding-bottom: 5px; border-radius: 10px;bottom:20px;float:left;">
                                                <span style="color:white">Oi, tudo sim e com você?&nbsp;</span>
                                                <br />
                                                <span style="color:white; font-size:12px" class="time-right">18:03</span>
                                            </div>
                                        </div>--%>



                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <div class="col-lg-12" style="background-color: #EDEDED; padding: 12px;">

                                <div class="col-lg-11" style="padding-left: 0px">
                                    <asp:TextBox ID="txtmensagem" placeholder="Digite uma mensagem" Style="border-radius: 10px;" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-lg-1" style="padding-left: 0px; padding-right: 0px;">
                                    <asp:Button ID="cmdenviar" runat="server" CssClass="btn btn-info" OnClientClick="InsereMensagem(); return false;" OnClick="cmdenviar_Click"  Style="border-radius: 10px;" Width="100%" Text="Enviar" />
                                </div>

                            </div>



                        </div>

                    </div>


                </div>
            </div>


        </div>

    </div>

</asp:Content>
