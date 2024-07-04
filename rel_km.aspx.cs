using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_km : System.Web.UI.Page
{
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public int tTotal = 0;

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

                    sdml = "select * from tel_mp_gatperfil (nolock) where idpermissao = 6 and habilitado = 1";
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


            Rel_kms(_sperfil, _sequipe,_susuario,_sdtini, _sdtfim);


            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            Grafico_km(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

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
    /// Rel_Kms
    /// </summary>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Kms(string dtini, string dtfim)
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

		                        A.IdCodTempo 'ID',
		                        B.IDCodUsuario,
		                        B.Nome,
		                        C.TipoTempo,
		                        A.Latitude,
		                        A.Longitude,
		                        A.Endereco,
		                        isnull(A.Calc_KM,'') 'Kms',
		                        isnull(A.Tempo_Estimado,'')'Tempo',
		                        A.DataTempo

                        FROM	TEL_MP_GATTempoUsuario A (NOLOCK),
		                        TEL_MP_GATUsuario B (NOLOCK),
		                        TEL_MP_GATTipoTempo C (NOLOCK)

                        WHERE	A.IdCodUsuario = B.IDCodUsuario
                        AND		A.IdCodTipoTempo = C.IdCodTipoTempo ";

                    //data
                    ssql = ssql + " and A.datatempo between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + " order by a.IdCodUsuario, a.DataTempo";


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
                Session["txt_kms"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_kms"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_kms"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_kms"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_kms"] += "\r\n";

                if (e.Row.Cells[1].Text == "&nbsp;")
                {
                    dconv = (Convert.ToDouble(e.Row.Cells[6].Text) / 1000);

                    e.Row.Cells[5].Text = "<font color='red'><strong>Total</strong></font>";
                    e.Row.Cells[6].Text = "<font color='red'><strong>"+ dconv + " Km</strong></font>";
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

               

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_kms_operacao.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_kms"]);
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
    public void Grafico_km(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            double dconv = 0;

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string ssql = @"select	b.Nome,
		                                sum(cast(REPLACE(isnull(a.Calc_KM,0),',','.')as decimal(10,2)))'Kms'
                                from	TEL_MP_GATTempoUsuario a (nolock),
		                                TEL_MP_GATUsuario b(nolock)

                                where	a.IdCodUsuario = b.IDCodUsuario ";
                                
                //data
                ssql = ssql + " and a.DataTempo between '" + dtini + "' and '" + dtfim + "'";

                ssql = ssql + @" group by	b.Nome

                                order by Kms desc";


                using (SqlDataAdapter da = new SqlDataAdapter(ssql, cn))
                {

                    //dataset
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //cabecalho do grafico
                    Literal1.Text = Literal1.Text + @" 
                        var chart2 = new CanvasJS.Chart('div_graf_kms',
                                       {
                                                      title:{
                                                                    text: 'Km Rodado por Usuário'
                                                      },
                                                      legend: {
                                                                    maxWidth: 350,
                                                                    itemWidth: 120
                                                      },
animationEnabled: true,
                                                      data: [
                                                      {
                                                                    showInLegend: true,
                                                                    legendText: '{indexLabel}',
                                                                    type: 'pie',
                                                                    dataPoints: [";


                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["Kms"].ToString().Replace(",",".") + " , label: '" + item["Nome"].ToString() + "' }";
                        svig = ",";
                        

                    }

                    //fim do grafico
                    Literal1.Text = Literal1.Text + @"]
                                              }
                                              ]
                               });

                               chart2.render();";

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }


    /// <summary>
    /// Rel_Pausas
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="slogin"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_kms(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";
            int total = 0;

            DataRow orow;

            //cria a estrutura da tabela
            DataTable otable = new DataTable("GATRel_Km");
            otable.Columns.Add("ID");
            otable.Columns.Add("Nome");
            otable.Columns.Add("TipoTempo");
            otable.Columns.Add("Latitude");
            otable.Columns.Add("Longitude");
            otable.Columns.Add("Endereco");
            otable.Columns.Add("Kms");
            otable.Columns.Add("Tempo");
            otable.Columns.Add("DataTempo");

            using (SqlConnection cn = ogat.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"SELECT idcodusuario,nome FROM TEL_MP_GATUsuario(NOLOCK)WHERE Habilitado = 1 and IDCodUsuario IN (SELECT IDCodUsuario FROM TEL_MP_GATTempousuario(NOLOCK)where 1=1 ";

                    //usuario
                    if (susuario.Trim() != "")
                    {
                        ssql = ssql + " and idcodusuario in (" + susuario + ")";
                    }

                    //data
                    ssql = ssql + " and DataTempo between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @") 
                                        
                            order by nome";

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
                                    ssql2 = @"SELECT 

		                                        A.IdCodTempo 'ID',
		                                        B.IDCodUsuario,
		                                        B.Nome,
		                                        C.TipoTempo,
		                                        A.Latitude,
		                                        A.Longitude,
		                                        A.Endereco,
		                                        isnull(A.Calc_KM,'') 'Kms',
		                                        isnull(A.Tempo_Estimado,'')'Tempo',
		                                        A.DataTempo

                                        FROM	TEL_MP_GATTempoUsuario A (NOLOCK),
		                                        TEL_MP_GATUsuario B (NOLOCK),
		                                        TEL_MP_GATTipoTempo C (NOLOCK)

                                        WHERE	A.IdCodUsuario = B.IDCodUsuario
                                        AND		A.IdCodTipoTempo = C.IdCodTipoTempo";

                                    ssql2 = ssql2 + " and a.idcodusuario = " + id_atual;

                                    if (sperfil.Trim() !="")
                                    {
                                        ssql2 = ssql2 + "";
                                    }

                                    if (sequipe.Trim() != "")
                                    {
                                        ssql2 = ssql2 + "";
                                    }

                                    ssql2 = ssql2 + " and a.DataTempo between '" + dtTrabini + "' and '" + dtTrabFim + "'";

                                    ssql2 = ssql2 + " order by b.nome, a.DataTempo";

                                    cmd.Connection = cn2;
                                    cmd.CommandText = ssql2;
                                    cmd.CommandType = CommandType.Text;

                                    SqlDataReader dr2 = cmd.ExecuteReader();

                                    while (dr2.Read())
                                    {
                                        //insere informacao inicial
                                        orow = otable.NewRow();

                                        //insere as linhas com as informacoes
                                        orow["ID"] = dr2["ID"].ToString();
                                        orow["Nome"] = dr2["Nome"].ToString();
                                        orow["TipoTempo"] = dr2["TipoTempo"].ToString();
                                        orow["Latitude"] = dr2["Latitude"].ToString();
                                        orow["Longitude"] = dr2["Longitude"].ToString();
                                        orow["Endereco"] = dr2["Endereco"].ToString();
                                        
                                        //verifica se está vazio
                                        if (dr2["Kms"].ToString() != "")
                                        {
                                            //verifica quantidade para definição de metros ou km
                                            if (Convert.ToInt32(dr2["Kms"].ToString()) < 999)
                                            {
                                                orow["Kms"] = dr2["Kms"].ToString() + " m"; 
                                            }
                                            else
                                            {
                                                orow["Kms"] = (Convert.ToInt32(dr2["Kms"].ToString()) / 1000) + " Km";
                                            }

                                            //soma
                                            total = total + Convert.ToInt32(dr2["Kms"].ToString());

                                        }
                                        else
                                        {
                                            orow["Kms"] = 0;
                                        }


                                        if (dr2["Tempo"].ToString() != "")
                                        {
                                            if (Convert.ToInt32(dr2["Tempo"].ToString()) < 60)
                                            {
                                                orow["Tempo"] = dr2["Tempo"].ToString() + " segundos";
                                            }
                                            else if (Convert.ToInt32(dr2["Tempo"].ToString()) >= 60)
                                            {
                                                if (Convert.ToInt32(dr2["Tempo"].ToString()) == 60)
                                                {
                                                    orow["Tempo"] = "1 minuto";
                                                }
                                                else
                                                {
                                                    orow["Tempo"] = dr2["Tempo"].ToString() + " minutos";
                                                }
                                            }
                                            else
                                            {
                                                orow["Tempo"] = dr2["Tempo"].ToString()+ " horas";
                                            }

                                        }
                                        



                                        orow["DataTempo"] = dr2["DataTempo"].ToString();

                                        otable.Rows.Add(orow);

                                    }

                                }
                            }
                        }

                        //insere informacao inicial
                        orow = otable.NewRow();

                        //linha de totalizador
                        orow["ID"] = "";
                        orow["Nome"] = "";
                        orow["TipoTempo"] = "";
                        orow["Latitude"] = "";
                        orow["Longitude"] = "";
                        orow["Endereco"] = "Total";
                        orow["Kms"] = total;
                        //tTotal = tTotal + tottempo;
                        orow["Tempo"] = "";
                        orow["DataTempo"] = "";

                        otable.Rows.Add(orow);

                        total = 0;

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