using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_ranking_rotas : System.Web.UI.Page
{
    //variaveis
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";
    public int itot = 0;

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

            Rel_Ranking(_sequipe, _susuario, _sdtini, _sdtfim);
            
            //graficos
            Literal1.Text = @"<script type='text/javascript'>
	        window.onload = function () {  ";

            Grafico_Ranking(_sequipe, _susuario, _sdtini, _sdtfim);

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
                //FILTROS
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
    /// Rel_Ranking
    /// </summary>    
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Ranking(string sequipe, string susuario, string dtini, string dtfim)
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
                                    A.IdCodUsuario,
		                            B.Nome,
		                            COUNT(1)'TOTAL'


                            FROM	TEL_MP_GATTempoUsuario A(NOLOCK),
		                            TEL_MP_GATUsuario B(NOLOCK)

                            WHERE	A.IdCodUsuario = B.IDCodUsuario";


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

                    //data
                    ssql = ssql + " and A.datatempo between '" + dtini + "' and '" + dtfim + "'";

                    ssql = ssql + @" GROUP BY A.IdCodUsuario,
                                              B.Nome
                                     ORDER BY TOTAL DESC";

                    cmd.Connection = cn;
                    cmd.CommandText = ssql;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_dados_ranking.DataSource = dr;
                    gdw_dados_ranking.DataBind();

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
                Session["txt_ranking_visitas"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_ranking_visitas"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_ranking_visitas"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                itot = itot + Convert.ToInt32(e.Row.Cells[3].Text);

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_ranking_visitas"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_ranking_visitas"] += "\r\n";


            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "<font color='red'><strong>Total</strong></font>";

                e.Row.Cells[3].Text = "<font color='red'><strong>" + itot + "</strong></font>";

                //Add new line.
                Session["txt_fin1"] += obj.TrataHTMLASCII(";Total") + ";";                
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
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_ranking_visitas.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_ranking_visitas"]);
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
    /// Grafico_Ranking
    /// </summary>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Grafico_Ranking(string sequipe, string susuario, string dtini, string dtfim)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                string svig = "";

                string ssql = @"SELECT

		                        case when len(B.Nome) - len(replace(B.Nome,' ','')) > 0
			                            then
				                            left(B.Nome, charindex(' ', B.Nome)-1)
			                            else
				                            B.Nome
			                            end as 'Nome',
			                            COUNT(1)'TOTAL'


	                            FROM	TEL_MP_GATTempoUsuario A(NOLOCK),
			                            TEL_MP_GATUsuario B(NOLOCK)

	                            WHERE	A.IdCodUsuario = B.IDCodUsuario";

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

                //data
                ssql = ssql + " and a.datatempo between '" + dtini + "' and '" + dtfim + "'";

                ssql = ssql + @" GROUP BY Nome
                                    ORDER BY TOTAL DESC";


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
                        var chart1 = new CanvasJS.Chart('div_graf_ranking',
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
			                        
			                        type: 'column',
			                        dataPoints: [";


                    double ivalor = 0;
                    string[] separators = { " " };

                    foreach (DataRow item in ds.Tables[0].Rows)
                    {

                        ivalor = Convert.ToInt32(item["TOTAL"].ToString());

                        //dados
                        Literal1.Text = Literal1.Text + svig + "{ y: " + item["TOTAL"].ToString() + " , label: '" + item["Nome"].ToString() + "' }";
                        svig = ",";

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