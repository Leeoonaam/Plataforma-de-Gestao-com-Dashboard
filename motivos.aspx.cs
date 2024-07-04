using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class usuarios : System.Web.UI.Page
{

    public string _snome = "";
    public string _slogin = "";
    public string _smatricula = "";
    public string _sperfil = "";
    public string _sequipe = "";


    int TotLinhas = 0;

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

    //// aki eh a area onde eu carrego os filtros.... os combos. vou comentar tah, vc dp coloca os precisarem
    /// pq esses estao voltados para a tela de chamados


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

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where habilitado=1 and idpermissao <> 6 ";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbPerfil_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbPerfil_filtro.Items.Add(oitem);

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

                    sdml = "select * from TEL_MP_GATPerfil (nolock) where habilitado=1 and idpermissao = 6 ";
                    sdml = sdml + " order by perfil";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbEquipe_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["perfil"].ToString();
                        oitem.Value = dr["idcodperfil"].ToString();

                        cmbEquipe_filtro.Items.Add(oitem);

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


            //nome
            svig = "";
            if (txtnome_filtro.Text != "")
            {
                _snome = _snome + svig + txtnome_filtro.Text;
                svig = ",";
            }


            //Login
            svig = "";
            if (txtlogin_filtro.Text != "")
            {
                _slogin = _slogin + svig + txtlogin_filtro.Text;
                svig = ",";
            }


            //Matricula
            svig = "";
            if (txtmatricula_filtro.Text != "")
            {
                _smatricula = _smatricula + svig + txtmatricula_filtro.Text;
                svig = ",";
            }

            //perfil
            svig = "";
            foreach (ListItem obj in cmbPerfil_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sperfil = _sperfil + svig + obj.Value;
                    svig = ",";
                }
            }


            //Equipe
            svig = "";
            foreach (ListItem obj in cmbEquipe_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _sequipe = _sequipe + svig + obj.Value;
                    svig = ",";
                }
            }

            


            Rel_Operacao(_snome, _slogin, _smatricula, _sperfil, _sequipe);

            lbl_tot.Text = TotLinhas.ToString();

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_usuarios.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_usuarios"]);
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
    protected void gdw_dados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gergatlink obj = new gergatlink();

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Session["txt_usuarios"] = "";

                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";

                Session["txt_usuarios"] = "";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1 && i < 13)
                    {
                        if (i == 7)
                        {
                            i = i + 3;
                        }

                        Session["txt_usuarios"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_usuarios"] += "\r\n";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";
                e.Row.Cells[10].CssClass = "col_acao";
                
                //qtd de linhas
                TotLinhas++;
                
                //Alterar
                e.Row.Cells[0].Text = "<a href='usuarios_dados.aspx?id=" + e.Row.Cells[2].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";

                //Excluir
                e.Row.Cells[1].Text = @"<a href=""javascript:deleta('usuarios_dados.aspx?acao=del&id=";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text + e.Row.Cells[2].Text + @"')"" ><img src=""imagens/delete.png""/> </a>";

                // EMUSO
                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "<img src='imagens/disponivel.png'/>";
                    e.Row.Cells[9].ToolTip = "O Usuário não está em uso";
                }
                else if (e.Row.Cells[9].Text == "1")
                {
                    e.Row.Cells[9].Text = "<img src='imagens/logado.png'/>";
                    e.Row.Cells[9].ToolTip = "O Usuário não está em uso";
                }
                
                //liberar
                e.Row.Cells[13].Text = @"<a href=""javascript:acao('usuarios_dados.aspx?acao=liberar&id=";
                e.Row.Cells[13].Text = e.Row.Cells[13].Text + e.Row.Cells[2].Text + @"')"" ><img src=""imagens/retrab.png""/> </a>";

                e.Row.Cells[14].Text = "<a style='text-decoration:none; color:black;' href='DetalhesMapaUsuario.aspx?_id=" + e.Row.Cells[2].Text + "' target='_blank'><img src='imagens/mapa.png'/></a>";
                e.Row.Cells[14].ToolTip = "Abrir Mapa com localização do usuário";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1 && i < 13)
                    {
                        if (i == 7)
                        {
                            i = i + 3;
                        }

                        Session["txt_usuarios"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_usuarios"] += "\r\n";

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

            if (IsPostBack == false)
            {
                Carrega_Lista_Filtro_Equipe("");
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
    /// Rel_Operacao
    /// </summary>
    /// <param name="salerta"></param>
    /// <param name="splace"></param>
    /// <param name="sequipamento"></param>
    /// <param name="dtini"></param>
    /// <param name="dtfim"></param>
    public void Rel_Operacao(string snome, string slogin, string smatricula, string sperfil, string sequipe)
    {
        try
        {
            gergatlink ogat = new gergatlink();
            string ssql = "";

            using (SqlConnection cn = ogat.abre_cn())
            {

                //US LANG
                using (SqlCommand cmd_Lang = new SqlCommand())
                {
                    cmd_Lang.Connection = cn;
                    cmd_Lang.CommandText = "SET LANGUAGE us_english;";
                    cmd_Lang.CommandType = CommandType.Text;

                    cmd_Lang.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand())
                {

                    //monta select
                    ssql = @"select	'Editar'='', 
		                    'Excluir'='',
		                    a.idcodusuario 'ID',
		                    a.Nome,
		                    a.Login,
                            b.status 'Status',
                            a.DataUltStatus,
                            a.Bloqueado,
                            a.Logado,
                            a.Emuso,
                            a.Latitude,
                            a.Longitude,
                            a.Endereco,
                            'Liberar'='',
                            'Mapa' = ''
                                      
                    from	TEL_MP_GATUsuario a (nolock),
                            TEL_MP_GATStatusUsuario b (nolock)

					where   a.habilitado=1 
                    and     a.codstatususer = b.codstatususer";


                    //nome
                    if (snome.Trim() != "")
                    {
                        ssql = ssql + " and a.nome like ('%" + snome + "%')";
                    }


                    //login
                    if (slogin.Trim() != "")
                    {
                        ssql = ssql + " and a.login like ('%" + slogin + "%')";
                    }


                    //matricula
                    if (smatricula.Trim() != "")
                    {
                        ssql = ssql + " and a.Matricula like ('%" + smatricula + "%')";
                    }


                    //Perfil
                    if (sperfil.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (select idcodusuario from TEL_MP_GATUsuarioPerfil (nolock) where idcodperfil in (" + sperfil + "))";
                    }


                    //equipe
                    if (sequipe.Trim() != "")
                    {
                        ssql = ssql + " and a.idcodusuario in (select idcodusuario from TEL_MP_GATUsuarioPerfil (nolock) where idcodperfil in (" + sequipe + "))";
                    }
                    

                    ssql = ssql + " order by a.nome";

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
    /// cmdnovo_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cmdnovo_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("usuarios_dados.aspx", false);
        }
        catch (Exception err)
        {
            Erro(err.Message);
        }
    }

    protected void gdw_dados_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }


}