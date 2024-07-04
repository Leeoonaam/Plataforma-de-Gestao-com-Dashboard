using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_callback_operador : System.Web.UI.Page
{
    //Variaveis
    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";

    double tLINHAS = 0;
    double tATENDIDAS = 0;
    double tCALL = 0;
    double tNOPTOU = 0;
    double tOPTOU = 0;
    double tNAO_OPTOU = 0;

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

            Rel_CallBack_Operador(_sperfil, _sequipe, _susuario, _sdtini, _sdtfim);

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

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_callbackoperador"] = "";

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_callbackoperador"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_callbackoperador"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tLINHAS++;

                //TOTAIS
                tATENDIDAS = tATENDIDAS + Convert.ToInt32(e.Row.Cells[3].Text);
                tCALL = tCALL + Convert.ToInt32(e.Row.Cells[4].Text);
                tOPTOU = tOPTOU + Convert.ToInt32(e.Row.Cells[5].Text);
                tNOPTOU = tNOPTOU + Convert.ToInt32(e.Row.Cells[6].Text);
                tNAO_OPTOU = tNAO_OPTOU + Convert.ToInt32(e.Row.Cells[7].Text);

                foreach (TableCell cell in e.Row.Cells)
                {
                    Session["txt_callbackoperador"] += obj.TrataHTMLASCII(cell.Text.Replace("&nbsp;", "")) + ";";
                }
                //Add new line.
                Session["txt_callbackoperador"] += "\r\n";
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[2].Text = "<font color='red'><strong>Total</strong></font>";

                e.Row.Cells[3].Text = "<font color='red'><strong>" + tATENDIDAS.ToString() + "</strong></font></a>";
                e.Row.Cells[4].Text = "<font color='red'><strong>" + tCALL.ToString() + "</strong></font></a>";
                e.Row.Cells[5].Text = "<font color='red'><strong>" + tOPTOU.ToString() + "</strong></font></a>";
                e.Row.Cells[6].Text = "<font color='red'><strong>" + tNOPTOU.ToString() + "</strong></font></a>";
                e.Row.Cells[7].Text = "<font color='red'><strong>" + tNAO_OPTOU.ToString() + "</strong></font></a>";

                //TOTAIS
                lbltotuser.Text = "Total de Operadores : " + tLINHAS.ToString();

                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(";") + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(";Total") + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(tATENDIDAS.ToString()) + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(tCALL.ToString()) + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(tOPTOU.ToString()) + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(tNOPTOU.ToString()) + ";";
                Session["txt_callbackoperador"] += obj.TrataHTMLASCII(tNAO_OPTOU.ToString()) + ";";

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
    /// Lista os dados
    /// </summary>
    /// <param name="scampanha"></param>
    /// <param name="sperfil"></param>
    /// <param name="sequipe"></param>
    /// <param name="susuario"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_CallBack_Operador(string sperfil, string sequipe, string susuario, string dtini, string dtfim)
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
		                        A.IDCodUsuario,
		                        A.NOME,
		
		                        'ATENDIDAS' = (	SELECT COUNT(1) FROM GATCliente X(NOLOCK)WHERE X.IDCodUsuario = A.IDCODUSUARIO";
                                               
                                                //data
                                                ssql = ssql + " AND X.DataInsercao between '" + dtini + "' and '" + dtfim + "'),";

                    ssql = ssql + @"'QTD_CALL' = (	SELECT COUNT(1) FROM GATCliente X(NOLOCK) WHERE X.IDCODUSUARIO = A.IDCodUsuario
						                        AND ISNULL(X.ABRIU_TELA_CALLBACK,0) = 1";

                                                //data
                                                ssql = ssql + " AND X.DataInsercao between '" + dtini + "' and '" + dtfim + "'),";

                    ssql = ssql + @"'OPTOU_FAZER' = (	SELECT COUNT(1) FROM GATCliente X(NOLOCK)WHERE X.IDCODUSUARIO = A.IDCodUsuario
						                        AND ISNULL(X.CALLBACK_OPTOUFAZER,0) = 1";

                                                //data
                                                ssql = ssql + " AND X.DataInsercao between '" + dtini + "' and '" + dtfim + "'),";


                    ssql = ssql + @"'OPTOU_NFAZER' = (	SELECT COUNT(1) FROM GATCliente X(NOLOCK)WHERE X.IDCODUSUARIO = A.IDCodUsuario
						                        AND ISNULL(X.CALLBACK_OPTOUNAOFAZER,0) = 1";

                                                //data
                                                 ssql = ssql + " AND X.DataInsercao between '" + dtini + "' and '" + dtfim + "'),";

                    ssql = ssql + @"'NAOOPTOU' = (	SELECT COUNT(1) FROM GATCliente X(NOLOCK)WHERE X.IDCODUSUARIO = A.IDCodUsuario
						                        AND ISNULL(X.CALLBACK_NAOOPTOU,0) = 1";

                                                //data
                                                ssql = ssql + " AND X.DataInsercao between '" + dtini + "' and '" + dtfim + "')";


                    ssql = ssql + @" FROM	GATUsuario A(NOLOCK)

                        WHERE	A.IDCodUsuario IN (	SELECT IDCodUsuario FROM GATUsuarioPerfil (NOLOCK)
							                        WHERE IDCodPerfil IN (SELECT IDCodPerfil FROM GATPerfil(NOLOCK)";

                        //equipe
                        if (sequipe.Trim() != "")
                        {
                            ssql = ssql + " WHERE IDPermissao = 10";
                            ssql = ssql + " and idcodperfil in (" + sequipe + ")";
                        }
                        else
                        {
                            ssql = ssql + " WHERE IDPermissao = 3";
                        }

                        //perfil
                        if (sperfil.Trim() != "")
                        {
                            ssql = ssql + " and idcodperfil in (" + sperfil + ")";
                        }

                        ssql = ssql + " ))";

                        //usuario
                        if (susuario.Trim() != "")
                        {
                            ssql = ssql + " and a.idcodusuario in (" + susuario + ")";
                        }

                    ssql = ssql + @" and a.IDCodUsuario in (select IDCodUsuario from GATTelChamadas(nolock)where 1=1";

                    //data
                    ssql = ssql + " AND DataGatChamada between '" + dtini + "' and '" + dtfim + "')";


                    ssql = ssql + @" GROUP BY	A.Nome,A.IDCodUsuario
                                     ORDER BY	ATENDIDAS DESC";

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
    protected void cmdexportar_Click(object sender, EventArgs e)
    {
        try
        {
            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=txt_callbackoperador.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_callbackoperador"]);
            Response.Flush();
            Response.End();
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}