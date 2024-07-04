<%@ Page Title="NOC | STT - GAT Link" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="noc.aspx.cs" Inherits="noc" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <!-- auto refresh -->
    <%--<asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000"></asp:Timer>--%>

    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>NOC - Núcleo Operacional de Controle</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">NOC</li>
            </ol>
        </div>
    </div>


    <!-- CORPO -->
    <div id="main-wrapper">

        <table width="30%">
            <tr>
                <td>
                    <h3>
                        <span class="label label-danger">
                            <asp:Label ID="lblNomeUsuario" runat="server" Text=""></asp:Label>
                        </span>
                    </h3>
                </td>
            </tr>
        </table>

        <!-- ESPACO -->
        <div class="row">&nbsp;</div>

        <!-- RELATORIOS -->
        <div class="row">
            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-body">

                        <div id="mapa">
                            <script async defer
                                src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCcPuAQ7HlOZbFjefs379aUWWL9Af0pndo&callback=initMap">
                            </script>
                        </div>

                    </div>
                </div>

            </div>
        </div>

    </div>

    <%-- FAZ O ESTILO DO MAPA --%>
    <style>
        #mapa {
            width: 860px;
            height: 400px;
            border: 10px solid #ccc;
            margin-bottom: 20px;
        }
    </style>

    <%-- JAVASCRIPT --%>

    <script>

        // inicializa o mapa
        function initMap() {

            //var uluru1 = { lat: -23.686, lng: -46.619 };

            //var map1 = new google.maps.Map(document.getElementById('mapa'), { zoom: 8, center: uluru1 });

            <%Response.Write(sPlace_Map);%>

        }

    </script>

</asp:Content>



