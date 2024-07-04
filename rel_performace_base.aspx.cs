using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_performace_base : System.Web.UI.Page
{

    double tQtdLigacoes = 0;
    double tQtdAtendidas = 0;
    double tQtdContatos = 0;
    double tQtdContatosEfetivos = 0;
    double tQtdVendaCliente = 0;
    double tQtdVendaProduto = 0;
    double tQtdContatosVirgens = 0;

    public string _scampanha = "";
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public string _sgrupo = "";
    public string _scampo1 = "";
    public string _scampo2 = "";

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


            _scampo1 = cmbcampobase.SelectedValue;
            _scampo2 = cmbcampobase2.SelectedValue;

            #endregion

            Rel_Finalizacoes(_scampanha, _sperfil, _sequipe, _susuario, _sproduto, _sdtini, _sdtfim, _sgrupo, _scampo1, _scampo2);



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
    public void Rel_Finalizacoes(string scampanha, string sperfil, string sequipe, string susuario, string sproduto, string dtini, string dtfim, string sgrupo, string scampo, string scampo2)
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
                    ssql = "SELECT	X." + scampo + " 'CAMPO',";

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " X." + scampo2 + " 'CAMPO2',";
                    }
                    else
                    {
                        ssql = ssql + " 'CAMPO2' = '',";
                    }

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT(AA.IDCodCliente) ";
                    ssql = ssql + " FROM	VOXTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;


                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodperfil in (" + sperfil + ")";
                    }


                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodusuario in (" + susuario + ")";
                    }


                    //data
                    ssql = ssql + " and AA.dataTELchamada between '" + dtini + "' and '" + dtfim + "'";


                    ssql = ssql + " ) 'QTD_LIGACOES', ";

                    ssql = ssql + " ( ";

                    ssql = ssql + " SELECT	COUNT(AA.IDCodCliente) ";
                    ssql = ssql + " FROM	VOXTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " and	    AA.codfinalizacao = 79  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodperfil in (" + sperfil + ")";
                    }


                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodusuario in (" + susuario + ")";
                    }


                    //data
                    ssql = ssql + " and AA.dataTELchamada between '" + dtini + "' and '" + dtfim + "'";


                    ssql = ssql + " ) 'QTD_ATENDIDAS', ";

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT( AA.IDCodCliente) ";
                    ssql = ssql + " FROM	GATTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK), ";
                    ssql = ssql + " GATMotivoChamada CC (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    ssql = ssql + " AND		AA.IDCodMotivo = CC.IDCodMotivo  ";
                    ssql = ssql + " AND		CC.Improdutivas = 0 ";

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodgrupo in (" + sgrupo + ")";
                    }

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

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ) 'QTD_CONTATOS', ";

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT( AA.IDCodCliente) ";
                    ssql = ssql + " FROM	GATTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK), ";
                    ssql = ssql + " GATMotivoChamada CC (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    ssql = ssql + " AND		AA.IDCodMotivo = CC.IDCodMotivo  ";
                    ssql = ssql + " AND		(CC.Venda = 1 OR CC.REC = 1 OR CC.Finalizado=1) ";

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodgrupo in (" + sgrupo + ")";
                    }

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

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ) 'QTD_CONTATOS_EFET', ";

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT(DISTINCT AA.IDCodCliente) ";
                    ssql = ssql + " FROM	GATTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK), ";
                    ssql = ssql + " GATMotivoChamada CC (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    ssql = ssql + " AND		AA.IDCodMotivo = CC.IDCodMotivo  ";
                    ssql = ssql + " AND		(CC.Venda = 1) ";

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodgrupo in (" + sgrupo + ")";
                    }

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

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ) 'QTD_VENDA_CLIENTE', ";

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT(1) ";
                    ssql = ssql + " FROM	GATTelChamadas AA (NOLOCK), ";
                    ssql = ssql + " GATCliente BB (NOLOCK), ";
                    ssql = ssql + " GATMotivoChamada CC (NOLOCK) ";
                    ssql = ssql + " WHERE	AA.IDCodCliente = BB.IDCodCliente  ";
                    ssql = ssql + " AND		BB." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		BB." + scampo2 + " = X." + scampo2;
                    }

                    ssql = ssql + " AND		AA.IDCodMotivo = CC.IDCodMotivo  ";
                    ssql = ssql + " AND		(CC.Venda = 1) ";

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodgrupo in (" + sgrupo + ")";
                    }

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

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ) 'QTD_VENDA_PRODUTO', ";

                    ssql = ssql + " ( ";
                    ssql = ssql + " SELECT	COUNT(1) ";
                    ssql = ssql + " FROM	GATCliente AA (NOLOCK)";
                    ssql = ssql + " WHERE	AA." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		AA." + scampo2 + " = X." + scampo2;
                    }

                    ssql = ssql + " AND		AA.CodStatusCliente = 0"; ;

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodgrupo in (" + sgrupo + ")";
                    }

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        //ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    //ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ) 'QTD_CLIENTE_VIRGEM', ";


                    ssql = ssql + " isnull(( ";
                    ssql = ssql + " SELECT	avg(aa.spin) ";
                    ssql = ssql + " FROM	GATCliente AA (NOLOCK)";
                    ssql = ssql + " WHERE	AA." + scampo + " = X." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " AND		AA." + scampo2 + " = X." + scampo2;
                    }

                    //ssql = ssql + " AND		AA.CodStatusCliente = 0"; ;

                    //grupo
                    if (sgrupo.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodgrupo in (" + sgrupo + ")";
                    }

                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodperfil in (" + sperfil + ")";
                    }

                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and AA.idcodequipe in (" + sequipe + ")";
                    }

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and aa.idcodusuario in (" + susuario + ")";
                    }

                    //produto
                    if (sproduto.Trim() != "")
                    {
                        //ssql = ssql + " and aa.idcodproduto in (" + sproduto + ")";
                    }

                    //data
                    //ssql = ssql + " and aa.datagatchamada between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " ),0) 'spin', ";


                    ssql = ssql + " 'P_ATEND_LIG' ='', ";
                    ssql = ssql + " 'P_CONTATOS_LIG' ='', ";
                    ssql = ssql + " 'P_CONTATOS_EFET_LIG' ='', ";
                    ssql = ssql + " 'P_VENDA_LIG' ='', ";
                    ssql = ssql + " 'P_VENDA_CONTATOS_EFET' ='' ";

                    ssql = ssql + " FROM	( ";
                    ssql = ssql + " SELECT	DISTINCT A." + scampo;

                    //CAMPO 2
                    if (scampo2.Trim() != "")
                    {
                        ssql = ssql + " , A." + scampo2;
                    }

                    ssql = ssql + " FROM	GATCliente A (NOLOCK) ";
                    ssql = ssql + " WHERE	1=1 ";



                    //campanha
                    if (scampanha.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodcampanha in (" + scampanha + ")";
                    }

                    //perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodcampanha in ( select idcodcampanha from gatcampperfil (nolock) where idcodperfil in (" + sperfil + ") )";
                    }


                    ssql = ssql + " ) X ";

                    ssql = ssql + " order by x.QTD_LIGACOES desc";



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

                if (cmbcampobase.SelectedValue.ToUpper() == "IDCODCAMPANHA")
                {
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            cmd.Connection = cn;
                            cmd.CommandText = "select campochave from gatcampanha (nolock) where idcodcampanha = " + e.Row.Cells[1].Text;
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                e.Row.Cells[1].Text = dr[0].ToString();
                            }
                            dr.Close();

                        }

                    }
                }

                if (cmbcampobase2.SelectedValue.ToUpper() == "IDCODCAMPANHA")
                {
                    using (SqlConnection cn = obj.abre_cn())
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {

                            cmd.Connection = cn;
                            cmd.CommandText = "select campochave from gatcampanha (nolock) where idcodcampanha = " + e.Row.Cells[2].Text;
                            cmd.CommandType = CommandType.Text;

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.Read())
                            {
                                e.Row.Cells[2].Text = dr[0].ToString();
                            }
                            dr.Close();

                        }

                    }
                }

                tQtdLigacoes = tQtdLigacoes + Convert.ToInt32(e.Row.Cells[3].Text);
                tQtdAtendidas = tQtdAtendidas + Convert.ToInt32(e.Row.Cells[4].Text);
                tQtdContatos = tQtdContatos + Convert.ToInt32(e.Row.Cells[5].Text);
                tQtdContatosEfetivos = tQtdContatosEfetivos + Convert.ToInt32(e.Row.Cells[6].Text);
                tQtdVendaCliente = tQtdVendaCliente + Convert.ToInt32(e.Row.Cells[7].Text);
                tQtdVendaProduto = tQtdVendaProduto + Convert.ToInt32(e.Row.Cells[8].Text);
                tQtdContatosVirgens = tQtdContatosVirgens + Convert.ToInt32(e.Row.Cells[14].Text);

                //Atend x Lig
                dconv = 0;
                if (Convert.ToDouble(e.Row.Cells[3].Text) > 0)
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[4].Text) / Convert.ToDouble(e.Row.Cells[3].Text)) * 100;
                }
                e.Row.Cells[9].Text = dconv.ToString("0.00");

                //Contatos x Lig
                dconv = 0;
                if (Convert.ToDouble(e.Row.Cells[3].Text) > 0)
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[5].Text) / Convert.ToDouble(e.Row.Cells[3].Text)) * 100;
                }
                e.Row.Cells[10].Text = dconv.ToString("0.00");

                //Contatos.Efet x Lig
                dconv = 0;
                if (Convert.ToDouble(e.Row.Cells[3].Text) > 0)
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[6].Text) / Convert.ToDouble(e.Row.Cells[3].Text)) * 100;
                }
                e.Row.Cells[11].Text = dconv.ToString("0.00");

                //Venda x Ligacoes
                dconv = 0;
                if (Convert.ToDouble(e.Row.Cells[3].Text) > 0)
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[7].Text) / Convert.ToDouble(e.Row.Cells[3].Text)) * 100;
                }
                e.Row.Cells[12].Text = dconv.ToString("0.00");

                //Venda x Cont.EfeT
                dconv = 0;
                if (Convert.ToDouble(e.Row.Cells[6].Text) > 0)
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[7].Text) / Convert.ToDouble(e.Row.Cells[6].Text)) * 100;
                }
                e.Row.Cells[13].Text = dconv.ToString("0.00");

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_fin1"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }

                //Add new line.
                Session["txt_fin1"] += "\r\n";


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "<font color='red'><strong>Total</strong></font>";

                e.Row.Cells[3].Text = "<font color='red'><strong>" + tQtdLigacoes.ToString() + "</strong></font>";
                e.Row.Cells[4].Text = "<font color='red'><strong>" + tQtdAtendidas.ToString() + "</strong></font>";
                e.Row.Cells[5].Text = "<font color='red'><strong>" + tQtdContatos.ToString() + "</strong></font>";
                e.Row.Cells[6].Text = "<font color='red'><strong>" + tQtdContatosEfetivos.ToString() + "</strong></font>";
                e.Row.Cells[7].Text = "<font color='red'><strong>" + tQtdVendaCliente.ToString() + "</strong></font>";
                e.Row.Cells[8].Text = "<font color='red'><strong>" + tQtdVendaProduto.ToString() + "</strong></font>";
                e.Row.Cells[14].Text = "<font color='red'><strong>" + tQtdContatosVirgens.ToString() + "</strong></font>";

                //Atend x Lig
                dconv = 0;
                if (tQtdLigacoes > 0)
                {
                    dconv = (Convert.ToDouble(tQtdAtendidas) / Convert.ToDouble(tQtdLigacoes)) * 100;
                }

                e.Row.Cells[9].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //Contatos x Lig
                dconv = 0;
                if (tQtdLigacoes > 0)
                {
                    dconv = (Convert.ToDouble(tQtdContatos) / Convert.ToDouble(tQtdLigacoes)) * 100;
                }
                e.Row.Cells[10].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //Contatos.Efet x Lig
                dconv = 0;
                if (tQtdLigacoes > 0)
                {
                    dconv = (Convert.ToDouble(tQtdContatosEfetivos) / Convert.ToDouble(tQtdLigacoes)) * 100;
                }
                e.Row.Cells[11].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //Venda x Ligacoes
                dconv = 0;
                if (tQtdLigacoes > 0)
                {
                    dconv = (Convert.ToDouble(tQtdVendaCliente) / Convert.ToDouble(tQtdLigacoes)) * 100;
                }
                e.Row.Cells[12].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

                //Venda x Cont.EfeT
                dconv = 0;
                if (tQtdContatosEfetivos > 0)
                {
                    dconv = (Convert.ToDouble(tQtdVendaCliente) / Convert.ToDouble(tQtdContatosEfetivos)) * 100;
                }
                e.Row.Cells[13].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";


                //Add new line.
                Session["txt_fin1"] += obj.TrataHTMLASCII(";Total") + ";";
                //Session["txt_fin1"] += obj.TrataHTMLASCII(obj.horacheia(Convert.ToInt32(tma_geral))) + ";";
                //Session["txt_fin1"] += obj.TrataHTMLASCII(tFinalizacoes.ToString()) + ";";
                //Session["txt_fin1"] += obj.TrataHTMLASCII(dconv.ToString("0.00")) + ";";
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



}