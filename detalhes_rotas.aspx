<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detalhes_rotas.aspx.cs" Inherits="detalhes_rotas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Detalhes Rotas | STT GAT Link</title>

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <meta name="description" content="STT GAT Link" />
    <meta name="keywords" content="admin,dashboard,stt" />
    <meta name="author" content="leonam" />

    <link rel="shortcut icon" type="image/png" href="favicon.png" />

    <!-- Styles -->
    <!-- <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,300,600' rel='stylesheet' type='text/css' /> -->
    <link href="files/css" rel="stylesheet" type="text/css" />
    <link href="files/pace-theme-flash.css" rel="stylesheet" />
    <link href="files/uniform.default.min.css" rel="stylesheet" />
    <link href="files/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="./weather-icons-master/css/weather-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="./simple-line-icons-master/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="files/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="files/menu_cornerbox.css" rel="stylesheet" type="text/css" />
    <link href="files/waves.min.css" rel="stylesheet" type="text/css" />
    <link href="files/switchery.min.css" rel="stylesheet" type="text/css" />
    <link href="files/style.css" rel="stylesheet" type="text/css" />
    <link href="files/component.css" rel="stylesheet" type="text/css" />
    <link href="files/MetroJs.min.css" rel="stylesheet" type="text/css" />
    <link href="files/toastr.min.css" rel="stylesheet" type="text/css" />
    <link href="files/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="files/summernote.css" rel="stylesheet" type="text/css">
    <link href="files/datepicker3.css" rel="stylesheet" type="text/css">
    <link href="files/colorpicker.css" rel="stylesheet" type="text/css">
    <link href="files/bootstrap-tagsinput.css" rel="stylesheet" type="text/css">
    <link href="files/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css">

    <!-- Theme Styles -->
    <link href="files/modern.min.css" rel="stylesheet" type="text/css" />
    <link href="files/green.css" class="theme-color" rel="stylesheet" type="text/css" />
    <link href="files/custom.css" rel="stylesheet" type="text/css" />

    <script src="files/modernizr.js"></script>
    <script src="files/snap.svg-min.js"></script>
    <script type="text/javascript" src="js/canvasjs.min.js"></script>

    <style>
        body{
            background-color:white;
        }
    </style>

</head>


<body>

    <form id="frmdetalhes" runat="server">

        <!-- TITULO DA PAGINA -->
        <div class="page-title" style="background-color:#6B1846;">
            <h3 style=" color:white;">Detalhes das Rotas</h3>
        </div>

        <!-- ESPAÇO -->
        <div class="row">&nbsp;</div>

        <!-- corpo -->
        <div class="col-lg-12">

            <div class="panel panel-default">
                <div class="panel-body" style="border-radius:10px; box-shadow: 0px 0px 1em #BBC0C8; border-top:5px;">

                    <!--barra de botoes -->
                    <div class="col-lg-12 text-right">
                        <br />
                        <asp:Button ID="cmdexportar" runat="server" CssClass="btn btn-xs btn-success" Text="Exportar" OnClick="cmdexportar_Click" />
                    </div>

                    <!-- relatorio -->
                    <div class="col-lg-12">

                        <br />

                        <!-- grid -->
                        <asp:Panel ID="panel_grd_dados" Width="100%" Height="500px" ScrollBars="Auto" Visible="true" runat="server">

                            <asp:GridView ID="gdw_dados" CssClass="table table-striped table-bordered table-hover"
                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                HorizontalAlign="Left" Font-Names="Calibri"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
                                PageSize="300" OnRowDataBound="gdw_dados_RowDataBound"
                                OnPageIndexChanging="gdw_dados_PageIndexChanging">

                                <PagerSettings Position="TopAndBottom" PageButtonCount="25" Mode="NextPreviousFirstLast "
                                    PreviousPageText="<img src='imagens/anterior.png' border='0' title='Página Anterior'/>"
                                    NextPageText="<img src='imagens/proximo.png' border='0' title='Próxima Página'/>"
                                    FirstPageText="<img src='imagens/primeiro.png' border='0' title='Primeira Página'/>"
                                    LastPageText="<img src='imagens/ultimo.png' border='0' title='Última Página'/>" />

                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="img" ImageUrl="imagens/os.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    
                                    <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Rota" HeaderText="Rota" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Endereco" HeaderText="Endereço" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Latitude" HeaderText="Latitude" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Longitude" HeaderText="Longitude" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Acao" HeaderText="Ação Rota" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SubStatus" HeaderText="Sub-Status" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DataultStatus" HeaderText="Data" ItemStyle-HorizontalAlign="Left" />
                                    
   

                                </Columns>

                            </asp:GridView>


                        </asp:Panel>
                        <br />
                        <asp:Label ID="lbltotal" style="border-radius:10px;background-color:#6B1846; color:white;padding:3px;" runat="server" Text=""></asp:Label>

                    </div>

                </div>
            </div>
        </div>


    </form>

    <div class="cd-overlay"></div>

    <!-- Javascripts -->
    <script src="files/jquery-2.1.4.min.js"></script>
    <script src="files/jquery-ui.min.js"></script>
    <script src="files/pace.min.js"></script>
    <script src="files/jquery.blockui.js"></script>
    <script src="files/bootstrap.min.js"></script>
    <script src="files/jquery.slimscroll.min.js"></script>
    <script src="files/switchery.min.js"></script>
    <script src="files/jquery.uniform.min.js"></script>
    <script src="files/classie.js"></script>
    <script src="files/main.js"></script>
    <script src="files/waves.min.js"></script>
    <script src="files/main2.js"></script>

    <script src="files/jquery.waypoints.min.js"></script>
    <script src="files/jquery.counterup.js"></script>
    <script src="files/toastr.min.js"></script>
    <script src="files/jquery.flot.min.js"></script>
    <script src="files/jquery.flot.time.min.js"></script>
    <script src="files/jquery.flot.symbol.min.js"></script>
    <script src="files/jquery.flot.resize.min.js"></script>
    <script src="files/jquery.flot.tooltip.min.js"></script>
    <script src="files/curvedLines.js"></script>
    <script src="files/MetroJs.min.js"></script>
    <script src="files/select2.min.js"></script>

    <script src="files/summernote.min.js"></script>
    <script src="files/bootstrap-datepicker.js"></script>
    <script src="files/bootstrap-colorpicker.js"></script>
    <script src="files/bootstrap-tagsinput.min.js"></script>
    <script src="files/bootstrap-timepicker.min.js"></script>

    <script src="files/modern.js"></script>
    <script src="files/form-elements.js"></script>

    <script src="files/form-select2.js"></script>
    <script src="files/dashboard.js"></script>

    <div id="flotTip" style="display: none; position: absolute;"></div>

    <div class="colorpicker dropdown-menu">
        <div class="colorpicker-saturation" style="background-color: rgb(255, 0, 0);">
            <i style="left: 70.7547px; top: 16.8627px;"><b></b></i>
        </div>
        <div class="colorpicker-hue">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-alpha">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-color">
            <div style="background-color: rgb(212, 62, 62);"></div>
        </div>
    </div>
    <div class="colorpicker dropdown-menu alpha">
        <div class="colorpicker-saturation" style="background-color: rgb(0, 39, 255);">
            <i style="left: 66.6667px; top: 20px;"><b></b></i>
        </div>
        <div class="colorpicker-hue">
            <i style="top: 35.9069px;"></i>
        </div>
        <div class="colorpicker-alpha" style="background-color: rgb(68, 89, 204);">
            <i style="top: 0px;"></i>
        </div>
        <div class="colorpicker-color">
            <div style="background-color: rgb(68, 89, 204);"></div>
        </div>
    </div>

    <!-- mensagem de alerta -->
    <script>

        //mensagem de erro
            <% 
        string smsg = Session["mensagem_alerta"].ToString();


        if (Session["titulo_alerta"].ToString() != "")
        {%>
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        toastr["<%=Session["tipo_alerta"].ToString() %>"]("<%=Session["mensagem_alerta"].ToString() %>", "<%=Session["titulo_alerta"].ToString() %>");

        <%
            //zera os valores
            Session["titulo_alerta"] = "";
            Session["mensagem_alerta"] = "";
            Session["tipo_alerta"] = "";

        }
              %>


    </script>

</body>
</html>


