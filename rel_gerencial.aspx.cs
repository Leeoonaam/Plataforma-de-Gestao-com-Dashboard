using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class rel_gerencial : System.Web.UI.Page
{

    public string _sperfil = "";
    public string _sequipe = "";
    public string _susuario = "";
    public string _sproduto = "";
    public string _sdtini = "";
    public string _sdtfim = "";
    public string _sdtini_min = "";
    public string _sdtfim_min = "";

    public string sHtmlCompleto = "";

    // VARIAVEIS PARA CALCULO DE RODA PÉ --> PRODUTO

    int iTotalVendedores = 0;
    int iTotalLogado = 0;
    int iTotalCheckin = 0;
    int iTotalCheckout = 0;
    int iTotalLogout = 0;
    int iTotalTrackin = 0;

    // VARIAVEIS PARA CALCULO DE RODA PÉ --> EQUIPE

    int iTotalVendedores_Equipe = 0;
    int iTotalLogado_Equipe = 0;
    int iTotalCheckin_Equipe = 0;
    int iTotalCheckout_Equipe = 0;
    int iTotalLogout_Equipe = 0;
    int iTotalTrackin_Equipe = 0;

    // VARIAVEIS PARA CALCULO DE RODA PÉ --> USUARIO

    int iTotalVendedores_Usuario = 0;
    int iTotalLogado_Usuario = 0;
    int iTotalCheckin_Usuario = 0;
    int iTotalCheckout_Usuario = 0;
    int iTotalLogout_Usuario = 0;
    int iTotalTrackin_Usuario = 0;

    string sIds_Usuarios = "";
    string sIds_Equipes = "";
    string sIds_Perfis = "";

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

                    sdml = "select * from tel_mp_gatperfil (nolock) where idpermissao = 7 and habilitado = 1 ";

                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbPerfil_Filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbPerfil_Filtro.Items.Add(oitem);

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

            svig = "";
            foreach (ListItem obj in cmbPerfil_Filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sperfil = _sperfil + svig + obj.Value;
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

            // LISTA DE PRODUTO
            gdw_Produto.Columns[1].Visible = true;
            Rel_Produto(_sdtini, _sdtfim);
            gdw_Produto.Columns[1].Visible = false;

            // LISTA DE EQUIPE
            gdw_Equipe.Columns[1].Visible = true;
            Rel_Equipe(_sdtini, _sdtfim);
            gdw_Equipe.Columns[1].Visible = false;


            Rel_Usuario(_sequipe, _sperfil,_susuario, _sdtini, _sdtfim);


            //graficos
            //   Literal1.Text = @"<script type='text/javascript'>
            //window.onload = function () {  ";

            //   Literal1.Text = Literal1.Text + @"}
            //   </script>";

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

                ////FILTRO
                Carrega_Lista_Filtro_Equipe("");
                Carrega_Lista_Filtro_Perfil("");
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
    /// Rel_Produto
    /// </summary>
    /// <param name="sPerfil"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Produto(string dtini, string dtfim)
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
                    ssql = " SELECT	A.IDCodPerfil,";
                    ssql = ssql + " A.Perfil AS 'DESCRICAO',";
                    ssql = ssql + "'TOTAL_VENDEDORES' = (SELECT COUNT(1) FROM tel_mp_GATUsuarioPerfil (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil),";
                    ssql = ssql + "'TOTAL_VENDEDORES_LOGADOS' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil AND IDCodTipoTempo = 1 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_CHECKIN' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil AND IDCodTipoTempo = 3 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_CHECKOUT' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil AND IDCodTipoTempo = 4 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_LOGOUT' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil AND IDCodTipoTempo = 2 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_TRACKIN' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "')";
                    ssql = ssql + @" FROM   tel_mp_GATPerfil A (NOLOCK)
                             WHERE	A.IDPermissao = 7
                             AND		A.Habilitado = 1";


                    ssql = ssql + " order by A.DESCRICAO";


                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;
                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_Produto.DataSource = dr;
                    gdw_Produto.DataBind();

                }
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// Rel_Equipe
    /// </summary>
    /// <param name="sPerfil"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Equipe(string dtini, string dtfim)
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
                    ssql = " SELECT	A.IDCodPerfil,";
                    ssql = ssql + " A.Perfil AS 'DESCRICAO',";
                    ssql = ssql + "'TOTAL_VENDEDORES' = (SELECT COUNT(1) FROM tel_mp_GATUsuarioPerfil (NOLOCK) WHERE IDCodPerfil = A.IDCodPerfil),";
                    ssql = ssql + "'TOTAL_VENDEDORES_LOGADOS' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodEquipe = A.IDCodPerfil AND IDCodTipoTempo = 1 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_CHECKIN' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodEquipe = A.IDCodPerfil AND IDCodTipoTempo = 3 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_CHECKOUT' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodEquipe = A.IDCodPerfil AND IDCodTipoTempo = 4 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_VENDEDORES_LOGOUT' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodEquipe = A.IDCodPerfil AND IDCodTipoTempo = 2 AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "'),";
                    ssql = ssql + "'TOTAL_TRACKIN' = (SELECT COUNT(1) FROM tel_mp_GATTEMPOUSUARIO (NOLOCK) WHERE IDCodEquipe = A.IDCodPerfil AND DataTempo BETWEEN '" + dtini + "' AND '" + dtfim + "')";
                    ssql = ssql + @" FROM   tel_mp_GATPerfil A (NOLOCK)
                             WHERE	A.IDPermissao = 6
                             AND	A.Habilitado = 1";


                    ssql = ssql + " order by A.DESCRICAO";

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ssql;
                    SqlDataReader dr = cmd.ExecuteReader();

                    gdw_Equipe.DataSource = dr;
                    gdw_Equipe.DataBind();

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
    /// <param name="sequipe"></param>
    /// <param name="sperfil"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Usuario(string sequipe, string sperfil,string susuario, string dtini, string dtfim)
    {
        try
        {

            gergatlink obj = new gergatlink();

            string ssql = @"SELECT  A.IDCodUsuario,
                                    a.Nome,
            		                'Login' = (select count(1) from tel_mp_GATTempoUsuario (nolock) where IDCodUsuario = a.IDCodUsuario and IDCodTipoTempo = 1 and DataTempo between '" + dtini + "' and '" + dtfim + "'),";
                    ssql = ssql + " 'Checkin' = (select count(1) from tel_mp_GATTempoUsuario (nolock) where IDCodUsuario = a.IDCodUsuario and IDCodTipoTempo = 3 and DataTempo between '" + dtini + "' and '" + dtfim + "'),";
                    ssql = ssql + " 'Chekout' = (select count(1) from tel_mp_GATTempoUsuario (nolock) where IDCodUsuario = a.IDCodUsuario and IDCodTipoTempo = 4 and DataTempo between '" + dtini + "' and '" + dtfim + "'),";
                    ssql = ssql + " 'Logout' = (select count(1) from tel_mp_GATTempoUsuario (nolock) where IDCodUsuario = a.IDCodUsuario and IDCodTipoTempo = 2 and DataTempo between '" + dtini + "' and '" + dtfim + "'),";
                    ssql = ssql + " 'Treckin' = (select count(1) from tel_mp_GATTempoUsuario (nolock) where IDCodUsuario = a.IDCodUsuario and DataTempo between '" + dtini + "' and '" + dtfim + "')";
            ssql = ssql + " FROM   tel_mp_GATUsuario A(NOLOCK)";
            ssql = ssql + " WHERE   Habilitado = 1";
            ssql = ssql + " AND     IDCodUsuario IN(SELECT IDCodUsuario FROM tel_mp_GATUsuarioPerfil(NOLOCK) WHERE IDCodPerfil IN(SELECT IDCodPerfil FROM tel_mp_GATPerfil(NOLOCK) WHERE IDPermissao <> 5))";

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

                    gdw_Usuarios.DataSource = dr;
                    gdw_Usuarios.DataBind();
        
                }
            }



        }
        catch (Exception err)
        {
            Erro("Erro " + err.Message);
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
            ////Build the Text file data.
            //string txt = string.Empty;

            //foreach (TableCell cell in gdw_dados.HeaderRow.Cells)
            //{
            //    //Add the Header row for Text file.
            //    txt += cell.Text.Replace("&nbsp;", "") + ";";
            //}

            ////Add new line.
            //txt += "\r\n";

            //foreach (GridViewRow row in gdw_dados.Rows)
            //{
            //    foreach (TableCell cell in row.Cells)
            //    {
            //        //Add the Data rows.
            //        txt += cell.Text + ";";
            //    }

            //    //Add new line.
            //    txt += "\r\n";
            //}

            //Download the Text file.
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=stt_rel_gerencial.txt");
            Response.AddHeader("content-disposition", "attachment;filename=stt_rel_gerencial.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_ger"]);
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
    /// gdw_dados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_Produto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (sIds_Perfis != "")
                {
                    sIds_Perfis = sIds_Perfis + ",";
                }

                // PEGA O TOTAL DE VENDEDORES
                iTotalVendedores = iTotalVendedores + Convert.ToInt32(e.Row.Cells[3].Text);
                iTotalLogado = iTotalLogado + Convert.ToInt32(e.Row.Cells[4].Text);
                iTotalCheckin = iTotalCheckin + Convert.ToInt32(e.Row.Cells[5].Text);
                iTotalCheckout = iTotalCheckout + Convert.ToInt32(e.Row.Cells[6].Text);
                iTotalLogout = iTotalLogout + Convert.ToInt32(e.Row.Cells[7].Text);
                iTotalTrackin = iTotalTrackin + Convert.ToInt32(e.Row.Cells[8].Text);

                if (e.Row.Cells[4].Text != "0")
                {
                    e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + e.Row.Cells[1].Text + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[4].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[5].Text != "0")
                {
                    e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + e.Row.Cells[1].Text + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[5].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[6].Text != "0")
                {
                    e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + e.Row.Cells[1].Text + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[6].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[7].Text != "0")
                {
                    e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + e.Row.Cells[1].Text + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[7].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[8].Text != "0")
                {
                    e.Row.Cells[8].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + e.Row.Cells[1].Text + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[8].Text + "</strong></font></a>";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[0].Text = "<font color='red'><strong>TOTAL:</strong></font>";

                e.Row.Cells[3].Text = iTotalVendedores.ToString(); ;
                e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + sIds_Perfis + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='red'><strong>" + iTotalLogado + "</strong></font></a>";
                e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + sIds_Perfis + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='red'><strong>" + iTotalCheckin + "</strong></font></a>";
                e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + sIds_Perfis + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='red'><strong>" + iTotalCheckout + "</strong></font></a>";
                e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + sIds_Perfis + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='red'><strong>" + iTotalLogout + "</strong></font></a>";
                e.Row.Cells[8].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + sIds_Perfis + "&_sequipe=" + _sequipe + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='red'><strong>" + iTotalTrackin + "</strong></font></a>";


            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_Equipe_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_Equipe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        gergatlink obj = new gergatlink();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (sIds_Equipes != "")
                {
                    sIds_Equipes = sIds_Equipes + ",";
                }

                // PEGA O TOTAL DE VENDEDORES
                iTotalVendedores_Equipe = iTotalVendedores_Equipe + Convert.ToInt32(e.Row.Cells[3].Text);
                iTotalLogado_Equipe = iTotalLogado_Equipe + Convert.ToInt32(e.Row.Cells[4].Text);
                iTotalCheckin_Equipe = iTotalCheckin_Equipe + Convert.ToInt32(e.Row.Cells[5].Text);
                iTotalCheckout_Equipe = iTotalCheckout_Equipe + Convert.ToInt32(e.Row.Cells[6].Text);
                iTotalLogout_Equipe = iTotalLogout_Equipe + Convert.ToInt32(e.Row.Cells[7].Text);
                iTotalTrackin_Equipe = iTotalTrackin_Equipe + Convert.ToInt32(e.Row.Cells[8].Text);
                
                if (e.Row.Cells[4].Text != "0")
                {
                    e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + e.Row.Cells[1].Text + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[4].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[5].Text != "0")
                {
                    e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + e.Row.Cells[1].Text + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[5].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[6].Text != "0")
                {
                    e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + e.Row.Cells[1].Text + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[6].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[7].Text != "0")
                {
                    e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + e.Row.Cells[1].Text + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[7].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[8].Text != "0")
                {
                    e.Row.Cells[8].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + e.Row.Cells[1].Text + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[8].Text + "</strong></font></a>";
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[3].Text = iTotalVendedores_Equipe.ToString(); ;

                e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + sIds_Equipes + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='red'><strong>" + iTotalLogado_Equipe + "</strong></font></a>";
                e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + sIds_Equipes + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='red'><strong>" + iTotalCheckin_Equipe + "</strong></font></a>";
                e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + sIds_Equipes + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='red'><strong>" + iTotalCheckout_Equipe + "</strong></font></a>";
                e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + sIds_Equipes + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='red'><strong>" + iTotalLogout_Equipe + "</strong></font></a>";
                e.Row.Cells[8].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + sIds_Equipes + "&_susuario=" + _susuario + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='red'><strong>" + iTotalTrackin_Equipe + "</strong></font></a>";
                
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    /// <summary>
    /// gdw_Usuarios_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdw_Usuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        gergatlink obj = new gergatlink();
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (sIds_Usuarios != "")
                {
                    sIds_Usuarios = sIds_Usuarios + ",";
                }

                // PEGA O TOTAL DE VENDEDORES
                iTotalLogado_Usuario = iTotalLogado_Usuario + Convert.ToInt32(e.Row.Cells[3].Text);
                iTotalCheckin_Usuario = iTotalCheckin_Usuario + Convert.ToInt32(e.Row.Cells[4].Text);
                iTotalCheckout_Usuario = iTotalCheckout_Usuario + Convert.ToInt32(e.Row.Cells[5].Text);
                iTotalLogout_Usuario = iTotalLogout_Usuario + Convert.ToInt32(e.Row.Cells[6].Text);
                iTotalTrackin_Usuario = iTotalTrackin_Usuario + Convert.ToInt32(e.Row.Cells[7].Text);

                sIds_Usuarios = sIds_Usuarios + e.Row.Cells[1].Text;

                // COLOCAR O HIPERLINK PARA OS DETALHES

                if (e.Row.Cells[3].Text != "0")
                {
                    e.Row.Cells[3].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + e.Row.Cells[1].Text + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[3].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[4].Text != "0")
                {
                    e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + e.Row.Cells[1].Text + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[4].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[5].Text != "0")
                {
                    e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + e.Row.Cells[1].Text + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[5].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[6].Text != "0")
                {
                    e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + e.Row.Cells[1].Text + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[6].Text + "</strong></font></a>";
                }

                if (e.Row.Cells[7].Text != "0")
                {
                    e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + e.Row.Cells[1].Text + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='black'><strong>" + e.Row.Cells[7].Text + "</strong></font></a>";
                }
                
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "<font color='red'><strong>TOTAL:</strong></font>";
                e.Row.Cells[3].Text = "<font color='red'><strong>" + iTotalLogado_Usuario + "</strong></font>";
                e.Row.Cells[4].Text = "<font color='red'><strong>" + iTotalCheckin_Usuario + "</strong></font>";
                e.Row.Cells[5].Text = "<font color='red'><strong>" + iTotalCheckout_Usuario + "</strong></font>";
                e.Row.Cells[6].Text = "<font color='red'><strong>" + iTotalLogout_Usuario + "</strong></font>";
                e.Row.Cells[7].Text = "<font color='red'><strong>" + iTotalTrackin_Usuario + "</strong></font>";

                // COLOCAR O HIPERLINK PARA OS DETALHES
                e.Row.Cells[3].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + sIds_Usuarios + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1' target='_blank'>" + "<font color='red'><strong>" + e.Row.Cells[3].Text + "</strong></font></a>";
                e.Row.Cells[4].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + sIds_Usuarios + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=3' target='_blank'>" + "<font color='red'><strong>" + e.Row.Cells[4].Text + "</strong></font></a>";
                e.Row.Cells[5].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + sIds_Usuarios + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=4' target='_blank'>" + "<font color='red'><strong>" + e.Row.Cells[5].Text + "</strong></font></a>";
                e.Row.Cells[6].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + sIds_Usuarios + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=2' target='_blank'>" + "<font color='red'><strong>" + e.Row.Cells[6].Text + "</strong></font></a>";
                e.Row.Cells[7].Text = "<a style='text-decoration:none; color:black;' href='DetalhesUsuario_Gerencial.aspx?_sperfil=" + _sperfil + "&_sequipe=" + _sequipe + "&_susuario=" + sIds_Usuarios + "&_sproduto=" + _sproduto + "&_sdtini=" + _sdtini + "&_sdtfim=" + _sdtfim + "&_sstatusbpm=1,3,4,2' target='_blank'>" + "<font color='red'><strong>" + e.Row.Cells[7].Text + "</strong></font></a>";
            }
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }
}