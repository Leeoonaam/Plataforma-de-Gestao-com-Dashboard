using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
public partial class controle_naotabulados : System.Web.UI.Page
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

            //contrato
            svig = "";
            if (txtContrato.Text != "")
            {
                _scontrato = txtContrato.Text.Replace(" ", "");
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

            ListaNaoTabulados(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim, _scontrato,_stab);

            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            //Grafico_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim);

            Literal1.Text = Literal1.Text + @"}
            </script>";

        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Load
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

    /// <summary>
    /// Atualiza Filtro
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
    /// Lista os dados
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="sproduto"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void ListaNaoTabulados(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim, string scontrato, string stab)
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
                    ssql = @"SELECT	
		                            A.IDCODREGISTRO,
		                            B.NOME,
		                            B.CPF,
		                            B.CONTRATO,
		                            B.CIDADE,
		                            C.NOME 'OPERADOR',
		                            C.LOGIN,
		                            C.LoginNetSMS 'MATRICULA',
                                    A.DATA,
		                            A.DATA_TABULACAO,
		                            ISNULL(A.OBS_NAO_TABULACAO,'')'OBS_NAO_TABULACAO',

		                            CASE ISNULL(A.TABULADO,0)
			                            WHEN 0 THEN 'NÃO'
			                            ELSE 'SIM'
		                            END 'TABULADO',
		
		                            D.PERFIL,
		                            E.PERFIL 'EQUIPE'

                            FROM	GATLogAtendNaoTabuladosNETSMS A(NOLOCK),
		                            GATCliente B(NOLOCK),
		                            GATUsuario C(NOLOCK),
		                            GATPerfil D(NOLOCK),
		                            GATPerfil E(NOLOCK)
		
                            WHERE	A.IDCODCLIENTE = B.IDCodCliente 
                            AND		A.IDCODUSUARIO = C.IDCodUsuario
                            AND		A.IDCODPERFIL = D.IDCodPerfil
                            AND		A.IDCODEQUIPE = E.IDCodPerfil";

                    #region FILTROS
                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " AND B.idcodcampanha in (" + scampanha + ")";
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
                        ssql = ssql + " and C.idcodusuario in (" + susuario + ")";
                    }

                    //Contrato
                    if (scontrato.Trim() != "")
                    {
                        ssql = ssql + " AND B.CONTRATO = '" + scontrato + "'";
                    }

                    //tabulado
                    if (stab.Trim() != "")
                    {
                        if (stab == "1") //SIM
                        {
                            ssql = ssql + " AND ISNULL(A.TABULADO,0) = 1";
                        }
                        else //NÃO
                        {
                            ssql = ssql + " AND ISNULL(A.TABULADO,0) = 0";
                        }
                    }

                    //data
                    ssql = ssql + " AND A.DATA between '" + dtini + "' and '" + dtfim + "'";
                    #endregion

                    ssql = ssql + " ORDER BY TABULADO,A.IDCODREGISTRO DESC";

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
    /// 
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
            string img = e.Row.Cells[9].Text.Trim();

            //Alterar Cor
            switch (img)
            {
                case "SIM":
                    e.Row.Cells[9].Text = "SIM " + "<img src='imagens/disponivel.png'/>";
                    break;

                case "N&#195;O":
                    e.Row.Cells[9].Text = "NÃO " + "<img src='imagens/pausa.png'/>";
                    break;
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
protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
{
    try
    {
        if (e.CommandName == "alterar")
        {
            Session["alt"] = "alt";
            Session["IdcodRegistro"] = e.CommandArgument.ToString();
            Response.Redirect("~/ger_naotabulados.aspx", false);
        }
    }
    catch (Exception err)
    {
        Erro(err.Message);
    }
}
}