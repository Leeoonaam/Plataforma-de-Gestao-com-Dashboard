using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_rotas : System.Web.UI.Page
{
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public int tTotal = 0;

    int totrotas = 0;
    int totaguard = 0;
    int totcheck = 0;
    int totagend = 0;

    public string stabela = "";
    public DataSet ds_global;

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

                    sdml = "select  * from TEL_MP_GATPerfil (nolock) where idpermissao = 7 and habilitado = 1 ";
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

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where idpermissao = 6 and habilitado = 1 ";
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


            Rel_Rotas(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);


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
                //Carrega_Lista_Filtro_Campanhas("");
                Carrega_Lista_Filtro_Perfil("");
                Carrega_Lista_Filtro_Equipe("");
                Carrega_Lista_Filtro_Usuarios("");
                //Carrega_Lista_Filtro_Produtos("");

                AplicaFiltro();

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

        double dconv = 0;

        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_rotas"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_rotas"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_rotas"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_rotas"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_rotas"] += "\r\n";

                totrotas = totrotas + Convert.ToInt32(e.Row.Cells[2].Text);
                totaguard = totaguard + Convert.ToInt32(e.Row.Cells[3].Text);
                totcheck = totcheck + Convert.ToInt32(e.Row.Cells[4].Text);
                totagend = totagend + Convert.ToInt32(e.Row.Cells[5].Text);

                dconv = (Convert.ToDouble(e.Row.Cells[4].Text) / Convert.ToDouble(e.Row.Cells[2].Text)) * 100;
                e.Row.Cells[6].Text = dconv.ToString("0.00");
                

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[1].Text = "<font color='red'><strong>Total</strong></font>";
                e.Row.Cells[2].Text = "<font color='red'><strong>" + totrotas.ToString() + "</strong></font>";
                e.Row.Cells[3].Text = "<font color='red'><strong>" + totaguard.ToString() + "</strong></font>";
                e.Row.Cells[4].Text = "<font color='red'><strong>" + totcheck.ToString() + "</strong></font>";
                e.Row.Cells[5].Text = "<font color='red'><strong>" + totagend.ToString() + "</strong></font>";

                dconv = (Convert.ToDouble(totcheck) / Convert.ToDouble(totrotas)) * 100;
                e.Row.Cells[6].Text = "<font color='red'><strong>" + dconv.ToString("0.00") + "</strong></font>";

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_rotas_operacao.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_rotas"]);
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
    /// Rel_Rotas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Rotas(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            DataRow orow;

            //cria a estrutura da tabela
            DataTable otable = new DataTable("GATRel_Rotas");
            otable.Columns.Add("Nome");
            otable.Columns.Add("Rotas");
            otable.Columns.Add("Aguardando");
            otable.Columns.Add("Checkin");
            otable.Columns.Add("Agendamento");
            otable.Columns.Add("porc");
            

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT idcodusuario,nome FROM TEL_MP_GATUsuario(NOLOCK)WHERE Habilitado = 1 
                            AND     IDCodUsuario IN(SELECT IDCodUsuario FROM tel_mp_GATUsuarioPerfil(NOLOCK) 
						WHERE IDCodPerfil IN(SELECT IDCodPerfil FROM tel_mp_GATPerfil(NOLOCK) WHERE IDPermissao = 5))";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data
                    //ssql = ssql + " and DataUltStatus between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @" order by nome";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    //Loop por usuario
                    while (dr.Read())
                    {
                        //Captura id do usuario
                        string id_atual = dr["idcodusuario"].ToString();

                        //Trata datas
                        string dtTrabini = "";
                        string dtTrabFim = "";

                        dtTrabini = dtini;
                        DateTime dtTrab = Convert.ToDateTime(dtTrabini);
                        DateTime dtIni = Convert.ToDateTime(dtini);
                        DateTime dtFim = Convert.ToDateTime(dtfim);

                        //loop por data
                        for (dtTrab = dtIni; dtTrab <= dtFim; dtTrab = dtTrab.AddDays(1))
                        {
                            //trata data trab 
                            dtTrabini = dtTrab.Year.ToString() + "-" + dtTrab.Month.ToString() + "-" + dtTrab.Day.ToString() + " 00:00:00";
                            dtTrabFim = dtTrab.Year.ToString() + "-" + dtTrab.Month.ToString() + "-" + dtTrab.Day.ToString() + " 23:59:59";

                            gergatlink ogat2 = new gergatlink();
                            string ssql2 = "";

                            //query
                            using (SqlConnection cn2 = ogat.abre_cn())
                            {
                                using (SqlCommand cmd2 = new SqlCommand())
                                {
                                    ssql2 = @"select 
		                                                a.IDCodUsuario 'ID',
		                                                a.Nome,
		                                                'Rotas' = (select count (1)from TEL_MP_Roteiro (nolock)where IdCodRota = b.IdCodRota),
		                                                'Aguardando' = (select count(1) from TEL_MP_Roteiro (nolock) where IdcodStatus = 1 and IdCodRota = b.IdCodRota), 
		                                                'Agendamento' = (select count (1)from TEL_MP_Roteiro (nolock)where IdcodStatus = 3 and IdCodRota = b.IdCodRota and IdCodUsuario = a.IdCodUsuario),
		                                                'Checkin' = (select count(1) from TEL_MP_Roteiro (nolock) where IdcodStatus <> 1 and IdCodRota = b.IdCodRota and IdCodUsuario = a.IdCodUsuario ),
		                                                'porc' = ''

                                                from	TEL_MP_GATUsuario a (nolock),
		                                                TEL_MP_Rota_Usuario b(nolock)

                                                where	a.IDCodUsuario = b.IdCodUsuario
                                                and		a.Habilitado = 1 ";

                                    ssql2 = ssql2 + " and a.idcodusuario = " + id_atual;

                                    if (sperfil.Trim() != "")
                                    {
                                        ssql2 = ssql2 + "";
                                    }

                                    if (sequipe.Trim() != "")
                                    {
                                        ssql2 = ssql2 + "";
                                    }

                                    //ssql2 = ssql2 + " and a.DataTempo between '" + dtTrabini + "' and '" + dtTrabFim + "'";

                                    cmd.Connection = cn2;
                                    cmd.CommandText = ssql2;
                                    cmd.CommandType = CommandType.Text;

                                    SqlDataReader dr2 = cmd.ExecuteReader();

                                    while (dr2.Read())
                                    {
                                        //insere informacao inicial
                                        orow = otable.NewRow();

                                        //insere as linhas com as informacoes
                                        orow["Nome"] = dr2["Nome"].ToString();
                                        orow["Rotas"] = dr2["Rotas"].ToString();
                                        orow["Aguardando"] = dr2["Aguardando"].ToString();
                                        orow["Checkin"] = dr2["Checkin"].ToString();
                                        orow["Agendamento"] = dr2["Agendamento"].ToString();
                                        orow["porc"] = "";

                                        otable.Rows.Add(orow);

                                    }

                                }
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
}