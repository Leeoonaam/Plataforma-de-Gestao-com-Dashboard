using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_painellideranca : System.Web.UI.Page
{
    //variaveis
    public string _scampanha = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    string itotnvend = "";
    double itotlogado = 0;
    double zerados = 0;
    double up = 0;
    double assit = 0;
    double totsite = 0;
    public string sSupervisor = "";

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
    /// 
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

            Rel_SupervisorSite(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            //Rel_MetaSite(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);


        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Rel_VisaoSite
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_SupervisorSite(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            DataRow orow;

            //cria a estrutura da tabela
            DataTable otable = new DataTable("GATVENDA");
            otable.Columns.Add("SITE");
            otable.Columns.Add("ZERADOS");
            otable.Columns.Add("UPGRADE");
            otable.Columns.Add("NETASSIST");
            otable.Columns.Add("TOTAL");

            Session["Idcodusuario"] = "33";

            ssql = "";
            //Captura os sites
            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT DISTINCT(IDCODSUPERVISOR)FROM GATTelChamadas(NOLOCK)WHERE 1=1";

                    //data
                    ssql = ssql + " and datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        sSupervisor = dr[0].ToString();


                        string ssql2 = "";
                        //Captura total de usuarios que realizou venda/logado
                        using (SqlConnection cn2 = ogat.abre_cn())
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {

                                //monta select
                                ssql2 = @"SELECT COUNT(DISTINCT IDCodUsuario) FROM GATTelChamadas(NOLOCK)
                                        WHERE   IDCodUsuario IN (SELECT	IDCodUsuario FROM GATVenda(NOLOCK)
						                                        WHERE	IDCODSUPERVISOR = " + sSupervisor;

                                //data
                                ssql2 = ssql2 + " and datavenda between '" + dtini + "' and '" + dtfim + "'";

                                ssql2 = ssql2 + @")
                                        AND IDCODSUPERVISOR = '" + sSupervisor + "'";

                                //data
                                ssql2 = ssql2 + " and datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                                cmd2.Connection = cn2;
                                cmd2.CommandText = ssql2;
                                cmd2.CommandType = CommandType.Text;

                                SqlDataReader dr2 = cmd2.ExecuteReader();

                                if (dr2.Read())
                                {
                                    itotlogado = Convert.ToInt32(dr2[0].ToString());
                                }
                                dr2.Close();

                            }
                        }


                        ssql2 = "";
                        //Captura total de usuarios que não realizou venda
                        using (SqlConnection cn2 = ogat.abre_cn())
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {

                                //monta select
                                ssql2 = @"SELECT    COUNT(DISTINCT IDCodUsuario)'IDCODUSUARIO' FROM GATTelChamadas(NOLOCK)
                                            WHERE	IDCODSUPERVISOR = " + sSupervisor + @"
                                            AND		IDCodUsuario NOT IN (SELECT IDCodUsuario FROM GATVenda(NOLOCK))";     

                                cmd2.Connection = cn2;
                                cmd2.CommandText = ssql2;
                                cmd2.CommandType = CommandType.Text;

                                SqlDataReader dr2 = cmd2.ExecuteReader();

                                if (dr2.Read())
                                {
                                    itotnvend = dr2[0].ToString();
                                }
                                dr2.Close();

                            }
                        }


                        ssql2 = "";
                        using (SqlConnection cn2 = ogat.abre_cn())
                        {
                            using (SqlCommand cmd2 = new SqlCommand())
                            {

                                //monta select
                                ssql2 = @"SELECT	
                                        
                                        C.Nome'SITE',
		                                'ZERADOS'='0',
		                                'UPGRADE' = (SELECT COUNT(1)FROM GATVenda X(NOLOCK)WHERE X.IDCODSUPERVISOR = A.IDCODSUPERVISOR AND X.IDCODPRODUTO = 1159 ";

                                //data
                                ssql2 = ssql2 + " and datavenda between '" + dtini + "' and '" + dtfim + "'";

                                ssql2 = ssql2 + @"),

		                                'NETASSIST' = (SELECT COUNT(1)FROM GATVenda X(NOLOCK)WHERE X.IDCODSUPERVISOR = A.IDCODSUPERVISOR AND X.IDCODPRODUTO = 1158 ";

                                //data
                                ssql2 = ssql2 + " and datavenda between '" + dtini + "' and '" + dtfim + "'";

                                ssql2 = ssql2 + @"),

		                                COUNT(1)'TOTAL'

                                FROM	GATVenda A (NOLOCK),
		                                GATProduto B(NOLOCK),
                                        GATUsuario C(NOLOCK)
		 
                                WHERE	A.IDCODPRODUTO = B.IDCodProduto
                                AND		A.IDCODSUPERVISOR = C.IDCodUsuario
                                AND     A.IDCODSUPERVISOR = '" + sSupervisor + "'";

                                #region FILTRO
                                //campanha
                                if (scampanha.Trim() != "")
                                {
                                    ssql2 = ssql2 + " and idcodcampanha in (" + scampanha + ")";
                                }

                                //perfil
                                if (sperfil.Trim() != "")
                                {
                                    ssql2 = ssql2 + " and idcodperfil in (" + sperfil + ")";
                                }

                                //equipe
                                if (sequipe.Trim() != "")
                                {
                                    ssql2 = ssql2 + " and idcodequipe in (" + sequipe + ")";
                                }

                                //usuario
                                if (susuario.Trim() != "")
                                {
                                    ssql2 = ssql2 + " and idcodusuario in (" + susuario + ")";
                                }

                                //produto
                                if (sproduto.Trim() != "")
                                {
                                    ssql2 = ssql2 + " and idcodproduto in (" + sproduto + ")";
                                }

                                //data
                                ssql2 = ssql2 + " and a.datavenda between '" + dtini + "' and '" + dtfim + "'";
                                #endregion

                                ssql2 = ssql2 + " GROUP BY A.IDCODSUPERVISOR,C.Nome";

                                cmd.Connection = cn2;
                                cmd.CommandText = ssql2;
                                cmd.CommandType = CommandType.Text;

                                SqlDataReader dr2 = cmd.ExecuteReader();

                                while (dr2.Read())
                                {
                                    //insere informacao inicial
                                    orow = otable.NewRow();

                                    orow["SITE"] = dr2["SITE"].ToString();

                                    if (itotnvend != "0")
                                    {
                                        zerados = (Convert.ToDouble(itotlogado) / Convert.ToInt32(itotnvend)) * 100;
                                    }
                                    else
                                    {
                                        zerados = 0;
                                    }

                                    orow["ZERADOS"] = Convert.ToInt32(zerados).ToString("0.00") + "%";
                                    orow["UPGRADE"] = dr2["UPGRADE"].ToString();
                                    orow["NETASSIST"] = dr2["NETASSIST"].ToString();
                                    orow["TOTAL"] = dr2["TOTAL"].ToString();

                                    otable.Rows.Add(orow);
                                }

                                dr2.Close();

                            }
                        }

                    }

                    //carrea o grid
                    gdw_dados.DataSource = otable;
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
    /// 
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
                Session["txt_supervisorsite"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_supervisorsite"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_supervisorsite"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                up = up + Convert.ToInt32(e.Row.Cells[3].Text);
                assit = assit + Convert.ToInt32(e.Row.Cells[4].Text);
                totsite = totsite + Convert.ToInt32(e.Row.Cells[5].Text);

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_supervisorsite"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_supervisorsite"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";
                e.Row.Cells[2].Text = "<font color='red'><strong>" + zerados.ToString("0.00") + "%</strong></font>";
                e.Row.Cells[3].Text = "<font color='red'><strong>" + up.ToString() + "</strong></font>";
                e.Row.Cells[4].Text = "<font color='red'><strong>" + assit.ToString() + "</strong></font>";
                e.Row.Cells[5].Text = "<font color='red'><strong>" + totsite.ToString() + "</strong></font>";

                //Add new line.
                Session["txt_supervisorsite"] += obj.TrataHTMLASCII(";Total") + ";";
                Session["txt_supervisorsite"] += "\r\n";

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
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
    /// 
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
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_vendas_visaosite.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_visaosite"]);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_metasite_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}