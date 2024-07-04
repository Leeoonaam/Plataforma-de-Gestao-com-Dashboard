<%@ Page Title="GATLink - Performance Base" Language="C#" MasterPageFile="~/MasterPageGATLink.master" AutoEventWireup="true" CodeFile="rel_performace_base.aspx.cs" Inherits="rel_performace_base" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!-- TITULO DA PAGINA -->
    <div class="page-title">
        <h3>Performance Base</h3>
        <div class="page-breadcrumb">
            <ol class="breadcrumb">
                <li><a href="dashboard.aspx">Home</a></li>
                <li class="active">Performance Base</li>
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

                        <!-- GRUPO -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Grupo Produto:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">

                                    <asp:DropDownList ID="cmbgrupoproduto" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="TODOS" Value=""></asp:ListItem>
                                        <asp:ListItem Text="SVAS" Value="65,34,35,36"></asp:ListItem>
                                        <asp:ListItem Text="VIVO FIXO" Value="60,6,32"></asp:ListItem>
                                        <asp:ListItem Text="VIVO INTERNET BOX" Value="31,59,58,64"></asp:ListItem>
                                        <asp:ListItem Text="VIVO INTERNET FIBRA" Value="66,42,38,61"></asp:ListItem>
                                        <asp:ListItem Text="VIVO INTERNET FIXA" Value="53,52,14"></asp:ListItem>
                                        <asp:ListItem Text="VIVO INTERNET MÓVEL" Value="30,57,56,63"></asp:ListItem>
                                        <asp:ListItem Text="VIVO TV" Value="29,55,54"></asp:ListItem>
                                        <asp:ListItem Text="VIVO TV FIBRA" Value="62,44,40,39"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                        </div>

                        
                        <!-- periodo -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Período:</label>
                            <div class="col-sm-10">
                                <div class="m-b-sm">
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


                        <!-- Agrupar -->
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Agrupar Por:</label>
                            <div class="col-sm-10">

                                <div class="form-inline">

                                    <div class="form-group">
                                        <asp:DropDownList ID="cmbcampobase" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="CAMPANHA" Value="IDCODCAMPANHA"></asp:ListItem>
                                            <asp:ListItem Text="ID_PLAY" Value="ID_PLAY"></asp:ListItem>
                                            <asp:ListItem Text="COD_CAMPANHA" Value="COD_CAMPANHA"></asp:ListItem>
                                            <asp:ListItem Text="EPS_NAME" Value="EPS_NAME"></asp:ListItem>
                                            <asp:ListItem Text="ID_PC_PRODUTO_COMERCIAL" Value="ID_PC_PRODUTO_COMERCIAL"></asp:ListItem>
                                            <asp:ListItem Text="CPF" Value="CPF"></asp:ListItem>
                                            <asp:ListItem Text="NOME " Value="NOME "></asp:ListItem>
                                            <asp:ListItem Text="LOGRADOURO " Value="LOGRADOURO "></asp:ListItem>
                                            <asp:ListItem Text="ENDERECO " Value="ENDERECO "></asp:ListItem>
                                            <asp:ListItem Text="NUMERO " Value="NUMERO "></asp:ListItem>
                                            <asp:ListItem Text="COMPLEMENTO " Value="COMPLEMENTO "></asp:ListItem>
                                            <asp:ListItem Text="BAIRRO " Value="BAIRRO "></asp:ListItem>
                                            <asp:ListItem Text="CEP " Value="CEP "></asp:ListItem>
                                            <asp:ListItem Text="CIDADE " Value="CIDADE "></asp:ListItem>
                                            <asp:ListItem Text="DDD1 " Value="DDD1 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE1 " Value="TELEFONE1 "></asp:ListItem>
                                            <asp:ListItem Text="DDD2 " Value="DDD2 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE2 " Value="TELEFONE2 "></asp:ListItem>
                                            <asp:ListItem Text="DDD3 " Value="DDD3 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE3 " Value="TELEFONE3 "></asp:ListItem>
                                            <asp:ListItem Text="MHC " Value="MHC "></asp:ListItem>
                                            <asp:ListItem Text="DATA_NASCIMENTO " Value="DATA_NASCIMENTO "></asp:ListItem>
                                            <asp:ListItem Text="TIPO_PESSOA " Value="TIPO_PESSOA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_FIXO " Value="POSSE_FIXO "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_BANDALARGA " Value="POSSE_BANDALARGA "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_BANDALARGA " Value="PROPENSAO_BANDALARGA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_TV " Value="POSSE_TV "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_TV_DTH " Value="PROPENSAO_TV_DTH "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_TV_FIBRA " Value="PROPENSAO_TV_FIBRA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_MOVEL" Value="POSSE_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="POSSE_SVA" Value="POSSE_SVA"></asp:ListItem>
                                            <asp:ListItem Text="VL_GLOBAL_CONTA" Value="VL_GLOBAL_CONTA"></asp:ListItem>
                                            <asp:ListItem Text="DS_CLASSE_LINHA" Value="DS_CLASSE_LINHA"></asp:ListItem>
                                            <asp:ListItem Text="DS_PLNOS_VOZ" Value="DS_PLNOS_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="DS_PLNO_LD" Value="DS_PLNO_LD"></asp:ListItem>
                                            <asp:ListItem Text="VLR_ASSINA_VOZ_3M" Value="VLR_ASSINA_VOZ_3M"></asp:ListItem>
                                            <asp:ListItem Text="TOTAL_MINUTO_MES" Value="TOTAL_MINUTO_MES"></asp:ListItem>
                                            <asp:ListItem Text="VL_CONTA_ATUAL_VOZ" Value="VL_CONTA_ATUAL_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA_LINHA" Value="DT_ALTA_LINHA"></asp:ListItem>
                                            <asp:ListItem Text="MIN_FRQ_LOC_FF" Value="MIN_FRQ_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="MIN_FRQ_LOC_FM" Value="MIN_FRQ_LOC_FM"></asp:ListItem>
                                            <asp:ListItem Text="MIN_EXC_LOC_FF" Value="MIN_EXC_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="MIN_EXC_LOC_FM" Value="MIN_EXC_LOC_FM"></asp:ListItem>
                                            <asp:ListItem Text="VL_ASSIN_PL_LOC_FF" Value="VL_ASSIN_PL_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="VL_TOTAL_EXC_ATUAL_C9" Value="VL_TOTAL_EXC_ATUAL_C9"></asp:ListItem>
                                            <asp:ListItem Text="NM_OFER_VOZ_RECOMENDA" Value="NM_OFER_VOZ_RECOMENDA"></asp:ListItem>
                                            <asp:ListItem Text="VLR_OFER__VOZ_RECOMENDA" Value="VLR_OFER__VOZ_RECOMENDA"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_VOZ" Value="VAR_CONTA_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_TOTAL" Value="VAR_CONTA_TOTAL"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA_SPEEDY" Value="DT_ALTA_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_MES_SPEEDY" Value="VL_ULT_MES_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_MES_SPEEDY_PROMO" Value="VL_ULT_MES_SPEEDY_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="FL_TIPO_BL" Value="FL_TIPO_BL"></asp:ListItem>
                                            <asp:ListItem Text="FL_AREA_CONCORRENCIA" Value="FL_AREA_CONCORRENCIA"></asp:ListItem>
                                            <asp:ListItem Text="FX_VELOC_DISP_MB" Value="FX_VELOC_DISP_MB"></asp:ListItem>
                                            <asp:ListItem Text="VL_VELOC_DISP_SPEEDY" Value="VL_VELOC_DISP_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_VELOC_DISP_SPEEDY_PROMO" Value="VL_VELOC_DISP_SPEEDY_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_BL" Value="VAR_CONTA_BL"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_BL_PROMO" Value="VAR_CONTA_BL_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA" Value="DT_ALTA"></asp:ListItem>
                                            <asp:ListItem Text="TECNOLOGIA_TV" Value="TECNOLOGIA_TV"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE" Value="VL_PACOTE"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE" Value="DS_PACOTE"></asp:ListItem>
                                            <asp:ListItem Text="QT_PT_SERVICO" Value="QT_PT_SERVICO"></asp:ListItem>
                                            <asp:ListItem Text="QT_CANAIS_ATUAL" Value="QT_CANAIS_ATUAL"></asp:ListItem>
                                            <asp:ListItem Text="PACOTE_TV_RECOMENDADO" Value="PACOTE_TV_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_TV" Value="VAR_CONTA_TV"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE_A01" Value="DS_PACOTE_A01"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE_A01" Value="VL_PACOTE_A01"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE_A_RECOMENDADO" Value="DS_PACOTE_A_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE_A_RECOMENDADO" Value="VL_PACOTE_A_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="PLNO_MOVEL_LOCAL" Value="PLNO_MOVEL_LOCAL"></asp:ListItem>
                                            <asp:ListItem Text="VL_PLNO_ATUAL" Value="VL_PLNO_ATUAL"></asp:ListItem>
                                            <asp:ListItem Text="SEG_MOVEL" Value="SEG_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_FAT_MOVEL" Value="VL_ULT_FAT_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="OFERTA_RECOMENDADA" Value="OFERTA_RECOMENDADA"></asp:ListItem>
                                            <asp:ListItem Text="VL_OFERTA_RECOMENDADA" Value="VL_OFERTA_RECOMENDADA"></asp:ListItem>
                                            <asp:ListItem Text="DET1" Value="DET1"></asp:ListItem>
                                            <asp:ListItem Text="DET2" Value="DET2"></asp:ListItem>
                                            <asp:ListItem Text="DET3" Value="DET3"></asp:ListItem>
                                            <asp:ListItem Text="DET4" Value="DET4"></asp:ListItem>
                                            <asp:ListItem Text="REGIONAL" Value="REGIONAL"></asp:ListItem>
                                            <asp:ListItem Text="IDADE" Value="IDADE"></asp:ListItem>

                                        </asp:DropDownList>

                                    </div>

                                    <div class="form-group">
                                        &nbsp;
                                        <img src="imagens/mais_16.png" />
                                        &nbsp;
                                    </div>

                                    <div class="form-group">

                                        <asp:DropDownList ID="cmbcampobase2" CssClass="form-control" runat="server">
                                            <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
                                            <asp:ListItem Text="CAMPANHA" Value="IDCODCAMPANHA"></asp:ListItem>
                                            <asp:ListItem Text="ID_PLAY" Value="ID_PLAY"></asp:ListItem>
                                            <asp:ListItem Text="COD_CAMPANHA" Value="COD_CAMPANHA"></asp:ListItem>
                                            <asp:ListItem Text="EPS_NAME" Value="EPS_NAME"></asp:ListItem>
                                            <asp:ListItem Text="ID_PC_PRODUTO_COMERCIAL" Value="ID_PC_PRODUTO_COMERCIAL"></asp:ListItem>
                                            <asp:ListItem Text="CPF" Value="CPF"></asp:ListItem>
                                            <asp:ListItem Text="NOME " Value="NOME "></asp:ListItem>
                                            <asp:ListItem Text="LOGRADOURO " Value="LOGRADOURO "></asp:ListItem>
                                            <asp:ListItem Text="ENDERECO " Value="ENDERECO "></asp:ListItem>
                                            <asp:ListItem Text="NUMERO " Value="NUMERO "></asp:ListItem>
                                            <asp:ListItem Text="COMPLEMENTO " Value="COMPLEMENTO "></asp:ListItem>
                                            <asp:ListItem Text="BAIRRO " Value="BAIRRO "></asp:ListItem>
                                            <asp:ListItem Text="CEP " Value="CEP "></asp:ListItem>
                                            <asp:ListItem Text="CIDADE " Value="CIDADE "></asp:ListItem>
                                            <asp:ListItem Text="DDD1 " Value="DDD1 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE1 " Value="TELEFONE1 "></asp:ListItem>
                                            <asp:ListItem Text="DDD2 " Value="DDD2 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE2 " Value="TELEFONE2 "></asp:ListItem>
                                            <asp:ListItem Text="DDD3 " Value="DDD3 "></asp:ListItem>
                                            <asp:ListItem Text="TELEFONE3 " Value="TELEFONE3 "></asp:ListItem>
                                            <asp:ListItem Text="MHC " Value="MHC "></asp:ListItem>
                                            <asp:ListItem Text="DATA_NASCIMENTO " Value="DATA_NASCIMENTO "></asp:ListItem>
                                            <asp:ListItem Text="TIPO_PESSOA " Value="TIPO_PESSOA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_FIXO " Value="POSSE_FIXO "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_BANDALARGA " Value="POSSE_BANDALARGA "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_BANDALARGA " Value="PROPENSAO_BANDALARGA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_TV " Value="POSSE_TV "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_TV_DTH " Value="PROPENSAO_TV_DTH "></asp:ListItem>
                                            <asp:ListItem Text="PROPENSAO_TV_FIBRA " Value="PROPENSAO_TV_FIBRA "></asp:ListItem>
                                            <asp:ListItem Text="POSSE_MOVEL" Value="POSSE_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="POSSE_SVA" Value="POSSE_SVA"></asp:ListItem>
                                            <asp:ListItem Text="VL_GLOBAL_CONTA" Value="VL_GLOBAL_CONTA"></asp:ListItem>
                                            <asp:ListItem Text="DS_CLASSE_LINHA" Value="DS_CLASSE_LINHA"></asp:ListItem>
                                            <asp:ListItem Text="DS_PLNOS_VOZ" Value="DS_PLNOS_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="DS_PLNO_LD" Value="DS_PLNO_LD"></asp:ListItem>
                                            <asp:ListItem Text="VLR_ASSINA_VOZ_3M" Value="VLR_ASSINA_VOZ_3M"></asp:ListItem>
                                            <asp:ListItem Text="TOTAL_MINUTO_MES" Value="TOTAL_MINUTO_MES"></asp:ListItem>
                                            <asp:ListItem Text="VL_CONTA_ATUAL_VOZ" Value="VL_CONTA_ATUAL_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA_LINHA" Value="DT_ALTA_LINHA"></asp:ListItem>
                                            <asp:ListItem Text="MIN_FRQ_LOC_FF" Value="MIN_FRQ_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="MIN_FRQ_LOC_FM" Value="MIN_FRQ_LOC_FM"></asp:ListItem>
                                            <asp:ListItem Text="MIN_EXC_LOC_FF" Value="MIN_EXC_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="MIN_EXC_LOC_FM" Value="MIN_EXC_LOC_FM"></asp:ListItem>
                                            <asp:ListItem Text="VL_ASSIN_PL_LOC_FF" Value="VL_ASSIN_PL_LOC_FF"></asp:ListItem>
                                            <asp:ListItem Text="VL_TOTAL_EXC_ATUAL_C9" Value="VL_TOTAL_EXC_ATUAL_C9"></asp:ListItem>
                                            <asp:ListItem Text="NM_OFER_VOZ_RECOMENDA" Value="NM_OFER_VOZ_RECOMENDA"></asp:ListItem>
                                            <asp:ListItem Text="VLR_OFER__VOZ_RECOMENDA" Value="VLR_OFER__VOZ_RECOMENDA"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_VOZ" Value="VAR_CONTA_VOZ"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_TOTAL" Value="VAR_CONTA_TOTAL"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA_SPEEDY" Value="DT_ALTA_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_MES_SPEEDY" Value="VL_ULT_MES_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_MES_SPEEDY_PROMO" Value="VL_ULT_MES_SPEEDY_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="FL_TIPO_BL" Value="FL_TIPO_BL"></asp:ListItem>
                                            <asp:ListItem Text="FL_AREA_CONCORRENCIA" Value="FL_AREA_CONCORRENCIA"></asp:ListItem>
                                            <asp:ListItem Text="FX_VELOC_DISP_MB" Value="FX_VELOC_DISP_MB"></asp:ListItem>
                                            <asp:ListItem Text="VL_VELOC_DISP_SPEEDY" Value="VL_VELOC_DISP_SPEEDY"></asp:ListItem>
                                            <asp:ListItem Text="VL_VELOC_DISP_SPEEDY_PROMO" Value="VL_VELOC_DISP_SPEEDY_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_BL" Value="VAR_CONTA_BL"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_BL_PROMO" Value="VAR_CONTA_BL_PROMO"></asp:ListItem>
                                            <asp:ListItem Text="DT_ALTA" Value="DT_ALTA"></asp:ListItem>
                                            <asp:ListItem Text="TECNOLOGIA_TV" Value="TECNOLOGIA_TV"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE" Value="VL_PACOTE"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE" Value="DS_PACOTE"></asp:ListItem>
                                            <asp:ListItem Text="QT_PT_SERVICO" Value="QT_PT_SERVICO"></asp:ListItem>
                                            <asp:ListItem Text="QT_CANAIS_ATUAL" Value="QT_CANAIS_ATUAL"></asp:ListItem>
                                            <asp:ListItem Text="PACOTE_TV_RECOMENDADO" Value="PACOTE_TV_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="VAR_CONTA_TV" Value="VAR_CONTA_TV"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE_A01" Value="DS_PACOTE_A01"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE_A01" Value="VL_PACOTE_A01"></asp:ListItem>
                                            <asp:ListItem Text="DS_PACOTE_A_RECOMENDADO" Value="DS_PACOTE_A_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="VL_PACOTE_A_RECOMENDADO" Value="VL_PACOTE_A_RECOMENDADO"></asp:ListItem>
                                            <asp:ListItem Text="PLNO_MOVEL_LOCAL" Value="PLNO_MOVEL_LOCAL"></asp:ListItem>
                                            <asp:ListItem Text="VL_PLNO_ATUAL" Value="VL_PLNO_ATUAL"></asp:ListItem>
                                            <asp:ListItem Text="SEG_MOVEL" Value="SEG_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="VL_ULT_FAT_MOVEL" Value="VL_ULT_FAT_MOVEL"></asp:ListItem>
                                            <asp:ListItem Text="OFERTA_RECOMENDADA" Value="OFERTA_RECOMENDADA"></asp:ListItem>
                                            <asp:ListItem Text="VL_OFERTA_RECOMENDADA" Value="VL_OFERTA_RECOMENDADA"></asp:ListItem>
                                            <asp:ListItem Text="DET1" Value="DET1"></asp:ListItem>
                                            <asp:ListItem Text="DET2" Value="DET2"></asp:ListItem>
                                            <asp:ListItem Text="DET3" Value="DET3"></asp:ListItem>
                                            <asp:ListItem Text="DET4" Value="DET4"></asp:ListItem>
                                            <asp:ListItem Text="REGIONAL" Value="REGIONAL"></asp:ListItem>
                                            <asp:ListItem Text="IDADE" Value="IDADE"></asp:ListItem>

                                        </asp:DropDownList>

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
                    <asp:Button ID="cmdexportar" runat="server" CssClass="btn btn-success" Text="Exportar" OnClick="cmdexportar_Click" />
                    <asp:Button ID="cmdatualizar" runat="server" CssClass="btn btn-primary" Text="Atualizar" OnClick="cmdatualizar_Click" />
                </div>
            </div>
        </div>

        <div class="row">&nbsp;</div>


        <!-- RELATORIO -->
        <div class="row">
            <div class="col-lg-12">


                <div class="panel panel-default">

                    <div class="panel-body">
                        <!-- grid -->
                        <asp:Panel ID="panel_dados" Width="100%" ScrollBars="Auto" runat="server">
                            <asp:GridView ID="gdw_dados_finalizacoes" CssClass="table table-striped table-hover"
                                runat="server" CellPadding="2" CellSpacing="2" ForeColor="#333333"
                                GridLines="None" ShowFooter="True" ShowHeaderWhenEmpty="True" AllowPaging="false"
                                HorizontalAlign="Left" Font-Names="Calibri"
                                Width="100%" AutoGenerateColumns="False" DataKeyNames="CAMPO"
                                PageSize="25" OnRowDataBound="gdw_dados_RowDataBound">

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

                                    <asp:BoundField DataField="CAMPO" HeaderText="Campo" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="CAMPO2" HeaderText="Campo 2" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_LIGACOES" HeaderText="Qtd.Ligações" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_ATENDIDAS" HeaderText="Qtd.Atendidas" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_CONTATOS" HeaderText="Qtd.Contatos" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_CONTATOS_EFET" HeaderText="Qtd.Cont.Efetivos" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_VENDA_CLIENTE" HeaderText="Qtd.Venda.Cliente" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_VENDA_PRODUTO" HeaderText="Qtd.Venda.Produto" ItemStyle-HorizontalAlign="Left" />

                                    <asp:BoundField DataField="P_ATEND_LIG" HeaderText="Atend x Lig" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="P_CONTATOS_LIG" HeaderText="Contatos x Lig" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="P_CONTATOS_EFET_LIG" HeaderText="Contatos.Efet x Lig" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="P_VENDA_LIG" HeaderText="Venda x Ligacoes" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="P_VENDA_CONTATOS_EFET" HeaderText="Venda x Cont.Efet" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="QTD_CLIENTE_VIRGEM" HeaderText="Qtd.Cont.Virgens" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="SPIN" HeaderText="Spin" ItemStyle-HorizontalAlign="Left" />

                                </Columns>

                            </asp:GridView>
                        </asp:Panel>

                    </div>
                </div>

            </div>
        </div>


    </div>


</asp:Content>

