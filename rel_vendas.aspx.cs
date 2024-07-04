using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_vendas : System.Web.UI.Page
{

    //variaveis
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public string iEquipe = "";

    public string sSite = "";

    double itotlogado = 0;
    string itotnvend = "";

    double itotlogadoger = 0;
    string itotnvendger = "";

    double zeradosger = 0;
    double upger = 0;
    double assitger = 0;
    double totsiteger = 0;

    double zerados = 0;
    double up = 0;
    double assit = 0;
    double totsite = 0;

    double itotlogadocoor = 0;
    string itotnvendcoor = "";

    double zeradoscoor = 0;
    double upcoor = 0;
    double assitcoor = 0;
    double totsitecoor = 0;

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

    #region FILTRAR

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

                    sdml = "select  * from tel_mp_gatperfil (nolock) where idpermissao not in (6,7) and habilitado = 1 ";
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

                    sdml = "select * from tel_mp_gatperfil (nolock) where idpermissao = 6 and habilitado = 1 ";
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

                    sdml = "select idcodusuario,nome from tel_mp_gatusuario (nolock) where habilitado = 1 ";
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

            Rel_Vendas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);


        }
        catch (Exception err)
        {
            Erro(err.Message);
        }

    }

    /// <summary>
    /// Rel_Vendas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Vendas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {

            gergatlink obj = new gergatlink();

            string ssql = @"select 
		                                a.IdCodVenda 'ID',	
		                                a.DataInsercao 'Data',
		                                b.Nome 'Cliente',
		                                i.TipoCliente,
		                                b.TipoSegmento,
		                                b.CNPJ 'CPF_CNPJ',
		                                b.Endereco,
		                                b.Decisor,
		                                b.Contato,
		                                f.TipoVenda,
		                                h.Tipo 'Check',
		                                a.TipoMaquina_Point	,
		                                a.NumeroControle_Point,	
		                                a.FormaPagamento_Point,
		                                'Foto_Point' = '',
		                                g.Status 'StatusVenda_QR',
		                                a.CadatroPix_QR	,
		                                a.PossuiConta_QR,
		                                a.Email_QR,
		                                a.DataAlteracao,
		                                a.TPV_Prometido,	
		                                a.Nome_Responsavel,	
		                                a.Telefone_Responsavel,	
		                                a.Email_MercadoPago,	
		                                a.Numero_Operacao1,	
		                                a.Numero_Operacao2,	
		                                a.Numero_Operacao3,	
		                                a.Numero_Card,	
		                                a.Chip_Anterior,	
		                                a.Chip_Atual,
		                                a.Observacao,
		                                d.Perfil,
		                                e.Perfil 'Equipe',
		                                c.Nome 'Promotor'

                                from	TEL_MP_GATVenda a(nolock),
		                                TEL_MP_GATCliente b(nolock),
		                                TEL_MP_GATUsuario c(nolock),
		                                TEL_MP_GATPerfil d(nolock),
		                                TEL_MP_GATPerfil e(nolock),
		                                TEL_MP_GATTipoVenda f(nolock),
		                                TEL_MP_GATStatusVenda g(nolock),
		                                TEL_MP_GATTipoCheckVenda h(nolock),
		                                TEL_MP_GATTipoCliente i (nolock)

                                where	a.IdCodCliente = b.IdCodCliente
                                and		a.IdCodUsuario = c.IDCodUsuario
                                and		a.IdCodPerfil = d.IdCodPerfil
                                and		a.IdCodEquipe = e.IdCodPerfil
                                and		a.IdTipoVenda = f.IdTipoVenda
                                and		a.IdCodStatusVenda_QR *= g.IdCodStatusVenda
                                and		a.IdCodTipoCheckVenda *= h.IdCodTipoCheckVenda
                                and		b.IdCodTipoCliente = i.IdCodTipoCliente";

            if (sequipe != "")
            {
                ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sequipe + ") )";
            }

            if (sperfil != "")
            {
                ssql = ssql + " and		a.IDCodUsuario in (select IDCodUsuario from tel_mp_GATUsuarioPerfil (nolock) where IDCodPerfil in (" + sperfil + "))";
            }

            if (susuario != "")
            {
                ssql = ssql + " and		a.IDCodUsuario in (" + susuario + ")";
            }

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados.DataSource = dr;
                    gdw_dados.DataBind();

                }
            }



        }
        catch (Exception err)
        {
            Erro("Erro " + err.Message);
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_vendas"] = "";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        Session["txt_vendas"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_vendas"] += "\r\n";

            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //carrega icone da foto para vendas tipos point
                if (e.Row.Cells[10].Text != "QR")
                {
                    e.Row.Cells[15].Text = "<a style='text-decoration:none; color:black;' href='DetalhesFotosVendaPoint.aspx?_id=" + e.Row.Cells[1].Text + "' target='_blank'><img src='imagens/galeria.png'/></a>";
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        if (i == 15)
                        {
                            Session["txt_vendas"] += "https://www.gatlink.com.br/adminapp/DetalhesFotosVendaPoint.aspx?_id="+ e.Row.Cells[1].Text + ";";
                        }
                        else
                        {
                            Session["txt_vendas"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                        }
                        
                    }
                }

                //Add new line.
                Session["txt_vendas"] += "\r\n";

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_vendas.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_vendas"]);
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