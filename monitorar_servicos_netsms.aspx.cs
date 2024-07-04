using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class monitorar_servicos_netsms : System.Web.UI.Page
{
    //var
    public string _scampanha = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public string _scampanhabko = "";
    public string _socorrencia = "";
    public string _scontrato = "";

    int itot = 0;


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

            //variaveis
            string svig = "";

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

            Rel_Servicos(_sdtini, _sdtfim);

            RelTMT(_sperfil, _sdtini, _sdtfim);
            Rel_TotalRegistros(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            Rel_TotalEmProcess(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            Rel_TotalErrocontrato(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            Rel_TotalProcessado(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);
            Rel_TotalErroProcessado(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);

            gdw_dados_operadores.Columns[3].Visible = true;
            RelDetalhes(_sperfil, _sdtini, _sdtfim);
            gdw_dados_operadores.Columns[3].Visible = false;

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

                    sdml = "select idcodperfil,perfil from gatperfil (nolock) where IDPermissao = 3 and Habilitado = 1";
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
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Session["desative_sessao"] = "1";

            if (IsPostBack == false)
            {

                //FILTRO
                Carrega_Lista_Filtro_Perfil("");

                AplicaFiltro();

            }

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Timer1_Tick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AplicaFiltro();
    }

    #region RELATORIO
    /// <summary>
    ///  Rel_Servicos
    /// </summary>
    public void Rel_Servicos(string dtini, string dtfim)
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
                    ssql = "SELECT top 1 * FROM GATROBO (NOLOCK) WHERE Robo LIKE 'GAT Link RPA - Tabulação CANCELAMENTO NET SMS%'";

                    //data
                    ssql = ssql + " and DataStatus between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " order by DataStatus desc";

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
    /// Rel_TotalRegistros
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_TotalRegistros(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"SELECT COUNT(1) FROM GATCliente (NOLOCK) WHERE idcodstatus_rpa_cancelamento = 4";

                    #region FILTRO
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe_n1 in (" + sequipe + ") or idcodequipe_n2 in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario_n1 in (" + susuario + ") or idcodusuario_n2 in (" + susuario + ")";
                    }

                    //data
                    if (chkData_Filtro.Checked == true)
                    {
                        ssql = ssql + " and data_carga between '" + dtini + "' and '" + dtfim + "'";
                    }
                    
                    #endregion

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;


                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblTotRegistros.Text = dr[0].ToString();
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
    /// Rel_TotalEmProcess
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_TotalEmProcess(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"SELECT COUNT(1) FROM GATCliente (NOLOCK) WHERE idcodstatus_rpa_cancelamento = 1";

                    #region FILTRO
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe_n1 in (" + sequipe + ") or idcodequipe_n2 in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario_n1 in (" + susuario + ") or idcodusuario_n2 in (" + susuario + ")";
                    }

                    //data
                    if (chkData_Filtro.Checked == true)
                    {
                        ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }
                    #endregion

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;


                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblTotEmProcessamento.Text = dr[0].ToString();
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
    /// Rel_TotalProcessado
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_TotalProcessado(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"SELECT COUNT(1) FROM GATCliente (NOLOCK) WHERE idcodstatus_rpa_cancelamento = 2 ";

                    #region FILTRO
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe_n1 in (" + sequipe + ") or idcodequipe_n2 in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario_n1 in (" + susuario + ") or idcodusuario_n2 in (" + susuario + ")";
                    }

                    //data
                    if (chkData_Filtro.Checked == true)
                    {
                        ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }

                    #endregion

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;


                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblTotProcess.Text = dr[0].ToString();
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
    /// Rel_TotalErroProcessado
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_TotalErroProcessado(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"SELECT COUNT(1) FROM GATCliente (NOLOCK) WHERE idcodstatus_rpa_cancelamento in (3)"; //Erro e indevido

                    #region FILTRO
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe_n1 in (" + sequipe + ") or idcodequipe_n2 in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario_n1 in (" + susuario + ") or idcodusuario_n2 in (" + susuario + ")";
                    }

                    //data
                    if (chkData_Filtro.Checked == true)
                    {
                        ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }
                    #endregion

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;


                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lblTotErroProcess.Text = dr[0].ToString();
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
    /// Rel_TotalErrocontrato
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_TotalErrocontrato(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim)
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
                    ssql = @"SELECT COUNT(1) FROM GATCliente (NOLOCK) WHERE idcodstatus_rpa_cancelamento in (6)"; //Erro e indevido

                    #region FILTRO
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and idcodequipe_n1 in (" + sequipe + ") or idcodequipe_n2 in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario_n1 in (" + susuario + ") or idcodusuario_n2 in (" + susuario + ")";
                    }

                    //data
                    if (chkData_Filtro.Checked == true)
                    {
                        ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";
                    }
                    #endregion

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;


                    SqlDataReader dr = cmd.ExecuteReader();

                    string sv = "";

                    if (dr.Read())
                    {
                        sv = dr[0].ToString();

                        lbltoterro_contrato.Text = dr[0].ToString();
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
    /// RelTMT
    /// </summary>
    public void RelTMT(string sperfil, string dtini, string dtfim)
    {
        try
        {

            gergatlink obj = new gergatlink();
            string ssql = "";
            int itmt = 0;

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //comando select
                    ssql = @"select	isnull(avg(DATEDIFF(second,data_ini_process_rpa_cancelamento,data_fim_process_rpa_cancelamento) ) ,0) 'tempo'
                            from	GATCliente (nolock)
                            where	ISNULL(data_ini_process_rpa_cancelamento,'') <> ''
                            and		ISNULL(data_fim_process_rpa_cancelamento,'') <> ''";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //data
                    //ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        itmt = Convert.ToInt32(dr[0].ToString());
                    }
                    dr.Read();

                    lblTMT.Text = obj.horacheia(itmt);


                }
            }

        }
        catch (Exception err)
        {

            Erro(err.Message);
        }
    }

    /// <summary>
    /// RelDetalhes
    /// </summary>
    public void RelDetalhes(string sperfil, string dtini, string dtfim)
    {
        try
        {

            gergatlink obj = new gergatlink();
            string ssql = "";
            int itmt = 0;

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //comando select
                    ssql = @"select	obs_rpa_cancelamento 'desc',
		                            COUNT(1) 'total',
		                            'perc' = ''
                            from	GATCliente (nolock)
                            where	ISNULL(obs_rpa_cancelamento,'') <> ''";

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and idcodperfil_n1 in (" + sperfil + ") OR idcodperfil_n2 in (" + sperfil + ")";
                    }

                    //data
                    //ssql = ssql + " and data_rpa_cancelamento between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " group by obs_rpa_cancelamento";
                    ssql = ssql + " order by total  desc";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados_operadores.DataSource = dr;
                    gdw_dados_operadores.DataBind();


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
    /// gdw_dados_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            gergatlink obj = new gergatlink();

            string sid = e.CommandArgument.ToString();

            //retrabalhar registros
            if (e.CommandName == "remover")
            {
                //retrablho de registros
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "delete from gatrobo where idcodrobo > 6 and idcodrobo = " + sid;
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();

                        AplicaFiltro();
                    }
                }

            }
        }
        catch (Exception err)
        {
            Erro(err.Message);

        }
    }

    /// <summary>
    /// cmdretrab_emprocess_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdretrab_emprocess_Click(object sender, EventArgs e)
    {
        try
        {

            gergatlink obj = new gergatlink();

            //retrablho de registros
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandText = "update GATFilaTabulacoesNETSMS set IDCODSTATUSRPA_NETSMS=4,DATA_STATUSRPA_NETSMS=getdate(),DATA_INIPROCESS_RPA_NETSMS=null,DATA_FIMPROCESS_RPA_NETSMS=null,DETALHE_PROCESSRPA_NETSMS='' where isnull(IDCODSTATUSRPA_NETSMS,0) = 1";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    AplicaFiltro();
                }
            }


            //limpa cache
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandText = "delete from GATTempRegistro_RPA_TABULACAO_NETSMS";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

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
    protected void cmdretrab_erro_Click(object sender, EventArgs e)
    {
        try
        {

            gergatlink obj = new gergatlink();

            //retrablho de registros
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandText = "update GATCliente set idcodstatus_rpa_cancelamento=4,data_rpa_cancelamento=getdate(),data_ini_process_rpa_cancelamento=null,data_fim_process_rpa_cancelamento=null,obs_rpa_cancelamento='' where isnull(idcodstatus_rpa_cancelamento,0) = 3";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    AplicaFiltro();
                }
            }


            //limpa cache
            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cn;
                    cmd.CommandText = "delete from GATTempRegistroRoboCancelamentoNETSMS";
                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                }
            }


        }
        catch (Exception err)
        {
            Erro(err.Message);

        }
    }

    /// <summary>
    /// gdw_dados_operadores_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_operadores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            double dval = 0;
            double dtot = 0;
            double dperc = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                itot = itot + Convert.ToInt32(e.Row.Cells[2].Text);

                //quando for registro de sucesso nao habilita botao
                if (e.Row.Cells[1].Text == "Processamento Conclu&#237;do com Sucesso !")
                {
                    e.Row.Cells[5].Text = "";
                }
                else
                {
                    //LINK - FILTRO
                    e.Row.Cells[2].Text = "<a style='text-decoration:none; color:black;' href='detalhes_rpa.aspx?_scampanha=" + _scampanha + "&_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sidassunto=&_sstatusbpm=0&_sidstatusnetsms=0&_sdetalhenetsms=" + e.Row.Cells[1].Text + "'' target='_blank'>" + e.Row.Cells[2].Text + "</a>";
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "<font color='red'><b>Total:</b></font>";
                e.Row.Cells[2].Text = "<font color='red'><b>" + itot.ToString() + "</b></font>";
                e.Row.Cells[4].Text = "<font color='red'><b>100</b></font>";

                dtot = Convert.ToDouble(itot);

                foreach (TableRow item in gdw_dados_operadores.Rows)
                {
                    dval = Convert.ToDouble(item.Cells[3].Text);

                    if (dtot > 0)
                    {
                        dperc = (dval / dtot) * 100;
                    }
                    else
                    {
                        dperc = 0;
                    }

                    item.Cells[4].Text = "<font color='red'><b>" + dperc.ToString("0.00") + "</b></font>";

                }

            }

        }
        catch (Exception err)
        {

            Erro(err.Message);
        }
    }

    /// <summary>
    ///  gdw_dados_operadores_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_operadores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            string sid = e.CommandArgument.ToString();


            //retrabalhar registros
            if (e.CommandName == "liberar")
            {
                //retrablho de registros
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "update GATCliente set idcodstatus_rpa_cancelamento=4,data_rpa_cancelamento=getdate(),data_ini_process_rpa_cancelamento=null,data_fim_process_rpa_cancelamento=null,obs_rpa_cancelamento='' where isnull(idcodstatus_rpa_cancelamento,0) = 3 AND obs_rpa_cancelamento = '" + sid + "'";
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();

                        AplicaFiltro();
                    }
                }


                //limpa cache
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = cn;
                        cmd.CommandText = "delete from GATTempRegistro_RPA_TABULACAO_NETSMS";
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();

                    }
                }

            }


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

}