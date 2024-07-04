using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class detalhes : System.Web.UI.Page
{

    //campos filtro
    int itotdados = 0;

    //campos filtro
    string _scampanha = "";
    string _sCampTelefonia = "";
    string _sperfil = "";
    string _sequipe = "";
    string _susuario = "";
    string _sstatus = "";
    string _sdtini = "";
    string _sdtfim = "";

    //campos dinamicos
    string _sstatusbpm = "";
    string _sfila = "";
    string _shora = "";
    string _stipo_horahora = "";
    string _sidusuario_ranking = "";
    string _sidproduto_ranking = "";
    string _sidequipe_ranking = "";
    string _sidilha_ranking = "";
    string _sidmotivo_finalizacoes = "";
    string _sidgrupo_finalizacoes = "";
    string _sstatusregistro = "";
    string _slog = "";

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


            Rel_Detalhes(_sperfil, _susuario, _sdtini, _sdtfim);

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

                        case "_sperfil":
                            _sperfil = Request.QueryString[item].ToString();
                            break;

                        case "_sequipe":
                            _sequipe = Request.QueryString[item].ToString();
                            break;

                        case "_susuario":
                            _susuario = Request.QueryString[item].ToString();
                            break;

                        case "_sstatus":
                            _sstatus = Request.QueryString[item].ToString();
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

                        case "_sstatusregistro":
                            _sstatusregistro = Request.QueryString[item].ToString();
                            break;

                        case "_slog":
                            _slog = Request.QueryString[item].ToString();
                            break;
                            
                        case "_sfila":
                            _sfila = Request.QueryString[item].ToString();
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

            //loop cabeçalho
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
                    //verifica link do mapa
                    if (i == 1)
                    {
                        txt += "https://www.gatlink.com.br/adminapp/DetalhesMapaUsuario.aspx?_id=" + obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[3].Text).Replace("&nbsp;", "") + "&_User=" + obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[4].Text).Replace("&nbsp;", "") + ";";
                    }
                    //verifica link do hodometro
                    else if (i == 2)
                    {
                        //verifica se contem imagem
                        if (obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[i].Text).Replace("&nbsp;", "") == "")
                        {
                            txt += ";";
                        }
                        else
                        {
                            txt += "https://www.gatlink.com.br/adminapp/DetalhesMapaUsuario.aspx?_id=" + obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[3].Text).Replace("&nbsp;", "") + "&_User=" + obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[4].Text).Replace("&nbsp;", "") + ";";
                        }
                        
                    }
                    else
                    {
                        txt += obj.TrataHTMLASCII(gdw_dados.Rows[l].Cells[i].Text).Replace("&nbsp;", "") + ";";
                    }
                }

                //Add new line.
                txt += "\r\n";
            }

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes.txt");
            Response.AddHeader("content-disposition", "attachment;filename=stt_detalhes.csv");
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
    public void Rel_Detalhes(string sperfil, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    ssql = @"select	* from vw_Detalhes_Portal_GATLink_TEL_MP_atual a (nolock) where 1=1";
                    
                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodperfil in (" + sperfil + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                    }

                    //TIPOS DE TEMPO
                    if (_sstatusbpm != "")
                    {
                        //Logado
                        if (_sstatusbpm == "1")
                        {
                            ssql = ssql + " and a.CodStatusUser = 1";
                        }
                        //Check in
                        if (_sstatusbpm == "2")
                        {
                            ssql = ssql + " and a.CodStatusUser = 3";
                        }
                        //Check out
                        if (_sstatusbpm == "3")
                        {
                            ssql = ssql + " and a.CodStatusUser = 4";
                        }
                        //LOGOUT
                        if (_sstatusbpm == "4")
                        {
                            ssql = ssql + " and a.CodStatusUser = 2";
                        }

                        ssql = ssql + " and a.data between '" + dtini + "' and '" + dtfim + "'";
                    }


                    SqlDataAdapter da = new SqlDataAdapter(ssql, cn);

                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    Session["ds"] = ds;

                    gdw_dados.DataSource = ds;
                    gdw_dados.DataBind();

                    lbltotal.Text = "<b>&emsp; Total de Registros : " + itotdados.ToString() + "  &emsp;</b>";
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
                if (e.Row.Cells[2].Text == "&nbsp;")
                {
                    e.Row.Cells[2].Text = "";
                }

                itotdados++;
                
                e.Row.Cells[1].Text = "<a style='text-decoration:none; color:black;' href='DetalhesMapaUsuario.aspx?_id=" + e.Row.Cells[3].Text + "&_User=" + e.Row.Cells[4].Text + "' target='_blank'><img src='imagens/mapa.png'/></a>";
                e.Row.Cells[1].ToolTip = "Abrir Mapa com localização do Usuário";

                if (e.Row.Cells[2].Text != "")
                {
                    e.Row.Cells[2].Text = "<a style='text-decoration:none; color:black;' href='DetalhesHodometroUsuario.aspx?_id=" + e.Row.Cells[3].Text + "&_User=" + e.Row.Cells[4].Text + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "' target='_blank'><img src='imagens/Hodometro.png'/></a>";
                    e.Row.Cells[2].ToolTip = "Abrir Hodômetro do Usuário";
                }
                   
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