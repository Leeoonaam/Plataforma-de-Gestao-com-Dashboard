using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class detalhes_operador : System.Web.UI.Page
{
    //campos filtro
    int itotdados = 0;

    //campos filtro
    string _scampanha = "";
    string _sCampTelefonia = "";
    string _sperfil = "";
    string _sequipe = "";
    string _susuario = "";
    string _sproduto = "";
    string _sdtini = "";
    string _sdtfim = "";

    //campos dinamicos
    string _sstatusbpm = "";
    string _shora = "";
    string _stipo_horahora = "";
    string _sidusuario_ranking = "";
    string _sidproduto_ranking = "";
    string _sidequipe_ranking = "";
    string _sidilha_ranking = "";
    string _sidmotivo_finalizacoes = "";
    string _sidgrupo_finalizacoes = "";

    string _sidusuario_monitoramento = "";
    string _sidtipo_monitoramento = "";
    string _sstusuario_monitoramento = "";
    string _sidstatus_vendaprocess = "";

    string _sidequipeger = "";

    //Layout Novo
    double tSINGLE_VND = 0;
    double tDOUBLE_VND = 0;
    double tTRIPLE_VND = 0;
    double tTP_PLAY = 0;
    double tTV = 0;
    double tG3 = 0;
    double tCM = 0;
    double tCML = 0;
    double tCMDados = 0;
    double tCMVOZ = 0;
    double tRecusa = 0;

    //variaveis
    double tATENDIDAS = 0;
    double tREGISTRADAS = 0;
    double tREGISTRADASLEAD = 0;
    double tREGISTRADASGERAL = 0;
    double tNAOREGISTRADAS = 0;

    double tATENDIDAS_TOT = 0;
    double tREGISTRADAS_TOT = 0;
    double tREGISTRADASLEAD_TOT = 0;
    double tREGISTRADASGERAL_TOT = 0;
    double tNAOREGISTRADAS_TOT = 0;

    double tIMPRODUTIVO = 0;
    double tAGENDAMENTO = 0;
    double tRECLAMACAO = 0;
    double tPRODUTIVAS = 0;
    double tVENDAS = 0;
    double tFINALIZADO = 0;
    double tDCC = 0;
    double tBOLETO = 0;
    double dVALOR_VENDA = 0;
    double tIMPRODUTIVAS = 0;
    double tTELEFONIA = 0;
    double tRECUSAS = 0;
    string stotuserlog = "";


    /// <summary>
    /// Erro
    /// </summary>
    /// <param name="smsg"></param>
    void Erro(string smsg)
    {
        Session["tipo_alerta"] = "error";
        Session["titulo_alerta"] = "Erro";
        Session["mensagem_alerta"] = smsg;
    }


    /// <summary>
    /// AplicaFiltro
    /// </summary>
    public void AplicaFiltro()
    {
        try
        {
            //data padrao
            if (_sdtini == "")
            {
                _sdtini = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 00:00:00";
                _sdtfim = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59";
            }

            gdw_dados.Columns[12].Visible = true;
            Rel_Operadores(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            gdw_dados.Columns[12].Visible = false;
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
     
            itotdados = 0;
            if (IsPostBack == false)
            {

                //seleciona as variaveis
                foreach (string item in Request.QueryString)
                {
                    //valores
                    switch (item)
                    {
                        case "_scampanha":
                            _scampanha = Request.QueryString[item].ToString();
                            break;

                        case "_sperfil":
                            _sperfil = Request.QueryString[item].ToString();
                            break;

                        case "_sequipe":
                            _sequipe = Request.QueryString[item].ToString();
                            break;

                        case "_susuario":
                            _susuario = Request.QueryString[item].ToString();
                            break;

                        case "_sproduto":
                            _sproduto = Request.QueryString[item].ToString();
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

                        case "_shora":
                            _shora = Request.QueryString[item].ToString();
                            break;

                        case "_stipo_horahora":
                            _stipo_horahora = Request.QueryString[item].ToString();
                            break;

                        case "_sidusuario_ranking":
                            _sidusuario_ranking = Request.QueryString[item].ToString();
                            break;

                        case "_sidproduto_ranking":
                            _sidproduto_ranking = Request.QueryString[item].ToString();
                            break;

                        case "_sidequipe_ranking":
                            _sidequipe_ranking = Request.QueryString[item].ToString();
                            break;

                        case "_sidilha_ranking":
                            _sidilha_ranking = Request.QueryString[item].ToString();
                            break;

                        case "_sidmotivo_finalizacoes":
                            _sidmotivo_finalizacoes = Request.QueryString[item].ToString();
                            break;

                        case "_sidgrupo_finalizacoes":
                            _sidgrupo_finalizacoes = Request.QueryString[item].ToString();
                            break;

                        case "_sidusuario_monitoramento":
                            _sidusuario_monitoramento = Request.QueryString[item].ToString();
                            break;

                        case "_sidtipo_monitoramento":
                            _sidtipo_monitoramento = Request.QueryString[item].ToString();
                            break;

                        case "_sstusuario_monitoramento":
                            _sstusuario_monitoramento = Request.QueryString[item].ToString();
                            break;

                        case "_sidstatus_vendaprocess":
                            _sidstatus_vendaprocess = Request.QueryString[item].ToString();
                            break;

                        case "_sidequipeger":
                            _sidequipeger = Request.QueryString[item].ToString();
                            break;
                        case "_sCampTelefonia":
                            _sCampTelefonia = Request.QueryString[item].ToString();
                            break;


                    }
                }

                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdexportar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdexportar_Click(object sender, EventArgs e)
    {
        try
        {
            //Build the Text file data.
            string txt = string.Empty;


            foreach (TableCell cell in gdw_dados.HeaderRow.Cells)
            {
                //Add the Header row for Text file.
                txt += cell.Text.Replace("&nbsp;", "") + ";";
            }

            //Add new line.
            txt += "\r\n";

            foreach (GridViewRow row in gdw_dados.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    //Add the Data rows.
                    txt += cell.Text + ";";
                }

                //Add new line.
                txt += "\r\n";
            }

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes_operadores.txt");
            Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes_operadores.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(txt);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Rel_Operadores
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Operadores(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            sequipe = _sidequipeger;
            _sequipe = _sidequipeger;

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT	x.IDCodUsuario,
		                            X.OPERADOR,
		                            X.ATENDIDAS,
                                    X.REGISTRADAS,
                                    'perc_REGISTRADAS' = '',
                                    X.REGISTRADASLEAD,
                                    'perc_REGISTRADASLEAD' = '',
                                    X.REGISTRADASGERAL,
                                    'perc_REGISTRADASGERAL' = '',
                                    X.NAOREGISTRADAS,
                                    'perc_NAOREGISTRADAS' = '',
		                            X.TMO
		
                            FROM (
                            SELECT	B.idcodusuario,
		                            B.nome 'operador',
		                            (
			                            SELECT	COUNT(1)
			                            FROM	GATTelChamadas AA (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario";

                    

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }


                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @"  ) 'ATENDIDAS',
                    (
			                            SELECT	COUNT(1)
			                            FROM	GATTelChamadas AA (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " and aa.IDCodCliente NOT IN (select X.IDCodCliente FROM GATLogAtendNaoTabuladosNETSMS X (NOLOCK) WHERE 1 = 1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and X.DATA between '" + dtini + "' and '" + dtfim + "')";

                    ssql = ssql + @"  ) 'REGISTRADAS',
                                       
                                     (
			                            SELECT	COUNT(1)
			                            FROM	GATTelChamadas AA (NOLOCK),
                                                GATCliente GC (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario
                                        AND AA.IDCodCliente = GC.IDCodCliente
                                        AND ISNULL(GC.ABRIU_TELA_LEAD,0) = 1";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " and aa.IDCodCliente NOT IN (select X.IDCodCliente FROM GATLogAtendNaoTabuladosNETSMS X (NOLOCK) WHERE 1 = 1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and X.DATA between '" + dtini + "' and '" + dtfim + "')";

                    ssql = ssql + @"  ) 'REGISTRADASLEAD',

                                      (
			                            SELECT	COUNT(1)
			                            FROM	GATTelChamadas AA (NOLOCK),
                                                GATCliente GC (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario
                                        AND AA.IDCodCliente = GC.IDCodCliente
                                        AND ISNULL(GC.ABRIU_TELA_LEAD,0) = 0";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " and aa.IDCodCliente NOT IN (select X.IDCodCliente FROM GATLogAtendNaoTabuladosNETSMS X (NOLOCK) WHERE 1 = 1";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and X.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and X.DATA between '" + dtini + "' and '" + dtfim + "')";

                    ssql = ssql + @"  ) 'REGISTRADASGERAL',

		                            (
			                            SELECT	COUNT(1)
			                            FROM	GATLogAtendNaoTabuladosNETSMS AA (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and aa.data between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @"  ) 'NAOREGISTRADAS',

                    (
			                            SELECT	AVG(DATEDIFF(SECOND,AA.DATAINIATEND,AA.DATAGATCHAMADA))
			                            FROM	GATTelChamadas AA (NOLOCK)
			                            WHERE	AA.IDCodUsuario  = B.IDCodUsuario";

                   

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }


                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @"  ) 'TMO'
		
                            FROM	GATUsuario B (NOLOCK)
                            WHERE	B.IDCodUsuario IN (
	                            SELECT	A.IDCodUsuario 
	                            FROM	GATTelChamadas A (NOLOCK)
	                            WHERE	1 = 1";

                                //campanha
                                if (scampanha.Trim() != "")
                                {
                                    ssql = ssql + " and a.idcodcampanha in (" + scampanha + ")";
                                }

                                //perfil
                                if (sperfil.Trim() != "")
                                {
                                    ssql = ssql + " and a.idcodperfil in (" + sperfil + ")";
                                }

                                //equipe
                                if (sequipe.Trim() != "")
                                {
                                    ssql = ssql + " and a.idcodequipe in (" + sequipe + ")";
                                }

                                //equipe seleciona no grid
                                ssql = ssql + " and a.idcodequipe in (" + _sidequipeger + ")";
                    
                                //usuario
                                if (susuario.Trim() != "")
                                {
                                    ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                                }

                    

                                //data
                                ssql = ssql + " and a.datagatchamada between '" + dtini + "' and '" + dtfim + "'";


                    ssql = ssql + @"   )
                            )X
                            order by X.TMO desc";




                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados.DataSource = dr;
                    gdw_dados.DataBind();

                    dr.Close();

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_dados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            double dconv = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_oper"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_oper"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_oper"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //Atendidas
                tATENDIDAS = Convert.ToInt32(e.Row.Cells[2].Text);
                tATENDIDAS_TOT = tATENDIDAS_TOT + Convert.ToInt32(e.Row.Cells[2].Text);

                //Registradas
                tREGISTRADAS = Convert.ToInt32(e.Row.Cells[3].Text);
                tREGISTRADAS_TOT = tREGISTRADAS_TOT + Convert.ToInt32(e.Row.Cells[3].Text);

                //Registradas LEAD
                tREGISTRADASLEAD = Convert.ToInt32(e.Row.Cells[5].Text);
                tREGISTRADASLEAD_TOT = tREGISTRADASLEAD_TOT + Convert.ToInt32(e.Row.Cells[5].Text);

                //Registradas GERAL
                tREGISTRADASGERAL = Convert.ToInt32(e.Row.Cells[7].Text);
                tREGISTRADASGERAL_TOT = tREGISTRADASGERAL_TOT + Convert.ToInt32(e.Row.Cells[7].Text);

                //Não Registradas
                tNAOREGISTRADAS = Convert.ToInt32(e.Row.Cells[9].Text);
                tNAOREGISTRADAS_TOT = tNAOREGISTRADAS_TOT + Convert.ToInt32(e.Row.Cells[9].Text);

                if (Convert.ToInt32(e.Row.Cells[11].Text) <= 480) //6 min
                {
                    e.Row.Cells[11].CssClass = "btn-success-list";
                }

                if (Convert.ToInt32(e.Row.Cells[11].Text) > 480 && Convert.ToInt32(e.Row.Cells[11].Text) <= 520) //
                {
                    e.Row.Cells[11].CssClass = "btn-warning";
                }

                if (Convert.ToInt32(e.Row.Cells[11].Text) > 520)
                {
                    e.Row.Cells[11].CssClass = "btn-danger";
                }


                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_oper"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }

                //Add new line.
                Session["txt_oper"] += "\r\n";

                //link - detalhes

                //atendidas
                e.Row.Cells[2].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidopger=" + e.Row.Cells[12].Text + "&_sCampTelefonia=" + _sCampTelefonia + "&_stipo_horahora=atendidas' target='_blank'>" + e.Row.Cells[2].Text + "</a>";

                //REGISTRADAS
                e.Row.Cells[3].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidopger=" + e.Row.Cells[12].Text + "&_sCampTelefonia=" + _sCampTelefonia + "&_stipo_horahora=registradas' target='_blank'>" + e.Row.Cells[3].Text + "</a>";

                //REGISTRADAS LEAD
                e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidopger=" + e.Row.Cells[12].Text + "&_sCampTelefonia=" + _sCampTelefonia + "&_stipo_horahora=registradaslead' target='_blank'>" + e.Row.Cells[5].Text + "</a>";

                //REGISTRADAS GERAL
                e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidopger=" + e.Row.Cells[12].Text + "&_sCampTelefonia=" + _sCampTelefonia + "&_stipo_horahora=registradasgeral' target='_blank'>" + e.Row.Cells[7].Text + "</a>";

                //NAO REGISTRADAS
                e.Row.Cells[9].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidopger=" + e.Row.Cells[12].Text + "&_sCampTelefonia=" + _sCampTelefonia + "&_stipo_horahora=naoregistradas' target='_blank'>" + e.Row.Cells[9].Text + "</a>";

                //% REGISTRADAS
                dconv = 0;
                if (tATENDIDAS > 0)
                {
                    dconv = (tREGISTRADAS / tATENDIDAS) * 100;
                }
                e.Row.Cells[4].Text = "<font color='black'>" + dconv.ToString("0.00") + "</font>";

                //% REGISTRADAS LEAD
                dconv = 0;
                if (tREGISTRADAS > 0)
                {
                    dconv = (tREGISTRADASLEAD / tREGISTRADAS) * 100;
                }
                e.Row.Cells[6].Text = "<font color='black'>" + dconv.ToString("0.00") + "</font>";

                //% REGISTRADAS GERAL
                dconv = 0;
                if (tREGISTRADAS > 0)
                {
                    dconv = (tREGISTRADASGERAL / tREGISTRADAS) * 100;
                }
                e.Row.Cells[8].Text = "<font color='black'>" + dconv.ToString("0.00") + "</font>";

                //% NAO REGISTRADAS
                dconv = 0;
                if (tATENDIDAS > 0)
                {
                    dconv = (tNAOREGISTRADAS / tATENDIDAS) * 100;
                }
                e.Row.Cells[10].Text = "<font color='black'>" + dconv.ToString("0.00") + "</font>";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";

                //ATENDIDAS
                e.Row.Cells[2].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sCampTelefonia=" + _sCampTelefonia + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_stipo_horahora=atendidas' target='_blank'>" + "<font color='red'><strong>" + tATENDIDAS_TOT.ToString() + "</strong></font></a>";

                //REGISTRADAS
                e.Row.Cells[3].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sCampTelefonia=" + _sCampTelefonia + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_stipo_horahora=registradas' target='_blank'>" + "<font color='red'><strong>" + tREGISTRADAS_TOT.ToString() + "</strong></font></a>";

                //REGISTRADAS LEAD
                e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sCampTelefonia=" + _sCampTelefonia + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_stipo_horahora=registradaslead' target='_blank'>" + "<font color='red'><strong>" + tREGISTRADASLEAD_TOT.ToString() + "</strong></font></a>";

                //REGISTRADAS GERAL
                e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sCampTelefonia=" + _sCampTelefonia + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_stipo_horahora=registradasgeral' target='_blank'>" + "<font color='red'><strong>" + tREGISTRADASGERAL_TOT.ToString() + "</strong></font></a>";

                //NAO REGISTRADAS
                e.Row.Cells[9].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sCampTelefonia=" + _sCampTelefonia + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_stipo_horahora=naoregistradas' target='_blank'>" + "<font color='red'><strong>" + tNAOREGISTRADAS_TOT.ToString() + "</strong></font></a>";

                //% REGISTRADAS
                dconv = 0;
                if (tATENDIDAS_TOT > 0)
                {
                    dconv = (tREGISTRADAS_TOT / tATENDIDAS_TOT) * 100;
                }
                e.Row.Cells[4].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //% REGISTRADAS LEAD
                dconv = 0;
                if (tREGISTRADAS_TOT > 0)
                {
                    dconv = (tREGISTRADASLEAD_TOT / tREGISTRADAS_TOT) * 100;
                }
                e.Row.Cells[6].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //% REGISTRADAS GERAL
                dconv = 0;
                if (tREGISTRADAS_TOT > 0)
                {
                    dconv = (tREGISTRADASGERAL_TOT / tREGISTRADAS_TOT) * 100;
                }
                e.Row.Cells[8].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //% NAO REGISTRADAS
                dconv = 0;
                if (tATENDIDAS_TOT > 0)
                {
                    dconv = (tNAOREGISTRADAS_TOT / tATENDIDAS_TOT) * 100;
                }
                e.Row.Cells[10].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                Session["txt_oper"] += obj.TrataHTMLASCII(";Total") + ";";
                Session["txt_oper"] += obj.TrataHTMLASCII(tATENDIDAS.ToString()) + ";";
                
                Session["txt_oper"] += "\r\n";

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_dados_PageIndexChanging
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            itotdados = 0;
            gdw_dados.PageIndex = e.NewPageIndex;
            gdw_dados.DataSource = Session["ds"];
            gdw_dados.DataBind();

            lbltotal.Text = "<b>Total de Registros : " + itotdados.ToString() + "</b>";

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


}