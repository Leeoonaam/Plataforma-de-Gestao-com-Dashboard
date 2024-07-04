using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_gerencial_naotabulados : System.Web.UI.Page
{
    //Variaveis
    public string _scampanha = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public string _stab = "";
    public string _scontrato = "";

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

            #region
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
            

            //tabulado
            svig = "";
            foreach (ListItem obj in cmbTabulado.Items)
            {
                if (obj.Selected == true)
                {
                    _stab = _stab + svig + obj.Value;
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

                DateTime dfim2 = DateTime.Now.AddDays(7);
            }

            #endregion

            //relatorios
            Rel_NaoTabulados(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);
            Rel_NaoTabulados_Equipe(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);
            Rel_NaoTabulados_Operador(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);

            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            Grafico_DiaDia(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);
            Grafico_Equipe_DiaDia(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);
            Grafico_Operador_DiaDia(_scampanha, _sperfil, _sequipe, _susuario, _sdtini, _sdtfim, _scontrato, _stab);

            Literal1.Text = Literal1.Text + @"}
            </script>";

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

    #endregion

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
                //FILTRO
                Carrega_Lista_Filtro_Campanhas("");
                Carrega_Lista_Filtro_Perfil("");
                Carrega_Lista_Filtro_Equipe("");
                Carrega_Lista_Filtro_Usuarios("");

                AplicaFiltro();
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    
    #region RELATORIOS
    /// <summary>
    /// Rel_NaoTabulados
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Rel_NaoTabulados(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            DataRow orow;
            DataSet ds_datas = new DataSet();

            long ltotal = 0;

            //Cria a estrutura da tabela
            DataTable otable = new DataTable("GATDados");

            otable.Columns.Add("Descrição");

            //Cria colunas dinamicamente
            using (SqlConnection cn = ogat.abre_cn())
            {

                //select
                ssql = " SELECT	X.DATA ";
                ssql = ssql + " FROM ";
                ssql = ssql + " (";
                ssql = ssql + " SELECT	DISTINCT CAST(A.DATA AS DATE) 'DATA'";
                ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK) WHERE 1=1";
                //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0";


                //campanha
                if (scampanha.Trim() != "")
                {
                    ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                //tabulado
                if (stab.Trim() != "")
                {
                    ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                }



                //data
                ssql = ssql + " AND     A.DATA BETWEEN '" + dtini + "' AND '" + dtfim + "'";

                ssql = ssql + " ) X";
                ssql = ssql + " ORDER BY X.DATA";

                //carrega o data set
                using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
                {
                    da.Fill(ds_datas);
                }

            }

            //Cria as colunas no datatable
            foreach (DataRow item in ds_datas.Tables[0].Rows)
            {
                otable.Columns.Add(Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy"));
            }

            otable.Columns.Add("Total");

            //Carrega o valoes
            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta o select
                    ssql = @"SELECT	count(1)'tot'

                    FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK)
                    WHERE	1 = 1";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                    }


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ltotal = 0;

                        //insere informacao inicial
                        orow = otable.NewRow();
                        orow["Descrição"] = "Não Tabulado";


                        //carrega as informacoes 
                        //cria as colunas no datatable
                        foreach (DataRow item in ds_datas.Tables[0].Rows)
                        {
                            using (SqlConnection cn2 = ogat.abre_cn())
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    ssql = " SELECT	COUNT(1)";
                                    ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS a (nolock)";
                                    ssql = ssql + " where	1=1";
                                    //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0";
                                    ssql = ssql + " and     CAST(a.DATA as DATE) = '" + Convert.ToDateTime(item[0].ToString()).ToString("yyyy-MM-dd") + "'";

                                    //campanha
                                    if (scampanha.Trim() != "")
                                    {
                                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                                    //tabulado
                                    if (stab.Trim() != "")
                                    {
                                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                                    }


                                    cmd2.Connection = cn2;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = ssql;

                                    SqlDataReader dr2 = cmd2.ExecuteReader();

                                    if (dr2.Read())
                                    {
                                        orow[Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy")] = dr2[0].ToString();
                                        ltotal = ltotal + Convert.ToInt32(dr2[0].ToString());
                                    }
                                    dr2.Close();


                                }

                            }

                        }

                        orow["TOTAL"] = ltotal.ToString();
                        otable.Rows.Add(orow);
                    }
                    dr.Close();

                }

            }

            //carrea o grid
            gdw_dados.DataSource = otable;
            gdw_dados.DataBind();

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Rel_NaoTabulados_Equipe(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            DataRow orow;
            DataSet ds_datas = new DataSet();

            long ltotal = 0;

            //Cria a estrutura da tabela
            DataTable otable = new DataTable("GATDados");

            otable.Columns.Add("Equipe");

            //Cria colunas dinamicamente
            using (SqlConnection cn = ogat.abre_cn())
            {

                //select
                ssql = " SELECT	X.DATA ";
                ssql = ssql + " FROM ";
                ssql = ssql + " (";
                ssql = ssql + " SELECT	DISTINCT CAST(A.DATA AS DATE) 'DATA'";
                ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK) WHERE 1=1";
                //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0";
                //ssql = ssql + " AND     A.IDCODEQUIPE IN (SELECT	IDCODPERFIL FROM GATPerfil(NOLOCK)";
                //ssql = ssql + " WHERE   HABILITADO = 1 AND IDPERMISSAO = 10)";

                //campanha
                if (scampanha.Trim() != "")
                {
                    ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                //tabulado
                if (stab.Trim() != "")
                {
                    ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                }


                //data
                ssql = ssql + " AND     A.DATA BETWEEN '" + dtini + "' AND '" + dtfim + "'";

                ssql = ssql + " ) X";
                ssql = ssql + " ORDER BY X.DATA";

                //carrega o data set
                using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
                {
                    da.Fill(ds_datas);
                }

            }

            //Cria as colunas no datatable
            foreach (DataRow item in ds_datas.Tables[0].Rows)
            {
                otable.Columns.Add(Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy"));
            }

            otable.Columns.Add("Total");

            //Carrega o valoes
            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta o select
                    ssql = @"SELECT	
		                            DISTINCT A.IDCODEQUIPE,B.Perfil

                            FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK),
		                            GATPerfil B(NOLOCK)
                            WHERE	A.IDCODEQUIPE = B.IDCodPerfil";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                    }


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ltotal = 0;

                        //insere informacao inicial
                        orow = otable.NewRow();
                        orow["Equipe"] = dr["PERFIL"].ToString();


                        //carrega as informacoes 
                        //cria as colunas no datatable
                        foreach (DataRow item in ds_datas.Tables[0].Rows)
                        {
                            using (SqlConnection cn2 = ogat.abre_cn())
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    ssql = " SELECT	COUNT(1)";
                                    ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS a (nolock)";
                                    ssql = ssql + " where	1=1";
                                    //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0 ";
                                    ssql = ssql + " AND		A.IDCODEQUIPE = " + dr["IDCODEQUIPE"].ToString();

                                    ssql = ssql + " and     CAST(a.DATA as DATE) = '" + Convert.ToDateTime(item[0].ToString()).ToString("yyyy-MM-dd") + "'";

                                    //campanha
                                    if (scampanha.Trim() != "")
                                    {
                                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                                    //tabulado
                                    if (stab.Trim() != "")
                                    {
                                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                                    }


                                    cmd2.Connection = cn2;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = ssql;

                                    SqlDataReader dr2 = cmd2.ExecuteReader();

                                    if (dr2.Read())
                                    {
                                        orow[Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy")] = dr2[0].ToString();
                                        ltotal = ltotal + Convert.ToInt32(dr2[0].ToString());
                                    }
                                    dr2.Close();


                                }

                            }

                        }

                        orow["TOTAL"] = ltotal.ToString();
                        otable.Rows.Add(orow);
                    }
                    dr.Close();

                }

            }

            //carrea o grid
            gdw_dados_equipe.DataSource = otable;
            gdw_dados_equipe.DataBind();

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Rel_NaoTabulados_Operador(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            DataRow orow;
            DataSet ds_datas = new DataSet();

            long ltotal = 0;

            //Cria a estrutura da tabela
            DataTable otable = new DataTable("GATDados");

            otable.Columns.Add("Operador");

            //Cria colunas dinamicamente
            using (SqlConnection cn = ogat.abre_cn())
            {

                //select
                ssql = " SELECT	X.DATA ";
                ssql = ssql + " FROM ";
                ssql = ssql + " (";
                ssql = ssql + " SELECT	DISTINCT CAST(A.DATA AS DATE) 'DATA'";
                ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK) WHERE 1=1";
                //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0";

                //data
                ssql = ssql + " AND     A.DATA BETWEEN '" + dtini + "' AND '" + dtfim + "'";

                //campanha
                if (scampanha.Trim() != "")
                {
                    ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                //tabulado
                if (stab.Trim() != "")
                {
                    ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                }


                ssql = ssql + " ) X";
                ssql = ssql + " ORDER BY X.DATA";

                //carrega o data set
                using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
                {
                    da.Fill(ds_datas);
                }

            }

            //Cria as colunas no datatable
            foreach (DataRow item in ds_datas.Tables[0].Rows)
            {
                otable.Columns.Add(Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy"));
            }

            otable.Columns.Add("Total");

            //Carrega o valoes
            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta o select
                    ssql = @"SELECT	
		                            DISTINCT A.IDCODUSUARIO,B.NOME

                            FROM	GATLogAtendNaoTabuladosNETSMS A (NOLOCK),
		                            GATUsuario B(NOLOCK)

                            WHERE	A.IDCODUSUARIO = B.IDCODUSUARIO";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                    }


                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ltotal = 0;

                        //insere informacao inicial
                        orow = otable.NewRow();
                        orow["Operador"] = dr["Nome"].ToString();


                        //carrega as informacoes 
                        //cria as colunas no datatable
                        foreach (DataRow item in ds_datas.Tables[0].Rows)
                        {
                            using (SqlConnection cn2 = ogat.abre_cn())
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    ssql = " SELECT	COUNT(1)";
                                    ssql = ssql + " FROM	GATLogAtendNaoTabuladosNETSMS a (nolock)";
                                    ssql = ssql + " where	1=1";
                                    //ssql = ssql + " AND     ISNULL(A.TABULADO,0)=0 ";
                                    ssql = ssql + " AND		A.IDCODUSUARIO = " + dr["IDCODUSUARIO"].ToString();

                                    ssql = ssql + " and     CAST(a.DATA as DATE) = '" + Convert.ToDateTime(item[0].ToString()).ToString("yyyy-MM-dd") + "'";

                                    //campanha
                                    if (scampanha.Trim() != "")
                                    {
                                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                                    //tabulado
                                    if (stab.Trim() != "")
                                    {
                                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                                    }


                                    cmd2.Connection = cn2;
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.CommandText = ssql;

                                    SqlDataReader dr2 = cmd2.ExecuteReader();

                                    if (dr2.Read())
                                    {
                                        orow[Convert.ToDateTime(item[0].ToString()).ToString("dd-MM-yyyy")] = dr2[0].ToString();
                                        ltotal = ltotal + Convert.ToInt32(dr2[0].ToString());
                                    }
                                    dr2.Close();


                                }

                            }

                        }

                        orow["TOTAL"] = ltotal.ToString();
                        otable.Rows.Add(orow);
                    }
                    dr.Close();

                }

            }

            //carrea o grid
            gdw_dados_operador.DataSource = otable;
            gdw_dados_operador.DataBind();

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #endregion

    #region GRAFICOS
    /// <summary>
    /// 
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Grafico_DiaDia(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink obj = new gergatlink();

            DateTime dtini_trab = Convert.ToDateTime(dtini);
            DateTime dtfim_trab = Convert.ToDateTime(dtfim);
            DateTime dt_trab = dtini_trab;
            string svig = "";

            Literal1.Text = Literal1.Text + @"  
                            var chart_1 = new CanvasJS.Chart('div_graf_diadia',
	                    {
		                    title:{
			                    text: 'Registros Não tabulados por Dia'
		                    },
		                    legend: {
			                    maxWidth: 350,
			                    itemWidth: 120
		                    },
animationEnabled: true,
		                    data: [
		                    {
			            
			                type: 'column',
			                dataPoints: [";

            while (dt_trab <= dtfim_trab)
            {
                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sresul = "";

                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;

                        string ssql = @"SELECT  X.DIA,
                                        COUNT(1)'TOTAL'
                                FROM    (
                                            SELECT  DATEPART(DAY, A.DATA)'DIA'
                                            FROM    GATLogAtendNaoTabuladosNETSMS A(NOLOCK)
                                            WHERE   1=1";

                        //campanha
                        if (scampanha.Trim() != "")
                        {
                            ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                        //tabulado
                        if (stab.Trim() != "")
                        {
                            ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                        }


                        //data
                        ssql = ssql + " and a.data between '" + dt_trab.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dt_trab.ToString("yyyy-MM-dd 23:59:59") + "'";

                        ssql = ssql + ") X";
                        ssql = ssql + " GROUP BY X.DIA ";
                        ssql = ssql + " ORDER BY X.DIA";

                        cmd.CommandText = ssql;

                        SqlDataReader dr = cmd.ExecuteReader();



                        if (dr.Read())
                        {
                            sresul = dr["Total"].ToString();

                            Literal1.Text = Literal1.Text + svig + "{  y: " + sresul + ", label: '" + dr["Dia"].ToString() + "' }";
                            svig = ",";
                        }
                        dr.Close();
                    }
                }

                dt_trab = dt_trab.AddDays(1);
            }

            Literal1.Text = Literal1.Text + @"]
		                }
		                    ]
	                    });
	                    chart_1.render();";
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Grafico_Equipe_DiaDia(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string svig = "";

            Literal1.Text = Literal1.Text + @"  
                            var chart_1 = new CanvasJS.Chart('div_graf_equipe_diadia',
	                    {
		                    title:{
			                    text: 'Registros Não tabulados por Equipe'
		                    },
		                    legend: {
			                    maxWidth: 350,
			                    itemWidth: 120
		                    },
animationEnabled: true,
		                    data: [
		                    {
			            
			                type: 'column',
			                dataPoints: [";

                using (SqlConnection cn = obj.abre_cn())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sresul = "";

                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.Text;

                        string ssql = @"SELECT  X.Perfil,
                                                COUNT(1)'TOTAL'
                                        FROM    (
                                                    SELECT  B.Perfil
                                                    FROM    GATLogAtendNaoTabuladosNETSMS A(NOLOCK),
					                                        GATPerfil B(NOLOCK)
                                                    WHERE   A.IDCODEQUIPE = B.IDCodPerfil
                                                    ";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                    }


                    //data
                    ssql = ssql + " and a.data between '" + dtini + "' and '" + dtfim + "'";

                        ssql = ssql + ") X";
                        ssql = ssql + " GROUP BY X.Perfil ";
                        ssql = ssql + " ORDER BY X.Perfil";

                        cmd.CommandText = ssql;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            sresul = dr["Total"].ToString();

                            Literal1.Text = Literal1.Text + svig + "{  y: " + sresul + ", label: '" + dr["Perfil"].ToString() + "' }";
                            svig = ",";
                        }
                        dr.Close();
                    }
                }

            Literal1.Text = Literal1.Text + @"]
		                }
		                    ]
	                    });
	                    chart_1.render();";
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    /// <param name="scontrato"></param>
    /// <param name="stab"></param>
    public void Grafico_Operador_DiaDia(string scampanha, string sperfil, string sequipe, string susuario, string dtini, string dtfim, string scontrato, string stab)
    {
        try
        {
            gergatlink obj = new gergatlink();
            string svig = "";

            Literal1.Text = Literal1.Text + @"  
                            var chart_1 = new CanvasJS.Chart('div_graf_operador_diadia',
	                    {
		                    title:{
			                    text: 'Registros Não tabulados por Operador'
		                    },
		                    legend: {
			                    maxWidth: 350,
			                    itemWidth: 120
		                    },
animationEnabled: true,
		                    data: [
		                    {
			            
			                type: 'column',
			                dataPoints: [";

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sresul = "";

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;

                    string ssql = @"SELECT      X.Nome,
                                                COUNT(1)'TOTAL'
                                        FROM    (
                                                    SELECT  B.Nome
                                                    FROM    GATLogAtendNaoTabuladosNETSMS A(NOLOCK),
					                                        GATUsuario B(NOLOCK)

                                                    WHERE   A.IdCodUsuario = B.IdCodUsuario
                                                    ";

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a. idcodcliente in (select idcodcliente from gatcliente (nolock) where idcodcampanha in (" + scampanha + "))";
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

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        ssql = ssql + " and isnull(a.tabulado,0) in (" + stab + ")";
                    }


                    //data
                    ssql = ssql + " and a.data between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + ") X";
                    ssql = ssql + " GROUP BY X.Nome ";
                    ssql = ssql + " ORDER BY X.Nome";

                    cmd.CommandText = ssql;

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        sresul = dr["Total"].ToString();

                        Literal1.Text = Literal1.Text + svig + "{  y: " + sresul + ", label: '" + dr["Nome"].ToString() + "' }";
                        svig = ",";
                    }
                    dr.Close();
                }
            }

            Literal1.Text = Literal1.Text + @"]
		                }
		                    ]
	                    });
	                    chart_1.render();";
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    #endregion

    #region DADOS DAS TABELAS (GRIDVIEW)
    
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                //escreve o total
                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";

                //soma valores ja preenchidos no grid
                foreach (GridViewRow item in gdw_dados.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        for (int i = 2; i < item.Cells.Count; i++)
                        {
                            if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text.IndexOf("<font color='red'>") > -1) e.Row.Cells[i].Text = "0";
                            e.Row.Cells[i].Text = "<font color='red'><strong>"+(Convert.ToInt32(e.Row.Cells[i].Text) + Convert.ToInt32(item.Cells[i].Text)).ToString()+"</strong></font>"; ;
                        }
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
    /// gdw_dados_equipe_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_equipe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        gergatlink obj = new gergatlink();

        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_equipe"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_equipe"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_equipe"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_equipe"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_equipe"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                //escreve o total
                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";

                //soma valores ja preenchidos no grid
                foreach (GridViewRow item in gdw_dados_equipe.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        for (int i = 2; i < item.Cells.Count; i++)
                        {
                            if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text.IndexOf("<font color='red'>")>-1 ) e.Row.Cells[i].Text = "0";
                            e.Row.Cells[i].Text = "<font color='red'><strong>" + (Convert.ToInt32(e.Row.Cells[i].Text) + Convert.ToInt32(item.Cells[i].Text)).ToString()+ "</strong ></font>";
                        }
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
    /// gdw_dados_operador_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_dados_operador_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        gergatlink obj = new gergatlink();

        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_operador"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_operador"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_operador"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_operador"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_operador"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                //escreve o total
                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";

                //soma valores ja preenchidos no grid
                foreach (GridViewRow item in gdw_dados_equipe.Rows)
                {
                    if (item.RowType == DataControlRowType.DataRow)
                    {
                        for (int i = 2; i < item.Cells.Count; i++)
                        {
                            if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text.IndexOf("<font color='red'>") > -1) e.Row.Cells[i].Text = "0";
                            e.Row.Cells[i].Text = "<font color='red'><strong>" + (Convert.ToInt32(e.Row.Cells[i].Text) + Convert.ToInt32(item.Cells[i].Text)).ToString() + "</strong ></font>";
                        }
                    }
                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    #endregion

}