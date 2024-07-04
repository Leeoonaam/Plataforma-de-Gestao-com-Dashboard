using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class detalhes_rpa : System.Web.UI.Page
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

    string _sidopger = "";
    string _sidstatusnetsms = "";
    string _sdetalhenetsms = "";


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

            //Oculta colunas sem informações do cancelamento outros canais
            if (_sstatusbpm == "-2")
            {
                gdw_dados.Columns[2].Visible = false;
                gdw_dados.Columns[4].Visible = false;
                gdw_dados.Columns[5].Visible = false;
                gdw_dados.Columns[6].Visible = false;
                gdw_dados.Columns[7].Visible = false;
                gdw_dados.Columns[8].Visible = false;
                gdw_dados.Columns[13].Visible = false;

            }

            Rel_Detalhes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim, _sCampTelefonia);

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

                        case "_sidopger":
                            _sidopger = Request.QueryString[item].ToString();
                            break;

                        case "_sCampTelefonia":
                            _sCampTelefonia = Request.QueryString[item].ToString();
                            break;

                        case "_sidstatusnetsms":
                            _sidstatusnetsms = Request.QueryString[item].ToString();
                            break;

                        case "_sdetalhenetsms":
                            _sdetalhenetsms = Request.QueryString[item].ToString();
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
            gergatlink obj = new gergatlink();
            //Build the Text file data.
            string txt = string.Empty;

            //foreach (TableCell cell in gdw_dados.HeaderRow.Cells)
            //{
            //    //Add the Header row for Text file.
            //    txt += cell.Text.Replace("&nbsp;", "") + ";";
            //}

            ////Add new line.
            //txt += "\r\n";

            //foreach (GridViewRow row in gdw_dados.Rows)
            //{
            //    foreach (TableCell cell in row.Cells)
            //    {
            //        //Add the Data rows.
            //        txt += cell.Text + ";";
            //    }

            //    //Add new line.
            //    txt += "\r\n";
            //}



            for (int i = 1; i < gdw_dados.Columns.Count - 1; i++)
            {
                //Add the Header row for Text file.
                txt += obj.TrataHTMLASCII(gdw_dados.Columns[i].HeaderText).Replace("&nbsp;", "") + ";";

            }

            //Add new line.
            txt += "\r\n";

            for (int l = 0; l < gdw_dados.Rows.Count; l++)
            {
                for (int i = 1; i < gdw_dados.Columns.Count - 1; i++)
                {
                    //Add the Data rows.
                    txt += obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[i].Text).Replace("&nbsp;", "") + ";";
                }

                //Add new line.
                txt += "\r\n";
            }

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes.txt");
            Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes_rpa.csv");
            Response.Charset = "UTF-8";
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
    /// Rel_Detalhes
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Detalhes(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim, string scampanhaTelefonia)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    if (_sstatusbpm == "10")
                    {
                        ssql = @"SELECT	* FROM vwDetalhesRPA_NETSMS a (nolock) where 1=1 "; //Tabulação
                    }
                    else
                    {
                        if (_sstatusbpm == "-2") 
                        {
                            ssql = @"SELECT	* FROM vwDetalhesRPA_NETSMS_Cancelamento2 a (nolock) where 1=1 "; //Cancelamento outros canais
                        }
                        else
                        {
                            ssql = @"SELECT	* FROM vwDetalhesRPA_NETSMS_N1 a (nolock) where 1=1 "; //Cancelamento
                        }
                    }
                    
                    //Novo
                    if (_sidstatusnetsms == "4")
                    {
                        ssql = ssql + " and a.idcodstatus_rpa_cancelamento = 4";
                    }

                    //Em processamento
                    if (_sidstatusnetsms == "1")
                    {
                        ssql = ssql + " and a.idcodstatus_rpa_cancelamento = 1";
                    }

                    //Processado com sucesso
                    if (_sidstatusnetsms == "2")
                    {
                        ssql = ssql + " and a.idcodstatus_rpa_cancelamento = 2";
                    }

                    //Erro e Indevido
                    if (_sidstatusnetsms == "3")
                    {
                        ssql = ssql + " and a.idcodstatus_rpa_cancelamento in (3)";
                    }

                    //Indevido - erro de contrato
                    if (_sidstatusnetsms == "6")
                    {
                        ssql = ssql + " and a.idcodstatus_rpa_cancelamento in (6)";
                    }

                    //total de registro 
                    if (_sidstatusnetsms == "0,4")
                    {
                        ssql = ssql + " and a.IDCODSTATUSRPA_NETSMS in (0,4)"; 
                    }

                    //Em processamento
                    if (_sidstatusnetsms == "11")
                    {
                        ssql = ssql + " and a.IDCODSTATUSRPA_NETSMS in (1)";
                    }

                    //Processado
                    if (_sidstatusnetsms == "12")
                    {
                        ssql = ssql + " and a.IDCODSTATUSRPA_NETSMS in (2)";
                        ssql = ssql + " and a.data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }

                    //Erro
                    if (_sidstatusnetsms == "13")
                    {
                        ssql = ssql + " and a.IDCODSTATUSRPA_NETSMS in (3)";
                    }

                    //Descricao
                    if (_sidstatusnetsms == "14")
                    {
                        ssql = ssql + " and a.obs_rpa_cancelamento = '" + _sdetalhenetsms + "'";
                        ssql = ssql + " and a.data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }

                    if (_sdetalhenetsms !="" && _sstatusbpm != "10")
                    {
                        ssql = ssql + " and a.obs_rpa_cancelamento = '" + _sdetalhenetsms + "'";
                    }

                    //Data
                    //ssql = ssql + " and a.data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ORDER BY a.data_rpa_cancelamento ";
                    
                    
                    SqlDataAdapter da = new SqlDataAdapter(ssql, cn);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //cmd.Connection = cn;
                    //cmd.CommandText = ssql;
                    //cmd.CommandType = CommandType.Text;

                    //SqlDataReader dr = cmd.ExecuteReader();

                    Session["ds"] = ds;

                    gdw_dados.DataSource = ds;
                    gdw_dados.DataBind();

                    lbltotal.Text = "<b>Total de Registros : " + itotdados.ToString() + "</b>";

                    //dr.Close();

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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                itotdados++;

               
                e.Row.Cells[14].Text = obj.horacheia(Convert.ToInt32(e.Row.Cells[14].Text));

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