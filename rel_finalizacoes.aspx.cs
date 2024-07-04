using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_finalizacoes : System.Web.UI.Page
{
    //variaveis
    double tFinalizacoes = 0;
    int ttma = 0;
    int tma_geral = 0;
    double tfinal_geral = 0;

    double tFinalizacoes_grupo = 0;
    int ttma_grupo = 0;
    int tma_geral_grupo = 0;
    double tfinal_geral_grupo = 0;


    public string _scampanha = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";


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


    #region FILTRAR


    /// <summary>
    /// Carrega_Lista_Filtro_Campanhas
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Campanhas(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from gatcampanha (nolock) where idcodstatusop = 1 ";
                    if (svalor != "") sdml = sdml + " and campochave like '%" + svalor + "%'";
                    sdml = sdml + " order by campochave";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbcampanhas_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["campochave"].ToString();
                        oitem.Value = dr["idcodcampanha"].ToString();

                        cmbcampanhas_filtro.Items.Add(oitem);

                    }
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
    /// Carrega_Lista_Filtro_Perfil
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Perfil(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select  * from gatperfil (nolock) where idpermissao = 3 and habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and perfil like '%" + svalor + "%'";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbperfil_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbperfil_filtro.Items.Add(oitem);

                    }
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
    /// Carrega_Lista_Filtro_Equipe
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Equipe(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from gatperfil (nolock) where idpermissao = 10 and habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and perfil like '%" + svalor + "%'";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbequipe_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbequipe_filtro.Items.Add(oitem);

                    }
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
    /// Carrega_Lista_Filtro_Usuarios
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Usuarios(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select idcodusuario,nome from gatusuario (nolock) where habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and nome like '%" + svalor + "%'";
                    sdml = sdml + " order by nome asc, dataultstatus";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbusuarios_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["nome"].ToString();
                        oitem.Value = dr["idcodusuario"].ToString();

                        cmbusuarios_filtro.Items.Add(oitem);

                    }
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
    /// Carrega_Lista_Filtro_Produtos
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Produtos(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from GATPRODUTO (nolock) where habilitado = 1 ";
                    if (svalor != "") sdml = sdml + " and descricao_produto like '%" + svalor + "%'";
                    sdml = sdml + " order by descricao_produto";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbprodutos_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["descricao_produto"].ToString();
                        oitem.Value = dr["idcodproduto"].ToString();

                        cmbprodutos_filtro.Items.Add(oitem);

                    }
                    dr.Close();

                }

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #endregion



    /// <summary>
    /// AplicaFiltro
    /// </summary>
    public void AplicaFiltro()
    {
        try
        {
            //variaveis

            string svig = "";

            #region filtro

            //campanhas
            svig = "";
            foreach (ListItem obj in cmbcampanhas_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _scampanha = _scampanha + svig + obj.Value;
                    svig = ",";
                }
            }

            //perfil
            svig = "";
            foreach (ListItem obj in cmbperfil_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sperfil = _sperfil + svig + obj.Value;
                    svig = ",";
                }
            }

            //equipe
            svig = "";
            foreach (ListItem obj in cmbequipe_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sequipe = _sequipe + svig + obj.Value;
                    svig = ",";
                }
            }

            //usuarios
            svig = "";
            foreach (ListItem obj in cmbusuarios_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _susuario = _susuario + svig + obj.Value;
                    svig = ",";
                }
            }

            //produtos
            svig = "";
            foreach (ListItem obj in cmbprodutos_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sproduto = _sproduto + svig + obj.Value;
                    svig = ",";
                }
            }

            //data
            if (chkData_Filtro.Checked == true)
            {
                if (txtdataini.Text.Trim() == "")
                {
                    Erro("Informe a Data Inicial");
                    return;
                }

                if (txtdatafim.Text.Trim() == "")
                {
                    Erro("Informe a Data Final");
                    return;
                }


                //monta a data
                _sdtini = txtdataini.Text.Substring(6, 4) + "-" + txtdataini.Text.Substring(3, 2) + "-" + txtdataini.Text.Substring(0, 2);
                _sdtfim = txtdatafim.Text.Substring(6, 4) + "-" + txtdatafim.Text.Substring(3, 2) + "-" + txtdatafim.Text.Substring(0, 2);

                _sdtini_min = txtdataini.Text.Substring(6, 4) + "-" + txtdataini.Text.Substring(3, 2);
                _sdtfim_min = txtdatafim.Text.Substring(6, 4) + "-" + txtdatafim.Text.Substring(3, 2);


                //coloca a hora se for preenchida
                if (txthoraini.Text.Trim() != "")
                {
                    _sdtini = _sdtini + " " + txthoraini.Text + ":00";
                }
                else
                {
                    _sdtini = _sdtini + " 00:00:00";
                }

                if (txthorafim.Text.Trim() != "")
                {
                    _sdtfim = _sdtfim + " " + txthorafim.Text + ":00";
                }
                else
                {
                    _sdtfim = _sdtfim + " 23:59:59";
                }

                //valida se a data esta ok
                DateTime resultado = DateTime.Now;
                if (DateTime.TryParse(_sdtini, out resultado))
                {
                    if (DateTime.TryParse(_sdtfim, out resultado))
                    {
                        //ok
                    }
                    else
                    {
                        Erro("Data Final inválida");
                        return;
                    }
                }
                else
                {
                    Erro("Data Inicial inválida");
                    return;
                }
            }
            else
            {
                _sdtini = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 00:00:00";
                _sdtfim = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " 23:59:59";

                _sdtini_min = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                _sdtfim_min = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            }

            #endregion

            gdw_dados_finalizacoes.Columns[6].Visible = true;
            gdw_dados_finalizacoes.Columns[7].Visible = true;

            Rel_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);

            gdw_dados_finalizacoes.Columns[6].Visible = false;
            gdw_dados_finalizacoes.Columns[7].Visible = false;

            //gdw_dados_grupos.Columns[5].Visible = true;
            //gdw_dados_grupos.Columns[6].Visible = true;

            // Rel_Grupo_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);

            // gdw_dados_grupos.Columns[5].Visible = false;
            //gdw_dados_grupos.Columns[6].Visible = false;

            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            Grafico_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            //Grafico_Grupo_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);

            Literal1.Text = Literal1.Text + @"}
            </script>";

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }


    /// <summary>
    ///  Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (IsPostBack == false)
            {

                //FILTRO
                Carrega_Lista_Filtro_Campanhas("");
                Carrega_Lista_Filtro_Perfil("");
                Carrega_Lista_Filtro_Equipe("");
                Carrega_Lista_Filtro_Usuarios("");
                Carrega_Lista_Filtro_Produtos("");

                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


 

    /// <summary>
    /// Rel_Finalizacoes
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Finalizacoes(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"select	b.idcodmotivo,
		                            b.motivo,
		                            COUNT(1) 'Total',
		                            avg(datediff(second,a.datainiatend,a.datagatchamada)) 'TMA',
                                    (
                                        select  count(1)
                                        from    gattelchamadas (nolock)
                                        where  1=1";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and datagatchamada between '" + dtini + "' and '" + dtfim + "'";


                    ssql = ssql + @") 'total_geral',
                                    (
                                        select  avg(datediff(second,datainiatend,datagatchamada))
                                        from    gattelchamadas (nolock)
                                        where   1=1";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and datagatchamada between '" + dtini + "' and '" + dtfim + "'";


                    ssql = ssql + @") 'tma_total',
                                    'perc' = null
                                    
                            from	gattelchamadas a (nolock),
		                            gatmotivochamada b (nolock)
                            where	a.idcodmotivo = b.idcodmotivo 
                            ";


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

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                    }

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodproduto in (" + sproduto + ")";
                    }

                    //ssql = ssql + " and A.IDCodPerfil in(309,321,330,347,370,372,376)";

                    //data
                    ssql = ssql + " and A.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @" group by	 b.idcodmotivo,
			                            b.motivo
                            order by Total desc";


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados_finalizacoes.DataSource = dr;
                    gdw_dados_finalizacoes.DataBind();

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
        gergatlink obj = new gergatlink();

        try
        {
            double dconv = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_fin1"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_fin1"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_fin1"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ttma = ttma + Convert.ToInt32(e.Row.Cells[3].Text);
                tFinalizacoes = tFinalizacoes + Convert.ToInt32(e.Row.Cells[4].Text);

                //e.Row.Cells[3].Text = obj.horacheia(Convert.ToInt32(e.Row.Cells[3].Text));

                if (Convert.ToInt32(e.Row.Cells[3].Text) <= 480) //6 min
                {
                    e.Row.Cells[3].CssClass = "btn-success-list";
                }

                if (Convert.ToInt32(e.Row.Cells[3].Text) > 480 && Convert.ToInt32(e.Row.Cells[3].Text) <= 520) //
                {
                    e.Row.Cells[3].CssClass = "btn-warning";
                }

                if (Convert.ToInt32(e.Row.Cells[3].Text) > 520)
                {
                    e.Row.Cells[3].CssClass = "btn-danger";

                }

                tma_geral = Convert.ToInt32(e.Row.Cells[7].Text);
                tfinal_geral = Convert.ToDouble(e.Row.Cells[6].Text);

                dconv = (Convert.ToDouble(e.Row.Cells[4].Text) / Convert.ToDouble(e.Row.Cells[6].Text)) * 100;

                e.Row.Cells[5].Text = dconv.ToString("0.00");

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_fin1"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_fin1"] += "\r\n";

                //LINK - FILTRO
                e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidmotivo_finalizacoes=" + e.Row.Cells[1].Text + "' target='_blank'>" + e.Row.Cells[4].Text + "</a>";


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "<font color='red'><strong>Total</strong></font>";
                e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='detalhes.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidmotivo_finalizacoes=-99' target='_blank'>" + "<font color='red'><strong>" + tFinalizacoes.ToString() + "</strong></font></a>";

                e.Row.Cells[3].Text = tma_geral.ToString();

                if (Convert.ToInt32(e.Row.Cells[3].Text) <= 480) //6 min
                {
                    e.Row.Cells[3].CssClass = "btn-success-list";
                }

                if (Convert.ToInt32(e.Row.Cells[3].Text) > 480 && Convert.ToInt32(e.Row.Cells[3].Text) <= 520) //
                {
                    e.Row.Cells[3].CssClass = "btn-warning";
                }

                if (Convert.ToInt32(e.Row.Cells[3].Text) > 520)
                {
                    e.Row.Cells[3].CssClass = "btn-danger";

                }



                dconv = (tFinalizacoes / tfinal_geral) * 100;

                e.Row.Cells[5].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //Add new line.
                Session["txt_fin1"] += obj.TrataHTMLASCII(";Total") + ";";
                Session["txt_fin1"] += obj.TrataHTMLASCII(obj.horacheia(Convert.ToInt32(tma_geral))) + ";";
                Session["txt_fin1"] += obj.TrataHTMLASCII(tFinalizacoes.ToString()) + ";";
                Session["txt_fin1"] += obj.TrataHTMLASCII(dconv.ToString("0.00")) + ";";
                Session["txt_fin1"] += "\r\n";

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

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_finalizacoes.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_fin1"]);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// cmdatualizar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdatualizar_Click(object sender, EventArgs e)
    {
        try
        {
            AplicaFiltro();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Grafico_Finalizacoes
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Finalizacoes(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string ssql = @"select b.IDCODMOTIVO,
		                            b.MOTIVO,
		                            COUNT(1) 'Total'
                            from	GATTELCHAMADAS a (nolock),
		                            GATMOTIVOCHAMADA b (nolock)
                            where	a.IDCODMOTIVO = b.IDCODMOTIVO 
                            ";


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

                //usuario
                if (susuario.Trim() != "")
                {
                    ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                }

                //produto
                if (sproduto.Trim() != "")
                {
                    ssql = ssql + " and a.idcodproduto in (" + sproduto + ")";
                }

                ssql = ssql + " and A.IDCodPerfil in(309,321,330,347,370,372,376)";

                //data
                ssql = ssql + " and a.DATAGATCHAMADA between '" + dtini + "' and '" + dtfim + "'";

                ssql = ssql + @" group by	 b.IDCODMOTIVO,
			                            b.MOTIVO
                            order by Total desc";


                using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //total
                    double itotal = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        itotal = itotal + Convert.ToInt32(item["TOTAL"].ToString());
                    }


                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart1 = new CanvasJS.Chart('div_graf_finalizacoes',
	                        {
		                        title:{
			                        text: ''
		                        },
		                        legend: {
			                        maxWidth: 350,
			                        itemWidth: 120
		                        },
		                        data: [
		                        {
			                        
			                        type: 'pie',
			                        dataPoints: [";


                    double ivalor = 0;
                    double dperc = 0;
                    string[] arr;
                    string[] separators = { " " };
                    int itot = 0;

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        //arr = item["motivo"].ToString().Split(separators, StringSplitOptions.RemoveEmptyEntries);

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        dperc = (ivalor / itotal) * 100;

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["motivo"].ToString() + " [" + dperc.ToString("0") + "%]' }";
                        svig = ",";

                        itot++;
                        if (itot == 6) break;

                    }

                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
		                }
		                ]
	                });

	                chart1.render();";

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

   

    
 

}