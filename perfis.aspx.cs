using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class perfis : System.Web.UI.Page
{

    public string _snome = "";
    public string _spermissao = "";


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
    /// Carrega_Lista_Filtro_Permissao
    /// </summary>
    /// <param name="svalor"></param>
    public void Carrega_Lista_Filtro_Permissao(string svalor)
    {
        try
        {
            gergatlink obj = new gergatlink();

            using (SqlConnection cn = obj.abre_cn())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sdml = "";

                    sdml = "select * from TEL_MP_GATPermissao (nolock) ";
                    sdml = sdml + " order by permissao";

                    cmd.Connection = cn;
                    cmd.CommandText = sdml;
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dr = cmd.ExecuteReader();

                    ListItem oitem;

                    cmbpermissao_filtro.Items.Clear();
                    while (dr.Read())
                    {
                        oitem = new ListItem();
                        oitem.Text = dr["permissao"].ToString();
                        oitem.Value = dr["idpermissao"].ToString();

                        cmbpermissao_filtro.Items.Add(oitem);

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
            if (txtperfil_filtro.Text != "")
            {
                _snome = _snome + svig + txtperfil_filtro.Text;
                svig = ",";
            }

            //permissao
            svig = "";
            foreach (ListItem obj in cmbpermissao_filtro.Items)
            {
                if (obj.Selected == true)
                {
                    _spermissao = _spermissao + svig + obj.Value;
                    svig = ",";
                }
            }

            Rel_Operacao(_snome, _spermissao);

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
            Response.AddHeader("content-disposition", "attachment;filename=stt_perfis.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(Session["txt_perfis"]);
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
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";

                Session["txt_perfis"] = "";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        Session["txt_perfis"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_perfis"] += "\r\n";

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].CssClass = "col_acao";
                e.Row.Cells[1].CssClass = "col_acao";
                
                //qtd de linhas
                TotLinhas++;
                
                //Alterar
                e.Row.Cells[0].Text = "<a href='perfis_dados.aspx?id=" + e.Row.Cells[2].Text + "' target='_self'><img src='imagens/alterar.png'/> </a>";

                //Excluir
                e.Row.Cells[1].Text = @"<a href=""javascript:deleta('perfis_dados.aspx?acao=del&id=";
                e.Row.Cells[1].Text = e.Row.Cells[1].Text + e.Row.Cells[2].Text + @"')"" ><img src=""imagens/delete.png""/> </a>";

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (i > 1)
                    {
                        Session["txt_perfis"] += obj.TrataHTMLASCII(e.Row.Cells[i].Text.Replace("&nbsp;", "")) + ";";
                    }
                }

                //Add new line.
                Session["txt_perfis"] += "\r\n";
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
                Carrega_Lista_Filtro_Permissao("");
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
    public void Rel_Operacao(string snome, string spermissao)
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
		                    a.idcodperfil 'ID',
		                    a.Perfil,
		                    b.Permissao
                                      
                    from	TEL_MP_GATPerfil a (nolock),
                            TEL_MP_GATPermissao b (nolock)

					where   a.habilitado=1 
                    and     a.idpermissao = b.idpermissao";


                    //nome
                    if (snome.Trim() != "")
                    {
                        ssql = ssql + " and a.perfil like ('%" + snome + "%')";
                    }
                    
                    //Permissao
                    if (spermissao.Trim() != "")
                    {
                        ssql = ssql + " and a.idpermissao in (" + spermissao + ")";
                    }
                    

                    ssql = ssql + " order by a.perfil";

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
            Response.Redirect("perfis_dados.aspx", false);
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