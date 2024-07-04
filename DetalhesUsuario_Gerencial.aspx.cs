using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// DetalhesUsuario_Gerencial
/// </summary>
public partial class DetalhesUsuario_Gerencial : System.Web.UI.Page
{

    public string sHtmlCompleto = "";

    // VARIAVEIS DE FILTRO
    string _sperfil = "";
    string _sequipe = "";
    string _susuario = "";
    string _sstatus = "";
    string _sdtini = "";
    string _sdtfim = "";

    // VARIAVEL DE INDEX DO STATUS PARA O SELECT
    string _sstatusbpm = "";
    
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                //seleciona as variaveis
                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {

                        case "_sperfil":
                            _sperfil = Request.QueryString[item].ToString();
                            break;

                        case "_sequipe":
                            _sequipe = Request.QueryString[item].ToString();
                            break;

                        case "_susuario":
                            _susuario = Request.QueryString[item].ToString();
                            break;

                        case "_sdtini":
                            _sdtini = Request.QueryString[item].ToString();
                            break;

                        case "_sdtfim":
                            _sdtfim = Request.QueryString[item].ToString();
                            break;

                        case "_sstatusbpm":
                            _sstatusbpm = Request.QueryString[item].ToString();
                            break;
                            
                    }
                }
                CarregaDetalhesUsuario(_sdtini,_sdtfim,_sperfil,_sequipe,_susuario);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// CarregaDetalhesUsuario
    /// </summary>
    /// <param name="sDataIni"></param>
    /// <param name="sDataFim"></param>
    /// <param name="sPerfil"></param>
    /// <param name="sEquipe"></param>
    /// <param name="sUsuario"></param>
    public void CarregaDetalhesUsuario(string sDataIni, string sDataFim, string sPerfil, string sEquipe, string sUsuario)
    {
        try
        {
            string sTable = ""; // VARIAVEL RESPOSAVEL POR MONTAR O TABLE
            string sCriptInitializeMapa = ""; // VARIAVEL RESPONSAVEL POR MONTAR O SCRIPT DE INICIALIZAÇÃO DO MAPA
            string sCriptMontaMapa = ""; // VARIAVEL RESPONSAVEL POR MONTAR O SCRIPT DE INICIALIZAÇÃO DO MAPA
            string sTyle = ""; // VARIAVEL RESPONSAVEL POR MONTAR O STYLE DOS MAPAS

            string sLat = ""; // VARIAVEL RESPOSAVEL POR RECEBER A LATITUDE
            string sLog = ""; // VARIAVEL RESPOSAVEL POR RECEBER A LONGITUDE
            int iCont = 1; // // VARIAVEL RESPOSAVEL POR COLOCAR OS INDICES DAS VARIAVEIS
            
            string ssql = @"select	a.Endereco,
		                            a.DataTempo,
		                            b.TipoTempo,
                                    a.latitude,
                                    a.longitude,
		                            'Usuario' = (select nome from TEL_MP_GATUsuario (nolock) where IDCodUsuario = a.IDCodUsuario)  
                            from	TEL_MP_GATTempoUsuario a (nolock),
		                            TEL_MP_GATTipoTempo b (nolock)
                            where	a.IDCodTipoTempo = b.IDCodTipoTempo";

            if (_sstatusbpm != "")
            {
                if (_sstatusbpm == "1")
                {
                    ssql = ssql + " and a.IDCodTipoTempo =" + _sstatusbpm;
                }else if (_sstatusbpm == "3")
                {
                    ssql = ssql + " and a.IDCodTipoTempo =" + _sstatusbpm;
                }
                else if (_sstatusbpm == "4")
                {
                    ssql = ssql + " and a.IDCodTipoTempo =" + _sstatusbpm;
                }
                else if (_sstatusbpm == "2")
                {
                    ssql = ssql + " and a.IDCodTipoTempo =" + _sstatusbpm;
                }
                else
                {
                    ssql = ssql + " and a.IDCodTipoTempo in (" + _sstatusbpm + ")";
                }
            }

            if (sPerfil != "")
            {
                ssql = ssql + " and	a.idcodperfil in (" + sPerfil+")";
            }

            if (sEquipe != "")
            {
                ssql = ssql + " and	a.IdCodEquipe in (" + sEquipe + ")";
            }

            if (sUsuario != "")
            {
                ssql = ssql + " and	a.IdCodusuario in (" + sUsuario + ")";
            }

            ssql = ssql + " and	a.datatempo between '" + sDataIni + "' and '" + sDataFim + "'";
            ssql = ssql + " order by a.DataTempo desc";


            gergatlink obj = new gergatlink();
            
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;

                    SqlDataReader dr = cmd.ExecuteReader();

                    
                    sTyle = "<style>";

                    while (dr.Read())
                    {

                        if (dr["latitude"].ToString() == "")
                        {
                            sLat = "0";
                        }
                        else
                        {
                            sLat = dr["latitude"].ToString();

                        }

                        if (dr["longitude"].ToString() == "")
                        {
                            sLog = "0";
                        }
                        else
                        {
                            sLog = dr["longitude"].ToString();
                        }

                        //////////////////////////
                        // DADOS ENCIMA DO MAPA//
                        /////////////////////////

                        sTable = sTable + " <div id = 'main-wrapper'>";
                         sTable = sTable + " <div id = 'headerwrapper' class='panel info-box panel-white'>";

                        sTable = sTable + " <div class='row'>&nbsp;</div>";
                        sTable = sTable + " <div class='col-lg-12'>";

                        sTable = sTable + " <table width='100%' cellpadding='0' cellspacing='0'>";

                            sTable = sTable + " <tr>";

                                // DATA
                                sTable = sTable + " <td>";
                                        sTable = sTable + "<b> Data: </b>" + dr["DATATEMPO"].ToString() + "";
                                sTable = sTable + " </td>";

                                // NOME OPERADOR
                                sTable = sTable + " <td>";
                                        sTable = sTable + " <b>Nome Vendedor: </b>" + dr["Usuario"].ToString() + "";
                                sTable = sTable + " </td>";

                            sTable = sTable + " </tr>";

                            sTable = sTable + " <tr>";

                                // ENDEREÇO
                                sTable = sTable + " <td>";
                                        sTable = sTable + " <b>Endereço: </b>" + dr["endereco"].ToString() + "";
                                sTable = sTable + " </td>";

                                // STATUS
                                sTable = sTable + " <td>";

                                        if (dr["TIPOTEMPO"].ToString() == "Login")
                                        {
                                            sTable = sTable + "<font size='3'> <span class='label label-sucess' style='color: White; background: Green;' ;><asp:Label>Status: " + dr["TIPOTEMPO"].ToString().ToUpper() + "</asp:Label ></span> </font>";
                                        }
                                        else if (dr["TIPOTEMPO"].ToString() == "Logout")
                                        {
                                            sTable = sTable + "<font size='3'> <span class='label label-default' style='color: White; background: Grey;' ><asp:Label>Status: " + dr["TIPOTEMPO"].ToString().ToUpper() + "</asp:Label ></span></font>";
                                        }
                                        else if (dr["TIPOTEMPO"].ToString() == "Check-In")
                                        {
                                            sTable = sTable + "<font size='3'>  <span class='label label-primary' style='color: white; background: blue; '><asp:Label>Status: " + dr["TIPOTEMPO"].ToString().ToUpper() + "</asp:Label ></span></font>";
                                        }
                                        else if (dr["TIPOTEMPO"].ToString() == "Check-Out")
                                        {
                                            sTable = sTable + "<font size='3'>  <span class='label label-danger'><asp:Label>Status: " + dr["TIPOTEMPO"].ToString().ToUpper() + "</asp:Label ></span></font>";
                                        }
                                        
                                sTable = sTable + " </td>";

                            sTable = sTable + " </tr>";

                        sTable = sTable + "</table>";
                        sTable = sTable + " <div class='row'>&nbsp;</div>";
                        sTable = sTable + " <table width='100%'>";
                        // MAPA
                        sTable = sTable + " <tr>";
                        sTable = sTable + " <td>";
                        sTable = sTable + " <div id='mapa" + iCont + "'></div>";
                        sTable = sTable + " <div class='row'>&nbsp;</div>";
                        sTable = sTable + " </td>";
                        sTable = sTable + " </tr>";
                        sTable = sTable + "</table>";

                        sTable = sTable + "</div>";



                        // CRIAR O STYLE DOS MAPAS
                        sTyle = sTyle + " #mapa" + iCont + " {";
                        sTyle = sTyle + " width: 1278;";
                        sTyle = sTyle + " height: 400px; ";
                        sTyle = sTyle + " border: 12px solid #ccc;";
                        sTyle = sTyle + " margin -bottom: 20px; ";
                        sTyle = sTyle + " }";

                        // CRIAS O SCRIPT DOS MAPAS COM SEUS MARKET
                        sCriptMontaMapa = sCriptMontaMapa + " var uluru" + iCont + " = { lat: " + sLat.ToString().Replace(",", ".") + ", lng: " + sLog.ToString().Replace(",", ".") + " };";
                        sCriptMontaMapa = sCriptMontaMapa + " var map" + iCont + " = new google.maps.Map(document.getElementById('mapa" + iCont + "'), { zoom: 15, center: uluru" + iCont + " }); ";
                        sCriptMontaMapa = sCriptMontaMapa + " var marker" + iCont + " = new google.maps.Marker({ position: uluru" + iCont + ", map: map" + iCont + ",title: 'NOME: " + dr["Usuario"].ToString() + " - LATITUDE: " + dr["Latitude"].ToString() + " - LONGITUDE: " + dr["Longitude"].ToString() + " - ENDEREÇO: " + dr["Endereco"].ToString() + " - STATUS: " + dr["TIPOTEMPO"].ToString() + "'});";

                        iCont++;


                        sTable = sTable + "</div>";
                        sTable = sTable + "</div>";

                    }

                   

                    sTyle = sTyle + " </style>";

                    // CRIAR SCRIPT DE INICIALIZAÇÃO DO MAPA
                    sCriptInitializeMapa = sCriptInitializeMapa + @"<script async defer src = 'https://maps.googleapis.com/maps/api/js?key=AIzaSyD64r3AtOT6uaGrLkQtyLUuy9ukWeFkiCs&callback=initMaps'></script>";

                    // MONTA O TABLE
                    ltrHistorico.Text = sTable + " " + sCriptInitializeMapa + " " + sTyle;

                    sHtmlCompleto = sHtmlCompleto + sCriptMontaMapa;

                }
            }



        }
        catch (Exception)
        {
            throw;
            //Erro("Erro " + err.Message);
        }
    }

}